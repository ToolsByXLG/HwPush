using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwPush.ApiModel.Data
{
    public class Mate8RequestBody
    {
        public rules rules { get; set; }
    }
    public class rules
    {
        public string FingerPrint { get; set; }
        public string DeviceName { get; set; }
        public string FirmWare { get; set; }
        public string IMEI { get; set; }
        public string IMSI { get; set; }
        public string Language { get; set; }
        public string OS { get; set; }
        public string HotaVersion { get; set; }
        public string saleinfo { get; set; }
        public string C_version { get; set; }
        public string D_version { get; set; }
        public string devicetoken { get; set; }
        public string PackageType { get; set; }
        public string ControlFlag { get; set; }
        public string extra_info { get; set; }
    }
}
