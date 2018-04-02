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
    public partial class DeliveredTableForm : Form
    {

        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "DeliveredTable";

        public DeliveredTableForm()
        {
            InitializeComponent();

            loadAdditionInfomation();
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

                //1 来源 2.客户故障	3.保内/保外	4 .客责描述
                cmd.CommandText = "select distinct source from sourceTable";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.source_briefComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct fault_describe from customFault";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_faultComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct guarantee_describe from guarantee";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.guaranteeComboBox.Items.Add(temp);
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
                        this.customResponsibilityComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                //加载没有收完货的订单
                cmd.CommandText = "select distinct orderno from receiveOrder where status = 'open'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_orderComboBox.Items.Add(temp);
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

        private void custom_orderComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (custom_orderComboBox.Text == "" || custommaterialNoTextBox.Text == "")
                {
                    MessageBox.Show("无效订单编号");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor, product, storehouse from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                        + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.vendorTextBox.Text = querySdr[0].ToString();
                        this.productTextBox.Text = querySdr[1].ToString();
                        this.storehouseTextBox.Text = querySdr[2].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select custom_machine_type,mb_brief,dpk_type,vendormaterialNo,mb_descripe,warranty_period from MBMaterialCompare where custommaterialNo ='" 
                        + this.custommaterialNoTextBox.Text + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.custom_machine_typeTextBox.Text = querySdr[0].ToString();
                        this.mb_briefTextBox.Text = querySdr[1].ToString();
                        this.dpk_statusTextBox.Text = querySdr[2].ToString();
                        this.vendormaterialNoTextBox.Text = querySdr[3].ToString();
                        this.mb_describeTextBox.Text = querySdr[4].ToString();
                        this.warranty_periodTextBox.Text = querySdr[5].ToString();
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

        private void custom_orderComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = this.custom_orderComboBox.Text;
            string substr = str.Substring(str.Length-8, 6);
            string inTime = "20"+substr.Substring(0, 2) +"/"+ substr.Substring(2, 2) +"/"+ substr.Substring(4, 2);
            this.order_out_dateTextBox.Text = inTime;

            DateTime dt1 = DateTime.Parse(inTime);
            DateTime dt2 = DateTime.Now;

            TimeSpan ts = dt2.Subtract(dt1);
            if (ts.TotalDays < 0)
            {
                MessageBox.Show("请检测当前机器的时间是否正确！");
                return;
            }
            
            this.order_receive_dateTextBox.Text = dt2.ToString("yyyy/MM/dd");            
        }

        private void custom_serial_noTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.productTextBox.Text != "TBG")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
            {
                //需要去掉前面的非0字段
                string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');

                if (this.custom_serial_noTextBox.Text.Contains(customSerial) == false)
                {
                    MessageBox.Show("在" + this.productTextBox.Text + "下客户序号没有包含客户料号");
                }
            }

            string customSerialNo= this.custom_serial_noTextBox.Text;
            string subData = "";
            if (customSerialNo.StartsWith("8S"))
            {
                subData = customSerialNo.Substring(customSerialNo.Length - 7, 3);                
            }
            else if (customSerialNo.StartsWith("11S"))
            {
                subData = customSerialNo.Substring(customSerialNo.Length - 6, 3);
            }
            else
            {
                MessageBox.Show("客户序号没有包含,没有做计算时间处理");
                return;
            }

            string year = Untils.getTimeByChar(true, Convert.ToChar(subData.Substring(0, 1)));
            string mouth = Untils.getTimeByChar(false, Convert.ToChar(subData.Substring(1, 1)));
            string day = Untils.getTimeByChar(false, Convert.ToChar(subData.Substring(2, 1)));
            this.mb_make_dateTextBox.Text = year + "/" + mouth + "/" + day;

            DateTime dt1 = Convert.ToDateTime(this.mb_make_dateTextBox.Text);
            DateTime dt2 = Convert.ToDateTime(this.order_receive_dateTextBox.Text);
            
            string period = this.warranty_periodTextBox.Text;
            if (period != "")
            {
                int warranty = Int32.Parse(period.Substring(0, period.Length - 1));

                dt1 = dt1.AddMonths(warranty);//生产日期加上保修期
                TimeSpan ts = dt2.Subtract(dt1);

                int overdays = ts.Days;

                if (overdays >= 0)
                {
                    this.guaranteeComboBox.Text = "过保";
                    this.guaranteeComboBox.Enabled = false;
                    this.customResponsibilityComboBox.Text = "过保";
                    this.customResponsibilityComboBox.Enabled = false;
                    MessageBox.Show((overdays) + " fail");
                }
                else
                {
                    this.guaranteeComboBox.Text = "";
                    this.guaranteeComboBox.Enabled = true;
                    this.customResponsibilityComboBox.Text = "";
                    this.customResponsibilityComboBox.Enabled = true;
                }
            }
        }

       
//         * Id INT PRIMARY KEY IDENTITY, 
//vendor NVARCHAR(128) NOT NULL, /*厂商*/
//product NVARCHAR(128) NOT NULL, /*客户别*/
//source_brief NVARCHAR(128) NOT NULL,/*来源*/
//storehouse NVARCHAR(128) NOT NULL,/*库别*/
//custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
//order_out_date NVARCHAR(128) NOT NULL,/*客户出库日期*/
//order_receive_date NVARCHAR(128),/*收货日期*/
//custom_machine_type NVARCHAR(128),/*客户机型*/
//mb_brief NVARCHAR(128) NOT NULL,/*mb简称*/
//custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
//dpk_status NVARCHAR(128) NOT NULL,/*DPK状态*/
//track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
//custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
//vendor_serail_no NVARCHAR(128) NOT NULL,/*厂商序号*/
//uuid NVARCHAR(128) NOT NULL,/*UUID*/
//mac NVARCHAR(128) NOT NULL,/*MAC*/
//vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
//mb_describe NVARCHAR(128) NOT NULL,/*mb描述*/
//mb_make_date NVARCHAR(128) NOT NULL,/*MB生产日期*/
//warranty_period NVARCHAR(128) NOT NULL,/*保修期*/
//custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
//guarantee NVARCHAR(128) NOT NULL,/*保内/保外*/
//customResponsibility NVARCHAR(128) NOT NULL,/*客责描述*/
//lenovo_custom_service_no NVARCHAR(128),/*联想客服序号*/
//lenovo_maintenance_no NVARCHAR(128),/*联想维修站编号*/
//lenovo_repair_no NVARCHAR(128),/*联想维修单编号*/
//whole_machine_no NVARCHAR(128)/*整机序号*/
         
        private void add_Click(object sender, EventArgs e)
        {            
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
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.source_briefComboBox.Text.Trim() + "','" +
                        this.storehouseTextBox.Text.Trim() + "','" +
                        this.custom_orderComboBox.Text.Trim() + "','" +
                        this.order_out_dateTextBox.Text.Trim() + "','" +
                        this.order_receive_dateTextBox.Text.Trim() + "','" +
                        this.custom_machine_typeTextBox.Text.Trim() + "','" +
                        this.mb_briefTextBox.Text.Trim() + "','" +
                        this.custommaterialNoTextBox.Text.Trim() + "','" +
                        this.dpk_statusTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "','" +
                        this.vendor_serail_noTextBox.Text.Trim() + "','" +
                        this.uuidTextBox.Text.Trim() + "','" +
                        this.macTextBox.Text.Trim() + "','" +
                        this.vendormaterialNoTextBox.Text.Trim() + "','" +
                        this.mb_describeTextBox.Text.Trim() + "','" +
                        this.mb_make_dateTextBox.Text.Trim() + "','" +
                        this.warranty_periodTextBox.Text.Trim() + "','" +
                        this.custom_faultComboBox.Text.Trim() + "','" +
                        this.guaranteeComboBox.Text.Trim() + "','" +
                        this.customResponsibilityComboBox.Text.Trim() + "','" +
                        this.lenovo_custom_service_noTextBox.Text.Trim() + "','" +
                        this.lenovo_maintenance_noTextBox.Text.Trim() + "','" +
                        this.lenovo_repair_noTextBox.Text.Trim() + "','" +
                        this.whole_machine_noTextBox.Text.Trim() + 
                        "')";
                   
                    cmd.ExecuteNonQuery();

                    //除正常插入数据外，还需要把收还货表格的数量修改 TODO...
                    //1. 修改收还货表格的收货数量， 判断，小于 等于，大于的情况
                    //2 如果小于 只是修改数据
                    //3 如果等于 则需要把状态也修改位close， 如果大于则直接报错
                    //update receiveOrder set returnNum = '1' where id = '1'

                    cmd.CommandText = "select status, ordernum, receivedNum, receivedate from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                         + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    int orderNum;
                    int receivedNum=0;
                    string status = "open";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        if (querySdr[0].ToString() == "close")
                        {
                            MessageBox.Show("本板子子已经收货完毕，请检测是否有错误!");
                        }
                        else
                        {
                            orderNum = Int32.Parse(querySdr[1].ToString());
                            receivedNum = Int32.Parse(querySdr[2].ToString()); 
                            if (orderNum == receivedNum + 1)
                            {
                                status = "close";
                            }
                        }
                    }
                    querySdr.Close();

                    cmd.CommandText = "update receiveOrder set status = '" + status + "',receivedNum = '" + (receivedNum + 1) +
                                "', receivedate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                                + "where orderno = '" + this.custom_orderComboBox.Text
                                + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
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
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  " + tableName;
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


            string[] hTxt = { "ID", "厂商", "客户别","来源"
            ,"库别","订单编号","客户出库日期","收货日期","客户机型","mb简称","客户料号","DPK状态","跟踪条码",
            "客户序号","厂商序号","UUID","MAC","厂商料号","mb描述","MB生产日期",",保修期",",客户故障","保内/保外"
            ,"客责描述","联想客服序号","联想维修站编号","联想维修单编号","整机序号"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());

            dr["vendor"] =this.vendorTextBox.Text.Trim();
            dr["product"] =this.productTextBox.Text.Trim() ;
            dr["source_brief"] =this.source_briefComboBox.Text.Trim() ;
            dr["storehouse"] =this.storehouseTextBox.Text.Trim() ;
            dr["custom_order"] =this.custom_orderComboBox.Text.Trim() ;
            dr["order_out_date"] =this.order_out_dateTextBox.Text.Trim() ;
            dr["order_receive_date"] =this.order_receive_dateTextBox.Text.Trim();
            dr["custom_machine_type "] =this.custom_machine_typeTextBox.Text.Trim() ;
            dr["mb_brief"] =this.mb_briefTextBox.Text.Trim() ;
            dr["custommaterialNo"] =this.custommaterialNoTextBox.Text.Trim() ;
            dr["dpk_status"] =this.dpk_statusTextBox.Text.Trim() ;
            dr["track_serial_no"] =this.track_serial_noTextBox.Text.Trim() ;
            dr["custom_serial_no"] =this.custom_serial_noTextBox.Text.Trim() ;
            dr["vendor_serail_no"] =this.vendor_serail_noTextBox.Text.Trim() ;
            dr["uuid"] =this.uuidTextBox.Text.Trim() ;
            dr["mac"] =this.macTextBox.Text.Trim() ;
            dr["vendormaterialNo"] =this.vendormaterialNoTextBox.Text.Trim() ;
            dr["mb_describe"] =this.mb_describeTextBox.Text.Trim() ;
            dr["mb_make_date"] = this.mb_make_dateTextBox.Text.Trim() ;
            dr["warranty_period"] =this.warranty_periodTextBox.Text.Trim() ;
            dr["custom_fault"] =this.custom_faultComboBox.Text.Trim() ;
            dr["guarantee"] =this.guaranteeComboBox.Text.Trim() ;
            dr["customResponsibility"] =this.customResponsibilityComboBox.Text.Trim() ;
            dr["lenovo_custom_service_no"] =this.lenovo_custom_service_noTextBox.Text.Trim() ;
            dr["lenovo_maintenance_no"] =this.lenovo_maintenance_noTextBox.Text.Trim() ;
            dr["lenovo_repair_no"] =this.lenovo_repair_noTextBox.Text.Trim() ;
            dr["whole_machine_no"] =this.whole_machine_noTextBox.Text.Trim() ;            

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

        private void DeliveredTableForm_Load(object sender, EventArgs e)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.numTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.source_briefComboBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.storehouseTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.custom_orderComboBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.order_out_dateTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.order_receive_dateTextBox.Text = dataGridView1.SelectedCells[7].Value.ToString();
            this.custom_machine_typeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.mb_briefTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.custommaterialNoTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.dpk_statusTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.track_serial_noTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.custom_serial_noTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.vendor_serail_noTextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.uuidTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.macTextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.vendormaterialNoTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();

            this.mb_describeTextBox.Text = dataGridView1.SelectedCells[18].Value.ToString(); ;
            this.mb_make_dateTextBox.Text = dataGridView1.SelectedCells[19].Value.ToString(); ;
            this.warranty_periodTextBox.Text = dataGridView1.SelectedCells[20].Value.ToString(); ;
            this.custom_faultComboBox.Text = dataGridView1.SelectedCells[21].Value.ToString(); ;
            this.guaranteeComboBox.Text = dataGridView1.SelectedCells[22].Value.ToString(); ;
            this.customResponsibilityComboBox.Text = dataGridView1.SelectedCells[23].Value.ToString(); ;
            this.lenovo_custom_service_noTextBox.Text = dataGridView1.SelectedCells[24].Value.ToString(); ;
            this.lenovo_maintenance_noTextBox.Text = dataGridView1.SelectedCells[25].Value.ToString(); ;
            this.lenovo_repair_noTextBox.Text = dataGridView1.SelectedCells[26].Value.ToString(); ;
            this.whole_machine_noTextBox.Text = dataGridView1.SelectedCells[27].Value.ToString(); ;      
        }
    }
}
