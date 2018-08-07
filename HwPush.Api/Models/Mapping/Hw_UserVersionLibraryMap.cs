using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_UserVersionLibraryMap : EntityTypeConfiguration<Hw_UserVersionLibrary>
    {
        public Hw_UserVersionLibraryMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.DeviceName)
                .HasMaxLength(200);

            this.Property(t => t.D_version)
                .HasMaxLength(200);

            this.Property(t => t.version)
                .HasMaxLength(200);

            this.Property(t => t.FirmWare)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Hw_UserVersionLibrary");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.UId).HasColumnName("UId");
            this.Property(t => t.DeviceName).HasColumnName("DeviceName");
            this.Property(t => t.D_version).HasColumnName("D_version");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.FirmWare).HasColumnName("FirmWare");
            this.Property(t => t.Json).HasColumnName("Json");
            this.Property(t => t.IsResource).HasColumnName("IsResource");
            this.Property(t => t.IMEI).HasColumnName("IMEI");
        }
    }
}
