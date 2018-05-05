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
    public partial class MB_OutSheetForm : Form
    {
        private String tableName = "mb_out_stock";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        private string requestId = "";
        private string requestNumber = "";

        public MB_OutSheetForm()
        {
            InitializeComponent();
            inputerTextBox.Text = LoginForm.currentUser;
            this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.custom_serial_notextBox.Text == "" || this.mpnTextBox.Text == ""||this.vendor_serial_notextBox.Text=="")
            {
                MessageBox.Show("要输入的内容为空！");
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

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.vendorcomboBox.Text.Trim() + "','" +
                        this.productcomboBox.Text.Trim() + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.mb_brieftextBox.Text.Trim() + "','" +
                        this.describeTextBox.Text.Trim() + "','" +
                        this.custom_serial_notextBox.Text.Trim() + "','" +
                        this.vendor_serial_notextBox.Text.Trim() + "','" +
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
            this.vendorcomboBox.Text = "";
            this.productcomboBox.Text = "";
            this.mpnTextBox.Text = "";
            this.mb_brieftextBox.Text = "";
            this.describeTextBox.Text = "";
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

                if (this.vendorcomboBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where vendor= '" + vendorcomboBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and vendor= '" + vendorcomboBox.Text.Trim() + "' ";
                    }
                }

                if (this.productcomboBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where product= '" + productcomboBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and product= '" + productcomboBox.Text.Trim() + "' ";
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

            string[] hTxt = { "ID", "厂商", "客户别", "MPN", "MPN简称", "MB描述", "客户序号", "厂商序号", "库位", "出库数量", "是否报关", "备注", "领用人", "出库人", "出库日期" };
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
           
            dr["vendor"] = this.vendorcomboBox.Text.Trim();
            dr["product"] = this.productcomboBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["mb_brief"] = this.mb_brieftextBox.Text.Trim();
            dr["describe"] = this.describeTextBox.Text.Trim();
            dr["custom_serial_no"] = this.custom_serial_notextBox.Text.Trim();
            dr["vendor_serial_no"] = this.vendor_serial_notextBox.Text.Trim();

            dr["stock_place"] = this.stock_placetextBox.Text.Trim();
            dr["out_number"] = this.stock_out_numTextBox.Text.Trim();
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
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorcomboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productcomboBox.Text = dataGridView1.SelectedCells[2].Value.ToString();           
            this.mpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();         
            this.describeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();

            this.custom_serial_notextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.vendor_serial_notextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();

            this.stock_placetextBox.Text= dataGridView1.SelectedCells[8].Value.ToString();
            this.stock_out_numTextBox.Text= dataGridView1.SelectedCells[9].Value.ToString();
            this.isDeclareTextBox.Text= dataGridView1.SelectedCells[10].Value.ToString();
            this.notetextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.takertextBox.Text= dataGridView1.SelectedCells[12].Value.ToString();
            this.inputerTextBox.Text= dataGridView1.SelectedCells[13].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
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

                    cmd.CommandText = "select vendor,product,mb_brief,describe,isdeclare from mb_in_stock where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.vendorcomboBox.Text = querySdr[0].ToString();
                        this.productcomboBox.Text = querySdr[1].ToString();
                        this.mb_brieftextBox.Text = querySdr[2].ToString();
                        this.describeTextBox.Text = querySdr[3].ToString();
                        this.isDeclareTextBox.Text = querySdr[4].ToString();
                        break;
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
        public void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                doRequestUsingMpn();   
            }
        }

        public void setparamters(string mb_brief, string material_mpn, string requestNumber, string index)
        {
            this.mb_brieftextBox.Text = mb_brief;
            this.mpnTextBox.Text = material_mpn;
            this.requestNumber = requestNumber;
            requestId = index;
        }

        private void mb_brieftextBox_KeyPress(object sender, KeyPressEventArgs e)
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

                    cmd.CommandText = "select distinct vendor from mb_in_stock where mb_brief ='" + this.mb_brieftextBox.Text + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string temp = querySdr[0].ToString();
                        if (temp != "")
                        {
                            this.vendorcomboBox.Items.Add(temp);
                        }
                    }
                    querySdr.Close();

                    cmd.CommandText = "select distinct product from mb_in_stock where  mb_brief ='" + this.mb_brieftextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string temp = querySdr[0].ToString();
                        if (temp != "")
                        {
                            this.productcomboBox.Items.Add(temp);
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
        }

        private void queryStock_Click(object sender, EventArgs e)
        {
            if (this.mb_brieftextBox.Text == "")
            {
                MessageBox.Show("BGA简称不能为空！");
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

                string sql = "select mpn,stock_place,input_number,isdeclare from mb_in_stock where mb_brief='" + this.mb_brieftextBox.Text + "'";

                if (this.vendorcomboBox.Text != "")
                {
                    sql += " and vendor='" + this.vendorcomboBox.Text + "'";
                }

                if (this.productcomboBox.Text != "")
                {
                    sql += " and product='" + this.productcomboBox.Text + "'";
                }

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "stock_in_sheet");
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.RowHeadersVisible = false;
                mConn.Close();

                string[] hTxt = { "MPN", "库位", "已有数量", "是否申报" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView2.Columns[i].HeaderText = hTxt[i];
                    dataGridView2.Columns[i].Name = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView2.CurrentRow == null)
            {
                return;
            }
            this.mpnTextBox.Text = dataGridView2.SelectedCells[0].Value.ToString();
            doRequestUsingMpn();
        }
    }
}
