using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_PhoneModel
    {
        public Hw_PhoneModel()
        {
            this.Hw_PhoneModel_Detail = new List<Hw_PhoneModel_Detail>();
            this.Hw_PhoneModel_VersionDetail = new List<Hw_PhoneModel_VersionDetail>();
        }

        public int Id { get; set; }
        public string VersionName { get; set; }
        public string PhoneModel { get; set; }
        public Nullable<int> IsValid { get; set; }
        public Nullable<int> SortId { get; set; }
        public virtual ICollection<Hw_PhoneModel_Detail> Hw_PhoneModel_Detail { get; set; }
        public virtual ICollection<Hw_PhoneModel_VersionDetail> Hw_PhoneModel_VersionDetail { get; set; }
    }
}
