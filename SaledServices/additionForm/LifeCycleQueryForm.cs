using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.Export;

namespace SaledServices
{
    public partial class LifeCycleQueryForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "stationInfoRecord";

        public LifeCycleQueryForm()
        {
            InitializeComponent();
        }

    
        class lifecycleinfo
        {
            public string trackNo { get; set; }
            public string station { get; set; }
            public string result { get; set; }

            public string failDescribe { get; set; }
            public string inputer { get; set; }
            public string inputdate { get; set; }
        }
     
        private void query_Click(object sender, EventArgs e)
        {
            queryContent(this.tracknoTextBox.Text.Trim(), false);
        }


        private void queryContent(string query, bool is8s)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                mConn.Open();
                SqlDataReader querySdr = null;
                string trackno = "";
                if (is8s)
                {
                    cmd.CommandText = "select track_serial_no from DeliveredTable where custom_serial_no='" + query + "' order by Id desc";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        trackno = querySdr[0].ToString();
                    }
                    querySdr.Close();
                }
                else
                {
                    trackno = query;
                }

                List<lifecycleinfo> list = new List<lifecycleinfo>();

                cmd.CommandText = "select * from stationInfoRecord where  trackno='" + trackno + "' order by Id desc";
                cmd.CommandType = CommandType.Text;
                querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    lifecycleinfo useclass = new lifecycleinfo();
                    useclass.trackNo = querySdr[1].ToString();
                    useclass.station = querySdr[2].ToString();
                    useclass.inputer = querySdr[3].ToString();
                    useclass.inputdate = querySdr[4].ToString();

                    list.Add(useclass);
                }
                querySdr.Close();

                foreach (lifecycleinfo info in list)
                {
                    //根据之前的时间查一个时间段
                    //  select  dateadd(ss,60, '2020-06-20 11:21:11.923')
    //select  dateadd(ss,-60, '2020-06-20 11:21:11.923')
                    string sTime="", eTime="";
                    cmd.CommandText = " select  dateadd(ss,-60, '"+info.inputdate+"')";
                    cmd.CommandType = CommandType.Text;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        sTime = querySdr[0].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = " select  dateadd(ss,60, '" + info.inputdate + "')";
                    cmd.CommandType = CommandType.Text;
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        eTime = querySdr[0].ToString();
                    }
                    querySdr.Close();


                    cmd.CommandText = "select top 1 result,failDescribe from test_all_result_record where  trackno='" + trackno + "' and station='" + info.station + "' and inputdate between '" + sTime + "' and '" + eTime + "' order by Id desc";
                    cmd.CommandType = CommandType.Text;
                    querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows)
                    {
                        while (querySdr.Read())
                        {
                            info.result = querySdr[0].ToString();
                            info.failDescribe = querySdr[1].ToString();
                        }
                    }
                    else
                    {
                        info.result = "PASS";
                    }
                    querySdr.Close();
                }

                querySdr.Close();

                dataGridView1.DataSource = list;

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "跟踪条码", "站别","结果","失败原因", "输入人", "输入时间" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }

            MessageBox.Show("查询完成！");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }                    
        }

        private void tracknoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query_Click(null, null);
            }
        }

        private void _8sCodetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                query8s_Click(null, null);
            }

        }

        private void query8s_Click(object sender, EventArgs e)
        {
            queryContent(this._8sCodetextBox.Text.Trim(), true);
        }

        private void LifeCycleQueryForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
            GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
            SetValue(tableLayoutPanel1, true, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime time1 = Convert.ToDateTime(this.dateTimePickerstart.Value.Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            //AddDays 1 是为了加上一天，查询当然的数据，因为天数里面有时分秒 
            DateTime time2 = Convert.ToDateTime(this.dateTimePickerend.Value.AddDays(1).Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));

            DateTime endTimeforName = Convert.ToDateTime(this.dateTimePickerend.Value.Date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));


            if (DateTime.Compare(time1, time2) > 0) //判断日期大小
            {
                MessageBox.Show("开始日期大于结束");
                return;
            }

            string startTime = time1.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string endTime = time2.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            string endTimeforNameStr = endTimeforName.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            List<lifeCycleStruct> receiveOrderList = new List<lifeCycleStruct>();
            List<lifeCycleSumStruct> lifeSumlist = new List<lifeCycleSumStruct>();

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select trackno,station,inputer,inputdate from stationInfoRecord where inputdate between '" + startTime + "' and '" + endTime + "'";
                SqlDataReader querySdr = cmd.ExecuteReader();
                while (querySdr.Read())
                {
                    lifeCycleStruct temp = new lifeCycleStruct();
                    temp.trackno = querySdr[0].ToString();
                    temp.station = querySdr[1].ToString();
                    temp.tester = querySdr[2].ToString();
                    temp.inputdate = querySdr[3].ToString();

                    receiveOrderList.Add(temp);
                }
                querySdr.Close();

                foreach (lifeCycleStruct temp in receiveOrderList)
                {
                    cmd.CommandText = "select product,vendor, custommaterialNo,mb_brief, mpn from DeliveredTable where track_serial_no='" + temp.trackno + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.product = querySdr[0].ToString();
                        temp.vendor = querySdr[1].ToString();
                        temp.custommaterialNo = querySdr[2].ToString();
                        temp.mb_brief = querySdr[3].ToString();
                        temp.mpn = querySdr[4].ToString();
                    }
                    querySdr.Close();

                    //查询测试结果
                    if (temp.station == "Test1" || temp.station == "测试1_2" || temp.station == "Test2")
                    {
                        string querystation = temp.station;
                        if (temp.station == "测试1_2")
                        {
                            querystation = "Test1_2";
                        }
                        cmd.CommandText = "select failDescribe from test_all_result_record where trackno='" + temp.trackno + "' and station='" + querystation + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.result = querySdr[0].ToString();
                            if (temp.result.Trim() == "")
                            {
                                temp.result = "PASS";
                            }
                        }
                        querySdr.Close();
                    }

                    //外观测试结果
                    if (temp.station == "外观")
                    {
                        cmd.CommandText = "select result from outlookcheck where track_serial_no='" + temp.trackno + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.result = querySdr[0].ToString();
                            if (temp.result.Trim() == "")
                            {
                                temp.result = "PASS";
                            }
                        }
                        querySdr.Close();
                    }

                    //OBE测试结果
                    if (temp.station == "Obe")
                    {
                        cmd.CommandText = "select failreason from ObeStationtable where track_serial_no='" + temp.trackno + "'";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.result = querySdr[0].ToString();
                            if (temp.result.Trim() == "")
                            {
                                temp.result = "PASS";
                            }
                        }
                        querySdr.Close();
                    }

                    //收货测试结果
                    if (temp.station == "收货")
                    {
                        cmd.CommandText = "select top 1 custom_fault from DeliveredTable where track_serial_no='" + temp.trackno + "' order by Id desc";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.result = querySdr[0].ToString();
                           
                        }
                        querySdr.Close();
                    }

                    //维修测试结果
                    if (temp.station == "维修")
                    {
                        cmd.CommandText = "select top 1 fault_type, _action, repair_result  from repair_record_table where track_serial_no='" + temp.trackno + "' order by Id desc";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.result = querySdr[0].ToString()+","+querySdr[1].ToString()+","+querySdr[2].ToString();

                        }
                        querySdr.Close();
                    }

                    //还货测试结果
                    if (temp.station == "还货")
                    {
                        cmd.CommandText = "select top 1 _status  from returnStore where track_serial_no='" + temp.trackno + "' order by Id desc";
                        querySdr = cmd.ExecuteReader();
                        while (querySdr.Read())
                        {
                            temp.result = querySdr[0].ToString();

                        }
                        querySdr.Close();
                    }

                    if (temp.station.StartsWith("BGA"))
                    {
                        temp.result = "PASS";
                    }


                }
                mConn.Close();

                //下面是统计信息
                foreach (lifeCycleStruct temp in receiveOrderList)
                {
                    if (lifeSumlist.Count == 0)
                    {
                        lifeCycleSumStruct newTemp = new lifeCycleSumStruct();
                        newTemp.station = temp.station;
                        lifeSumlist.Add(newTemp);                        
                    }
                    else
                    {
                        bool exist = false;
                        foreach (lifeCycleSumStruct oldrecord in lifeSumlist)
                        {
                            if (oldrecord.equals(temp.station))
                            {
                                oldrecord.incrementNum();
                                exist = true;
                                break;
                            }
                        }

                        if (exist == false)
                        {
                            lifeCycleSumStruct newTemp = new lifeCycleSumStruct();
                            newTemp.station = temp.station;
                            lifeSumlist.Add(newTemp);                            
                        }
                    }
                }                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                mConn.Close();
            }

            generateExcelToCheck(receiveOrderList, startTime, endTimeforNameStr, lifeSumlist);
        }

        public void generateExcelToCheck(List<lifeCycleStruct> StockCheckList, string startTime, string endTime, List<lifeCycleSumStruct> bagWaitSumList)
        {
            List<allContent> allcontentList = new List<allContent>();

            allContent firstsheet = new allContent();
            firstsheet.sheetName = "主板生命周期" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-');
            firstsheet.titleList = new List<string>();
            firstsheet.contentList = new List<object>();

            firstsheet.titleList.Add("跟踪条码");
            firstsheet.titleList.Add("站别");
            firstsheet.titleList.Add("输入人");
            firstsheet.titleList.Add("时间");

            firstsheet.titleList.Add("结果");

            firstsheet.titleList.Add("厂商");
            firstsheet.titleList.Add("客户别");
            firstsheet.titleList.Add("客户料号");
            firstsheet.titleList.Add("MB简称");
            firstsheet.titleList.Add("MPN");

            foreach (lifeCycleStruct stockcheck in StockCheckList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.trackno);
                ct1.Add(stockcheck.station);
                ct1.Add(stockcheck.tester);
                ct1.Add(stockcheck.inputdate);

                ct1.Add(stockcheck.result);

                ct1.Add(stockcheck.product);
                ct1.Add(stockcheck.vendor);
                ct1.Add(stockcheck.custommaterialNo);
                ct1.Add(stockcheck.mb_brief);
                ct1.Add(stockcheck.mpn);

                ctest1.contentArray = ct1;
                firstsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(firstsheet);

            allContent secondsheet = new allContent();
            secondsheet.sheetName = "统计信息" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-');
            secondsheet.titleList = new List<string>();
            secondsheet.contentList = new List<object>();

            secondsheet.titleList.Add("站别");
            secondsheet.titleList.Add("数量");

            foreach (lifeCycleSumStruct stockcheck in bagWaitSumList)
            {
                ExportExcelContent ctest1 = new ExportExcelContent();
                List<string> ct1 = new List<string>();
                ct1.Add(stockcheck.station);
                ct1.Add(stockcheck.returnNum + "");

                ctest1.contentArray = ct1;
                secondsheet.contentList.Add(ctest1);
            }

            allcontentList.Add(secondsheet);


            Utils.createMulitSheetsUsingNPOI("主板生命周期信息" + "-" + startTime.Replace('/', '-') + "-" + endTime.Replace('/', '-') + ".xls", allcontentList);
        }
    }

    public class lifeCycleSumStruct
    {
        public string station;
        public int returnNum = 1;

        public bool equals(string station)
        {
            return (this.station == station);
        }

        public void incrementNum()
        {
            this.returnNum++;
        }
    }

    //
    public class lifeCycleStruct
    {
        public string trackno;
        public string station;

        public string tester;
        public string inputdate;

        public string vendor;
        public string product;
        public string custommaterialNo;
        public string mb_brief;
        public string mpn;

        public string result;////测试 要测试结果，p/f
            //外观，p/f
            //bga 换件
            //维修 维修现象/结果
            //收货 不良现象
    }
}
