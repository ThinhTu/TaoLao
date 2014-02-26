using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Hash.Models.Mapping
{
    public class DaThuMap : EntityTypeConfiguration<DaThu>
    {
        public DaThuMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MatKhau)
                .HasMaxLength(50);

            this.Property(t => t.LoaiPass)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DaThu");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TaiKhoan).HasColumnName("TaiKhoan");
            this.Property(t => t.MatKhau).HasColumnName("MatKhau");
            this.Property(t => t.ThoiGian).HasColumnName("ThoiGian");
            this.Property(t => t.LoaiPass).HasColumnName("LoaiPass");

            // Relationships
            this.HasOptional(t => t.TaiKhoan1)
                .WithMany(t => t.DaThus)
                .HasForeignKey(d => d.TaiKhoan);

        }
    }
}
