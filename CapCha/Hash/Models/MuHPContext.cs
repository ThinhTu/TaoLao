using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Hash.Models.Mapping;

namespace Hash.Models
{
    public partial class MuHPContext : DbContext
    {
        static MuHPContext()
        {
            Database.SetInitializer<MuHPContext>(null);
        }

        public MuHPContext()
            : base("Name=MuHPContext")
        {
        }

        public DbSet<DaThu> DaThus { get; set; }
        public DbSet<Decaptcha> Decaptchas { get; set; }
        public DbSet<MatKhau> MatKhaus { get; set; }
        public DbSet<NhanVat> NhanVats { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<TenHienThi> TenHienThis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DaThuMap());
            modelBuilder.Configurations.Add(new DecaptchaMap());
            modelBuilder.Configurations.Add(new MatKhauMap());
            modelBuilder.Configurations.Add(new NhanVatMap());
            modelBuilder.Configurations.Add(new TaiKhoanMap());
            modelBuilder.Configurations.Add(new TenHienThiMap());
        }
    }
}
