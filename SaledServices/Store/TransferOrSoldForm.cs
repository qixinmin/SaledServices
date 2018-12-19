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
    public partial class TransferOrSoldForm : Form
    {
        private String tableName = "TransferOrSold_sheet";
        private SqlConnection mConn;
        private SqlDataAdapter sda;
        private DataSet ds;

        private ChooseStock chooseStock = new ChooseStock();

        public TransferOrSoldForm()
        {
            InitializeComponent();

            inputerTextBox.Text = LoginForm.currentUser;
            this.input_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            loadAdditionInfomation();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
            this.material_typecomboBox.SelectedIndex = 0;
            this.statuscomboBox.SelectedIndex = 0;
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

                cmd.CommandText = "select distinct vendor from receiveOrder";
                this.vendorComboBox.Items.Clear();
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

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            string bga_type = "";
            if (this.material_typecomboBox.Text.Trim() == "BGA")
            {
                if (this.CPU.Checked == false && this.PCH.Checked == false && this.VGA.Checked == false)
                {
                    MessageBox.Show("BGA的类型没有选择！");
                    return;
                }
                else
                {
                    if (PCH.Checked)
                    {
                        bga_type = "PCH";
                    }
                    else if (CPU.Checked)
                    {
                        bga_type = "CPU";
                    }
                    else if (this.VGA.Checked)
                    {
                        bga_type = "VGA";
                    }
                }

                if (this.otherbriefTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("在BGA下 简称不能为空！");
                    return;
                }
            }
            else if (this.material_typecomboBox.Text.Trim() == "MB")
            {
                if (this.mb_briefTextBox.Text.Trim() =="")
                {
                    MessageBox.Show("在MB下MB简称不能为空！");
                    return;
                }
            }

            if (this.mpnTextBox.Text.Trim() == "" || this.vendorComboBox.Text.Trim() =="")
            {
                MessageBox.Show("MPN 或订单号为空！");
                return;
            }

            try
            {
                int outnum = Int16.Parse(this.numberTextBox.Text.Trim());
                if (outnum == 0)
                {
                    MessageBox.Show("请输入大于0的数字！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数量框里面的内容有误！");
                return;
            }

            if(this.outreasontextBox.Text.Trim()=="" || this.receivertextBox.Text.Trim() == "")
            {
                MessageBox.Show("出货原因或收货商的内容为空！");
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
                        this.vendorComboBox.Text.Trim() + "','" +
                        this.material_typecomboBox.Text.Trim() + "','" +
                        bga_type + "','" +
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.mb_briefTextBox.Text.Trim() + "','" +
                        this.otherbriefTextBox.Text.Trim() + "','" +
                        this.describeTextBox.Text.Trim() + "','" +
                        this.statuscomboBox.Text.Trim() + "','" +
                        this.numberTextBox.Text.Trim() + "','" +
                        this.outreasontextBox.Text.Trim() + "','" +
                        this.receivertextBox.Text.Trim() + "','" +
                        this.notetextBox.Text.Trim() + "','" +                     
                        this.inputerTextBox.Text.Trim() + "','" +
                        this.input_dateTextBox.Text.Trim() + "')";

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                clearinput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearinput()
        {
            this.mpnTextBox.Text = "";
            this.mb_briefTextBox.Text = "";
            this.otherbriefTextBox.Text = "";
            this.describeTextBox.Text = "";
            this.statuscomboBox.Text = "";
            this.numberTextBox.Text = "";
            this.outreasontextBox.Text = "";
            this.receivertextBox.Text = "";
            this.notetextBox.Text = "";
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                string sqlStr =  "select top 20 * from " + tableName;

                if (mpnTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn= '" + this.mpnTextBox.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlStr += " and mpn= '" + mpnTextBox.Text.Trim() + "' ";
                    }
                }

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

            string[] hTxt = { "ID", "厂商", "材料类别", "BGA类别", "MPN", "MB简称", "简述", "描述", "状态", "数量", "出库原因", "收货商名称", "备注", "输入人", "日期" };
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

            string bga_type = "";
            if (PCH.Checked)
            {
                bga_type = "PCH";
            }
            else if (CPU.Checked)
            {
                bga_type = "CPU";
            }
            else if (this.VGA.Checked)
            {
                bga_type = "VGA";
            }

            dr["vendor"] = this.vendorComboBox.Text.Trim();
            dr["material_type"] = this.material_typecomboBox.Text.Trim();
            dr["bga_type"] = bga_type;
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["mb_brief"] = this.mb_briefTextBox.Text.Trim();
            dr["other_brief"] = this.otherbriefTextBox.Text.Trim();
            dr["describe"] = this.describeTextBox.Text.Trim();
            dr["_state"] = this.statuscomboBox.Text.Trim();
            dr["number"] = this.numberTextBox.Text.Trim();

            dr["out_reason"] = this.outreasontextBox.Text.Trim();
            dr["receiver"] = this.receivertextBox.Text.Trim();           
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
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorComboBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.material_typecomboBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            string bgatype = dataGridView1.SelectedCells[3].Value.ToString();
            switch(bgatype){
                case "CPU":
                    this.CPU.Checked = true;
                    break;
                case "PCH":
                    this.PCH.Checked = true;
                    break;
                case "VGA":
                    this.VGA.Checked = true;
                    break;
            }
            this.mpnTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.mb_briefTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.otherbriefTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.describeTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.statuscomboBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.numberTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.outreasontextBox.Text= dataGridView1.SelectedCells[10].Value.ToString();
            this.receivertextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.notetextBox.Text= dataGridView1.SelectedCells[12].Value.ToString();
            this.inputerTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
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
        
        private void CPU_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (this.material_typecomboBox.Text.Trim() == "" && rb.Checked)
            {
                MessageBox.Show("材料类别不能为空！");
                rb.Checked = false;
                this.mpnTextBox.Focus();
                return;
            }
            if (this.material_typecomboBox.Text.Trim() != "BGA")
            {
                return;
            }

            string type="";
            string mpnType = "";
            string describle = "";
            if(PCH.Checked)
            {
                type = "pcb_brief_describe";
                mpnType = "vendor_pch_mpn";
                describle = "pcb_describe";
            }
            else if (CPU.Checked )
            {
                type = "cpu_brief";
                mpnType = "vendor_cpu_mpn";
                describle = "cpu_describe";
            }
            else if (this.VGA.Checked)
            {
                type = "vga_brief_describe";
                mpnType = "vendor_vga_mpn";
                describle = "vga_describe";
            }
            else 
            {
                return;
            }
            
            //根据信息查询BGA简述
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select " + type + ","+describle+" from MBMaterialCompare where " + mpnType + "='" + this.mpnTextBox.Text.Trim() + "'";
              
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    this.otherbriefTextBox.Text = querySdr[0].ToString();
                    this.describeTextBox.Text = querySdr[1].ToString();
                    break;
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void material_typecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.material_typecomboBox.Text == "BGA")
            {
                panel1.Enabled = true;
                this.otherbriefTextBox.ReadOnly = false;
                this.describeTextBox.ReadOnly = false;
            }
            else
            {
                panel1.Enabled = false;
                this.otherbriefTextBox.ReadOnly = true;
                this.describeTextBox.ReadOnly = true;
            }
        }

        private void mpnTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.material_typecomboBox.Text.Trim() == "BGA")
                {
                    return;
                }
                else if (this.material_typecomboBox.Text.Trim() == "MB")
                {
                    try
                    {
                        SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                        mConn.Open();

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mConn;
                        cmd.CommandType = CommandType.Text;

                        //根据信息查询MB简述
                        cmd.CommandText = "select mb_brief,mb_descripe from MBMaterialCompare where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            this.mb_briefTextBox.Text = querySdr[0].ToString();
                            this.describeTextBox.Text = querySdr[1].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            List<TansferSoldStruct> receiveOrderList = new List<TansferSoldStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from TransferOrSold_sheet";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    TansferSoldStruct temp = new TansferSoldStruct();
                    temp.Id = querySdr[0].ToString();
                    temp.vendor = querySdr[1].ToString();
                    temp.material_type = querySdr[2].ToString();
                    temp.bga_type = querySdr[3].ToString();
                    temp.mpn = querySdr[4].ToString();
                    temp.mb_brief = querySdr[5].ToString();
                    temp.other_brief = querySdr[6].ToString();
                    temp.describe = querySdr[7].ToString();
                    temp._state = querySdr[8].ToString();
                    temp.number = querySdr[9].ToString();
                    temp.out_reason = querySdr[10].ToString();
                    temp.receiver = querySdr[11].ToString();
                    temp.note = querySdr[12].ToString();
                    temp.inputer = querySdr[13].ToString();
                    temp.input_date = querySdr[14].ToString();

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            generateExcelToCheck(receiveOrderList);        
        }

        public void generateExcelToCheck(List<TansferSoldStruct> StockCheckList)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("ID"); 
            titleList.Add("厂商");
            titleList.Add("材料类别MB/BGA/FRU/SMT");
            titleList.Add("bga类型");
            titleList.Add("MPN");
            titleList.Add("MB简称");
            titleList.Add("简述");
            titleList.Add("描述");
            titleList.Add("状态");
            titleList.Add("数量");
            titleList.Add("出库原因");
            titleList.Add("收货商名称");
            titleList.Add("备注");
            titleList.Add("操作人");
            titleList.Add("日期");

            foreach (TansferSoldStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.material_type);
                ct1.Add(stockcheck.bga_type);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.other_brief);
                ct1.Add(stockcheck.describe);
                ct1.Add(stockcheck._state);
                ct1.Add(stockcheck.number);
                ct1.Add(stockcheck.out_reason);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.note);
                ct1.Add(stockcheck.inputer);
                ct1.Add(stockcheck.input_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("报废转卖详细" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx", titleList, contentList);
        }
    }

    public class TansferSoldStruct
    {
        public string Id;
        public string vendor;

        public string material_type;
        public string bga_type;
        public string mpn;
        public string mb_brief;
        public string other_brief;
        public string describe;
        public string _state;
        public string number;
        public string out_reason;
        public string receiver;
        public string note;
        public string inputer;
        public string input_date;
    }
}
