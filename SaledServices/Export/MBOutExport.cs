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
    public partial class MBOutExport : Form
    {
        public MBOutExport()
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

            List<MBOutStruct> receiveOrderList = new List<MBOutStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id,track_serial_no,vendor,product,mpn,mb_brief,describe,custom_serial_no,"+
                    "vendor_serial_no,vendormaterialNo,custommaterialNo,dpk_type,dpkpn,stock_place,"+
                    "note,taker,inputer,input_date from mb_out_stock where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MBOutStruct temp = new MBOutStruct();
                    temp.Id = querySdr[0].ToString();
                    temp.track_serial_no = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.product = querySdr[3].ToString();
                    temp.mpn = querySdr[4].ToString();
                    temp.mb_brief = querySdr[5].ToString();
                    temp.describe = querySdr[6].ToString();
                    temp.custom_serial_no = querySdr[7].ToString();
                    temp.vendor_serial_no = querySdr[8].ToString();
                    temp.vendormaterialNo = querySdr[9].ToString();
                    temp.custommaterialNo = querySdr[10].ToString();
                    temp.dpk_type = querySdr[11].ToString();
                    temp.dpkpn = querySdr[12].ToString();
                    temp.stock_place = querySdr[13].ToString();
                    temp.note = querySdr[14].ToString();
                    temp.taker = querySdr[15].ToString();
                    temp.inputer = querySdr[16].ToString();
                    temp.input_date = querySdr[17].ToString();

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

        public void generateExcelToCheck(List<MBOutStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();

            titleList.Add("ID");
            titleList.Add("跟踪条码");
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("MPN");
            titleList.Add("MB简称");
            titleList.Add("描述");
            titleList.Add("客户序号");
            titleList.Add("厂商序号");
            titleList.Add("厂商料号");
            titleList.Add("客户料号");
            titleList.Add("DPK类型");
            titleList.Add("DPKPN");
           
            titleList.Add("库位");
            titleList.Add("备注");
            titleList.Add("领用人");
            titleList.Add("输入人");
            titleList.Add("日期");

            foreach (MBOutStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.describe);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.vendor_serial_no);
                ct1.Add(stockcheck.vendormaterialNo);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.dpk_type);
                ct1.Add(stockcheck.dpkpn);
                ct1.Add(stockcheck.stock_place);
                ct1.Add(stockcheck.note);
                ct1.Add(stockcheck.taker);
                ct1.Add(stockcheck.inputer);
                ct1.Add(stockcheck.input_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\MB出货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class MBOutStruct
    {
     
        public string Id;
        public string track_serial_no;
        public string vendor;
        public string product;
        public string mpn;
        public string mb_brief;
        public string describe;
        public string custom_serial_no;
        public string vendor_serial_no;
        public string vendormaterialNo;
        public string custommaterialNo;

        public string dpk_type;
        public string dpkpn;
        public string stock_place;
      
        public string note;
         public string taker;
        public string inputer;
        public string input_date;
    }
}
