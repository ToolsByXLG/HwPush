using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_QunInfoMap : EntityTypeConfiguration<Hw_QunInfo>
    {
        public Hw_QunInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.QunName)
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Hw_QunInfo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QunID).HasColumnName("QunID");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.QunName).HasColumnName("QunName");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
