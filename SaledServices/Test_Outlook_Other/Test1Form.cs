﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SaledServices.Test_Outlook
{
    public partial class Test1Form : Form
    {
        private String tableName = "test1table";

        Dictionary<string, string> myDictionary = new Dictionary<string, string>();


        public Test1Form()
        {
            InitializeComponent();
            testerTextBox.Text = LoginForm.currentUser;
            testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            this.tracker_bar_textBox.Focus();

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
             
                cmd.CommandText = "select fault_index, fault_describe from customFault";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    string index = querySdr[0].ToString();
                    string temp = querySdr[1].ToString();
                    if (temp != "")
                    {                       
                        if (myDictionary.Keys.Contains(index) == false)
                        {
                            myDictionary.Add(index, temp);
                        }
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

        string tempKeySerial="";
        string track_serial_no = "",product;
        string customMaterialNo = "", vendor_serail_no = "", mac = "", uuid = "", custom_serial_no = "", mb_brief = "";
        string KEYID = "", KEYSERIAL = "";
        string cpu_type = "", cpu_freq = "", dpk_type = "", dpkpn = "", mpn = "",eco="";
        bool existBuffer = false, existRepair = false;
        string currentStoreHouse = "";

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

        private void tracker_bar_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.tracker_bar_textBox.Text.Trim() == "")
                {
                    this.tracker_bar_textBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
                }

                try
                {

                    //先删除已经存在的三个文件,后面再生成
                    Utils.deleteFile("D:\\fru\\", "BOM.bat");
                    Utils.deleteFile("D:\\fru\\", "BOM.NSH");
                    Utils.deleteFile("D:\\fru\\", "DPK.TXT");

                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string station = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (station != "维修" && station != "BGA")
                    {
                        cmd.CommandText = "select track_serial_no,product from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                            //buffer板不检查站别
                            querySdr.Close();
                        }
                        else
                        {
                            MessageBox.Show("主板未维修，板子已经经过站别[" + station + "]，测试未测试");
                            querySdr.Close();
                            mConn.Close();
                            this.tracker_bar_textBox.Focus();
                            this.tracker_bar_textBox.SelectAll();
                            this.bomdownload.Enabled = false;
                            return;
                        }
                    }

                    //检查打fail后 防止维修不敲记录的情况
                    string trackbar = this.tracker_bar_textBox.Text.Trim().ToUpper();
                    cmd.CommandText = "select top 1 inputdate from test_all_result_record where trackno='" + trackbar + "' and result='Fail' order by Id desc";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        string lastFailInputdate = "";
                        while (querySdr.Read())
                        {
                            lastFailInputdate = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        cmd.CommandText = "select Id from stationInfoRecord where inputdate >'" + lastFailInputdate + "' and trackno='" + trackbar + "' and station='维修'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows == false)
                        {
                            querySdr.Close();
                            MessageBox.Show("自测试打不良后维修没有输入记录");
                            querySdr.Close();
                            mConn.Close();
                            this.tracker_bar_textBox.Focus();
                            this.tracker_bar_textBox.SelectAll();
                            this.bomdownload.Enabled = false;
                            return;
                        }
                        querySdr.Close();

                    }
                    querySdr.Close();
                    //end 检查

                    this.bomdownload.Enabled = true;
                    this.button1.Enabled = true;
                   

                    currentStoreHouse = "";
                    cmd.CommandText = "select storehouse,vendor_serail_no,vendor from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    string vendor_serail_no = "",  vendorStr="";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        currentStoreHouse = querySdr[0].ToString().Trim();
                        vendor_serail_no = querySdr[1].ToString().Trim();
                        vendorStr = querySdr[2].ToString().Trim();
                    }
                    querySdr.Close();
                    this.Text = "测试1界面：" + vendorStr;

                    //查询维修记录，如果有则自动调取之前的记录
                    if (this.showRepairList.Checked)
                    {
                        cmd.CommandText = "SELECT Id FROM repair_record_table where vendor_serail_no='" + vendor_serail_no + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                            showRepairRecordIfExist(vendor_serail_no);
                        }
                        querySdr.Close();
                    }
                    //end

                    if (currentStoreHouse == "")//从替换表里查询
                    {
                        cmd.CommandText = "select storehouse from DeliveredTableTransfer where track_serial_no_transfer='" + this.tracker_bar_textBox.Text.Trim() + "'";

                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            currentStoreHouse = querySdr[0].ToString().Trim(); ;
                        }
                        querySdr.Close();
                    }

                    cmd.CommandText = "select track_serial_no,product,custom_serial_no from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    track_serial_no = ""; product = "";
                    String custom_serial_no = "";
                    this.bomdownload.Enabled = false; this.buffertest.Enabled = false; this.isburn.Enabled = false;
                    if (querySdr.HasRows == false)
                    {
                        querySdr.Close();
                        MessageBox.Show("本条形码不在维修表中，现在检查buffer表！");

                        cmd.CommandText = "select track_serial_no,product from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows == false)
                        {
                            MessageBox.Show("本条形码不在buffer出库表中，请检查！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }
                        else
                        {
                            while (querySdr.Read())
                            {
                                track_serial_no = querySdr[0].ToString();
                                product = querySdr[1].ToString();
                            }
                            querySdr.Close();
                            existBuffer = true;
                            this.buffertest.Enabled = true;
                            this.isburn.Enabled = true;
                            this.bomdownload.Enabled = true;
                        }
                    }
                    else
                    {
                        while (querySdr.Read())
                        {
                            track_serial_no = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                            custom_serial_no = querySdr[2].ToString();
                        }
                        querySdr.Close();
                        this.bomdownload.Enabled = true;
                        existRepair = true;
                    }
                    if (existRepair)
                    {
                        cmd.CommandText = "select top 1 _status from bga_wait_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "' order by Id desc";

                        querySdr = cmd.ExecuteReader();
                        string bgastatus = "";
                        while (querySdr.Read())
                        {
                            bgastatus = querySdr[0].ToString();
                        }
                        querySdr.Close();

                        if (bgastatus == "BGA不良")
                        {
                            MessageBox.Show("BGA的维修记录没有结束，请检查！");
                            querySdr.Close();
                            mConn.Close();
                            return;
                        }

                        cmd.CommandText = "select customFault,repair_date from repair_record_table where custom_serial_no='" + custom_serial_no + "' order by Id desc";

                        querySdr = cmd.ExecuteReader();
                        string faultContent = "";
                        while (querySdr.Read())
                        {
                            faultContent += "：" + querySdr[0].ToString() + "," + Utils.modifyDataFormat(querySdr[1].ToString()) + "\n";
                        }
                        querySdr.Close();
                        this.repairedLabel.Text=faultContent;
                    }else{
                        this.repairedLabel.Text="无维修记录";
                    }
                    

                    //当是LBG的时候，需要分1与2test站别，否则只需要设置一个testall站别
                    if (product != "" && product == "LBG")
                    {
                        if (track_serial_no != "")
                        {
                            customMaterialNo = ""; vendor_serail_no = ""; mac = ""; uuid = ""; custom_serial_no = ""; mb_brief = "";

                            if (existRepair)
                            {
                                cmd.CommandText = "select custommaterialNo, vendor_serail_no,mac,uuid,custom_serial_no,mb_brief from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    customMaterialNo = querySdr[0].ToString();
                                    vendor_serail_no = querySdr[1].ToString();

                                    mac = querySdr[2].ToString();
                                    uuid = querySdr[3].ToString();
                                    custom_serial_no = querySdr[4].ToString();
                                    mb_brief = querySdr[5].ToString();
                                }
                                querySdr.Close();

                                if (customMaterialNo == "")//从替换表里查询
                                {
                                    cmd.CommandText = "select custommaterialNo, vendor_serail_no,mac,uuid,custom_serial_no,mb_brief from DeliveredTableTransfer where track_serial_no_transfer='" + this.tracker_bar_textBox.Text.Trim() + "'";
                                    querySdr = cmd.ExecuteReader();
                                    while (querySdr.Read())
                                    {
                                        customMaterialNo = querySdr[0].ToString();
                                        vendor_serail_no = querySdr[1].ToString();

                                        mac = querySdr[2].ToString();
                                        uuid = querySdr[3].ToString();
                                        custom_serial_no = querySdr[4].ToString();
                                        mb_brief = querySdr[5].ToString();
                                    }
                                    querySdr.Close();
                                }
                            }
                            else if (existBuffer)
                            {
                                cmd.CommandText = "select custommaterialNo, vendor_serial_no,custom_serial_no,mb_brief from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                                querySdr = cmd.ExecuteReader();
                                while (querySdr.Read())
                                {
                                    customMaterialNo = querySdr[0].ToString();
                                    vendor_serail_no = querySdr[1].ToString();

                                    custom_serial_no = querySdr[2].ToString();
                                    mb_brief = querySdr[3].ToString();
                                }
                                querySdr.Close();
                            }
                           

                            if (customMaterialNo != "")
                            {
                                cpu_type = ""; cpu_freq = ""; dpk_type = ""; dpkpn = ""; mpn = ""; eco = "";
                                cmd.CommandText = "select cpu_type,cpu_freq,dpk_type,dpkpn, mpn,eco from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                                querySdr = cmd.ExecuteReader();

                                while (querySdr.Read())
                                {
                                    cpu_type = querySdr[0].ToString();
                                    cpu_freq = querySdr[1].ToString();
                                    dpk_type = querySdr[2].ToString();
                                    dpkpn = querySdr[3].ToString();
                                    mpn = querySdr[4].ToString();
                                    eco = querySdr[5].ToString();
                                }
                                querySdr.Close();

                                this.cpuTypetextBox.Text = cpu_type;
                                this.cpuFreqtextBox.Text = cpu_freq;
                                this.testerTextBox.Text = LoginForm.currentUser;
                                this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);


                                KEYID = ""; KEYSERIAL = "";                               
                                if (dpk_type != "NOK" && dpkpn != "")//此时需要查找导入的dpk表格，查找对应的KEYI KEYSERIAL
                                {
                                    //首先判断这个板子有没有来过，若来过则重新拿号给他，否则去新的
                                    cmd.CommandText = "select top 1 burn_date,KEYID,KEYSERIAL from DPK_table where custom_serial_no='" + custom_serial_no + "' order by burn_date desc";
                                    querySdr = cmd.ExecuteReader();
                                    string burn_date = "";                                    
                                    while (querySdr.Read())
                                    {
                                        burn_date = querySdr[0].ToString();
                                        KEYID = querySdr[1].ToString();
                                        KEYSERIAL = querySdr[2].ToString();
                                        break;
                                    }
                                    querySdr.Close();

                                    if (burn_date != "" && Utils.in90Days(burn_date)) //不为空且在90天内
                                    {
                                        //KEYID,KEYSERIAL按之前的序列号，其他内容不变
                                    }
                                    else//不存在或超过90天，则分配新的东西
                                    {
                                        cmd.CommandText = "select KEYID,KEYSERIAL,Id from DPK_table where KEYPN='" + dpkpn + "' and _status ='未使用' order by Id asc";
                                        querySdr = cmd.ExecuteReader();
                                        bool exist = false;
                                        string id = "";
                                        while (querySdr.Read())
                                        {
                                            exist = true;
                                            KEYID = querySdr[0].ToString();
                                            KEYSERIAL = querySdr[1].ToString();
                                            id = querySdr[2].ToString();
                                            break;
                                        }
                                        querySdr.Close();

                                        if (exist == false)
                                        {
                                            MessageBox.Show("此DPKPN" + dpkpn + "的序列号已经使用完毕或不存在！");
                                            mConn.Close();
                                            return;
                                        }
                                        else
                                        {
                                            if (currentStoreHouse != "DOA_整机" && currentStoreHouse != "惠阳库" && currentStoreHouse != "成都库")
                                            {
                                                //更新烧录日期与custom_serial_no,与使用状态
                                                cmd.CommandText = "update DPK_table set _status = '已使用', burn_date = GETDATE(),custom_serial_no = '" + custom_serial_no + "' where Id = '" + id + "'";
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                                else 
                                {
                                    KEYID = "NOK";
                                    KEYSERIAL = "NOK";
                                    this.button5.Enabled = false;
                                }


                                //检查文件是否存在
                                if (Utils.existAndCopyToServer(this.tracker_bar_textBox.Text.Trim(), "test1", this.testerTextBox.Text.Trim(), mb_brief) == false)
                                {
                                    MessageBox.Show("追踪条码的Log内容为空，请检查！");
                                    return;
                                }                   


                                this.button5.Enabled = true;

                                this.keyidtextBox.Text = KEYID;

                                tempKeySerial = KEYSERIAL;
                                this.KEYSERIALtextBox.Text = KEYSERIAL;
                                if (KEYID != "NOK")
                                {
                                    int lastEm = tempKeySerial.LastIndexOf('-');
                                    this.KEYSERIALtextBox.Text = "XXXXX-XXXXX-XXXXX-XXXXX-" + tempKeySerial.Substring(lastEm + 1, 5);
                                }
                            }
                            else
                            {
                                this.tracker_bar_textBox.Focus();
                                this.tracker_bar_textBox.SelectAll();
                                MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                                mConn.Close();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("此追踪条码没有维修记录！");
                            mConn.Close();
                            return;
                        }
                    }
                    else
                    {
                        if (product == "")
                        {
                            MessageBox.Show("此追踪条码没有维修记录！");
                            mConn.Close();
                            return;
                        }else
                        {
                            MessageBox.Show("此追踪条码对应的客户别不是LBG！");
                            mConn.Close();
                            return;
                        }
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

               // MessageBox.Show("成功生成BOM文档，请重启机器！");
            }
        }

        public bool isNotNOKCheck()
        {
            string generateFile = "D:\\fru\\DPK.TXT";
            if (File.Exists(generateFile) == false)
            {
                MessageBox.Show("D:\\fru\\DPK.txt文件不存在！");
                return false;
            }

            StreamReader sr = new StreamReader(generateFile, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                // MessageBox.Show(line.ToString());
                break;
            }
            sr.Close();
            if (tempKeySerial != "" && line != null && line.Contains(tempKeySerial))
            {
            }
            else
            {
                MessageBox.Show("文件不存在或者DPK内容与序列号不匹配， 请重新烧录！");
                return false;
            }
            return true;
        }

        private void confirmbutton_Click(object sender, EventArgs e)
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
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

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string Id = "";
                    while (querySdr.Read())
                    {
                        Id = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();
                    if (Id != "")
                    {
                        //MessageBox.Show("此序列号已经存在！");
                        //this.tracker_bar_textBox.Text = "";
                        //this.cpuFreqtextBox.Text = "";
                        //this.cpuTypetextBox.Text = "";
                        //this.keyidtextBox.Text = "";
                        //this.KEYSERIALtextBox.Text = "";
                        //conn.Close();
                        //return;

                        cmd.CommandText = "update " + tableName + " set test_date = GETDATE() "
                             + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {

                        cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                            + this.tracker_bar_textBox.Text.Trim() + "','"
                            + this.testerTextBox.Text.Trim() + "',GETDATE())";

                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "select Id from stationInformation where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        querySdr.Close();
                        cmd.CommandText = "update stationInformation set station = 'Test1', updateDate = GETDATE()  "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        querySdr.Close();
                        cmd.CommandText = "select track_serial_no,product from mb_out_stock where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)//从buffer发出来的板子
                        {
                            //记录站别信息
                            querySdr.Close();
                            cmd.CommandText = "INSERT INTO stationInformation VALUES('"
                                + this.tracker_bar_textBox.Text.Trim() + "','Test1',GETDATE())";
                            cmd.ExecuteNonQuery();
                        }
                        querySdr.Close();
                    }

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim() +
                "','Test1','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO test_all_result_record VALUES('"
                     + this.tracker_bar_textBox.Text.Trim() + "','"
                     + this.testerTextBox.Text.Trim() + "',GETDATE(),'Pass','','Test1')";
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

               
                MessageBox.Show("插入测试1数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (KEYID == "" || KEYSERIAL == "")
            {
                MessageBox.Show("序列号还没有下载，请检查操作！");
                return;
            }

            //SET MBID=RIBM160907010247     跟踪条码
            //SET SN=1021948402900          厂商序号
            //SET SKU=45101201065           MPN
            //SET MAC=28D24475FE86          MAC
            //SET UUID=11111111111111111111111111111111  UUID
            //SET MB11S=8SSB20A29572L1HF4440062  客户序号
            //SET OA3KEY=N/A    KEYID
            //SET OA3PID=N/A    KEYSERIAL
            //SET FRUPN=04X5152  客户料号
            //SET MODELID=VIUX2  MB简称

            string newMac = Regex.Replace(mac, "([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})", "$1-$2-$3-$4-$5-$6");

            string tempCustomMaterialNo = "";
            //if (customMaterialNo.Length == 10 && customMaterialNo.StartsWith("000"))
            //{
            //    tempCustomMaterialNo = customMaterialNo.Substring(3);
            //}
            string totalStr = "SET -v MBID " + track_serial_no + "\r\n"
                            + "SET -v SN " + vendor_serail_no + "\r\n"
                            + "SET -v SKU " + mpn + "\r\n"
                            + "SET -v MAC " + newMac + "\r\n"
                            + "SET -v UUID " + uuid + "\r\n"
                            + "SET -v MB11S " + custom_serial_no + "\r\n"
                            + "SET -v OA3KEY " + KEYSERIAL + "\r\n"
                            + "SET -v OA3PID " + KEYID + "\r\n"
                            + "SET -v DPKNO " + KEYSERIAL + "\r\n"
                            + "SET -v DPKID " + KEYID + "\r\n"
                            + "SET -v FRUPN " + tempCustomMaterialNo + "\r\n"
                            + "SET -v MODELID " + mb_brief + "\r\n"
                            + "SET -v storehouse " + currentStoreHouse + "\r\n"
                            + "SET -v eco " + eco + "\r\n"
                         +"SET -v DPK=" + dpk_type + "\r\n"
                         + "SET -v CPUTYPE=" + cpu_type + "\r\n"
                         + "SET -v CPUFREQ=" + cpu_freq + "\r\n"
                         + "SET -v USERID=" + User.UserSelfForm.workId;
            Utils.createFile("D:\\fru\\", "BOM.NSH", totalStr);

            totalStr = "SET MBID=" + track_serial_no + "\r\n"
                           + "SET SN=" + vendor_serail_no + "\r\n"
                           + "SET SKU=" + mpn + "\r\n"
                           + "SET MAC=" + newMac + "\r\n"
                           + "SET UUID=" + uuid + "\r\n"
                           + "SET MB11S=" + custom_serial_no + "\r\n"
                           + "SET OA3KEY=" + KEYSERIAL + "\r\n"
                           + "SET OA3PID=" + KEYID + "\r\n"
                           + "SET DPKNO=" + KEYSERIAL + "\r\n"
                           + "SET DPKID=" + KEYID + "\r\n"
                           + "SET FRUPN=" + tempCustomMaterialNo + "\r\n"
                           + "SET MODELID=" + mb_brief + "\r\n"
                            + "SET storehouse=" + currentStoreHouse + "\r\n"
                             + "SET eco=" + eco + "\r\n"
                           + "SET DPK=" + dpk_type + "\r\n"
                           + "SET CPUTYPE=" + cpu_type + "\r\n"
                           + "SET CPUFREQ=" + cpu_freq + "\r\n"
                           + "SET USERID=" + User.UserSelfForm.workId;
               
            Utils.createFile("D:\\fru\\", "BOM.bat", totalStr);

            //string totalStr = "SET MBID=" + track_serial_no + "\r\n"
            //                + "SET SN=" + vendor_serail_no + "\r\n"
            //                + "SET SKU=" + mpn + "\r\n"
            //                + "SET MAC=" + mac + "\r\n"
            //                + "SET UUID=" + uuid + "\r\n"
            //                + "SET MB11S=" + custom_serial_no + "\r\n"
            //                + "SET OA3KEY=" + KEYSERIAL + "\r\n"
            //                + "SET OA3PID=" + KEYID + "\r\n"
            //                + "SET FRUPN=" + customMaterialNo + "\r\n"
            //                + "SET MODELID=" + mb_brief + "\r\n"
            //                + "SET DPK=" + dpk_type;
            //Untils.createFile("D:\\fru\\", "BOM.bat", totalStr);
            //Untils.createFile("D:\\fru\\", "BOM.NSH", totalStr);

            Utils.createFile("C:\\CHKCPU\\", "BOM.bat", totalStr);

            //清空变量
            KEYID = ""; this.keyidtextBox.Text = "";
            KEYSERIAL = ""; this.KEYSERIALtextBox.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(null, null);//fru
            
            string dir = Directory.GetCurrentDirectory() + "\\Files\\";
            if (Directory.Exists(dir))
            {
                string fileInput = dir + "FileInput.xml";
                if (File.Exists(fileInput))
                {
                    File.Copy(fileInput, @"C:\OA3\FileInput.xml", true);
                }
                
                downloadFiles(@"C:\CHKCPU\CPUPN.txt", @"C:\CHKCPU\CHKCPU.BAT");
                runBatFile(@"C:\CHKCPU\", "CHKCPU.BAT");
            }
            else 
            {
                MessageBox.Show(dir + "不存在！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentStoreHouse != "DOA_整机" && currentStoreHouse != "惠阳库" && currentStoreHouse != "成都库")//三个库别外的东西，成都库，惠阳库，DMA整机
            {
                if (KEYID == "" || KEYSERIAL == "")
                {
                    MessageBox.Show("序列号还没有下载，请检查操作！");
                    return;
                }

                button2_Click(null, null);//fru

                downloadFiles(@"C:\CHKCPU\CPUPN.txt", @"C:\CHKCPU\CHKCPU.BAT");

                runBatFile(@"C:\CHKCPU\", "CHKCPU.BAT");

                if (this.dpk_type == "NOK")//否则需要点击 DPK烧录检查 按钮
                {
                    confirmbutton_Click(null, null);
                    this.Close();
                }
            }
            else
            {
                KEYID = "";
                KEYSERIAL = "";
                string newMac = Regex.Replace(mac, "([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})", "$1-$2-$3-$4-$5-$6");

                string tempCustomMaterialNo = "";
                string totalStr = "SET -v MBID " + track_serial_no + "\r\n"
                                + "SET -v SN " + vendor_serail_no + "\r\n"
                                + "SET -v SKU " + mpn + "\r\n"
                                + "SET -v MAC " + newMac + "\r\n"
                                + "SET -v UUID " + uuid + "\r\n"
                                + "SET -v MB11S " + custom_serial_no + "\r\n"
                                + "SET -v OA3KEY " + KEYSERIAL + "\r\n"
                                + "SET -v OA3PID " + KEYID + "\r\n"
                                + "SET -v DPKNO " + KEYSERIAL + "\r\n"
                                + "SET -v DPKID " + KEYID + "\r\n"
                                + "SET -v FRUPN " + tempCustomMaterialNo + "\r\n"
                                + "SET -v MODELID " + mb_brief + "\r\n"
                                + "SET -v storehouse " + currentStoreHouse + "\r\n"
                                 + "SET -v eco " + eco + "\r\n"
                            +"SET -v DPK=" + dpk_type + "\r\n"
                        + "SET -v CPUTYPE=" + cpu_type + "\r\n"
                        + "SET -v CPUFREQ=" + cpu_freq + "\r\n"
                        + "SET -v USERID=" + User.UserSelfForm.workId;
                Utils.createFile("D:\\fru\\", "BOM.NSH", totalStr);

                totalStr = "SET MBID=" + track_serial_no + "\r\n"
                               + "SET SN=" + vendor_serail_no + "\r\n"
                               + "SET SKU=" + mpn + "\r\n"
                               + "SET MAC=" + newMac + "\r\n"
                               + "SET UUID=" + uuid + "\r\n"
                               + "SET MB11S=" + custom_serial_no + "\r\n"
                               + "SET OA3KEY=" + KEYSERIAL + "\r\n"
                               + "SET OA3PID=" + KEYID + "\r\n"
                               + "SET DPKNO=" + KEYSERIAL + "\r\n"
                               + "SET DPKID=" + KEYID + "\r\n"
                               + "SET FRUPN=" + tempCustomMaterialNo + "\r\n"
                               + "SET MODELID=" + mb_brief + "\r\n"
                               + "SET storehouse=" + currentStoreHouse + "\r\n"
                               + "SET eco=" + eco + "\r\n"
                            + "SET DPK=" + dpk_type + "\r\n"
                           + "SET CPUTYPE=" + cpu_type + "\r\n"
                           + "SET CPUFREQ=" + cpu_freq + "\r\n"
                           + "SET USERID=" + User.UserSelfForm.workId;
                Utils.createFile("D:\\fru\\", "BOM.bat", totalStr);

                Utils.createFile("C:\\CHKCPU\\", "BOM.bat", totalStr);

                //清空变量
                KEYID = ""; this.keyidtextBox.Text = "";
                KEYSERIAL = ""; this.KEYSERIALtextBox.Text = "";

                downloadFiles(@"C:\CHKCPU\CPUPN.txt", @"C:\CHKCPU\CHKCPU.BAT");
                runBatFile(@"C:\CHKCPU\", "CHKCPU.BAT");

                try
                {
                    SqlConnection conn = new SqlConnection(Constlist.ConStr);
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string Id = "";
                        while (querySdr.Read())
                        {
                            Id = querySdr[0].ToString();
                            break;
                        }
                        querySdr.Close();
                        if (Id != "")
                        {
                            cmd.CommandText = "update " + tableName + " set test_date =GETDATE()  "
                                 + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                                + this.tracker_bar_textBox.Text.Trim().ToUpper() + "','"
                                + this.testerTextBox.Text.Trim() + "',GETDATE())";

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim().ToUpper() +
              "','Test1','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update stationInformation set station = 'Test1', updateDate = GETDATE()  "
                                + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("SaledService is not opened");
                    }

                    conn.Close();
                    MessageBox.Show("插入测试1数据OK");
                    currentStoreHouse = "";
                    this.tracker_bar_textBox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (KEYID == "" || KEYSERIAL == "")
            {
                MessageBox.Show("序列号还没有下载，请检查操作！");
                return;
            }
            runBatFile(@"C:\CHKDPK\", "CHKDPK.BAT");

            //检查文件是否存在
            string generateFile = "D:\\YMDOS\\LOG\\"+this.tracker_bar_textBox.Text.Trim()+".TXT";
            if (File.Exists(generateFile) == false)
            {
                MessageBox.Show(generateFile + "文件不存在！");
                return;
            }

            if (isNotNOKCheck())
            {
                confirmbutton_Click(null, null);
                this.Close();
            }           
        }

        private void runBatFile(string path, string filename)
        {
            try
            {
                string targetDir = string.Format(path);//this is where testChange.bat lies
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = filename;
                // proc.StartInfo.Arguments = string.Format("10");//this is argument
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }

        private void downloadFiles(string cpupnfile, string chkcpufile)
        {
            try
            {
                if (File.Exists(cpupnfile))
                {
                    File.Delete(cpupnfile);
                }
                if (File.Exists(chkcpufile))
                {
                    File.Delete(chkcpufile);
                }
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();
                
                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT cpupn, chkcpu FROM TestCpu";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        byte[] cpupn = (byte[])reader.GetValue(0);
                        byte[] chkcpu = (byte[])reader.GetValue(1);

                        string saveFileName = cpupnfile;
                        int arraysize = new int();
                        arraysize = cpupn.GetUpperBound(0);
                        FileStream fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(cpupn, 0, arraysize);
                        fs.Close();

                        saveFileName = chkcpufile;
                        arraysize = chkcpu.GetUpperBound(0);
                        fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(chkcpu, 0, arraysize);
                        fs.Close();
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
        }

        private void buffertest_Click(object sender, EventArgs e)
        {
            if (KEYID == "NOK")
            {
                //过站
                makePassInfo();
            }
            else
            {
                //判断是否下载
                if (isburn.Checked)
                {
                    button4_Click(null, null);
                }
                else
                {
                    makePassInfo();
                }
            }
        }

        private void makePassInfo()
        {
            if (this.tracker_bar_textBox.Text.Trim() == "")
            {
                MessageBox.Show("追踪条码的内容为空，请检查！");
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

                    cmd.CommandText = "select Id from " + tableName + " where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string Id = "";
                    while (querySdr.Read())
                    {
                        Id = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (Id != "")
                    {
                        MessageBox.Show("此序列号已经存在！");
                        this.tracker_bar_textBox.Text = "";
                        this.cpuFreqtextBox.Text = "";
                        this.cpuTypetextBox.Text = "";
                        this.keyidtextBox.Text = "";
                        this.KEYSERIALtextBox.Text = "";
                        conn.Close();
                        return;
                    }

                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim().ToUpper() +
              "','Test1','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = 'Test1', updateDate = GETDATE() "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim().ToUpper() + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试All数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.failDescribe.Text.Trim() == "")
            {
                MessageBox.Show("不良现象内容为空！");
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


                    cmd.CommandText = "INSERT INTO test_all_result_record VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim().ToUpper() + "',GETDATE(),'Fail','" + this.failDescribe.Text.Trim() + "','Test1')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update stationInformation set station = '维修', updateDate = GETDATE() "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into stationInfoRecord  VALUES('" + this.tracker_bar_textBox.Text.Trim().ToUpper() +
               "','Test1','" + this.testerTextBox.Text.Trim() + "',GETDATE())";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试Fail数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        private void failDescribe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.failDescribe.Text.Trim() != "" && Regex.IsMatch(this.failDescribe.Text.Trim(), @"^[+-]?\d*[.]?\d*$"))
                {
                    try
                    {
                        this.failDescribe.Text = myDictionary[this.failDescribe.Text.Trim()];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("故障代码" + this.failDescribe.Text.Trim() + "不存在");
                    }
                }
            }
        }
    }
}
