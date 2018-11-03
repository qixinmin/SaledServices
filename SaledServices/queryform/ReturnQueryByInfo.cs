using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.queryform
{
    public partial class ReturnQueryByInfo : Form
    {
        public ReturnQueryByInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.mbBrieftextBox.Text.Trim() == "" && this.customMaterialNotextBox.Text.Trim() == "")
            {
                MessageBox.Show("查询必须有内容");
                return;
            }
            try
            {
                this.dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;

                List<returnQuery> list = new List<returnQuery>();
                List<returnQuery> listtarget = new List<returnQuery>();

                string sql = "select track_serial_no,custommaterialno,mb_brief from DeliveredTable ";
                if (this.mbBrieftextBox.Text != "")
                {
                    sql += "where mb_brief like '%" + this.mbBrieftextBox.Text + "%'";
                }

                if (this.customMaterialNotextBox.Text != "")
                {
                    if (sql.Contains("where"))
                    {
                        sql += " and custommaterialNo like '%" + this.customMaterialNotextBox.Text + "%'";
                    }
                    else
                    {
                        sql += " where custommaterialNo like '%" + this.customMaterialNotextBox.Text + "%'";
                    }
                   
                }                              

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    returnQuery useclass = new returnQuery();
                    useclass.trackerNo= querySdr[0].ToString();
                    useclass.custommaterialno = querySdr[1].ToString();
                    useclass.mbbrief = querySdr[2].ToString();
                    list.Add(useclass);
                }
                querySdr.Close();

                foreach (returnQuery temp in list)
                {
                    cmd.CommandText = "select Id,station from stationInformation where station !='return' and track_serial_no='" + temp.trackerNo + "'";
                    querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        temp.station = querySdr[1].ToString();
                        listtarget.Add(temp);
                    }
                    querySdr.Close();
                }

                dataGridView1.DataSource = listtarget;

                dataGridView1.RowHeadersVisible = false;
                mConn.Close();

                string[] hTxt = { "跟踪条码", "站别", "客户料号", "MB简称" };
                for (int i = 0; i < hTxt.Length; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hTxt[i];
                    dataGridView1.Columns[i].Name = hTxt[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    class returnQuery
    {
        public string trackerNo{ get; set; }
        public string station { get; set; }
        public string custommaterialno { get; set; }
        public string mbbrief { get; set; }
    }
}
