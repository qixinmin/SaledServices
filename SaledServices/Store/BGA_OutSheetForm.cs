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
    public partial class BGA_OutSheetForm : Form
    {
        private String tableName = "bga_out_stock";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        private string requestId = "";
        private string requestNumber = "";

        public BGA_OutSheetForm()
        {
            InitializeComponent();
            inputerTextBox.Text = LoginForm.currentUser;
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();
               
                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.bga_brieftextBox.Text.Trim() + "','" +
                        this.bgadescribeTextBox.Text.Trim() + "','" +
                        this.stock_placetextBox.Text.Trim() + "','" +
                        this.stock_out_numTextBox.Text.Trim() + "','" +
                        this.isDeclareTextBox.Text.Trim() + "','" +
                        this.notetextBox.Text.Trim() + "','" +
                        this.takertextBox.Text.Trim() + "','" +
                        this.inputerTextBox.Text.Trim() + "','" +
                        this.input_dateTextBox.Text.Trim() + "')";
                    
                    cmd.ExecuteNonQuery();                   

                    //需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.mpnTextBox.Text.Trim() + "'";
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

                    cmd.CommandText = "update store_house set number = '" + (Int32.Parse(number) - Int32.Parse(this.stock_out_numTextBox.Text)) + "'  where house='"+ house+"' and place='"+place+"'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                clearTexts();
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearTexts()
        {
            this.vendorTextBox.Text = "";
            this.productTextBox.Text = "";
            this.mpnTextBox.Text = "";
            this.bga_brieftextBox.Text = "";
            this.bgadescribeTextBox.Text = "";
            this.stock_placetextBox.Text = "";
            this.stock_out_numTextBox.Text = "";
            this.isDeclareTextBox.Text = "";
            this.notetextBox.Text = "";
            this.takertextBox.Text = "";          
            this.input_dateTextBox.Text = "";
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr =  "select top 100 * from " + tableName;

                if (vendorTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where vendor= '" + vendorTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and vendor= '" + vendorTextBox.Text.Trim() + "' ";
                    }
                }

                if (productTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where product= '" + productTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and product= '" + productTextBox.Text.Trim() + "' ";
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
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "厂商", "客户别", "MPN", "BGA简称", "BGA描述", "库位", "出库数量", "是否报关", "备注", "领用人", "出库人", "出库日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.idTextBox.Text.Trim());  
           
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["bga_brief"]= this.bga_brieftextBox.Text.Trim();
            dr["bga_describe"] = this.bgadescribeTextBox.Text.Trim();
            dr["stock_place"] = this.stock_placetextBox.Text.Trim();
            dr["stock_out_num"]= this.stock_out_numTextBox.Text.Trim();
            dr["isdeclare"] = this.isDeclareTextBox.Text.Trim();           
            dr["note"] = this.notetextBox.Text.Trim();
           
            dr["taker"] = this.takertextBox.Text.Trim();
            dr["inputer"] = this.inputerTextBox.Text.Trim();
            dr["input_date"] = this.input_dateTextBox.Text.Trim();

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();           
            this.mpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.bga_brieftextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();         
            this.bgadescribeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.stock_placetextBox.Text= dataGridView1.SelectedCells[6].Value.ToString();
            this.stock_out_numTextBox.Text= dataGridView1.SelectedCells[7].Value.ToString();
            this.isDeclareTextBox.Text= dataGridView1.SelectedCells[8].Value.ToString();
            this.notetextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.takertextBox.Text= dataGridView1.SelectedCells[10].Value.ToString();
            this.inputerTextBox.Text= dataGridView1.SelectedCells[11].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
        }

        private void ReceiveOrderForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
              GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
              SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel2, true, null);
            tableLayoutPanel3.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel3, true, null);

            tableLayoutPanel4.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel4, true, null);
        }

        public void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                try
                {
                    this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    SqlConnection conn = new SqlConnection(Constlist.ConStr);
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        //查询库位和数量
                        cmd.CommandText = "select house,place,Id,number from store_house where mpn='" + this.mpnTextBox.Text.Trim() + "'";
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

                        if (house == "" || place == "")
                        {
                            MessageBox.Show("此料不在库存里面！");
                            conn.Close();
                            return;
                        }
                        this.currentStockNumbertextBox.Text = number;
                        this.stock_placetextBox.Text = house + "," + place;

                        cmd.CommandText = "select vendor,product,bga_describe,describe,isdeclare from bga_in_stock where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();                       
                        while (querySdr.Read())
                        {
                            this.vendorTextBox.Text = querySdr[0].ToString();
                            this.productTextBox.Text = querySdr[1].ToString();
                            this.bga_brieftextBox.Text = querySdr[2].ToString();
                            this.bgadescribeTextBox.Text = querySdr[3].ToString();
                            this.isDeclareTextBox.Text = querySdr[4].ToString();                          
                        }
                        querySdr.Close();
                    }
                    else
                    {
                        MessageBox.Show("SaledService is not opened");
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }              
            }
        }

        public void setparamters(string mb_brief, string material_mpn, string requestNumber, string index)
        {
            this.bga_brieftextBox.Text = mb_brief;
            this.mpnTextBox.Text = material_mpn;
            this.requestNumber = requestNumber;
            requestId = index;
        }


        string reqeusterStatus = "";
        string fru_smt_in_id = "";
        string used_num = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    this.add.Enabled = false;
            //    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
            //    mConn.Open();


            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = mConn;
            //    cmd.CommandType = CommandType.Text;

            //    cmd.CommandText = "select vendor,buy_type,product,material_type,mpn,mb_brief, material_name,vendormaterialNo,describe,isdeclare,pricePer,stock_place,stock_in_num,used_num, Id from fru_smt_in_stock where mpn='" + this.mpnTextBox.Text.Trim() + "' and mb_brief='" + this.mb_brieftextBox.Text.Trim() + "'";

            //    SqlDataReader querySdr = cmd.ExecuteReader();

            //    string stock_in_num ="";                
            //    while (querySdr.Read())
            //    {
            //        this.vendorTextBox.Text = querySdr[0].ToString();
            //        this.buy_typeTextBox.Text = querySdr[1].ToString();
            //        this.productTextBox.Text = querySdr[2].ToString();
            //        this.material_typeTextBox.Text = querySdr[3].ToString();
            //        this.mpnTextBox.Text = querySdr[4].ToString();
            //        this.mb_brieftextBox.Text = querySdr[5].ToString();
            //        this.material_nameTextBox.Text = querySdr[6].ToString();
            //        this.vendormaterialNoTextBox.Text = querySdr[7].ToString();
            //        this.describeTextBox.Text = querySdr[8].ToString();
            //        this.isDeclareTextBox.Text = querySdr[9].ToString();
            //        this.pricePerTextBox.Text = querySdr[10].ToString();
            //        this.stock_placetextBox.Text = querySdr[11].ToString();
            //        stock_in_num = querySdr[12].ToString();
            //        used_num = querySdr[13].ToString();
            //        fru_smt_in_id = querySdr[14].ToString();
            //    }
            //    querySdr.Close();

            //    mConn.Close();

            //    int currentNumber = Int32.Parse(stock_in_num) - Int32.Parse(used_num);
            //    int requestNumber = Int32.Parse(this.requestNumber);
               
            //    if (currentNumber == 0)
            //    {
            //        reqeusterStatus = "wait";
            //        this.add.Enabled = false;
            //        MessageBox.Show("TODO 生成购买记录！");
            //        //修改请求状态
            //    }
            //    else if (requestNumber > currentNumber)
            //    {
            //        reqeusterStatus = "part";
                    
            //        DialogResult ret = MessageBox.Show("库房数量不能满足要求数量" + this.requestNumber+",是否部分分配，并生成申请记录！","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            //        if (ret == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            this.add.Enabled = true;
            //            this.stock_out_numTextBox.Text = currentNumber+"";//给与能给的最大数量
            //        }
            //        else if (ret == System.Windows.Forms.DialogResult.No || ret == System.Windows.Forms.DialogResult.Cancel)
            //        {
            //            this.add.Enabled = false;
            //        }

            //        MessageBox.Show("TODO 生成购买记录！");
            //    }
            //    else if (requestNumber <= currentNumber)
            //    {
            //        //满足条件，正常分配
            //        reqeusterStatus = "close";
            //        this.add.Enabled = true;
            //        this.stock_out_numTextBox.Text = this.requestNumber;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void queryStock_Click(object sender, EventArgs e)
        {

        }

        private void stock_out_numTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.currentStockNumbertextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入mpn号并回车！");
                    this.mpnTextBox.Focus();
                    return;
                }

                if (this.stock_out_numTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入数量并回车！");
                    this.stock_out_numTextBox.Focus();
                    return;
                }

                try
                {
                    int currentStockNumber = Int32.Parse(this.currentStockNumbertextBox.Text.Trim());
                    int outNumber = Int32.Parse(this.stock_out_numTextBox.Text.Trim());
                    if (outNumber > currentStockNumber)
                    {
                        MessageBox.Show("输入的数量不能大于库存数量");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


    }
}
