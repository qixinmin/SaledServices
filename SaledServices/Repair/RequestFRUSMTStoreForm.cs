using System;
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

            if (Utils.isTimeError(this.dateTextBox.Text.Trim()))
            {
                this.requestbutton.Enabled = false;
            }
        }

        private void requestbutton_Click(object sender, EventArgs e)
        {
            if (this.mb_brieftextBox.Text.Trim() == "" 
                || this.not_good_placeTextBox.Text.Trim() == ""
                || this.numberTextBox.Text.Trim() == ""
                || this.materialMpnTextBox.Text.Trim() =="")
            {
                MessageBox.Show("输入框的内容不能为空！");
                return;
            }

            if (Int32.Parse(this.numberTextBox.Text.Trim()) > chooseTotalNum)
            {
                MessageBox.Show("输入的数量大于库存数量！");
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
                        + this.materialMpnTextBox.Text.Trim() + "','"
                        + this.materialDescribetextBox.Text.Trim().Replace('\'','_') + "','"
                        + this.numberTextBox.Text.Trim() + "','"
                        + this.numberTextBox.Text.Trim() + "','"//realNumber, 开始为跟申请数量一样
                        + this.requesterTextBox.Text.Trim() + "','"
                        + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                        + "close" + "','"
                        + "" + "','"
                        + "" + "','"
                        + "" + "','"
                        + "" + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.materialMpnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string house = "", place = "", Id = "", number = "";
                    while (querySdr.Read())
                    {
                        house = querySdr[0].ToString();
                        place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        number = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "update store_house set number = '" + (Int32.Parse(number) - Int32.Parse(this.numberTextBox.Text)) + "'  where house='" + house + "' and place='" + place + "'";
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
        class useClass
        {
           public string materialName{get;set;}
            public string materialDescribe{get;set;}
            public string storeNum { get; set; }
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

                    List<useClass> list = new List<useClass>();
                    if (this.mb_brieftextBox.Text != "")
                    {
                        cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8,material_describe,vendor from " + Constlist.table_name_LCFC_MBBOM + " where mb_brief ='" + this.mb_brieftextBox.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        
                        while (querySdr.Read())
                        {
                            useClass useclass = new useClass();
                            string material_mpn = querySdr[0].ToString() + "_" + querySdr[10].ToString();
                            string temp = querySdr[1].ToString().Trim();
                            string matertialDes = querySdr[9].ToString();
                           
                            if (temp != "" && temp.ToLower() == not_good_place.ToLower())
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[2].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[3].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[4].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[5].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[6].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[7].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            } temp = querySdr[8].ToString().Trim();
                            if (temp != "" && temp.ToLower().Equals(not_good_place.ToLower()))
                            {
                                useclass.materialName = material_mpn;
                                useclass.materialDescribe = matertialDes;
                                list.Add(useclass);
                                continue;
                            }
                            
                        }
                        querySdr.Close();
                    }

                    if (list.Count == 0)
                    {
                        error = true;
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        foreach (useClass temp in list)
                        {
                            cmd.CommandText = "select number from store_house where mpn ='" + temp.materialName + "'";
                            SqlDataReader querySdr = cmd.ExecuteReader();
                            string storeNum = "0";
                            while (querySdr.Read())
                            {
                                 storeNum = querySdr[0].ToString();
                            }
                            temp.storeNum = storeNum;
                            querySdr.Close();
                        }

                        dataGridView.DataSource = list;
                    }

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

        int chooseTotalNum = 0;
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                return;
            }

            //先判断数量，如果数量不对，则不能选择
            chooseTotalNum =Int32.Parse(dataGridView.SelectedCells[2].Value.ToString());
            if (chooseTotalNum <= 0)
            {
                MessageBox.Show("库存不足，不能选择！");
                return;
            }

            this.materialMpnTextBox.Text = dataGridView.SelectedCells[0].Value.ToString();
            this.materialDescribetextBox.Text = dataGridView.SelectedCells[1].Value.ToString();
        }

        private void mb_brieftextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                not_good_placeTextBox.Focus();
                not_good_placeTextBox.SelectAll();
            }
        }
    }
}

