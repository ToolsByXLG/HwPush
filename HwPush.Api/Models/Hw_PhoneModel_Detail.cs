using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_PhoneModel_Detail
    {
        public int Id { get; set; }
        public Nullable<int> Pid { get; set; }
        public string Name { get; set; }
        public string PhoneModel { get; set; }
        public string Operator { get; set; }
        public Nullable<int> IsValid { get; set; }
        public Nullable<int> SortId { get; set; }
        public virtual Hw_PhoneModel Hw_PhoneModel { get; set; }
    }
}
