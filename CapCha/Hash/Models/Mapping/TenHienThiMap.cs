using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Hash.Models.Mapping
{
    public class TenHienThiMap : EntityTypeConfiguration<TenHienThi>
    {
        public TenHienThiMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TenHienThi");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Text).HasColumnName("Text");
        }
    }
}
