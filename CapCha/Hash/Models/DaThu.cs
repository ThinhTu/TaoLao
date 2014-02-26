using System;
using System.Collections.Generic;

namespace Hash.Models
{
    public partial class DaThu
    {
        public int ID { get; set; }
        public Nullable<int> TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public Nullable<System.DateTime> ThoiGian { get; set; }
        public string LoaiPass { get; set; }
        public virtual TaiKhoan TaiKhoan1 { get; set; }
    }
}
