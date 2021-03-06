﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace SaledServices.additionForm
{
    public partial class DatabaseForm : Form
    {
        bool isShow = false;

        public DatabaseForm()
        {
            isShow = true;
            InitializeComponent();
        }
        
        public DatabaseForm(bool isShown)
        {
            isShow = isShown;
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(delegate()
            {
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandTimeout = 5*60;//5分钟超时设置，修改默认30s
                    cmd.CommandType = CommandType.Text;

                    string path = "D:\\backup\\";
                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filename = path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";

                    cmd.CommandText = "BACKUP DATABASE  SaledService TO DISK = '" + filename + "'";
                    cmd.ExecuteNonQuery();
                    mConn.Close();

                    if (isShow)
                    {
                        MessageBox.Show("备份成功到服务器的 " + filename);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });

            thread.Start();
            
        }
    }
}
