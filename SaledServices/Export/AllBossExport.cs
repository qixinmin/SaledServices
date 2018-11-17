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
    public partial class AllBossExport : Form
    {
        public AllBossExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            exportxmlbutton.Enabled = false;

            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            //List<nb_aio_mblistSheet5_6> repairList = new List<nb_aio_mblistSheet5_6>();
            List<nb_aio_mblistSheet5_6> repairListtarget = new List<nb_aio_mblistSheet5_6>();
            List<nb_aio_mblistSheet5_6> repairListtargetAIO = new List<nb_aio_mblistSheet5_6>();
            List<nb_aio_mblistSheet5_6> repairListtargetNB = new List<nb_aio_mblistSheet5_6>();
           // List<allReturnOrderStruct> allReturnOrderList = new List<allReturnOrderStruct>();
            List<allSumStruct> allSumStructListAIO = new List<allSumStruct>();
            List<allSumStruct> allSumStructListNB = new List<allSumStruct>();
            List<fruListSheet4> fruList = new List<fruListSheet4>();
            
            debitnotsSheet3 debitnots = new debitnotsSheet3();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
            
                //查询fru的记录
                cmd.CommandText = "SELECT receive_date,orderno,customermaterialno,machine_type,name,peijian_no,make_date,gurantee,vendor_material_no,mpn1,custom_fault from fruDeliveredTable where receive_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    fruListSheet4 temp = new fruListSheet4();
                    temp.receive_date = querySdr[0].ToString();
                    temp.orderno = querySdr[1].ToString();
                    temp.customermaterialno = querySdr[2].ToString();
                    temp.machine_type = querySdr[3].ToString();
                    temp.name = querySdr[4].ToString();
                    temp.peijian_no = querySdr[5].ToString();
                    temp.make_date = querySdr[6].ToString();
                    temp.gurantee = querySdr[7].ToString();
                    temp.vendor_material_no = querySdr[8].ToString();
                    temp.mpn1 = querySdr[9].ToString();
                    temp.custom_fault = querySdr[10].ToString();
                    fruList.Add(temp);
                }
                querySdr.Close();

                foreach (fruListSheet4 temp in fruList)
                {
                    //查询fru是否计费
                    cmd.CommandText = "SELECT eol from frubomtable where custom_material_no='"+temp.customermaterialno+"'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.eol = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();
                }

                ///维修记录
                cmd.CommandText = "SELECT track_serial_no,COUNT(*)  from repair_record_table where repair_date between '" + startTime + "' and '" + endTime + "' and vendor='COMPAL' group by track_serial_no";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    nb_aio_mblistSheet5_6 temp = new nb_aio_mblistSheet5_6();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.repair_Num = querySdr[1].ToString();

                    repairListtarget.Add(temp);
                }
                querySdr.Close();

                foreach (nb_aio_mblistSheet5_6 repairRecord in repairListtarget)
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

                    cmd.CommandText = "select eol from MBMaterialCompare where custommaterialNo ='" + repairRecord.custommaterialNo + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairRecord.eol = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

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

                    cmd.CommandText = "select bgatype,BGAPN,BGA_place from bga_repair_record_table where track_serial_no ='" + repairRecord.track_serial_no + "' and newSn !=''";
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
                    repairRecord.smtRecords = new List<allSmtRecort>();
                    while (querySdr.Read())
                    {
                        allSmtRecort sub = new allSmtRecort();

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

                int frucount=0;
                foreach (nb_aio_mblistSheet5_6 repairRecord in repairListtarget)
                {
                    if (repairRecord.product == "AIO" && repairRecord.eol.Trim() == "Free")
                    {
                        repairListtargetAIO.Add(repairRecord);
                     }
                    else if (repairRecord.product == "LBG" && repairRecord.eol.Trim() == "Free")
                    {
                        repairListtargetNB.Add(repairRecord);
                    }

                    //统计信息
                    allSumStruct temp1 = new allSumStruct();
                    temp1.mb_brief = repairRecord.mb_brief;
                    if (repairRecord.product == "AIO")
                    {
                        if (allSumStructListAIO.Count == 0)
                        {
                            if (temp1.mb_brief != null && temp1.mb_brief.Trim() != "")
                            {
                                allSumStructListAIO.Add(temp1);
                            }
                        }
                        else
                        {
                            bool exist = false;
                            foreach (allSumStruct oldrecord in allSumStructListAIO)
                            {
                                if (oldrecord.equals(temp1))
                                {
                                    oldrecord.incrementNum();
                                    exist = true;
                                    break;
                                }
                            }
                            if (exist == false)
                            {
                                if (temp1.mb_brief != null && temp1.mb_brief.Trim() != "")
                                {
                                    allSumStructListAIO.Add(temp1);
                                }
                            }
                        }
                    }
                    else if (repairRecord.product == "LBG")
                    {
                        if (allSumStructListNB.Count == 0)
                        {
                            if (temp1.mb_brief != null && temp1.mb_brief.Trim() != "")
                            {
                                allSumStructListNB.Add(temp1);
                            }
                        }
                        else
                        {
                            bool exist = false;
                            foreach (allSumStruct oldrecord in allSumStructListNB)
                            {
                                if (oldrecord.equals(temp1))
                                {
                                    oldrecord.incrementNum();
                                    exist = true;
                                    break;
                                }
                            }
                            if (exist == false)
                            {
                                if (temp1.mb_brief != null && temp1.mb_brief.Trim() != "")
                                {
                                    allSumStructListNB.Add(temp1);
                                }
                            }
                        }
                    }                    
                 }
               
                foreach (fruListSheet4 temp in fruList)
                {
                    if (temp.eol.Trim() == "Free")
                    {  
                        frucount++;
                    }
                }

                //修改debitnots中部分数据，todo

                debitnots.dateG5 = endTime;
                debitnots.invF6 = "Inv#HWB" + endTime;

                debitnots.contentD14 = startTime;
                debitnots.contentE14 = endTime;
                debitnots.contentD16 = repairListtargetAIO.Count + "";
                debitnots.contentD17 = repairListtargetNB.Count + "";
                debitnots.contentD20 = frucount + "";

                debitnots.contentF16 = "$"+(Int16.Parse(debitnots.contentD16) * Double.Parse(debitnots.contentE16.Replace("$", "").Trim()));
                debitnots.contentF17 = "$"+(Int16.Parse(debitnots.contentD17) * Double.Parse(debitnots.contentE17.Replace("$", "").Trim()));
                debitnots.contentF20 = "$"+(Int16.Parse(debitnots.contentD20) * Double.Parse(debitnots.contentE20.Replace("$", "").Trim())) ;

                debitnots.contentF22 = "$" + (Double.Parse(debitnots.contentF16.Replace("$", "").Trim()) +
                    Double.Parse(debitnots.contentF17.Replace("$", "").Trim()) +
                    Double.Parse(debitnots.contentF20.Replace("$", "").Trim()));

                //debitnots.contentF24?如何计算
                //debitnots.contentF25?如何计算
                debitnots.contentF27 = "$" + (Double.Parse(debitnots.contentF22.Replace("$", "").Trim()) +
                    Double.Parse(debitnots.contentF24.Replace("$", "").Trim()) +
                    Double.Parse(debitnots.contentF25.Replace("$", "").Trim()));//结果计算如何 ？
                debitnots.contentE31 = "韩宝军"+endTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(debitnots, allSumStructListAIO, allSumStructListNB, fruList, repairListtargetAIO, repairListtargetNB, startTime, endTime);
        }

        public void generateExcelToCheck(
            debitnotsSheet3 debitnots,
            List<allSumStruct> allSumStructListAIO,
            List<allSumStruct> allSumStructListNB,
            List<fruListSheet4> fruList,
            List<nb_aio_mblistSheet5_6> repairRecordListAIO,
            List<nb_aio_mblistSheet5_6> repairRecordListNB,
            string startTime, string endTime)
        {
            List<allContent> allcontentList = new List<allContent>();

            //汇总AIO
            allContent sumContentAio = new allContent();
            sumContentAio.sheetName = startTime+"到"+endTime+"汇总AIO";
            sumContentAio.titleList = new List<string>();
            sumContentAio.contentList = new List<object>();
            sumContentAio.titleList.Add("Model");
            sumContentAio.titleList.Add("数量");
            if (allSumStructListAIO.Count == 0)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ctest1.contentArray = ct1;
                ct1.Add("");
                ct1.Add("");
                sumContentAio.contentList.Add(ctest1);
            }
            else
            {
                foreach (allSumStruct temp in allSumStructListAIO)
                {
                    ExportExcelContent ctest1 = new ExportExcelContent();
                    List<string> ct1 = new List<string>();
                    ctest1.contentArray = ct1;
                    ct1.Add(temp.mb_brief);
                    ct1.Add(temp.returnNum + "");
                    sumContentAio.contentList.Add(ctest1);
                }
            }
            
            allcontentList.Add(sumContentAio);

            //汇总NB
            allContent sumContentNB = new allContent();
            sumContentNB.sheetName = startTime+"到"+endTime+"汇总NB";
            sumContentNB.titleList = new List<string>();
            sumContentNB.contentList = new List<object>();
            sumContentNB.titleList.Add("Model");
            sumContentNB.titleList.Add("数量");
            if (allSumStructListNB.Count == 0)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ctest1.contentArray = ct1;
                ct1.Add("");
                ct1.Add("");
                sumContentNB.contentList.Add(ctest1);
            }
            else
            {
                foreach (allSumStruct temp in allSumStructListNB)
                {
                    ExportExcelContent ctest1 = new ExportExcelContent();
                    List<string> ct1 = new List<string>();
                    ctest1.contentArray = ct1;
                    ct1.Add(temp.mb_brief);
                    ct1.Add(temp.returnNum + "");
                    sumContentNB.contentList.Add(ctest1);
                }
            }

            allcontentList.Add(sumContentNB);

            //FRU记录
            allContent fruContent = new allContent();
            fruContent.sheetName = startTime+"到"+endTime+"FRU记录";
            fruContent.titleList = new List<string>();
            fruContent.contentList = new List<object>();
            fruContent.titleList.Add("收料日期");
            fruContent.titleList.Add("rma号码");
            fruContent.titleList.Add("联想料号");
            fruContent.titleList.Add("机型");
            fruContent.titleList.Add("名称");
            fruContent.titleList.Add("配件序号");
            fruContent.titleList.Add("周期");
            fruContent.titleList.Add("保内外");
            fruContent.titleList.Add("料号71");
            fruContent.titleList.Add("原材料号");
            fruContent.titleList.Add("客户故障");
           // fruContent.titleList.Add("是否收费");
            fruContent.titleList.Add("charge ");
            if (fruList.Count == 0)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ctest1.contentArray = ct1;
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                //ct1.Add("");
                ct1.Add("");
                fruContent.contentList.Add(ctest1);
            }
            else
            {
                foreach (fruListSheet4 temp in fruList)
                {
                    ExportExcelContent ctest1 = new ExportExcelContent();
                    List<string> ct1 = new List<string>();
                    ctest1.contentArray = ct1;

                    ct1.Add(temp.receive_date);
                    ct1.Add(temp.orderno);

                    ct1.Add(temp.customermaterialno);
                    ct1.Add(temp.machine_type);
                    ct1.Add(temp.name);
                    ct1.Add(temp.peijian_no);
                    ct1.Add(temp.make_date);
                    ct1.Add(temp.gurantee);
                    ct1.Add(temp.vendor_material_no);
                    ct1.Add(temp.mpn1);
                    ct1.Add(temp.custom_fault);
                    //ct1.Add(temp.eol);
                    ct1.Add(temp.charge);

                    fruContent.contentList.Add(ctest1);
                }
            }
           
            allcontentList.Add(fruContent);

            //维修记录AIO
            allContent repairRecordContentAIO = new allContent();
            repairRecordContentAIO.sheetName = startTime+"到"+endTime+"维修记录AIO";
            repairRecordContentAIO.titleList = new List<string>();
            repairRecordContentAIO.contentList = new List<object>();

            repairRecordContentAIO.titleList.Add("订单编号");
            repairRecordContentAIO.titleList.Add("来源");
            repairRecordContentAIO.titleList.Add("跟踪条码");
            repairRecordContentAIO.titleList.Add("收货日期");
            repairRecordContentAIO.titleList.Add("MB简称");
            repairRecordContentAIO.titleList.Add("MB描述");
            repairRecordContentAIO.titleList.Add("MPN");
            repairRecordContentAIO.titleList.Add("厂商序号");
            repairRecordContentAIO.titleList.Add("MB生产日期");
            repairRecordContentAIO.titleList.Add("客户故障");
            for (int i = 1; i <= 3; i++)
            {
                repairRecordContentAIO.titleList.Add("现象" + i);
            }
            for (int i = 1; i <= 1; i++)
            {
                repairRecordContentAIO.titleList.Add("mbfa" + i);
            }
            repairRecordContentAIO.titleList.Add("短路电压");
            repairRecordContentAIO.titleList.Add("北桥");
            repairRecordContentAIO.titleList.Add("南北桥料号");
            repairRecordContentAIO.titleList.Add("南桥");
            repairRecordContentAIO.titleList.Add("vga芯片料号");
            repairRecordContentAIO.titleList.Add("显存");
            repairRecordContentAIO.titleList.Add("CPU料号");
            repairRecordContentAIO.titleList.Add("CPU底座");

            for (int i = 1; i <= 3; i++)
            {
                repairRecordContentAIO.titleList.Add("其他" + i + "位置");
                //repairRecordContentAIO.titleList.Add("其他" + i + "位置料号");
            }

            repairRecordContentAIO.titleList.Add("断线氧化内短位置");

            repairRecordContentAIO.titleList.Add("维修人");
            repairRecordContentAIO.titleList.Add("测试人");
            repairRecordContentAIO.titleList.Add("修复日期 ");
            repairRecordContentAIO.titleList.Add("工程变更 ");
            repairRecordContentAIO.titleList.Add("charge");

            if (repairRecordListAIO.Count == 0)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");

                for (int i = 0; i < 3; i++)
                {
                    ct1.Add("");
                }

                for (int i = 0; i < 1; i++)
                {
                    ct1.Add("");
                }
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");

                for (int i = 0; i < 3; i++)
                {
                    ct1.Add("");
                }

                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");

                ctest1.contentArray = ct1;
                repairRecordContentAIO.contentList.Add(ctest1);
            }
            else
            {
                foreach (nb_aio_mblistSheet5_6 repaircheck in repairRecordListAIO)
                {
                    ExportExcelContent ctest1 = new ExportExcelContent();
                    List<string> ct1 = new List<string>();
                    ct1.Add(repaircheck.order_no);
                    ct1.Add(repaircheck.source);
                    ct1.Add(repaircheck.track_serial_no);
                    ct1.Add(repaircheck.receivedate != null ? repaircheck.receivedate.Replace("0:00:00", "").Trim() : "");
                    ct1.Add(repaircheck.mb_brief);

                    ct1.Add(repaircheck.mb_describe);
                    ct1.Add(repaircheck.mpn);
                    ct1.Add(repaircheck.vendor_serail_no);
                    ct1.Add(repaircheck.mb_make_date != null ? repaircheck.mb_make_date.Replace("0:00:00", "").Trim() : "");
                    ct1.Add(repaircheck.custom_fault);

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
                    for (int i = 0; i < 1; i++)
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
                    ct1.Add("");
                    ct1.Add(repaircheck.pch);
                    ct1.Add("");
                    ct1.Add(repaircheck.vga);
                    ct1.Add("");
                    ct1.Add(repaircheck.cpu);
                    ct1.Add("");
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < repaircheck.smtRecords.Count)
                        {
                            ct1.Add(repaircheck.smtRecords[i].smtplace);
                            // ct1.Add(repaircheck.smtRecords[i].smtMpn);
                        }
                        else
                        {
                            ct1.Add("");
                            // ct1.Add("");
                        }
                    }
                    ct1.Add("");

                    ct1.Add(repaircheck.repairer);
                    ct1.Add(repaircheck.tester);
                    ct1.Add(repaircheck.repair_date != null ? repaircheck.repair_date.Replace("0:00:00", "").Trim() : "");
                    ct1.Add("");
                    ct1.Add(repaircheck.charge);

                    ctest1.contentArray = ct1;
                    repairRecordContentAIO.contentList.Add(ctest1);
                }
            }           
            
            allcontentList.Add(repairRecordContentAIO);

            //维修记录NB
            allContent repairRecordContentNB = new allContent();
            repairRecordContentNB.sheetName = startTime+"到"+endTime+"维修记录NB";
            repairRecordContentNB.titleList = new List<string>();
            repairRecordContentNB.contentList = new List<object>();

            repairRecordContentNB.titleList.Add("订单编号");
            repairRecordContentNB.titleList.Add("来源");
            repairRecordContentNB.titleList.Add("跟踪条码");
            repairRecordContentNB.titleList.Add("收货日期");
            repairRecordContentNB.titleList.Add("MB简称");
            repairRecordContentNB.titleList.Add("MB描述");
            repairRecordContentNB.titleList.Add("MPN");
            repairRecordContentNB.titleList.Add("厂商序号");
            repairRecordContentNB.titleList.Add("MB生产日期");
            repairRecordContentNB.titleList.Add("客户故障");
            for (int i = 1; i <= 3; i++)
            {
                repairRecordContentNB.titleList.Add("现象" + i);
            }
            for (int i = 1; i <= 1; i++)
            {
                repairRecordContentNB.titleList.Add("mbfa" + i);
            }
            repairRecordContentNB.titleList.Add("短路电压");
            repairRecordContentNB.titleList.Add("北桥");
            repairRecordContentNB.titleList.Add("南北桥料号");
            repairRecordContentNB.titleList.Add("南桥");
            repairRecordContentNB.titleList.Add("vga芯片料号");
            repairRecordContentNB.titleList.Add("显存");
            repairRecordContentNB.titleList.Add("CPU料号");
            repairRecordContentNB.titleList.Add("CPU底座");

            for (int i = 1; i <= 3; i++)
            {
                repairRecordContentNB.titleList.Add("其他" + i + "位置");
                //repairRecordContentNB.titleList.Add("其他" + i + "位置料号");
            }

            repairRecordContentNB.titleList.Add("断线氧化内短位置");

            repairRecordContentNB.titleList.Add("维修人");
            repairRecordContentNB.titleList.Add("测试人");
            repairRecordContentNB.titleList.Add("修复日期 ");
            repairRecordContentNB.titleList.Add("工程变更 ");
            repairRecordContentNB.titleList.Add("charge");

            if (repairRecordListNB.Count == 0)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");

                for (int i = 0; i < 3; i++)
                {
                    ct1.Add("");
                }

                for (int i = 0; i < 1; i++)
                {
                    ct1.Add("");
                }

                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");

                for (int i = 0; i < 3; i++)
                {
                    ct1.Add("");
                }

                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");
                ct1.Add("");

                ctest1.contentArray = ct1;
                repairRecordContentNB.contentList.Add(ctest1);
            }
            else
            {
                foreach (nb_aio_mblistSheet5_6 repaircheck in repairRecordListNB)
                {
                    ExportExcelContent ctest1 = new ExportExcelContent();
                    List<string> ct1 = new List<string>();

                    ct1.Add(repaircheck.order_no);
                    ct1.Add(repaircheck.source);
                    ct1.Add(repaircheck.track_serial_no);
                    ct1.Add(repaircheck.receivedate != null ? repaircheck.receivedate.Replace("0:00:00", "").Trim() : "");  
                    ct1.Add(repaircheck.mb_brief);

                    ct1.Add(repaircheck.mb_describe);
                    ct1.Add(repaircheck.mpn);
                    ct1.Add(repaircheck.vendor_serail_no);
                    ct1.Add(repaircheck.mb_make_date != null ? repaircheck.mb_make_date.Replace("0:00:00", "").Trim() : "");
                    ct1.Add(repaircheck.custom_fault);

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
                    for (int i = 0; i < 1; i++)
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
                    ct1.Add("");
                    ct1.Add(repaircheck.pch);
                    ct1.Add("");
                    ct1.Add(repaircheck.vga);
                    ct1.Add("");
                    ct1.Add(repaircheck.cpu);
                    ct1.Add("");
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < repaircheck.smtRecords.Count)
                        {
                            ct1.Add(repaircheck.smtRecords[i].smtplace);
                           // ct1.Add(repaircheck.smtRecords[i].smtMpn);
                        }
                        else
                        {
                            ct1.Add("");
                           // ct1.Add("");
                        }
                    }
                    ct1.Add("");

                    ct1.Add(repaircheck.repairer);
                    ct1.Add(repaircheck.tester);
                    ct1.Add(repaircheck.repair_date != null ? repaircheck.repair_date.Replace("0:00:00", "").Trim() : "");
                    ct1.Add("");
                    ct1.Add(repaircheck.charge);


                    ctest1.contentArray = ct1;
                    repairRecordContentNB.contentList.Add(ctest1);
                }
            }

            allcontentList.Add(repairRecordContentNB);

            exportxmlbutton.Enabled = true;
            Utils.createExcelListUsingNPOI("D:\\" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + "新维修数据.xls",debitnots,allcontentList);
        }
    }

    public class allContent
    {
        public string sheetName;
        public List<string> titleList ;//= new List<string>();
        public List<Object> contentList;// = new List<object>();
    }

    public class allReturnOrderStruct
    {
        public string Id;
        public string vendor;
        public string product;
        public string return_file_no;
        public string storehouse;
        public string receive_date;
        public string return_date;
        public string orderno;
        public string custommaterialNo;

        public string dpkpn;
        public string track_serial_no;
        public string custom_serial_no;
        public string vendor_serail_no;
        public string vendormaterialNo;
        public string _status;
        public string custom_res_type;
        public string response_describe;

        public string tat;
        public string inputuser;
        public string lenovo_maintenance_no;
        public string lenovo_repair_no;

        public string mb_brief;
    }

    public class allSumStruct
    {
        public string mb_brief;
        public int returnNum = 1;

        public bool equals(allSumStruct temp)
        {
            return (this.mb_brief == temp.mb_brief);
        }

        public void incrementNum()
        {
            this.returnNum++;
        }
    }
    public class AIO_NB_StructSheet1_Sheet2//AIO产品别AIO,NB产品别为LBG
    {
        public string model;
        public string number;
    }

   public class debitnotsSheet3
   {
       public string titleC1 = "HONG WEI BAO CO., LTD.";
       public string addressA2 = "ADDRESSlNO.550,Jinhai Road,Pudong New District Shanghai,China";
       public string telA3 = "TEL:021-61624611";
       public string billto1A5 = "Bill to:";
       public string billto2A6 = "Compal Electronics, Inc.";
       public string billto3A7 = "581, Juikuang Rd., Neihu, Taipei, (114)";
       public string billto4A8 = "Taiwan, R.O.C.";
       public string telA9 = "Tel 886-2-8797-8588";
       public string faxA10 = "Fax 886-2-2658-4298";
       public string dateF5 = "Date:";
       public string dateG5 = "2018-10-22";
       public string invF6 = "Inv#HWB2018-10-22";
       public string contentA14 = "RMA相关费用明细";
       public string contentC14 = "时间段";
       public string contentD14 = "2018-10-1";
       public string contentE14 = "2018-10-22";
       public string contentD15 = "数量";
       public string contentE15 = "单价";

       public string contentB16 = "联想NBMB维修";
       public string contentD16 = "0";
       public string contentE16 = "$17.6";
       public string contentF16 = "$0.0";

       public string contentB17 = "AIO MB维修";
       public string contentD17 = "0";
       public string contentE17 = "$17.6";
       public string contentF17 = "$0.0";

       public string contentB20 = "联想NB FRU";
       public string contentD20 = "0";
       public string contentE20 = "$2.0";
       public string contentF20 = "$0.0";

       public string contentE22 = "费用合计";
       public string contentF22 = "$45,725.6=SUM(F16:F20)";

       public string contentB24 = "AIO 运费";
       public string contentE24 = "$6.5";
       public string contentF24 = "$0.0";

       public string contentB25 = "PAD 运费";
       public string contentE25 = "$6.5";
       public string contentF25 = "$0.0";

       public string contentE27 = "合计";
       public string contentF27 = "$0";

       public string contentE30 = "HONG WEI BAO CO., LTD";
       public string contentE31 = "韩宝军2018-10-22";
    }

   public class fruListSheet4
   {
       //收料日期	rma号码	联想料号	机型	名称	配件序号	周期	保内外	料号71	原材料号1	客户故障	charge
       public string receive_date;//收料日期
       public string orderno;//rma号码
       public string customermaterialno;//联想料号
       public string machine_type;//机型
       public string name ;//名称
       public string peijian_no;//配件序号
       public string make_date;//周期
       public string gurantee;//保内外
       public string vendor_material_no;//料号71
       public string mpn1;//	原材料号1n
       public string custom_fault;//客户故障
       public string eol;//是否收费
       public string charge="$2.0";//charge       
    }

   public class nb_aio_mblistSheet5_6//sheet5与6的信息在一起做
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
       public List<allSmtRecort> smtRecords;//最多三条
       public string fault_type;//故障类别
       public string repairer;
       public string tester;//测试1站别的测试人
       public string repair_date;//修复日期 
       public string repair_result;//修复结果
       public string lenovo_maintenance_no;//联想维修站编号
       public string software_update;//软体更新
       public string repair_Num;//维修次数
       public string eol;//是否收费
       public string charge = "$17.6";
   }

   public class allSmtRecort
   {
       public string smtMpn;//位置
       public string smtplace;//料号
   }
}
