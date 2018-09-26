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
    public partial class BgaInExport : Form
    {
        public BgaInExport()
        {
            InitializeComponent();
        }

        private void exportxmlbutton_Click(object sender, EventArgs e)
        {
            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy/MM/dd"));
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy/MM/dd"));

            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy/MM/dd");
            string endTime = this.dateTimePickerend.Value.ToString("yyyy/MM/dd");

            List<BgaInStruct> receiveOrderList = new List<BgaInStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id,buy_order_serial_no,vendor,buy_type,product,material_type,mpn,vendormaterialNo,"+
                    "describe,pricePer,bga_describe,order_number,input_number,bgasn,stock_place,note,inputer,input_date from bga_in_stock where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    BgaInStruct temp = new BgaInStruct();
                    temp.Id = querySdr[0].ToString();
                    temp.buy_order_serial_no = querySdr[1].ToString();
                    temp.vendor = querySdr[2].ToString();
                    temp.buy_type = querySdr[3].ToString();
                    temp.product = querySdr[4].ToString();
                    temp.material_type = querySdr[5].ToString();
                    temp.mpn = querySdr[6].ToString();
                    temp.vendormaterialNo = querySdr[7].ToString();
                    temp.describe = querySdr[8].ToString();
                    temp.pricePer = querySdr[9].ToString();
                    temp.bga_describe = querySdr[10].ToString();
                    temp.order_number = querySdr[11].ToString();
                    temp.input_number = querySdr[12].ToString();
                    temp.bgasn = querySdr[13].ToString();
                    temp.stock_place = querySdr[14].ToString();
                    temp.note = querySdr[15].ToString();
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

        public void generateExcelToCheck(List<BgaInStruct> StockCheckList, string startTime, string endTime)
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
            titleList.Add("单价");
            titleList.Add("BGA简述");
            titleList.Add("订单数量");
            titleList.Add("入库数量");
            titleList.Add("BGASN");
            titleList.Add("库位");
            titleList.Add("备注");
            titleList.Add("输入人");
            titleList.Add("日期");

            foreach (BgaInStruct stockcheck in StockCheckList)
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
                ct1.Add(stockcheck.pricePer);
                ct1.Add(stockcheck.bga_describe);
                ct1.Add(stockcheck.order_number);
                ct1.Add(stockcheck.input_number);
                ct1.Add(stockcheck.bgasn);
                ct1.Add(stockcheck.stock_place);
                ct1.Add(stockcheck.note);
                ct1.Add(stockcheck.inputer);
                ct1.Add(stockcheck.input_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\BGA收货信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class BgaInStruct
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
        public string pricePer;
        public string bga_describe;
        public string order_number;
        public string input_number;
        public string bgasn;
        public string stock_place;
        public string note;
        public string inputer;
        public string input_date;
    }
}
