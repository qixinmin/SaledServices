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
    public partial class TestInfoExport : Form
    {
        public TestInfoExport()
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

            string startTime = this.dateTimePickerstart.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = this.dateTimePickerend.Value.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<TestInfoStruct> receiveOrderList = new List<TestInfoStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                //需要三张表循环导出testalltable test1table test2table
                cmd.CommandText = "select track_serial_no,tester,test_date  from testalltable where test_date between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    TestInfoStruct temp = new TestInfoStruct();
                    temp.track_serial_no =  querySdr[0].ToString();
                    temp.tester =  querySdr[1].ToString();
                    temp.test_date = querySdr[2].ToString();
                    temp.station = "Test1&2";
                    receiveOrderList.Add(temp);                  
                }
                querySdr.Close();

                cmd.CommandText = "select track_serial_no,tester,test_date  from test1table where test_date between '" + startTime + "' and '" + endTime + "'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    TestInfoStruct temp = new TestInfoStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.tester = querySdr[1].ToString();
                    temp.test_date = querySdr[2].ToString();
                    temp.station = "Test1";
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                cmd.CommandText = "select track_serial_no,tester,test_date  from test2table where test_date between '" + startTime + "' and '" + endTime + "'";
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    TestInfoStruct temp = new TestInfoStruct();
                    temp.track_serial_no = querySdr[0].ToString();
                    temp.tester = querySdr[1].ToString();
                    temp.test_date = querySdr[2].ToString();
                    temp.station = "Test2";
                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (TestInfoStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select mb_brief from DeliveredTable where track_serial_no='" + temp.track_serial_no + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {                        
                        temp.mb_brief = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    if (temp.mb_brief == "") {
                        cmd.CommandText = "select mb_brief from DeliveredTableTransfer where track_serial_no='" + temp.track_serial_no + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.mb_brief = querySdr[0].ToString();
                        }
                        querySdr.Close();
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generateExcelToCheck(receiveOrderList, startTime, endTime);
        }

        public void generateExcelToCheck(List<TestInfoStruct> StockCheckList, string startTime, string endTime)
        {
            List<string> titleList = new List<string>();
            List<Object> contentList = new List<object>();
          
           
            titleList.Add("跟踪条码");
            titleList.Add("测试人");
            titleList.Add("测试时间");
            titleList.Add("MB检测");
            titleList.Add("站别");

            foreach (TestInfoStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.track_serial_no);
                ct1.Add(stockcheck.tester);
                ct1.Add(Utils.modifyDataFormat(stockcheck.test_date));
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.station);

                ctest1.contentArray = ct1;
                contentList.Add(ctest1);
            }

            Utils.createExcel("测试查询信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xlsx", titleList, contentList);
            //Utils.createExcelList(titleList, contentList);
        }
    }

   public class TestInfoStruct
    {
        public string track_serial_no;
        public string tester;
        public string test_date;

        public string station;
        public string mb_brief;
    }
}
