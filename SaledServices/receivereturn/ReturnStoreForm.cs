﻿using System;
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
            doQueryAfterSelection(true);
        }

        private void productComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.productStr = this.productComboBox.Text;
            doQueryAfterSelection(true);
        }

         private void storehouseTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.storehousestr = this.storehouseTextBox.Text;
            doQueryAfterSelection(true);
        }

        Dictionary<string, string> tatMap = new Dictionary<string, string>();
        private void doQueryAfterSelection(Boolean isUpdateTAT)
        {
            if (this.vendorStr == "" || this.productStr == "" || this.storehousestr == "")
            {
                return;
            }
            
            try
            {
                dataGridViewToReturn.DataSource = null;
                dataGridViewToReturn.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                checkTime(122);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select orderno, custom_materialNo,mb_brief,receivedNum,returnNum,ordertime,DATEDIFF( Day, ordertime, GETDATE()) from receiveOrder where vendor='" + vendorStr
                + "' and product ='" + productStr + "' and _status = 'close' and storehouse like '" + this.storehousestr + "'"; 

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "receiveOrder");
                dataGridViewToReturn.DataSource = ds.Tables[0];
                dataGridViewToReturn.RowHeadersVisible = false;
                checkTime(123);
                string[] hTxt = { "订单编号", "客户料号", "MB简称", "收货数量", "还货数量", "制单时间", "TAT" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridViewToReturn.Columns[i].HeaderText = hTxt[i];
                    dataGridViewToReturn.Columns[i].Name = hTxt[i];
                }
                /*
                checkTime(124);
                DataGridViewColumn dc = new DataGridViewColumn();
                dc.Name = "TAT";
                //dc.DataPropertyName = "FID";

                dc.Visible = true;
                // dc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dc.HeaderText = "TAT";
                dc.CellTemplate = new DataGridViewTextBoxCell();
                int columnIndex = dataGridViewToReturn.Columns.Add(dc);
                checkTime(125);
                
                if (tatMap.Count == 0) {
                    checkTime(dataGridViewToReturn.Rows.Count);
                    foreach (DataGridViewRow dr in dataGridViewToReturn.Rows)
                    {
                        try
                        {
                            checkTime(12500);
                            DateTime dt1 = Convert.ToDateTime(dr.Cells["制单时间"].Value.ToString());
                            DateTime dt2 = DateTime.Now;

                            TimeSpan ts = dt2.Subtract(dt1);
                            int overdays = ts.Days;
                            checkTime(12501);
                            dr.Cells["TAT"].Value = overdays + " ";
                            checkTime(12502);
                            string val1= dr.Cells["订单编号"].Value.ToString();
                            string val2= dr.Cells["客户料号"].Value.ToString();
                            string val3= dr.Cells["MB简称"].Value.ToString();
                                
                            tatMap.Add(val1+val2+val3, dr.Cells["TAT"].Value.ToString());
                            checkTime(12503);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    checkTime(1250);
                }else{
                    checkTime(dataGridViewToReturn.Rows.Count);
                    foreach (DataGridViewRow dr in dataGridViewToReturn.Rows)
                    {
                        checkTime(125101);
                            string val1= dr.Cells["订单编号"].Value.ToString();
                            string val2= dr.Cells["客户料号"].Value.ToString();
                            string val3= dr.Cells["MB简称"].Value.ToString();
                            checkTime(125102);
                            dr.Cells["TAT"].Value = tatMap[val1+val2+val3];
                            checkTime(12511);

                     }
                     checkTime(1251);
                }
                checkTime(126);
                 * */
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
            //this.matertiallibMpnTextBox.Text = "";
           // this.track_serial_noTextBox.Text = "";
            this.custom_serial_noTextBox.Text = "";

            currentMaterialNo = this.custommaterialNoTextBox.Text = dataGridViewToReturn.SelectedCells[1].Value.ToString();
            orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.SelectedCells[0].Value.ToString();
            tat = this.tatTextBox.Text = dataGridViewToReturn.SelectedCells[6].Value.ToString();
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

        private void checkTime(int count)
        {
            //Console.WriteLine(count+"'"+DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        }

        private void returnStore_Click(object sender, EventArgs e)
        {
            if (checkIsNull())
            {
                MessageBox.Show("输入的内容为空, 请检查！");
                return;                
            }

            checkTime(0);
            if (checkCustomSerialNo() == false)
            {
                return;
            }

            if (this.matertiallibMpnTextBox.Text.Trim() != this.bommpnTextBox.Text.Trim())
            {
                if (this.track_serial_noTextBox.Text.Trim().StartsWith("RMUP") == false)//RMUP的板子不检查mpn
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
            }
            checkTime(1);
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
                    checkTime(2);
                    //判断obe是否通过
                    if (statusComboBox.Text.Trim() != "不良品")
                    {
                        cmd.CommandText = "select ischeck from decideOBEchecktable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and ischeck='True'";

                        querySdr = cmd.ExecuteReader();
                        string ischeck = "";

                        while (querySdr.Read())
                        {
                            ischeck = querySdr[0].ToString();
                        }
                        querySdr.Close();
                        if (ischeck == "True")
                        {
                            cmd.CommandText = "select checkresult from ObeStationtable where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";

                            querySdr = cmd.ExecuteReader();
                            string checkresult = "";

                            while (querySdr.Read())
                            {
                                checkresult = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            if (checkresult == "" || checkresult != "P")
                            {
                                MessageBox.Show("追踪条码的内容在OBE站别中，没有检查结果！");
                                conn.Close();
                                return;
                            }
                        }
                    }

                    //end 判断obe
                    checkTime(3);
                    //加入判断8s的跟fru或fru的替换料的包含关系
                    cmd.CommandText = "select fruNo, replace_fruNo  from MBMaterialCompare where  custommaterialNo = '" + this.custommaterialNoTextBox.Text + "'";
                    bool existfru = false;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string fru = querySdr[0].ToString();
                        string replacefru = querySdr[1].ToString();
                        if (fru != null && fru != "" && this.custom_serial_noTextBox.Text.Trim().Contains(fru))
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
                    checkTime(4);
                    if (!existfru)
                    {
                        this.track_serial_noTextBox.Focus();
                        this.track_serial_noTextBox.SelectAll();
                        querySdr.Close();
                        conn.Close();
                        MessageBox.Show("8s条码不包含fru与替换fru的内容!");
                        return;
                    }
                    checkTime(5);
                    //在插入之前再考虑一下是否已经锁定，否则不能还货
                    cmd.CommandText = "select isLock from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();

                    if (querySdr.HasRows)
                    {
                        querySdr.Read();
                        string result = querySdr[0].ToString().Trim();
                        if (result == "true")
                        {
                            MessageBox.Show("此序列号已经锁定，不能走下面的流程！");
                            querySdr.Close();
                            conn.Close();
                            this.returnStore.Enabled = false;
                            return;
                        }
                    }
                    querySdr.Close();
                    checkTime(6);
                    //根据料号查询物料对照表的厂商与客户别，看是否与选择的厂商与客户别对应，否则报错
                    cmd.CommandText = "select top 1 vendor,product from MBMaterialCompare where custommaterialNo = '"
                       + this.custommaterialNoTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    string vendortemp="", producttemp = "";
                    while (querySdr.Read())
                    {
                        vendortemp = querySdr[0].ToString();
                        producttemp = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    if (this.vendorComboBox.Text.Trim() != vendortemp || this.productComboBox.Text.Trim() != producttemp)
                    {
                        MessageBox.Show("物料对照表中的厂商与客户别跟选择的厂商与客户别不相同，请检查！");
                        conn.Close();
                        return;
                    }
                    checkTime(7);
                    cmd.CommandText = "select _8sCode from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and isLock='true'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        MessageBox.Show("此序列号已经锁定，不能走下面的流程！");
                        querySdr.Close();
                        conn.Close();
                        this.returnStore.Enabled = false;
                        return;
                    }
                    this.returnStore.Enabled = true;
                    querySdr.Close();
                    checkTime(8);
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
                    checkTime(9);
                    if (isDone == false)
                    {
                        cmd.CommandText = "update receiveOrder set _status = '" + status + "',returnNum = '" + (returnNum + 1) +"' "
                                    + "where orderno = '" + this.ordernoTextBox.Text.Trim()
                                    + "' and custom_materialNo = '" + this.custommaterialNoTextBox.Text.Trim() + "'";

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
                        checkTime(10);
                        cmd.CommandText = "update stationInformation set station = 'return', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "' "
                             + "where track_serial_no = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.track_serial_noTextBox.Text.Trim() +
            "','还货','" + this.inputUserTextBox.Text.Trim() + "',GETDATE())";
                        cmd.ExecuteNonQuery();
                        checkTime(11);
                        //dataGridViewToReturn里面的数据要更新
                        doQueryAfterSelection(false);
                        checkTime(12);
                        clearInputData();
                        checkTime(13);
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
                checkTime(14);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.track_serial_noTextBox.Focus();
            queryLastest(true);
            checkTime(15);
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

                    cmd.CommandText = "select isLock from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "' and locktype='mb_receive_check'";
                    querySdr = cmd.ExecuteReader();

                    if (querySdr.HasRows)
                    {
                        querySdr.Read();
                        string result = querySdr[0].ToString().Trim();
                        if (result == "true")
                        {
                            MessageBox.Show("此序号被拦截已经锁定，不能走下面的流程，请找管理员解锁！");
                            querySdr.Close();
                            mConn.Close();
                            this.returnStore.Enabled = false;
                            return;
                        }
                    }
                    querySdr.Close();

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
                            //if (station != "Test2" && station != "Test1&2")
                            //{
                            //    MessageBox.Show("此单没有经过Test2检查站别！");
                            //    mConn.Close();
                            //    return;
                            //}

                            if (station != "外观" && station != "Obe")//obe是可选站别
                            {
                                MessageBox.Show("此单没有经过外观检查站别！");
                                mConn.Close();
                                return;
                            }
                        }
                        else
                        {
                            statusComboBox.Text = "不良品";
                        }

                        cmd.CommandText = "select custom_serial_no, vendor_serail_no,mpn ,lenovo_maintenance_no,lenovo_repair_no,custommaterialNo from DeliveredTable where track_serial_no = '"
                            + this.track_serial_noTextBox.Text + "'";
                        bool exist = false;
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            exist = true;
                            //this.custom_serial_noTextBox.Text = querySdr[0].ToString();改成手动输入，后续验证8s，防止放入错误的8s码
                            this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                            this.matertiallibMpnTextBox.Text = querySdr[2].ToString();
                            this.lenovo_maintenance_noTextBox.Text = querySdr[3].ToString();
                            this.lenovo_repair_noTextBox.Text = querySdr[4].ToString();
                            this.currentMaterialNo = querySdr[5].ToString();
                        }
                        querySdr.Close();

                        if (exist == false)//从替换表里查询
                        {
                            cmd.CommandText = "select custom_serial_no, vendor_serail_no,mpn ,lenovo_maintenance_no,lenovo_repair_no,custommaterialNo from DeliveredTableTransfer where track_serial_no_transfer = '"
                            + this.track_serial_noTextBox.Text + "'";                          
                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                exist = true;
                               // this.custom_serial_noTextBox.Text = querySdr[0].ToString();
                                this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                                this.matertiallibMpnTextBox.Text = querySdr[2].ToString();
                                this.lenovo_maintenance_noTextBox.Text = querySdr[3].ToString();
                                this.lenovo_repair_noTextBox.Text = querySdr[4].ToString();
                                this.currentMaterialNo = querySdr[5].ToString();
                            }
                            querySdr.Close();
                        }

                        if (this.vendor_serail_noTextBox.Text == "")//说明板子从buffer库出来的
                        {
                            cmd.CommandText = "select custom_serial_no, vendor_serial_no,mpn from mb_out_stock where track_serial_no = '"
                          + this.track_serial_noTextBox.Text + "'";

                            querySdr = cmd.ExecuteReader();
                            while (querySdr.Read())
                            {
                                //this.custom_serial_noTextBox.Text = querySdr[0].ToString();
                                this.vendor_serail_noTextBox.Text = querySdr[1].ToString();
                                this.matertiallibMpnTextBox.Text = querySdr[2].ToString();
                                this.lenovo_maintenance_noTextBox.Text = "";
                                this.lenovo_repair_noTextBox.Text = "";
                            }
                            querySdr.Close();
                        }

                        int row = dataGridViewToReturn.Rows.Count;
                        for (int i = 0; i < row; i++)
                        {
                            dataGridViewToReturn.Rows[i].Selected = false;
                        }

                        //开始查询内容
                        //首先查找tat最大的
                        int tatTemp = -1;
                        for (int i = 0; i < row; i++)
                        {
                            string queryedStr = dataGridViewToReturn.Rows[i].Cells[1].Value.ToString();
                            if (queryedStr.EndsWith(currentMaterialNo))
                            {
                               this.tatTextBox.Text = dataGridViewToReturn.Rows[i].Cells[6].Value.ToString().Trim();
                               if (Int16.Parse(this.tatTextBox.Text) > tatTemp)
                                {
                                    tatTemp = Int16.Parse(this.tatTextBox.Text);
                                    tat = this.tatTextBox.Text.Trim();
                                    orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.Rows[i].Cells[0].Value.ToString();
                                }
                            }
                        }

                        for (int i = 0; i < row; i++)
                        {
                            string queryedStr = dataGridViewToReturn.Rows[i].Cells[1].Value.ToString();
                            if (queryedStr.EndsWith(currentMaterialNo)
                                && this.orderNo.Trim() == dataGridViewToReturn.Rows[i].Cells[0].Value.ToString().Trim())
                            {
                                this.custommaterialNoTextBox.Text = queryedStr;
                                orderNo = this.ordernoTextBox.Text = dataGridViewToReturn.Rows[i].Cells[0].Value.ToString();
                                tat = this.tatTextBox.Text = dataGridViewToReturn.Rows[i].Cells[6].Value.ToString();

                                dataGridViewToReturn.Rows[i].Selected = true;
                                dataGridViewToReturn.CurrentCell = dataGridViewToReturn.Rows[i].Cells[0];
                                break;
                            }
                        }
                        simulateEnter(currentMaterialNo, orderNo, tat);

                        //根据来的次数进行锁定，并提示锁定
                        cmd.CommandText = "select vendor from DeliveredTable where vendor_serail_no = '" + this.vendor_serail_noTextBox.Text + "'";
                        querySdr = cmd.ExecuteReader();
                        int count = 0;
                        while (querySdr.Read())
                        {
                            count++;
                        }
                        querySdr.Close();
                        

                        if (count == 2)//这是第二次来
                        {
                           //判断前2次是否都是NTF，如果是则拦下了 锁定

                            //查询是否有2此NTF，如果有进入锁定表格
                            cmd.CommandText = "select _action,orderno,repair_result,repair_date,fault_describe,software_update from repair_record_table where vendor_serail_no ='" + vendor_serail_noTextBox.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();
                            int ntfcount = 0;
                            List<string> orderlist = new List<string>();
                            while (querySdr.Read())
                            {
                                if (querySdr[0].ToString().Trim().ToUpper() == "NTF")
                                {
                                    ntfcount++;

                                    if (!orderlist.Contains(querySdr[1].ToString().Trim()))
                                    {
                                        orderlist.Add(querySdr[1].ToString().Trim());
                                    }
                                }
                            }
                            querySdr.Close();

                            if (ntfcount >= 2 && orderlist.Count >= 2)
                            {
                                cmd.CommandText = "select isLock from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                                querySdr = cmd.ExecuteReader();

                                if (querySdr.HasRows)
                                {
                                    querySdr.Read();
                                    string result = querySdr[0].ToString().Trim();
                                    if (result == "true")
                                    {
                                        MessageBox.Show("此序列号已经锁定，不能走下面的流程！");
                                        querySdr.Close();
                                        mConn.Close();
                                        this.returnStore.Enabled = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    querySdr.Close();

                                    cmd.CommandText = "INSERT INTO need_to_lock VALUES('" +
                                                "ntf_twice" + "','" +
                                            this.track_serial_noTextBox.Text.Trim() + "','" +
                                            this.ordernoTextBox.Text.Trim() + "','" +
                                            this.vendor_serail_noTextBox.Text.Trim() + "','" +//记录厂商序号
                                            "true" + "','" +
                                            DateTime.Now.ToString("yyyy/MM/dd") +
                                            "','')";
                                    cmd.ExecuteNonQuery();

                                    this.returnStore.Enabled = false;
                                    MessageBox.Show("提示：因为超过2个订单的记录有2次NTF，请注意已锁定");
                                }
                            }
                        }
                        else if (count >= 3)
                        {
                            cmd.CommandText = "select isLock from need_to_lock where track_serial_no='" + this.track_serial_noTextBox.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();

                            if (querySdr.HasRows)
                            {
                                querySdr.Read();
                                string result = querySdr[0].ToString().Trim();
                                if(result == "true")
                                {
                                    MessageBox.Show("此序列号已经锁定，不能走下面的流程！");
                                    querySdr.Close();
                                    mConn.Close();
                                    this.returnStore.Enabled = false;
                                    return;
                                }
                            }
                            else 
                            { 
                                querySdr.Close();

                                //这次是第三次过来，不管前面记录，则直接锁定
                                cmd.CommandText = "INSERT INTO need_to_lock VALUES('" +
                                        "more_than_three_return" + "','" +
                                    this.track_serial_noTextBox.Text.Trim() + "','" +
                                    this.ordernoTextBox.Text.Trim() + "','" +
                                    this.vendor_serail_noTextBox.Text.Trim() + "','" +//记录厂商序号
                                    "true" + "','" +
                                    DateTime.Now.ToString("yyyy/MM/dd") +
                                    "','')";
                                cmd.ExecuteNonQuery();

                                this.returnStore.Enabled = false;
                                MessageBox.Show("此主板已经来过【" + count + "】次，请注意已锁定");
                            }
                        }

                        mConn.Close();

                        //从历史数据查询是否匹配
                        //if (this.custom_serial_noTextBox.Text == "")
                        //{
                        //    MessageBox.Show("客户序号不能为空，严重，检查！");
                        //}

                        //if (this.productComboBox.Text != "TBG" && this.productComboBox.Text != "DT")//在某种客户别下 客户序号包含客户料号的东西，需要主动验证
                        //{
                        //    //需要去掉前面的非0字段
                        //    string customSerial = this.custommaterialNoTextBox.Text.TrimStart('0');
                        //    string replacedCustomSerial = this.replace_custom_materialNotextBox.Text.TrimStart('0');

                        //    if (this.custom_serial_noTextBox.Text.ToLower().Contains(customSerial.ToLower()))
                        //    {
                        //    }
                        //    else
                        //    {
                        //        if (this.track_serial_noTextBox.Text.Trim().StartsWith("RMUP") == false)//RMUP的板子不检查mpn
                        //        {
                        //            if (this.custom_serial_noTextBox.Text.ToLower().Contains(replacedCustomSerial.ToLower()))
                        //            {
                        //            }
                        //            else
                        //            {
                        //                MessageBox.Show("在" + this.productComboBox.Text + "下客户序号没有包含客户料号, 请检查追踪条码是否正确");
                        //                this.track_serial_noTextBox.Focus();
                        //                this.track_serial_noTextBox.SelectAll();

                        //                this.custom_serial_noTextBox.Text = "";
                        //                return;
                        //            }
                        //        }
                        //    }
                        //}
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

        private bool checkCustomSerialNo()
        {
            bool exist = false;
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                String inputSerialNo = this.custom_serial_noTextBox.Text.Trim();
               
                //先验证收货表
                cmd.CommandText = "select custom_serial_no from DeliveredTable where track_serial_no = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    if (inputSerialNo == querySdr[0].ToString().Trim())
                    {
                        exist = true;
                    }
                }
                querySdr.Close();

                if (!exist)//从转换表中查询
                {
                    cmd.CommandText = "select custom_serial_no from DeliveredTableTransfer where track_serial_no_transfer = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        if (inputSerialNo == querySdr[0].ToString().Trim())
                        {
                            exist = true;
                        }
                    }
                    querySdr.Close();
                }

                if (!exist)//从主板出库查询
                {
                    cmd.CommandText = "select custom_serial_no from mb_out_stock where track_serial_no = '" + this.track_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        if (inputSerialNo == querySdr[0].ToString().Trim())
                        {
                            exist = true;
                        }
                    }
                    querySdr.Close();
                }

                if (exist == false)//均不存在
                {
                    this.returnStore.Enabled = false;
                    MessageBox.Show("此客户序号不存在于收货表，转换表与主板出库表中，请检查是否正确！");

                    if (User.UserSelfForm.isSuperManager())
                    {
                        MessageBox.Show("现在由超级管理员操作，可以跳过！");
                        this.returnStore.Enabled = true;
                        exist = true;
                    }
                }
                else
                {

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
                            if (this.track_serial_noTextBox.Text.Trim().StartsWith("RMUP") == false)//RMUP的板子不检查mpn
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
                                    this.custom_serial_noTextBox.Text = "";
                                    mConn.Close();
                                    return exist;
                                }
                            }
                        }
                    }

                    //查询已经来过几次了，第3次直接拦截
                    cmd.CommandText = "select count(Id) from returnStore where custom_serial_no = '" + this.custom_serial_noTextBox.Text.Trim() + "'";
                    querySdr = cmd.ExecuteReader();
                    int returnCount = 0;
                    while (querySdr.Read())
                    {
                        returnCount = Int32.Parse(querySdr[0].ToString().Trim());
                        break;
                    }
                    querySdr.Close();

                    if (returnCount >= 2)
                    {
                        MessageBox.Show("已经来过2次以上，不能出货！");
                        this.returnStore.Enabled = false;
                    }
                    else
                    {
                        this.returnStore.Enabled = true;
                    }
                   
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return exist;
        }

        private void custom_serial_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {

                //需要增加验证逻辑，1主动输入此8s码， 如果没有则不能还货
                //2 根据输入的8s来验证，1）跟收货的8s，如果不存在则与主板转换的8s，如果不存在则跟库房出货的8s比对，这逻辑是根据跟踪条码拉出来的8s，来取三个地方
                if (this.custom_serial_noTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("此客户序号为空！");
                    this.returnStore.Enabled = false;
                    return;
                }

                if (this.track_serial_noTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("此跟踪条码为空！");
                    this.returnStore.Enabled = false;
                    return;
                }


                if (checkCustomSerialNo() == false)
                {
                    return;
                }

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
           // PrintUtils.printCustomMaterialNo(this.custommaterialNoTextBox.Text);
        }

       
    }
}
