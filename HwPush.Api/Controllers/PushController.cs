using HwPush.Api.Models;
using HwPush.ApiModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HwPush.Api.Controllers
{
    public class PushController : SoapBaseController
    {
         [HttpPost][HttpGet]


        public AjaxResCode GetBlacklist()
        { 
            var Blacklist = HwPushCon.Hw_Blacklist.ToList();

            AjaxResCode arc = new AjaxResCode();
            arc.Data = Blacklist;
            arc.Message = "成功";

            return arc;
        }

        public AjaxResCode GetTime()
        {
            AjaxResCode arc = new AjaxResCode();
            arc.Data = DateTime.Now.Ticks;
            arc.Message = "时间获取成功";
            return arc;
        }
    }
}