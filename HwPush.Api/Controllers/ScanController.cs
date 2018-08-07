using HwPush.ApiModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntityFramework.Extensions;
using HwPush.Api.Models;
using HwPush.ApiBase;
using Newtonsoft.Json;

namespace HwPush.Api.Controllers
{
    public class ScanController : SoapBaseController
    {
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetVersion()
        {
            var Versionlist = HwPushCon.Hw_ScanVersion.ToList();

            AjaxResCode arc = new AjaxResCode();
            //arc.Data = Versionlist;
            arc.Data = EncryptByModel(Versionlist);
            arc.Message = "成功";

            return arc;
        }
        [HttpPost]
        [HttpGet]
        public AjaxResCode GetScanUrl()
        {
            var ScanUrllist = HwPushCon.Hw_ScanUrl.ToList();

            AjaxResCode arc = new AjaxResCode();
            arc.Data = EncryptByModel(ScanUrllist);
            arc.Message = "成功";

            return arc;
        }

        [HttpPost]
        [HttpGet]
        public AjaxResCode SetVersion()
        {
            var ScanVersionlist = HwPushCon.Hw_ScanVersion.ToList();
            List<Hw_ScanVersion> SvListNoV = ScanVersionlist.Where(x => x.V == null).ToList();

            List<Hw_ScanVersion> ScanVersionList = DecryptByModel<List<Hw_ScanVersion>>();
            List<Hw_ScanVersion> NewVersionList = new List<Hw_ScanVersion>();

            int i = 0;
            ScanVersionList.ForEach(x =>
            {
                if (SvListNoV.Where(m => m.Bg == x.Bg && m.Sg == x.Sg).Count() == 0)
                {
                    NewVersionList.Add(x);
                    i++;
                }
            });

            HwPushCon = new HwPushContext();
            HwPushCon.Hw_ScanVersion.AddRange(NewVersionList);
            HwPushCon.SaveChangesAsync();


            AjaxResCode arc = new AjaxResCode();
            arc.Data = i;
            arc.Message = "添加成功";

            return arc;
        }
    }
}