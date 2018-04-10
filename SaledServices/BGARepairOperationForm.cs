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
    public partial class BGARepairOperationForm : Form
    {
        public BGARepairOperationForm()
        {
            InitializeComponent();

            loadAdditionInfomation();
        }

        private void loadAdditionInfomation()//TODO
        {
            //try
            //{
            //    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
            //    mConn.Open();

            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = mConn;
            //    cmd.CommandType = CommandType.Text;

            //    cmd.CommandText = "select distinct type from repairFaultType";
            //    SqlDataReader querySdr = cmd.ExecuteReader();
            //    while (querySdr.Read())
            //    {
            //        string temp = querySdr[0].ToString();
            //        if (temp != "")
            //        {
            //            this.vgaRepair_resultcomboBox.Items.Add(temp);
            //        }
            //    }
            //    querySdr.Close();

            //    mConn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
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
                    error = true;
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor, product,source,orderno,receivedate,mb_brief, custom_serial_no,vendor_serail_no,mpn,mb_make_date,customFault,fault_describe,mbfa1,short_cut,BGAPN,BGA_place,bga_brief,repairer,repair_date from repair_record_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();                  
                    while (querySdr.Read())
                    {
                        this.vendorTextBox.Text = querySdr[0].ToString();
                        this.producttextBox.Text = querySdr[1].ToString();
                        this.sourcetextBox.Text = querySdr[2].ToString();
                        this.ordernotextBox.Text = querySdr[3].ToString();
                        this.receivedatetextBox.Text = querySdr[4].ToString();
                        this.mb_brieftextBox.Text = querySdr[5].ToString();
                        this.custom_serial_notextBox.Text = querySdr[6].ToString();
                        this.vendor_serail_notextBox.Text = querySdr[7].ToString();
                        this.mpntextBox.Text = querySdr[8].ToString();
                        this.mb_make_dateTextBox.Text = querySdr[9].ToString();
                        this.customFaulttextBox.Text = querySdr[10].ToString();
                        this.fault_describetextBox.Text = querySdr[11].ToString();

                        this.mb_brieftextBox.Text = querySdr[12].ToString();
                        this.shortcutRichTextBox.Text = querySdr[13].ToString();
                        this.BGAPNtextBox.Text = querySdr[14].ToString();
                        this.BGA_placetextBox.Text = querySdr[15].ToString();
                        this.bga_brieftextBox.Text = querySdr[16].ToString();
                        this.repairertextBox.Text = querySdr[17].ToString();
                        this.repair_datetextBox.Text = querySdr[18].ToString();

                    }
                    querySdr.Close();

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());

                    this.track_serial_noTextBox.Focus();
                    this.track_serial_noTextBox.SelectAll();
                    MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                }

                if (!error)
                {
                    mbfa1richTextBox.Focus();
                    mbfa1richTextBox.SelectAll();
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
            if (this.track_serial_noTextBox.Text.Trim() == "" || this.bgaRepair_resultcomboBox.Text.Trim() =="")
            {
                MessageBox.Show("需要加入的内容为空，请检查！");
                return;
            }

            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string vendor_txt = this.vendorTextBox.Text.Trim();
            string product_txt = this.producttextBox.Text.Trim();
            string source_txt = this.sourcetextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string receivedate_txt = this.receivedatetextBox.Text.Trim();
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string vendor_serail_no_txt = this.vendor_serail_notextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string mb_make_date_txt = this.mb_make_dateTextBox.Text.Trim();
            string customFault_txt = this.customFaulttextBox.Text.Trim();
            string fault_describe_txt = this.fault_describetextBox.Text.Trim();
            string mbfa1rich_txt = this.mbfa1richTextBox.Text.Trim();
            string short_cut_txt = this.shortcutRichTextBox.Text.Trim();

            string BGAPN_txt = this.BGAPNtextBox.Text.Trim();
            string BGA_place_txt = this.BGA_placetextBox.Text.Trim();
            string bga_brief_txt = this.bga_brieftextBox.Text.Trim();

            string repairer_txt = this.repairertextBox.Text.Trim();
            string repair_date_txt = this.repairertextBox.Text.Trim();

            string bgarepairer_txt = this.bgarepairertextBox.Text.Trim();
            string bgaRepairDate_txt = DateTime.Now.ToString("yyyy/MM/dd");
            string bgaRepairResult_txt = this.bgaRepair_resultcomboBox.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO bga_repair_record_table VALUES('"
                        + track_serial_no_txt + "','"
                        + vendor_txt + "','"
                        + product_txt + "','"
                        + source_txt + "','"
                        + orderno_txt + "','"
                        + receivedate_txt + "','"
                        + mb_brief_txt + "','"
                        + custom_serial_no_txt + "','"
                        + vendor_serail_no_txt + "','"
                        + mpn_txt + "','"
                        + mb_make_date_txt + "','"
                        + customFault_txt + "','"
                        + fault_describe_txt + "','"
                        + mbfa1rich_txt + "','"
                        + short_cut_txt + "','"
                        + BGAPN_txt + "','"
                        + BGA_place_txt + "','"
                        + bga_brief_txt + "','"
                        + repairer_txt + "','"
                        + repair_date_txt + "','"
                        + bgarepairer_txt + "','"
                        + bgaRepairDate_txt + "','"
                        + bgaRepairResult_txt + "')";

                    cmd.CommandType = CommandType.Text;
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
                MessageBox.Show("添加维修数据成功");
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            if (this.track_serial_noTextBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码为空");
                return;
            }
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from bga_repair_record_table";
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "vga_repair_record_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码", "厂商","客户别","来源","订单编号",
                             "收货日期","MB简称","客户序号","厂商序号","MPN",
                             "MB生产日期","客户故障","故障原因","mbfa1","短路电压",
                             "BGAPN","BGA位置","BGA简述","维修人", "修复日期","bga更换人","BGA更换日期","bga状态"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }
    }
}
