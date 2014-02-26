using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace WebRQ
{
    public class MuWebResquest
    {
        public static CookieContainer CookieContainer { get; set; }

        static Encoding _encode = Encoding.GetEncoding("utf-8");

        public static CookieContainer Login(string urlx, string username, string pass, string equal)
        {
            string url = string.Format(urlx,username, pass);
            CookieContainer = new CookieContainer();
            var content = GetContent(url);
            if (content == equal)
                return CookieContainer;
            return null;
        }


        public static Stream GetStream(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = CookieContainer;
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        public static string GetContent(string url)
        {
            string resuft;
            var stream = GetStream(url);
            using (var rd = new StreamReader(stream, _encode))
            {
                resuft = rd.ReadToEnd();
            }
            stream.Close();
            return resuft;
        }

        public static string GetHash(Stream stream)
        {
            string kq;
            using (MD5 md5 = MD5.Create())
            {
                kq = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
            return kq;
        }

        public static string GetHash(byte[] stream)
        {
            string kq;
            using (MD5 md5 = MD5.Create())
            {
                kq = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
            return kq;
        }
    }
}
