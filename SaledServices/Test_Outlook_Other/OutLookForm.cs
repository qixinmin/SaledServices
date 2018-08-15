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
    public partial class OutLookForm : Form
    {
        private String tableName = "outlookcheck";
        public OutLookForm()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
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

                    cmd.CommandText = "select product from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string product = "";
                    while (querySdr.Read())
                    {
                        product = querySdr[0].ToString();
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
                            this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
                    }
                    else 
                    {
                        MessageBox.Show("追踪条码的不在测试列表中，请检查！");
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

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                   
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '外观', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
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

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();
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
    }
}
