using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace TKB
{
    /// <summary>
    /// một ngày học nhiều môn, mỗi tuần thì học nhiều ngày
    /// </summary>
    public class DSNgayHoc
    {
        /// <summary>
        /// danh sách các ngày học trong tuần
        /// </summary>
        public List<NgayHoc> ListNgayHoc { get; set; }

        /// <summary>
        /// danh sách môn học
        /// </summary>
        public DSMonHoc DSMonHoc { get; set; }

        /// <summary>
        /// lớp học
        /// </summary>
        public string Lop { get; set; }

        /// <summary>
        /// Tuần
        /// </summary>
        public int Tuan { get; set; }

        /// <summary>
        /// danh sách các ngày học
        /// </summary>
        /// <param name="mssv">Mã số sinh viên</param>
        /// <param name="lop">Lớp</param>
        /// <param name="tuan">Tuần</param>
        public DSNgayHoc(string mssv, string lop, int tuan)
        {
            DSMonHoc = new DSMonHoc(new Uri("http://dlu.edu.vn/scoreboard_display.aspx?mssv=" + mssv), 2, 2013);
            Lop = lop;
            Tuan = tuan;
            ListNgayHoc = new List<NgayHoc>();
        }
        
        /// <summary>
        /// lấy tất cả ngày học
        /// </summary>
        public void GetNgayHoc()
        {
            foreach (var monHoc in DSMonHoc.MonHocs)
            {
                GetNgayHocTheoMon(monHoc);
            }
        }

        /// <summary>
        /// lấy ngày học theo môn
        /// </summary>
        /// <param name="monHoc">môn học</param>
        public void GetNgayHocTheoMon(MonHoc monHoc)
        {
            string url = string.Format("http://dlu.edu.vn/timetable_display.aspx?course={0}&subjectcode={1}&week={2}",
                Lop, monHoc.MaMonHoc, Tuan);
            string content = WebRequestcCT.GetContent(url);
            var document = new HtmlDocument();
            document.LoadHtml(content.Replace("\t", ""));
            int i = 1;
            foreach (HtmlNode doc in document.DocumentNode.SelectNodes("//*[@width='14%']"))
            {
                i++;
                var newdoc = new HtmlDocument();
                newdoc.LoadHtml(doc.InnerHtml);
                var listbuoihoc = new List<BuoiHoc>();
                HtmlNodeCollection tdtext = newdoc.DocumentNode.SelectNodes("//td");
                if (tdtext == null) continue;
                foreach (HtmlNode textdoc in tdtext)
                {
                    if (!string.IsNullOrEmpty(textdoc.InnerHtml))
                    {
                        string text = textdoc.InnerHtml.Replace("<br><br>", "<br>");
                        string[] arr = Regex.Split(text, "<br>").Where(x => x != "").ToArray();
                        var buoiHoc = new BuoiHoc();
                        buoiHoc.MonHoc = monHoc;
                        MatchCollection arr2 = Regex.Matches(arr[2], @"\d{1,}");
                        buoiHoc.TietHocBatDau = int.Parse(arr2[0].Value);
                        buoiHoc.TietHocKetThuc = int.Parse(arr2[1].Value);
                       
                        // chua test duoc co trung lich hay khong

                        NgayHoc ngay = ListNgayHoc.FirstOrDefault(x => x.Thu == i);
                        if (ngay == null)
                            buoiHoc.TrungLich = false;
                        else
                            buoiHoc.TrungLich = TrungBuoiHoc(ngay, buoiHoc);
                        buoiHoc.PhongHoc = arr[3];
                        listbuoihoc.Add(buoiHoc);
                    }
                }
                if (ListNgayHoc.All(x => x.Thu != i))
                {
                    var ngayHoc = new NgayHoc
                    {
                        Thu = i,
                        BuoiHocs = listbuoihoc
                    };
                    ListNgayHoc.Add(ngayHoc);
                }
                else
                {
                    ListNgayHoc.First(x=>x.Thu==i).BuoiHocs.AddRange(listbuoihoc);
                }
            }
        }

        /// <summary>
        /// kiểm tra buổi học đó có bị trùng hay không
        /// </summary>
        /// <param name="ngayHoc">Ngày học</param>
        /// <param name="buoihoc">Buổi học</param>
        /// <returns></returns>
        public bool TrungBuoiHoc(NgayHoc ngayHoc, BuoiHoc buoihoc)
        {
            foreach (var bHoc in ngayHoc.BuoiHocs)
            {
                if (bHoc.TietHocBatDau < buoihoc.TietHocBatDau)
                {
                    if (bHoc.TietHocKetThuc > bHoc.TietHocBatDau)
                        return true;
                }
                else
                    if(bHoc.TietHocBatDau<bHoc.TietHocKetThuc)
                        return true;
            }
            return false;
        }
    }
}