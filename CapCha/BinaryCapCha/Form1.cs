using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BinaryCapCha
{
    public partial class Form1 : Form
    {
        DbHinhCT _dbHinhCT = new DbHinhCT();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
//            var bmsource = ClearWhiteSpace(pictureBox1.Image);
//            foreach (Bitmap bitmap in bmsource)
//            {
//                if(bitmap==null)break;
//                var bm = new Bitmap(100, 50);
//                using (Graphics g = Graphics.FromImage(bm))
//                {
//                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//                    g.DrawImage(bitmap, 0, 0, 100, 50);
//                }
//                SaveImageDb(bm,"a");
//            }
            var array = Directory.GetFiles(@"D:\Hinhanh", "*.jpg");
            foreach (var arr in array)
            {
                try
                {
                    if(!File.Exists(arr))continue;
                    Image img = Image.FromFile(arr,true);
                    var bmsource = ClearWhiteSpace(img);
                    foreach (Bitmap bitmap in bmsource)
                    {
                        if (bitmap == null) break;
                        var bm = new Bitmap(100, 50);
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(bitmap, 0, 0, 100, 50);
                        }
                        SaveImageDb(bm, "a");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
            }
            MessageBox.Show("Xong!");
        }

        private void WriteText(Image imgImage)
        {
            var bm = new Bitmap(100, 50);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgImage, 0, 0, 100, 50);
            }
            var sw = new StreamWriter(new FileStream(@"D:\text.txt", FileMode.Truncate));
            for (int i = 0; i < bm.Height; i++)
            {
                for (int j = 0; j < bm.Width; j++)
                {
                    if (bm.GetPixel(j, i).ToArgb() == Color.White.ToArgb() || j == 0 || i == 0 || j == bm.Width - 1 ||
                        i == bm.Height - 1)
                    {
                        sw.Write('0');
                    }
                    else
                    {
                        sw.Write('1');
                    }
                }
                sw.WriteLine();
            }
            sw.Dispose();
            sw.Close();
        }

        /// <summary>
        ///     xoa bot mau trong hinh chi de lai 2 mau
        /// </summary>
        /// <param name="image"></param>
        /// <param name="backColor"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        private Bitmap ClearNose(Image image,Color backColor, Color nav )
        {
            var bm = new Bitmap(image);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (bm.GetPixel(i, j).ToArgb() != nav.ToArgb())
                    {
                        bm.SetPixel(i, j, backColor);
                    }
                }
            }
            return bm;
        }


        /// <summary>
        ///     get list mau
        /// </summary>
        /// <param name="image"></param>
        private List<Color> GetListColor(Image image)
        {
            var colors = new List<Color>();
            var bm = new Bitmap(image);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color c = bm.GetPixel(i, j);
                    colors.Add(c);
                }
            }
            var g2 = colors.GroupBy(x => x).Select(y => new {y.Key, Souce = y.Count()}).OrderByDescending(x => x.Souce);
            var kq = new List<Color> {g2.ElementAt(0).Key, g2.ElementAt(1).Key};
            return kq;
        }

        /// <summary>
        ///     ham xóa khoản trắng theo chiều dọc
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap ClearBitmap(Image image, Color backColor)
        {
            var bm = new Bitmap(image);
            var kq = new List<int>();
            var testbool = true;
            for (int i = 0; i < bm.Height - 1; i++)
            {
                for (int j = 0; j < bm.Width; j++)
                {
                    if (bm.GetPixel(j, i).ToArgb() != backColor.ToArgb())
                    {
                        testbool = false;
                    }
                }
                if (testbool == false)
                    kq.Add(i);
            }

            var kq2 = new List<int>();
            testbool = true;
            for (var i = bm.Height - 1; i > 0; i--)
            {
                for (var j = bm.Width - 1; j > 0; j--)
                {
                    if (bm.GetPixel(j, i).ToArgb() != backColor.ToArgb())
                    {
                        testbool = false;
                    }
                }
                if (testbool == false)
                    kq2.Add(i);
            }
            if (kq2.Count == 0 || kq.Count == 0) return null;
            var newbm = bm.Clone(new Rectangle(0, kq[0] - 1, bm.Width, kq2[0] - kq[0] + 2), bm.PixelFormat);
            return newbm;
        }


        /// <summary>
        ///     xoa bot khoan trang trong hinh trang den
        /// </summary>
        /// <param name="imgImage"></param>
        /// <returns> lay ve cac hinh chua cac ky tu</returns>
        private IEnumerable<Bitmap> ClearWhiteSpace(Image imgImage)
        {
            List<Color> c = GetListColor(imgImage);
            var backColor = c[0];
            var navColor = c[1];
            var bm = new Bitmap(imgImage);
            bm = ClearNose(bm,backColor,navColor);
            pictureBox1.Image = bm;
            int hight = bm.Height;
            int width = bm.Width;
            var matrix = new int[bm.Width, bm.Height];
            bool nocontent = true;
            //duyệt theo chiều ngang của hình 
            for (int i = 0; i < width; i++)
            {
                //duyệt theo chiều dọc của hình
                //tìm xem thử đây có phải là đường trắng hay không
                for (int j = 0; j < hight; j++)
                {
                    if ((bm.GetPixel(i, j).ToArgb() != backColor.ToArgb()) && nocontent)
                    {
                        nocontent = false;
                        break;
                    }
                    //xem đây có phải là đường có hình hay không
                    nocontent = true;
                    if ((bm.GetPixel(i, j).ToArgb() != backColor.ToArgb()))
                    {
                        nocontent = false;
                        break;
                    }
                }

                for (int j = 0; j < hight; j++)
                {
                    //tô đểm không có hình màu đỏ
                    if (nocontent)
                    {
                        bm.SetPixel(i, j, Color.Red);
                    }
                    else
                    {
                        if (bm.GetPixel(i, j).ToArgb() == backColor.ToArgb())
                        {
                            matrix[i, j] = 0;
                        }
                        else
                        {
                            matrix[i, j] = 1;
                        }
                    }
                }
            }

            var kq = new List<int>();
            for (int i = 0; i < bm.Width - 1; i++)
            {
                if (bm.GetPixel(i, 0).ToArgb() != bm.GetPixel(i + 1, 0).ToArgb())
                {
                    kq.Add(i);
                }
            }

            var listbm = new List<Bitmap>();
            for (int i = 0; i < kq.Count; i = i + 2)
            {
                Bitmap newbm = bm.Clone(new Rectangle(kq[i] + 1, 0, kq[i + 1] - kq[i], bm.Height), bm.PixelFormat);
                listbm.Add(ClearBitmap(newbm,backColor));
            }

            return listbm;
        }

        public void SaveImageDb(Bitmap bm,string name)
        {
            var converter = new ImageConverter();
            var tmpHinh = (byte[]) converter.ConvertTo(bm, typeof (byte[]));
            string hashcode = GetHash(tmpHinh);
            if (!_dbHinhCT.HinhAnhs.Any(x=>x.HashCode==hashcode))
            {
                _dbHinhCT.HinhAnhs.Add(new HinhAnh()
                {
                    Hinh = tmpHinh,
                    Ten = name,
                    HashCode = hashcode
                });
                _dbHinhCT.SaveChanges();
            }
            
        }

        public string GetHash(byte[] sr)
        {
            string var2 = null;
            using (var md5 = MD5.Create())
            {
                var2 = BitConverter.ToString(md5.ComputeHash(sr)).Replace("-", "").ToLower();
            }
            return var2;
        }

        public Bitmap GetImageDb(string name)
        {
            var converter = new ImageConverter();
            var img = (Image)converter.ConvertFrom(_dbHinhCT.HinhAnhs.First(x => x.Ten == name).Hinh);
            return new Bitmap(img);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var converter = new ImageConverter();
            byte[] varBytes = _dbHinhCT.HinhAnhs.ElementAt(0).Hinh;
            var img = (Image)converter.ConvertFrom(varBytes);
            pictureBox1.Image = new Bitmap(img);
        }
    }
}