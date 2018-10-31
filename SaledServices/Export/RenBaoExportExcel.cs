using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class RenBaoExportExcel : Form
    {
        List<badcodescompare> badcodeslist = new List<badcodescompare>();//odescompare
        public RenBaoExportExcel()
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
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

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

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<RenBaoDataStruct> receiveOrderList = new List<RenBaoDataStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select return_date,orderno,track_serial_no,custommaterialNo,vendormaterialNo,custom_serial_no,_status,vendor_serail_no,custom_res_type from returnStore where return_date between '" 
                    + startTime + "' and '" + endTime + "' and vendor ='"+this.vendorComboBox.Text.Trim()+"' and product='"+this.productComboBox.Text.Trim()+"'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    RenBaoDataStruct temp = new RenBaoDataStruct();
                    temp.shipping_date = querySdr[0].ToString();
                    temp.rma_no = querySdr[1].ToString();
                    temp.wip_sn = querySdr[2].ToString();
                    temp.cust_pn = querySdr[3].ToString();
                    temp.kp_no = querySdr[4].ToString();
                    temp.ssn = querySdr[5].ToString();
                    temp.final_status = querySdr[6].ToString() == "良品" ?"Repair":"Scrap";
                    
                    temp.dps_tag_ron_no = querySdr[7].ToString();
                    temp.return_reason = querySdr[8].ToString();

                    temp.repair_site = "CCC-HWB";
                    temp.customer_no = "C38";
                    temp.region = "CCC";
                    temp.return_times = "1";
                    temp.material_group = "MB";
                    temp.ccd_decision = "";

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (RenBaoDataStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select order_receive_date,mb_brief,guarantee,custom_machine_type,mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + temp.wip_sn + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.receive_date = querySdr[0].ToString();
                        temp.model_name = querySdr[1].ToString();
                        temp.compal_warranty = querySdr[2].ToString() == "保内" ? "IW" : "OOW";
                        temp.dell_market_model = querySdr[3].ToString();
                        temp.mb_make_date = querySdr[4].ToString();
                        temp.customer_fault = querySdr[5].ToString();
                        break;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select vendormaterialNo from MBMaterialCompare where custommaterialNo='" + temp.cust_pn + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.sap_compal_pn = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

                    cmd.CommandText = "select BGA_place from bga_repair_record_table where track_serial_no ='" + temp.wip_sn + "' and newSn !=''";
                    querySdr = cmd.ExecuteReader();
                    temp.repair_recodes = "";
                    while (querySdr.Read())
                    {
                        temp.repair_recodes += querySdr[0].ToString();
                        temp.repair_recodes += ":";
                    }
                    querySdr.Close();

                    cmd.CommandText = "select stock_place from fru_smt_used_record where track_serial_no ='" + temp.wip_sn + "'";
                    querySdr = cmd.ExecuteReader();
                   
                    while (querySdr.Read())
                    {
                        temp.repair_recodes += querySdr[0].ToString();
                        temp.repair_recodes += ":";
                    }
                    querySdr.Close();
                }

                foreach (RenBaoDataStruct temp in receiveOrderList)
                {
                    if (temp.final_status == "Scrap")
                    {
                        temp.disposition = "OTH";
                        temp.duty = "CID";
                    }
                    if (temp.repair_recodes.Contains("J"))
                    {
                        temp.disposition = "OTH";
                        temp.duty = "CID";
                    }
                    else if(temp.repair_recodes.Replace(":","").Trim() == "")
                    {
                        temp.disposition = "CND";
                        temp.duty = "CND";
                    }
                    else
                    {
                        temp.disposition = "VFF";
                        temp.duty = "COMPAL_DUTY";
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (this.radioButton1.Checked)
            {
                generateExcelToCheck(receiveOrderList, startTime, endTime);
            }
            else if (this.radioButton2.Checked)
            {
                generateExcelToCheck2(receiveOrderList, startTime, endTime);
            }           
        }

        public void generateExcelToCheck2(List<RenBaoDataStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
            //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
            titleList.Add("RMA_NO");
            titleList.Add("SSN");
            titleList.Add("RECV_MB_VERSION");
            titleList.Add("REAL_DATE_CODE");
            titleList.Add("TEST_TIME");
            titleList.Add("REPAIR TIME");
            titleList.Add("STATION");
            titleList.Add("Error code");
            titleList.Add("ERROR DESC");
            titleList.Add("Location");
            titleList.Add("Action_code");
            titleList.Add("Action_desc");
            titleList.Add("Reason CODE");
            titleList.Add("Reason desc");
            titleList.Add("Customer FAILURE_SYMPTOM");

            foreach (RenBaoDataStruct stockcheck in StockCheckList)
            {
                if (stockcheck.final_status != "Scrap" && stockcheck.repair_recodes != "")
                {
                    string[] locations = stockcheck.repair_recodes.Split(':');
                    for (int i = 0; i < locations.Length - 1; i++)
                    {
                        ExportExcelContent ctest1 = new ExportExcelContent();
                        List<string> ct1 = new List<string>();
                        ct1.Add(stockcheck.rma_no);
                        ct1.Add(stockcheck.ssn);
                        ct1.Add(stockcheck.wip_sn);

                        DateTime makedate = Convert.ToDateTime(stockcheck.mb_make_date);

                        ct1.Add("Y" + stockcheck.mb_make_date.Substring(2, 2) + Utils.GetWeekOfYear(makedate));

                        DateTime receiveDate = Convert.ToDateTime(stockcheck.receive_date);
                        ct1.Add(receiveDate.AddDays(1).ToString("yyyy-MM-dd"));
                        ct1.Add(receiveDate.AddDays(1).ToString("yyyy-MM-dd"));

                        ct1.Add("QC1");
                        //stockcheck.customer_fault
                        string renbaocode="",renbaodes="";//aocodeaoDataStruct
                        foreach (badcodescompare temp in badcodeslist)
                        {
                            if (temp.yimaides.Trim() == stockcheck.customer_fault.Trim())
                            {
                                renbaocode = temp.renbaocode;
                                renbaodes = temp.renbaodes;
                                break;
                            }
                        }
                        ct1.Add(renbaocode);
                        ct1.Add(renbaodes);
                        ct1.Add(locations[i]);//multi
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add("");
                        ct1.Add(renbaodes);

                        ctest1.contentArray = ct1;
                        contentList.Add(ctest1);
                    }
                }
               
            }

            Utils.createExcel("D:\\仁宝大数据2-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }

        public void generateExcelToCheck(List<RenBaoDataStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
            //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
            titleList.Add("Repair Site");
            titleList.Add("CUSTOMER_NO");
            titleList.Add("RECEVING DATE");
            titleList.Add("REGION");
            titleList.Add("RMA_NO");
            titleList.Add("WIP_SN");
            titleList.Add("MODEL_NAME");
            titleList.Add("SSN");
            titleList.Add("SAP_COMPAL_PN");
            titleList.Add("RETURN_TIMES");
            titleList.Add("KP_NO");
            titleList.Add("MATERIAL_GROUP");
            titleList.Add("DPS/TAG/RON_NO");
            titleList.Add("CUST_PN");
            titleList.Add("SHIPPING_DATE");
            titleList.Add("COMPAL_WARRANTY");
            titleList.Add("FINAL_STATUS");
            titleList.Add("DISPOSITION");
            titleList.Add("DUTY");
            titleList.Add("CCD_DECISION");

            titleList.Add("Dell market model");
            titleList.Add("客户故障");
            titleList.Add("MB生产日期");
            titleList.Add("退货原因");
            titleList.Add("维修换件汇总");

            foreach (RenBaoDataStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.repair_site);
                ct1.Add(stockcheck.customer_no);
               
                ct1.Add(stockcheck.receive_date !=null ? stockcheck.receive_date.Replace("0:00:00", "").Trim():"");               

                ct1.Add(stockcheck.region);
                ct1.Add(stockcheck.rma_no);
                ct1.Add(stockcheck.wip_sn);
                ct1.Add(stockcheck.model_name);
                ct1.Add(stockcheck.ssn);
                ct1.Add(stockcheck.sap_compal_pn);
                ct1.Add(stockcheck.return_times);
                ct1.Add(stockcheck.kp_no);
                ct1.Add(stockcheck.material_group);
                ct1.Add(stockcheck.dps_tag_ron_no);
                ct1.Add(stockcheck.cust_pn);
               
                ct1.Add(stockcheck.shipping_date!=null ? stockcheck.shipping_date.Replace("0:00:00", "").Trim():"");               

                ct1.Add(stockcheck.compal_warranty);
                ct1.Add(stockcheck.final_status);
                ct1.Add(stockcheck.disposition);
                ct1.Add(stockcheck.duty);
                ct1.Add(stockcheck.ccd_decision);
                ct1.Add(stockcheck.dell_market_model);
                ct1.Add(stockcheck.customer_fault);
                
                ct1.Add(stockcheck.mb_make_date!=null ? stockcheck.mb_make_date.Replace("0:00:00", "").Trim():"");
                
                ct1.Add(stockcheck.return_reason);

                ct1.Add(stockcheck.repair_recodes);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\仁宝大数据1-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class badcodescompare
    {
        public string yimaicode;    //Repair Site(F)
        public string yimaides;    //CUSTOMER_NO(F)
        public string renbaocode;//RECEVING DATE
        public string renbaodes;//REGION(F)
    }

    public class RenBaoDataStruct
    {
        public string repair_site;    //Repair Site(F)
        public string customer_no;    //CUSTOMER_NO(F)
        public string receive_date;//RECEVING DATE
        public string region;//REGION(F)
        public string rma_no;//RMA_NO
        public string wip_sn;//WIP_SN
        public string model_name;//MODEL_NAME
        public string ssn;//SSN
        public string sap_compal_pn;//SAP_COMPAL_PN厂商料号(物料对照表中的名称,还货表中不存在)
        public string return_times;//RETURN_TIMES(F)
        public string kp_no;//KP_NO
        public string material_group;//MATERIAL_GROUP(F)
        public string dps_tag_ron_no;//DPS/TAG/RON_NO
        public string cust_pn;//CUST_PN
        public string shipping_date;//SHIPPING_DATE
        public string compal_warranty;//COMPAL_WARRANTY
        public string final_status;//FINAL_STATUS
        public string disposition;//DISPOSITION
        public string duty;//DUTY
        public string ccd_decision;//CCD_DECISION(F)
        public string dell_market_model;//Dell market model
        public string customer_fault;//客户故障
        public string mb_make_date;//MB生产日期
        public string return_reason;//退货原因
        public string repair_recodes;//维修换件汇总
    }
    
}
