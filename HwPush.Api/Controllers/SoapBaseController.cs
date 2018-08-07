using HwPush.Api.Models;
using System.Linq;
using System.Web.Http;
using HwPush.ApiBase;
using Newtonsoft.Json;
using System;

namespace HwPush.Api.Controllers
{
    public class SoapBaseController : ApiController
    {
        protected HwPushContext HwPushCon = new HwPushContext();
        protected string Ver
        {
            get
            {
                try
                {
                    var resVer = Request.Headers.GetValues("Ver");
                    if (resVer.Count() > 0)
                    {
                        return RsaHelper.RSACrypto.Decrypt(PrivateKey, resVer.FirstOrDefault());
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }
        }
        protected long QQNumber
        {
            get
            {
                try
                {
                    var resqqnumber = Request.Headers.GetValues("QQ");
                    if (resqqnumber.Count() > 0)
                    {
                        return long.Parse(RsaHelper.RSACrypto.Decrypt(PrivateKey, resqqnumber.FirstOrDefault()));
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch { return 0; }
            }
        }
        protected string ResultStr
        {
            get
            {
                try
                {
                    string result = Request.Content.ReadAsStringAsync().Result;
                    return RsaHelper.RSACrypto.Decrypt(PrivateKey, result);
                }
                catch (Exception ee)
                {
                    return "";
                }
            }
        }
        protected string PublicKey
        {
            get
            {
                HwPushCon = new HwPushContext(); return HwPushCon.Hw_Users.Where(m => m.QQNumber == 119564557).FirstOrDefault().PublicKey;
                //return MemoryCacheHelper.GetOrSetCache<string>("PublicKeySys", delegate () { HwPushCon = new HwPushContext(); return HwPushCon.Hw_Users.Where(m => m.QQNumber == 119564557).FirstOrDefault().PublicKey; });
            }
        }
        protected string PrivateKey
        {
            get
            {
                 HwPushCon = new HwPushContext(); return HwPushCon.Hw_Users.Where(m => m.QQNumber == 119564557).FirstOrDefault().PrivateKey;
                //return MemoryCacheHelper.GetOrSetCache<string>("PrivateKeySys", delegate () { HwPushCon = new HwPushContext(); return HwPushCon.Hw_Users.Where(m => m.QQNumber == 119564557).FirstOrDefault().PrivateKey; });
            }
        }
        protected string PublicKeyByQQ
        {
            get
            {
                return PublicKeyByQ(QQNumber);
            }
        }
        protected string PublicKeyByQ(long QQNumber)
        {
            return UsersByQ(QQNumber).PublicKey;
        }
        protected Hw_Users UsersByQ(long QQNumber)
        {           
            HwPushCon = new HwPushContext(); return HwPushCon.Hw_Users.Where(m => m.QQNumber == QQNumber).FirstOrDefault();      
            //return MemoryCacheHelper.GetOrSetCache<Hw_Users>("PublicKey" + QQNumber, delegate () { HwPushCon = new HwPushContext(); return HwPushCon.Hw_Users.Where(m => m.QQNumber == QQNumber).FirstOrDefault(); });
        }


        /// <summary>
        /// 实体加密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string EncryptByModel(object model)
        {
            return Encrypt(JsonConvert.SerializeObject(model));
        }
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string Encrypt(string str)
        {
            return RsaHelper.RSACrypto.Encrypt(PublicKeyByQQ, str);
        }







        /// <summary>
        /// 字符串解密为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected T DecryptByModel<T>(string result)
        {
            var Str = Decrypt(result);
            T List = JsonConvert.DeserializeObject<T>(Str);
            return List;
        }
        /// <summary>
        /// 字符串解密为字符串
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected string Decrypt(string result)
        {
            var Str = RsaHelper.RSACrypto.Decrypt(PrivateKey, result);
            return Str;
        }
        /// <summary>
        /// 原报文解密为字符串
        /// </summary>
        /// <returns></returns>
        protected string Decrypt()
        {
            return ResultStr;
        }
        /// <summary>
        /// 原报文解密为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T DecryptByModel<T>()
        {
            T List = JsonConvert.DeserializeObject<T>(ResultStr);
            return List;
        }
    }
}
