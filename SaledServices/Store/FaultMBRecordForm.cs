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

namespace SaledServices
{
    public partial class FaultMBRecordForm : Form
    {
        private string tableName = "fault_mb_enter_record_table";
        private ChooseStock chooseStock = new ChooseStock();
        private PrepareUseDetail mPrepareUseDetail;
        public FaultMBRecordForm()
        {
            InitializeComponent();

            inputertextBox.Text = LoginForm.currentUser;
            this.input_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");
            mPrepareUseDetail = new PrepareUseDetail();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }          

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    this.track_serial_noTextBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from "+tableName+" where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();

                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        MessageBox.Show("此序列号的板子已经登记过了！");
                        this.add.Enabled = false;
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        querySdr.Close();
                        this.add.Enabled = true;
                    }

                    cmd.CommandText = "select vendor, product,mb_brief,mpn,mb_describe from fault_mb_confirm_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string vendor = "";
                    while (querySdr.Read())
                    {
                        vendor = this.vendorTextBox.Text = querySdr[0].ToString();
                        this.producttextBox.Text = querySdr[1].ToString();
                        this.mb_brieftextBox.Text = querySdr[2].ToString();
                        this.mpntextBox.Text = querySdr[3].ToString();
                        this.mbdescribeTextBox.Text = querySdr[4].ToString();
                    }
                    querySdr.Close();

                    if (vendor == "")
                    {
                        MessageBox.Show("此序列号的板子不在主板报废判定表中！");
                        this.add.Enabled = false;
                        mConn.Close();
                        return;
                    }
                    this.add.Enabled = true;

                    cmd.CommandText = "select pcb_describe,vga_describe,cpu_describe from MBMaterialCompare where mpn='" + this.mpntextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();                 
                    while (querySdr.Read())
                    {
                        this.PCHbrieftextBox.Text = querySdr[0].ToString();
                        this.VGAbreiftextBox.Text = querySdr[1].ToString();
                        this.CPUbrieTtextBox.Text = querySdr[2].ToString();
                        break;
                    }
                    querySdr.Close();

                    //确定库位
                    cmd.CommandText = "select Id,house, place from store_house_ng_buffer_mb where mpn='" + this.mpntextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string id="",house="",place="";
                    while (querySdr.Read())
                    {
                        id = querySdr[0].ToString();
                        house = querySdr[1].ToString();
                        place = querySdr[2].ToString();
                    }
                    querySdr.Close();
                    if(id !="")
                    {
                        setChooseStock(id, house, place);
                    }
                   
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void vendorSnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.vendorTextBox.Text.Trim() == "")
                {
                    this.vendorTextBox.Focus();
                    MessageBox.Show("厂商SN的内容为空，请检查！");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from " + tableName + " where vendor_sn='" + this.vendorSnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();

                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        MessageBox.Show("此序列号的板子已经登记过了！");
                        this.add.Enabled = false;
                        mConn.Close();
                        return;
                    }
                    else
                    {
                        querySdr.Close();
                        this.add.Enabled = true;
                    }

                    cmd.CommandText = "select Id from fault_mb_confirm_table where vendor_sn='" + this.vendorSnTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        MessageBox.Show("此序列号的板子不在主板报废判定表中！");
                        this.add.Enabled = false;
                        mConn.Close();
                        return;
                    }
                    this.add.Enabled = true;
                    querySdr.Close();
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void RepairOperationForm_Load(object sender, EventArgs e)
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

        private void add_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (this.track_serial_noTextBox.Text.Trim() == "" || this.vendorSnTextBox.Text.Trim()=="")
            {
                MessageBox.Show("跟踪条码或厂商SN为空！");
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

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.track_serial_noTextBox.Text + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string exist = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (exist != "")
                    {
                        MessageBox.Show("此序列号已经存在库中了，请检查！");
                        conn.Close();
                        return;
                    }
                   
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.track_serial_noTextBox.Text.Trim() + "','"
                        + this.vendorSnTextBox.Text.Trim() + "','"
                        + this.vendorTextBox.Text.Trim() + "','"                      
                        + this.producttextBox.Text.Trim() + "','"
                        + this.mb_brieftextBox.Text.Trim() + "','"
                        + this.mpntextBox.Text.Trim() + "','"
                        + this.mb_brieftextBox.Text.Trim() + "','"

                        + this.statustextBox.Text.Trim() + "','"
                        + this.PCHbrieftextBox.Text.Trim() + "','"
                        + this.VGAbreiftextBox.Text.Trim() + "','"
                        + this.CPUbrieTtextBox.Text.Trim() + "','"

                        + this.statustextBox.Text.Trim() + "','"    
                        + this.inputertextBox.Text.Trim() + "','"
                        + this.input_datetextBox.Text.Trim() + "')";
                    
                    cmd.ExecuteNonQuery();
                    
                    //更新数量
                    cmd.CommandText = "select Id,number from store_house_ng where house='" + chooseStock.house + "' and place='" + chooseStock.place + "'";
                    querySdr = cmd.ExecuteReader();
                    exist = "";
                    string left_number = "";
                    while (querySdr.Read())
                    {
                        exist = querySdr[0].ToString();
                        left_number = querySdr[1].ToString();
                        break;
                    }
                    querySdr.Close();

                    if (left_number == null || left_number == "")
                    {
                        left_number = "0";
                    }
                    
                    try 
                    {
                        int totalLeft = Int32.Parse(left_number);
                        int thistotal = totalLeft +1;

                        cmd.CommandText = "update store_house_ng set number = '" + thistotal + "', mpn='" + this.mbdescribeTextBox.Text.Trim() + "'"
                                + " where house='" + chooseStock.house + "' and place='" + chooseStock.place + "'";
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    error = true;
                    MessageBox.Show("SaledService is not opened");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(ex.ToString());
            }

            if (error == false)
            {
                MessageBox.Show("添不良品库数据成功");

                this.track_serial_noTextBox.Text = "";
                this.vendorTextBox.Text = "";
                this.producttextBox.Text = "";
              
                this.mb_brieftextBox.Text = "";
                this.statustextBox.Text = "";
                this.PCHbrieftextBox.Text = "";
                this.mpntextBox.Text = "";               
             
                //this.repairertextBox.Text = "";
                this.input_datetextBox.Text = "";
                this.mbdescribeTextBox.Text = "";

                this.track_serial_noTextBox.Focus();
                query_Click(null, null);

                stockplacetextBox.Text = "";
                stockplacetextBox.Enabled = true;
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from "+tableName;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码","厂商SN", "厂商","客户别","MB简称","MPN","MB描述","状态","PCH描述","VGA描述","CPU描述",
                             "输入人", "录入日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        public void setChooseStock(string id, string house, string place)
        {
            chooseStock.Id = id;
            chooseStock.house = house;
            chooseStock.place = place;

            this.stockplacetextBox.Text = chooseStock.house + "," + chooseStock.place;
            this.stockplacetextBox.Enabled = false;
        }

        private void stockplacetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {   
                //打开选择界面，并把结果返回到本界面来
                ChooseStoreHouseForm csform = new ChooseStoreHouseForm(this, "store_house_ng_buffer_mb");
                csform.MdiParent = Program.parentForm;
                csform.Show();
            }
        }
    }
}
