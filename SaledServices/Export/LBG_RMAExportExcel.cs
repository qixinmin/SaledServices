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
    public partial class LBG_RMAExportExcel : Form
    {
        public LBG_RMAExportExcel()
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

            List<FruQianHuoStruct> fruQianhuoList = new List<FruQianHuoStruct>();
            List<FruReturnStruct> frureturnOrderList = new List<FruReturnStruct>();

            List<RMAAIO_ReceiveOrderStruct> receiveOrderList = new List<RMAAIO_ReceiveOrderStruct>();
            List<RMAAIO_ReceiveOrderStruct> receiveOrderListTarget = new List<RMAAIO_ReceiveOrderStruct>();

            List<ReturnOrderStructLBG> returnOrderList = new List<ReturnOrderStructLBG>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select orderno,vendor,product, customermaterialno,machine_type, name, customermaterialdes," +
                       "peijian_no, customer_serial_no,custom_fault, make_date,gurantee,gurantee_note, vendor_material_no, mpn1, receiver, receive_date, tat," +
                       "_status from frureturnStore where receive_date between '" + startTime + "' and '" + endTime + "' and  frureturnStore.vendor='" + this.vendorComboBox.Text + "' and frureturnStore.product='" + this.productComboBox.Text + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    FruReturnStruct temp = new FruReturnStruct();
                    temp.orderno = querySdr[0].ToString();
                    temp.vendor = querySdr[1].ToString();
                    temp.product = querySdr[2].ToString();
                    temp.customermaterialno = querySdr[3].ToString();
                    temp.machine_type = querySdr[4].ToString();
                    temp.name = querySdr[5].ToString();
                    temp.customermaterialdes = querySdr[6].ToString();

                    temp.peijian_no = querySdr[7].ToString();
                    temp.customer_serial_no = querySdr[8].ToString();
                    temp.custom_fault = querySdr[9].ToString();
                    temp.make_date = querySdr[10].ToString();
                    temp.gurantee = querySdr[11].ToString();
                    temp.gurantee_note = querySdr[12].ToString();
                    temp.vendor_material_no = querySdr[13].ToString();

                    temp.mpn1 = querySdr[14].ToString();
                    temp.receiver = querySdr[15].ToString();
                    temp.receive_date = querySdr[16].ToString();

                    temp.tat = querySdr[17].ToString();
                    temp._status = querySdr[18].ToString();

                    frureturnOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (FruReturnStruct temp in frureturnOrderList)
                {
                    cmd.CommandText = "select receive_date  from fruDeliveredTable where peijian_no='"+temp.peijian_no+"'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.make_date = querySdr[0].ToString();
                    }
                    querySdr.Close();
                }

                cmd.CommandText = "select receiveOrder.vendor,receiveOrder.product,receiveOrder.storehouse,receiveOrder.orderno,receiveOrder.custom_materialNo,receiveOrder.mb_brief,custom_material_describe, receiveOrder.receivedNum,receiveOrder.returnNum,receiveOrder.receivedate from frureceiveOrder as receiveOrder" +
                " inner join frubomtable on custom_materialNo = frubomtable.custom_material_no where receiveOrder.receivedate >='" + startTime + "' and receiveOrder.receivedate <='" + endTime + "' and  receiveOrder.vendor='" + this.vendorComboBox.Text + "' and receiveOrder.product='" + this.productComboBox.Text + "'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    FruQianHuoStruct temp = new FruQianHuoStruct();
                    temp.vendor = querySdr[0].ToString();
                    temp.product = querySdr[1].ToString();
                    temp.storehouse = querySdr[2].ToString();
                    temp.orderno = querySdr[3].ToString();
                    temp.custom_material_no = querySdr[4].ToString();
                    temp.mb_brief = querySdr[5].ToString();
                    temp.mpn = querySdr[6].ToString();

                    temp.receiveNum = querySdr[7].ToString();
                    temp.returnNum = querySdr[8].ToString();
                    temp.recievedate = querySdr[9].ToString();

                    fruQianhuoList.Add(temp);
                }
                querySdr.Close();
                
                foreach (FruQianHuoStruct temp in fruQianhuoList)
                {
                    temp.diffNum = (Int16.Parse(temp.receiveNum) - Int16.Parse(temp.returnNum)).ToString();

                    DateTime dt1 = Convert.ToDateTime(temp.recievedate);
                    DateTime dt2 = DateTime.Now;

                    TimeSpan ts = dt2.Subtract(dt1);
                    temp.tat = ts.Days.ToString();
                }


                cmd.CommandText = "select receiveOrder.vendor,receiveOrder.product,receiveOrder.storehouse,receiveOrder.orderno,receiveOrder.custom_materialNo,receiveOrder.mb_brief,mpn, dpk_type, receiveOrder.receivedNum,receiveOrder.returnNum,receiveOrder.receivedate from receiveOrder" +
                  " inner join MBMaterialCompare on custom_materialNo = MBMaterialCompare.custommaterialNo where receiveOrder.receivedate >='" + startTime + "' and receiveOrder.receivedate <='" + endTime + "' and  receiveOrder.vendor='" + this.vendorComboBox.Text + "' and receiveOrder.product='" + this.productComboBox.Text + "'";
                querySdr = cmd.ExecuteReader();
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
                    int diff = Int16.Parse(temp.receiveNum) - Int16.Parse(temp.returnNum);
                    temp.diffNum = diff + "";

                    DateTime dt1 = Convert.ToDateTime(temp.recievedate);
                    DateTime dt2 = DateTime.Now;

                    TimeSpan ts = dt2.Subtract(dt1);
                    temp.tat = ts.Days.ToString();

                    cmd.CommandText = "select BGAPN from bga_wait_material_record_table where orderno ='" + temp.orderno + "' and customMaterialNo='" + temp.custom_material_no + "'";
                    querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
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
                   "response_describe,tat,inputuser,lenovo_maintenance_no,lenovo_repair_no from returnStore where return_date between '" + startTime + "' and '" + endTime + "'  and  returnStore.vendor='" + this.vendorComboBox.Text + "' and returnStore.product='" + this.productComboBox.Text + "'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReturnOrderStructLBG temp = new ReturnOrderStructLBG();
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

                foreach (ReturnOrderStructLBG temp in returnOrderList)
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

                    cmd.CommandText = "select BGAPN from bga_wait_material_record_table where track_serial_no ='" + temp.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();

                    while (querySdr.Read())
                    {
                        temp.reason = "待料";
                        temp.waitMaterial = querySdr[0].ToString();
                        break;
                    }
                    querySdr.Close();
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(fruQianhuoList, frureturnOrderList,receiveOrderListTarget,returnOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<FruQianHuoStruct> StockCheckList, List<FruReturnStruct> returnOrderListfru,  
            List<RMAAIO_ReceiveOrderStruct> receiveOrderListTarget,
            List<ReturnOrderStructLBG> returnOrderList,
            string startTime, string endTime)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent sumContentAio = new allContent();
            sumContentAio.sheetName = "FRU未还在途";
            sumContentAio.titleList = new List<string>();
            sumContentAio.contentList = new List<object>();
          
            //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
            //sumContentAio.titleList.Add("厂商");
            //sumContentAio.titleList.Add("客户别");
            //sumContentAio.titleList.Add("仓库别");
            sumContentAio.titleList.Add("订单编号");
            sumContentAio.titleList.Add("客户料号");
            sumContentAio.titleList.Add("描述");
            sumContentAio.titleList.Add("MB简称");

            sumContentAio.titleList.Add("收货数量");
            sumContentAio.titleList.Add("还货数量");
            sumContentAio.titleList.Add("欠货数量");
            sumContentAio.titleList.Add("在途天数");

            foreach (FruQianHuoStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                //ct1.Add(stockcheck.vendor);
                //ct1.Add(stockcheck.product);
                //ct1.Add(stockcheck.storehouse);
                ct1.Add(stockcheck.orderno);
                ct1.Add(stockcheck.custom_material_no);
                ct1.Add(stockcheck.mpn);//此处mpn用做描述字段存储
                ct1.Add(stockcheck.mb_brief);

                ct1.Add(stockcheck.receiveNum);
                ct1.Add(stockcheck.returnNum);
                ct1.Add(stockcheck.diffNum);
                ct1.Add(stockcheck.tat);

                ctest1.contentArray = ct1;
                sumContentAio.contentList.Add(ctest1);
            }
            allcontentList.Add(sumContentAio);


            allContent sumContentAio2 = new allContent();
            sumContentAio2.sheetName = "FRU TAT";
            sumContentAio2.titleList = new List<string>();
            sumContentAio2.contentList = new List<object>();

            sumContentAio2.titleList.Add("订单编号");
            //sumContentAio2.titleList.Add("厂商");
            //sumContentAio2.titleList.Add("客户别");
            sumContentAio2.titleList.Add("客户料号");
            sumContentAio2.titleList.Add("机型");
            sumContentAio2.titleList.Add("名称");
            //sumContentAio2.titleList.Add("客户物料描述");
            sumContentAio2.titleList.Add("配件序号");
            //sumContentAio2.titleList.Add("客户序号");
            //sumContentAio2.titleList.Add("客户故障");
            //sumContentAio2.titleList.Add("生产日期");
            //sumContentAio2.titleList.Add("保内/保外");
            //sumContentAio2.titleList.Add("保外备注");
            //sumContentAio2.titleList.Add("厂商料号");
            //sumContentAio2.titleList.Add("MPN1");
            //sumContentAio2.titleList.Add("还货人");
            sumContentAio2.titleList.Add("收货日期");
            sumContentAio2.titleList.Add("还货日期");
            sumContentAio2.titleList.Add("TAT");
           // sumContentAio2.titleList.Add("状态");

            foreach (FruReturnStruct stockcheck in returnOrderListfru)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add(stockcheck.orderno);
                //ct1.Add(stockcheck.vendor);
                //ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.customermaterialno);
                ct1.Add(stockcheck.machine_type);
                ct1.Add(stockcheck.name);
                //ct1.Add(stockcheck.customermaterialdes);

                ct1.Add(stockcheck.peijian_no);
                //ct1.Add(stockcheck.customer_serial_no);
                //ct1.Add(stockcheck.custom_fault);
                //ct1.Add(stockcheck.make_date);
                //ct1.Add(stockcheck.gurantee);
                //ct1.Add(stockcheck.gurantee_note);
                //ct1.Add(stockcheck.vendor_material_no);

                //ct1.Add(stockcheck.mpn1);
                //ct1.Add(stockcheck.receiver);
                ct1.Add(Utils.modifyDataFormat(stockcheck.make_date));//用来装载收货日期
                ct1.Add(Utils.modifyDataFormat(stockcheck.receive_date));
                ct1.Add(stockcheck.tat);
                //ct1.Add(stockcheck._status);

                ctest1.contentArray = ct1;
                sumContentAio2.contentList.Add(ctest1);
            }
            allcontentList.Add(sumContentAio2);

            allContent sumContentAio3 = new allContent();
            sumContentAio3.sheetName = "MB 未还在途";
            sumContentAio3.titleList = new List<string>();
            sumContentAio3.contentList = new List<object>();

            //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
            sumContentAio3.titleList.Add("厂商");
            sumContentAio3.titleList.Add("客户别");
            sumContentAio3.titleList.Add("仓库别");
            sumContentAio3.titleList.Add("订单编号");
            sumContentAio3.titleList.Add("客户料号");
            sumContentAio3.titleList.Add("MB简称");
            sumContentAio3.titleList.Add("MPN");
            sumContentAio3.titleList.Add("DPK类型");

            sumContentAio3.titleList.Add("收货数量");
            sumContentAio3.titleList.Add("还货数量");
            sumContentAio3.titleList.Add("欠货数量");
            sumContentAio3.titleList.Add("在途天数");
            sumContentAio3.titleList.Add("超期原因");
            sumContentAio3.titleList.Add("料号");

            foreach (RMAAIO_ReceiveOrderStruct stockcheck in receiveOrderListTarget)
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
                sumContentAio3.contentList.Add(ctest1);
            }
            allcontentList.Add(sumContentAio3);

            allContent sumContentAio4 = new allContent();
            sumContentAio4.sheetName = "MB TAT";
            sumContentAio4.titleList = new List<string>();
            sumContentAio4.contentList = new List<object>();

            sumContentAio4.titleList.Add("厂商");
            sumContentAio4.titleList.Add("客户别");
            sumContentAio4.titleList.Add("还货文件编号");
            sumContentAio4.titleList.Add("客户库别");
            sumContentAio4.titleList.Add("做单时间");
            sumContentAio4.titleList.Add("收货时间");
            sumContentAio4.titleList.Add("还货时间");
            sumContentAio4.titleList.Add("订单编号");
            sumContentAio4.titleList.Add("客户料号");
            sumContentAio4.titleList.Add("MB简称");
            sumContentAio4.titleList.Add("DPK状态");
            sumContentAio4.titleList.Add("跟踪条码");
            sumContentAio4.titleList.Add("客户序号");
            sumContentAio4.titleList.Add("厂商序号");
            sumContentAio4.titleList.Add("厂商料号");

            sumContentAio4.titleList.Add("TAT");
            sumContentAio4.titleList.Add("超期原因");
            sumContentAio4.titleList.Add("料号");

            foreach (ReturnOrderStructLBG stockcheck in returnOrderList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();

                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.return_file_no);
                ct1.Add(stockcheck.storehouse);
                ct1.Add(stockcheck.order_make_date);
                ct1.Add(Utils.modifyDataFormat(stockcheck.receive_date));
                ct1.Add(Utils.modifyDataFormat(stockcheck.return_date));
                ct1.Add(stockcheck.orderno);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.dpkpn);
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.vendor_serail_no);
                ct1.Add(stockcheck.vendormaterialNo);
                ct1.Add(stockcheck.tat);
                ct1.Add(stockcheck.reason);
                ct1.Add(stockcheck.waitMaterial);

                ctest1.contentArray = ct1;
                sumContentAio4.contentList.Add(ctest1);
            }
            allcontentList.Add(sumContentAio4);

            Utils.createMulitSheetsUsingNPOI("RMA_LBG信息" + "-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);
        }
    }

    public class ReturnOrderStructLBG
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

        public string reason;//超期原因
        public string waitMaterial;//料号
    }
    
}
