using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_QunInfo
    {
        public int Id { get; set; }
        public int QunID { get; set; }
        public int IsValid { get; set; }
        public string QunName { get; set; }
        public string Remarks { get; set; }
    }
}
