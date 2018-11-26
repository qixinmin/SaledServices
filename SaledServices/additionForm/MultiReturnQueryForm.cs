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
    public partial class MultiReturnQueryForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "DeliveredTable";

        public MultiReturnQueryForm()
        {
            InitializeComponent();
        }

        private void faultIndexTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.faultIndexTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
                }

                try
                {
                    SqlConnection conn = new SqlConnection(Constlist.ConStr);
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "select Id from " + tableName + " where vendor_serail_no='" + this.faultIndexTextBox.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        int count = 0;
                        while (querySdr.Read())
                        {
                            count++;
                        }
                        querySdr.Close();
                        if (count > 1)
                        {
                            this.query.Enabled = true;
                        }
                        else
                        {
                            this.query.Enabled = false;
                            MessageBox.Show("此厂商序号不是多次返修的机器！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("SaledService is not opened");
                    }

                    conn.Close();                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
     
        private void query_Click(object sender, EventArgs e)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from ("+
                    "select D.track_serial_no, D.vendor_serail_no,D.custom_serial_no," +
                    "D.mb_brief, D.custom_fault,D.order_receive_date," +
                    "S.return_date from DeliveredTable as D, returnStore as S  where D.track_serial_no  = S.track_serial_no" +
                    ") as A where  vendor_serail_no='" + this.faultIndexTextBox.Text.Trim() + "'";
                cmd.CommandType = CommandType.Text;

                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, tableName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "跟踪条码","厂商序号","客户序号","MB简称","客户故障","收货日期","还货日期"};
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
    }
}
