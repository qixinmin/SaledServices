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

            List<HefeiDataStruct> receiveOrderList = new List<HefeiDataStruct>();

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
                    HefeiDataStruct temp = new HefeiDataStruct();
                  

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (HefeiDataStruct temp in receiveOrderList)
                {
                   // cmd.CommandText = "select order_receive_date,mb_brief,guarantee,custom_machine_type,mb_make_date,custom_fault from DeliveredTable where track_serial_no='" + temp.wip_sn + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        //temp.receive_date = querySdr[0].ToString();
                        //temp.model_name = querySdr[1].ToString();
                        //temp.compal_warranty = querySdr[2].ToString() == "保内" ? "IW" : "OOW";
                        //temp.dell_market_model = querySdr[3].ToString();
                        //temp.mb_make_date = querySdr[4].ToString();
                        //temp.customer_fault = querySdr[5].ToString();
                        //break;
                    }
                    querySdr.Close();

                   // cmd.CommandText = "select vendormaterialNo from MBMaterialCompare where custommaterialNo='" + temp.cust_pn + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        //temp.sap_compal_pn = querySdr[0].ToString();
                        //break;
                    }
                    querySdr.Close();

                  //  cmd.CommandText = "select BGA_place from bga_repair_record_table where track_serial_no ='" + temp.wip_sn + "' and newSn !=''";
                    querySdr = cmd.ExecuteReader();
                    //temp.repair_recodes = "";
                    while (querySdr.Read())
                    {
                        //temp.repair_recodes += querySdr[0].ToString();
                        //temp.repair_recodes += ":";
                    }
                    querySdr.Close();

                   // cmd.CommandText = "select stock_place from fru_smt_used_record where track_serial_no ='" + temp.wip_sn + "'";
                    querySdr = cmd.ExecuteReader();
                   
                    while (querySdr.Read())
                    {
                        //temp.repair_recodes += querySdr[0].ToString();
                        //temp.repair_recodes += ":";
                    }
                    querySdr.Close();
                }

                foreach (HefeiDataStruct temp in receiveOrderList)
                {
                   
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
                //ct1.Add(stockcheck.repair_site);
                //ct1.Add(stockcheck.customer_no);
                //if (stockcheck.receive_date != null)
                //{
                //    ct1.Add(stockcheck.receive_date.Replace("0:00:00", "").Trim());
                //}
                //else
                //{
                //    ct1.Add(stockcheck.receive_date);
                //}

                //ct1.Add(stockcheck.region);
                //ct1.Add(stockcheck.rma_no);
                //ct1.Add(stockcheck.wip_sn);
                //ct1.Add(stockcheck.model_name);
                //ct1.Add(stockcheck.ssn);
                //ct1.Add(stockcheck.sap_compal_pn);
                //ct1.Add(stockcheck.return_times);
                //ct1.Add(stockcheck.kp_no);
                //ct1.Add(stockcheck.material_group);
                //ct1.Add(stockcheck.dps_tag_ron_no);
                //ct1.Add(stockcheck.cust_pn);

                //if (stockcheck.shipping_date != null)
                //{
                //    ct1.Add(stockcheck.shipping_date.Replace("0:00:00", "").Trim());
                //}
                //else
                //{
                //    ct1.Add(stockcheck.shipping_date);
                //}

                //ct1.Add(stockcheck.compal_warranty);
                //ct1.Add(stockcheck.final_status);
                //ct1.Add(stockcheck.disposition);
                //ct1.Add(stockcheck.duty);
                //ct1.Add(stockcheck.ccd_decision);
                //ct1.Add(stockcheck.dell_market_model);
                //ct1.Add(stockcheck.customer_fault);
                //if (stockcheck.mb_make_date != null)
                //{
                //    ct1.Add(stockcheck.mb_make_date.Replace("0:00:00", "").Trim());
                //}
                //else
                //{
                //    ct1.Add(stockcheck.mb_make_date);
                //}
                //ct1.Add(stockcheck.return_reason);

                //ct1.Add(stockcheck.repair_recodes);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\仁宝大数据1-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
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
        List<repairDetail> repairDetailList;//5个,? 问题是如何弄出5个字段出来，注意title的变化，从1到5
       
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
