using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Hash.Models;
using HtmlAgilityPack;
using Microsoft.SqlServer.Server;
using WebRQ;

namespace Hash
{
    internal class Program
    {
        public static CookieContainer CookieContainer { get; set; }

        private static void Main(string[] args)
        {
//            string var1;
//            string var2;
//            using (var md5 = MD5.Create())
//            {
//                using (var stream = File.OpenRead("file1.jpg"))
//                {
//                    var1 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
//                }
//                using (var stream = File.OpenRead("file2.jpg"))
//                {
//                    var2 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
//                }
//            }
//            Console.WriteLine(var1);
//            Console.WriteLine(var2);


            //            var wc = new WebResquest();
            //            var wccontent = wc.Getcontent("http://taikhoan.muhaiphong.net/CV/includes/captcha/CaptchaSecurityImages.php",
            //                "?characters=2");


            var dbCapCha = new DbCapCha();
//            var dic = dbCapCha.CapchaPics.ToDictionary(x => x.HashCode, x => x.Name);
//            var captStream =GetStream(
//                                    "http://taikhoan.muhaiphong.net/CV/includes/captcha/CaptchaSecurityImages.php?characters=2");
//            var md5Hash = GetHash(captStream);
//            var capt = dic[md5Hash];
//            var exchangeRerult = Get(
//                               string.Format(
//                                   "",capt));
//            Console.WriteLine(exchangeRerult);

//            for (;;)
//            {
//                string url = "http://taikhoan.muhaiphong.net/CV/includes/captcha/CaptchaSecurityImages.php?characters=2";
//                if (dbCapCha.CapchaPics.Count() >= 28*28)
//                {
//                    break;
//                }
//                var stream = GetStream(url);
//                var hash = GetHash(stream);
//                var pic = new CapchaPic
//                {
//                    HashCode = hash
//                };
//                if (!dbCapCha.CapchaPics.Any(x => x.HashCode == pic.HashCode))
//                {
//                    File.WriteAllBytes(hash+".jpg",stream);
//                    Console.WriteLine(pic.HashCode);
//                    dbCapCha.CapchaPics.Add(pic);
//                    dbCapCha.SaveChanges();
//                }
//            }

//            var dir = new DirectoryInfo(Environment.CurrentDirectory);
//            foreach (FileInfo fileInfo in dir.GetFiles("*.jpg"))
//            {
//                string hash = GetHash(File.ReadAllBytes(Environment.CurrentDirectory + "\\" + fileInfo.Name));
//                dbCapCha.CapchaPics.First(x => x.HashCode == hash).Name = fileInfo.Name.Split('.')[0];
//                dbCapCha.SaveChanges();
//            }

//            var dictionary = dbCapCha.CapchaPics.ToDictionary(x=>x.HashCode,x=>x.Name);
//
//            if (Login("halyban", "1234567"))
//            {
//                Console.WriteLine("Login thanh cong!");
//                const string urlcapcha = "http://taikhoan.muhaiphong.net/CV/includes/captcha/CaptchaSecurityImages.php?characters=2";
//                string hash = GetHash(GetStream(urlcapcha));
//                string url =string.Format(
//                    "http://taikhoan.muhaiphong.net/CV/Load_Content.php?Page=Bank_Manager/MoneyExChange&security_code={0}&passcap2={1}&money1=0&TypeDoi=1&MoneyExChange=1",dictionary[hash],"passcap2");
//                var htmlDocument = new HtmlDocument();
//                htmlDocument.LoadHtml(GetContent(url));
//                var selectSingleNode = htmlDocument.DocumentNode.SelectSingleNode("//*[id='dialog-message1']");
//                if (selectSingleNode != null)
//                {
//                    Console.WriteLine(selectSingleNode.InnerText);
//                }
//            }


//            else
//            {
//                Console.WriteLine("Login bi loi!");
//            }
            
            MuHPContext muHPContext = new MuHPContext();
            MuHaiPhong mu = new MuHaiPhong(muHPContext.MatKhaus.Select(x => x.Text).ToList(),
                muHPContext.Decaptchas.ToDictionary(x => x.Hash, x => x.Captcha));
            Console.WriteLine(mu.DoPass2("201191", "88888888"));
//            foreach (var taiKhoan in muHPContext.TaiKhoans)
//            {
//                var doPass1 = mu.DoPass1(taiKhoan.TenDangNhap);
//                if(doPass1 != null)
//                    Console.WriteLine(doPass1);
//            }


            Console.WriteLine("Xong!");
            Console.ReadKey();
        }

        public static bool Login(string username, string pass)
        {
            string url = string.Format(
                "http://taikhoan.muhaiphong.net/CV/Load_Content.php?Page=Login&username={0}&password={1}&login=1",
                username, pass);
            CookieContainer = new CookieContainer();
            var content = GetContent(url);
            if(content=="Successfuly")
                return true;
            return false;
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
            using (var rd = new StreamReader(stream))
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