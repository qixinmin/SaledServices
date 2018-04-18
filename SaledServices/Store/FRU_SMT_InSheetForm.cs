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
    public partial class FRU_SMT_InSheetForm : Form
    {
        private String tableName = "fru_smt_in_stock";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        public FRU_SMT_InSheetForm()
        {
            InitializeComponent();

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

                //1 来源 2.客户故障	3.保内/保外	4 .客责描述
                cmd.CommandText = "select distinct buy_order_serial_no from stock_in_sheet";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.buy_order_serial_noComboBox.Items.Add(temp);
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
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.buy_order_serial_noComboBox.Text.Trim() + "','" +
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.buy_typeTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.material_typeTextBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.vendormaterialNoTextBox.Text.Trim() + "','" +
                        this.describeTextBox.Text.Trim() + "','" +
                        this.numberTextBox.Text.Trim() + "','" +
                        this.pricePerTextBox.Text.Trim() + "','" +
                        this.isDeclareTextBox.Text.Trim() + "','" +

                        this.mb_brieftextBox.Text.Trim() + "','" +
                        this.material_nameTextBox.Text.Trim() + "','" +
                        this.stock_in_numTextBox.Text.Trim() + "','" +
                        this.totalMoneyTextBox.Text.Trim() + "','" +
                        this.stock_placetextBox.Text.Trim() + "','" +                       
                        this.notetextBox.Text.Trim() + "','" +
                        this.inputerTextBox.Text.Trim() + "','" +
                        this.input_dateTextBox.Text.Trim() + "')";

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


            string[] hTxt = { "ID", "采购订单编号", "厂商", "采购类别", "客户别", "材料大类", "MPN", "厂商料号", "描述", "订单数量", "单价", "是否报关", "MB简称", "材料名称", "入库数量", "合计金额", "库位", "备注", "输入人", "日期" };
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

            dr["buy_order_serial_no"] = this.buy_order_serial_noComboBox.Text.Trim();
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["buy_type"] = this.buy_typeTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["material_type"] = this.material_typeTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["vendormaterialNo"] = this.vendormaterialNoTextBox.Text.Trim();
            dr["describe"] = this.describeTextBox.Text.Trim();
            dr["number"] = this.numberTextBox.Text.Trim();
            dr["pricePer"] = this.pricePerTextBox.Text.Trim();
            dr["isdeclare"] = this.isDeclareTextBox.Text.Trim();


            dr["mb_brief"] = this.mb_brieftextBox.Text.Trim();
            dr["material_name"] = this.material_nameTextBox.Text.Trim();
            dr["stock_in_num"] = this.stock_in_numTextBox.Text.Trim();
            dr["totalMoney"] = this.totalMoneyTextBox.Text.Trim();              
            dr["stock_place"]= this.stock_placetextBox.Text.Trim();
            dr["note"] = this.notetextBox.Text.Trim();      
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.buy_order_serial_noComboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.buy_typeTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.material_typeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.vendormaterialNoTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.describeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.numberTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.pricePerTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();

            this.isDeclareTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();

            this.mb_brieftextBox.Text= dataGridView1.SelectedCells[12].Value.ToString();
            this.material_nameTextBox.Text= dataGridView1.SelectedCells[13].Value.ToString();
            this.stock_in_numTextBox.Text= dataGridView1.SelectedCells[14].Value.ToString();
            this.totalMoneyTextBox.Text= dataGridView1.SelectedCells[15].Value.ToString();
            this.stock_placetextBox.Text= dataGridView1.SelectedCells[16].Value.ToString();
            this.notetextBox.Text= dataGridView1.SelectedCells[17].Value.ToString();
            this.inputerTextBox.Text= dataGridView1.SelectedCells[18].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[19].Value.ToString();
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

        private void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select buy_order_serial_no, vendor,buy_type,product,material_type,vendormaterialNo, describe,number,pricePer,material_name,isdeclare from stock_in_sheet where mpn='" + this.mpnTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                   
                    while (querySdr.Read())
                    {
                        this.buy_order_serial_noComboBox.Text = querySdr[0].ToString();
                        this.vendorTextBox.Text = querySdr[1].ToString();
                        this.buy_typeTextBox.Text = querySdr[2].ToString();
                        this.productTextBox.Text = querySdr[3].ToString();
                        this.material_typeTextBox.Text = querySdr[4].ToString();
                        this.vendormaterialNoTextBox.Text = querySdr[5].ToString();
                        this.describeTextBox.Text = querySdr[6].ToString();
                        this.numberTextBox.Text = querySdr[7].ToString();
                        this.pricePerTextBox.Text = querySdr[8].ToString();
                        this.material_nameTextBox.Text = querySdr[9].ToString();
                        this.isDeclareTextBox.Text = querySdr[10].ToString();
                    }
                    querySdr.Close();
                   
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
