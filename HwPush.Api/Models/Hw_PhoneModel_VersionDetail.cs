using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_PhoneModel_VersionDetail
    {
        public int Id { get; set; }
        public Nullable<int> Pid { get; set; }
        public string Name { get; set; }
        public string IsValid { get; set; }
        public string VersionType { get; set; }
        public string oldversion { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public Nullable<int> BG { get; set; }
        public Nullable<int> sg { get; set; }
        public Nullable<int> v { get; set; }
        public Nullable<int> f { get; set; }
        public Nullable<int> SortId { get; set; }
        public virtual Hw_PhoneModel Hw_PhoneModel { get; set; }
    }
}
