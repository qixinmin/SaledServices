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
    public partial class MBInExport : Form
    {
        public MBInExport()
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

            List<MbInStruct> receiveOrderList = new List<MbInStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id,buy_order_serial_no,vendor,buy_type,product,material_type,mpn,vendormaterialNo,"+
                    "describe,number,input_number,stock_place,pricePer,mb_brief,custom_serial_no,vendor_serial_no,track_serial_no,note," +
                    "inputer,input_date from mb_in_stock where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    MbInStruct temp = new MbInStruct();
                    temp.Id = querySdr[0].ToString();
                    temp.buy_order_serial_no = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.buy_type = querySdr[3].ToString();
                    temp.product = querySdr[4].ToString();
                    temp.material_type = querySdr[5].ToString();
                    temp.mpn = querySdr[6].ToString();
                    temp.vendormaterialNo = querySdr[7].ToString();
                    temp.describe = querySdr[8].ToString();

                    temp.number = querySdr[9].ToString();
                    temp.input_number = querySdr[10].ToString();
                    temp.stock_place = querySdr[11].ToString();
                    temp.pricePer = querySdr[12].ToString();
                    temp.mb_brief = querySdr[13].ToString();
                    temp.custom_serial_no = querySdr[14].ToString();
                    temp.vendor_serial_no = querySdr[15].ToString();
                    temp.track_serial_no = querySdr[16].ToString();
                    temp.note = querySdr[17].ToString();
                    temp.inputer = querySdr[18].ToString();
                    temp.input_date = querySdr[19].ToString();

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

        public void generateExcelToCheck(List<MbInStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
              


            titleList.Add("ID");
            titleList.Add("采购订单编号");
            titleList.Add("厂商");
            titleList.Add("采购类别");
            titleList.Add("客户别");
            titleList.Add("材料大类");
            titleList.Add("MPN");
            titleList.Add("厂商料号");
            titleList.Add("描述");
            titleList.Add("订单数量");
            titleList.Add("入库数量");
            titleList.Add("库位");
            titleList.Add("单价");
            titleList.Add("MB简称");
            titleList.Add("客户序号");
            titleList.Add("厂商序号");
            titleList.Add("跟踪条码");

            titleList.Add("备注");
            titleList.Add("输入人");
            titleList.Add("日期");

            foreach (MbInStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.buy_order_serial_no);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.buy_type);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.material_type);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.vendormaterialNo);
                ct1.Add(stockcheck.describe);

                ct1.Add(stockcheck.number);
                ct1.Add(stockcheck.input_number);
                ct1.Add(stockcheck.stock_place);
                ct1.Add(stockcheck.pricePer);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.custom_serial_no);
                ct1.Add(stockcheck.vendor_serial_no);
                ct1.Add(stockcheck.track_serial_no);

                ct1.Add(stockcheck.note);
                ct1.Add(stockcheck.inputer);
                ct1.Add(stockcheck.input_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\MB收货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

    public class MbInStruct
    {
        public string Id;
        public string buy_order_serial_no;
        public string vendor;
        public string buy_type;
        public string product;
        public string material_type;
        public string mpn;
        public string vendormaterialNo;
        public string describe;
        public string number;
        public string input_number;
        public string stock_place;
        public string pricePer;
        public string mb_brief;
        public string custom_serial_no;
        public string vendor_serial_no;
        public string track_serial_no;
        public string note;
        public string inputer;
        public string input_date;
    }
}
