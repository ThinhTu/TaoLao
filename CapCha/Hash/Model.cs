using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Hash
{
    public class CapchaPic
    {
        [Key]
        public string HashCode { get; set; }
        public string Name { get; set; }
    }

    public class DbCapCha:DbContext
    {
        public DbSet<CapchaPic> CapchaPics { get; set; }
    }
}
