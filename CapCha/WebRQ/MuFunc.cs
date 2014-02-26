using System.Net;

namespace WebRQ
{
    public class MuFunc
    {
        public static CookieContainer CookieContainer { get; set; }

        public static bool Login(string url, string username, string password, string equal)
        {
            if (MuWebResquest.Login(url,username, password,equal)!=null)
            {
                CookieContainer = MuWebResquest.CookieContainer;
                return true;
            }
            return false;
        }
         
    }
}