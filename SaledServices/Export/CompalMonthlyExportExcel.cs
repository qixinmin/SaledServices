using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class CompalMonthlyExportExcel : Form
    {
        public CompalMonthlyExportExcel()
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

                //cmd.CommandText = "select yimaicode,yimaides,renbaocode,renbaodes from badcodes";
                //querySdr = cmd.ExecuteReader();
                //while (querySdr.Read())
                //{
                //    badcodescompare temp = new badcodescompare();

                //    temp.yimaicode = querySdr[0].ToString();
                //    temp.yimaides = querySdr[1].ToString();
                //    temp.renbaocode = querySdr[2].ToString();
                //    temp.renbaodes = querySdr[3].ToString();
                //    badcodeslist.Add(temp);
                //}
                //querySdr.Close();

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

            List<CompalMonthlyDataStruct> receiveOrderList = new List<CompalMonthlyDataStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //记录还货的记录
                cmd.CommandText = "select A.track_serial_no,A.custom_order,A.mb_brief,A.custommaterialNo,A.custom_serial_no,B.custom_serial_no,A.custom_fault, " +                 
                    " B.return_date,B._status  from DeliveredTable as A, returnStore as B where A.receiveOrderIndex = B.returnOrderIndex and B.return_date between '" 
                    + startTime + "' and '" + endTime + "' and B.vendor ='"+this.vendorComboBox.Text.Trim()+"' and B.product='"+this.productComboBox.Text.Trim()+"'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    CompalMonthlyDataStruct temp = new CompalMonthlyDataStruct();
                    temp.tracker_no_receive = querySdr[0].ToString();
                    temp.RMACaseID = querySdr[1].ToString();
                    temp.Region = "";
                    temp.TPR = "";
                    temp.Model = querySdr[2].ToString();
                    temp.P_N = querySdr[3].ToString();
                    temp.PPID_IN = querySdr[4].ToString();
                    temp.PPID_OUT = querySdr[5].ToString();
                    temp.FAILURE_DESCRIPTIONS_Customer = querySdr[6].ToString();
                    temp.ERROR_CODE = "";
                    temp.FAILURE_DESCRIPTIONS_Compal = "";
                    temp.PART = "";

                    temp.ShippingDate = querySdr[7].ToString();
                    temp.repairDetailList = new List<repairDetail>();
                    temp.INCOMING_INSPECTION = (querySdr[8].ToString() == "良品" ? "IW" : "CID");
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (CompalMonthlyDataStruct temp in receiveOrderList)
                {
                    DateTime shipdate = Convert.ToDateTime(temp.ShippingDate);
                    DateTime subOneDay = shipdate.AddDays(-1);
                    temp.ReceivedDate = subOneDay.ToString("yyyy-MM-dd");

                    cmd.CommandText = "  select COUNT(*) from DeliveredTable where custom_serial_no ='"+temp.PPID_IN+"'";
                    querySdr = cmd.ExecuteReader();                    
                    while (querySdr.Read())
                    {
                        temp.Return_times = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    //维修记录，首先查询BGA然后查询小材料                  
                    cmd.CommandText = "select BGA_place, bgatype,BGAPN,bga_repair_date from bga_repair_record_table where track_serial_no ='" + temp.tracker_no_receive + "' and newSn !='' order by Id desc";
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
                        temp.RepairMonth = querySdr[3].ToString();

                        temp.repairDetailList.Add(repair);
                    }
                    querySdr.Close();

                    cmd.CommandText = "select stock_place,material_mpn,_action,thisNumber,input_date from fru_smt_used_record where track_serial_no ='" + temp.tracker_no_receive + "'";
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

                        temp.RepairMonth = querySdr[4].ToString();

                        temp.repairDetailList.Add(repair);
                    }
                    querySdr.Close();

                    if (temp.INCOMING_INSPECTION == "CID")
                    {
                        temp.CATEGORY = "CID";
                    }
                    else if (temp.repairDetailList.Count == 0)
                    {
                        temp.CATEGORY = "NTF";                       
                    }
                    else
                    {
                        temp.CATEGORY = "ROK";
                    }

                    foreach (repairDetail repairtemp in temp.repairDetailList)
                    {
                        temp.LOCATION += repairtemp.REPAIR1_LOCATION+",";
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

        public void generateExcelToCheck(List<CompalMonthlyDataStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
       
            titleList.Add("No");
            titleList.Add("RMACaseID");
            titleList.Add("Region");
            titleList.Add("TPR");
            titleList.Add("Model");
            titleList.Add("P N");
            titleList.Add("PPID_IN");
            titleList.Add("PPID_OUT");
            titleList.Add("Return times");
            titleList.Add("FAILURE DESCRIPTIONS-Customer");
            titleList.Add("ERROR CODE");
            titleList.Add("FAILURE DESCRIPTIONS-Compal");
            titleList.Add("PART");
            titleList.Add("LOCATION");
            titleList.Add("CATEGORY");
            titleList.Add("RepairMonth");
            titleList.Add("ShippingDate");
            titleList.Add("ReceivedDate");

            int i = 1;
            foreach (CompalMonthlyDataStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                stockcheck.No =(i++ )+"";
                ct1.Add(stockcheck.No);              
                ct1.Add(stockcheck.RMACaseID);
                ct1.Add(stockcheck.Region);
                ct1.Add(stockcheck.TPR);
                ct1.Add(stockcheck.Model);
                ct1.Add(stockcheck.P_N);
                ct1.Add(stockcheck.PPID_IN);
                ct1.Add(stockcheck.PPID_OUT);
                ct1.Add(stockcheck.Return_times);
                ct1.Add(stockcheck.FAILURE_DESCRIPTIONS_Customer);
                ct1.Add(stockcheck.ERROR_CODE);
                ct1.Add(stockcheck.FAILURE_DESCRIPTIONS_Compal);
                ct1.Add(stockcheck.PART);
                ct1.Add(stockcheck.LOCATION);
                ct1.Add(stockcheck.CATEGORY);
                ct1.Add(Utils.modifyDataFormat(stockcheck.RepairMonth));
                ct1.Add(Utils.modifyDataFormat(stockcheck.ShippingDate));
                ct1.Add(stockcheck.ReceivedDate);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("Compal月报表-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class CompalMonthlyDataStruct
    {
        //No	RMACaseID	Region	TPR	Model	P/N	PPID_IN	PPID_OUT	Return times	
        //FAILURE DESCRIPTIONS-Customer	ERROR CODE	FAILURE DESCRIPTIONS-Compal	PART	LOCATION	CATEGORY	RepairMonth	ShippingDate	ReceivedDate	
        public string No;
        public string RMACaseID;
        public string Region;    
        public string TPR;     
        public string Model; 
        public string P_N; 
        public string PPID_IN; 
        public string PPID_OUT; 
        public string Return_times;
        public string FAILURE_DESCRIPTIONS_Customer;
        public string ERROR_CODE;
        public string FAILURE_DESCRIPTIONS_Compal;
        public string PART;
        public string LOCATION;
        public string CATEGORY;
        public string RepairMonth;
        public string ShippingDate;
        public string ReceivedDate;

        public string tracker_no_receive;
        public List<repairDetail> repairDetailList;
        public string INCOMING_INSPECTION;//IW：还货数据中的良品还货  CID:还货数据中的不良品还货  OOW：收货数据中的保外
    }    
}
