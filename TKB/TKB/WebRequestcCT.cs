using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace TKB
{
    /// <summary>
    /// http web request
    /// </summary>
    public class WebRequestcCT
    {
        /// <summary>
        /// lấy 1 stream từ url 
        /// </summary>
        /// <param name="url">url cần lấy</param>
        /// <returns>stream chưa đóng close</returns>
        public static Stream GetStream(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();
            return response.GetResponseStream();
        }

        /// <summary>
        /// lấy mảng byte từ ủrl trong stream 
        /// </summary>
        /// <param name="url">iurl cần lấy</param>
        /// <returns>mảng byte</returns>
        public static byte[] GetBytesStream(string url)
        {
            byte[] buff;
            Stream stream = GetStream(url);
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                buff = ms.ToArray();
            }
            stream.Close();
            return buff;
        }

        /// <summary>
        /// lấy nội dung trang web 
        /// </summary>
        /// <param name="url">url trang web</param>
        /// <returns>nội dung trang web</returns>
        public static string GetContent(string url)
        {
            using (var rd = new StreamReader(GetStream(url)))
            {
                return rd.ReadToEnd();
            }
        }

        /// <summary>
        /// lấy nội dung trang web theo xpath
        /// </summary>
        /// <param name="url">nội dung trang web</param>
        /// <param name="xpath">string xpath</param>
        /// <returns>nội dung cần lấy</returns>
        public static string GetContentSingle(string url, string xpath)
        {
            using (var rd = new StreamReader(GetStream(url)))
            {
                var document = new HtmlDocument();
                document.LoadHtml(rd.ReadToEnd());
                return document.DocumentNode.SelectSingleNode(xpath).InnerText;
            }
        }

        /// <summary>
        /// lấy nội dung trang web theo xpath
        /// </summary>
        /// <param name="url">nội dung trang web</param>
        /// <param name="xpath">string xpath</param>
        /// <returns>list nội dung cần lấy</returns>
        public static List<string> GetContent(string url, string xpath)
        {
            using (var rd = new StreamReader(GetStream(url)))
            {
                var document = new HtmlDocument();
                document.LoadHtml(rd.ReadToEnd());
                return document.DocumentNode.SelectNodes(xpath).Select(x => x.InnerText).ToList();
            }
        }
    }
}