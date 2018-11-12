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
    public partial class AdjustStoreHouseForm : Form
    {
        public AdjustStoreHouseForm()
        {
            InitializeComponent();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.releasePlacebutton.Enabled = false;              
            }
            else
            {
                this.releasePlacebutton.Enabled = true;
            }
        }

        class useClass
        {
           public string materialName{get;set;}
            public string materialDescribe{get;set;}
            public string storeNum { get; set; }

            public string stockplace { get; set; }
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
                            cmd.CommandText = "select number,house,place from store_house where mpn ='" + temp.materialName + "'";
                            SqlDataReader querySdr = cmd.ExecuteReader();
                            string storeNum = "0";
                            while (querySdr.Read())
                            {
                                 storeNum = querySdr[0].ToString();
                                 temp.stockplace = querySdr[1].ToString()+","+querySdr[2].ToString();
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

        private void materialDestextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.materialDestextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入描述的内容");
                    this.materialDestextBox.Focus();
                    return;
                }

               // string not_good_place = this.not_good_placeTextBox.Text.Trim();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    List<useClass> list = new List<useClass>();
                    if (this.materialDestextBox.Text != "")
                    {
                        cmd.CommandText = "select material_mpn,L1, L2, L3, L4, L5, L6, L7, L8,material_describe,vendor from " + Constlist.table_name_LCFC_MBBOM + " where material_describe like '%" + this.materialDestextBox.Text.Trim() + "%'";
                        SqlDataReader querySdr = cmd.ExecuteReader();

                        while (querySdr.Read())
                        {
                            useClass useclass = new useClass();
                            string material_mpn = querySdr[0].ToString() + "_" + querySdr[10].ToString();
                            string temp = querySdr[1].ToString().Trim();
                            string matertialDes = querySdr[9].ToString();

                            useclass.materialName = material_mpn;
                            useclass.materialDescribe = matertialDes;
                            list.Add(useclass);
                        }
                        querySdr.Close();
                    }

                    if (list.Count == 0)
                    {                     
                        MessageBox.Show("是否输入错误的位置信息，或者bom表信息不全！");
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        foreach (useClass temp in list)
                        {
                            cmd.CommandText = "select number,house,place from store_house where mpn ='" + temp.materialName + "'";
                            SqlDataReader querySdr = cmd.ExecuteReader();
                            string storeNum = "0";
                            while (querySdr.Read())
                            {
                                storeNum = querySdr[0].ToString();
                                temp.stockplace = querySdr[1].ToString() + "," + querySdr[2].ToString();
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
        //查询左右报表
        private void queryAll()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                DataSet ds;
                SqlDataAdapter sda;

                //左侧查询
                dataGridViewleft.DataSource = null;
                dataGridViewleft.Columns.Clear();

                string sqlStr = "select top 3 * from store_house";

                if (this.mpnlefttextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn like '%" + mpnlefttextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mpn like '%" + mpnlefttextBox.Text.Trim() + "%' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "templeft");
                dataGridViewleft.DataSource = ds.Tables[0];
                dataGridViewleft.RowHeadersVisible = false;


                //右侧查询
                dataGridViewright.DataSource = null;
                dataGridViewright.Columns.Clear();

                sqlStr = "select top 3 * from store_house";
                if (this.mpnlefttextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn like '%" + this.mpnrighttextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mpn like '%" + mpnrighttextBox.Text.Trim() + "%' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "tempright");
                dataGridViewright.DataSource = ds.Tables[0];
                dataGridViewright.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "库房", "储位", "物料", "数量" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewleft.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                DataSet ds;
                SqlDataAdapter sda;
                dataGridViewleft.DataSource = null;
                dataGridViewleft.Columns.Clear();

                string sqlStr = "select top 3 * from store_house";

                if (this.mpnlefttextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn like '%" + mpnlefttextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mpn like '%" + mpnlefttextBox.Text.Trim() + "%' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "templeft");
                dataGridViewleft.DataSource = ds.Tables[0];
                dataGridViewleft.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "库房", "储位", "物料", "数量" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewleft.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void releasePlacebutton_Click(object sender, EventArgs e)
        {
            if (dataGridViewleft.CurrentCell == null)
            {
                MessageBox.Show("左侧要转移的料没有选中");
                return;
            }

            if (dataGridViewright.CurrentCell == null)
            {
                MessageBox.Show("转移到的目的库位没有");
                return;
            }

            if (Utils.IsNum(this.transferNumtextBox.Text.Trim()) == false)
            {
                MessageBox.Show("请输入正确的数量");
                return;
            }

            //当确定不需要此储位的时候，可以释放位置,把相应的物料与数量清空即可
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    string idLeft  = dataGridViewleft.SelectedCells[0].Value.ToString();
                    string houseleft = dataGridViewleft.SelectedCells[1].Value.ToString();
                    string placeleft = dataGridViewleft.SelectedCells[2].Value.ToString();
                    string mpnleft  = dataGridViewleft.SelectedCells[3].Value.ToString();
                    string numberleft = dataGridViewleft.SelectedCells[4].Value.ToString();

                    string idright = dataGridViewright.SelectedCells[0].Value.ToString();
                    string houseright = dataGridViewright.SelectedCells[1].Value.ToString();
                    string placeright = dataGridViewright.SelectedCells[2].Value.ToString();
                    string mpnright = dataGridViewright.SelectedCells[3].Value.ToString();
                    string numberright = dataGridViewright.SelectedCells[4].Value.ToString();

                    int leftTotalNumber = Int16.Parse(numberleft) - Int16.Parse(this.transferNumtextBox.Text.Trim());
                    int rightTotalNumber = Int16.Parse(this.transferNumtextBox.Text.Trim()) + numberright == "" ? 0 : Int16.Parse(numberright);

                    cmd.CommandText = "update store_house set number = '" + leftTotalNumber + "' where house='"+houseleft+"' and place ='"+placeleft+"'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update store_house set number = '" + rightTotalNumber + "' where house='" + houseright + "' and place ='" + placeright + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("转移完毕");
                queryAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                DataSet ds;
                SqlDataAdapter sda;
                dataGridViewright.DataSource = null;
                dataGridViewright.Columns.Clear();

                string sqlStr = "select top 3 * from store_house";

                if (this.mpnrighttextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn like '%" + this.mpnrighttextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mpn like '%" + mpnrighttextBox.Text.Trim() + "%' ";
                    }
                }

                if (this.storeTargetTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where place like '%" + storeTargetTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and place like '%" + storeTargetTextBox.Text.Trim() + "%' ";
                    }
                }

                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "tempright");
                dataGridViewright.DataSource = ds.Tables[0];
                dataGridViewright.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "库房", "储位", "物料", "数量" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewright.Columns[i].HeaderText = hTxt[i];
            }
        }
    }
}

