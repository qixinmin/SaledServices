﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SaledServices.Test_Outlook
{
    public partial class Test2Form : Form
    {
        private String tableName = "test2table";

        Dictionary<string, string> myDictionary = new Dictionary<string, string>();

        public Test2Form()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.tracker_bar_textBox.Focus();
            loadAdditionInfomation();
        }

        private void loadAdditionInfomation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select fault_index, fault_describe from customFault";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string index = querySdr[0].ToString();
                    string temp = querySdr[1].ToString();
                    if (temp != "")
                    {
                        if (myDictionary.Keys.Contains(index) == false)
                        {
                            myDictionary.Add(index, temp);
                        }
                    }
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                    string _8s = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (station != "Test1")
                    {
                        MessageBox.Show("测试1未测试，板子已经经过站别[" + station + "]，测试未测试");
                        mConn.Close();
                        this.tracker_bar_textBox.Focus();
                        this.tracker_bar_textBox.SelectAll();
                        this.confirmbutton.Enabled = false;
                        //this.button1.Enabled = false;
                        return;
                    }

                    bool existBuffer = false, existRepair = false;
                    string mb_brief="";
                    cmd.CommandText = "select track_serial_no,product,custom_serial_no from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        querySdr.Close();

                        cmd.CommandText = "select track_serial_no,product,custom_serial_no from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                           // _8s = querySdr[2].ToString();
                            existBuffer = true;
                        }
                        querySdr.Close();
                    }
                    else
                    {                      
                       // _8s = querySdr[2].ToString(); 在下面修改8s
                        querySdr.Close();
                        existRepair = true;
                    }

                    this.repairedLabel.Text = "无维修记录";
                    if (existRepair)
                    {
                        cmd.CommandText = "select mb_brief,custom_serial_no,vendor from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        String vendorStr = "";
                        while (querySdr.Read())
                        {
                            mb_brief = querySdr[0].ToString();
                            _8s = querySdr[1].ToString();
                            vendorStr = querySdr[2].ToString();
                        }
                        querySdr.Close();
                        this.Text = "测试2界面：" + vendorStr;

                        if (mb_brief == "")//从替换表里查询
                        {
                            cmd.CommandText = "select mb_brief,custom_serial_no from DeliveredTableTransfer where track_serial_no_transfer='" + this.tracker_bar_textBox.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                mb_brief = querySdr[0].ToString();
                                _8s = querySdr[1].ToString();
                            }
                            querySdr.Close();
                        }

                        cmd.CommandText = "select customFault,repair_date from repair_record_table where custom_serial_no='" + _8s + "' order by Id desc";

                        querySdr = cmd.ExecuteReader();
                        string faultContent = "";
                        while (querySdr.Read())
                        {
                            faultContent += "：" + querySdr[0].ToString() + "," + Utils.modifyDataFormat(querySdr[1].ToString()) + "\n";
                        }
                        querySdr.Close();
                        this.repairedLabel.Text = faultContent;
                    }
                    else if (existBuffer)
                    {
                        cmd.CommandText = "select mb_brief,custom_serial_no from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            mb_brief = querySdr[0].ToString();
                            _8s = querySdr[1].ToString();
                        }
                        querySdr.Close();
                    }

                    // 所有更换过的BGA的主板，或者维修记录最后一条记录是NTF的， 测试2 running的时间必须在一个小时以上
                    bool isBgaRepaired = false;
                    cmd.CommandText = "select top 1 Id from bga_repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        isBgaRepaired = true;
                    }
                    querySdr.Close();

                    bool isNTF = false;

                    cmd.CommandText = "select top 1 track_serial_no from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' and _action='NTF' order by Id desc";

                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows )
                    {
                        isNTF = true;
                    }               
                    querySdr.Close();


                    //需要加入后台管理信息
                    cmd.CommandText = "select able from functionControltable where funtion='test2_jump'";

                    querySdr = cmd.ExecuteReader();
                    string able = "";
                    while (querySdr.Read())
                    {
                        able = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (able == "1")//只有为1的时候才可以检查
                    {
                        if (isBgaRepaired || isNTF)
                        {
                            cmd.CommandText = "select top 1 inputdate from stationInfoRecord where trackno='" + this.tracker_bar_textBox.Text.Trim() + "' and station='Test1' order by inputdate desc";
                            querySdr = cmd.ExecuteReader();
                            string test1Date = "";
                            while (querySdr.Read())
                            {
                                test1Date = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            try
                            {

                                DateTime test1datetime = Convert.ToDateTime(test1Date);
                                DateTime now = DateTime.Now;
                                TimeSpan ts = now.Subtract(test1datetime);
                                if (ts.Hours < 1)
                                {
                                    if (isBgaRepaired)
                                    {
                                        MessageBox.Show("从BGA过来的测试1到测试2的不足一个小时");
                                    }
                                    if (isNTF)
                                    {
                                        MessageBox.Show("从NTF过来的测试1到测试2的不足一个小时");
                                    }
                                    mConn.Close();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString() + "Test1Date的时间是：【" + test1Date + "】");
                            }
                        }

                    }

                    this.testerTextBox.Text = LoginForm.currentUser;

                    //检查文件是否存在
                    if (Utils.existAndCopy3DToServer(_8s, "test2", this.testerTextBox.Text.Trim(), mb_brief) == false)
                    {
                        MessageBox.Show("追踪条码的3DMark Log内容为空，请检查！");
                        mConn.Close();
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
                        + this.tracker_bar_textBox.Text.Trim().ToUpper() + "','"
                        + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Test2', updateDate =GETDATE()  "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim().ToUpper() +
               "','Test2','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO test_all_result_record VALUES('"
                      + this.tracker_bar_textBox.Text.Trim().ToUpper() + "','"
                      + this.testerTextBox.Text.Trim() + "',GETDATE(),'Pass','','Test2')";
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


                    cmd.CommandText = "INSERT INTO test_all_result_record VALUES('"
                        + this.tracker_bar_textBox.Text.Trim().ToUpper() + "','"
                        + this.testerTextBox.Text.Trim() + "',GETDATE(),'Fail','" + this.failDescribe.Text.Trim() + "','Test2')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
              "','Test2','" + this.testerTextBox.Text.Trim().ToUpper() + "',GETDATE())";
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

        private void failDescribe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.failDescribe.Text.Trim() != "" && Regex.IsMatch(this.failDescribe.Text.Trim(), @"^[+-]?\d*[.]?\d*$"))
                {
                    try
                    {
                        this.failDescribe.Text = myDictionary[this.failDescribe.Text.Trim()];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("故障代码" + this.failDescribe.Text.Trim() + "不存在");
                    }
                }
            }
        }
    }
}
