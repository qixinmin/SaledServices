using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Test_Outlook
{
    public partial class TestAllForm : Form
    {
        private String tableName = "testalltable";
        public TestAllForm()
        {
            InitializeComponent();
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
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select track_serial_no,product from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string track_serial_no = "", product = "";
                    while (querySdr.Read())
                    {
                        track_serial_no = querySdr[0].ToString();
                        product =  querySdr[1].ToString();
                    }
                    querySdr.Close();

                    if (product != "" && product != "LBG")//TBG, DT, AIO 
                    {
                        if (track_serial_no != "")
                        {
                            cmd.CommandText = "select custommaterialNo, vendor_serail_no,mac,uuid,custom_serial_no,mb_brief  from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            querySdr = cmd.ExecuteReader();
                            string customMaterialNo = "", vendor_serail_no = "", mac = "", uuid = "", custom_serial_no = "", mb_brief = "";

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

                            if (customMaterialNo != "")
                            {
                                string cpu_type = "", cpu_freq = "", dpk_type = "", dpkpn = "", mpn = "";
                                cmd.CommandText = "select cpu_type,cpu_freq,dpk_type,dpkpn, mpn from MBMaterialCompare where custommaterialNo='" + customMaterialNo + "'";

                                querySdr = cmd.ExecuteReader();

                                while (querySdr.Read())
                                {
                                    cpu_type = querySdr[0].ToString();
                                    cpu_freq = querySdr[1].ToString();
                                    dpk_type = querySdr[2].ToString();
                                    dpkpn = querySdr[3].ToString();
                                    mpn = querySdr[4].ToString();
                                }
                                querySdr.Close();

                                this.cpuTypetextBox.Text = cpu_type;
                                this.cpuFreqtextBox.Text = cpu_freq;
                                this.testerTextBox.Text = "tester";
                                this.testdatetextBox.Text = DateTime.Now.ToString("yyyy/MM/dd");

                                string KEYID = "", KEYSERIAL = "";
                                if (dpk_type != "NOK" && dpkpn != "")//此时需要查找导入的dpk表格，查找对应的KEYI KEYSERIAL
                                {
                                    //首先判断这个板子有没有来过，若来过则重新拿号给他，否则去新的
                                    cmd.CommandText = "select burn_date,KEYID,KEYSERIAL from DPK_table where custom_serial_no='" + custom_serial_no + "'";
                                    querySdr = cmd.ExecuteReader();
                                    string burn_date = "";
                                    while (querySdr.Read())
                                    {
                                        burn_date = querySdr[0].ToString();
                                        KEYID = querySdr[1].ToString();
                                        KEYSERIAL = querySdr[2].ToString();
                                    }
                                    querySdr.Close();

                                    if (burn_date != "" && Untils.in90Days(burn_date)) //不为空且在90天内
                                    {
                                        //KEYID,KEYSERIAL按之前的序列号，其他内容不变
                                    }
                                    else//不存在或超过90天，则分配新的东西
                                    {
                                        cmd.CommandText = "select KEYID,KEYSERIAL,Id from DPK_table where KEYPN='" + dpkpn + "' and status ='未使用'";

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
                                        }
                                        else
                                        {
                                            //更新烧录日期与custom_serial_no
                                            cmd.CommandText = "update DPK_table set burn_date = '" + DateTime.Now.ToString("yyyy/MM/dd") + "',custom_serial_no = '" + custom_serial_no + "'";
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    KEYID = "N/A";
                                    KEYSERIAL = "N/A";
                                }

                                this.keyidtextBox.Text = KEYID;
                                this.KEYSERIALtextBox.Text = KEYSERIAL;

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
                                string totalStr = "SET MBID=" + track_serial_no + "\r\n"
                                                + "SET SN=" + vendor_serail_no + "\r\n"
                                                + "SET SKU=" + mpn + "\r\n"
                                                + "SET MAC=" + mac + "\r\n"
                                                + "SET UUID=" + uuid + "\r\n"
                                                + "SET MB11S=" + custom_serial_no + "\r\n"
                                                + "SET OA3KEY=" + KEYID + "\r\n"
                                                + "SET OA3PID=" + KEYSERIAL + "\r\n"
                                                + "SET FRUPN=" + customMaterialNo + "\r\n"
                                                + "SET MODELID=" + mb_brief;
                                Untils.createFile("D:\\fru\\", "BOM.bat", totalStr);
                                Untils.createFile("D:\\fru\\", "BOM.NSH", totalStr);
                            }
                            else
                            {
                                this.tracker_bar_textBox.Focus();
                                this.tracker_bar_textBox.SelectAll();
                                MessageBox.Show("追踪条码的内容不在收货表中，请检查！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("此追踪条码没有维修记录！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("此追踪条码对应的客户别不是TBG, DT, AIO ！");
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
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

                    
                    cmd.CommandText = "INSERT INTO " + tableName + " VALUES('"
                        + this.tracker_bar_textBox.Text.Trim() + "','"
                        + this.testerTextBox.Text.Trim() + "','"
                        + this.testdatetextBox.Text.Trim()
                        + "')";
                    cmd.ExecuteNonQuery();                    
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("插入测试2数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
