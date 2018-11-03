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
    public partial class ReturnStoreForm : Form
    {
        string tableName = "returnStore";

        private string vendorStr = "";
        private string productStr = "";
        private string storehousestr = "";

        //Dictionary<string, string> myDictionary = new Dictionary<string, string>();

        public ReturnStoreForm()
        {
            InitializeComponent();
            loadToReturnInformation();
            this.inputUserTextBox.Text = LoginForm.currentUser;

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
                cmd.CommandText = "select distinct vendor from receiveOrder";
                cmd.CommandType = CommandType.Text;
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

                cmd.CommandText = "select distinct product from receiveOrder";
                this.productComboBox.Items.Clear();
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

                cmd.CommandText = "select distinct storehouse from receiveOrder";
                querySdr = cmd.ExecuteReader();
                this.storehouseTextBox.Items.Clear();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.storehouseTextBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct _type from customResponsibilityType";
                querySdr = cmd.ExecuteReader();
                this.custom_res_typeComboBox.Items.Clear();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.custom_res_typeComboBox.Items.Add(temp);
                        //myDictionary.Add(temp, temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct _status from returnStoreStatus";
                querySdr = cmd.ExecuteReader();
                this.statusComboBox.Items.Clear();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.statusComboBox.Items.Add(temp);
                    }
                }
                querySdr.Close();

                cmd.CommandText = "select distinct responsibility_describe from customResponsibility";
                querySdr = cmd.ExecuteReader();
                this.response_describeComboBox.Items.Clear();
                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.response_describeComboBox.Items.Add(temp);
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

         private void storehouseTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.storehousestr = this.storehouseTextBox.Text;
            doQueryAfterSelection();
        }

        private void doQueryAfterSelection()
        {
            if (this.vendorStr == "" || this.productStr == "" )
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

                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,receivedNum,returnNum,ordertime from receiveOrder where vendor='" + vendorStr
                + "' and product ='" + productStr + "' and _status = 'close' and storehouse like '" + this.storehousestr + "'"; 

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
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

        List<string> replaceablempn = new List<string>();

        private void simulateEnter(string custommaterialNo,string orderNo, string tat)
        {
            try
            {
                this.return_file_noTextBox.Text = generateFileNo();
                this.ordernoTextBox.Text = orderNo;// dataGridViewToReturn.SelectedCells[0].Value.ToString();
                this.custommaterialNoTextBox.Text = custommaterialNo;// dataGridViewToReturn.SelectedCells[1].Value.ToString();
                // this.storehouseTextBox.Text = dataGridViewToReturn.SelectedCells[2].Value.ToString();
                this.return_dateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

                if (Utils.isTimeError(this.return_dateTextBox.Text.Trim()))
                {
                    this.returnStore.Enabled = false;
                }

                this.tatTextBox.Text = tat;// dataGridViewToReturn.SelectedCells[5].Value.ToString();


                //根据输入的客户料号，查询MB物料对照表找到dpk状态与mpn
                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    //cmd.CommandText = "select storehouse from receiveOrder where vendor='" + vendorStr
                    //+ "' and product ='" + productStr + "' and _status = 'close'";

                    //SqlDataReader querySdr = cmd.ExecuteReader();
                    //while (querySdr.Read())
                    //{
                    //    this.storehouseTextBox.Text = querySdr[0].ToString();
                    //    break;
                    //}
                    //querySdr.Close();

                    cmd.CommandText = "select dpk_type, mpn, replace_custom_materialNo,replace_mpn from MBMaterialCompare where custommaterialNo = '"
                        + this.custommaterialNoTextBox.Text.Trim() + "'";

                    replaceablempn.Clear();
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        this.dpkpnTextBox.Text = querySdr[0].ToString();
                        this.bommpnTextBox.Text = querySdr[1].ToString();
                        this.replace_custom_materialNotextBox.Text = querySdr[2].ToString();

                        if (querySdr[2].ToString().Trim() != "")
                        {
                            replaceablempn.Add(querySdr[2].ToString());
                        }

                        if (querySdr[3].ToString().Trim() != "")
                        {
                            replaceablempn.Add(querySdr[3].ToString());
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
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void dataGridViewToReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewToReturn.CurrentRow == null)
            {
                return;
            }
            this.matertiallibMpnTextBox.Text = "";
            this.track_serial_noTextBox.Text = "";
            this.custom_serial_noTextBox.Text = "";

            currentMaterialNo = this.custommaterialNoTextBox.Text = dataGridViewToReturn.SelectedCells[1].Value.ToString();
            orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.SelectedCells[0].Value.ToString();
            tat = this.tatTextBox.Text = dataGridViewToReturn.SelectedCells[5].Value.ToString();
            simulateEnter(this.custommaterialNoTextBox.Text.Trim(), orderNo, tat);
        }

        private string generateFileNo()
        {
            string retStr = "";
            string preStr = this.vendorComboBox.Text.Trim() + this.productComboBox.Text.Trim() + DateTime.Now.ToString("yyMMdd");

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select distinct return_file_no from returnStore where vendor ='" + this.vendorComboBox.Text.Trim() + "' and product ='" + this.productComboBox.Text.Trim() + "'";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string subRetStr= "";
                while (querySdr.Read())
                {                    
                    string queryStr = querySdr[0].ToString();
                    if (queryStr != "")
                    {
                        subRetStr = queryStr.Substring(preStr.Length, 2);
                    }
                }
                querySdr.Close();

                if (subRetStr == "")
                {
                    retStr = preStr + "01";
                }
                else
                {
                    int last = Int32.Parse(subRetStr);

                    if (checkBoxMakeNew.Checked)
                    {
                        last += 1;
                    }

                    if (last < 10)
                    {
                        retStr = preStr + "0" + last;
                    }
                    else
                    {
                        retStr = preStr + last;
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return retStr;
        }

        private void returnStore_Click(object sender, EventArgs e)
        {
            if (checkIsNull())
            {
                MessageBox.Show("输入的内容为空, 请检查！");
                return;                
            }

            if (this.matertiallibMpnTextBox.Text.Trim() != this.bommpnTextBox.Text.Trim())
            {
                if (replaceablempn.Contains(this.matertiallibMpnTextBox.Text.Trim()) == false)
                {
                    MessageBox.Show("收获表中的mpn与物料对照表的所有mpn不一致，请检查！");
                    this.track_serial_noTextBox.Text = "";
                    this.track_serial_noTextBox.Focus();
                    this.custom_serial_noTextBox.Text = "";
                    return;
                }               
            }

            if (statusComboBox.Text.Trim() == "不良品")
            {
                if (this.lenovo_maintenance_noTextBox.Text == "" || this.lenovo_repair_noTextBox.Text == "")
                {
                    MessageBox.Show("联想的相关信息不能为空！");
                    return;
                }
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
                    if (statusComboBox.Text.Trim() == "不良品")
                    {
                        cmd.CommandText = "select Id from cidRecord where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                        querySdr = cmd.ExecuteReader();
                        string isExist = "";
                        while (querySdr.Read())
                        {
                            isExist = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (isExist == "")
                        {
                            MessageBox.Show("不良品的序列号不在CID库中，请检查！");
                            conn.Close();
                            return;
                        }
                    }

                    //在更新收货表的同时，需要同时更新导入的表格收货数量，不然数据会乱掉
                    cmd.CommandText = "select _status, ordernum, receivedNum, returnNum,cid_number from receiveOrder where orderno = '" + this.ordernoTextBox.Text
                           + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

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
                        cmd.CommandText = "update receiveOrder set _status = '" + status + "',returnNum = '" + (returnNum + 1) +"' "
                                    + "where orderno = '" + this.ordernoTextBox.Text
                                    + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text + "'";

                        cmd.ExecuteNonQuery();

                        string returnOrderIndex = this.ordernoTextBox.Text.Trim() + this.custommaterialNoTextBox.Text.Trim() + ((returnNum + 1));
                        cmd.CommandText = "INSERT INTO " + tableName + " VALUES('" +
                        this.vendorComboBox.Text.Trim() + "','" +
                        this.productComboBox.Text.Trim() + "','" +
                        this.return_file_noTextBox.Text.Trim() + "','" +
                        this.storehouseTextBox.Text.Trim() + "','" +
                        this.return_dateTextBox.Text.Trim() + "','" +
                        this.ordernoTextBox.Text.Trim() + "','" +
                        this.custommaterialNoTextBox.Text.Trim() + "','" +
                        this.dpkpnTextBox.Text.Trim() + "','" +
                        this.track_serial_noTextBox.Text.Trim() + "','" +
                        this.custom_serial_noTextBox.Text.Trim() + "','" +
                        this.vendor_serail_noTextBox.Text.Trim() + "','" +
                        this.bommpnTextBox.Text.Trim() + "','" +
                        this.statusComboBox.Text.Trim() + "','" +
                        this.custom_res_typeComboBox.Text.Trim() + "','" +
                        this.response_describeComboBox.Text.Trim() + "','" +
                        this.tatTextBox.Text.Trim() + "','" +
                        this.inputUserTextBox.Text.Trim() + "','" +
                        this.lenovo_maintenance_noTextBox.Text.Trim() + "','" +
                        this.lenovo_repair_noTextBox.Text.Trim() + "','" +
                        returnOrderIndex +
                        "')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update stationInformation set station = 'return', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                             + "where track_serial_no = '" + this.track_serial_noTextBox.Text + "'";
                        cmd.ExecuteNonQuery();
                        
                        //dataGridViewToReturn里面的数据要更新
                        doQueryAfterSelection();
                        clearInputData();

                        if (status == "return")
                        {
                            custommaterialNoTextBox.Text = "";
                            dpkpnTextBox.Text = "";
                            bommpnTextBox.Text = "";
                            storehouseTextBox.Text = "";
                            ordernoTextBox.Text = "";
                            tatTextBox.Text = "";
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
            this.track_serial_noTextBox.Focus();
            queryLastest(true);
        }

        private bool checkIsNull()
        {
            if (custommaterialNoTextBox.Text.Trim() == ""
               // || this.replace_custom_materialNotextBox.Text.Trim() == ""
                || this.track_serial_noTextBox.Text.Trim() == ""
                || this.custom_serial_noTextBox.Text.Trim() == ""
                || this.statusComboBox.Text.Trim() == ""
                || this.dpkpnTextBox.Text.Trim() == ""
                || this.matertiallibMpnTextBox.Text.Trim() == ""
                || this.vendor_serail_noTextBox.Text.Trim() == ""
                || this.bommpnTextBox.Text.Trim() == ""
                || this.storehouseTextBox.Text.Trim() == ""
                || this.return_dateTextBox.Text.Trim() == ""
                || this.ordernoTextBox.Text.Trim() == ""
                || this.tatTextBox.Text.Trim() == "")
            {
                return true;  
            }

            if (custom_res_typeComboBox.Enabled &&  response_describeComboBox.Enabled)
            {
                if (this.custom_res_typeComboBox.Text.Trim() == ""
                    || this.response_describeComboBox.Text.Trim() == "")
                {
                    return true;
                }
            }
               
            return false;           
        }

        private void clearInputData()
        {
            //custommaterialNoTextBox.Text = "";
            //dpkpnTextBox.Text = "";
            //bommpnTextBox.Text = "";
            //storehouseTextBox.Text = "";
            //ordernoTextBox.Text = "";
            //tatTextBox.Text = "";

            replace_custom_materialNotextBox.Text = "";
            track_serial_noTextBox.Text = "";

            custom_serial_noTextBox.Text = "";
            custom_res_typeComboBox.SelectedIndex = -1;
            response_describeComboBox.SelectedIndex = -1;
         
            vendor_serail_noTextBox.Text = "";
            return_dateTextBox.Text = "";
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
                    cmd.CommandText = "select top 3 Id,vendor,product,return_file_no,storehouse,return_date,orderno,custommaterialNo,dpkpn,"+
                        "track_serial_no,custom_serial_no,vendor_serail_no,vendormaterialNo,_status,custom_res_type,response_describe,tat,"+
                        "inputuser,lenovo_maintenance_no,lenovo_repair_no from " + tableName + " order by id desc";
                }
                else
                {
                    cmd.CommandText = "select top 20 Id,vendor,product,return_file_no,storehouse,return_date,orderno,custommaterialNo,dpkpn," +
                       "track_serial_no,custom_serial_no,vendor_serail_no,vendormaterialNo,_status,custom_res_type,response_describe,tat," +
                       "inputuser,lenovo_maintenance_no,lenovo_repair_no from " + tableName + " order by id desc";
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

            string[] hTxt = { "ID", "厂商","客户别","还货文件编号","客户库别","还货时间","订单编号","客户料号","DPK状态",
                                "跟踪条码","客户序号","厂商序号","厂商料号","状态","客责类别","客责描述","TAT","还货人"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridViewReturnedDetail.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            queryLastest(false);
        }

        private void modify_Click(object sender, EventArgs e)
        {
           
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

            tableLayoutPanel5.GetType().
                GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
                SetValue(tableLayoutPanel5, true, null);
        }

        private void track_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {                
                //跟踪条码可以为空，如果为空则代表本板子是通过库房拿过来替换的，原来的板子没有维修好
                if (track_serial_noTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("跟踪条码为空!");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string returnExist = "";
                    while (querySdr.Read())
                    {
                        returnExist = querySdr[0].ToString();
                    }
                    querySdr.Close();
                    if (returnExist != "")
                    {
                        MessageBox.Show("此序列号已经还货了，请检查！");
                        this.returnStore.Enabled = false;
                        mConn.Close();
                        return;
                    }

                    cmd.CommandText = "select 跟踪条码,仁宝料号,客户序号,仁宝序号,MB11S from CSD_old_data where 跟踪条码 = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    bool existOldDatabase = false;
                    string renbaoliaohao = "";
                    string renbao_mpn = "", renbao_custom_serial_no = "", renbao_vendor_serial_no = "";
                    while (querySdr.Read())
                    {
                        existOldDatabase = true;
                        returnExist = querySdr[0].ToString();
                        renbaoliaohao = querySdr[1].ToString();
                        renbao_mpn = querySdr[1].ToString();
                        renbao_custom_serial_no = querySdr[4].ToString();
                        renbao_vendor_serial_no = querySdr[3].ToString();
                        break;
                    }
                    querySdr.Close();

                    if (existOldDatabase)
                    {
                        if (custommaterialNoTextBox.Text.Trim() == "")
                        {
                            MessageBox.Show("请先选择料号");
                            this.returnStore.Enabled = false;
                            mConn.Close();
                            return;
                        }
                        //先查询原来的料号的内容，根据客户料号查询一些列mpn
                        List<string> mpnList = new List<string>();
                        cmd.CommandText = "select mpn,replace_mpn,replace_custom_materialNo  from MBMaterialCompare where custommaterialNo like '%" + custommaterialNoTextBox.Text.Trim() + "%'";
                        querySdr = cmd.ExecuteReader();
                        string replace_mpn = "", replace_custom_materialNo = "";
                        while (querySdr.Read())
                        {
                            mpnList.Add(querySdr[0].ToString());
                            replace_mpn = querySdr[1].ToString().Trim();
                            replace_custom_materialNo = querySdr[2].ToString().Trim() ;
                        }
                        querySdr.Close();

                        if (mpnList.Contains(renbaoliaohao) == false && replace_mpn!=renbaoliaohao && replace_custom_materialNo != renbaoliaohao)
                        {
                            MessageBox.Show("归还的板子与选择的料号不符合");
                            this.returnStore.Enabled = false;
                            mConn.Close();
                            return;
                        }
                        else
                        {
                            this.matertiallibMpnTextBox.Text = renbao_mpn;
                            this.custom_serial_noTextBox.Text = renbao_custom_serial_no;
                            this.vendor_serail_noTextBox.Text = renbao_vendor_serial_no;
                        }
                    }
                    else
                    {
                        //先检查CID，如果在cid存在，则跳过站别检查
                        cmd.CommandText = "select Id from cidRecord where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        string cidExist = "";
                        while (querySdr.Read())
                        {
                            cidExist = querySdr[0].ToString();
                        }
                        querySdr.Close();
                      
                        if (cidExist == "")
                        {
                            //增加站别检查，如果没有经过最后一站，则认为此板子有问题，不能归还 
                            //cmd.CommandText = "select track_serial_no from outlookcheck where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                            //暂时从Test2走
                            string currentUsedTable = "test2table";
                            if (this.productComboBox.Text == "DT" || productComboBox.Text == "AIO" || productComboBox.Text == "TBG")
                            {
                                currentUsedTable = "testalltable";
                            }
                            cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();
                            string station = "";
                            while (querySdr.Read())
                            {
                                station = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            if (station != "Test2" && station != "Test1&2")
                            {
                                MessageBox.Show("此单没有经过Test2检查站别！");
                                mConn.Close();
                                return;
                            }
                        }
                        else
                        {
                            statusComboBox.Text = "不良品";
                        }

                        cmd.CommandText = "select custom_serial_no, vendor_serail_no,mpn ,lenovo_maintenance_no,lenovo_repair_no from DeliveredTable where track_serial_no = '"
                            + this.track_serial_noTextBox.Text + "'";

                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            this.custom_serial_noTextBox.Text = querySdr[0].ToString();
                            this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                            this.matertiallibMpnTextBox.Text = querySdr[2].ToString();
                            this.lenovo_maintenance_noTextBox.Text = querySdr[3].ToString();
                            this.lenovo_repair_noTextBox.Text = querySdr[4].ToString();
                        }
                        querySdr.Close();

                        if (this.custom_serial_noTextBox.Text == "")//说明板子从buffer库出来的
                        {
                            cmd.CommandText = "select custom_serial_no, vendor_serial_no,mpn from mb_out_stock where track_serial_no = '"
                          + this.track_serial_noTextBox.Text + "'";

                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                this.custom_serial_noTextBox.Text = querySdr[0].ToString();
                                this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                                this.matertiallibMpnTextBox.Text = querySdr[2].ToString();
                                this.lenovo_maintenance_noTextBox.Text = "";
                                this.lenovo_repair_noTextBox.Text = "";
                            }
                            querySdr.Close();
                        }
                        mConn.Close();

                        //从历史数据查询是否匹配
                        if (this.custom_serial_noTextBox.Text == "")
                        {
                            MessageBox.Show("客户序号不能为空，严重，检查！");
                        }

                        if (this.productComboBox.Text != "TBG" && this.productComboBox.Text != "DT")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                        {
                            //需要去掉前面的非0字段
                            string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');
                            string replacedCustomSerial = this.replace_custom_materialNotextBox.Text.TrimStart('0');

                            if (this.custom_serial_noTextBox.Text.ToLower().Contains(customSerial.ToLower()))
                            {
                            }
                            else
                            {
                                if (this.custom_serial_noTextBox.Text.ToLower().Contains(replacedCustomSerial.ToLower()))
                                {
                                }
                                else
                                {
                                    MessageBox.Show("在" + this.productComboBox.Text + "下客户序号没有包含客户料号, 请检查追踪条码是否正确");
                                    this.track_serial_noTextBox.Focus();
                                    this.track_serial_noTextBox.SelectAll();

                                    this.custom_serial_noTextBox.Text = "";
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.custom_serial_noTextBox.Focus();
                this.custom_serial_noTextBox.SelectAll();
            }
        }

        string currentMaterialNo, orderNo, tat;
        private void custommaterialNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                int row = dataGridViewToReturn.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    dataGridViewToReturn.Rows[i].Selected = false;
                }

                int count = 0;
                currentMaterialNo = this.custommaterialNoTextBox.Text.Trim();
                for (int i = 0; i < row; i++)
                {
                    string queryedStr = dataGridViewToReturn.Rows[i].Cells[1].Value.ToString();
                    if (queryedStr.EndsWith(currentMaterialNo) 
                        && this.ordernoTextBox.Text.Trim() == dataGridViewToReturn.Rows[i].Cells[0].Value.ToString().Trim())
                    {
                        count++;
                        this.custommaterialNoTextBox.Text = queryedStr;
                        orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.Rows[i].Cells[0].Value.ToString();
                        tat = this.tatTextBox.Text = dataGridViewToReturn.Rows[i].Cells[5].Value.ToString();

                        dataGridViewToReturn.Rows[i].Selected = true;
                        dataGridViewToReturn.CurrentCell = dataGridViewToReturn.Rows[i].Cells[0];
                        break;
                    }
                }
                if (count > 1 || count == 0)
                {
                    this.custommaterialNoTextBox.Text = this.currentMaterialNo = "";
                    MessageBox.Show("你输入的不存在或者不唯一，请重新输入！");

                    for (int i = 0; i < row; i++)
                    {
                        dataGridViewToReturn.Rows[i].Selected = false;
                    }
                }
                else
                {
                    this.currentMaterialNo = this.custommaterialNoTextBox.Text;
                    simulateEnter(this.currentMaterialNo, orderNo, tat);

                    this.track_serial_noTextBox.Focus();
                    this.track_serial_noTextBox.SelectAll();
                }
            }
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.response_describeComboBox.Focus();

                if (track_serial_noTextBox.Text.Trim() == "")
                {
                    if (this.productComboBox.Text != "TBG" && this.productComboBox.Text != "DT")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                    {
                        //需要去掉前面的非0字段
                        string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');
                        string replacedCustomSerial = this.replace_custom_materialNotextBox.Text.TrimStart('0');

                        if (this.custom_serial_noTextBox.Text.ToLower().Contains(customSerial.ToLower()) == false
                            || this.custom_serial_noTextBox.Text.ToLower().Contains(replacedCustomSerial.ToLower()))
                        {
                            this.track_serial_noTextBox.Focus();
                            this.track_serial_noTextBox.SelectAll();

                            this.custom_serial_noTextBox.Text = "";

                            MessageBox.Show("在" + this.productComboBox.Text + "下客户序号没有包含客户料号, 请检查追踪条码是否正确");
                            return;
                        }
                    }

                    MessageBox.Show("需要查询库存表，然后调出厂商序号! todo");
                }
            }
        }

        private void statusComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.statusComboBox.Text == "良品")
            {
                custom_res_typeComboBox.Enabled = false;
                response_describeComboBox.Enabled = false;
            }
            else if (this.statusComboBox.Text == "不良品")
            {
                custom_res_typeComboBox.Enabled = true;
                response_describeComboBox.Enabled = true;
            }
        }

        private void custom_res_typeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == System.Convert.ToChar(13))
            //{
            //    try
            //    {
            //        //if (myDictionary[this.custom_res_typeComboBox.Text.Trim()] == "11")
            //        {
            //            this.custom_res_typeComboBox.Text = myDictionary[this.custom_res_typeComboBox.Text.Trim()];
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("故障代码" + this.custom_res_typeComboBox.Text.Trim() + "不存在");
            //    }
            //}
        }

        private void dataGridViewToReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void print_Click(object sender, EventArgs e)
        {
            if (this.custommaterialNoTextBox.Text == "")
            {
                MessageBox.Show("客户料号为空!");
                return;
            }
            PrintUtils.printCustomMaterialNo(this.custommaterialNoTextBox.Text);
        }

       
    }
}
