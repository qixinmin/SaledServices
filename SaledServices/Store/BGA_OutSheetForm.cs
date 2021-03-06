﻿using System;
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
            this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.takertextBox.Text == "")
            {
                MessageBox.Show("领用人为空!");
                return;
            }

            if (this.stock_out_numTextBox.Text == "")
            {
                MessageBox.Show("领用数量为空!");
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

                    //需要更新库房对应储位的数量 减去 本次出库的数量
                    //根据mpn查对应的查询
                    cmd.CommandText = "select house,place,Id,number from store_house where mpn='" +this.mpnTextBox.Text.Trim() + "'";
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

                    int currentNum = (Int32.Parse(number) - Int32.Parse(this.stock_out_numTextBox.Text));
                    if (currentNum >= 0)
                    {

                        cmd.CommandText = "update store_house set number = '" + currentNum + "'  where house='" + house + "' and place='" + place + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                            "" + "','" +
                            "" + "','" +
                            this.mpnTextBox.Text.Trim() + "','" +
                            this.bga_brieftextBox.Text.Trim() + "','" +
                            this.bgadescribeTextBox.Text.Trim() + "','" +
                            this.stock_placetextBox.Text.Trim() + "','" +
                            this.stock_out_numTextBox.Text.Trim() + "','" +
                            this.notetextBox.Text.Trim() + "','" +
                            this.takertextBox.Text.Trim() + "','" +
                            this.inputerTextBox.Text.Trim() + "','" +
                            this.input_dateTextBox.Text.Trim() + "')";

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("数量不对，不能出库！");
                        conn.Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                clearTexts();
                query_Click(null, null);
                similatorBGABrief();
                MessageBox.Show("BGA出库成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearTexts()
        {           
            this.mpnTextBox.Text = "";
           // this.bga_brieftextBox.Text = "";
            this.bgadescribeTextBox.Text = "";
            this.stock_placetextBox.Text = "";
            this.stock_out_numTextBox.Text = "";
            this.notetextBox.Text = "";
            this.takertextBox.Text = "";          
            this.input_dateTextBox.Text = "";
            this.currentStockNumbertextBox.Text = "";
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr =  "select top 100 * from " + tableName;              

                sqlStr += " order by Id desc";

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

            string[] hTxt = { "ID", "厂商", "客户别", "MPN", "BGA简称", "BGA描述", "库位", "出库数量",  "备注", "领用人", "出库人", "出库日期"};
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
           
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["bga_brief"]= this.bga_brieftextBox.Text.Trim();
            dr["bga_describe"] = this.bgadescribeTextBox.Text.Trim();
            dr["stock_place"] = this.stock_placetextBox.Text.Trim();
            dr["stock_out_num"]= this.stock_out_numTextBox.Text.Trim();
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
            //this.vendorcomboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //this.productcomboBox.Text = dataGridView1.SelectedCells[2].Value.ToString();           
            this.mpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.bga_brieftextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();         
            this.bgadescribeTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.stock_placetextBox.Text= dataGridView1.SelectedCells[6].Value.ToString();
            this.stock_out_numTextBox.Text= dataGridView1.SelectedCells[7].Value.ToString();
            this.notetextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.takertextBox.Text= dataGridView1.SelectedCells[9].Value.ToString();
            this.inputerTextBox.Text= dataGridView1.SelectedCells[10].Value.ToString();
            this.input_dateTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
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
                this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.CommandText = "select bga_describe,describe from bga_in_stock where mpn='" + this.mpnTextBox.Text.Trim().Split('_')[0] + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.bga_brieftextBox.Text = querySdr[0].ToString();
                        this.bgadescribeTextBox.Text = querySdr[1].ToString();
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
            this.bga_brieftextBox.Text = mb_brief;
            this.mpnTextBox.Text = material_mpn;
            this.requestNumber = requestNumber;
            requestId = index;
        }

        private void queryStock_Click(object sender, EventArgs e)
        {
            if (this.bga_brieftextBox.Text == "")
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

                string sql = "select distinct mpn,stock_place,input_number,vendor,bga_describe from bga_in_stock where bga_describe like '%" + this.bga_brieftextBox.Text + "%'";
            
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "stock_in_sheet");
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.RowHeadersVisible = false;
                mConn.Close();

                string[] hTxt = { "MPN", "库位", "买入数量", "厂商","描述" };
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

        class useClass
        {
            public string mpn ;//{ get; set; }
            public string vendor;// { get; set; }
            public string mpnVendor { get; set; }
            public string storeNum { get; set; }

            public string stockplace { get; set; }
            public string house ;//{ get; set; }
            public string place ;//{ get; set; }
        }

        private void bga_brieftextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                similatorBGABrief();
            }
        }

        private void similatorBGABrief()
        {
            try
            {
                List<useClass> orilist = new List<useClass>();
                List<useClass> targetList = new List<useClass>();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select distinct mpn, vendor from bga_in_stock where describe like '%" + this.bga_brieftextBox.Text + "%'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    useClass useclass = new useClass();
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        useclass.mpn = temp;
                        useclass.vendor = querySdr[1].ToString();
                        useclass.mpnVendor = useclass.mpn + "_" + useclass.vendor;
                        orilist.Add(useclass);
                    }
                }
                querySdr.Close();

                foreach (useClass temp in orilist)
                {
                    cmd.CommandText = "select house,place,number from store_house where mpn='" + temp.mpn + "_" + temp.vendor + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        try
                        {
                            int number = Int16.Parse(querySdr[2].ToString());
                            if (number > 0)
                            {
                                temp.house = querySdr[0].ToString();
                                temp.place = querySdr[1].ToString();
                                temp.storeNum = querySdr[2].ToString();
                                temp.stockplace = temp.house + "," + temp.place;

                                targetList.Add(temp);
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    querySdr.Close();
                }

                this.dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();

                dataGridView2.DataSource = targetList;
                dataGridView2.RowHeadersVisible = false;


                string[] hTxt = { "MPN_Vendor", "数量", "位置" };
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                return;
            }
            this.mpnTextBox.Text = dataGridView2.SelectedCells[0].Value.ToString();
            this.currentStockNumbertextBox.Text = dataGridView2.SelectedCells[1].Value.ToString(); ;
            this.stock_placetextBox.Text = dataGridView2.SelectedCells[2].Value.ToString();
            doRequestUsingMpn();
        }

        private void buttoncheckoldsn_Click(object sender, EventArgs e)
        {
            if (oldsntextBox.Text.Trim() == "") {
                MessageBox.Show("内容为空");
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

                    //CPU的采购类别
                    cmd.CommandText = "select buy_type from bga_in_stock where bgasn='" + this.oldsntextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string isbgaExist = "首次更换";
                    while (querySdr.Read())
                    {
                        isbgaExist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select track_serial_no from bga_repair_record_table where oldSn='" + this.oldsntextBox.Text.Trim() + "'";
                     querySdr = cmd.ExecuteReader();
                    string trackno = "";
                    while(querySdr.Read())
                    {
                        trackno=querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (trackno == "")
                    {
                        MessageBox.Show("此Sn不存在与CPu待还记录中！");
                        conn.Close();
                        return;
                    }
                    else
                    {
                        cmd.CommandText = "select Id from old_cpu_sn_record where oldsn='" + this.oldsntextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                            MessageBox.Show("此Sn已经存在记录中！" + isbgaExist);
                            querySdr.Close();
                            conn.Close();
                            return;
                        }
                        querySdr.Close();

                        cmd.CommandText = "INSERT INTO old_cpu_sn_record VALUES('" +
                           trackno + "','" +
                           this.oldsntextBox.Text.Trim() + "','" +
                           this.inputerTextBox.Text.Trim() + "','" +
                           this.input_dateTextBox.Text.Trim() + "')";

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("存入数据库成功！" + isbgaExist);
                    }
                    
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                this.oldsntextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
