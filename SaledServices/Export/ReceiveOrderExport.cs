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
    public partial class ReceiveOrderExport : Form
    {
        public ReceiveOrderExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<ReceiveOrderStruct> receiveOrderList = new List<ReceiveOrderStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select vendor,product, source_brief,storehouse,custom_order,order_out_date,order_receive_date,custom_machine_type,mb_brief,"+
                    "custommaterialNo,dpk_status,track_serial_no,custom_serial_no,vendor_serail_no,uuid,mac,mpn,mb_describe,mb_make_date,warranty_period,custom_fault,"+
                    "guarantee,customResponsibility,lenovo_custom_service_no,lenovo_maintenance_no,lenovo_repair_no,whole_machine_no,inputuser from DeliveredTable where order_receive_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReceiveOrderStruct temp = new ReceiveOrderStruct();
                    temp.vendor = querySdr[0].ToString();
                    temp.product = querySdr[1].ToString();
                    temp.source_brief = querySdr[2].ToString();
                    temp.storehouse = querySdr[3].ToString();
                    temp.custom_order = querySdr[4].ToString();
                    temp.order_out_date = querySdr[5].ToString();
                    temp.order_receive_date = querySdr[6].ToString();
                    temp.custom_machine_type = querySdr[7].ToString();
                    temp.mb_brief = querySdr[8].ToString();
                    temp.custommaterialNo = querySdr[9].ToString();
                    temp.dpk_status = querySdr[10].ToString();
                    temp.track_serial_no = querySdr[11].ToString();
                    temp.custom_serial_no = querySdr[12].ToString();
                    temp.vendor_serail_no = querySdr[13].ToString();
                    temp.uuid = querySdr[14].ToString();
                    temp.mac = querySdr[15].ToString();
                    temp.mpn = querySdr[16].ToString();
                    temp.mb_describe = querySdr[17].ToString();

                    temp.mb_make_date = querySdr[18].ToString();
                    temp.warranty_period = querySdr[19].ToString();
                    temp.custom_fault = querySdr[20].ToString();
                    temp.guarantee = querySdr[21].ToString();
                    temp.customResponsibility = querySdr[22].ToString();

                    temp.lenovo_custom_service_no = querySdr[23].ToString();
                    temp.lenovo_maintenance_no = querySdr[24].ToString();
                    temp.lenovo_repair_no = querySdr[25].ToString();
                    temp.whole_machine_no = querySdr[26].ToString();

                    temp.inputuser = querySdr[27].ToString();

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<ReceiveOrderStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("来源");
            titleList.Add("库别");
            titleList.Add("订单编号");
            titleList.Add("客户出库日期");
            titleList.Add("收货日期");
            titleList.Add("客户机型");
            titleList.Add("mb简称");
            titleList.Add("客户料号");
            titleList.Add("DPK状态");
            titleList.Add("跟踪条码");
            titleList.Add("客户序号");
            titleList.Add("厂商序号");
            titleList.Add("UUID");
            titleList.Add("MAC");
            titleList.Add("厂商料号");
            titleList.Add("mb描述");
            titleList.Add("MB生产日期");
            titleList.Add("保修期");
            titleList.Add("客户故障");
            titleList.Add("保内/外");
            titleList.Add("客责描述");
            titleList.Add("联想客服序号");
            titleList.Add("联想维修站编号");
            titleList.Add("联想维修单编号");
            titleList.Add("整机序号");
            titleList.Add("收货人");

            foreach (ReceiveOrderStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.source_brief);
                ct1.Add(stockcheck.storehouse);
                ct1.Add(stockcheck.custom_order);
                ct1.Add(stockcheck.order_out_date);
                ct1.Add(stockcheck.order_receive_date);
                ct1.Add(stockcheck.custom_machine_type);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.dpk_status);
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.vendor_serail_no);
                ct1.Add(stockcheck.uuid);
                ct1.Add(stockcheck.mac);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_describe);

                ct1.Add(stockcheck.mb_make_date);
                ct1.Add(stockcheck.warranty_period);
                ct1.Add(stockcheck.custom_fault);
                ct1.Add(stockcheck.guarantee);
                ct1.Add(stockcheck.customResponsibility);

                ct1.Add(stockcheck.lenovo_custom_service_no);
                ct1.Add(stockcheck.lenovo_maintenance_no);
                ct1.Add(stockcheck.lenovo_repair_no);
                ct1.Add(stockcheck.whole_machine_no);

                ct1.Add(stockcheck.inputuser);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\收货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class ReceiveOrderStruct
    {
        public string vendor;
        public string product;
        public string source_brief;
        public string storehouse;
        public string custom_order;
        public string order_out_date;
        public string order_receive_date;
        public string custom_machine_type;
        public string mb_brief;
        public string custommaterialNo;
        public string dpk_status;
        public string track_serial_no;
        public string custom_serial_no;
        public string vendor_serail_no;
        public string uuid;
        public string mac;
        public string mpn;
        public string mb_describe;
        public string mb_make_date;
        public string warranty_period;
        public string custom_fault;
        public string guarantee;
        public string customResponsibility;
        public string lenovo_custom_service_no;
        public string lenovo_maintenance_no;
        public string lenovo_repair_no;
        public string whole_machine_no;
        public string inputuser;
    }
}
