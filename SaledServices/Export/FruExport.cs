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
    public partial class FruExport : Form
    {
        public FruExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<FruReceiveStruct> frureceiveOrderList = new List<FruReceiveStruct>();
            List<FruReturnStruct> frureturnOrderList = new List<FruReturnStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;
                if (this.receiveradioButton.Checked)
                {
                    cmd.CommandText = "select orderno, vendor, product, customermaterialno, machine_type, name, customermaterialdes , peijian_no, customer_serial_no," +
                    "custom_fault, make_date, gurantee, gurantee_note, vendor_material_no, mpn1, receiver, receive_date  from fruDeliveredTable where receive_date between '" + startTime + "' and '" + endTime + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        FruReceiveStruct temp = new FruReceiveStruct();
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

                        frureceiveOrderList.Add(temp);
                    }
                    querySdr.Close();
                }
                else if (this.returnradioButton.Checked)
                {
                    cmd.CommandText = "select orderno,vendor,product, customermaterialno,machine_type, name, customermaterialdes," +
                        "peijian_no, customer_serial_no,custom_fault, make_date,gurantee,gurantee_note, vendor_material_no, mpn1, receiver, receive_date, tat," +
                        "_status from frureturnStore where receive_date between '" + startTime + "' and '" + endTime + "'";
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
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (this.receiveradioButton.Checked)
            {
                generateExcelToCheck1(frureceiveOrderList, startTime, endTime);
            }
            else if (this.returnradioButton.Checked)
            {
                generateExcelToCheck2(frureturnOrderList, startTime, endTime);
            }
        }

        public void generateExcelToCheck2(List<FruReturnStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("订单编号");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("客户料号");
            titleList.Add("机型");
            titleList.Add("名称");
            titleList.Add("客户物料描述");
            titleList.Add("配件序号");
            titleList.Add("客户序号");
            titleList.Add("客户故障");
            titleList.Add("生产日期");
            titleList.Add("保内/保外");
            titleList.Add("保外备注");
            titleList.Add("厂商料号");
            titleList.Add("MPN1");
            titleList.Add("收件人");
            titleList.Add("收货日期");
            titleList.Add("TAT");
            titleList.Add("状态");

            foreach (FruReturnStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.orderno);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.customermaterialno);
                ct1.Add(stockcheck.machine_type);
                ct1.Add(stockcheck.name);
                ct1.Add(stockcheck.customermaterialdes);

                ct1.Add(stockcheck.peijian_no);
                ct1.Add(stockcheck.customer_serial_no);
                ct1.Add(stockcheck.custom_fault);
                ct1.Add(stockcheck.make_date);
                ct1.Add(stockcheck.gurantee);
                ct1.Add(stockcheck.gurantee_note);
                ct1.Add(stockcheck.vendor_material_no);

                ct1.Add(stockcheck.mpn1);
                ct1.Add(stockcheck.receiver);
                ct1.Add(stockcheck.receive_date);
                ct1.Add(stockcheck.tat);
                ct1.Add(stockcheck._status);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\Fru还货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }

        public void generateExcelToCheck1(List<FruReceiveStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("订单编号");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("客户料号");
            titleList.Add("机型");
            titleList.Add("名称");
            titleList.Add("客户物料描述");
            titleList.Add("配件序号");
            titleList.Add("客户序号");
            titleList.Add("客户故障");
            titleList.Add("生产日期");
            titleList.Add("保内/保外");
            titleList.Add("保外备注");
            titleList.Add("厂商料号");
            titleList.Add("MPN1");
            titleList.Add("收件人");
            titleList.Add("收货日期");

            foreach (FruReceiveStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.orderno);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.customermaterialno);
                ct1.Add(stockcheck.machine_type);
                ct1.Add(stockcheck.name);
                ct1.Add(stockcheck.customermaterialdes);

                ct1.Add(stockcheck.peijian_no);
                ct1.Add(stockcheck.customer_serial_no);
                ct1.Add(stockcheck.custom_fault);
                ct1.Add(stockcheck.make_date);
                ct1.Add(stockcheck.gurantee);
                ct1.Add(stockcheck.gurantee_note);
                ct1.Add(stockcheck.vendor_material_no);

                ct1.Add(stockcheck.mpn1);
                ct1.Add(stockcheck.receiver);
                ct1.Add(stockcheck.receive_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\Fru收货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class FruReceiveStruct
    {
        public string orderno;/*订单编号*/
        public string vendor; /*厂商*/
        public string product; /*客户别*/
        public string customermaterialno;/*客户料号*/
        public string machine_type;/*机型*/
        public string name; /*名称*/
        public string customermaterialdes;/*客户物料描述*/
        public string peijian_no;/*配件序号*/
        public string customer_serial_no;/*客户序号*/
        public string custom_fault;/*客户故障*/
        public string make_date; /*生产日期*/
        public string gurantee;/*保内/保外*/
        public string gurantee_note;/*保外备注*/
        public string vendor_material_no;/*厂商料号*/
        public string mpn1;/*MPN1*/
        public string receiver;/*收件人*/
        public string receive_date; /*收货日期*/
    }

   public class FruReturnStruct
   {
       public string orderno;/*订单编号*/
       public string vendor; /*厂商*/
       public string product; /*客户别*/
       public string customermaterialno;/*客户料号*/
       public string machine_type;/*机型*/
       public string name; /*名称*/
       public string customermaterialdes;/*客户物料描述*/
       public string peijian_no;/*配件序号*/
       public string customer_serial_no;/*客户序号*/
       public string custom_fault;/*客户故障*/
       public string make_date; /*生产日期*/
       public string gurantee;/*保内/保外*/
       public string gurantee_note;/*保外备注*/
       public string vendor_material_no;/*厂商料号*/
       public string mpn1;/*MPN1*/
       public string receiver;/*还货人*/
       public string receive_date;/*还货日期*/
       public string tat;/*时间差*/
       public string _status;/*状态，良品不良品*/
   }
}
