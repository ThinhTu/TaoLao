using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace TKB
{
    /// <summary>
    /// lấy danh sách môn học đã và đang học
    /// </summary>
    public class DSMonHoc
    {
        /// <summary>
        /// danh sách môn học
        /// </summary>
        public List<MonHoc> MonHocs { get; set; }

        /// <summary>
        /// nội dung trang web cần lấy
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// lấy môn học từ url 
        /// </summary>
        /// <param name="url">trang web </param>
        public DSMonHoc(Uri url)
        {
            MonHocs=new List<MonHoc>();
            Content = WebRequestcCT.GetContent(url.AbsoluteUri);
            MonHocs = GetListMonHoc();
        }

        /// <summary>
        /// lấy môn học từ trang web 
        /// </summary>
        /// <param name="url">trang web </param>
        /// <param name="hocKy">học kỳ cần lấy</param>
        /// <param name="nam">năm cần lấy</param>
        public DSMonHoc(Uri url,int hocKy, int nam)
        {
            MonHocs = new List<MonHoc>();
            Content = WebRequestcCT.GetContent(url.AbsoluteUri);
            MonHocs = GetListMonHoc(hocKy,nam);
        }

        /// <summary>
        /// lấy danh sách môn học từ content 
        /// </summary>
        /// <param name="content">nội dung cần phân rả thành môn học </param>
        public DSMonHoc(string content)
        {
            MonHocs = new List<MonHoc>();
            Content = content;
        }

        /// <summary>
        /// lấy danh sách môn học
        /// </summary>
        /// <returns>danh sách môn học </returns>
        public List<MonHoc> GetListMonHoc()
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(Content);
            var text = document.DocumentNode.SelectSingleNode("//*[@width='100%']").InnerHtml;
            return GetMonHocs(text);
        }

        /// <summary>
        /// lấy danh sách môn học theo học kỳ theo năm
        /// </summary>
        /// <param name="hocKy">học kỳ</param>
        /// <param name="nam">Năm</param>
        /// <returns>Danh sách môn học</returns>
        public List<MonHoc> GetListMonHoc(int hocKy, int nam)
        {
            var regex = Regex.Split(Content, @"Năm học "+nam+" - Học kỳ "+hocKy);
            var regex2 = Regex.Split(regex[1], @"Năm học \d{4} - Học kỳ ");
            return GetMonHocs(regex2[0]);
        }

        /// <summary>
        /// lấy danh sách môn học từ text
        /// </summary>
        /// <param name="text">nội dung phân rã</param>
        /// <returns>danh sách môn học</returns>
        public List<MonHoc> GetMonHocs(string text)
        {
            List<MonHoc> kq = new List<MonHoc>();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(text.Replace("\t",""));
            foreach (var selectNode in document.DocumentNode.SelectNodes("//tr"))
            {
                var childnode = selectNode.ChildNodes;
                if (childnode.Count == 7)
                {
                    try
                    {
                        MonHoc mh = new MonHoc();
                        mh.MaMonHoc = childnode[0].InnerText;
                        mh.TenMonHoc = childnode[1].InnerText;
                        mh.SoTC = int.Parse(childnode[2].InnerText);
                        mh.DiemTongKet = float.Parse(childnode[5].InnerText);
                        if (kq.Any(x => x.MaMonHoc == mh.MaMonHoc))
                        {
                            var t = kq.First(x => x.MaMonHoc == mh.MaMonHoc);
                            t.DiemTongKet = Math.Max(t.DiemTongKet, mh.DiemTongKet);
                        }
                        else
                            kq.Add(mh);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return kq;
        }
    }
}
