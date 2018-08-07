using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_UserVersionLibrary
    {
        public int id { get; set; }
        public int UId { get; set; }
        public string DeviceName { get; set; }
        public string D_version { get; set; }
        public string version { get; set; }
        public string FirmWare { get; set; }
        public string Json { get; set; }
        public Nullable<int> IsResource { get; set; }
        public Nullable<long> IMEI { get; set; }
    }
}
