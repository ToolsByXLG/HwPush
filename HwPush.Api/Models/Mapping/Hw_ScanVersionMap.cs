using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_ScanVersionMap : EntityTypeConfiguration<Hw_ScanVersion>
    {
        public Hw_ScanVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Hw_ScanVersion");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Bg).HasColumnName("Bg");
            this.Property(t => t.Sg).HasColumnName("Sg");
            this.Property(t => t.V).HasColumnName("V");
            this.Property(t => t.F).HasColumnName("F");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
