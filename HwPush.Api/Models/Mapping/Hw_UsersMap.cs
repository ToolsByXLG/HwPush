using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HwPush.Api.Models.Mapping
{
    public class Hw_UsersMap : EntityTypeConfiguration<Hw_Users>
    {
        public Hw_UsersMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(200);

            this.Property(t => t.PublicKey)
                .HasMaxLength(2000);

            this.Property(t => t.PublicKeyMd5)
                .HasMaxLength(32);

            this.Property(t => t.PrivateKey)
                .HasMaxLength(2000);

            this.Property(t => t.PrivateKeyMd5)
                .HasMaxLength(32);

            this.Property(t => t.QunIds)
                .HasMaxLength(200);

            this.Property(t => t.Roles)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Hw_Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QQNumber).HasColumnName("QQNumber");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PublicKey).HasColumnName("PublicKey");
            this.Property(t => t.PublicKeyMd5).HasColumnName("PublicKeyMd5");
            this.Property(t => t.PrivateKey).HasColumnName("PrivateKey");
            this.Property(t => t.PrivateKeyMd5).HasColumnName("PrivateKeyMd5");
            this.Property(t => t.IMEI).HasColumnName("IMEI");
            this.Property(t => t.QunIds).HasColumnName("QunIds");
            this.Property(t => t.Roles).HasColumnName("Roles");
            this.Property(t => t.QQInfo).HasColumnName("QQInfo");
            this.Property(t => t.Gold).HasColumnName("Gold");
        }
    }
}
