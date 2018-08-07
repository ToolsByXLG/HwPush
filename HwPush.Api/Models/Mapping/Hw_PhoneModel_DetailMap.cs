using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_PhoneModel_DetailMap : EntityTypeConfiguration<Hw_PhoneModel_Detail>
    {
        public Hw_PhoneModel_DetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.PhoneModel)
                .HasMaxLength(200);

            this.Property(t => t.Operator)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Hw_PhoneModel_Detail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Pid).HasColumnName("Pid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PhoneModel).HasColumnName("PhoneModel");
            this.Property(t => t.Operator).HasColumnName("Operator");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.SortId).HasColumnName("SortId");

            // Relationships
            this.HasOptional(t => t.Hw_PhoneModel)
                .WithMany(t => t.Hw_PhoneModel_Detail)
                .HasForeignKey(d => d.Pid);

        }
    }
}
