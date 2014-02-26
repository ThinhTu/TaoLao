using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Hash;

namespace ChangeNamePic
{
    public partial class FromChangePass : Form
    {

        public FromChangePass()
        {
            InitializeComponent();
        }

        public string RootPath { get; set; }

        private void btnchangepath_Click(object sender, EventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog {RootFolder = Environment.SpecialFolder.MyDocuments};
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                RootPath = openFileDialog.SelectedPath;
                RootPath = @"C:\Users\hoa\Documents\visual studio 2010\Projects\CapCha\Hash\bin\Debug\";
                txt_path.Text = RootPath;
                var files = GetListFile();
                Array.Sort(files);
                if(files.Any())
                pictureBox1.ImageLocation =RootPath+ files[0];
                else
                    MessageBox.Show("Khong con files nao ca");
            }

//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                string hash;
//                Image image = Image.FromFile("2b.jpg");
//                using(MemoryStream ms = new MemoryStream())
//                {
//                    using (FileStream fs = new FileStream("2b.jpg",FileMode.Open,FileAccess.Read))
//                    {
//                        fs.CopyTo(ms);
//                    }
//                    hash = GetHash(ms);
//                }
//
//                DbCapCha dbHinh = new DbCapCha();
//                DirectoryInfo directory = new DirectoryInfo(openFileDialog.SelectedPath);
//                
//                
//
//                using (var ms = File.OpenRead("2b.jpg"))
//                {
//                    hash = GetHash(ms);
//                }
//
//                foreach (var fileInfo in directory.GetFiles("*.jpg"))
//                {
//                    using (var ms = File.OpenRead(fileInfo.FullName))
//                    {
//                        hash = GetHash(ms);
//                    }
//                    try
//                    {
//                        var hinh = dbHinh.CapchaPics.First(x => x.HashCode == hash);
//                        hinh.Name = fileInfo.Name.Split('.')[0];
//                        dbHinh.SaveChanges();
//                    }
//                    catch (Exception)
//                    {
//                    }
//                    
//                }
//            }
        }

        private string[] GetListFile()
        {
            var directoryInfo = new DirectoryInfo(RootPath);
            var dir = directoryInfo.GetFiles("*.jpg").Select(x=>x.Name);
            var dirname = dir.Where(x => Regex.IsMatch(x, @"^[a-z0-9]{2}\.jpg"));
            return dirname.ToArray();
        }

        public string GetHash(Stream stream)
        {
            string kq;
            using (MD5 md5 = MD5.Create())
            {
                kq = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
            return kq;
        }

        public string GetHash(byte[] stream)
        {
            string kq;
            using (MD5 md5 = MD5.Create())
            {
                kq = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            }
            return kq;
        }

        private void bt_change_Click(object sender, EventArgs e)
        {
            var files = GetListFile();
            this.Text = (724 - files.Count()) + "/724";
            if (!files.Any())
            {
                MessageBox.Show(@"Khong con file nao chua doi ten");
            }
            else
            {
                    pictureBox1.ImageLocation = RootPath + files[1];
                    File.Move(RootPath + files[0], RootPath + @"\" + txt_text.Text + ".jpg");
                
            }
            txt_text.Focus();
            txt_text.Text = "";
        }
    }
}