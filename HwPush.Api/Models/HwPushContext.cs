using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HwPush.Api.Models.Mapping;

namespace HwPush.Api.Models
{
    public partial class HwPushContext : DbContext
    {
        static HwPushContext()
        {
            Database.SetInitializer<HwPushContext>(null);
        }

        public HwPushContext()
            : base("Name=HwPushContext")
        {
        }

        public DbSet<Hw_Blacklist> Hw_Blacklist { get; set; }
        public DbSet<Hw_LatestVersion> Hw_LatestVersion { get; set; }
        public DbSet<Hw_PhoneModel> Hw_PhoneModel { get; set; }
        public DbSet<Hw_PhoneModel_Detail> Hw_PhoneModel_Detail { get; set; }
        public DbSet<Hw_PhoneModel_VersionDetail> Hw_PhoneModel_VersionDetail { get; set; }
        public DbSet<Hw_QunInfo> Hw_QunInfo { get; set; }
        public DbSet<Hw_ScanUrl> Hw_ScanUrl { get; set; }
        public DbSet<Hw_ScanVersion> Hw_ScanVersion { get; set; }
        public DbSet<Hw_Users> Hw_Users { get; set; }
        public DbSet<Hw_UserVersionLibrary> Hw_UserVersionLibrary { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Hw_BlacklistMap());
            modelBuilder.Configurations.Add(new Hw_LatestVersionMap());
            modelBuilder.Configurations.Add(new Hw_PhoneModelMap());
            modelBuilder.Configurations.Add(new Hw_PhoneModel_DetailMap());
            modelBuilder.Configurations.Add(new Hw_PhoneModel_VersionDetailMap());
            modelBuilder.Configurations.Add(new Hw_QunInfoMap());
            modelBuilder.Configurations.Add(new Hw_ScanUrlMap());
            modelBuilder.Configurations.Add(new Hw_ScanVersionMap());
            modelBuilder.Configurations.Add(new Hw_UsersMap());
            modelBuilder.Configurations.Add(new Hw_UserVersionLibraryMap());
        }
    }
}
