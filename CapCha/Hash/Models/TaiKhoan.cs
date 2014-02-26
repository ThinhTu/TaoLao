using System;
using System.Collections.Generic;

namespace Hash.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            this.DaThus = new List<DaThu>();
            this.NhanVats = new List<NhanVat>();
        }

        public int ID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau1 { get; set; }
        public string MatKhau2 { get; set; }
        public string MatKhauGame { get; set; }
        public Nullable<bool> TonTai { get; set; }
        public Nullable<decimal> GCent { get; set; }
        public Nullable<decimal> VCent { get; set; }
        public Nullable<decimal> Chao { get; set; }
        public Nullable<decimal> Cre { get; set; }
        public Nullable<decimal> Blue { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> LanCapNhatCuoi { get; set; }
        public virtual ICollection<DaThu> DaThus { get; set; }
        public virtual ICollection<NhanVat> NhanVats { get; set; }
    }
}
