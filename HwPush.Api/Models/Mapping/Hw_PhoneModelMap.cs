using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_PhoneModelMap : EntityTypeConfiguration<Hw_PhoneModel>
    {
        public Hw_PhoneModelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.VersionName)
                .HasMaxLength(200);

            this.Property(t => t.PhoneModel)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Hw_PhoneModel");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.VersionName).HasColumnName("VersionName");
            this.Property(t => t.PhoneModel).HasColumnName("PhoneModel");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.SortId).HasColumnName("SortId");
        }
    }
}
