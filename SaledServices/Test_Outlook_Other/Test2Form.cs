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
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.tracker_bar_textBox.Focus();
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

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string station = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (station != "Test1")
                    {
                        MessageBox.Show("板子已经经过站别[" + station+"]");
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        this.confirmbutton.Enabled = false;
                        this.button1.Enabled = false;
                        return;
                    }

                    bool existBuffer = false, existRepair = false; ;
                    string mb_brief="";
                    cmd.CommandText = "select track_serial_no,product from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        querySdr.Close();

                        cmd.CommandText = "select track_serial_no,product from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {  
                            existBuffer = true;
                        }
                        querySdr.Close();
                    }
                    else
                    {
                        querySdr.Close();
                        existRepair = true;
                    }                   

                    if (existRepair)
                    {
                        cmd.CommandText = "select mb_brief from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            mb_brief = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (mb_brief == "")//从替换表里查询
                        {
                            cmd.CommandText = "select mb_brief from DeliveredTableTransfer where track_serial_no_transfer='" + this.tracker_bar_textBox.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                mb_brief = querySdr[0].ToString();
                            }
                            querySdr.Close();
                        }
                    }
                    else if (existBuffer)
                    {
                        cmd.CommandText = "select mb_brief from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            mb_brief = querySdr[0].ToString();
                        }
                        querySdr.Close();
                    }

                    this.testerTextBox.Text = LoginForm.currentUser;

                    //检查文件是否存在
                    if (Utils.existAndCopyToServer(this.tracker_bar_textBox.Text.Trim(), "test2", this.testerTextBox.Text.Trim(), mb_brief) == false)
                    {
                        MessageBox.Show("追踪条码的Log内容为空，请检查！");
                        return;
                    }

                    this.confirmbutton.Enabled = true;
                    this.button1.Enabled = true;    

                    this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
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

                    //cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    //SqlDataReader querySdr = cmd.ExecuteReader();
                    //string Id = "";
                    //while (querySdr.Read())
                    //{
                    //    Id = querySdr[0].ToString();
                    //}
                    //querySdr.Close();
                    //if (Id != "")
                    //{
                    //    MessageBox.Show("此序列号已经存在！");
                    //    this.tracker_bar_textBox.Text = "";
                    //    conn.Close();
                    //    return;
                    //}

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Test2', updateDate =GETDATE()  "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
               "','Test2','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
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
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = GETDATE() "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试2 Fail数据, 現在需要把板子給維修人員");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
