using System;
using System.Collections.Generic;

namespace Hash.Models
{
    public partial class NhanVat
    {
        public int ID { get; set; }
        public Nullable<int> MaTaiKhoan { get; set; }
        public string TenNhanVat { get; set; }
        public Nullable<int> Reset { get; set; }
        public Nullable<int> Relife { get; set; }
        public Nullable<int> ResetTrongNgay { get; set; }
        public Nullable<int> ResetTrongThang { get; set; }
        public string ChungToc { get; set; }
        public string DiaDiem { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
