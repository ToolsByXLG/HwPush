using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_ScanVersion
    {
        public int Id { get; set; }
        public Nullable<int> Bg { get; set; }
        public Nullable<int> Sg { get; set; }
        public Nullable<int> V { get; set; }
        public Nullable<int> F { get; set; }
        public string Remarks { get; set; }
    }
}
