using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Hash.Models.Mapping
{
    public class TaiKhoanMap : EntityTypeConfiguration<TaiKhoan>
    {
        public TaiKhoanMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TenDangNhap)
                .HasMaxLength(50);

            this.Property(t => t.MatKhau1)
                .HasMaxLength(50);

            this.Property(t => t.MatKhau2)
                .HasMaxLength(50);

            this.Property(t => t.MatKhauGame)
                .HasMaxLength(50);

            this.Property(t => t.GhiChu)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("TaiKhoan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TenDangNhap).HasColumnName("TenDangNhap");
            this.Property(t => t.MatKhau1).HasColumnName("MatKhau1");
            this.Property(t => t.MatKhau2).HasColumnName("MatKhau2");
            this.Property(t => t.MatKhauGame).HasColumnName("MatKhauGame");
            this.Property(t => t.TonTai).HasColumnName("TonTai");
            this.Property(t => t.GCent).HasColumnName("GCent");
            this.Property(t => t.VCent).HasColumnName("VCent");
            this.Property(t => t.Chao).HasColumnName("Chao");
            this.Property(t => t.Cre).HasColumnName("Cre");
            this.Property(t => t.Blue).HasColumnName("Blue");
            this.Property(t => t.GhiChu).HasColumnName("GhiChu");
            this.Property(t => t.LanCapNhatCuoi).HasColumnName("LanCapNhatCuoi");
        }
    }
}
