using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_BlacklistMap : EntityTypeConfiguration<Hw_Blacklist>
    {
        public Hw_BlacklistMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Hw_Blacklist");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.IMEI).HasColumnName("IMEI");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
        }
    }
}
