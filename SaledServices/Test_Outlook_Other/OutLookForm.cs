using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.Test_Outlook_Other;

namespace SaledServices.Test_Outlook
{
    public partial class OutLookForm : Form
    {
        private String tableName = "outlookcheck";
        public OutLookForm()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.tracker_bar_textBox.Focus();
        }

        private void autochecktextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.autochecktextBox.Text.Trim() == "")
                {
                    this.autochecktextBox.Focus();
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

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.autochecktextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string station = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (station != "Test2" && station != "Test1&2")
                    {
                        MessageBox.Show("板子已经经过站别[" + station + "]");
                        mConn.Close();
                        return;
                    }

                    cmd.CommandText = "select * from testalltable where track_serial_no='" + this.autochecktextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    bool isTestExist = false;
                    if (querySdr.HasRows == false)
                    {
                        querySdr.Close();

                        cmd.CommandText = "select * from test2table where track_serial_no='" + this.autochecktextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                            isTestExist = true;
                        }
                        querySdr.Close();

                    }
                    else
                    {
                        isTestExist = true;
                    }
                    isTestExist = true;///TODO delete
                    if (isTestExist)
                    {
                        cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.autochecktextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        string Id = "";
                        while (querySdr.Read())
                        {
                            Id = querySdr[0].ToString();
                        }
                        querySdr.Close();
                       
                        bool bothExist = false;
                        if (Id != "")
                        {
                            cmd.CommandText = "select top 1 Id from stationInformation where track_serial_no='" + this.autochecktextBox.Text.Trim() + "' and station='外观'  order by updateDate desc";
                            querySdr = cmd.ExecuteReader();
                            if (querySdr.HasRows)
                            {
                                bothExist = true;
                            }
                            querySdr.Close();
                        }
                        else
                        {
                            bothExist = false;
                        }

                        if (bothExist)
                        {
                            MessageBox.Show("此序列号已经存在！");
                            this.autochecktextBox.Text = "";
                            mConn.Close();
                            return;
                        }

                        cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                            + this.autochecktextBox.Text.Trim() + "','"
                            + this.testerTextBox.Text.Trim() + "','"
                            + "pass" + "','"
                            + this.testdatetextBox.Text.Trim()
                            + "')";

                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update stationInformation set station = '外观', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                                  + "where track_serial_no = '" + this.autochecktextBox.Text + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.autochecktextBox.Text.Trim() +
                 "','外观','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                        cmd.ExecuteNonQuery();
                      

                        //obe提示是否要过
                        cmd.CommandText = "select Id from decideOBEchecktable where track_serial_no='" + this.autochecktextBox.Text.Trim() + "'  and ischeck='True'";

                        querySdr = cmd.ExecuteReader();
                        string obecheckexist = "";
                        while (querySdr.Read())
                        {
                            obecheckexist = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (obecheckexist != "")
                        {
                            MessageBox.Show("提示：此板子是必须过obe站别");
                        }

                        this.autochecktextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("追踪条码的不在测试列表中，请检查！");
                    }
                 
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        string _8sCodes = "",mac="", dpk_status="",custommaterialno="";

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
                //
                //pictureBox1.Image = Image.FromFile(@"local图片路径");
                //pictureBox1.ImageLocation = @";网络图片地址";
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

                    if (station != "Test2" && station != "Test1&2")
                    {
                        MessageBox.Show("板子已经经过站别[" + station + "]");
                        mConn.Close();
                        this.confirmbutton.Enabled = false;
                        this.button1.Enabled = false;
                        return;
                    }

                    cmd.CommandText = "select product,custom_serial_no,mac,custommaterialNo,dpk_status from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string product = "";
                    while (querySdr.Read())
                    {
                        product = querySdr[0].ToString();

                        _8sCodes = querySdr[1].ToString();

                        mac = querySdr[2].ToString();
                        custommaterialno = querySdr[3].ToString();
                        dpk_status = querySdr[4].ToString();
                    }
                    querySdr.Close();

                    string tableName = "testalltable";
                    if (product == "LBG")
                    {
                        tableName = "test2table";
                    }

                    cmd.CommandText = "select track_serial_no from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string track_serial_no = "";                   
                    while (querySdr.Read())
                    {
                        track_serial_no = querySdr[0].ToString();
                       
                    }
                    querySdr.Close();

                    if (track_serial_no != "")
                    {
                        if (product != "")
                        {
                            this.testerTextBox.Text = LoginForm.currentUser;
                            this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            this.confirmbutton.Enabled = true;
                            this.button1.Enabled = true;
                            this.print.Enabled = true;
                            this.button2.Enabled = true;
                            this.button3.Enabled = true;
                        }
                        else
                        {
                            this.tracker_bar_textBox.Focus();
                            this.tracker_bar_textBox.SelectAll();
                            MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                            this.confirmbutton.Enabled = false;
                            this.button1.Enabled = false;
                            this.print.Enabled = false;
                            this.button2.Enabled = false;
                            this.button3.Enabled = false;
                        }
                    }
                    else 
                    {
                        MessageBox.Show("追踪条码的不在测试列表中，请检查！");
                        this.confirmbutton.Enabled = false;
                        this.button1.Enabled = false;
                        this.print.Enabled = false;
                        this.button2.Enabled = false;
                        this.button3.Enabled = false;
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

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string Id = "";
                    while (querySdr.Read())
                    {
                        Id = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    bool bothExist = false;
                    if (Id != "")
                    {
                        cmd.CommandText = "select top 1 Id from stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' and station='外观' order by updateDate desc";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                            bothExist = true;
                        }
                        querySdr.Close();
                    }
                    else
                    {
                        bothExist = false;
                    }

                    if (bothExist)
                    {
                        MessageBox.Show("此序列号已经存在！");
                        this.tracker_bar_textBox.Text = "";
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + "pass" + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                   
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '外观', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
             "','外观','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();
                    this.tracker_bar_textBox.Text = "";

                    //obe提示是否要过
                    cmd.CommandText = "select Id from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'  and ischeck='True'";

                    querySdr = cmd.ExecuteReader();
                    string obecheckexist = "";
                    while (querySdr.Read())
                    {
                        obecheckexist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (obecheckexist != "")
                    {
                        MessageBox.Show("提示：此板子是必须过obe站别");
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入外观站数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           // this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
                return;
            }

            if (this.failReasonTextBox.Text.Trim() == "")
            {
                MessageBox.Show("失败原因的内容为空，请检查！");
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
                      + this.failReasonTextBox.Text.Trim() + "','"
                      + this.testdatetextBox.Text.Trim()
                      + "')";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
             "','外观','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();
                    this.tracker_bar_textBox.Text = "";
                    this.failReasonTextBox.Text = "";
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入外观 Fail数据, 現在需要把板子給維修人員");
                this.tracker_bar_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            //从收货表里面把信息拿出来
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();
                string temp8s="", tempdpkstatus = "";
                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select product,custom_serial_no,mac,custommaterialNo,dpk_status from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string product = "";
                    while (querySdr.Read())
                    {
                        product = querySdr[0].ToString();

                        temp8s = querySdr[1].ToString();

                        mac = querySdr[2].ToString();
                        custommaterialno = querySdr[3].ToString();
                        tempdpkstatus = querySdr[4].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select Id from print_8s_record where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("此条形码已经打印过，可以继续");
                    }
                    querySdr.Close();

                    cmd.CommandText = "insert into print_8s_record  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
            "','"+temp8s+"','" + this.testerTextBox.Text.Trim() + "','" + this.testdatetextBox.Text.Trim() + "')";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                if (temp8s == "" || tempdpkstatus == "")
                {
                    MessageBox.Show("客户序号或DPK为空!");
                    return;
                }

                PrintUtils.print8sCode(temp8s, tempdpkstatus);
                _8sCodes = "";
                this.print.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mac == "")
            {
                MessageBox.Show("MAC为空!");
                return;
            }
            PrintUtils.printMac(mac);
            mac = "";
            this.print.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (custommaterialno == "")
            {
                MessageBox.Show("联系料号为空!");
                return;
            }
            string filename = "LCFC-FRU_PN-00HM331-WIN.lab";
            if (dpk_status == "NOK")
            {
                filename = "LCFC-FRU_PN-00HM339-NOK.lab";
            }
            PrintUtils.printCustomMaterial(filename, custommaterialno);
            custommaterialno = "";
            this.print.Enabled = false;
        }

        private ImageCheckForm imageCheckForm = null;
        private void button4_Click(object sender, EventArgs e)
        {
            if (imageCheckForm == null || imageCheckForm.IsDisposed)
            {
                imageCheckForm = new ImageCheckForm(this.tracker_bar_textBox.Text.Trim());
            }

            imageCheckForm.BringToFront();
            imageCheckForm.Show();
        }
    }
}
