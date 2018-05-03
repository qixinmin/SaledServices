﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.Repair;

namespace SaledServices.Store
{
    public partial class RequestFRUSMTStoreForm : Form
    {
        public RequestFRUSMTStoreForm()
        {
            InitializeComponent();

            this.dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            requesterTextBox.Text = LoginForm.currentUser;
        }

        string status = "request";
        private void requestbutton_Click(object sender, EventArgs e)
        {
            if (this.mb_brieftextBox.Text.Trim() == "" 
                || this.not_good_placeTextBox.Text.Trim() == ""
                || this.numberTextBox.Text.Trim() == ""
                || this.materialMpnComboBox.Text.Trim() == "")
            {
                MessageBox.Show("输入框的内容不能为空！");
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
                    cmd.CommandText = "INSERT INTO request_fru_smt_to_store_table VALUES('"
                        + this.mb_brieftextBox.Text.Trim() + "','"
                        + this.not_good_placeTextBox.Text.Trim() + "','"
                        + this.materialMpnComboBox.Text.Trim() + "','"
                        + this.numberTextBox.Text.Trim() + "','"
                        +  "0','"//realNumber, 开始为0
                        + this.requesterTextBox.Text.Trim() + "','"
                        + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                        + status + "','"
                        + "" + "','"
                        + "" + "','"
                        + "" + "','"
                        + "" + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("发送请求成功，请到库房领料！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void not_good_placeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.mb_brieftextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先MB简称的内容");
                    this.mb_brieftextBox.Focus();
                    return;
                }

                string not_good_place = this.not_good_placeTextBox.Text.Trim();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;
                  
                    if (this.mb_brieftextBox.Text != "")
                    {
                        cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8 from " +  Constlist.table_name_LCFC_MBBOM +" where mb_brief ='" + this.mb_brieftextBox.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        this.materialMpnComboBox.Items.Clear();
                        while (querySdr.Read())
                        {
                            string material_mpn = querySdr[0].ToString(); ;
                            string temp = querySdr[1].ToString();
                            if (temp != "" && temp.ToLower() == not_good_place.ToLower())
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[2].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[3].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[4].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[5].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[6].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[7].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            } temp = querySdr[8].ToString();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                this.materialMpnComboBox.Items.Add(material_mpn);
                                continue;
                            }
                        }
                        querySdr.Close();
                    }

                    if (this.materialMpnComboBox.Items.Count == 0)
                    {
                        error = true;
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                        mConn.Close();
                        return;
                    }
                    //else
                    //{
                    //    cmd.CommandText = "select material_vendor_pn from LCFC71BOM_table where material_mpn='" + this.material_mpntextBox.Text.Trim() + "'";
                    //    querySdr = cmd.ExecuteReader();
                    //    string material_71pn_txt = "";
                    //    while (querySdr.Read())
                    //    {
                    //        material_71pn_txt = querySdr[0].ToString();
                    //        if (material_71pn_txt != "")
                    //        {
                    //            this.material_71pntextBox.Text = material_71pn_txt;
                    //        }
                    //        else
                    //        {
                    //            error = true;
                    //            MessageBox.Show("LCFC71BOM表中" + this.material_mpntextBox.Text.Trim() + "信息不全！");
                    //        }
                    //    }
                    //    querySdr.Close();
                    //}

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void checkRequestListbutton_Click(object sender, EventArgs e)
        {
            RrepareUseListForm prepareUseList = new RrepareUseListForm(null);
            prepareUseList.MdiParent = Program.parentForm;
            prepareUseList.Show();
        }
    }
}
