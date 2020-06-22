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
using SaledServices.Test_Outlook;

namespace SaledServices
{
    public partial class DeliveredTableForm : Form
    {
        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "DeliveredTable";

        public DeliveredTableForm()
        {
            InitializeComponent();

            loadAdditionInfomation();

            inputUserTextBox.Text = LoginForm.currentUser;

            if (User.UserSelfForm.isSuperManager() == false)
            {
                if (User.UserSelfForm.isReceiveReturn() == true)
                {
                    this.modify.Visible = true;
                    this.delete.Visible = false;
                }
                else
                {
                    this.modify.Visible = false;
                    this.delete.Visible = false;
                }
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

                this.source_briefComboBox.Items.Clear();

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

                this.custom_faultComboBox.Items.Clear();
                cmd.CommandText = "select fault_index, fault_describe from customFault";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string index = querySdr[0].ToString();
                    string temp = querySdr[1].ToString();
                    if (temp != "")
                    {
                        this.custom_faultComboBox.Items.Add(temp);
                        if (myDictionary.Keys.Contains(index) == false)
                        {
                            myDictionary.Add(index, temp);
                        }
                    }
                }
                querySdr.Close();

                this.guaranteeComboBox.Items.Clear();
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
                guaranteeComboBox.SelectedIndex = 0;

                this.customResponsibilityComboBox.Items.Clear();
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
                this.custom_orderComboBox.Items.Clear();
                cmd.CommandText = "select distinct orderno from receiveOrder where _status = 'open'";
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

        private void simulateEnter(string custommaterialNo)
        {
            if (custom_orderComboBox.Text == "" || custommaterialNo == "")
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

                cmd.CommandText = "select vendor, product, storehouse, _status from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                    + "' and custom_materialNo = '" + custommaterialNo + "'";

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    this.vendorTextBox.Text = querySdr[0].ToString();
                    this.productTextBox.Text = querySdr[1].ToString();
                    this.storehouseTextBox.Text = querySdr[2].ToString();
                    status = querySdr[3].ToString();
                }
                querySdr.Close();

                if (status == "open")
                {
                    cmd.CommandText = "select custom_machine_type,mb_brief,dpk_type,mpn,mb_descripe,warranty_period,vendor from MBMaterialCompare where custommaterialNo ='"
                        + custommaterialNo + "'";

                    querySdr = cmd.ExecuteReader();
                    string materialVendor = "";
                    while (querySdr.Read())
                    {
                        this.custom_machine_typeTextBox.Text = querySdr[0].ToString();
                        this.mb_briefTextBox.Text = querySdr[1].ToString();
                        this.dpk_statusTextBox.Text = querySdr[2].ToString();
                        this.mpnTextBox.Text = querySdr[3].ToString();
                        this.mb_describeTextBox.Text = querySdr[4].ToString();
                        this.warranty_periodTextBox.Text = querySdr[5].ToString();
                        materialVendor = querySdr[6].ToString();
                    }
                    querySdr.Close();

                    if (this.vendorTextBox.Text.Trim() != materialVendor)
                    {
                        MessageBox.Show("厂商不一致，导入厂商是：" + this.vendorTextBox.Text + " 而物料对照表的厂商是：" + materialVendor + ",请检查");
                        this.add.Enabled = false;
                    }
                    else
                    {
                        this.add.Enabled = true;
                    }
                }
                else if (status == "close")
                {
                    this.custommaterialNoTextBox.Focus();
                    this.custommaterialNoTextBox.SelectAll();
                    MessageBox.Show("客户料号：" + this.custom_orderComboBox.Text + " 已经收货完毕，请检测是否有错误!");
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (status == "close")
            {
                this.custommaterialNoTextBox.Text = "";
                this.custommaterialNoTextBox.Focus();
                this.custommaterialNoTextBox.SelectAll();
            }
            else
            {
                this.track_serial_noTextBox.Focus();
                this.track_serial_noTextBox.SelectAll();
            }
        }

        private void custom_orderComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                simulateEnter(this.custommaterialNoTextBox.Text.Trim());
            }
        }

        private void custommaterialNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (!Utils.IsNumAndEnCh(this.custommaterialNoTextBox.Text))
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
                currentMaterialNo = this.custommaterialNoTextBox.Text.Trim();
                for (int i = 0; i < row; i++)
                {
                    string queryedStr = dataGridViewWaitToReturn.Rows[i].Cells[1].Value.ToString();
                    if (queryedStr.EndsWith(currentMaterialNo))
                    {
                        count++;                        
                        this.custommaterialNoTextBox.Text = queryedStr;
                        dataGridViewWaitToReturn.Rows[i].Selected = true;
                    }
                }
                if (count > 1 || count == 0)
                {
                    this.custommaterialNoTextBox.Text = this.currentMaterialNo = "";
                    MessageBox.Show("你输入的不存在或者不唯一，请重新输入！");

                    for (int i = 0; i < row; i++)
                    {                       
                        dataGridViewWaitToReturn.Rows[i].Selected = false;                        
                    }
                }
                else
                {
                    this.currentMaterialNo = this.custommaterialNoTextBox.Text;
                    simulateEnter(this.currentMaterialNo);
                }
            }
        }

        private void custom_orderComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = this.custom_orderComboBox.Text;
            if (str == "")
            {
                return;
            }

            string substr = "";
            string inTime = "";
            //try
            //{
            //    substr = str.Substring(str.Length - 8, 6);
            //    inTime = "20" + substr.Substring(0, 2) + "/" + substr.Substring(2, 2) + "/" + substr.Substring(4, 2);
            //    this.order_out_dateTextBox.Text = inTime;
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("订单号码时间格式错误, 设置默认值！");
            //    this.order_out_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //}
            this.order_out_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            //if (Utils.isTimeError(this.order_out_dateTextBox.Text.Trim()))
            //{
            //    this.add.Enabled = false;
            //}

            try
            {
                DateTime dt1 = DateTime.Parse(inTime);
                DateTime dt2 = DateTime.Now;

                TimeSpan ts = dt2.Subtract(dt1);
                if (ts.TotalDays < 0)
                {
                    MessageBox.Show("请检测当前机器的时间是否正确！");
                    return;
                }
                this.order_receive_dateTextBox.Text = dt2.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("订单号码时间格式错误, 设置默认值！");
                this.order_receive_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
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
                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,ordernum, receivedNum from receiveOrder where orderno='" + this.custom_orderComboBox.Text + "' and _status='open'" ;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
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
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (!Utils.IsNumAndEnCh(this.custom_serial_noTextBox.Text.Trim()))
                {
                    MessageBox.Show("包含非字符与数字的字符，请检查！");
                    this.add.Enabled = false;
                    return;
                }
                string customSerialNo = this.custom_serial_noTextBox.Text.Trim();
                customSerialNo = Regex.Replace(customSerialNo, "[^a-zA-Z0-9]", "");
                this.custom_serial_noTextBox.Text = customSerialNo;
                if (customSerialNo.StartsWith("8S"))
                {
                    if (customSerialNo.Length != 23)
                    {
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        MessageBox.Show("客户序号的长度不是23位，请检查！");
                        this.add.Enabled = false;
                        return;
                    }
                }
                else if (customSerialNo.StartsWith("11S"))
                {
                    if (customSerialNo.Length != 22)
                    {
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        MessageBox.Show("客户序号的长度不是22位，请检查！");
                        this.add.Enabled = false;
                        return;
                    }
                }
                if (this.vendorTextBox.Text != "宝龙达")
                {
                    if (this.productTextBox.Text != "TBG" && this.productTextBox.Text != "DT" && this.productTextBox.Text !="AIO")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                    {
                        //需要去掉前面的非0字段
                        string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');

                        if (this.custom_serial_noTextBox.Text.Trim().ToLower().Contains(customSerial.ToLower()) == false)
                        {
                            MessageBox.Show("在" + this.productTextBox.Text + "下客户序号没有包含客户料号");
                            this.custom_serial_noTextBox.Focus();
                            this.custom_serial_noTextBox.SelectAll();
                            this.add.Enabled = false;
                            return;
                        }
                    }

                    if (this.storehouseTextBox.Text == "成都库" || this.storehouseTextBox.Text == "惠阳库")//成都库惠阳库单独检测
                    {
                        //需要去掉前面的非0字段
                        string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');

                        if (this.custom_serial_noTextBox.Text.Trim().ToLower().Contains(customSerial.ToLower()) == false)
                        {
                            MessageBox.Show("在" + this.productTextBox.Text + "下客户序号没有包含客户料号");
                            this.custom_serial_noTextBox.Focus();
                            this.custom_serial_noTextBox.SelectAll();
                            this.add.Enabled = false;
                            return;
                        }
                    }
                }
                
               
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
                    this.custom_serial_noTextBox.Focus();
                    this.custom_serial_noTextBox.SelectAll();
                    this.add.Enabled = false;
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

                    cmd.CommandText = "select vendor from " + this.tableName + " where custom_serial_no = '" + this.custom_serial_noTextBox.Text.Trim()
                        + "' and custom_order = '" + this.custom_orderComboBox.Text + "'"; 

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (vendor != "")
                    {
                        MessageBox.Show("客户序号：" + this.custom_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        this.custom_serial_noTextBox.Focus();
                        this.custom_serial_noTextBox.SelectAll();
                        this.add.Enabled = false;
                        return;
                    }

                    cmd.CommandText = "select check_reason from mb_receive_check where custom_serial_no = '" + this.custom_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                     while (querySdr.Read())
                    {
                        MessageBox.Show("客户序号被拦截，原因是：" + querySdr[0].ToString());
                    }
                    querySdr.Close();

                    //TODO 120天的多次主板需要写FA分析
                    //select D.custom_serial_no from DeliveredTable as D inner Join returnStore as R on D.custom_serial_no = R.custom_serial_no where R.return_date>'2020-03-01' order by R.return_date desc
                    //1 时间动态变化，需要之前推四个月
                    //2 找到最近一个的还货时间，并记录下来，等会在维修表中使用
                    //DateTime nowTime = DateTime.Now;
                    //DateTime beforeTime = nowTime.AddMonths(-4);
                    //String beforeTimeStr = beforeTime.ToString("yyyy-MM-dd");
                    //cmd.CommandText = "select top 1 D.custom_serial_no from DeliveredTable as D inner Join returnStore as R on D.custom_serial_no = R.custom_serial_no where R.custom_serial_no ='"
                    //    + this.custom_serial_noTextBox.Text.Trim() + "' and R.return_date>'" + beforeTimeStr + "' order by R.return_date desc";
                    //querySdr = cmd.ExecuteReader();
                    //if (querySdr.HasRows)
                    //{

                    //}
                    //querySdr.Close();


                


                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                

                string year, mouth, day;
                vendor = this.vendorTextBox.Text.Trim();
                if (vendor == "COMPAL")
                {
                    year = Utils.getTimeByCharCompal(true, Convert.ToChar(subData.Substring(0, 1)));
                    mouth = Utils.getTimeByCharCompal(false, Convert.ToChar(subData.Substring(1, 1)));
                    day = Utils.getTimeByCharCompal(false, Convert.ToChar(subData.Substring(2, 1)));

                    if(day == "31")
                    {
                         switch (mouth)
                        {
                             //case "2":
                             case "4":
                             case "6":
                             case "9":
                             case "11":
                                 day = "30";
                                 break;
                        }
                    }                   
                }
                else
                {
                    year = Utils.getTimeByChar(true, Convert.ToChar(subData.Substring(0, 1)));
                    mouth = Utils.getTimeByChar(false, Convert.ToChar(subData.Substring(1, 1)));
                    day = Utils.getTimeByChar(false, Convert.ToChar(subData.Substring(2, 1)));
                }
              
                this.mb_make_dateTextBox.Text = year + "/" + mouth + "/" + day;

                try
                {
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
                            this.guaranteeComboBox.Text = "保外";
                            this.guaranteeComboBox.Enabled = false;
                            this.customResponsibilityComboBox.Text = "过保";
                            this.customResponsibilityComboBox.Enabled = false;
                            MessageBox.Show((overdays) + " 天超过， 已经过保!");
                        }
                        else
                        {
                            this.guaranteeComboBox.Text = "";
                            this.guaranteeComboBox.Enabled = true;
                            this.customResponsibilityComboBox.Text = "";
                            //this.customResponsibilityComboBox.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("客户料号里面的日期规则不对! 手動修改");
                   // this.add.Enabled = false;
                    return;
                }

                this.vendor_serail_noTextBox.Focus();
                this.vendor_serail_noTextBox.SelectAll();
                this.add.Enabled = true;
            }
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

                    cmd.CommandText = "select vendor from " + this.tableName + " where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string existTrack = "";
                    while (querySdr.Read())
                    {
                        existTrack = querySdr[0].ToString();
                    }
                    if (existTrack != "")
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        querySdr.Close();
                        conn.Close();
                        clearInputContent();
                        MessageBox.Show("跟踪条码：" + this.track_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        return;
                    }
                    else
                    {
                        querySdr.Close();
                    }

                    //加入判断此料号是否存在于订单中                    
                    cmd.CommandText = "select custom_materialNo  from receiveOrder where orderno = '" + this.custom_orderComboBox.Text + "'";
                    bool existCustom_material_no=false;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        if(this.custommaterialNoTextBox.Text.Trim().ToUpper().Equals(querySdr[0].ToString().ToUpper().Trim()))
                        {
                            existCustom_material_no = true;
                            break;
                        }
                    }
                    querySdr.Close();
                    if (existCustom_material_no == false)
                    {
                        querySdr.Close();
                        conn.Close();
                        clearInputContent();
                        MessageBox.Show("此客户料号不存在此订单中，请检测是否有错误!");
                        return;
                    }
                    //end

                    //加入判断8s的跟fru或fru的替换料的包含关系
                    cmd.CommandText = "select fruNo, replace_fruNo  from MBMaterialCompare where  custommaterialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    bool existfru=false;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string fru = querySdr[0].ToString();
                        string replacefru = querySdr[1].ToString();
                        if (fru != null && fru!="" && this.custom_serial_noTextBox.Text.Trim().Contains(fru))                        
                        {
                            existfru = true;
                            break;
                        }

                        if (replacefru != null && replacefru != "" && this.custom_serial_noTextBox.Text.Trim().Contains(replacefru))
                        {
                            existfru = true;
                            break;
                        }
                    }
                    querySdr.Close();

                    if (!existfru)
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        querySdr.Close();
                        conn.Close();
                        clearInputContent();
                        MessageBox.Show("8s条码不包含fru与替换fru的内容!");
                        return;
                    }

                    this.order_out_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);//设置为当前日期
                                       

                    //除正常插入数据外，还需要把收还货表格的数量修改 TODO...
                    //1. 修改收还货表格的收货数量， 判断，小于 等于，大于的情况
                    //2 如果小于 只是修改数据
                    //3 如果等于 则需要把状态也修改位close， 如果大于则直接报错
                    //update receiveOrder set returnNum = '1' where id = '1'

                    cmd.CommandText = "select _status, ordernum, receivedNum, receivedate from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                         + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
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

                    //插入obe的抽查比例
                    cmd.CommandText = "select ordernum from receiveOrder where orderno='" + this.custom_orderComboBox.Text.Trim() 
                        + "' and custom_materialNo='" + currentMaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    string ordernum = "";
                    while (querySdr.Read())
                    {
                        ordernum = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (ordernum == "")
                    {
                        MessageBox.Show("此订单编号不存在！"+this.custom_orderComboBox.Text.Trim());                        
                        conn.Close();
                        return;
                    }

                    try
                    {
                        int num = Int16.Parse(ordernum);
                        if (num <= 0)
                        {
                            MessageBox.Show("此订单编号对应的数量不对！");
                            conn.Close();
                            return;
                        }

                        cmd.CommandText = "select top 1 rate from obe_checkrate_table where orderno='" + this.custom_orderComboBox.Text.Trim()
                            + "' and custom_materialNo='" + currentMaterialNo + "'";
                        cmd.CommandType = CommandType.Text;
                        querySdr = cmd.ExecuteReader();

                        string rateStr = "";
                        while (querySdr.Read())
                        {
                            rateStr = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        try
                        {
                            //现在基数有了，查询数据库中有多少个，然后决定当前跟踪条码是第几个
                            double rate = Double.Parse(rateStr);// 0.15;

                            int totalchecknum = (int)Math.Ceiling(num * rate);

                            //查询现在有多少个了，只需要查最后一个，也许没有
                            cmd.CommandText = "select COUNT(*)  from decideOBEchecktable where orderno='" + this.custom_orderComboBox.Text.Trim()
                                + "'and custom_materialNo='" + currentMaterialNo + "'";
                            querySdr = cmd.ExecuteReader();
                            string existnum = "";
                            while (querySdr.Read())
                            {
                                existnum = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            int currentIndex = Int16.Parse(existnum) + 1;
                            bool ischeck = isObeCheck(num, totalchecknum, currentIndex);

                            //后续要插入到数据库中
                            cmd.CommandText = "INSERT INTO decideOBEchecktable VALUES('"
                              + this.track_serial_noTextBox.Text.Trim() + "','"
                              + this.custom_orderComboBox.Text.Trim() + "','"
                              + currentMaterialNo + "','"
                              + num + "','"
                              + rate + "','"
                              + currentIndex + "','"
                              + (ischeck ? "True" : "False") + "','"
                              + this.inputUserTextBox.Text.Trim() + "','"
                              + this.order_receive_dateTextBox.Text.Trim()
                              + "')";
                            cmd.ExecuteNonQuery();

                            if (ischeck)
                            {
                                MessageBox.Show(this.track_serial_noTextBox.Text.Trim() + " 需要过OBE站别，请分类");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    //end obe 抽查比例

                    cmd.CommandText = "update receiveOrder set _status = '" + status + "',receivedNum = '" + (receivedNum + 1) +
                                "', receivedate = '" + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                                + "where orderno = '" + this.custom_orderComboBox.Text
                                + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    string receiveOrderindex = this.custom_orderComboBox.Text.Trim() + this.custommaterialNoTextBox.Text.Trim() + (receivedNum + 1);
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
                        this.mpnTextBox.Text.Trim() + "','" +
                        this.mb_describeTextBox.Text.Trim() + "','" +
                        this.mb_make_dateTextBox.Text.Trim() + "','" +
                        this.warranty_periodTextBox.Text.Trim() + "','" +
                        this.custom_faultComboBox.Text.Trim().Replace('\'', '_') + "','" +
                        this.guaranteeComboBox.Text.Trim() + "','" +
                        this.customResponsibilityComboBox.Text.Trim() + "','" +
                        this.lenovo_custom_service_noTextBox.Text.Trim() + "','" +
                        this.lenovo_maintenance_noTextBox.Text.Trim() + "','" +
                        this.lenovo_repair_noTextBox.Text.Trim() + "','" +
                        this.whole_machine_noTextBox.Text.Trim() + "','" +
                        this.inputUserTextBox.Text.Trim() + "','" +
                        receiveOrderindex+
                        "')";
                    cmd.ExecuteNonQuery();

                    //记录站别信息
                    cmd.CommandText = "INSERT INTO stationInformation VALUES('"
                        + this.track_serial_noTextBox.Text.Trim() + "','收货','"
                        + DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                    cmd.ExecuteNonQuery();

                
                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
                   "','收货','" + this.inputUserTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("收货成功！");

                    conn.Close();

                    clearInputContent();
                    doQueryAfterSelection();
                    queryLatest(true);

                    if (status != "close")
                    {
                        this.custommaterialNoTextBox.Text = this.currentMaterialNo;
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

            

        private bool isObeCheck(int totalnum, int totalchecknum, int currentIndex)
        {
            if (totalnum == currentIndex)//比如总数为1
            {
                return true;
            }

            if (totalchecknum == currentIndex)//比如最后一个
            {
                return true;
            }

            int step = (int)Math.Floor(totalnum / totalchecknum*1.0);//步长取数值
            int forward = 1;
            while (forward <= totalchecknum)
            {
                forward += step;
                if (forward == currentIndex)
                {
                    return true;
                }
            }

            return false;
        }

        private void clearOrderReleatedInfo()
        {
            this.vendorTextBox.Text = "";
            this.productTextBox.Text = "";
            this.storehouseTextBox.Text = "";

            this.custom_machine_typeTextBox.Text = "";
            this.mb_briefTextBox.Text = "";
            this.dpk_statusTextBox.Text = "";
            this.mpnTextBox.Text = "";
            this.mb_describeTextBox.Text = "";
            this.warranty_periodTextBox.Text = "";
            this.numTextBox.Text = "";
            this.mb_make_dateTextBox.Text = "";
            //this.order_out_dateTextBox.Text = "";
            guaranteeComboBox.SelectedIndex = 0;
        }

        private void clearInputContent()
        {
            this.custommaterialNoTextBox.Text = "";

            this.custom_orderComboBox.Text = "";
            //this.source_briefComboBox.Text = "";
            //this.source_briefComboBox.SelectedIndex = -1;
            this.track_serial_noTextBox.Text = "";
            this.custom_serial_noTextBox.Text = "";
            this.vendor_serail_noTextBox.Text = "";
            this.uuidTextBox.Text = "";
            this.macTextBox.Text = "";
            this.custom_faultComboBox.Text = "";
            this.custom_faultComboBox.SelectedIndex = -1;
            //this.guaranteeComboBox.Text = "";
            //this.guaranteeComboBox.SelectedIndex = -1;
            this.customResponsibilityComboBox.Text = "";
            this.customResponsibilityComboBox.SelectedIndex = -1;
            this.lenovo_custom_service_noTextBox.Text = "";
            this.lenovo_maintenance_noTextBox.Text = "";
            this.lenovo_repair_noTextBox.Text = "";
            this.whole_machine_noTextBox.Text = "";
            guaranteeComboBox.SelectedIndex = 0;
        }

        private bool checkInputIsNull()
        {
            if (this.custom_orderComboBox.Text == ""
                || this.source_briefComboBox.Text == ""

                || this.custommaterialNoTextBox.Text == ""
                || this.track_serial_noTextBox.Text == ""
                || this.custom_serial_noTextBox.Text == ""
                || this.vendor_serail_noTextBox.Text == ""
                //|| this.uuidTextBox.Text == ""
                || this.macTextBox.Text == ""
                || this.custom_faultComboBox.Text == ""
                || this.guaranteeComboBox.Text == ""
                //|| this.customResponsibilityComboBox.Text == ""
                || this.lenovo_custom_service_noTextBox.Text == ""
                || this.lenovo_maintenance_noTextBox.Text == ""
                || this.lenovo_repair_noTextBox.Text == ""
                || this.whole_machine_noTextBox.Text == ""

                || this.vendorTextBox.Text == ""
                || this.productTextBox.Text == ""
                || this.mb_describeTextBox.Text == ""
                || this.mpnTextBox.Text == ""
                )
            {
                return true;
            }

            if (customResponsibilityComboBox.Enabled == true && customResponsibilityComboBox.Text == "")
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

                string sqlstr = "select top 3 Id,vendor,product,source_brief,storehouse,custom_order,order_out_date,order_receive_date,"+
                        "custom_machine_type,mb_brief,custommaterialNo,dpk_status,track_serial_no,custom_serial_no,vendor_serail_no,uuid,mac,mpn,"+
                        "mb_describe,mb_make_date,warranty_period,custom_fault,guarantee,customResponsibility,lenovo_custom_service_no,"+
                        "lenovo_maintenance_no,lenovo_repair_no,whole_machine_no,inputuser from " + tableName;
                if(track_serial_noTextBox.Text.Trim()!="")
                {
                    sqlstr +=" where track_serial_no like '%"+this.track_serial_noTextBox.Text.Trim()+"%' ";
                }
                if (latest)
                {
                    cmd.CommandText =  sqlstr + " order by id desc"; 
                }
                else
                {
                    cmd.CommandText = sqlstr +  " order by id desc"; 
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


            string[] hTxt = { "ID", "厂商", "客户别","来源"
            ,"库别","订单编号","客户出库日期","收货日期","客户机型","mb简称","客户料号","DPK状态","跟踪条码",
            "客户序号","厂商序号","UUID","MAC","MPN","mb描述","MB生产日期","保修期","客户故障","保内/保外"
            ,"客责描述","联想客服序号","联想维修站编号","联想维修单编号","整机序号","收货人"};
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
            DataTable dt = ds.Tables[tableName];
            sda.FillSchema(dt, SchemaType.Mapped);
            DataRow dr = dt.Rows.Find(this.numTextBox.Text.Trim());

            dr["vendor"] = this.vendorTextBox.Text.Trim();
            dr["product"] = this.productTextBox.Text.Trim();
            dr["source_brief"] = this.source_briefComboBox.Text.Trim();
            dr["storehouse"] = this.storehouseTextBox.Text.Trim();
            dr["custom_order"] = this.custom_orderComboBox.Text.Trim();
            dr["order_out_date"] = this.order_out_dateTextBox.Text.Trim();
            dr["order_receive_date"] = this.order_receive_dateTextBox.Text.Trim();            
            dr["custom_machine_type"] = this.custom_machine_typeTextBox.Text.Trim();
            dr["mb_brief"] = this.mb_briefTextBox.Text.Trim();
            dr["custommaterialNo"] = this.custommaterialNoTextBox.Text.Trim();
            dr["dpk_status"] = this.dpk_statusTextBox.Text.Trim();
            dr["track_serial_no"] = this.track_serial_noTextBox.Text.Trim();
            dr["custom_serial_no"] = this.custom_serial_noTextBox.Text.Trim();
            dr["vendor_serail_no"] = this.vendor_serail_noTextBox.Text.Trim();
            dr["uuid"] = this.uuidTextBox.Text.Trim();
            dr["mac"] = this.macTextBox.Text.Trim();
            dr["mpn"] = this.mpnTextBox.Text.Trim();
            dr["mb_describe"] = this.mb_describeTextBox.Text.Trim();
            dr["mb_make_date"] = this.mb_make_dateTextBox.Text.Trim();
            dr["warranty_period"] = this.warranty_periodTextBox.Text.Trim();
            dr["custom_fault"] = this.custom_faultComboBox.Text.Trim();
            dr["guarantee"] = this.guaranteeComboBox.Text.Trim();
            dr["customResponsibility"] = this.customResponsibilityComboBox.Text.Trim();
            dr["lenovo_custom_service_no"] = this.lenovo_custom_service_noTextBox.Text.Trim();
            dr["lenovo_maintenance_no"] = this.lenovo_maintenance_noTextBox.Text.Trim();
            dr["lenovo_repair_no"] = this.lenovo_repair_noTextBox.Text.Trim();
            dr["whole_machine_no"] = this.whole_machine_noTextBox.Text.Trim();
            dr["inputuser"] = this.inputUserTextBox.Text.Trim();

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sda);
            sda.Update(dt);

          
            //清除所有内容
            this.vendorTextBox.Text = "";
            this.productTextBox.Text = "";
            this.source_briefComboBox.Text = "";
            this.storehouseTextBox.Text = "";
            this.custom_orderComboBox.Text = "";
            this.order_out_dateTextBox.Text = "";
            this.order_receive_dateTextBox.Text = "";
            this.custom_machine_typeTextBox.Text = "";
            this.mb_briefTextBox.Text = "";
            this.custommaterialNoTextBox.Text = "";
            this.dpk_statusTextBox.Text = "";
            this.track_serial_noTextBox.Text = "";
            this.custom_serial_noTextBox.Text = "";
            this.vendor_serail_noTextBox.Text = "";
            this.uuidTextBox.Text = "";
            this.macTextBox.Text = "";
            this.mpnTextBox.Text = "";
            this.mb_describeTextBox.Text = "";
            this.mb_make_dateTextBox.Text = "";
            this.warranty_periodTextBox.Text = "";
            this.custom_faultComboBox.Text = "";
            this.guaranteeComboBox.Text = "";
            this.customResponsibilityComboBox.Text = "";
            this.lenovo_custom_service_noTextBox.Text = "";
            this.lenovo_maintenance_noTextBox.Text = "";
            this.lenovo_repair_noTextBox.Text = "";
            this.whole_machine_noTextBox.Text = "";
            this.inputUserTextBox.Text = "";
            this.numTextBox.Text = "";

            loadAdditionInfomation();
            MessageBox.Show("修改完成！");
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

                    cmd.CommandText = "select receivedNum from receiveOrder where orderno = '" + this.custom_orderComboBox.Text
                         + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
                   
                    int receivedNum = 0;
                    string status = "open";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        receivedNum = Int32.Parse(querySdr[0].ToString());                           
                    }
                    querySdr.Close();

                    cmd.CommandText = "update receiveOrder set _status = '" + status + "',receivedNum = '" + (receivedNum - 1) + "' "
                                + "where orderno = '" + this.custom_orderComboBox.Text
                                + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";
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

            this.numTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.vendorTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.productTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.source_briefComboBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.storehouseTextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();

            this.custom_orderComboBox.Items.Clear();
            this.custom_orderComboBox.Items.Add(dataGridView1.SelectedCells[5].Value.ToString());
            this.custom_orderComboBox.SelectedIndex = 0;//选中第一个

            this.order_out_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[6].Value.ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.order_receive_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[7].Value.ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.custom_machine_typeTextBox.Text = dataGridView1.SelectedCells[8].Value.ToString();
            this.mb_briefTextBox.Text = dataGridView1.SelectedCells[9].Value.ToString();
            this.custommaterialNoTextBox.Text = dataGridView1.SelectedCells[10].Value.ToString();
            this.dpk_statusTextBox.Text = dataGridView1.SelectedCells[11].Value.ToString();
            this.track_serial_noTextBox.Text = dataGridView1.SelectedCells[12].Value.ToString();
            this.custom_serial_noTextBox.Text = dataGridView1.SelectedCells[13].Value.ToString();
            this.vendor_serail_noTextBox.Text = dataGridView1.SelectedCells[14].Value.ToString();
            this.uuidTextBox.Text = dataGridView1.SelectedCells[15].Value.ToString();
            this.macTextBox.Text = dataGridView1.SelectedCells[16].Value.ToString();
            this.mpnTextBox.Text = dataGridView1.SelectedCells[17].Value.ToString();

            this.mb_describeTextBox.Text = dataGridView1.SelectedCells[18].Value.ToString();
            this.mb_make_dateTextBox.Text = DateTime.Parse(dataGridView1.SelectedCells[19].Value.ToString()).ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.warranty_periodTextBox.Text = dataGridView1.SelectedCells[20].Value.ToString();
            this.custom_faultComboBox.Text = dataGridView1.SelectedCells[21].Value.ToString();
            this.guaranteeComboBox.Text = dataGridView1.SelectedCells[22].Value.ToString();
            this.customResponsibilityComboBox.Text = dataGridView1.SelectedCells[23].Value.ToString();
            this.lenovo_custom_service_noTextBox.Text = dataGridView1.SelectedCells[24].Value.ToString();
            this.lenovo_maintenance_noTextBox.Text = dataGridView1.SelectedCells[25].Value.ToString();
            this.lenovo_repair_noTextBox.Text = dataGridView1.SelectedCells[26].Value.ToString();
            this.whole_machine_noTextBox.Text = dataGridView1.SelectedCells[27].Value.ToString();

            this.inputUserTextBox.Text = dataGridView1.SelectedCells[28].Value.ToString(); 
        }
        
        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.track_serial_noTextBox.Text.Trim().Length != 16)
                {
                    MessageBox.Show("跟踪条码的长度不是16位!");
                    return;
                }

                if (!Utils.IsNumAndEnCh(this.track_serial_noTextBox.Text))
                {
                    MessageBox.Show("包含非字符与数字的字符，请检查！");
                    return;
                }

                //检查跟踪条码是否在系统中存在过，否则报错
                string vendor = "";
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select vendor from " + this.tableName + " where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    mConn.Close();

                    if (vendor != "")
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        MessageBox.Show("跟踪条码：" + this.track_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (vendor == "")
                {
                    this.custom_serial_noTextBox.Focus();
                    this.custom_serial_noTextBox.SelectAll();
                }
                else 
                {
                    this.track_serial_noTextBox.Focus();
                    this.track_serial_noTextBox.SelectAll();
                }
            }
        }

        private QueryAllInfoExistForm aIO_RMAExportExcel = null; 
        private void showRepairRecordIfExist(String vendor_serial_no)
        {
            if (aIO_RMAExportExcel == null || aIO_RMAExportExcel.IsDisposed)
            {
                aIO_RMAExportExcel = new QueryAllInfoExistForm();
            }
            aIO_RMAExportExcel.resetInfo(vendor_serial_no);
            aIO_RMAExportExcel.BringToFront();
            aIO_RMAExportExcel.Show();
        }

        private void vendor_serail_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.vendorTextBox.Text.Trim() == "宝龙达")//当前vendor的内容可以为空
                {
                    this.uuidTextBox.Focus();
                    this.uuidTextBox.SelectAll();
                    return;
                }

                if (this.vendor_serail_noTextBox.Text.Length != 13)
                {
                    this.vendor_serail_noTextBox.SelectAll();
                    MessageBox.Show("厂商序号的内容长度不是13位，请检查！");
                    return;
                }

                if (!Utils.IsNumAndEnCh(this.vendor_serail_noTextBox.Text))
                {
                    MessageBox.Show("包含非字符与数字的字符，请检查！");
                    return;
                }

                if (this.custom_serial_noTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("请先输入客户序号，本次检查需要客户序号资料！");
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

                    cmd.CommandText = "select vendor from " + this.tableName + " where vendor_serail_no = '" + this.vendor_serail_noTextBox.Text
                        + "' and custom_order = '" + this.custom_orderComboBox.Text + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        vendor = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (vendor != "")
                    {
                        MessageBox.Show("客户序号：" + this.custom_serial_noTextBox.Text + " 已经被使用过，请检测是否有错误!");
                        this.vendor_serail_noTextBox.Focus();
                        this.vendor_serail_noTextBox.SelectAll();
                        return;
                    }

                    cmd.CommandText = "select vendor from " + this.tableName + " where vendor_serail_no = '" + this.vendor_serail_noTextBox.Text+ "'";
                    querySdr = cmd.ExecuteReader();
                    int vendorCount = 0;
                    while (querySdr.Read())
                    {
                        vendorCount++;
                    }
                    querySdr.Close();

                    if (vendorCount != 0)
                    {
                        MessageBox.Show("此厂商序号已经来过【" + vendorCount + "】次，请记录下来");
                    }

                    //查询客户序号来过几次，并查询对应的厂商序号是否相同，包含本次的厂商序号对比
                    cmd.CommandText = "select vendor_serail_no from " + this.tableName + " where custom_serial_no = '" + this.custom_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    HashSet<string> set = new HashSet<string>();//去重集合
                    while (querySdr.Read())
                    {
                        set.Add(querySdr[0].ToString().Trim());
                    }
                    querySdr.Close();
                    set.Add(this.vendor_serail_noTextBox.Text.Trim());

                    if (set.Count > 1)
                    {
                        MessageBox.Show("此客户料号对应的厂商序号（含本次）不一样，请记录下来");
                    }


                    //查询是否有CID记录                    
                    cmd.CommandText = "SELECT DeliveredTable.vendor_serail_no FROM cidRecord INNER JOIN DeliveredTable ON cidRecord.track_serial_no = DeliveredTable.track_serial_no";
                    querySdr = cmd.ExecuteReader();
                    bool cidexit = false;
                    while (querySdr.Read())
                    {
                        if (this.vendor_serail_noTextBox.Text.Trim().Equals(querySdr[0].ToString().Trim()))
                        {
                            cidexit = true;
                            break;
                        }
                    }
                    querySdr.Close();

                    if (cidexit)
                    {
                        MessageBox.Show("此板子之前存在于CID库中，请记录下来");
                    }
                    //end

                    //查询维修记录，如果有则自动调取之前的记录
                    cmd.CommandText = "SELECT Id FROM repair_record_table where vendor_serail_no='"+this.vendor_serail_noTextBox.Text.Trim()+"'";
                    querySdr = cmd.ExecuteReader();                   
                    if(querySdr.HasRows)
                    {
                        showRepairRecordIfExist(this.vendor_serail_noTextBox.Text.Trim());
                    }
                    querySdr.Close();
                    //end

                    //根据数据库的内容，把内容查找如果，如果存在，则保修期为15个月，否则默认
                    cmd.CommandText = "select Id from limit_gurante where MB_COMPAL_SN = '" + this.vendor_serail_noTextBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    bool existVendorSerial = false;
                    if (querySdr.HasRows)
                    {
                        existVendorSerial = true;
                        this.warranty_periodTextBox.Text = "15M";
                    }
                    querySdr.Close();

                    try
                    {
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
                                this.guaranteeComboBox.Text = "保外";
                                this.guaranteeComboBox.Enabled = false;
                                this.customResponsibilityComboBox.Text = "过保";
                                this.customResponsibilityComboBox.Enabled = false;
                                MessageBox.Show((overdays) + " 天超过， 已经过保!");
                            }
                            else
                            {
                                this.guaranteeComboBox.Text = "";
                                this.guaranteeComboBox.Enabled = true;
                                this.customResponsibilityComboBox.Text = "";
                                //this.customResponsibilityComboBox.Enabled = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("客户料号里面的日期规则不对!");
                    }
                    //end 保修期修改

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.uuidTextBox.Focus();
                this.uuidTextBox.SelectAll();
            }
        }

        private void uuidTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.uuidTextBox.Text.Length > 32)
                {
                    string[] temp = this.uuidTextBox.Text.Split(new char[]{' ',';'});
                    foreach (string str in temp)
                    {
                        if (str.Length == 32)
                        {
                            this.uuidTextBox.Text = str;
                            break;
                        }
                    }
                }

                //当uuid为0的时候也可以通过
                int length = this.uuidTextBox.Text.Length;
                
                if (length != 0 && this.uuidTextBox.Text.Length != 32)
                {
                    MessageBox.Show("UUID中的长度不是32位，请检查！");
                    uuidTextBox.Focus();
                    uuidTextBox.SelectAll();
                    return;
                }
                else if (length == 0)
                {
                }

                this.macTextBox.Focus();
                this.macTextBox.SelectAll();
            }
        }

        private void macTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (!Utils.IsNumAndEnCh(this.macTextBox.Text))
                {
                    MessageBox.Show("包含非字符与数字的字符，请检查！");
                    return;
                }

                string mactext = this.macTextBox.Text.Trim();
                mactext = Regex.Replace(mactext, "[^a-zA-Z0-9]", "");
                this.macTextBox.Text = mactext;
                int length = 12;
                if (this.productTextBox.Text.Trim() == "TBG")
                {
                    length = 15;
                }
                if (this.macTextBox.Text.Length != length)
                {
                    this.macTextBox.SelectAll();
                    MessageBox.Show("MAC的长度不是" + length + "位，请检查！");
                    return;
                }

                this.custom_faultComboBox.Focus();
                this.custom_faultComboBox.SelectAll();
            }
        }

        private void lenovo_custom_service_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.lenovo_maintenance_noTextBox.Focus();
                this.lenovo_maintenance_noTextBox.SelectAll();
            }
        }

        private void lenovo_maintenance_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.lenovo_repair_noTextBox.Focus();
                this.lenovo_repair_noTextBox.SelectAll();
            }
        }

        private void lenovo_repair_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.whole_machine_noTextBox.Focus();
                this.whole_machine_noTextBox.SelectAll();
            }
        }

        private void whole_machine_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //this.macTextBox.Focus();
                //this.macTextBox.SelectAll();
            }
        }

        string currentMaterialNo = "";
        private void dataGridViewWaitToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewWaitToReturn.CurrentRow == null)
            {
                return;
            }
            currentMaterialNo = this.custommaterialNoTextBox.Text = dataGridViewWaitToReturn.SelectedCells[1].Value.ToString();
            simulateEnter(this.custommaterialNoTextBox.Text.Trim());
        }

        private void custom_faultComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.custom_faultComboBox.Text != "" && Regex.IsMatch(this.custom_faultComboBox.Text, @"^[+-]?\d*[.]?\d*$"))
                {
                    try
                    {
                        this.custom_faultComboBox.Text = myDictionary[this.custom_faultComboBox.Text.Trim()];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("故障代码" + this.custom_faultComboBox.Text.Trim() + "不存在");
                    }
                }
            }
        }

        private void guaranteeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (this.guaranteeComboBox.Text.Trim() == "保内")
            //{
            //    customResponsibilityComboBox.Text = "";
            //    customResponsibilityComboBox.Enabled = false;
            //}
            //else 
            //{
            //    customResponsibilityComboBox.Enabled = true;
            //}
            customResponsibilityComboBox.Enabled = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
