﻿using System;
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
    public partial class ObeForm : Form
    {
        private String tableName = "Obetable";
        public ObeForm()
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
                    if (Utils.isTimeError(testdatetextBox.Text.Trim()))
                    {
                        this.confirmbutton.Enabled = false;
                    }

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
                    if (station == "外观")
                    {
                        cmd.CommandText = "select custommaterialNo,vendor from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();
                        string customMaterialNo = "",vendorStr="";

                        while (querySdr.Read())
                        {
                            customMaterialNo = querySdr[0].ToString();
                            vendorStr = querySdr[1].ToString();
                        }
                        querySdr.Close();
                        this.Text = "OBE界面：" + vendorStr;

                        if (customMaterialNo != "")
                        {
                            this.testerTextBox.Text = LoginForm.currentUser;
                            this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            this.confirmbutton.Enabled = true;
                            this.button1.Enabled = true;
                        }
                        else
                        {
                            this.tracker_bar_textBox.Focus();
                            this.tracker_bar_textBox.SelectAll();
                            MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                            this.confirmbutton.Enabled = false;
                            this.button1.Enabled = false;
                        }

                        //判断是否必须走obe站别
                        cmd.CommandText = "select Id from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'  and ischeck='True'";

                        querySdr = cmd.ExecuteReader();
                        string obecheckexist = "";
                        while (querySdr.Read())
                        {
                            obecheckexist = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (obecheckexist == "")
                        {
                            MessageBox.Show("此板子不是必须过obe站别");

                            this.confirmbutton.Enabled = false;
                            this.button1.Enabled = false;
                        }
                        else
                        {
                            this.confirmbutton.Enabled = true;
                            this.button1.Enabled = true;
                        }
                    }
                    else 
                    {
                        MessageBox.Show("板子已经经过站别【" + station+"】");
                        this.confirmbutton.Enabled = false;
                        this.button1.Enabled = false;
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
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Obe', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
            "','Obe','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO test_all_result_record VALUES('"
                  + this.tracker_bar_textBox.Text.Trim() + "','"
                  + this.testerTextBox.Text.Trim() + "',GETDATE(),'Pass','','Obe')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select orderno,custom_materialNo from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string orderno = "", custom_materialNo = "";
                    while (querySdr.Read())
                    {
                        orderno = querySdr[0].ToString();
                        custom_materialNo = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    //插入obe站别信息
                    cmd.CommandText = "INSERT INTO ObeStationtable VALUES('"
                       + this.tracker_bar_textBox.Text.Trim() + "','"
                       + orderno + "','"
                       + custom_materialNo + "','"
                       + "P','"// checkresult P- Pass
                       + "','"//failreason empty
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
                MessageBox.Show("插入OBE数据OK");
                this.confirmbutton.Enabled = false;
                this.button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
                return;
            }

            if (this.failresontextBox.Text.Trim() == "")
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

                    //防止重复入库
                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string Id = "";
                    while (querySdr.Read())
                    {
                        Id = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (Id != "")
                    {
                        MessageBox.Show("此序列号已经存在！");
                        this.tracker_bar_textBox.Text = "";
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
           "','Obe','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = "INSERT INTO test_all_result_record VALUES('"
                       + this.tracker_bar_textBox.Text.Trim() + "','"
                       + this.testerTextBox.Text.Trim().ToUpper() + "',GETDATE(),'Fail','" + this.failresontextBox.Text.Trim() + "','Obe')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select orderno,custom_materialNo from decideOBEchecktable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string orderno = "", custom_materialNo = "";
                    while (querySdr.Read())
                    {
                        orderno = querySdr[0].ToString();
                        custom_materialNo = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    //插入obe站别信息
                    cmd.CommandText = "INSERT INTO ObeStationtable VALUES('"
                       + this.tracker_bar_textBox.Text.Trim() + "','"
                       + orderno + "','"
                       + custom_materialNo + "','"
                       + "F','"// checkresult F fail
                       + this.failresontextBox.Text.Trim() + "','"//failreason non empty
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
                MessageBox.Show("插入OBE Fail数据, 現在需要把板子給維修人員");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
