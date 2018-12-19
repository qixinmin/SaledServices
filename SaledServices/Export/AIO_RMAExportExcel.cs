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
using SaledServices.Export;

namespace SaledServices
{
    public partial class AIO_RMAExportExcel : Form
    {
        public AIO_RMAExportExcel()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.productComboBox.Text == "" || this.vendorComboBox.Text == "")
            {
                MessageBox.Show("请选择厂商或客户别");
                return;
            }
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<RMAAIO_ReceiveOrderStruct> receiveOrderList = new List<RMAAIO_ReceiveOrderStruct>();
            List<RMAAIO_ReceiveOrderStruct> receiveOrderListTarget = new List<RMAAIO_ReceiveOrderStruct>();

            List<ReturnOrderStructAIO> returnOrderList = new List<ReturnOrderStructAIO>();
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select receiveOrder.vendor,receiveOrder.product,receiveOrder.storehouse,receiveOrder.orderno,receiveOrder.custom_materialNo,receiveOrder.mb_brief,mpn, dpk_type, receiveOrder.receivedNum,receiveOrder.returnNum,receiveOrder.receivedate from receiveOrder" +
                    " inner join MBMaterialCompare on custom_materialNo = MBMaterialCompare.custommaterialNo where receiveOrder.receivedate >='" + startTime + "' and receiveOrder.receivedate <='" + endTime + "' and  receiveOrder.vendor='"+this.vendorComboBox.Text+"' and receiveOrder.product='"+this.productComboBox.Text+"'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    RMAAIO_ReceiveOrderStruct temp = new RMAAIO_ReceiveOrderStruct();
                    temp.vendor = querySdr[0].ToString();
                    temp.product = querySdr[1].ToString();
                    temp.storehouse = querySdr[2].ToString();
                    temp.orderno = querySdr[3].ToString();
                    temp.custom_material_no = querySdr[4].ToString();
                    temp.mb_brief = querySdr[5].ToString();
                    temp.mpn = querySdr[6].ToString();
                    temp.dpktype = querySdr[7].ToString();

                    temp.receiveNum = querySdr[8].ToString();
                    temp.returnNum = querySdr[9].ToString();
                    temp.recievedate = querySdr[10].ToString();

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (RMAAIO_ReceiveOrderStruct temp in receiveOrderList)
                {
                    int diff =Int16.Parse(temp.receiveNum) - Int16.Parse(temp.returnNum);
                    temp.diffNum = diff+"";

                    DateTime dt1 = Convert.ToDateTime(temp.recievedate);
                    DateTime dt2 = DateTime.Now;
                 
                    TimeSpan ts = dt2.Subtract(dt1);
                    temp.tat = ts.Days.ToString();

                    cmd.CommandText = "select BGAPN from bga_wait_material_record_table where orderno ='" + temp.orderno + "' and customMaterialNo='"+temp.custom_material_no+"'";
                    querySdr = cmd.ExecuteReader();

                    while(querySdr.Read())
                    {
                        temp.reason = "待料";
                        temp.waitMaterial = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();

                    if (diff > 0)
                    {
                        receiveOrderListTarget.Add(temp);
                    }
                }

                cmd.CommandText = "select Id,vendor,product,return_file_no,storehouse,return_date,orderno,custommaterialNo," +
                   "dpkpn,track_serial_no,custom_serial_no,vendor_serail_no,vendormaterialNo,_status,custom_res_type," +
                   "response_describe,tat,inputuser,lenovo_maintenance_no,lenovo_repair_no from returnStore where return_date between '" + startTime + "' and '" + endTime + "'  and  returnStore.vendor='"+this.vendorComboBox.Text+"' and returnStore.product='"+this.productComboBox.Text+"'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReturnOrderStructAIO temp = new ReturnOrderStructAIO();
                    //temp.Id = querySdr[0].ToString();
                    temp.vendor = querySdr[1].ToString();
                    temp.product = querySdr[2].ToString();
                    temp.return_file_no = querySdr[3].ToString();
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
                    temp.tat = "";// querySdr[16].ToString();
                    temp.inputuser = querySdr[17].ToString();
                    temp.lenovo_maintenance_no = querySdr[18].ToString();
                    temp.lenovo_repair_no = querySdr[19].ToString();

                    returnOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (ReturnOrderStructAIO temp in returnOrderList)
                {
                    cmd.CommandText = "select order_receive_date,mb_brief from DeliveredTable where track_serial_no='" + temp.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.receive_date = querySdr[0].ToString();
                        temp.mb_brief = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    try
                    {
                        temp.order_make_date = Convert.ToDateTime(temp.receive_date).AddDays(-1).ToString("yyyy-MM-dd");

                        if (temp.receive_date != "" && temp.return_date != "")
                        {
                            DateTime dt1 = Convert.ToDateTime(temp.receive_date);
                            DateTime dt2 = Convert.ToDateTime(temp.return_date);
                            TimeSpan ts = dt2.Subtract(dt1);
                            int overdays = ts.Days;

                            temp.tat = overdays + "";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderListTarget,returnOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<RMAAIO_ReceiveOrderStruct> StockCheckList, List<ReturnOrderStructAIO> returnOrderList, string startTime, string endTime)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent sumContentAio = new allContent();
            sumContentAio.sheetName = "MB 未还在途";
            sumContentAio.titleList = new List<string>();
            sumContentAio.contentList = new List<object>();

          
            //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
            sumContentAio.titleList.Add("厂商");
            sumContentAio.titleList.Add("客户别");
            sumContentAio.titleList.Add("仓库别");
            sumContentAio.titleList.Add("订单编号");
            sumContentAio.titleList.Add("客户料号");
            sumContentAio.titleList.Add("MB简称");
            sumContentAio.titleList.Add("MPN");
            sumContentAio.titleList.Add("DPK类型");

            sumContentAio.titleList.Add("收货数量");
            sumContentAio.titleList.Add("还货数量");
            sumContentAio.titleList.Add("欠货数量");
            sumContentAio.titleList.Add("在途天数");
            sumContentAio.titleList.Add("超期原因");
            sumContentAio.titleList.Add("料号");
            

            foreach (RMAAIO_ReceiveOrderStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.storehouse);
                ct1.Add(stockcheck.orderno);
                ct1.Add(stockcheck.custom_material_no);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.dpktype);
                ct1.Add(stockcheck.receiveNum);
                ct1.Add(stockcheck.returnNum);
                ct1.Add(stockcheck.diffNum);
                ct1.Add(stockcheck.tat);
                ct1.Add(stockcheck.reason);
                ct1.Add(stockcheck.waitMaterial);

                ctest1.contentArray = ct1;
                sumContentAio.contentList.Add(ctest1);
            }
            allcontentList.Add(sumContentAio);


            allContent sumContentAio2 = new allContent();
            sumContentAio2.sheetName = "MB TAT";
            sumContentAio2.titleList = new List<string>();
            sumContentAio2.contentList = new List<object>();          

            //sumContentAio2.titleList.Add("ID");
            sumContentAio2.titleList.Add("厂商");
            sumContentAio2.titleList.Add("客户别");
            sumContentAio2.titleList.Add("还货文件编号");
            sumContentAio2.titleList.Add("客户库别");
            sumContentAio2.titleList.Add("做单时间");
            sumContentAio2.titleList.Add("收货时间");
            sumContentAio2.titleList.Add("还货时间");
            sumContentAio2.titleList.Add("订单编号");
            sumContentAio2.titleList.Add("客户料号");
            sumContentAio2.titleList.Add("MB简称");
            sumContentAio2.titleList.Add("DPK状态");
            sumContentAio2.titleList.Add("跟踪条码");
            sumContentAio2.titleList.Add("客户序号");
            sumContentAio2.titleList.Add("厂商序号");
            sumContentAio2.titleList.Add("厂商料号");
            //sumContentAio2.titleList.Add("状态");
            //sumContentAio2.titleList.Add("客责类别");
            //sumContentAio2.titleList.Add("客责描述");
            //sumContentAio2.titleList.Add("联想维修站编号");
            //sumContentAio2.titleList.Add("联想维修单编号");

            sumContentAio2.titleList.Add("TAT");

            foreach (ReturnOrderStructAIO stockcheck in returnOrderList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

               // ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.return_file_no);
                ct1.Add(stockcheck.storehouse);
                ct1.Add(stockcheck.order_make_date);
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
                //ct1.Add(stockcheck._status);
                //ct1.Add(stockcheck.custom_res_type);
                //ct1.Add(stockcheck.response_describe);
                //ct1.Add(stockcheck.lenovo_maintenance_no);
                //ct1.Add(stockcheck.lenovo_repair_no);
                ct1.Add(stockcheck.tat);        

                ctest1.contentArray = ct1;
                sumContentAio2.contentList.Add(ctest1);
            }
            allcontentList.Add(sumContentAio2);

            Utils.createMulitSheetsUsingNPOI("RMA_AIO信息" + "-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);
        }
    }

   public class RMAAIO_ReceiveOrderStruct
    {
        //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
        public string vendor;
        public string product;
        public string storehouse;
        public string orderno;
        public string custom_material_no;
        public string mb_brief;
        public string mpn;
        public string dpktype;
        public string receiveNum;
        public string returnNum;
        public string diffNum;

        public string recievedate;
        public string tat;
        public string reason;//超期原因
        public string waitMaterial;//料号
    }

   public class ReturnOrderStructAIO
   {
      // public string Id;
       public string vendor;
       public string product;
       public string return_file_no;
       public string storehouse;
       public string order_make_date;
       public string receive_date;
       public string return_date;
       public string orderno;
       public string custommaterialNo;

       public string mb_brief;

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
   }
    
}
