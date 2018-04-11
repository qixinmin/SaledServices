using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Test_Outlook
{
    public partial class Test2Form : Form
    {
        private String tableName = "test2table";
        public Test2Form()
        {
            InitializeComponent();
        }

        private void tracker_bar_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.tracker_bar_textBox.Text.Trim() == "")
                {
                    this.tracker_bar_textBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select track_serial_no from test1table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string track_serial_no = "";
                    while (querySdr.Read())
                    {
                        track_serial_no = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (track_serial_no != "")
                    {
                        cmd.CommandText = "select custommaterialNo from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();
                        string customMaterialNo = "";

                        while (querySdr.Read())
                        {
                            customMaterialNo = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (customMaterialNo != "")
                        {
                            this.testerTextBox.Text = "tester";
                            this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            this.tracker_bar_textBox.Focus();
                            this.tracker_bar_textBox.SelectAll();
                            MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("此追踪条码没有Test1的记录！");
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void confirmbutton_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试2数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
