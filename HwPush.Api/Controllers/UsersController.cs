using HwPush.Api.Models;
using HwPush.ApiModel.Data;
using HwPush.ApiModel.QQ;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Http;
using HwPush.ApiBase;
using EntityFramework.Extensions;
using System;

namespace HwPush.Api.Controllers
{
    public class UsersController : SoapBaseController
    {
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetQunNumber()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var qqqunlist = HwPushCon.Hw_QunInfo.ToList();

                res.Data = qqqunlist;
                res.Message = "成功";

            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetQunInfo()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var qqqunlist = HwPushCon.Hw_QunInfo.ToList();

                res.Data = qqqunlist;
                res.Message = "成功";

            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }

        [HttpPost]
        [HttpGet]
        public AjaxResCode GetQQInfoList()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                var qqlist = HwPushCon.Hw_Users.ToList();


                res.Data = qqlist;
                res.Message = "成功";

            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetQQInfo()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {

                if (!string.IsNullOrWhiteSpace(Ver))
                {
                    int verint = int.Parse(Ver.Replace(".", ""));
                    if (verint < 104)
                    {
                        res.Message = Ver + "以下版本停用";
                        res.ResultCode = -1;
                        return res;
                    }
                }
                else {
                    res.Message = "PublicKey.rsa 证书已失效,请重新打开软件";
                    res.ResultCode = -2;
                    return res;

                }


                HwPushCon = new HwPushContext();
                var qqinfo = HwPushCon.Hw_Users.Where(x => x.QQNumber == this.QQNumber).FirstOrDefault();
                if (qqinfo == null)
                {
                    res.Message = this.QQNumber+".rsa 证书已失效,请重新打开软件";
                    res.ResultCode = -2;
                    return res;
                }
                qqinfo.PrivateKey = "";
                qqinfo.PublicKey = "";
                qqinfo.QQInfo = "";

                res.Data = EncryptByModel(qqinfo);
                res.Message = "成功";
                res.ResultCode = 1000;

            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode UpdateIMEI()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var IQqqinfo = HwPushCon.Hw_Users.Where(x => x.QQNumber == this.QQNumber);
                var qqinfo = IQqqinfo.FirstOrDefault();
                if (qqinfo != null)
                {
                    long reqIMEI = long.Parse(ResultStr);
                    if (IQqqinfo.Where(x => x.Id == qqinfo.Id).Update(x => new Hw_Users { IMEI = reqIMEI }) > 0)
                    {
                        HwPushCon.SaveChanges();
                        return GetQQInfo();
                    }
                }


            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode RegQQ()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                var qqnumber = this.QQNumber;
                string qqinfostr = this.ResultStr;

                myinfo my_info = DecryptByModel<myinfo>();
                if (my_info != null && qqnumber > 0)
                {
                    string[] rsa = RsaHelper.GenerateKeys();
                    HwPushCon = new HwPushContext();
                    HwPushCon.Hw_Users.Add(new Hw_Users
                    {
                        QQNumber = qqnumber,
                        UserName = my_info.result.nick,
                        PrivateKey = rsa[0],
                        PublicKey = rsa[1],
                        PrivateKeyMd5 = rsa[2],
                        PublicKeyMd5 = rsa[3],
                        QQInfo = qqinfostr
                    });
                    HwPushCon.SaveChanges();

                    res.Data = rsa[0];
                    res.Message = "注册成功,生成Key";
                    res.ResultCode = 1;
                }
                else
                {
                    res.Data = "";
                    res.Message = "注册失败,无法生成Key";
                    res.ResultCode = 2;

                }
            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
    }
}