using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_Blacklist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> QQ { get; set; }
        public Nullable<long> IMEI { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> IsValid { get; set; }
    }
}
