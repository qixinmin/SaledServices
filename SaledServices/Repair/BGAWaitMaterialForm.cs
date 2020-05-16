using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class BGAWaitMaterialForm : Form
    {
        public BGAWaitMaterialForm()
        {
            InitializeComponent();
            this.repairertextBox.Text = LoginForm.currentUser;
            repair_datetextBox.Text  = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

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
                this.track_serial_noTextBox.Text = this.track_serial_noTextBox.Text.ToUpper();
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from cidRecord where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string cidExist = "";
                    while (querySdr.Read())
                    {
                        cidExist = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (cidExist != "")
                    {
                        MessageBox.Show("此序列号已经在CID中，不能走下面的流程！");
                        this.add.Enabled = false;
                        mConn.Close();
                        return;
                    }

                    cmd.CommandText = "select custommaterialNo, source_brief,custom_order,order_receive_date,custom_serial_no,vendor_serail_no, mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    string customMaterialNo = "";
                    string sourceBrief = "", customOrder = "", order_receive_date = "", custom_serial_no = "", vendor_serial_no = "", mb_make_date = "", custom_fault = "";
                    while (querySdr.Read())
                    {
                        customMaterialNo = querySdr[0].ToString();
                        sourceBrief = querySdr[1].ToString();
                        customOrder = querySdr[2].ToString();
                        order_receive_date = querySdr[3].ToString();
                        custom_serial_no = querySdr[4].ToString();
                        vendor_serial_no = querySdr[5].ToString();
                        mb_make_date = DateTime.Parse(querySdr[6].ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        custom_fault = querySdr[7].ToString();
                    }
                    querySdr.Close();

                    if (customMaterialNo == "")//从转换表里面取数据
                    {
                        cmd.CommandText = "select custommaterialNo, source_brief,custom_order,order_receive_date,custom_serial_no,vendor_serail_no, mb_make_date,custom_fault from DeliveredTableTransfer where track_serial_no_transfer='" + this.track_serial_noTextBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();                     
                        while (querySdr.Read())
                        {
                            customMaterialNo = querySdr[0].ToString();
                            sourceBrief = querySdr[1].ToString();
                            customOrder = querySdr[2].ToString();
                            order_receive_date = querySdr[3].ToString();
                            custom_serial_no = querySdr[4].ToString();
                            vendor_serial_no = querySdr[5].ToString();
                            mb_make_date = DateTime.Parse(querySdr[6].ToString()).ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            custom_fault = querySdr[7].ToString();
                        }
                        querySdr.Close();
                    }

                    if (customMaterialNo != "")
                    {
                        string vendor = "", product = "", mb_describe = "", mb_brief = "", mpn = "", eco = "";
                        cmd.CommandText = "select vendor,product, mb_descripe, mb_brief,mpn,eol from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                        querySdr = cmd.ExecuteReader();

                        while (querySdr.Read())
                        {
                            vendor = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                            mb_describe = querySdr[2].ToString();
                            mb_brief = querySdr[3].ToString();
                            mpn = querySdr[4].ToString();
                            eco = querySdr[5].ToString();
                        }
                        querySdr.Close();

                        this.customMaterialNoTextBox.Text = customMaterialNo;
                        this.vendorTextBox.Text = vendor;
                        this.producttextBox.Text = product;
                        this.sourcetextBox.Text = sourceBrief;
                        this.ordernotextBox.Text = customOrder;
                        this.receivedatetextBox.Text = order_receive_date;
                        this.mb_describetextBox.Text = mb_describe;
                        this.mb_brieftextBox.Text = mb_brief;
                        this.custom_serial_notextBox.Text = custom_serial_no;
                        this.vendor_serail_notextBox.Text = vendor_serial_no;
                        this.mpntextBox.Text = mpn;
                        this.mb_make_dateTextBox.Text = mb_make_date;
                        this.customFaulttextBox.Text = custom_fault;
                        this.ECOtextBox.Text = eco;                    
         
                        this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

                        if (Utils.isTimeError(this.repair_datetextBox.Text.Trim()))
                        {
                            this.add.Enabled = false;
                        }
                    }
                    else
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                        this.add.Enabled = false;
                        error = true;
                        mConn.Close();
                        return;
                    }
                   
                    this.add.Enabled = true;
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.repair_datetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);              
            }
        }

        private void CPU_CheckedChanged(object sender, EventArgs e)
        {
            if (this.VGA.Checked == false && this.CPU.Checked == false && this.PCH.Checked == false)
            {
                return;
            }

            this.BGAmaterialdesTextBox.Text = "";
            string bga_mpn = "";
            string bga_brief = "";
            string bga_describe = "";
            if (this.mpntextBox.Text.Trim() == "")
            {
                MessageBox.Show("请输入追踪条码！");
                this.VGA.Checked = false;
                this.CPU.Checked = false;
                this.PCH.Checked = false;
                return;
            }

            if (this.VGA.Checked)
            {
                bga_mpn = "vendor_vga_mpn";
                bga_brief = "vga_brief_describe";
                bga_describe = "vga_describe";
            }
            else if (this.CPU.Checked)
            {
                bga_mpn = "vendor_cpu_mpn";
                bga_brief = "cpu_brief";
                bga_describe = "cpu_describe";
            }
            else if (this.PCH.Checked)
            {
                bga_mpn = "vendor_pch_mpn";
                bga_brief = "pcb_brief_describe";
                bga_describe = "pcb_describe";
            }
            else
            {
                return;
            }

            string tableName  = Constlist.table_name_LCFC_MBBOM;

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select " + bga_mpn + "," + bga_brief + "," + bga_describe + " from MBMaterialCompare where mpn='" + this.mpntextBox.Text.Trim() + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                string bga_mpn_txt = "", bga_brief_txt = "",bga_describe_txt="";
                while (querySdr.Read())
                {
                    bga_mpn_txt = querySdr[0].ToString();
                    bga_brief_txt = querySdr[1].ToString();
                    bga_describe_txt = querySdr[2].ToString();
                }
                querySdr.Close();

                this.BGAPNtextBox.Text = bga_mpn_txt;
                this.bga_brieftextBox.Text = bga_brief_txt;
                this.BGAmaterialdesTextBox.Text = bga_describe_txt;

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.track_serial_noTextBox.Text == ""
                || this.mpntextBox.Text == ""
                || this.BGAPNtextBox.Text == ""
                || this.bga_brieftextBox.Text == ""
                || this.BGAmaterialdesTextBox.Text == ""
                || this.repair_datetextBox.Text == "")
            {
                MessageBox.Show("输入的内容有空，请检查！");
                return;
            }
            if (this.VGA.Checked == false && this.CPU.Checked == false && this.PCH.Checked == false)
            {
                MessageBox.Show("VGA,CPU, PCH 必须选择一个！");
                return;
            }

            if (this.BGAmaterialdesTextBox.Text == "")
            {
                MessageBox.Show("BGA位置必须输入信息!");
                return;
            }

              string bgaType = "";
            if (this.CPU.Checked)
            {
                bgaType = "CPU";
            }
            else if (this.VGA.Checked)
            {
                bgaType = "VGA";
            }
            else if (this.PCH.Checked)
            {
                bgaType = "PCH";
            }

            //检测板子的同类型是否已经在数据库中了
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id from bga_wait_material_record_table where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and bgatype='" + bgaType + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                string exist = "";
                while (querySdr.Read())
                {
                    exist = querySdr[0].ToString();
                }
                querySdr.Close();

                if (exist != "")
                {
                    MessageBox.Show("此序列号和类型已经在记录中了，重复录入，不能走下面的流程！");
                    mConn.Close();
                    return;
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            bool error = false;
            //1.包含NTF的逻辑， 所有输入的有效信息均为NTF， 2. 若第一次输入信息没有输入完毕，需提醒并把某些字段清空即可
            string track_serial_no_txt = this.track_serial_noTextBox.Text.Trim();
            string customMaterialNo = this.customMaterialNoTextBox.Text.Trim();

          

            string vendor_txt = this.vendorTextBox.Text.Trim();
            string product_txt = this.producttextBox.Text.Trim();
            string source_txt = this.sourcetextBox.Text.Trim();
            string orderno_txt = this.ordernotextBox.Text.Trim();
            string receivedate_txt = this.receivedatetextBox.Text.Trim();
            string mb_describe_txt = this.mb_describetextBox.Text.Trim();
            string mb_brief_txt = this.mb_brieftextBox.Text.Trim();
            string custom_serial_no_txt = this.custom_serial_notextBox.Text.Trim();
            string vendor_serail_no_txt = this.vendor_serail_notextBox.Text.Trim();
            string mpn_txt = this.mpntextBox.Text.Trim();
            string mb_make_date_txt = this.mb_make_dateTextBox.Text.Trim();
            string customFault_txt = this.customFaulttextBox.Text.Trim();
       
            string ECO_txt = this.ECOtextBox.Text.Trim();          
            string BGAPN_txt = this.BGAPNtextBox.Text.Trim();
            string BGA_describe_txt = this.BGAmaterialdesTextBox.Text.Trim();
            string bga_brief_txt = this.bga_brieftextBox.Text.Trim();
           
            string repairer_txt = this.repairertextBox.Text.Trim();
            string repair_date_txt = this.repair_datetextBox.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO bga_wait_material_record_table VALUES('"
                        + track_serial_no_txt.ToUpper() + "','"
                        + customMaterialNo + "','"
                        + vendor_txt + "','"
                        + product_txt + "','"
                        + source_txt + "','"
                        + orderno_txt + "','"
                        + receivedate_txt + "','"
                        + mb_describe_txt + "','"
                        + mb_brief_txt + "','"
                        + custom_serial_no_txt + "','"
                        + vendor_serail_no_txt + "','"
                        + mpn_txt + "','"
                        + mb_make_date_txt + "','"
                        + customFault_txt + "','"
                        + ECO_txt + "','"
                        + bgaType + "','"
                        + BGAPN_txt + "','"
                        + BGA_describe_txt + "','"
                        + bga_brief_txt + "','"
                        + repairer_txt + "','"
                        + repair_date_txt + "','"
                        +""//新家状态字段
                        + "')";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
          "','BGA待料','" + repairer_txt + "',GETDATE())";
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

                this.track_serial_noTextBox.Text = "";
                this.customMaterialNoTextBox.Text = "";
                
                this.mpntextBox.Text = "";                
                
                this.BGAPNtextBox.Text = "";
                this.BGAmaterialdesTextBox.Text = "";
                this.bga_brieftextBox.Text = "";

                this.vendorTextBox.Text = "";
                this.producttextBox.Text = "";
                this.sourcetextBox.Text = "";
                this.ordernotextBox.Text = "";
                this.receivedatetextBox.Text = "";
                this.mb_describetextBox.Text = "";
                this.mb_brieftextBox.Text = "";
                this.custom_serial_notextBox.Text = "";
                this.vendor_serail_notextBox.Text = "";
                this.mpntextBox.Text = "";
                this.mb_make_dateTextBox.Text = "";
                this.customFaulttextBox.Text = "";

                this.ECOtextBox.Text = "";
                this.BGAPNtextBox.Text = "";
                this.BGAmaterialdesTextBox.Text = "";
                this.bga_brieftextBox.Text = "";
                this.repair_datetextBox.Text = "";
                this.repair_datetextBox.Text = "";

                this.VGA.Checked = false;
                this.CPU.Checked = false;
                this.PCH.Checked = false;

                query_Click(null, null);
            }

            this.track_serial_noTextBox.Focus();
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;              

                string sqlStr = "select top 3 * from bga_wait_material_record_table";

                if (this.track_serial_noTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where track_serial_no= '" + track_serial_noTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and track_serial_no= '" + track_serial_noTextBox.Text.Trim() + "' ";
                    }
                }

                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "bga_wait_material_record_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = {"ID", "跟踪条码", "客户料号","厂商","客户别","来源","订单编号",
                             "收货日期","MB描述","MB简称","客户序号","厂商序号","MPN",
                             "MB生产日期","客户故障","EOL", "BGA类型", "BGAPN","BGA描述","BGA简述","录入人", "录入日期","状态"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
            MessageBox.Show("查询完毕");
        }

        private void BGAInfoInputForm_Load(object sender, EventArgs e)
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
    }
}
