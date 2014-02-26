using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Hash
{
    class WebRS
    {
        public CookieContainer CookieContainer { get; set; }

        public WebRS()
        {
            CookieContainer = new CookieContainer();
        }

        public Stream GetStream(string url,string content="GET")
        {
            if (content == "GET")
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.CookieContainer = CookieContainer;
                var response = (HttpWebResponse) request.GetResponse();
                return response.GetResponseStream();
            }
            else
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                using (var writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII))
                {
                    writer.Write(content);
                }
                var response = (HttpWebResponse)request.GetResponse();
                return response.GetResponseStream();
            }
        }

        public string GetString(string url)
        {
            using (var streamreader = new StreamReader(GetStream(url),encoding:Encoding.UTF8))
            {
                return streamreader.ReadToEnd();
            } 
        }

        public Image GetImage(string url)
        {
            using (var stream = GetStream(url))
            {
                Image image = Image.FromStream(stream);
                return image;
            }
        }

        public Image GetImage(Stream stream)
        {
            return Image.FromStream(stream);
        }

        public string GetHash(string url)
        {
            using (var stream = GetStream(url))
            {
                return GetHash(stream);
            }
        }

        public string GetHash(string url, int count2)
        {
            var stream = GetStream(url);
            Image image = GetImage(stream);
            image.Save(count2+".jpg", ImageFormat.Jpeg);
            return GetHash(stream);
            
        }

        public string GetHash(Stream stream, int count)
        {
            Image image = GetImage(stream);
            image.Save(count+".jpg",ImageFormat.Jpeg);
            return GetHash(stream);
        }

        public string GetHash(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }
    }
}
