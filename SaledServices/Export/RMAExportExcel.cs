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

namespace SaledServices
{
    public partial class RMAExportExcel : Form
    {
        public RMAExportExcel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

            List<ReceiveOrderStruct> receiveOrderList = new List<ReceiveOrderStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select receiveOrder.vendor,receiveOrder.product,receiveOrder.storehouse,receiveOrder.orderno,receiveOrder.custom_materialNo,receiveOrder.mb_brief,mpn, dpk_type, receiveOrder.receivedNum,receiveOrder.returnNum,receiveOrder.receivedate from receiveOrder" + 
                    " inner join MBMaterialCompare on custom_materialNo = MBMaterialCompare.custommaterialNo where receiveOrder.receivedate >='" + startTime + "' and receiveOrder.receivedate <='" + endTime + "' ";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    ReceiveOrderStruct temp = new ReceiveOrderStruct();
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

                foreach (ReceiveOrderStruct temp in receiveOrderList)
                {
                    temp.diffNum = (Int16.Parse(temp.receiveNum) - Int16.Parse(temp.returnNum)).ToString();

                    DateTime dt1 = Convert.ToDateTime(temp.recievedate);
                    DateTime dt2 = DateTime.Now;
                 
                    TimeSpan ts = dt2.Subtract(dt1);
                    temp.tat = ts.Days.ToString();
                }

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
            //厂商	客户别	仓库别	订单编号	客户料号	MB简称	MPN	DPK类型	收货数量	还货数量	欠货数量
            titleList.Add("厂商");
            titleList.Add("客户别");
            titleList.Add("仓库别");
            titleList.Add("订单编号");
            titleList.Add("客户料号");
            titleList.Add("MB简称");
            titleList.Add("MPN");
            titleList.Add("DPK类型");

            titleList.Add("收货数量");
            titleList.Add("还货数量");
            titleList.Add("欠货数量");
            titleList.Add("在途天数");

            foreach (ReceiveOrderStruct stockcheck in StockCheckList)
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

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("RMA信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
        }
    }

   public class ReceiveOrderStruct
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
    }
    
}
