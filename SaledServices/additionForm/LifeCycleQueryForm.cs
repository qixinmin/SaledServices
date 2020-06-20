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

                string trackno = "";
                if (is8s)
                {
                    cmd.CommandText = "select track_serial_no from DeliveredTable where custom_serial_no='" + query + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
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


                cmd.CommandText = "select * from stationInfoRecord where  trackno='" + trackno + "'";
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "ID", "跟踪条码", "站别", "输入人", "输入时间" };
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
    }
}
