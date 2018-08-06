using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class FaultBatchMBOutStoreForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "store_house_ng_out";

        //string notgood_house = "", notgood_place = "";

        private ChooseStock chooseStock = new ChooseStock();

        public FaultBatchMBOutStoreForm()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.mpnTextBox.Text.Trim() == "" || this.numberTextBox.Text.Trim() == "" || this.storehoustTextBox.Text == "")
            {
                MessageBox.Show("需要输入的内容为空!");
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

                    //不良品库, 需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house_ng where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string house = "", place = "", Id = "", number = "0";
                    while (querySdr.Read())
                    {
                        this.housetextBox.Text = house = querySdr[0].ToString();
                        this.placetextBox.Text = place = querySdr[1].ToString();
                        Id = querySdr[2].ToString();
                        this.currentNumberTextBox.Text = number = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    //若库房不存在，则自动生成库
                    if (house == "")
                    {
                        MessageBox.Show("要出库的MB不存在！");                        
                    }
                    else
                    {
                        if ( Int32.Parse(this.numberTextBox.Text) > Int32.Parse(this.currentNumberTextBox.Text))
                        {
                            MessageBox.Show("要出库的数量大于库存数量！");
                            conn.Close();
                            this.add.Enabled = false;
                            return;
                        }
                        this.add.Enabled = true;

                        cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                            + this.housetextBox.Text.Trim() + "','"
                            + this.placetextBox.Text.Trim() + "','"
                            + this.mpnTextBox.Text.Trim() + "','"
                            + this.numberTextBox.Text.Trim() + "','"
                            + LoginForm.currentUser + "','"
                            + DateTime.Now.ToString("yyyy/MM/dd") + "')";

                        cmd.ExecuteNonQuery();
                        this.storehoustTextBox.Text = "";


                        cmd.CommandText = "update store_house_ng set number = '" + (Int32.Parse(this.currentNumberTextBox.Text) - Int32.Parse(this.numberTextBox.Text)) + "', mpn='" 
                            + this.mpnTextBox.Text.Trim() + "'  where house='" + this.housetextBox.Text.Trim() + "' and place='" + this.placetextBox.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.ExecuteNonQuery();  
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                this.mpnTextBox.Text = "";
                this.numberTextBox.Text = "";
                this.currentNumberTextBox.Text = "";
                this.housetextBox.Text = "";
                this.placetextBox.Text = "";
                MessageBox.Show("新增成功！");
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  " + tableName;
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "库房", "储位", "MPN", "数量", "录入人", "录入时间" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
            MessageBox.Show("查询完成！");
        }

        private void modify_Click(object sender, EventArgs e)
        {
            //DataTable dt = ds.Tables[tableName];
            //sda.FillSchema(dt, SchemaType.Mapped);
            //DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());
            //dr["house"] = this.housetextBox.Text.Trim();
            //dr["place"] = this.placetextBox.Text.Trim();
            //dr["mpn"] = this.mpnTextBox.Text.Trim();
            //dr["number"] = this.numberTextBox.Text.Trim();            

            //SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            //sda.Update(dt);
            //MessageBox.Show("修改成功！");
        }

        private void delete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlConnection conn = new SqlConnection(Constlist.ConStr);
            //    conn.Open();

            //    if (conn.State == ConnectionState.Open)
            //    {
            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = conn;
            //        cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
            //        cmd.CommandType = CommandType.Text;
            //        cmd.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        MessageBox.Show("SaledService is not opened");
            //    }

            //    conn.Close();
            //    MessageBox.Show("删除完毕!");
            //    query_Click(null, null);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            //this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            //this.housetextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //this.placetextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            //this.mpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            //this.numberTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            //this.storehoustTextBox.Text = this.housetextBox.Text + "," + this.placetextBox.Text;
        }

        public void setChooseStock(string id, string house, string place)
        {
            chooseStock.Id = id;
            chooseStock.house = house;
            chooseStock.place = place;

            this.storehoustTextBox.Text = chooseStock.house + "," + chooseStock.place;
            this.housetextBox.Text = chooseStock.house;
            this.placetextBox.Text = chooseStock.place;
        }

        private void storehoustTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //打开选择界面，并把结果返回到本界面来
                ChooseStoreHouseForm csform = new ChooseStoreHouseForm(this, "store_house_ng");
                csform.MdiParent = Program.parentForm;
                csform.Show();
            }
        }

        private void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    this.housetextBox.Text = "";
                    this.placetextBox.Text = "";
                    this.storehoustTextBox.Text = "";
                    this.currentNumberTextBox.Text = "";
                    this.numberTextBox.Text = "";

                    cmd.CommandText = "select house,place,Id,number from store_house_ng where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string ghouse = "", gplace = "";
                    while (querySdr.Read())
                    {
                        this.housetextBox.Text = ghouse = querySdr[0].ToString();
                        this.placetextBox.Text = gplace = querySdr[1].ToString();
                        this.currentNumberTextBox.Text = querySdr[3].ToString();
                    }
                    querySdr.Close();

                    
                    if (ghouse == "" || gplace == "")
                    {
                        MessageBox.Show("此料号不存在与不良品库！");
                        this.add.Enabled = false;
                        mConn.Close();
                        return;
                    }
                    else
                    {                      
                        this.storehoustTextBox.Text = this.housetextBox.Text.Trim() + "," + this.placetextBox.Text.Trim();
                    }

                    this.add.Enabled = true;
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
             }
        }
    }
}
