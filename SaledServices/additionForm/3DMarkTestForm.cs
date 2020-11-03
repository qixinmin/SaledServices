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
using System.Diagnostics;

namespace SaledServices.additionForm
{
    public partial class _3DMarkTestForm : Form
    {

        string tempKeySerial = "";
        string track_serial_no = "", product;
        string customMaterialNo = "", vendor_serail_no = "", mac = "", uuid = "", custom_serial_no = "", mb_brief = "";
        string KEYID = "", KEYSERIAL = "";
        string cpu_type = "", cpu_freq = "", dpk_type = "", dpkpn = "", mpn = "", eco = "";
        bool existBuffer = false, existRepair = false;
        string currentStoreHouse = "";


        public _3DMarkTestForm()
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

                    this.button1.Enabled = true;
                    this.button2.Enabled = true;

                    currentStoreHouse = "";
                    cmd.CommandText = "select storehouse,vendor_serail_no from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    string vendor_serail_no = "";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        currentStoreHouse = querySdr[0].ToString().Trim();
                        vendor_serail_no = querySdr[1].ToString().Trim();
                    }
                    querySdr.Close();

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

                    cmd.CommandText = "select track_serial_no,product from repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    querySdr = cmd.ExecuteReader();
                    track_serial_no = ""; product = "";
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
                        }
                    }
                    else
                    {
                        while (querySdr.Read())
                        {
                            track_serial_no = querySdr[0].ToString();
                            product = querySdr[1].ToString();
                        }
                        querySdr.Close();
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
                    }

                    //当是LBG的时候，需要分1与2test站别，否则只需要设置一个testall站别
                   // if (product != "" && product == "LBG")
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

                                }
                                else
                                {
                                    KEYID = "NOK";
                                    KEYSERIAL = "NOK";
                                }

                                tempKeySerial = KEYSERIAL;
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
                    
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                // MessageBox.Show("成功生成BOM文档，请重启机器！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            assembleBat();
            runBatFile(@"D:\ymdos\", "ym_test.bat");
        }


        private void button2_Click(object sender, EventArgs e)
        {

            assembleBat();
            runBatFile(@"D:\Runin\", "3dmark.bat");
        }

        private void assembleBat()
        {
            KEYID = "";
            KEYSERIAL = "";
            string newMac = Regex.Replace(mac, "([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})([A-Za-z0-9]{2})", "$1-$2-$3-$4-$5-$6");

            string tempCustomMaterialNo = "";

            string totalStr = "SET MBID=" + track_serial_no + "\r\n"
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
                          
                         +"SET DPK=" + dpk_type + "\r\n"
                         + "SET CPUTYPE=" + cpu_type + "\r\n"
                         + "SET CPUFREQ=" + cpu_freq + "\r\n"
                         + "SET USERID=" + User.UserSelfForm.workId;
            Utils.createFile("D:\\fru\\", "BOM.bat", totalStr);
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
                MessageBox.Show(ex.ToString());
               // Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }

    }
}
