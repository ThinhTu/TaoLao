using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuHP
{
    class Program
    {
        static void Main(string[] args)
        {
            var addInfo = new AddInfo();
            addInfo.AddCharactor(new TaiKhoan() { TenDangNhap = "halyban", MatKhau1 = "1234567" });
            Console.ReadKey();
        }
    }
}
