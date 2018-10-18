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
    public partial class BgaOutExport : Form
    {
        public BgaOutExport()
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

            List<BgaOutStruct> receiveOrderList = new List<BgaOutStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select Id,vendor,product,mpn,bga_brief,bga_describe,stock_place,out_number,note,taker,"+
                    "inputer,input_date from bga_out_stock where input_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    BgaOutStruct temp = new BgaOutStruct();
                    temp.Id =  querySdr[0].ToString();
                    temp.vendor = querySdr[1].ToString();
                    temp.product = querySdr[2].ToString();
                    temp.mpn = querySdr[3].ToString();
                    temp.bga_brief = querySdr[4].ToString();
                    temp.bga_describe = querySdr[5].ToString();
                    temp.stock_place = querySdr[6].ToString();
                    temp.out_number = querySdr[7].ToString();
                    temp.note = querySdr[8].ToString();
                    temp.taker = querySdr[9].ToString();
                    temp.inputer = querySdr[10].ToString();
                    temp.input_date = querySdr[11].ToString();

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

        public void generateExcelToCheck(List<BgaOutStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
                  
            titleList.Add("ID");
            titleList.Add("厂商");            
            titleList.Add("客户别");
            titleList.Add("MPN");
            titleList.Add("BGA简称");
            titleList.Add("BGA描述");            
            titleList.Add("库位");
            titleList.Add("出库数量");
            titleList.Add("备注");            
            titleList.Add("领用人");
            titleList.Add("输入人");
            titleList.Add("日期");

            foreach (BgaOutStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
               
                ct1.Add(stockcheck.Id);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.mpn);
                ct1.Add(stockcheck.bga_brief);
                ct1.Add(stockcheck.bga_describe);
                ct1.Add(stockcheck.stock_place);
                ct1.Add(stockcheck.out_number);
                ct1.Add(stockcheck.note);
                ct1.Add(stockcheck.taker);
                ct1.Add(stockcheck.inputer);
                ct1.Add(stockcheck.input_date);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("D:\\Bga出库信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class BgaOutStruct
    {
        public string Id;
       public string vendor;
       public string product;
       public string mpn;
       public string bga_brief;
       public string bga_describe;
       public string stock_place;
       public string out_number;
       public string note;
       public string taker;
       public string inputer;
       public string input_date;
    }
}
