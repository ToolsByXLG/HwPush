using HwPush.Api.Models;
using HwPush.ApiModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntityFramework.Extensions;
namespace HwPush.Api.Controllers
{
    public class VersionController : SoapBaseController
    {

        [HttpPost]
        [HttpGet]
        public AjaxResCode UpdateVersion()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                res.Data = 0;
                Mate8RequestBody Version = DecryptByModel<Mate8RequestBody>();
                rules rule = Version.rules;


                Hw_Users u = UsersByQ(QQNumber);
                Hw_UserVersionLibrary Uvl = new Hw_UserVersionLibrary
                {
                    UId = u.Id,
                    DeviceName = rule.DeviceName,
                    D_version = rule.D_version,
                    Json = ResultStr,
                    IsResource = 0,
                    IMEI = u.IMEI,
                    version = rule.FirmWare.Split('B')[1].Substring(0, 3),
                    FirmWare = rule.FirmWare
                };

                HwPushCon = new HwPushContext();

                var Iusv = HwPushCon.Hw_UserVersionLibrary.Where(x => x.UId == Uvl.UId && x.DeviceName == Uvl.DeviceName && x.D_version == Uvl.D_version);
                if (Iusv.Count() == 0)
                {
                    HwPushCon.Hw_UserVersionLibrary.Add(Uvl);
                    if (HwPushCon.SaveChanges() > 0)
                    {
                        res.Data = Uvl.id;
                    }
                }
                else
                {
                    Hw_UserVersionLibrary OldUvl = Iusv.FirstOrDefault();
                    if (OldUvl.IMEI == null)
                    {
                        res.Data = OldUvl.id;
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
        public AjaxResCode SetIMEI()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                string[] results = ResultStr.Split('|');
                int id = int.Parse(results[0]);
                long? newIMEI = long.Parse(results[1]);
                int IsResource = 0;
                if (results.Length > 2)
                {
                    IsResource = int.Parse(results[2]);
                }
                HwPushCon = new HwPushContext();

                var Iusv = HwPushCon.Hw_UserVersionLibrary.Where(x => x.id == id).Update(x => new Hw_UserVersionLibrary { IMEI = newIMEI, IsResource = IsResource });

                res.Data = Iusv;
            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }


        [HttpPost]
        [HttpGet]
        public AjaxResCode GetLatestVersion()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var maxver = HwPushCon.Hw_LatestVersion.Where(x => x.DeviceName == ResultStr).Max(m => m.version);
                res.Data = maxver;
            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetVersion()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var VersionLibraryQ = HwPushCon.Hw_UserVersionLibrary.Where(x => x.DeviceName == ResultStr && x.IsResource == 1 && x.IMEI != null);
                var maxversion = VersionLibraryQ.Max(m => m.version);
                var Uvl = VersionLibraryQ.Where(x => x.version == maxversion).OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
                res.Data = EncryptByModel(Uvl);
            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetValidVersion()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var ValidVersion = HwPushCon.Hw_UserVersionLibrary.Where(x => x.IsResource == 1 && x.IMEI != null).OrderBy(y => y.FirmWare);
                var s = ValidVersion.GroupBy(x => x.DeviceName, (x, v) => new
                {
                    FirmWare = v.Max(y => y.FirmWare)
                }).Distinct().ToList();

                res.Data = EncryptByModel(s);
            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }


        [HttpPost]
        [HttpGet]
        public AjaxResCode GetModelVersion()
        {
            AjaxResCode res = new AjaxResCode();
            try
            {
                HwPushCon = new HwPushContext();
                var Hw_PhoneModelList = HwPushCon.Hw_PhoneModel.ToList();
            
             
                res.Data = EncryptByModel(Hw_PhoneModelList);
            }
            catch (Exception ee)
            {
                res.Message = ee.Message;
            }
            return res;
        }


    }
}