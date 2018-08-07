using HwPush.Api.Models;
using HwPush.ApiBase;
using HwPush.ApiModel.Data;
using System.Linq;
using System.Web.Http;
using EntityFramework.Extensions;
namespace HwPush.Api.Controllers
{
    public class KeyController : SoapBaseController
    {

        [HttpPost]
        [HttpGet]
        public AjaxResCode GetPublicKey()
        {
            AjaxResCode arc = new AjaxResCode();
            arc.Data = this.PublicKey;
            arc.Message = "成功";
            return arc;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetPrivateKey()
        {
            var qqnumber = this.QQNumber;
            var Reset = ResultStr;
            AjaxResCode arc = new AjaxResCode();
            //if (qqnumber == 119564557)
            {
                arc.ResultCode = 2;
                arc.Data = "";
                arc.Message = "权限不足";
                //  return arc;
            }

            Hw_Users Users = HwPushCon.Hw_Users.FirstOrDefault(m => m.QQNumber == qqnumber);
            if (Users == null)
            {
                arc.ResultCode = 0;
                arc.Data = "";
                arc.Message = "用户不存在";
            }
            else
            {
                if (qqnumber == 119564557)
                {
                    arc.ResultCode = 1;
                    arc.Data = Users.PrivateKey;
                    arc.Message = "成功";
                }
                else
                {
                    if (Reset == "1")
                    {
                        string[] rsa = RsaHelper.GenerateKeys();
                        HwPushCon.Hw_Users.Where(m => m.QQNumber == qqnumber).Update(u => new Hw_Users
                        {
                            PrivateKey = rsa[0],
                            PublicKey = rsa[1],
                            PrivateKeyMd5 = rsa[2],
                            PublicKeyMd5 = rsa[3]
                        });

                        arc.Data = rsa[0];
                    }
                    else
                    {
                        arc.Data = Users.PrivateKey;
                    }
                    arc.ResultCode = 1;
                    arc.Message = "成功";
                }
            }
            return arc;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetVerifyPrivateKeyMd5()
        {
            Hw_Users Users = HwPushCon.Hw_Users.Where(m => m.QQNumber == 12345).FirstOrDefault();
            AjaxResCode arc = new AjaxResCode();
            arc.Data = Users.PrivateKeyMd5;
            arc.Message = "成功";
            return arc;
        }

    }
}
