using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_LatestVersionMap : EntityTypeConfiguration<Hw_LatestVersion>
    {
        public Hw_LatestVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.DeviceName)
                .HasMaxLength(200);

            this.Property(t => t.version)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Hw_LatestVersion");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.DeviceName).HasColumnName("DeviceName");
            this.Property(t => t.version).HasColumnName("version");
        }
    }
}
