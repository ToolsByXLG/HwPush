using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_ScanUrlMap : EntityTypeConfiguration<Hw_ScanUrl>
    {
        public Hw_ScanUrlMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Url)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Hw_ScanUrl");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
        }
    }
}
