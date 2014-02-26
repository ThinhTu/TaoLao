using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace MuHP
{
    internal class AddInfo
    {
        public CookieContainer CookieContainer { get; set; }
        public string Content { get; set; }

        public bool AddGcentAccount(TaiKhoan taiKhoan)
        {
            if (!Login(taiKhoan.TenDangNhap, taiKhoan.MatKhau1)) return false;
            Content = GetContent("http://taikhoan.muhaiphong.net/CV/Load_Content.php", "Page=Right_Content");
            var list = GetContentXpath(Content, "//*[@class='info_acc']/div/span");
            if (list == null) return false;
            taiKhoan.GCent = int.Parse(list[0]);
            return true;
        }

        public void AddCharactor(TaiKhoan taiKhoan)
        {
            if (!Login(taiKhoan.TenDangNhap, taiKhoan.MatKhau1)) return;
            string content = GetContent("http://taikhoan.muhaiphong.net/CV/Load_Content.php", "Page=Right_Content");
            var list = GetContentXpath(content, "//*[@class='char-select']/li/a");
            if (list == null) return;
            for (int i = 0; i < list.Count; i++)
            {
                GetContent("http://taikhoan.muhaiphong.net/CV/Load_Content.php",
                    "Page=Character_Manager%2FCharacterChoice&CharacterID="+i);
                string contentcharactor = GetContent("http://taikhoan.muhaiphong.net/CV/Load_Content.php",
                    "Page=Character_Manager/List_Act");
                var listch = GetContentXpath(contentcharactor, "//*[@class='col2']/li/span");
                taiKhoan.NhanVats.Add(new NhanVat()
                {
                    TenNhanVat = list[i],
                    Relife = int.Parse(listch[0].Split('/')[0])
                });
            }
        }

        /// <summary>
        ///     hàm get content của trang cần tìm với get và post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string GetContent(string url, string content, string method = "GET")
        {
            string resuft = null;
            if (method == "GET")
            {
                var request = (HttpWebRequest) WebRequest.Create(url + "?" + content);
                request.CookieContainer = CookieContainer;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        resuft = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CookieContainer = CookieContainer;
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(content);
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        resuft = sr.ReadToEnd();
                    }
                }
            }
            return resuft;
        }


        /// <summary>
        ///     hàm login vào trang web
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool Login(string username, string pass)
        {
            CookieContainer = new CookieContainer();
            return GetContent("http://taikhoan.muhaiphong.net/CV/Load_Content.php",
                "Page=Login&username=halyban&password=1234567&login=1") == "Successfuly";
        }

        /// <summary>
        ///     lấy nội dung trang web theo xpath
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="xpath"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public List<string> GetContentXpath(string url, string content, string xpath, string method = "GET")
        {
            string resuft = GetContent(url, content, method);
            if(resuft==null) return null;
            var doc = new HtmlDocument();
            doc.LoadHtml(resuft);
            return doc.DocumentNode.SelectNodes(xpath).Select(htmlNode => htmlNode.InnerText).ToList();
        }


        /// <summary>
        ///     lấy nội dung trang web theo xpath
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="xpath"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public List<string> GetContentXpath(string content, string xpath)
        {
            if (string.IsNullOrEmpty(content)) return null;
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            try
            {
                return doc.DocumentNode.SelectNodes(xpath).Select(htmlNode => htmlNode.InnerText).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}