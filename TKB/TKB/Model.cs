using System.Collections.Generic;

namespace TKB
{
    /// <summary>
    /// class môn học 
    /// </summary>
    public class MonHoc
    {
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTC { get; set; }
        public float DiemTongKet { get; set; }
        public bool BacBuoc { get; set; }

        public override string ToString()
        {
            return MaMonHoc + " :" + TenMonHoc.PadRight(40) + " :" + SoTC + " :" + DiemTongKet;
        }
    }


    public class BuoiHoc
    {
        public MonHoc MonHoc { get; set; }
        public int TietHocBatDau { get; set; }
        public int TietHocKetThuc { get; set; }
        public string PhongHoc { get; set; }
        public bool TrungLich { get; set; }
    }

    public class NgayHoc
    {
        public int Thu { get; set; }
        public List<BuoiHoc> BuoiHocs { get; set; }

        public NgayHoc()
        {
            BuoiHocs = new List<BuoiHoc>();
        }
    }
}
