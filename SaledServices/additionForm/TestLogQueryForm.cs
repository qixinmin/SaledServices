using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class TestLogQueryForm : Form
    {
        public TestLogQueryForm()
        {
            InitializeComponent();
        }

        //厂商 订单编号 客户料号 跟踪条码 客户序号 mb描述 测试开始时间 测试结束时间 测试人
        class useClass
        {
            public string vendor { get; set; }
            public string orderNo { get; set; }
            public string customNo { get; set; }

            public string trackNo { get; set; }
            public string customSerialNo { get; set; }
            public string mb_describe { get; set; }

            public string testStart { get; set; }
            public string testEnd { get; set; }
            public string tester { get; set; }
        }

        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection mConn;

                if (this.trackNoTxt.Text.Trim().Equals(""))
                {
                    MessageBox.Show("请输入跟踪条码内容！");
                    return;
                }

                List<useClass> list = new List<useClass>();

                mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select vendor,custom_order,custommaterialNo,track_serial_no,custom_serial_no,mb_describe from DeliveredTable where track_serial_no='" + this.trackNoTxt.Text.Trim() + "' ";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                        
                while (querySdr.Read())
                {
                    useClass useclass = new useClass();
                    useclass.vendor = querySdr[0].ToString();
                    useclass.orderNo = querySdr[1].ToString();
                    useclass.customNo = querySdr[2].ToString();
                    useclass.trackNo = querySdr[3].ToString();
                    useclass.customSerialNo = querySdr[4].ToString();
                    useclass.mb_describe = querySdr[5].ToString();

                    list.Add(useclass);
                }

                querySdr.Close();

                foreach (useClass temp in list)
                {
                    cmd.CommandText = "select inputdate,inputer from stationInfoRecord where trackno ='" + temp.trackNo + "' and station='Test1'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.testStart = querySdr[0].ToString();
                        temp.tester = querySdr[1].ToString();
                    }
                    querySdr.Close();

                    cmd.CommandText = "select inputdate from stationInfoRecord where trackno ='" + temp.trackNo + "' and station='Test2'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.testEnd = querySdr[0].ToString();
                    }
                    querySdr.Close();
                }

                dataGridView1.DataSource = list;
                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //厂商 订单编号 客户料号 跟踪条码 客户序号 mb描述 测试开始时间 测试结束时间 测试人
            string[] hTxt = { "厂商","订单编号","客户料号","跟踪条码","客户序号","MB描述","测试开始时间","测试结束时间","测试人" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

        }
    }
}
