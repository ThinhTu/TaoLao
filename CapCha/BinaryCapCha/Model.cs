using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace BinaryCapCha
{
    public class HinhAnh
    {
        [Key]
        public int HinhAnhId { get; set; }
        [Required]
        public string Ten { get; set; }
        [Column(TypeName = "image")]
        public byte[] Hinh { get; set; }
        [Required]
        public string HashCode { get; set; }
    }
}
