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
    public partial class FaultMBConfirmForm : Form
    {
        private String tableName = "fault_mb_confirm_table";
        public FaultMBConfirmForm()
        {
            InitializeComponent();
           
            confirmertextBox.Text = LoginForm.currentUser;
            this.confrim_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
            track_serial_noTextBox.Focus();
        }       

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                bool error = false;
                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    this.track_serial_noTextBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    this.add.Enabled = false;
                    error = true;
                    return;
                }

                this.track_serial_noTextBox.Text = this.track_serial_noTextBox.Text.ToUpper();//防止输入小写字符
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        mConn.Close();
                        this.add.Enabled = false;
                        MessageBox.Show("此跟踪条码的已经记录过了，不能走下面的流程！");
                        return;
                    }
                    querySdr.Close();
                    this.add.Enabled = true;

                    cmd.CommandText = "select custommaterialNo from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (customMaterialNo != "")
                    {
                        string vendor = "", product = "", mb_describe = "", mb_brief = "", mpn = "";
                        cmd.CommandText = "select vendor,product, mb_descripe, mb_brief,mpn from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                        querySdr = cmd.ExecuteReader();

                        while (querySdr.Read())
                        {
                            vendor = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                            mb_describe = querySdr[2].ToString();
                            mb_brief = querySdr[3].ToString();
                            mpn = querySdr[4].ToString();
                        }
                        querySdr.Close();

                        this.vendorTextBox.Text = vendor;
                        this.producttextBox.Text = product;                   
                        this.mb_describetextBox.Text = mb_describe;
                        this.mb_brieftextBox.Text = mb_brief;                       
                        this.mpntextBox.Text = mpn;

                        if (Utils.isTimeError(this.confrim_datetextBox.Text.Trim()))
                        {
                            this.add.Enabled = false;
                        }
                    }
                    else
                    {  
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        error = true;
                        mConn.Close();
                        this.add.Enabled = false;
                        return;
                    }

                    //cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    //querySdr = cmd.ExecuteReader();
                    //string stationInfo = "";
                    //while (querySdr.Read())
                    //{
                    //    stationInfo = querySdr[0].ToString();
                    //}
                    //querySdr.Close();

                    //if (stationInfo == "维修" || stationInfo == "收货")
                    //{
                    //    this.add.Enabled = true;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("此序列号的站别已经在:" + stationInfo + "，不能走下面的流程！");
                    //    mConn.Close();
                    //    this.add.Enabled = false;
                    //    return;
                    //}

                    mConn.Close();

                    this.add.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.confrim_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
                if (!error)
                {
                    this.vendorSnTextBox.Focus();
                    this.vendorSnTextBox.SelectAll();
                }
            }
        }

        private void fault_describetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.faultPlaceTextBox.SelectAll();
                this.faultPlaceTextBox.Focus();
            }
        }

        private void faultPlaceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.faultReasonRichTextBox.SelectAll();
                this.faultReasonRichTextBox.Focus();
            }
        }

        private void vendorSnTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

                    cmd.CommandText = "select Id from " + tableName + " where vendor_sn='" + this.vendorSnTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        mConn.Close();
                        this.add.Enabled = false;
                        MessageBox.Show("此厂商SN已经记录过了，不能走下面的流程！");
                        return;
                    }
                    this.add.Enabled = true;

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.fault_describetextBox.SelectAll();
                this.fault_describetextBox.Focus();
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
            if (this.track_serial_noTextBox.Text.Trim() == "" || this.vendorTextBox.Text.Trim() == "")
            {
                MessageBox.Show("跟踪条码与厂商不能为空！");
                return;
            }

            if (this.mb_brieftextBox.Text == "" || this.vendorTextBox.Text == "")
            {
                MessageBox.Show("输入完跟踪条码需要回车！");
                return;
            }

            bool error = false;
          
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
                       this.track_serial_noTextBox.Text.Trim() + "','" +
                       this.vendorSnTextBox.Text.Trim() + "','" +
                       this.vendorTextBox.Text.Trim() + "','" +
                       this.producttextBox.Text.Trim() + "','" +
                       this.mb_brieftextBox.Text.Trim() + "','" +
                       this.mpntextBox.Text.Trim() + "','" +
                       this.mb_describetextBox.Text.Trim() + "','" +
                       this.fault_describetextBox.Text.Trim() + "','" +
                       this.faultPlaceTextBox.Text.Trim() + "','" +
                       this.faultReasonRichTextBox.Text.Trim() + "','" +

                       this.confirmertextBox.Text.Trim() + "','" +
                       this.confrim_datetextBox.Text.Trim() + "','" +
                       this.pcbzhouqitextBox.Text.Trim() + "','" +
                       this.pcbtypetextBox.Text.Trim() + "')";

                    cmd.ExecuteNonQuery();

                    //更新维修站别
                    cmd.CommandText = "update stationInformation set station = '报废', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                               + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    cmd.ExecuteNonQuery();
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
                MessageBox.Show("添加报废判定成功");

                this.track_serial_noTextBox.Text = "";
                this.vendorSnTextBox.Text = "";
                this.vendorTextBox.Text = "";
                this.producttextBox.Text = "";
                
                this.mb_describetextBox.Text = "";
                this.mb_brieftextBox.Text = "";              
                this.mpntextBox.Text = "";               
                this.fault_describetextBox.Text = "";
                this.faultPlaceTextBox.Text = "";
                this.faultReasonRichTextBox.Text = "";
               
                this.confrim_datetextBox.Text = "";

                this.track_serial_noTextBox.Focus();
                query_Click(null, null);
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                
                cmd.CommandType = CommandType.Text;

                string sqlStr = "select * from fault_mb_confirm_table";

                if (track_serial_noTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where track_serial_no like '%" + track_serial_noTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and track_serial_no like '%" + track_serial_noTextBox.Text.Trim() + "%'";
                    }
                }

                cmd.CommandText = sqlStr;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "repair_record_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码", "厂商SN","厂商","客户别","MB简称","MPN","MB描述","不良现象","不良位置", "报废原因",
                             "判定人", "判定日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

       

       
    }
}
