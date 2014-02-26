using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace TKB
{
    /// <summary>
    /// lấy danh sách môn học của một khoa
    /// </summary>
    public class GetResouceMonHoc
    {
        /// <summary>
        /// tổng số tín chỉ của khoa
        /// </summary>
        public int TongTc { get; set; }

        /// <summary>
        /// tổng số tín chỉ bắt buộc của khoa
        /// </summary>
        public int TongTcBacBuoc { get; set; }

        /// <summary>
        /// phương thức khởi tạo
        /// </summary>
        public GetResouceMonHoc()
        {
            string url = "http://www.dlu.edu.vn/detail_major.aspx?majorid=60&orgId=70";
            string content = WebRequestcCT.GetContent(url);
            GetMonHocs((WebUtility.HtmlDecode(content)));
        }

        /// <summary>
        /// lấy danh sách môn học từ web cửa trường 
        /// </summary>
        /// <param name="content">nội dung trang web của trường cần lấy</param>
        /// <returns>danh sách môn học của khoa đó</returns>
        public List<MonHoc> GetMonHocs(string content)
        {
            var listm = new List<MonHoc>();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var document = doc.DocumentNode.SelectNodes("//table[@class='MsoNormalTable']");
            var text = document[1].InnerHtml;
            doc.LoadHtml(document[0].InnerHtml);
            var nodevalue = doc.DocumentNode.SelectNodes("//tr[11]/td/p/b/span");
            this.TongTc = int.Parse(nodevalue[0].InnerText);
            this.TongTcBacBuoc = int.Parse(nodevalue[1].InnerText);
            Console.WriteLine(TongTc +": "+ TongTcBacBuoc);
            doc.LoadHtml(text);
            foreach (var selectNode in doc.DocumentNode.SelectNodes("//tr"))
            {
                var newdoc = new HtmlDocument();
                newdoc.LoadHtml(selectNode.InnerHtml);
                var newnode = newdoc.DocumentNode.SelectNodes("//td/p/span");
                if (newnode != null&&!string.IsNullOrEmpty(newnode[0].InnerText.Trim()))
                {

                    MonHoc mh = new MonHoc()
                    {
                        MaMonHoc = newnode[0].InnerText,
                        TenMonHoc = newnode[1].InnerText,
                        SoTC = int.Parse(newnode[2].InnerText),
                    };
                    try
                    {
                        int.Parse(newnode[5].InnerText);
                        mh.BacBuoc = true;
                    }
                    catch (Exception)
                    {
                        mh.BacBuoc = false;
                    }
                    listm.Add(mh);
                }
            }
            return listm;
        }
    }
}
