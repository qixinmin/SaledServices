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
    public partial class ReturnOrderExport : Form
    {
        public ReturnOrderExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<ReturnOrderStruct> receiveOrderList = new List<ReturnOrderStruct>();

            List<goodSumStruct> goodSumStructList = new List<goodSumStruct>();

            List<cidSumStruct> cidSumStructList = new List<cidSumStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id,vendor,product,return_file_no,storehouse,return_date,orderno,custommaterialNo,"+
                    "dpkpn,track_serial_no,custom_serial_no,vendor_serail_no,vendormaterialNo,_status,custom_res_type,"+
                    "response_describe,tat,inputuser,lenovo_maintenance_no,lenovo_repair_no from returnStore where return_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReturnOrderStruct temp = new ReturnOrderStruct();
                    temp.Id =  querySdr[0].ToString();
                    temp.vendor =  querySdr[1].ToString();
                    temp.product = querySdr[2].ToString();
                    temp.return_file_no= querySdr[3].ToString();
                    temp.storehouse = querySdr[4].ToString();
                    temp.return_date = querySdr[5].ToString();
                    temp.orderno = querySdr[6].ToString();
                    temp.custommaterialNo = querySdr[7].ToString();
                    temp.dpkpn = querySdr[8].ToString();
                    temp.track_serial_no = querySdr[9].ToString();
                    temp.custom_serial_no = querySdr[10].ToString();
                    temp.vendor_serail_no = querySdr[11].ToString();
                    temp.vendormaterialNo = querySdr[12].ToString();
                    temp._status = querySdr[13].ToString();
                    temp.custom_res_type = querySdr[14].ToString();
                    temp.response_describe = querySdr[15].ToString();
                    temp.tat = querySdr[16].ToString();
                    temp.inputuser = querySdr[17].ToString();
                    temp.lenovo_maintenance_no = querySdr[18].ToString();
                    temp.lenovo_repair_no = querySdr[19].ToString();

                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                foreach (ReturnOrderStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select order_receive_date,custom_fault,lenovo_custom_service_no,mb_brief from DeliveredTable where track_serial_no='" + temp.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.receive_date = querySdr[0].ToString();
                        temp.custom_fault = querySdr[1].ToString();
                        temp.lenovo_custom_service_no = querySdr[2].ToString();
                        temp.mb_brief = querySdr[3].ToString();
                        break;
                    }
                    querySdr.Close();


                    DateTime dt1 = Convert.ToDateTime(temp.receive_date);
                    DateTime dt2 = Convert.ToDateTime(temp.return_date);
                    TimeSpan ts = dt2.Subtract(dt1);
                    int overdays = ts.Days;                   
                    temp.tat = overdays + "";
                }

                if (this.goodsum.Checked)
                {
                    foreach (ReturnOrderStruct temp in receiveOrderList)
                    {
                        if (temp._status == "良品")
                        {
                            goodSumStruct temp1 = new goodSumStruct();
                            temp1.ramno = temp.orderno;
                            temp1.returndate = temp.return_date;
                            temp1.returnfileNo = temp.return_file_no;
                            temp1.custommaterialNo = temp.custommaterialNo;
                            if (goodSumStructList.Count == 0)
                            {
                                goodSumStructList.Add(temp1);
                            }
                            else
                            {
                                bool exist = false;
                                foreach (goodSumStruct oldrecord in goodSumStructList)
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
                                    goodSumStructList.Add(temp1);
                                }
                            }

                        }
                    }
                    foreach (goodSumStruct oldrecord in goodSumStructList)
                    {
                        cmd.CommandText = "select mb_brief from MBMaterialCompare where custommaterialNo='" + oldrecord.custommaterialNo + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            oldrecord.mb_brief = querySdr[0].ToString();
                            break;
                        }
                        querySdr.Close();
                    }
                }
                else if (this.cidsum.Checked)
                {
                    foreach (ReturnOrderStruct temp in receiveOrderList)
                    {
                        if (temp._status == "不良品")
                        {
                            cidSumStruct temp1 = new cidSumStruct();
                            temp1.ramno = temp.orderno;
                            temp1.returndate = temp.return_date;
                            temp1.custommaterialNo = temp.custommaterialNo;
                            if (cidSumStructList.Count == 0)
                            {
                                cidSumStructList.Add(temp1);
                            }
                            else
                            {
                                bool exist = false;
                                foreach (cidSumStruct oldrecord in cidSumStructList)
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
                                    cidSumStructList.Add(temp1);
                                }
                            }
                        }
                    }

                    foreach (cidSumStruct oldrecord in cidSumStructList)
                    {
                        cmd.CommandText = "select mb_brief from MBMaterialCompare where custommaterialNo='" + oldrecord.custommaterialNo + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            oldrecord.mb_brief = querySdr[0].ToString();
                            break;
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

            if (this.returndetail.Checked)
            {
                generateExcelToCheck(receiveOrderList, startTime, endTime);
            }
            else if (this.goodsum.Checked)
            {
                generategoodExcelToCheck(goodSumStructList, startTime, endTime);
            }
            else if (this.cidsum.Checked)
            {
                generatecidExcelToCheck(cidSumStructList, startTime, endTime);
            }           
        }

        public void generateExcelToCheck(List<ReturnOrderStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
            
            titleList.Add("ID");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("还货文件编号");
            titleList.Add("客户库别");
            titleList.Add("收货时间");
            titleList.Add("还货时间");
            titleList.Add("订单编号");
            titleList.Add("客户料号");
            titleList.Add("MB简称");
            titleList.Add("DPK状态");
            titleList.Add("跟踪条码");
            titleList.Add("客户序号");
            titleList.Add("厂商序号");
            titleList.Add("厂商料号");
            titleList.Add("状态");
            titleList.Add("客责类别");
            titleList.Add("客责描述");
            titleList.Add("联想维修站编号");    
            titleList.Add("联想维修单编号");
        
            titleList.Add("TAT");
       
             titleList.Add("客户故障");    
             titleList.Add("联想客服序号");
             titleList.Add("还货人");

            foreach (ReturnOrderStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
               
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.vendor);                
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.return_file_no);
                ct1.Add(stockcheck.storehouse);
                ct1.Add(stockcheck.receive_date);
                ct1.Add(stockcheck.return_date);                
                ct1.Add(stockcheck.orderno);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.mb_brief);      
                ct1.Add(stockcheck.dpkpn);
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.custom_serial_no);                
                ct1.Add(stockcheck.vendor_serail_no);                
                ct1.Add(stockcheck.vendormaterialNo);           
                ct1.Add(stockcheck._status);           
                ct1.Add(stockcheck.custom_res_type);           
                ct1.Add(stockcheck.response_describe);   
                ct1.Add(stockcheck.lenovo_maintenance_no);
                ct1.Add(stockcheck.lenovo_repair_no);
                ct1.Add(stockcheck.tat);           
                //ct1.Add(stockcheck.inputuser);     
                ct1.Add(stockcheck.custom_fault);
                ct1.Add(stockcheck.lenovo_custom_service_no);

                ct1.Add(stockcheck.inputuser);
               
                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("还货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }

        public void generategoodExcelToCheck(List<goodSumStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("还货时间");
            titleList.Add("还货文件编号");
            titleList.Add("订单编号");
            titleList.Add("客户料号");

            titleList.Add("MB简称");
            titleList.Add("数量");

            foreach (goodSumStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add(stockcheck.returndate);
                ct1.Add(stockcheck.returnfileNo);
                ct1.Add(stockcheck.ramno);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.returnNum.ToString());
                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("良品还货清单" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }

        public void generatecidExcelToCheck(List<cidSumStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("还货时间");
            titleList.Add("索赔订单");
            titleList.Add("客户料号");

            titleList.Add("MB简称");
            titleList.Add("数量");

            foreach (cidSumStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add(stockcheck.returndate);
                ct1.Add(stockcheck.ramno);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.returnNum.ToString());
                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }
            Utils.createExcel("CID还货清单" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class goodSumStruct
    {
        public string returndate;
        public string returnfileNo;
        public string ramno;
        public string custommaterialNo;
        public string mb_brief;
        public int returnNum=1;

        public bool equals(goodSumStruct temp)
        {
            return (this.returndate == temp.returndate 
                && this.returnfileNo == temp.returnfileNo 
                && this.custommaterialNo == temp.custommaterialNo 
                && this.ramno == temp.ramno);
        }

        public void incrementNum()
        {
            this.returnNum++;
        }
       
    }

    public class cidSumStruct
    {
        public string returndate;
        public string ramno;
        public string custommaterialNo;
        public string mb_brief;
        public int returnNum=1;

        public bool equals(cidSumStruct temp)
        {
            return (this.returndate == temp.returndate
                && this.custommaterialNo == temp.custommaterialNo
                && this.ramno == temp.ramno);
        }

        public void incrementNum()
        {
            this.returnNum++;
        }
    }

   public class ReturnOrderStruct
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

        public string custom_fault;
        public string lenovo_custom_service_no;

        public string mb_brief;
    }
}

