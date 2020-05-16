using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class HefeiExportExcel : Form
    {
        List<badcodescompare> badcodeslist = new List<badcodescompare>();//odescompare
        public HefeiExportExcel()
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

                cmd.CommandText = "select yimaicode,yimaides,renbaocode,renbaodes from badcodes";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    badcodescompare temp = new badcodescompare();

                    temp.yimaicode = querySdr[0].ToString();
                    temp.yimaides = querySdr[1].ToString();
                    temp.renbaocode = querySdr[2].ToString();
                    temp.renbaodes = querySdr[3].ToString();
                    badcodeslist.Add(temp);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            if (this.vendorComboBox.Text.Trim() == "" || this.productComboBox.Text.Trim() == "")
            {
                MessageBox.Show("请选择厂商客户别");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<HefeiDataStruct> receiveOrderList = new List<HefeiDataStruct>();

            List<HefeiDataStruct> receiveOrdersource = new List<HefeiDataStruct>();//只记录没要还货记录的数据

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //记录收货但是没有还货的记录               
                cmd.CommandText = "select A.track_serial_no,A.custom_order,A.custom_serial_no,A.mpn,A.dpk_status, " +//5列
                    " A.custommaterialNo,A.custom_machine_type,A.vendor,A.mb_brief,A.mb_make_date,A.order_receive_date," +//6列
                    "A.guarantee, A.custom_fault, A.warranty_period" +//3
                    " from DeliveredTable as A where  A.order_receive_date between '"
                    + startTime + "' and '" + endTime + "' and A.vendor ='" + this.vendorComboBox.Text.Trim() + "' and A.product='" + this.productComboBox.Text.Trim() + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    HefeiDataStruct temp = new HefeiDataStruct();
                    temp.tracker_no_receive = querySdr[0].ToString();
                    temp.REPAIR_CENTER = "HWB";
                    temp.RMA_NO = querySdr[1].ToString();
                    temp.PRODUCT = "MB";
                    temp.PRODUCT_SN = querySdr[2].ToString();
                    temp.SHIPPING_SN = "";
                    temp.LCFC_PN = querySdr[3].ToString();
                    temp.OS_VERSION = querySdr[4].ToString();
                    temp.LNV_FRU_PN = querySdr[5].ToString();
                    temp.LNV_MODEL_NAME = querySdr[6].ToString();
                    temp.LNV_SERIES = "";
                    //temp.PRODUCT = querySdr[7].ToString();
                    temp.LCFC_MODEL_NAME = querySdr[8].ToString();
                    temp.PRODUCT_DAY_CODE = querySdr[9].ToString();
                    temp.RETURN_AREA = "CHINA";
                    temp.SERVICE_REQUESTER = "LENOVO";
                    temp.SHIP_BACK_DATE = querySdr[10].ToString();//收货日期
                    temp.SVC_RECEIVE_DATE = querySdr[10].ToString();//同一天
                    temp.SERVICE_TYPE = (querySdr[11].ToString() == "保内" ? "IW" : "OOW");
                    temp.INCOMING_INSPECTION = "";
                    temp.LINE_INPUT_DATE = querySdr[10].ToString();
                    temp.REPAIR_START_DATE = querySdr[10].ToString();
                    temp.PACKING_DATE = "";//还货日期
                    temp.DELIVERY_DATE = "";
                    temp.CLOSE_DATE = "";
                    temp.NORMAL_SYMPTOM = querySdr[12].ToString();
                    temp.tracker_no_return = querySdr[0].ToString();//还没有还货的使用收货跟踪条码替代
                    temp.Q_A_Result = "";
                    temp.DELIVERY = "Y";
                    temp.TAT_TARGET = "7";
                    temp.warranty_period = querySdr[13].ToString();

                    temp.repairDetailList = new List<repairDetail>();



                    receiveOrdersource.Add(temp);

                }
                querySdr.Close();

                //记录还货的记录
                cmd.CommandText = "select A.track_serial_no,A.custom_order,A.custom_serial_no,B.custom_serial_no,A.mpn,A.dpk_status, "+//6列
                    " A.custommaterialNo,A.custom_machine_type,A.vendor,A.mb_brief,A.mb_make_date,A.order_receive_date," +//6列
                    "A.guarantee, B._status,B.return_date,A.custom_fault,B.track_serial_no, A.warranty_period" +//5
                    " from DeliveredTable as A, returnStore as B where A.receiveOrderIndex = B.returnOrderIndex and B.return_date between '" 
                    + startTime + "' and '" + endTime + "' and B.vendor ='"+this.vendorComboBox.Text.Trim()+"' and B.product='"+this.productComboBox.Text.Trim()+"'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    HefeiDataStruct temp = new HefeiDataStruct();
                    temp.tracker_no_receive = querySdr[0].ToString();
                    temp.REPAIR_CENTER = "HWB";
                    temp.RMA_NO = querySdr[1].ToString();
                    temp.PRODUCT = "MB";
                    temp.PRODUCT_SN = querySdr[2].ToString();
                    temp.SHIPPING_SN = querySdr[3].ToString();
                    temp.LCFC_PN = querySdr[4].ToString();
                    temp.OS_VERSION = querySdr[5].ToString();
                    temp.LNV_FRU_PN = querySdr[6].ToString();
                    temp.LNV_MODEL_NAME = querySdr[7].ToString();
                    temp.LNV_SERIES = "";
                    //temp.PRODUCT = querySdr[8].ToString();
                    temp.LCFC_MODEL_NAME = querySdr[9].ToString();
                    temp.PRODUCT_DAY_CODE = querySdr[10].ToString();
                    temp.RETURN_AREA = "CHINA";
                    temp.SERVICE_REQUESTER = "LENOVO";
                    temp.SHIP_BACK_DATE = querySdr[11].ToString();//收货日期
                    temp.SVC_RECEIVE_DATE = querySdr[11].ToString();//同一天
                    temp.SERVICE_TYPE = (querySdr[12].ToString() == "保内" ? "IW" : "OOW");
                    temp.INCOMING_INSPECTION = (querySdr[13].ToString() == "良品" ? "IW" : "CID");
                    if (temp.INCOMING_INSPECTION == "CID")
                    {
                        temp.SHIPPING_SN = temp.SHIPPING_SN+","+temp.PRODUCT_SN;
                    }
                    temp.LINE_INPUT_DATE = querySdr[11].ToString();
                    temp.REPAIR_START_DATE = querySdr[11].ToString();
                    temp.PACKING_DATE = querySdr[14].ToString();//还货日期
                    temp.DELIVERY_DATE = querySdr[14].ToString();
                    temp.CLOSE_DATE = querySdr[14].ToString();
                    temp.NORMAL_SYMPTOM = querySdr[15].ToString();
                    temp.tracker_no_return = querySdr[16].ToString();
                    temp.Q_A_Result = "";
                    temp.DELIVERY = "Y";
                    temp.TAT_TARGET = "7";
                    temp.warranty_period = querySdr[17].ToString();

                    temp.repairDetailList = new List<repairDetail>();

                    foreach (HefeiDataStruct temp1 in receiveOrdersource)
                    {
                        if (temp1.tracker_no_receive.Trim() == temp.tracker_no_return.Trim()
                            || temp1.tracker_no_receive.Trim() == temp.tracker_no_receive.Trim())
                        {
                            receiveOrdersource.Remove(temp1);
                            break;
                        }
                    }

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (HefeiDataStruct temp in receiveOrdersource)
                {
                    receiveOrderList.Add(temp);
                }

                foreach (HefeiDataStruct temp in receiveOrderList)
                {
                    DateTime mb_makedate = Convert.ToDateTime(temp.PRODUCT_DAY_CODE);
                    DateTime mb_reciveDate = Convert.ToDateTime(temp.SVC_RECEIVE_DATE);
                    DateTime waranty_date = mb_makedate.AddMonths(Int16.Parse(temp.warranty_period.Replace('M', ' ').Trim()));
                    DateTime subOneDay = mb_reciveDate.AddDays(-1);
                    temp.WARRANTY_PERIOD = waranty_date.ToString("yyyy-MM-dd");
                    temp.REQUEST_DATE = subOneDay.ToString("yyyy-MM-dd");
                    temp.APPROVAL_DATE = subOneDay.ToString("yyyy-MM-dd");

                    if (temp.SHIP_BACK_DATE != "" && temp.PACKING_DATE != "")
                    {
                        DateTime dt1 = Convert.ToDateTime(temp.SHIP_BACK_DATE);
                        DateTime dt2 = Convert.ToDateTime(temp.PACKING_DATE);
                        TimeSpan ts = dt2.Subtract(dt1);
                        int overdays = ts.Days;
                        if (overdays > 7)
                        {
                            temp.TAT_HIT = "N";
                        }
                        else
                        {
                            temp.TAT_HIT = "Y";
                        }
                        temp.TAT = overdays + "";
                    }

                    cmd.CommandText = "select top 1 test_date from test1table where track_serial_no='" + temp.tracker_no_return + "' order by Id desc";
                    querySdr = cmd.ExecuteReader();
                    temp.FINAL_TEST_DATE =null;
                    while (querySdr.Read())
                    {
                        temp.FINAL_TEST_DATE = querySdr[0].ToString();
                        temp.WH_TAKEIN_DATE = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if(temp.FINAL_TEST_DATE == null)
                    {
                        cmd.CommandText = "select top 1 test_date from testalltable where track_serial_no='" + temp.tracker_no_return + "' order by Id desc";
                        querySdr = cmd.ExecuteReader();
                        temp.FINAL_TEST_DATE =null;
                        while (querySdr.Read())
                        {
                             temp.FINAL_TEST_DATE = querySdr[0].ToString();
                             temp.WH_TAKEIN_DATE = querySdr[0].ToString();
                        }
                        querySdr.Close();
                    }

                    //维修记录，首先查询BGA然后查询小材料       
                  
                    cmd.CommandText = "select BGA_place, bgatype,BGAPN from bga_repair_record_table where track_serial_no ='" + temp.tracker_no_return + "' and newSn !='' order by Id desc";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairDetail repair = new repairDetail();
                        repair.REPAIR1_LOCATION = querySdr[0].ToString();
                        repair.REPAIR1_ACTION = "CHANGE";
                        repair.REPAIR1_PARTS= querySdr[1].ToString();
                        repair.REPAIR1_USED_MATERIAL_CODE = querySdr[2].ToString();
                        repair.REPAIR1_USED_MATERIAL_QTY = "1";
                        repair.REPAIR1_RESULT ="REPAIR GOOD";
                        repair.REPAIR1_RESPONSIBILITY ="NORMAL";

                        temp.repairDetailList.Add(repair);
                    }
                    querySdr.Close();

                    cmd.CommandText = "select stock_place,material_mpn,_action,thisNumber from fru_smt_used_record where track_serial_no ='" + temp.tracker_no_return + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        repairDetail repair = new repairDetail();
                        repair.REPAIR1_LOCATION = querySdr[0].ToString();
                        repair.REPAIR1_USED_MATERIAL_CODE = querySdr[1].ToString();
                        if (querySdr[2].ToString().Trim() != "")
                        {
                            repair.REPAIR1_PARTS = "";
                            repair.REPAIR1_USED_MATERIAL_QTY = "";
                            repair.REPAIR1_RESULT = "";
                            repair.REPAIR1_RESPONSIBILITY = "";
                        }
                        string action = querySdr[3].ToString().Trim();
                        if (action.Contains("清洁") || action.Contains("加焊"))
                        {
                            repair.REPAIR1_ACTION = "MAINTAIN";
                        }
                        else if (action.Contains("BIOS") || action.Contains("软体"))
                        {
                            repair.REPAIR1_ACTION = "UPDATE";
                        }
                        else if (action.Contains("更换"))
                        {
                            repair.REPAIR1_ACTION = "CHANGE";
                        }

                        temp.repairDetailList.Add(repair);
                    }
                    querySdr.Close();

                    if (temp.INCOMING_INSPECTION == "CID")
                    {
                        temp.REPAIR_RESULT = "NO REPAIR";
                        temp.REPAIR_RESPONSIBILITY = "CUSTOMER";
                        temp.RMA_STATUS = "COMPLETE";
                    }
                    else if (temp.repairDetailList.Count == 0)
                    {
                        temp.REPAIR_RESULT = "NDF";
                        temp.REPAIR_RESPONSIBILITY = "";
                        temp.RMA_STATUS = "CLOSE";
                    }
                    else
                    {
                        temp.REPAIR_RESULT = "REPAIR GOOD";
                        temp.REPAIR_RESPONSIBILITY = "NORMAL";
                        temp.RMA_STATUS = "COMPLETE";
                    }

                    //如果不够5个数据，则补齐
                    while (temp.repairDetailList.Count < 5)
                    {
                        repairDetail repair = new repairDetail();
                        temp.repairDetailList.Add(repair);
                    }

                    //差一个RRR_90 TODO
                    //规则：根据收货序号，查询其收货日期，然后查这个收货日期之前的还货记录，查最近的一次还货记录，如果在90天内则为1，否则为0
                    if (temp.LINE_INPUT_DATE != "")
                    {
                        string receiveDateEnd = temp.LINE_INPUT_DATE;
                        string lastReturnDateStart = Convert.ToDateTime(receiveDateEnd).AddDays(-90).ToString("yyyy-MM-dd");
                        cmd.CommandText = "select top 1 return_date from returnStore where return_date between '"
                        + lastReturnDateStart + "' and '" + receiveDateEnd + "' and custom_serial_no='" + temp.PRODUCT_SN + "'";
                        querySdr = cmd.ExecuteReader();
                        if (querySdr.HasRows)
                        {
                            temp.RRR_90 = "1";
                        }
                        else
                        {
                            temp.RRR_90 = "0";
                        }
                        querySdr.Close();
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

        public void generateExcelToCheck(List<HefeiDataStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
            titleList.Add("还货跟踪条码");
            titleList.Add("REPAIR_CENTER");
            titleList.Add("RMA_NO");
            titleList.Add("PRODUCT");
            titleList.Add("PRODUCT_SN");
            titleList.Add("SHIPPING_SN");
            titleList.Add("LCFC_PN");
            titleList.Add("OS_VERSION");
            titleList.Add("LNV_FRU_PN");
            titleList.Add("LNV_MODEL_NAME");
            titleList.Add("LNV_SERIES");
            titleList.Add("PRODUCT_GROUP");
            titleList.Add("LCFC_MODEL_NAME");
            titleList.Add("PRODUCT_DAY_CODE");
            titleList.Add("RETURN_AREA");
            titleList.Add("SERVICE_REQUESTER");
            titleList.Add("WARRANTY_PERIOD");
            titleList.Add("REQUEST_DATE");
            titleList.Add("APPROVAL_DATE");
            titleList.Add("SHIP_BACK_DATE");
            titleList.Add("SVC_RECEIVE_DATE");
            titleList.Add("SERVICE_TYPE");
            titleList.Add("INCOMING_INSPECTION");
            titleList.Add("LINE_INPUT_DATE");
            titleList.Add("REPAIR_START_DATE");
            titleList.Add("FINAL_TEST_DATE");
            titleList.Add("WH_TAKEIN_DATE");
            titleList.Add("PACKING_DATE");
            titleList.Add("DELIVERY_DATE");
            titleList.Add("CLOSE_DATE");
            titleList.Add("NORMAL_SYMPTOM");

            for (int i = 1; i <= 5; i++)
            {
                titleList.Add("REPAIR"+i+"_LOCATION");
                titleList.Add("REPAIR" + i + "_ACTION");
                titleList.Add("REPAIR" + i + "_PARTS");
                titleList.Add("REPAIR" + i + "_USED_MATERIAL_CODE");
                titleList.Add("REPAIR" + i + "_USED_MATERIAL_QTY");
                titleList.Add("REPAIR" + i + "_RESULT");
                titleList.Add("REPAIR" + i + "_RESPONSIBILITY");
            }
       
            titleList.Add("REPAIR_RESULT");
            titleList.Add("REPAIR_RESPONSIBILITY");
            titleList.Add("Q_A_Result");
            titleList.Add("DELIVERY");
            titleList.Add("RMA_STATUS");
            titleList.Add("TAT");
            titleList.Add("TAT_TARGET");    
            titleList.Add("TAT_HIT");
            titleList.Add("RRR_90");

            foreach (HefeiDataStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add(stockcheck.tracker_no_receive);
                ct1.Add(stockcheck.REPAIR_CENTER);
                ct1.Add(stockcheck.RMA_NO);
                ct1.Add(stockcheck.PRODUCT);
                ct1.Add(stockcheck.PRODUCT_SN);
                ct1.Add(stockcheck.SHIPPING_SN);
                ct1.Add(stockcheck.LCFC_PN);
                ct1.Add(stockcheck.OS_VERSION);
                ct1.Add(stockcheck.LNV_FRU_PN);
                ct1.Add(stockcheck.LNV_MODEL_NAME);
                ct1.Add(stockcheck.LNV_SERIES);
                ct1.Add(stockcheck.PRODUCT_GROUP);
                ct1.Add(stockcheck.LCFC_MODEL_NAME);
                ct1.Add(stockcheck.PRODUCT_DAY_CODE!=null ? stockcheck.PRODUCT_DAY_CODE.Substring(0,stockcheck.PRODUCT_DAY_CODE.IndexOf(" ")).Trim():"");
                ct1.Add(stockcheck.RETURN_AREA);
                ct1.Add(stockcheck.SERVICE_REQUESTER);
                ct1.Add(stockcheck.WARRANTY_PERIOD);
                ct1.Add(stockcheck.REQUEST_DATE);
                ct1.Add(stockcheck.APPROVAL_DATE);
                ct1.Add(stockcheck.SHIP_BACK_DATE != null ? stockcheck.SHIP_BACK_DATE.Substring(0, stockcheck.SHIP_BACK_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add(stockcheck.SVC_RECEIVE_DATE != null ? stockcheck.SVC_RECEIVE_DATE.Substring(0, stockcheck.SVC_RECEIVE_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add(stockcheck.SERVICE_TYPE);
                ct1.Add(stockcheck.INCOMING_INSPECTION);
                ct1.Add(stockcheck.LINE_INPUT_DATE != null ? stockcheck.LINE_INPUT_DATE.Substring(0, stockcheck.LINE_INPUT_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add(stockcheck.REPAIR_START_DATE != null ? stockcheck.REPAIR_START_DATE.Substring(0, stockcheck.REPAIR_START_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add((stockcheck.FINAL_TEST_DATE != null &&stockcheck.FINAL_TEST_DATE !="") ? stockcheck.FINAL_TEST_DATE.Substring(0, stockcheck.FINAL_TEST_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add((stockcheck.WH_TAKEIN_DATE != null && stockcheck.WH_TAKEIN_DATE !="") ? stockcheck.WH_TAKEIN_DATE.Substring(0, stockcheck.WH_TAKEIN_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add((stockcheck.PACKING_DATE != null && stockcheck.PACKING_DATE != "") ? stockcheck.PACKING_DATE.Substring(0, stockcheck.PACKING_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add((stockcheck.DELIVERY_DATE != null && stockcheck.DELIVERY_DATE != "") ? stockcheck.DELIVERY_DATE.Substring(0, stockcheck.DELIVERY_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add((stockcheck.CLOSE_DATE != null && stockcheck.CLOSE_DATE != "") ? stockcheck.CLOSE_DATE.Substring(0, stockcheck.CLOSE_DATE.IndexOf(" ")).Trim() : "");
                ct1.Add(stockcheck.NORMAL_SYMPTOM);

                for (int i = 0; i < 5; i++)//至少包括5个数据，前面已经补齐
                {
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_LOCATION);
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_ACTION);
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_PARTS);
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_USED_MATERIAL_CODE);
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_USED_MATERIAL_QTY);
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_RESULT);
                    ct1.Add(stockcheck.repairDetailList[i].REPAIR1_RESPONSIBILITY);
                }
       
                ct1.Add(stockcheck.REPAIR_RESULT);
                ct1.Add(stockcheck.REPAIR_RESPONSIBILITY);
                ct1.Add(stockcheck.Q_A_Result);
                ct1.Add(stockcheck.DELIVERY);
                ct1.Add(stockcheck.RMA_STATUS);
                ct1.Add(stockcheck.TAT);
                ct1.Add(stockcheck.TAT_TARGET);    
                ct1.Add(stockcheck.TAT_HIT);
                ct1.Add(stockcheck.RRR_90);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("合肥报表-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }   

    public class repairDetail
    {
        public string REPAIR1_LOCATION;//维修数据中其他1位置，NTF的无需填写  只需在BO列填写NDF，做此维修换件 需要把维修数据中的换件位置都合并之后再做
        public string REPAIR1_ACTION;//CHANGE(更换) , MAINTAIN(清洁、加焊) ,  UPDATE(更新BIOS)   , NO REPAIR(OOW/CID标注)   
        public string REPAIR1_PARTS;//CPU:维修数据中更换CPU芯片需填写 ,GPU:维修数据中更换VGA芯片需填写 ,PCH:维修数据中更换南北桥需填写
        public string REPAIR1_USED_MATERIAL_CODE;//维修数据中更换：南北桥&VGA芯片&CPU的料号
        public string REPAIR1_USED_MATERIAL_QTY;//固定字段  只要AG列为：CHANGE(更换) 的 此处全部填写“1”
        public string REPAIR1_RESULT;//固定字段   AF列有换件的 此处都填写：REPAIR GOOD
        public string REPAIR1_RESPONSIBILITY;//固定字段   AF列有换件的 此处都填写：NORMAL        
    }

    public class HefeiDataStruct
    {
        public string tracker_no_receive;
        public string tracker_no_return;
        public string REPAIR_CENTER;    //固定字段  "HWB"
        public string RMA_NO;    //订单编号
        public string PRODUCT;//固定字段 "MB"
        public string PRODUCT_SN;//收货系统的客户序号？
        public string SHIPPING_SN;//还货系统的客户序号？
        public string LCFC_PN;//MPN
        public string OS_VERSION;//DPK状态
        public string LNV_FRU_PN;//客户料号
        public string LNV_MODEL_NAME;//客户机型
        public string LNV_SERIES;//？？
        public string PRODUCT_GROUP;//客户别
        public string LCFC_MODEL_NAME;//MB简称
        public string PRODUCT_DAY_CODE;//MB生产日期
        public string RETURN_AREA;//固定字段"CHINA"
        public string SERVICE_REQUESTER;//固定字段"LENOVO"
        public string WARRANTY_PERIOD;//生产日期算出的保修期
        public string warranty_period;//保修，单位：月
        public string REQUEST_DATE;//收货日期减去1天
        public string APPROVAL_DATE;//收货日期减去1天
        public string SHIP_BACK_DATE;//收货日期
        public string SVC_RECEIVE_DATE;//收货日期
        public string SERVICE_TYPE;//收货系统的保内保外判断     保内：IW   保外：OOW
        public string INCOMING_INSPECTION;//IW：还货数据中的良品还货  CID:还货数据中的不良品还货  OOW：收货数据中的保外
        public string LINE_INPUT_DATE;//收货日期
        public string REPAIR_START_DATE;//收货日期
        public string FINAL_TEST_DATE;//测试一中的测试时间
        public string WH_TAKEIN_DATE;//测试一中的测试时间
        public string PACKING_DATE;//还货日期
        public string DELIVERY_DATE;//还货日期
        public string CLOSE_DATE;//还货日期
        public string NORMAL_SYMPTOM;//收货数据中客户故障
        public List<repairDetail> repairDetailList;//5个,? 问题是如何弄出5个字段出来，注意title的变化，从1到5
       
        public string REPAIR_RESULT;//固定字段   AF列有换件的 此处都填写：REPAIR GOOD      NTF的填写NDF
        public string REPAIR_RESPONSIBILITY;//固定字段   AF列有换件的 此处都填写：NORMAL       NTF的为空
        public string Q_A_Result;//空  无需填写
        public string DELIVERY;//？？根据F列来看，如果归还就为Y，未未归还为N
        public string RMA_STATUS;//OPEN：未还主板           CLOSE：已还主板无维修换件 COMPLETE：不良品归还主板&良品归还已有维修数据主板
        public string TAT;//AC列减去U列，收货减还货？？？
        public string TAT_TARGET;//固定字段“7”
    
        public string TAT_HIT;//看BT列小于等于7天为Y  大于7天为N
        public string RRR_90;//归还的主板二次返回数据，新系统中未做？？
    }    
}
