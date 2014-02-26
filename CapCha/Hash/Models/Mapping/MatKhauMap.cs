using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Hash.Models.Mapping
{
    public class MatKhauMap : EntityTypeConfiguration<MatKhau>
    {
        public MatKhauMap()
        {
            // Primary Key
            this.HasKey(t => t.Text);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Loai)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MatKhau");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Loai).HasColumnName("Loai");
        }
    }
}
