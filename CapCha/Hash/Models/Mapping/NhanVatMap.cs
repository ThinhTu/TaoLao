using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Hash.Models.Mapping
{
    public class NhanVatMap : EntityTypeConfiguration<NhanVat>
    {
        public NhanVatMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TenNhanVat)
                .HasMaxLength(50);

            this.Property(t => t.ChungToc)
                .HasMaxLength(50);

            this.Property(t => t.DiaDiem)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NhanVat");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MaTaiKhoan).HasColumnName("MaTaiKhoan");
            this.Property(t => t.TenNhanVat).HasColumnName("TenNhanVat");
            this.Property(t => t.Reset).HasColumnName("Reset");
            this.Property(t => t.Relife).HasColumnName("Relife");
            this.Property(t => t.ResetTrongNgay).HasColumnName("ResetTrongNgay");
            this.Property(t => t.ResetTrongThang).HasColumnName("ResetTrongThang");
            this.Property(t => t.ChungToc).HasColumnName("ChungToc");
            this.Property(t => t.DiaDiem).HasColumnName("DiaDiem");

            // Relationships
            this.HasOptional(t => t.TaiKhoan)
                .WithMany(t => t.NhanVats)
                .HasForeignKey(d => d.MaTaiKhoan);

        }
    }
}
