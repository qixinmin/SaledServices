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
    public partial class ReturnStoreForm : Form
    {
        string tableName = "returnStore";

        private string vendorStr = "";
        private string productStr = "";
        public ReturnStoreForm()
        {
            InitializeComponent();
            loadToReturnInformation();
        }
        
        public void loadToReturnInformation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct vendor from receiveOrder";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.vendorComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct product from receiveOrder";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.productComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct type from customResponsibilityType";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_res_typeComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct status from returnStoreStatus";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.statusComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct responsibility_describe from customResponsibility";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.response_describeComboBox.Items.Add(temp);
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

        private void vendorComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.vendorStr = this.vendorComboBox.Text;
            doQueryAfterSelection();
        }

        private void productComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.productStr = this.productComboBox.Text;
            doQueryAfterSelection();
        }

        private void doQueryAfterSelection()
        {
            if (this.vendorStr == "" || this.productStr == "")
            {
                return;
            }

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select orderno, custom_materialNo,storehouse, ordertime,ordernum , receivedNum, returnNum from receiveOrder where vendor='" + vendorStr 
                    + "' and product ='" + productStr + "' and status = 'close'";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
                dataGridViewToReturn.DataSource = ds.Tables[0];
                dataGridViewToReturn.RowHeadersVisible = false;
                

                string[] hTxt = { "订单编号",
                                "客户料号","仓库别","制单时间","订单数量","收货数量","还货数量"};
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewToReturn.Columns[i].Name = hTxt[i];
                }

                DataGridViewColumn dc = new DataGridViewColumn();
                dc.Name = "VAT";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "VAT";
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridViewToReturn.Columns.Add(dc);

                foreach (DataGridViewRow dr in dataGridViewToReturn.Rows)
                {
                    try
                    {
                        DateTime dt1 = Convert.ToDateTime(dr.Cells["制单时间"].Value.ToString());
                        DateTime dt2 = DateTime.Now;

                        TimeSpan ts = dt2.Subtract(dt1);
                        int overdays = ts.Days;

                        dr.Cells["VAT"].Value = overdays + " ";
                    }
                    catch (Exception ex)
                    { }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  string[] hTxt = { "订单编号",
                               // "客户料号","仓库别","制单时间","订单数量","收货数量","还货数量"};
            try
            {
                this.return_file_noTextBox.Text = generateFileNo();
                this.ordernoTextBox.Text = dataGridViewToReturn.SelectedCells[0].Value.ToString();
                this.custommaterialNoTextBox.Text = dataGridViewToReturn.SelectedCells[1].Value.ToString();
                this.storehouseTextBox.Text = dataGridViewToReturn.SelectedCells[2].Value.ToString();
                this.return_dateTextBox.Text = DateTime.Now.ToString("yyyyMMdd");

                this.tatTextBox.Text = dataGridViewToReturn.SelectedCells[7].Value.ToString();
            }
            catch (Exception ex)
            { }
        }

        private string generateFileNo()
        {
            string retStr = "";
            string preStr = this.vendorComboBox.Text.Trim() + this.productComboBox.Text.Trim() + DateTime.Now.ToString("yyMMdd");

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct return_file_no from returnStore";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string subRetStr= "";
                while (querySdr.Read())
                {
                    
                    string queryStr = querySdr[0].ToString();
                    subRetStr = queryStr.Substring(preStr.Length, 2);
                }
                querySdr.Close();

                if (subRetStr == "")
                {
                    retStr = preStr + "01";
                }
                else
                {
                    int last = Int32.Parse(subRetStr);

                    if (checkBoxMakeNew.Checked)
                    {
                        last += 1;
                    }

                    if (last < 10)
                    {
                        retStr = preStr + "0" + last;
                    }
                    else
                    {
                        retStr = preStr + last;
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return retStr;
        }

        private void returnStore_Click(object sender, EventArgs e)
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
                        
                        this.vendorComboBox.Text.Trim() + "','" +
                        this.productComboBox.Text.Trim() + "','" +
                        this.return_file_noTextBox.Text.Trim() + "','" +
                        this.storehouseTextBox.Text.Trim() + "','" +
                        this.return_dateTextBox.Text.Trim() + "','" +
                        this.ordernoTextBox.Text.Trim() + "','" +
                        this.custommaterialNoTextBox.Text.Trim() + "','" +
                        this.dpkpnTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "','" +
                        this.vendor_serail_noTextBox.Text.Trim() + "','" +
                        this.vendormaterialNoTextBox.Text.Trim() + "','" +
                        this.statusComboBox.Text.Trim() + "','" +
                        this.custom_res_typeComboBox.Text.Trim() + "','" +
                        this.response_describeComboBox.Text.Trim() + "','"+
                        this.tatTextBox.Text.Trim()+
                        "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                    //在更新收货表的同时，需要同时更新导入的表格收货数量，不然数据会乱掉
                    cmd.CommandText = "select status, ordernum, receivedNum, returnNum from receiveOrder where orderno = '" + this.ordernoTextBox.Text
                           + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                    int receivedNum = 0, returnNum =0;
                    string status = "close";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    bool isDone = false;
                    while (querySdr.Read())
                    {                       
                        receivedNum = Int32.Parse(querySdr[2].ToString());
                        returnNum = Int32.Parse(querySdr[3].ToString());

                        if (returnNum >= receivedNum)
                        {
                            MessageBox.Show("本料号已经还完！");
                            isDone = true;
                        }
                        else if (receivedNum == returnNum + 1)
                        {
                            status = "return";
                        }                        
                    }
                    querySdr.Close();

                    if (isDone == false)
                    {
                        cmd.CommandText = "update receiveOrder set status = '" + status + "',returnNum = '" + (returnNum + 1) +"' "
                                    + "where orderno = '" + this.ordernoTextBox.Text
                                    + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                        cmd.ExecuteNonQuery();
                        if (status == "return")
                        {
                            MessageBox.Show("本料号已经还完！");
                        }
                    }
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

            //dataGridViewToReturn里面的数据要更新
            doQueryAfterSelection();
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);               
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  " + tableName;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridViewReturnedDetail.DataSource = ds.Tables[0];
                dataGridViewReturnedDetail.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "厂商","客户别","还货文件编号","客户库别","还货时间","订单编号","客户料号","DPK状态",
                                "跟踪条码","客户序号","厂商序号","厂商料号","状态","客责类别","客责描述","TAT" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewReturnedDetail.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
           
        }

        private void ReturnStoreForm_Load(object sender, EventArgs e)
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
        }

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //DPK状态	跟踪条码	客户序号	厂商序号	厂商料号

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select dpk_status, custom_serial_no, vendor_serail_no, vendormaterialNo from DeliveredTable where track_serial_no = '"
                        + this.track_serial_noTextBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.dpkpnTextBox.Text = querySdr[0].ToString();
                        this.custom_serial_noTextBox.Text = querySdr[1].ToString();
                        this.vendor_serail_noTextBox.Text = querySdr[2].ToString();
                        this.vendormaterialNoTextBox.Text = querySdr[3].ToString();
                    }
                    querySdr.Close();                    

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.custom_serial_noTextBox.Focus();
                this.custom_serial_noTextBox.SelectAll();
            }
        }

        private void custommaterialNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.track_serial_noTextBox.Focus();
                this.track_serial_noTextBox.SelectAll();
            }
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.response_describeComboBox.Focus();
            }
        }
    }
}
