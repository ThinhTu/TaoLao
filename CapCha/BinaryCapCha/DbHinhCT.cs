using System.Data.Entity;

namespace BinaryCapCha
{
    public class DbHinhCT:DbContext
    {
        public DbSet<HinhAnh> HinhAnhs { get; set; }
    }
}