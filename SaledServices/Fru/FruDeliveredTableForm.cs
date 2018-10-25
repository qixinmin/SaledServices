using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SaledServices
{
    public partial class FruDeliveredTableForm : Form
    {
        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "FruDeliveredTable";

        public FruDeliveredTableForm()
        {
            InitializeComponent();

            loadAdditionInfomation();

            receiverTextBox.Text = LoginForm.currentUser;          
            this.receive_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.modify.Visible = false;
                this.delete.Visible = false;
            }
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

                cmd.CommandText = "select distinct guarantee_describe from guarantee";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.guaranteeComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();
                guaranteeComboBox.SelectedIndex = 0;

                //加载没有收完货的订单
                cmd.CommandText = "select distinct orderno from frureceiveOrder where _status = 'open'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.ordernoComboBox.Items.Add(temp);
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

        private void simulateEnter(string custommaterialNo)
        {
            if (ordernoComboBox.Text == "" || custommaterialNo == "")
            {
                MessageBox.Show("无效订单编号");
                return;
            }
            string status = "";
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select vendor, product, storehouse, _status from frureceiveOrder where orderno = '" + this.ordernoComboBox.Text
                    + "' and custom_materialNo = '" + custommaterialNo + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    this.vendorTextBox.Text = querySdr[0].ToString();
                    this.productTextBox.Text = querySdr[1].ToString();
                    this.machine_typeTextBox.Text = querySdr[2].ToString();
                    status = querySdr[3].ToString();
                }
                querySdr.Close();

                if (status == "open")
                {
                    cmd.CommandText = "select vendor,product,machine_type,name,custommaterialdescribe,vendor_material_no,mpn1 from frubomtable where custom_material_no ='"
                        + custommaterialNo + "'";

                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.vendorTextBox.Text = querySdr[0].ToString();
                        this.productTextBox.Text = querySdr[1].ToString();
                        this.machine_typeTextBox.Text = querySdr[2].ToString();
                        this.nameTextBox.Text = querySdr[3].ToString();
                        this.customermaterialdesTextBox.Text = querySdr[4].ToString();
                        this.vendor_material_noTextBox.Text = querySdr[5].ToString();
                        this.mpn1TextBox.Text = querySdr[5].ToString();
                    }
                    querySdr.Close();
                }
                else if (status == "close")
                {
                    this.customermaterialnoTextBox.Focus();
                    this.customermaterialnoTextBox.SelectAll();
                    MessageBox.Show("客户料号：" + this.ordernoComboBox.Text + " 已经收货完毕，请检测是否有错误!");
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (status == "close")
            {
                this.customermaterialnoTextBox.Text = "";
                this.customermaterialnoTextBox.Focus();
                this.customermaterialnoTextBox.SelectAll();
            }
            else
            {
                this.peijian_noTextBox.Focus();
                this.peijian_noTextBox.SelectAll();
            }
        }

        private void custom_orderComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                simulateEnter(this.customermaterialnoTextBox.Text.Trim());
            }
        }

        private void custommaterialNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (!Utils.IsNumAndEnCh(this.customermaterialnoTextBox.Text))
                {
                    MessageBox.Show("包含非字符与数字的字符，请检查！");
                    return;
                }
                
                int row = dataGridViewWaitToReturn.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    dataGridViewWaitToReturn.Rows[i].Selected = false;
                }

                int count = 0;                
                currentMaterialNo = this.customermaterialnoTextBox.Text.Trim();
                for (int i = 0; i < row; i++)
                {
                    string queryedStr = dataGridViewWaitToReturn.Rows[i].Cells[1].Value.ToString();
                    if (queryedStr.EndsWith(currentMaterialNo))
                    {
                        count++;                        
                        this.customermaterialnoTextBox.Text = queryedStr;
                        dataGridViewWaitToReturn.Rows[i].Selected = true;
                    }
                }
                if (count > 1 || count == 0)
                {
                    this.customermaterialnoTextBox.Text = this.currentMaterialNo = "";
                    MessageBox.Show("你输入的不存在或者不唯一，请重新输入！");

                    for (int i = 0; i < row; i++)
                    {                       
                        dataGridViewWaitToReturn.Rows[i].Selected = false;                        
                    }
                }
                else
                {
                    this.currentMaterialNo = this.customermaterialnoTextBox.Text;
                    simulateEnter(this.currentMaterialNo);
                }
            }
        }

        private void custom_orderComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = this.ordernoComboBox.Text;
            if (str == "")
            {
                return;
            }
      
            doQueryAfterSelection();
        }

        private void doQueryAfterSelection()
        {
            try
            {
                this.dataGridViewWaitToReturn.DataSource = null;
                dataGridViewWaitToReturn.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                //加入条件判断，只显示未收完的货物
                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,ordernum, receivedNum from frureceiveOrder where orderno='" + this.ordernoComboBox.Text + "' and _status='open'" ;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "frureceiveOrder");
                dataGridViewWaitToReturn.DataSource = ds.Tables[0];
                dataGridViewWaitToReturn.RowHeadersVisible = false;

                string[] hTxt = { "订单编号", "客户料号", "MB简称", "订单数量", "收货数量" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewWaitToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewWaitToReturn.Columns[i].Name = hTxt[i];
                }

                DataGridViewColumn dc = new DataGridViewColumn();
                dc.DefaultCellStyle.BackColor = Color.Red;
                dc.Name = "差数";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "差数";
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridViewWaitToReturn.Columns.Add(dc);

                foreach (DataGridViewRow dr in dataGridViewWaitToReturn.Rows)
                {
                    try
                    {
                        int oNum = Int32.Parse(dr.Cells["订单数量"].Value.ToString());
                        int rNum = Int32.Parse(dr.Cells["收货数量"].Value.ToString());

                        if(oNum-rNum == 0)
                        {
                            dr.Cells["差数"].Style.BackColor = Color.Green;
                        }
                        dr.Cells["差数"].Value = (oNum - rNum) + " ";
                    }
                    catch (Exception ex)
                    { }
                }

                mConn.Close();

                if (ds.Tables[0].Rows.Count > 0) 
                {
                    dataGridViewWaitToReturn.Rows[0].Selected = false;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
       
        private void add_Click(object sender, EventArgs e)
        {
            if (checkInputIsNull())
            {
                MessageBox.Show("需要输入的内容为空，请检查！");
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

                    cmd.CommandText = "select vendor from " + this.tableName + " where peijian_no = '" + this.peijian_noTextBox.Text + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string existTrack = "";
                    while (querySdr.Read())
                    {
                        existTrack = querySdr[0].ToString();
                    }
                    if (existTrack != "")
                    {
                        this.peijian_noTextBox.Focus();
                        this.peijian_noTextBox.SelectAll();
                        querySdr.Close();
                        conn.Close();
                        clearInputContent();
                        MessageBox.Show("跟踪条码：" + this.peijian_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        return;
                    }
                    else
                    {
                        querySdr.Close();
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.ordernoComboBox.Text.Trim() + "','" +
                        this.vendorTextBox.Text.Trim() + "','" +
                        this.productTextBox.Text.Trim() + "','" +
                        this.customermaterialnoTextBox.Text.Trim() + "','" +
                        this.machine_typeTextBox.Text.Trim() + "','" +
                        this.nameTextBox.Text.Trim() + "','" +
                        this.customermaterialdesTextBox.Text.Trim() + "','" +
                        this.peijian_noTextBox.Text.Trim() + "','" +
                        this.customer_serial_noTextBox.Text.Trim() + "','" +
                        this.custom_faultTextBox.Text.Trim() + "','" +
                        this.make_datedateTimePicker.Text.Trim()+ "','" +
                        this.guaranteeComboBox.Text.Trim() + "','" +
                        this.gurantee_noteTextBox.Text.Trim() + "','" +
                        this.vendor_material_noTextBox.Text.Trim() + "','" +
                        this.mpn1TextBox.Text.Trim() + "','" +
                        this.receiverTextBox.Text.Trim() + "','" +                       
                        this.receive_dateTextBox.Text.Trim() +
                        "')";
                   
                    cmd.ExecuteNonQuery();

                    //除正常插入数据外，还需要把收还货表格的数量修改 TODO...
                    //1. 修改收还货表格的收货数量， 判断，小于 等于，大于的情况
                    //2 如果小于 只是修改数据
                    //3 如果等于 则需要把状态也修改位close， 如果大于则直接报错
                    //update receiveOrder set returnNum = '1' where id = '1'

                    cmd.CommandText = "select _status, ordernum, receivedNum, receivedate from frureceiveOrder where orderno = '" + this.ordernoComboBox.Text
                         + "' and custom_materialNo = '" + this.customermaterialnoTextBox.Text + "'";
                    int orderNum;
                    int receivedNum=0;
                    string status = "open";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        if (querySdr[0].ToString() == "close")
                        {
                            MessageBox.Show("本板子已经收货完毕，请检测是否有错误!");
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

                    cmd.CommandText = "update frureceiveOrder set _status = '" + status + "',receivedNum = '" + (receivedNum + 1) +
                                "', receivedate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                                + "where orderno = '" + this.ordernoComboBox.Text
                                + "' and custom_materialNo = '" + this.customermaterialnoTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Fru收货成功！");

                    clearInputContent();
                    doQueryAfterSelection();
                    queryLatest(true);

                    if (status != "close")
                    {
                        this.customermaterialnoTextBox.Text = this.currentMaterialNo;
                        simulateEnter(this.currentMaterialNo);

                        int row = dataGridViewWaitToReturn.Rows.Count;
                        for (int i = 0; i < row; i++)
                        {
                            if (currentMaterialNo == dataGridViewWaitToReturn.Rows[i].Cells[1].Value.ToString())
                            {                              
                                dataGridViewWaitToReturn.Rows[i].Selected = true;
                            }
                        }   
                    }
                    else
                    {
                        this.currentMaterialNo = "";
                        clearOrderReleatedInfo();
                    }
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearOrderReleatedInfo()
        {
            this.vendorTextBox.Text = "";
            this.productTextBox.Text = "";
            this.machine_typeTextBox.Text = "";
           
            this.customermaterialdesTextBox.Text = "";
            this.nameTextBox.Text = "";
            this.vendor_material_noTextBox.Text = "";
            this.mpn1TextBox.Text = "";
        }

        private void clearInputContent()
        {
            this.customermaterialnoTextBox.Text = "";

            this.ordernoComboBox.Text = "";
            this.peijian_noTextBox.Text = "";
            this.customer_serial_noTextBox.Text = "";
            this.custom_faultTextBox.Text = "";
          
            this.gurantee_noteTextBox.Text = "";
        }

        private bool checkInputIsNull()
        {
            if (this.ordernoComboBox.Text == ""
                || this.customermaterialnoTextBox.Text == ""
                || this.peijian_noTextBox.Text == ""
                || this.customer_serial_noTextBox.Text == ""
                || this.custom_faultTextBox.Text == ""          
                || this.guaranteeComboBox.Text == ""
              )
            {
                return true;
            }

            return false;           
        }

        private void queryLatest(bool latest)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                if (latest)
                {
                    cmd.CommandText = "select top 3 * from " + tableName + " order by id desc"; 
                }
                else
                {
                    cmd.CommandText = "select * from  " + tableName;
                }
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

            string[] hTxt = { "ID","订单编号", "厂商", "客户别","客户料号"
            ,"机型","名称","客户物料描述","配件序号","客户序号","客户故障","生产日期","保内/保外","保外备注",
            "厂商料号","MPN1","收件人","收货日期"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            queryLatest(false);
        }

        private void modify_Click(object sender, EventArgs e)
        {
            //DataTable dt = ds.Tables[tableName];
            //sda.FillSchema(dt, SchemaType.Mapped);
            //DataRow dr = dt.Rows.Find(this.mpn1TextBox.Text.Trim());

            //dr["vendor"] = this.vendorTextBox.Text.Trim();
            //dr["product"] = this.productTextBox.Text.Trim();
            //dr["source_brief"] = this.source_briefComboBox.Text.Trim();
            //dr["storehouse"] = this.machine_typeTextBox.Text.Trim();
            //dr["custom_order"] = this.ordernoComboBox.Text.Trim();
            //dr["order_out_date"] = this.order_out_dateTextBox.Text.Trim();
            //dr["order_receive_date"] = this.receive_dateTextBox.Text.Trim();            
            //dr["custom_machine_type"] = this.custom_machine_typeTextBox.Text.Trim();
            //dr["mb_brief"] = this.customermaterialdesTextBox.Text.Trim();
            //dr["custommaterialNo"] = this.customermaterialnoTextBox.Text.Trim();
            //dr["dpk_status"] = this.dpk_statusTextBox.Text.Trim();
            //dr["track_serial_no"] = this.peijian_noTextBox.Text.Trim();
            //dr["custom_serial_no"] = this.customer_serial_noTextBox.Text.Trim();
            //dr["vendor_serail_no"] = this.custom_faultTextBox.Text.Trim();
            //dr["uuid"] = this.uuidTextBox.Text.Trim();
            //dr["mac"] = this.gurantee_noteTextBox.Text.Trim();
            //dr["mpn"] = this.mpnTextBox.Text.Trim();
            //dr["mb_describe"] = this.nameTextBox.Text.Trim();
            //dr["mb_make_date"] = this.mb_make_dateTextBox.Text.Trim();
            //dr["warranty_period"] = this.vendor_material_noTextBox.Text.Trim();
            //dr["custom_fault"] = this.custom_faultComboBox.Text.Trim();
            //dr["guarantee"] = this.guaranteeComboBox.Text.Trim();
            //dr["customResponsibility"] = this.customResponsibilityComboBox.Text.Trim();
            //dr["lenovo_custom_service_no"] = this.lenovo_custom_service_noTextBox.Text.Trim();
            //dr["lenovo_maintenance_no"] = this.lenovo_maintenance_noTextBox.Text.Trim();
            //dr["lenovo_repair_no"] = this.lenovo_repair_noTextBox.Text.Trim();
            //dr["whole_machine_no"] = this.whole_machine_noTextBox.Text.Trim();
            //dr["inputuser"] = this.receiverTextBox.Text.Trim();

            //SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            //sda.Update(dt);
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
                    cmd.CommandType = CommandType.Text;                    

                    cmd.CommandText = "select receivedNum from frureceiveOrder where orderno = '" + this.ordernoComboBox.Text
                         + "' and custom_materialNo = '" + this.customermaterialnoTextBox.Text + "'";
                   
                    int receivedNum = 0;
                    string status = "open";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        receivedNum = Int32.Parse(querySdr[0].ToString());                           
                    }
                    querySdr.Close();

                    cmd.CommandText = "update frureceiveOrder set _status = '" + status + "',receivedNum = '" + (receivedNum - 1) + "' "
                                + "where orderno = '" + this.ordernoComboBox.Text
                                + "' and custom_materialNo = '" + this.customermaterialnoTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Delete from " + tableName + " where id = " + dataGridView1.SelectedCells[0].Value.ToString();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();

                MessageBox.Show("删除完毕!");
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
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            //this.mpn1TextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            //this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //this.productTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            //this.source_briefComboBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            //this.machine_typeTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            //this.ordernoComboBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            //this.order_out_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[6].Value.ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //this.receive_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[7].Value.ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //this.custom_machine_typeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            //this.customermaterialdesTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            //this.customermaterialnoTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            //this.dpk_statusTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            //this.peijian_noTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            //this.customer_serial_noTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            //this.custom_faultTextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            //this.uuidTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            //this.gurantee_noteTextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            //this.mpnTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();

            //this.nameTextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();
            //this.mb_make_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[19].Value.ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //this.vendor_material_noTextBox.Text = dataGridView1.SelectedCells[20].Value.ToString();
            //this.custom_faultComboBox.Text = dataGridView1.SelectedCells[21].Value.ToString();
            //this.guaranteeComboBox.Text = dataGridView1.SelectedCells[22].Value.ToString();
            //this.customResponsibilityComboBox.Text = dataGridView1.SelectedCells[23].Value.ToString();
            //this.lenovo_custom_service_noTextBox.Text = dataGridView1.SelectedCells[24].Value.ToString();
            //this.lenovo_maintenance_noTextBox.Text = dataGridView1.SelectedCells[25].Value.ToString();
            //this.lenovo_repair_noTextBox.Text = dataGridView1.SelectedCells[26].Value.ToString();
            //this.whole_machine_noTextBox.Text = dataGridView1.SelectedCells[27].Value.ToString();

            //this.receiverTextBox.Text = dataGridView1.SelectedCells[28].Value.ToString(); 
        }
        
        private void vendor_serail_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.custom_faultTextBox.Text.Length != 13)
                {
                    this.custom_faultTextBox.SelectAll();
                    MessageBox.Show("厂商序号的内容长度不是13位，请检查！");
                    return; 
                }

                if (!Utils.IsNumAndEnCh(this.custom_faultTextBox.Text))
                {
                    MessageBox.Show("包含非字符与数字的字符，请检查！");
                    return;
                }

                //检查客户序号或厂商序号是否已经存在本订单编号里面了，收货表中
                string vendor = "";
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor from " + this.tableName + " where vendor_serail_no = '" + this.custom_faultTextBox.Text
                        + "' and custom_order = '" + this.ordernoComboBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (vendor != "")
                    {
                        MessageBox.Show("客户序号：" + this.customer_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        this.custom_faultTextBox.Focus();
                        this.custom_faultTextBox.SelectAll();
                        return;
                    }

                    cmd.CommandText = "select vendor from " + this.tableName + " where vendor_serail_no = '" + this.custom_faultTextBox.Text+ "'";
                    querySdr = cmd.ExecuteReader();
                    int count = 0;
                    while (querySdr.Read())
                    {
                        count++;
                    }
                    querySdr.Close();

                    if (count !=0)
                    {
                        MessageBox.Show("此客户序号已经来过【"+count+"】次，请记录下来");
                    }
                    
                    //根据数据库的内容，把内容查找如果，如果存在，则保修期为15个月，否则默认
                    cmd.CommandText = "select Id from limit_gurante where MB_COMPAL_SN = '" + this.custom_faultTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    bool existVendorSerial = false;
                    if (querySdr.HasRows)
                    {
                        existVendorSerial = true;
                        this.vendor_material_noTextBox.Text = "15M";
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

        string currentMaterialNo = "";
        private void dataGridViewWaitToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewWaitToReturn.CurrentRow == null)
            {
                return;
            }
            currentMaterialNo = this.customermaterialnoTextBox.Text = dataGridViewWaitToReturn.SelectedCells[1].Value.ToString();
            simulateEnter(this.customermaterialnoTextBox.Text.Trim());
        }

        private void peijian_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.customer_serial_noTextBox.SelectAll();
                this.customer_serial_noTextBox.Focus();
            }
        }

        private void customer_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.custom_faultTextBox.SelectAll();
                this.custom_faultTextBox.Focus();
            }
        }       
    }
}
