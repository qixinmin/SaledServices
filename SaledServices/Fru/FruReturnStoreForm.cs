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
    public partial class FruReturnStoreForm : Form
    {
        string tableName = "frureturnStore";

        private string vendorStr = "";
        private string productStr = "";

        public FruReturnStoreForm()
        {
            InitializeComponent();
            loadToReturnInformation();
            this.receiverTextBox.Text = LoginForm.currentUser;
            this.receive_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }
        
        public void loadToReturnInformation()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct vendor from frureceiveOrder";
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

                cmd.CommandText = "select distinct product from frureceiveOrder";
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
                dataGridViewToReturn.DataSource = null;
                dataGridViewToReturn.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,receivedNum,returnNum,ordertime from frureceiveOrder where vendor='" + vendorStr 
                    + "' and product ='" + productStr + "' and _status = 'close'";                

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "frureceiveOrder");
                dataGridViewToReturn.DataSource = ds.Tables[0];
                dataGridViewToReturn.RowHeadersVisible = false;


                string[] hTxt = { "订单编号", "客户料号", "MB简称", "收货数量", "还货数量", "制单时间" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewToReturn.Columns[i].Name = hTxt[i];
                }

                DataGridViewColumn dc = new DataGridViewColumn();
                dc.Name = "TAT";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "TAT";
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

                        dr.Cells["TAT"].Value = overdays + " ";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                mConn.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridViewToReturn.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void simulateEnter(string custommaterialNo,string orderNo, string tat)
        {
            try
            {              
                this.ordernotextBox.Text = orderNo;// dataGridViewToReturn.SelectedCells[0].Value.ToString();
                this.customermaterialnoTextBox.Text = custommaterialNo;// dataGridViewToReturn.SelectedCells[1].Value.ToString();
                // this.storehouseTextBox.Text = dataGridViewToReturn.SelectedCells[2].Value.ToString();

                this.tattextBox.Text = tat;// dataGridViewToReturn.SelectedCells[5].Value.ToString();

                //根据输入的客户料号，查询MB物料对照表找到dpk状态与mpn
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select machine_type,name,custommaterialdescribe,vendor_material_no,mpn1 from frubomtable where custom_material_no ='"
                                         + custommaterialNo + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {                       
                        this.machine_typeTextBox.Text = querySdr[0].ToString();
                        this.nameTextBox.Text = querySdr[1].ToString();
                        this.customermaterialdesTextBox.Text = querySdr[2].ToString();
                        this.vendor_material_noTextBox.Text = querySdr[3].ToString();
                        this.mpn1TextBox.Text = querySdr[4].ToString();
                    }
                    querySdr.Close();
                    
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        string currentMaterialNo, orderNo, tat;
        private void dataGridViewToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewToReturn.CurrentRow == null)
            {
                return;
            }

            currentMaterialNo = this.customermaterialnoTextBox.Text = dataGridViewToReturn.SelectedCells[1].Value.ToString();
            orderNo = this.ordernotextBox.Text = dataGridViewToReturn.SelectedCells[0].Value.ToString();
            tat = this.tattextBox.Text = dataGridViewToReturn.SelectedCells[6].Value.ToString();
            simulateEnter(this.customermaterialnoTextBox.Text.Trim(), orderNo, tat);
        }

        private void returnStore_Click(object sender, EventArgs e)
        {
            if (checkIsNull())
            {
                MessageBox.Show("输入的内容为空, 请检查！");
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

                    SqlDataReader querySdr = null;

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                       this.ordernotextBox.Text.Trim() + "','" +
                       this.vendorComboBox.Text.Trim() + "','" +
                       this.productComboBox.Text.Trim() + "','" +
                       this.customermaterialnoTextBox.Text.Trim() + "','" +
                       this.machine_typeTextBox.Text.Trim() + "','" +
                       this.nameTextBox.Text.Trim() + "','" +
                       this.customermaterialdesTextBox.Text.Trim() + "','" +
                       this.peijian_noTextBox.Text.Trim() + "','" +
                       this.customer_serial_noTextBox.Text.Trim() + "','" +
                       this.custom_faultTextBox.Text.Trim() + "','" +
                       this.makedatetextBox.Text.Trim() + "','" +
                       this.guaranteeTextBox.Text.Trim() + "','" +
                       this.gurantee_noteTextBox.Text.Trim() + "','" +
                       this.vendor_material_noTextBox.Text.Trim() + "','" +
                       this.mpn1TextBox.Text.Trim() + "','" +
                       this.receiverTextBox.Text.Trim() + "','" +
                       this.receive_dateTextBox.Text.Trim() + "','" +
                        this.tattextBox.Text.Trim() + "','" +
                        this.statusComboBox.Text.Trim() +
                       "')";

                    cmd.ExecuteNonQuery();

                    //在更新收货表的同时，需要同时更新导入的表格收货数量，不然数据会乱掉
                    cmd.CommandText = "select _status, ordernum, receivedNum, returnNum,cid_number from frureceiveOrder where orderno = '" + this.ordernotextBox.Text
                           + "' and custom_materialNo = '" + this.customermaterialnoTextBox.Text + "'";

                    int receivedNum = 0, returnNum =0,cidNum=0;
                    string status = "close";
                    querySdr = cmd.ExecuteReader();
                    bool isDone = false;
                    while (querySdr.Read())
                    {
                        cidNum = Int32.Parse(querySdr[4].ToString());
                        receivedNum = Int32.Parse(querySdr[2].ToString());
                        int leftNum = receivedNum - cidNum;
                        if (querySdr[3].ToString() == "")
                        {
                            returnNum = 0;
                        }
                        else
                        {
                            returnNum = Int32.Parse(querySdr[3].ToString());
                        }

                        if (returnNum >= leftNum)
                        {
                            MessageBox.Show("本料号已经还完！");
                            isDone = true;
                        }
                        else if (leftNum == returnNum + 1)
                        {
                            status = "return";
                        }
                    }
                    querySdr.Close();

                    if (isDone == false)
                    {
                        cmd.CommandText = "update frureceiveOrder set _status = '" + status + "',returnNum = '" + (returnNum + 1) +"' "
                                    + "where orderno = '" + this.ordernotextBox.Text.Trim()
                                    + "' and custom_materialNo = '" + this.customermaterialnoTextBox.Text.Trim() + "'";

                        cmd.ExecuteNonQuery();
                        
                        //dataGridViewToReturn里面的数据要更新
                        doQueryAfterSelection();
                        clearInputData();

                        if (status == "return")
                        {
                            this.clearInputData();
                            MessageBox.Show("本料号已经还完！");
                        }
                        else
                        {  
                            simulateEnter(this.currentMaterialNo, this.orderNo, this.tat);

                            int row = dataGridViewToReturn.Rows.Count;
                            for (int i = 0; i < row; i++)
                            {
                                if (currentMaterialNo == dataGridViewToReturn.Rows[i].Cells[1].Value.ToString().Trim()
                                    && this.orderNo == dataGridViewToReturn.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    dataGridViewToReturn.Rows[i].Selected = true;
                                    dataGridViewToReturn.CurrentCell = dataGridViewToReturn.Rows[i].Cells[0];
                                    break;
                                }
                            }   
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
            this.peijian_noTextBox.Focus();
            queryLastest(true);
        }

        private bool checkIsNull()
        {
            if (this.statusComboBox.Text.Trim() == "")
            {
                return true;
            }
               
            return false;           
        }

        private void clearInputData()
        {
            this.ordernotextBox.Text="";

            this.customermaterialnoTextBox.Text = "";
            this.machine_typeTextBox.Text = "";
            this.nameTextBox.Text = "";
            this.customermaterialdesTextBox.Text = "";
            this.peijian_noTextBox.Text = "";
            this.customer_serial_noTextBox.Text = "";
            this.custom_faultTextBox.Text = "";
            this.makedatetextBox.Text = "";
            this.guaranteeTextBox.Text = "";
            this.gurantee_noteTextBox.Text = "";
            this.vendor_material_noTextBox.Text = "";
            this.mpn1TextBox.Text = "";
      
            this.tattextBox.Text = "";
        }

        private void queryLastest(bool latest)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                if (latest)
                {
                    cmd.CommandText = "select top 3 * from " + tableName + " order by id desc";
                }
                else
                {
                    cmd.CommandText = "select top 3 * from  " + tableName;
                }
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

            string[] hTxt = { "ID","订单编号", "厂商", "客户别","客户料号"
            ,"机型","名称","客户物料描述","配件序号","客户序号","客户故障","生产日期","保内/保外","保外备注",
            "厂商料号","MPN1","收件人","收货日期","TAT"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewReturnedDetail.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            queryLastest(false);
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

            tableLayoutPanel4.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel4, true, null);
        }


        private void peijian_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
                    ///配件序号	客户序号	客户故障	生产日期	保内/保外	保外备注

                    cmd.CommandText = "select customer_serial_no,custom_fault,make_date,gurantee,gurantee_note  from  fruDeliveredTable where peijian_no='" + this.peijian_noTextBox.Text + "'";         

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.customer_serial_noTextBox.Text = querySdr[0].ToString();
                        this.custom_faultTextBox.Text = querySdr[1].ToString();
                        this.makedatetextBox.Text = querySdr[2].ToString();
                        this.guaranteeTextBox.Text = querySdr[3].ToString();
                        this.gurantee_noteTextBox.Text = querySdr[4].ToString();
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
