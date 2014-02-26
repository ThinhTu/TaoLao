using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Hash.Models.Mapping
{
    public class DecaptchaMap : EntityTypeConfiguration<Decaptcha>
    {
        public DecaptchaMap()
        {
            // Primary Key
            this.HasKey(t => t.Hash);

            // Properties
            this.Property(t => t.Hash)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Captcha)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Decaptcha");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Captcha).HasColumnName("Captcha");
        }
    }
}
