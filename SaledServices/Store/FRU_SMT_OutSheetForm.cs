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
    public partial class FRU_SMT_OutSheetForm : Form
    {
        private String tableName = "fru_smt_out_stock";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        private string requestId = "";
        private string requestNumber = "";

        public FRU_SMT_OutSheetForm()
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
                        this.buy_typeTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.material_typeTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.mb_brieftextBox.Text.Trim() + "','" +
                        this.material_nameTextBox.Text.Trim() + "','" +
                        this.vendormaterialNoTextBox.Text.Trim() + "','" +
                        this.describeTextBox.Text.Trim() + "','" +
                        this.isDeclareTextBox.Text.Trim() + "','" +

                        this.stock_out_numTextBox.Text.Trim() + "','" +
                        this.pricePerTextBox.Text.Trim() + "','" +
                        this.stock_placetextBox.Text.Trim() + "','" +
                        this.takertextBox.Text.Trim() + "','" +
                        this.inputerTextBox.Text.Trim() + "','" +                       
                        this.use_describetextBox.Text.Trim() + "','" +
                        this.notetextBox.Text.Trim() + "','" +
                        this.input_dateTextBox.Text.Trim() + "')";
                    
                    cmd.ExecuteNonQuery();

                    //跟新请求表格的状态
                    cmd.CommandText = "update request_fru_smt_to_store_table set status = '" + this.reqeusterStatus + "',realNumber = '" + (this.stock_out_numTextBox.Text) + "', fromId='" + fru_smt_in_id + "'"
                               + " where Id = '" + this.requestId + "'";
                    cmd.ExecuteNonQuery();

                    //更新入库表的使用数量，要加上出库的数量
                    cmd.CommandText = "update fru_smt_in_stock set used_num = '" +  (Int32.Parse(this.used_num) + Int32.Parse(this.stock_out_numTextBox.Text)) + "' "
                               + "where Id = '" + fru_smt_in_id + "'";
                    cmd.ExecuteNonQuery();
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

            string[] hTxt = { "ID", "厂商", "采购类别", "客户别", "材料大类", "MPN", "MB简称", "材料名称", "厂商料号", "描述", "是否报关", "出库数量", "单价", "库位", "领用人", "出库人", "用途", "备注", "出库日期"};
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
            dr["buy_type"] = this.buy_typeTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["material_type"] = this.material_typeTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["mb_brief"]= this.mb_brieftextBox.Text.Trim();
            dr["material_name"]= this.material_nameTextBox.Text.Trim();
            dr["vendormaterialNo"] = this.vendormaterialNoTextBox.Text.Trim();
            dr["describe"] = this.describeTextBox.Text.Trim();
            dr["isdeclare"] = this.isDeclareTextBox.Text.Trim();
            dr["stock_out_num"]= this.stock_out_numTextBox.Text.Trim();
            dr["pricePer"] = this.pricePerTextBox.Text.Trim();
            dr["stock_place"] = this.stock_placetextBox.Text.Trim();
            dr["taker"] = this.takertextBox.Text.Trim();
            dr["inputer"] = this.inputerTextBox.Text.Trim();
            dr["use_describe"] = this.use_describetextBox.Text.Trim();
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
            this.buy_typeTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.material_typeTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.material_nameTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.vendormaterialNoTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.describeTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.isDeclareTextBox.Text= dataGridView1.SelectedCells[10].Value.ToString();
            this.stock_out_numTextBox.Text= dataGridView1.SelectedCells[11].Value.ToString();
            this.pricePerTextBox.Text= dataGridView1.SelectedCells[12].Value.ToString();
   
            this.stock_placetextBox.Text= dataGridView1.SelectedCells[13].Value.ToString();
            this.takertextBox.Text= dataGridView1.SelectedCells[14].Value.ToString();
            this.inputerTextBox.Text= dataGridView1.SelectedCells[15].Value.ToString();
            this.use_describetextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.notetextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();

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

        public void doRequestUsingMpn()
        {
            if (this.mb_brieftextBox.Text == "" || this.mpnTextBox.Text == "")
            {
                return;
            }

            try
            {
                this.dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select vendor,product,material_type,mpn,mb_brief, material_name,describe,isdeclare,stock_place,stock_in_num from fru_smt_in_stock where mpn='" + this.mpnTextBox.Text.Trim() + "' and mb_brief='" + this.mb_brieftextBox.Text.Trim() + "' order by Id asc";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "fru_smt_in_stock");
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.RowHeadersVisible = false;

                string[] hTxt = { "厂商", "客户别", "材料大类", "MPN", 
                                     "MB简称","材料名称", "描述","是否报关","库位", "入库数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView2.Columns[i].HeaderText = hTxt[i];
                    dataGridView2.Columns[i].Name = hTxt[i];
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
               //doRequestUsingMpn();            
            }
        }

        public void setparamters(string mb_brief, string material_mpn, string requestNumber, string index)
        {
            this.mb_brieftextBox.Text = mb_brief;
            this.mpnTextBox.Text = material_mpn;
            this.requestNumber = requestNumber;
            requestId = index;
        }


        string reqeusterStatus = "";
        string fru_smt_in_id = "";
        string used_num = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.add.Enabled = false;
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select vendor,buy_type,product,material_type,mpn,mb_brief, material_name,vendormaterialNo,describe,isdeclare,pricePer,stock_place,stock_in_num,used_num, Id from fru_smt_in_stock where mpn='" + this.mpnTextBox.Text.Trim() + "' and mb_brief='" + this.mb_brieftextBox.Text.Trim() + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                string stock_in_num ="";                
                while (querySdr.Read())
                {
                    this.vendorTextBox.Text = querySdr[0].ToString();
                    this.buy_typeTextBox.Text = querySdr[1].ToString();
                    this.productTextBox.Text = querySdr[2].ToString();
                    this.material_typeTextBox.Text = querySdr[3].ToString();
                    this.mpnTextBox.Text = querySdr[4].ToString();
                    this.mb_brieftextBox.Text = querySdr[5].ToString();
                    this.material_nameTextBox.Text = querySdr[6].ToString();
                    this.vendormaterialNoTextBox.Text = querySdr[7].ToString();
                    this.describeTextBox.Text = querySdr[8].ToString();
                    this.isDeclareTextBox.Text = querySdr[9].ToString();
                    this.pricePerTextBox.Text = querySdr[10].ToString();
                    this.stock_placetextBox.Text = querySdr[11].ToString();
                    stock_in_num = querySdr[12].ToString();
                    used_num = querySdr[13].ToString();
                    fru_smt_in_id = querySdr[14].ToString();
                }
                querySdr.Close();

                mConn.Close();

                int currentNumber = Int32.Parse(stock_in_num) - Int32.Parse(used_num);
                int requestNumber = Int32.Parse(this.requestNumber);
               
                if (currentNumber == 0)
                {
                    reqeusterStatus = "wait";
                    this.add.Enabled = false;
                    MessageBox.Show("TODO 生成购买记录！");
                    //修改请求状态
                }
                else if (requestNumber > currentNumber)
                {
                    reqeusterStatus = "part";
                    
                    DialogResult ret = MessageBox.Show("库房数量不能满足要求数量" + this.requestNumber+",是否部分分配，并生成申请记录！","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
                    if (ret == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.add.Enabled = true;
                        this.stock_out_numTextBox.Text = currentNumber+"";//给与能给的最大数量
                    }
                    else if (ret == System.Windows.Forms.DialogResult.No || ret == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.add.Enabled = false;
                    }

                    MessageBox.Show("TODO 生成购买记录！");
                }
                else if (requestNumber <= currentNumber)
                {
                    //满足条件，正常分配
                    reqeusterStatus = "close";
                    this.add.Enabled = true;
                    this.stock_out_numTextBox.Text = this.requestNumber;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void queryStock_Click(object sender, EventArgs e)
        {

        }


    }
}
