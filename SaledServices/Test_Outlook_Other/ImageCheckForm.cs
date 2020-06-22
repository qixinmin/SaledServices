using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace SaledServices.Test_Outlook_Other
{
    public partial class ImageCheckForm : Form
    {
        private string track_no;
        public ImageCheckForm(string trackno)
        {
            InitializeComponent();
            track_no = trackno;
            currentInfotextBox.Text = track_no;

            initForm(track_no);

        }

        private void currentInfotextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                initForm(currentInfotextBox.Text.Trim());
            }
        }

        private void initForm(string trackno)
        {
            pictureBox1.Hide();
            filelistBox.Items.Clear();
            string vendor = "", product = "", mb_brief="";
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor, product,mb_brief from DeliveredTable where track_serial_no='" + trackno + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                        product = querySdr[1].ToString();
                        mb_brief = querySdr[2].ToString();
                    }

                    querySdr.Close();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                //从服务器读取内容
                string path = @"\\192.168.5.222\\imagecheck\\" + vendor + "_" + product + "\\";
                DirectoryInfo Dir = new DirectoryInfo(path);
                if (Dir.Exists == false)
                {
                    MessageBox.Show("请检查图片路径是否存在:" + path);
                }
                else
                {
                    
                    foreach (FileInfo info in Dir.GetFiles())
                    {
                        if (info.Name.StartsWith(mb_brief))
                        {
                            filelistBox.Items.Add(info.FullName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void filelistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           string file =  filelistBox.SelectedItem.ToString();

           pictureBox1.Image = Image.FromFile(file);
           pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
           pictureBox1.Show();

           //本地程序打开图片
           OpenImage(file);

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left) //放大图bai片du
            {
                pictureBox1.Size = new Size(pictureBox1.Width + 50, pictureBox1.Height + 50);
            }
            else
            {  //缩小图片
                pictureBox1.Size = new Size(pictureBox1.Width - 50, pictureBox1.Height - 50);
            }
            //设置图片在窗体居中
            pictureBox1.Location = new Point((this.Width - pictureBox1.Width) / 2, (this.Height - pictureBox1.Height) / 2);

        }

        private void OpenImage(string fileName)
        {
            try
            {
                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                // LogHelper.WriteLog("调用默认看图软件打开失败", ex);
                try
                {
                    string arg =
                        string.Format(
                            "\"{0}\\Windows Photo Viewer\\PhotoViewer.dll\", ImageView_Fullscreen  {1} ",
                            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                            , fileName);
                    var dllExe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System),
                        "rundll32.exe");
                    // LogHelper.WriteLog(string.Format("调用系统默认的图片查看器打开图片，参数为：{0} {1}", dllExe, arg));
                    System.Diagnostics.Process.Start(dllExe, arg);
                }
                catch (Exception e)
                {
                    // LogHelper.WriteLog("系统图片查看器打开图片失败", ex);

                    //打开文件夹并选中文件
                    string argment = string.Format(@"/select,""{0}""", fileName);
                    System.Diagnostics.Process.Start("Explorer", argment);
                }
            }
        }

    }
}
