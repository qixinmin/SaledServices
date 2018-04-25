using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Repair
{
    public partial class RrepareUseListForm : Form
    {
        public RrepareUseListForm()
        {
            InitializeComponent();
            refreshbutton_Click(null, null);
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // string sqlStr = "select top 100 * from fru_smt_out_stock where requester='"+tester+"'";
                string sqlStr = "select * from request_fru_smt_to_store_table where requester='tester'";

                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "request_fru_smt_to_store_table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string[] hTxt = { "机型", "不良位置", "材料mpn", "请求数量", "请求人", "请求日期", "状态" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }

            //数量要减一， 同时如果变成0，数量不显示出来
        }
    }
}
