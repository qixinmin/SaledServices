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
    public partial class FaultMBRecordExport : Form
    {
        public FaultMBRecordExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<FaultMbStruct> receiveOrderList = new List<FaultMbStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select track_serial_no,vendor_sn,vendor,product,mb_brief,mpn,mb_describe,_status,pch_brief,vga_brief,cpu_brief,"+
                             "repairer,repair_date  from fault_mb_enter_record_table where repair_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    FaultMbStruct temp = new FaultMbStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.vendor_sn = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.product = querySdr[3].ToString();
                    temp.mb_brief = querySdr[4].ToString();
                    temp.mpn = querySdr[5].ToString();
                    temp.mb_describe = querySdr[6].ToString();
                    temp._status = querySdr[7].ToString();
                    temp.pch_brief = querySdr[8].ToString();

                    temp.vga_brief = querySdr[9].ToString();
                    temp.cpu_brief = querySdr[10].ToString();
                    temp.repairer = querySdr[11].ToString();
                    temp.repair_date = querySdr[12].ToString();

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

        public void generateExcelToCheck(List<FaultMbStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("跟踪条码");
            titleList.Add("厂商sn");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("MB简称");
            titleList.Add("MPN");
            titleList.Add("MB描述");

            titleList.Add("状态");
            titleList.Add("PCH描述");
            titleList.Add("VGA描述");
            titleList.Add("CPU描述");

            titleList.Add("入库人");
            titleList.Add("入库日期");
            titleList.Add("类型");

            foreach (FaultMbStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.vendor_sn);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_describe);
                ct1.Add(stockcheck._status);
                ct1.Add(stockcheck.pch_brief);

                ct1.Add(stockcheck.vga_brief);
                ct1.Add(stockcheck.cpu_brief);
                ct1.Add(stockcheck.repairer);
                ct1.Add(stockcheck.repair_date);
                ct1.Add("报废");

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\MB报废信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class FaultMbStruct
    {
        public string track_serial_no;
        public string vendor_sn;
        public string vendor;
        public string product;
        public string mb_brief;
        public string mpn;
        public string mb_describe;

        public string _status;
        public string pch_brief;
        public string vga_brief;
        public string cpu_brief;

        public string repairer;
        public string repair_date;
    }
}
