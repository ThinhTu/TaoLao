using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace TEst
{
    class Program
    {
        private static string Url =
            "http://taikhoan.muhaiphong.net/CV/includes/captcha/CaptchaSecurityImages.php?characters=2";
        static void Main(string[] args)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                var buff = ms.ToArray();
//                var image = Image.FromStream(stream);
//                image.Save("text.jpg");
//                stream.Position = 0;
                string hash = GetHash(buff);
                File.WriteAllBytes("test.jpg",buff);
                
                using (var ms3 = new MemoryStream())
                {
                    using (var fs = new FileStream("test.jpg", FileMode.Open))
                    {
                        fs.CopyTo(ms3);
                        var buff2 = ms3.ToArray();
                        var hash2 = GetHash(buff2);
                    }
                }
                var h = GetHash(File.ReadAllBytes("test.jpg"));



                using (var sr = new StreamReader(stream))
                {
                    string text = sr.ReadToEnd();
                    Console.WriteLine(text);
                }
            }
            Console.ReadKey();
        }

        public static string GetHash(Stream stream)
        {
            using (var Md5 = MD5.Create())
            {
                return BitConverter.ToString(Md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }

        public static string GetHash(byte[] stream)
        {
            using (var Md5 = MD5.Create())
            {
                return BitConverter.ToString(Md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
        }
    }
}
