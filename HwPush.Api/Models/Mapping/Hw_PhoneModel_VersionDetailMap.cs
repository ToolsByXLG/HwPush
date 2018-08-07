using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_PhoneModel_VersionDetailMap : EntityTypeConfiguration<Hw_PhoneModel_VersionDetail>
    {
        public Hw_PhoneModel_VersionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.IsValid)
                .HasMaxLength(200);

            this.Property(t => t.VersionType)
                .HasMaxLength(200);

            this.Property(t => t.oldversion)
                .HasMaxLength(200);

            this.Property(t => t.version)
                .HasMaxLength(200);

            this.Property(t => t.description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Hw_PhoneModel_VersionDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Pid).HasColumnName("Pid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.VersionType).HasColumnName("VersionType");
            this.Property(t => t.oldversion).HasColumnName("oldversion");
            this.Property(t => t.version).HasColumnName("version");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.createTime).HasColumnName("createTime");
            this.Property(t => t.BG).HasColumnName("BG");
            this.Property(t => t.sg).HasColumnName("sg");
            this.Property(t => t.v).HasColumnName("v");
            this.Property(t => t.f).HasColumnName("f");
            this.Property(t => t.SortId).HasColumnName("SortId");

            // Relationships
            this.HasOptional(t => t.Hw_PhoneModel)
                .WithMany(t => t.Hw_PhoneModel_VersionDetail)
                .HasForeignKey(d => d.Pid);

        }
    }
}
