using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Export
{
    public partial class ReturnGT2RepairRecordExport : Form
    {
        public ReturnGT2RepairRecordExport()
        {
            InitializeComponent();
            loadToReturnInformation();
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

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }          

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd");

            List<ReturnGT2RepairRecordStruct> receiveOrderList = new List<ReturnGT2RepairRecordStruct>();

            List<ReturnGT2RepairRecordStruct> receiveOrderListtarget = new List<ReturnGT2RepairRecordStruct>();

            List<ReturnGT2RepairRecordStruct> receiveOrderList2return = new List<ReturnGT2RepairRecordStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT track_serial_no,COUNT(*)  from repair_record_table where repair_date between '" + startTime + "' and '" + endTime + "'  group by track_serial_no";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReturnGT2RepairRecordStruct temp = new ReturnGT2RepairRecordStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.repair_Num = querySdr[1].ToString();

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                //过滤到test2的信息
                foreach (ReturnGT2RepairRecordStruct repairRecord in receiveOrderList)
                {
                    cmd.CommandText = "select Id from test2table where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {                       
                        receiveOrderListtarget.Add(repairRecord);
                    }
                    querySdr.Close();
                }

                foreach (ReturnGT2RepairRecordStruct repairRecord in receiveOrderList)
                {
                    cmd.CommandText = "select Id from testalltable where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        receiveOrderListtarget.Add(repairRecord);
                    }
                    querySdr.Close();
                }

                foreach (ReturnGT2RepairRecordStruct repairRecord in receiveOrderListtarget)
                {
                    cmd.CommandText = "select vendor,product,source_brief,custom_order,order_receive_date,custommaterialNo," +
                        "custom_serial_no,mb_describe,mb_brief,vendor_serail_no,mpn,mb_make_date,custom_fault,lenovo_maintenance_no from DeliveredTable where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairRecord.vendor = querySdr[0].ToString();
                        repairRecord.product = querySdr[1].ToString();
                        repairRecord.source = querySdr[2].ToString();
                        repairRecord.order_no = querySdr[3].ToString();
                        repairRecord.receivedate = querySdr[4].ToString();
                        repairRecord.custommaterialNo = querySdr[5].ToString();
                        repairRecord.custom_serial_no = querySdr[6].ToString();
                        repairRecord.mb_describe = querySdr[7].ToString();
                        repairRecord.mb_brief = querySdr[8].ToString();
                        repairRecord.vendor_serail_no = querySdr[9].ToString();
                        repairRecord.mpn = querySdr[10].ToString();
                        repairRecord.mb_make_date = querySdr[11].ToString();
                        repairRecord.custom_fault = querySdr[12].ToString();
                        repairRecord.lenovo_maintenance_no = querySdr[13].ToString();
                        break;
                    }
                    querySdr.Close();
                }


                //根据之前的维修列表开始查询2返信息
                List<string> filter8s = new List<string>();
                string lastReturnDateStart = Convert.ToDateTime(endTime).AddDays(-90).ToString("yyyy-MM-dd");
                cmd.CommandText = "select custom_serial_no from DeliveredTable where order_receive_date between '" + lastReturnDateStart + "' and '" + endTime + "' group by custom_serial_no having count(*) > 1";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    filter8s.Add(querySdr[0].ToString());
                }
                querySdr.Close();

                foreach (ReturnGT2RepairRecordStruct repairRecord in receiveOrderListtarget)
                {
                    if (filter8s.Contains(repairRecord.custom_serial_no))
                    {
                        cmd.CommandText = "select distinct(track_serial_no) ,Id from DeliveredTable	where custom_serial_no='" + repairRecord.custom_serial_no + "' order by Id desc";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            if (repairRecord.track_serial_no != querySdr[0].ToString())//取第二个内容
                            {
                                repairRecord.last_track_serial_no = querySdr[0].ToString();
                                break;
                            }
                        }
                        querySdr.Close();

                        receiveOrderList2return.Add(repairRecord);
                    }
                }

                receiveOrderList.Clear();//过滤公用
                foreach (ReturnGT2RepairRecordStruct repairRecord in receiveOrderList2return)
                {
                    //查询还货日期
                    cmd.CommandText = "select return_date from returnStore where track_serial_no ='" + repairRecord.last_track_serial_no + "' order by id desc";
                    querySdr = cmd.ExecuteReader();                   
                    while (querySdr.Read())
                    {
                        repairRecord.last_return_date = querySdr[0].ToString();
                    }

                    querySdr.Close();

                    if (repairRecord.last_return_date.Trim() == "")
                    {
                        continue;
                    }

                    try
                    {
                        DateTime dt1 = Convert.ToDateTime(repairRecord.receivedate);
                        DateTime dt2 = Convert.ToDateTime(repairRecord.last_return_date);

                        TimeSpan ts = dt1.Subtract(dt2);
                        int overdays = ts.Days;

                        if (overdays < 0)
                        {
                            continue;
                        }

                        repairRecord.last_tat= overdays + " ";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    //查询客户故障
                    cmd.CommandText = "select custom_fault from DeliveredTable where track_serial_no ='" + repairRecord.last_track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairRecord.last_custom_fault = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

                    //查询更换材料
                    cmd.CommandText = "select bgatype,BGAPN,BGA_place from bga_repair_record_table where track_serial_no ='" + repairRecord.last_repair_place + "' and  bga_repair_result='更换OK待测量' ";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string type = querySdr[0].ToString();
                        switch (type)
                        {
                            case "CPU":
                                repairRecord.last_repair_place += "CPU:" + querySdr[1].ToString() + ":" + querySdr[2].ToString();
                                break;
                            case "PCH":
                                repairRecord.last_repair_place += "PCH:" + querySdr[1].ToString() + ":" + querySdr[2].ToString();
                                break;
                            case "VGA":
                                repairRecord.last_repair_place += "VGA:" + querySdr[1].ToString() + ":" + querySdr[2].ToString();
                                break;
                        }
                    }
                    querySdr.Close();

                    cmd.CommandText = "select material_mpn,stock_place from fru_smt_used_record where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();                
                    while (querySdr.Read())
                    {
                        repairRecord.last_repair_place +=" SMT:"+ querySdr[0].ToString()+":"+querySdr[1].ToString();
                    }
                    querySdr.Close();

                    receiveOrderList.Add(repairRecord);
                }



                foreach (ReturnGT2RepairRecordStruct repairRecord in receiveOrderList)
                {                   
                    cmd.CommandText = "select Id,short_cut,fault_type,repairer,repair_date,repair_result,software_update from repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' order by id desc";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.fault_describeList = new List<string>();
                    repairRecord.mbfaList = new List<string>();
                    while (querySdr.Read())
                    {
                        repairRecord.shortcut = querySdr[1].ToString();

                        // repairRecord.fault_type = querySdr[2].ToString();
                        repairRecord.repairer = querySdr[3].ToString();
                        repairRecord.repair_date = querySdr[4].ToString();
                        repairRecord.repair_result = querySdr[5].ToString();
                        repairRecord.software_update = querySdr[6].ToString();
                    }
                    querySdr.Close();


                    cmd.CommandText = "select top 3 repair_result, fault_describe,mbfa1,fault_type from repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' order by id desc";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.fault_describeList = new List<string>();
                    repairRecord.mbfaList = new List<string>();
                    while (querySdr.Read())
                    {
                        if (Int16.Parse(repairRecord.repair_Num) > 1)
                        {
                            if (querySdr[0].ToString() == "NTF待测")
                            {
                                continue;
                            }
                            else
                            {
                                repairRecord.repair_result = querySdr[0].ToString();
                                repairRecord.fault_describeList.Add(querySdr[1].ToString());
                                repairRecord.mbfaList.Add(querySdr[2].ToString());
                                repairRecord.fault_type = querySdr[3].ToString();
                            }
                        }
                        else
                        {
                            repairRecord.repair_result = querySdr[0].ToString();
                            repairRecord.fault_describeList.Add(querySdr[1].ToString());
                            repairRecord.mbfaList.Add(querySdr[2].ToString());
                            repairRecord.fault_type = querySdr[3].ToString();
                        }
                    }
                    querySdr.Close();

                    cmd.CommandText = "select bgatype,BGAPN,BGA_place from bga_repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' and  bga_repair_result='更换OK待测量' ";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        string type = querySdr[0].ToString();
                        switch (type)
                        {
                            case "CPU":
                                repairRecord.cpu = querySdr[1].ToString();
                                repairRecord.cpu_place = querySdr[2].ToString();
                                repairRecord.fault_type = "功能不良";//针对只换BGA的，但是小材料信息没有的情况
                                break;
                            case "PCH":
                                repairRecord.pch = querySdr[1].ToString();
                                repairRecord.pch_place = querySdr[2].ToString();
                                repairRecord.fault_type = "功能不良";//针对只换BGA的，但是小材料信息没有的情况
                                break;
                            case "VGA":
                                repairRecord.vga = querySdr[1].ToString();
                                repairRecord.vga_place = querySdr[2].ToString();
                                repairRecord.fault_type = "功能不良";//针对只换BGA的，但是小材料信息没有的情况
                                break;
                        }
                    }
                    querySdr.Close();

                    cmd.CommandText = "select material_mpn,stock_place from fru_smt_used_record where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    repairRecord.smtRecords = new List<ReturnGT2SmtRecort>();
                    while (querySdr.Read())
                    {
                        ReturnGT2SmtRecort sub = new ReturnGT2SmtRecort();

                        sub.smtMpn = querySdr[0].ToString();
                        sub.smtplace = querySdr[1].ToString();

                        repairRecord.smtRecords.Add(sub);
                    }
                    querySdr.Close();

                    cmd.CommandText = "select top 1 tester from test1table where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairRecord.tester = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select top 1 tester from testalltable where track_serial_no ='" + repairRecord.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairRecord.tester = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    //修改最终结果repairRecord.repair_result
                    if (repairRecord.pch == "" && repairRecord.pch_place == ""
                        && repairRecord.vga == "" && repairRecord.vga_place == ""
                        && repairRecord.cpu == "" && repairRecord.cpu_place == ""
                        && repairRecord.smtRecords.Count == 0)
                    {
                        repairRecord.repair_result = "NTF测试OK";
                    }
                    else
                    {
                        repairRecord.repair_result = "修复良品";
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<ReturnGT2RepairRecordStruct> repairRecordList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("跟踪条码");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("来源");
            titleList.Add("订单编号");
            
            titleList.Add("客户料号");
            titleList.Add("客户序号");
            titleList.Add("MB描述");
            titleList.Add("MB简称");
            titleList.Add("厂商序号");
            titleList.Add("MPN");

            titleList.Add("MB生产日期");
            titleList.Add("客户故障");
            titleList.Add("软体更新");

            for (int i = 1; i <= 3; i++)
            {
                titleList.Add("现象" + i);
            }

            for (int i = 1; i <= 3; i++)
            {
                titleList.Add("mbfa" + i);
            }

            titleList.Add("短路电压");
            titleList.Add("南北桥料号");
            titleList.Add("南北桥位置");
            titleList.Add("vga芯片料号");
            titleList.Add("vga芯片位置");
            titleList.Add("CPU料号");
            titleList.Add("CPU芯片位置");

            for (int i = 1; i <= 3; i++)
            {
                titleList.Add("其他" + i + "位置");
                titleList.Add("其他" + i + "位置料号");
            }

            titleList.Add("故障类别");
            titleList.Add("维修人");
            titleList.Add("测试人");
            titleList.Add("修复日期 ");
            titleList.Add("修复结果");
            titleList.Add("联想维修站编号");
            titleList.Add("收货日期");
            titleList.Add("上一个跟踪条码");

            titleList.Add("FA上次还货日期");
            titleList.Add("天数");
            titleList.Add("FA上次客户故障");
            titleList.Add("FA上次维修换件");
            titleList.Add("FA不良现象");
            titleList.Add("FA量测判定");
            titleList.Add("FA不良原因");

            foreach (ReturnGT2RepairRecordStruct repaircheck in repairRecordList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add(repaircheck.track_serial_no);
                ct1.Add(repaircheck.vendor);
                ct1.Add(repaircheck.product);
                ct1.Add(repaircheck.source);
                ct1.Add(repaircheck.order_no);
               
                ct1.Add(repaircheck.custommaterialNo);
                ct1.Add(repaircheck.custom_serial_no);
                ct1.Add(repaircheck.mb_describe);
                ct1.Add(repaircheck.mb_brief);
                ct1.Add(repaircheck.vendor_serail_no);

                ct1.Add(repaircheck.mpn);
                ct1.Add(repaircheck.mb_make_date!=null ? repaircheck.mb_make_date.Replace("0:00:00", "").Trim():"");
                ct1.Add(repaircheck.custom_fault);
                ct1.Add(repaircheck.software_update);

                for (int i = 0; i < 3; i++)
                {
                    if (i < repaircheck.fault_describeList.Count)
                    {
                        ct1.Add(repaircheck.fault_describeList[i]);                      
                    }
                    else
                    {
                        ct1.Add("");                       
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (i < repaircheck.mbfaList.Count)
                    {
                        ct1.Add(repaircheck.mbfaList[i]);
                    }
                    else
                    {
                        ct1.Add("");
                    }
                }

                ct1.Add(repaircheck.shortcut);

                ct1.Add(repaircheck.pch);
                ct1.Add(repaircheck.pch_place);
                ct1.Add(repaircheck.vga);
                ct1.Add(repaircheck.vga_place);
                ct1.Add(repaircheck.cpu);
                ct1.Add(repaircheck.cpu_place);

                for (int i = 0; i < 3; i++)
                {
                    if (i < repaircheck.smtRecords.Count)
                    {
                        ct1.Add(repaircheck.smtRecords[i].smtplace);
                        ct1.Add(repaircheck.smtRecords[i].smtMpn);
                    }
                    else
                    {
                        ct1.Add("");
                        ct1.Add("");
                    }
                }

                ct1.Add(repaircheck.fault_type);
                ct1.Add(repaircheck.repairer);
                ct1.Add(repaircheck.tester);
                ct1.Add(repaircheck.repair_date!=null ? repaircheck.repair_date.Replace("0:00:00", "").Trim():"");
                ct1.Add(repaircheck.repair_result);
                ct1.Add(repaircheck.lenovo_maintenance_no);
                ct1.Add(repaircheck.receivedate!=null?repaircheck.receivedate.Replace("0:00:00", "").Trim():"");
                ct1.Add(repaircheck.last_track_serial_no);
                ct1.Add(repaircheck.last_return_date != null ? repaircheck.last_return_date.Replace("0:00:00", "").Trim() : "");
                ct1.Add(repaircheck.last_tat);
                ct1.Add(repaircheck.last_custom_fault);
                ct1.Add(repaircheck.last_repair_place);
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("2返维修信息记录" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    /**
     * SELECT track_serial_no,COUNT(*) as 次数
     
  FROM [SaledService].[dbo].[repair_record_table] group by track_serial_no
     * */
    public class ReturnGT2RepairRecordStruct
    {
        public string track_serial_no;//跟踪条码
        public string vendor;//厂商
        public string product;//客户别
        public string source;//来源
        public string order_no;//订单编号

        public string receivedate;//收货日期
        public string custommaterialNo;//客户料号
        public string custom_serial_no;//客户序号

        public string mb_describe;//mb描述
        public string mb_brief;//mb简称

        public string vendor_serail_no;//厂商序号
        public string mpn;//mpn
        public string mb_make_date;//mb生产日期
        public string custom_fault;//客户故障

        public List<string> fault_describeList;//最多三条
        public List<string> mbfaList;//最多三条

        public string shortcut;//短路电压

        public string pch;//南北桥料号
        public string pch_place;//南北桥位置
        public string vga;//vga芯片料号
        public string vga_place;//vga芯片位置
        public string cpu;//CPU料号
        public string cpu_place;//cpu芯片位置

        public List<ReturnGT2SmtRecort> smtRecords;//最多三条
        public string fault_type;//故障类别
        public string repairer;
        public string tester;//测试1站别的测试人
        public string repair_date;//修复日期 
        public string repair_result;//修复结果
        public string lenovo_maintenance_no;//联想维修站编号

        public string software_update;//软体更新

        public string repair_Num;//维修次数

        public string last_track_serial_no;//上次的跟踪条码

        public string last_return_date="";
        public string last_tat;
        public string last_custom_fault;
        public string last_repair_place="";
    }

    

    public class ReturnGT2SmtRecort
    {
        public string smtMpn;//位置
        public string smtplace;//料号
    }
}
