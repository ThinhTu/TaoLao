using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TKB
{
    class Program
    {
        static void Main(string[] args)
        {
//            int HocKy = 0;
//            string text = WebRequestcCT.GetContent("");
//            var maths = Regex.Matches(text, "Năm học " + (DateTime.Now.Year - 1) + " - Học kỳ ");
//            if (maths.Count == 1)
//                HocKy = 1;
//            else
//            {
//                HocKy = 2;
//            }

//            DSMonHoc monHoc = new DSMonHoc(new Uri("http://dlu.edu.vn/scoreboard_display.aspx?mssv=1010226"));
//            monHoc.GetListMonHoc(1, 2013);
//            monHoc.GetListMonHoc();
//            DSNgayHoc ngayHoc = new DSNgayHoc("1211791", "CTK36", 31);
//            ngayHoc.GetNgayHoc();

            var resouceMonHoc = new GetResouceMonHoc();
            Console.WriteLine("Xong!");
            Console.ReadKey();
        }
    }
}
