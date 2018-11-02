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
    public partial class MBMaterialCompareForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;

        private SqlDataAdapter sda;

        private String tableName = "MBMaterialCompare";

        public MBMaterialCompareForm()
        {
            InitializeComponent();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());
            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["product"] = this.prouductTextBox.Text.Trim();

            dr["mb_brief"] = this.mbBriefTextBox.Text.Trim();
            dr["vendormaterialNo"] = this.vendormaterialNoTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["replace_mpn"] = this.replaceMpnTextBox.Text.Trim();
            dr["custommaterialNo"] = this.custommaterialNotextBox.Text.Trim();
            dr["replace_custom_materialNo"] = this.replace_custom_materialNoTextBox.Text.Trim();
            dr["fruNo"] = this.fruNoTextBox.Text.Trim();
            dr["replace_fruNo"] = this.replace_fruNoTextBox.Text.Trim();
            dr["mb_descripe"] = this.mb_descripeTextBox.Text.Trim();
            dr["vendor_pch_mpn"] = this.vendor_pch_mpnTextBox.Text.Trim();
            dr["pcb_brief_describe"] = this.pcb_brief_describeTextBox.Text.Trim();
            dr["pcb_describe"] = this.pcb_describeTextBox.Text.Trim();
            dr["vendor_vga_mpn"] = this.vendor_vga_mpnTextBox.Text.Trim();
            dr["vga_brief_describe"] = this.vga_brief_describeTextBox.Text.Trim();
            dr["vga_describe"] = this.vga_describeTextBox.Text.Trim();
            dr["vendor_cpu_mpn"] = this.vendor_cpu_mpnTextBox.Text.Trim();
            dr["cpu_brief"] = this.cpu_briefTextBox.Text.Trim();
            dr["cpu_describe"] = this.cpu_describeTextBox.Text.Trim();
            dr["dpk_type"] = this.dpk_typeTextBox.Text.Trim();
            dr["dpkpn"] = this.dpkpnTextBox.Text.Trim();
            dr["warranty_period"] = this.warranty_periodTextBox.Text.Trim();
            dr["custom_machine_type"] = this.custom_machine_typeTextBox.Text.Trim();
            dr["whole_machine_num"] = this.whole_machine_numTextBox.Text.Trim();
            dr["area"] = this.areaTextBox.Text.Trim();
            dr["_status"] = this.statusTextBox.Text.Trim();
            dr["cpu_type"] = this.cpu_typeTextBox.Text.Trim();
            dr["cpu_freq"] = this.cpu_freqTextBox.Text.Trim();
            dr["eco"] = this.ecoTextBox.Text.Trim();
            dr["eol"] = this.eolTextBox.Text.Trim();
            dr["adddate"] = this.addDateTextBox.Text.Trim();
            dr["inputuser"] = this.inputUserTextBox.Text.Trim();

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (this.custommaterialNotextBox.Text.Trim() == "")
            {
                MessageBox.Show("客户编号为空!");
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
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.vendorTextBox.Text.Trim() + "','"
                        + this.prouductTextBox.Text.Trim() + "','"
                        + this.mbBriefTextBox.Text.Trim() + "','"
                        + this.vendormaterialNoTextBox.Text.Trim() + "','"
                        + this.mpnTextBox.Text.Trim() + "','"
                        + this.replaceMpnTextBox.Text.Trim() + "','"
                        + this.custommaterialNotextBox.Text.Trim() + "','"
                        + this.replace_custom_materialNoTextBox.Text.Trim() + "','"
                        + this.fruNoTextBox.Text.Trim() + "','"
                        + this.replace_fruNoTextBox.Text.Trim() + "','"
                        + this.mb_descripeTextBox.Text.Trim() + "','"
                        + this.vendor_pch_mpnTextBox.Text.Trim() + "','"
                        + this.pcb_brief_describeTextBox.Text.Trim() + "','"
                        + this.pcb_describeTextBox.Text.Trim() + "','"
                        + this.vendor_vga_mpnTextBox.Text.Trim() + "','"
                        + this.vga_brief_describeTextBox.Text.Trim() + "','"
                        + this.vga_describeTextBox.Text.Trim() + "','"
                        + this.vendor_cpu_mpnTextBox.Text.Trim() + "','"
                        + this.cpu_briefTextBox.Text.Trim() + "','"
                        + this.cpu_describeTextBox.Text.Trim() + "','"
                        + this.dpk_typeTextBox.Text.Trim() + "','"
                        + this.dpkpnTextBox.Text.Trim() + "','"
                        + this.warranty_periodTextBox.Text.Trim() + "','"
                        + this.custom_machine_typeTextBox.Text.Trim() + "','"
                        + this.whole_machine_numTextBox.Text.Trim() + "','"
                        + this.areaTextBox.Text.Trim() + "','"
                        + this.statusTextBox.Text.Trim() + "','"
                        + this.cpu_typeTextBox.Text.Trim() + "','"
                        + this.cpu_freqTextBox.Text.Trim() + "','"
                        + this.ecoTextBox.Text.Trim() + "','"
                        + this.eolTextBox.Text.Trim() + "','"
                        + this.addDateTextBox.Text.Trim() + "','"
                        + this.inputUserTextBox.Text.Trim() + "')";

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

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlStr = "select top 1000 * from " + tableName;

                if (this.custommaterialNotextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where custommaterialNo like '%" + custommaterialNotextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and custommaterialNo like '%" + custommaterialNotextBox.Text.Trim() + "%' ";
                    }
                }

                if (this.mbBriefTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mb_brief like '%" + mbBriefTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mb_brief like '%" + mbBriefTextBox.Text.Trim() + "%' ";
                    }
                }

                if (this.mpnTextBox.Text.Trim() != "")
                {
                    if (!sqlStr.Contains("where"))
                    {
                        sqlStr += " where mpn like '%" + mpnTextBox.Text.Trim() + "%' ";
                    }
                    else
                    {
                        sqlStr += " and mpn like '%" + mpnTextBox.Text.Trim() + "%' ";
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

            string[] hTxt = {"ID", "厂商", "客户别","MB简称","厂商料号","MPN",
                             "可替换MPN","客户料号","可替换客户料号","FRU料号","可替换FRU料号","MB描述",
                             "厂商PCH_MPN","PCH简述","PCH描述","厂商VGA_MPN","VGA简述","VGA描述",
                             "厂商CPU_MPN","CPU简述","CPU描述","DPK类型","DPKPN", "保修期",
                             "客户机型","整机出货量","地区","状态","CPU型号","CPU频率", "ECO","EOL","添加日期","添加人"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            this.numTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.prouductTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.mbBriefTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.vendormaterialNoTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.replaceMpnTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.custommaterialNotextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.replace_custom_materialNoTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.fruNoTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.replace_fruNoTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.mb_descripeTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.vendor_pch_mpnTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.pcb_brief_describeTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.pcb_describeTextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.vendor_vga_mpnTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.vga_brief_describeTextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.vga_describeTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();
            this.vendor_cpu_mpnTextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();
            this.cpu_briefTextBox.Text = dataGridView1.SelectedCells[19].Value.ToString();
            this.cpu_describeTextBox.Text = dataGridView1.SelectedCells[20].Value.ToString();
            this.dpk_typeTextBox.Text = dataGridView1.SelectedCells[21].Value.ToString();
            this.dpkpnTextBox.Text = dataGridView1.SelectedCells[22].Value.ToString();
            this.warranty_periodTextBox.Text = dataGridView1.SelectedCells[23].Value.ToString();
            this.custom_machine_typeTextBox.Text = dataGridView1.SelectedCells[24].Value.ToString();
            this.whole_machine_numTextBox.Text = dataGridView1.SelectedCells[25].Value.ToString();
            this.areaTextBox.Text = dataGridView1.SelectedCells[26].Value.ToString();
            this.statusTextBox.Text = dataGridView1.SelectedCells[27].Value.ToString();
            this.cpu_typeTextBox.Text = dataGridView1.SelectedCells[28].Value.ToString();
            this.cpu_freqTextBox.Text = dataGridView1.SelectedCells[29].Value.ToString();

            this.ecoTextBox.Text = dataGridView1.SelectedCells[30].Value.ToString();
            this.eolTextBox.Text = dataGridView1.SelectedCells[31].Value.ToString();
            this.addDateTextBox.Text = dataGridView1.SelectedCells[32].Value.ToString();
            this.inputUserTextBox.Text = dataGridView1.SelectedCells[33].Value.ToString();
        }

        private void MBMaterialCompareForm_Load(object sender, EventArgs e)
        {
            //当TableLayoutPanel控件中的需要更新的Label过多的时候，刷新Label的时候会出现闪烁问题，主要解决办法就是增加双缓冲，代码如下

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.mpnTextBox.Text.Trim() == "" || this.statusTextBox.Text.Trim() == "")
            {
                MessageBox.Show("修改状态的字段为空");
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
                    cmd.CommandText = "update " + tableName + " set _status ='" + this.statusTextBox.Text.Trim() + "'where mpn='" + this.mpnTextBox.Text.Trim() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("修改成功！");
                query_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<MBMaterialStruct> receiveOrderList = new List<MBMaterialStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select vendor,product,mb_brief,vendormaterialNo,mpn,replace_mpn,custommaterialNo,replace_custom_materialNo,fruNo,replace_fruNo," +
                    "mb_descripe,vendor_pch_mpn,pcb_brief_describe,pcb_describe,vendor_vga_mpn,vga_brief_describe,vga_describe,vendor_cpu_mpn,cpu_brief," +
                    "cpu_describe,dpk_type,dpkpn,warranty_period,custom_machine_type,whole_machine_num,area,_status,cpu_type,cpu_freq,eco,eol,adddate,inputuser" +
                    " from MBMaterialCompare where adddate = '" + this.addDateTextBox.Text.Trim() + "' and custom_machine_type='" + this.custom_machine_typeTextBox.Text.Trim() + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MBMaterialStruct temp = new MBMaterialStruct();
                    temp.vendor = querySdr[0].ToString();
                    temp.product = querySdr[1].ToString();

                    temp.mb_brief = querySdr[2].ToString();
                    temp.vendormaterialNo = querySdr[3].ToString();
                    temp.mpn = querySdr[4].ToString();
                    temp.replace_mpn = querySdr[5].ToString();
                    temp.custommaterialNo = querySdr[6].ToString();
                    temp.replace_custom_materialNo = querySdr[7].ToString();
                    temp.fruNo = querySdr[8].ToString();
                    temp.replace_fruNo = querySdr[9].ToString();
                    temp.mb_descripe = querySdr[10].ToString();
                    temp.vendor_pch_mpn = querySdr[11].ToString();
                    temp.pcb_brief_describe = querySdr[12].ToString();
                    temp.pcb_describe = querySdr[13].ToString();
                    temp.vendor_vga_mpn = querySdr[14].ToString();
                    temp.vga_brief_describe = querySdr[15].ToString();
                    temp.vga_describe = querySdr[16].ToString();
                    temp.vendor_cpu_mpn = querySdr[17].ToString();
                    temp.cpu_brief = querySdr[18].ToString();
                    temp.cpu_describe = querySdr[19].ToString();
                    temp.dpk_type = querySdr[20].ToString();
                    temp.dpkpn = querySdr[21].ToString();
                    temp.warranty_period = querySdr[22].ToString();
                    temp.custom_machine_type = querySdr[23].ToString();
                    temp.whole_machine_num = querySdr[24].ToString();
                    temp.area = querySdr[25].ToString();
                    temp._status = querySdr[26].ToString();
                    temp.cpu_type = querySdr[27].ToString();
                    temp.cpu_freq = querySdr[28].ToString();
                    temp.eco = querySdr[29].ToString();
                    temp.eol = querySdr[30].ToString();
                    temp.adddate = querySdr[31].ToString();
                    temp.inputuser = querySdr[32].ToString();
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, this.addDateTextBox.Text.Trim(), this.custom_machine_typeTextBox.Text.Trim());
        }

        public void generateExcelToCheck(List<MBMaterialStruct> StockCheckList, string startTime, string machineType)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("MB简称");
            titleList.Add("厂商料号");
            titleList.Add("MPN");
            titleList.Add("可替换MPN");
            titleList.Add("客户料号");
            titleList.Add("可替换客户料号");
            titleList.Add("FRU料号");
            titleList.Add("可替换FRU料号");
            titleList.Add("MB描述"); ;
            titleList.Add("厂商PCH_MPN");
            titleList.Add("PCH简述");
            titleList.Add("PCH描述");
            titleList.Add("厂商VGA_MPN");
            titleList.Add("VGA简述");
            titleList.Add("VGA描述"); ;
            titleList.Add("厂商CPU_MPN");
            titleList.Add("CPU简述");
            titleList.Add("CPU描述");
            titleList.Add("DPK类型");
            titleList.Add("DPKPN");
            titleList.Add("保修期"); ;
            titleList.Add("客户机型");
            titleList.Add("整机出货量");
            titleList.Add("地区");
            titleList.Add("状态");
            titleList.Add("CPU型号");
            titleList.Add("CPU频率");
            titleList.Add("ECO");
            titleList.Add("EOL");
            titleList.Add("添加日期");
            titleList.Add("添加人");

            foreach (MBMaterialStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);

                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.vendormaterialNo);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.replace_mpn);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.replace_custom_materialNo);
                ct1.Add(stockcheck.fruNo);
                ct1.Add(stockcheck.replace_fruNo);
                ct1.Add(stockcheck.mb_descripe);
                ct1.Add(stockcheck.vendor_pch_mpn);
                ct1.Add(stockcheck.pcb_brief_describe);
                ct1.Add(stockcheck.pcb_describe);
                ct1.Add(stockcheck.vendor_vga_mpn);
                ct1.Add(stockcheck.vga_brief_describe);
                ct1.Add(stockcheck.vga_describe);
                ct1.Add(stockcheck.vendor_cpu_mpn);
                ct1.Add(stockcheck.cpu_brief);
                ct1.Add(stockcheck.cpu_describe);
                ct1.Add(stockcheck.dpk_type);
                ct1.Add(stockcheck.dpkpn);
                ct1.Add(stockcheck.warranty_period);
                ct1.Add(stockcheck.custom_machine_type);
                ct1.Add(stockcheck.whole_machine_num);
                ct1.Add(stockcheck.area);
                ct1.Add(stockcheck._status);
                ct1.Add(stockcheck.cpu_type);
                ct1.Add(stockcheck.cpu_freq);
                ct1.Add(stockcheck.eco);
                ct1.Add(stockcheck.eol);
                ct1.Add(stockcheck.adddate);
                ct1.Add(stockcheck.inputuser);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\MB物料对照表" +   machineType + ".xlsx", titleList, contentList);
        }
    }

    public class MBMaterialStruct
    {
        public string vendor;
        public string product;

        public string mb_brief;
        public string vendormaterialNo;
        public string mpn;
        public string replace_mpn;
        public string custommaterialNo;
        public string replace_custom_materialNo;
        public string fruNo;
        public string replace_fruNo;
        public string mb_descripe;
        public string vendor_pch_mpn;
        public string pcb_brief_describe;
        public string pcb_describe;
        public string vendor_vga_mpn;
        public string vga_brief_describe;
        public string vga_describe;
        public string vendor_cpu_mpn;
        public string cpu_brief;
        public string cpu_describe;
        public string dpk_type;
        public string dpkpn;
        public string warranty_period;
        public string custom_machine_type;
        public string whole_machine_num;
        public string area;
        public string _status;
        public string cpu_type;
        public string cpu_freq;
        public string eco;
        public string eol;
        public string adddate;
        public string inputuser;
    }
}
