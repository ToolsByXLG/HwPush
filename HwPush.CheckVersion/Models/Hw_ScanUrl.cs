using System;
using System.Collections.Generic;

namespace HwPush.CheckVersion.Models
{
    public partial class Hw_ScanUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Nullable<int> IsDefault { get; set; }
    }
}
