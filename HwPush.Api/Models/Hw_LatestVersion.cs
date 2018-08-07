using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_LatestVersion
    {
        public int id { get; set; }
        public string DeviceName { get; set; }
        public string version { get; set; }
    }
}
