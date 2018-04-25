using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Store
{
    public partial class CheckRequestForm : Form
    {
        public CheckRequestForm()
        {
            InitializeComponent();
            loadInfo();
        }

        private void loadInfo()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  request_fru_smt_to_store_table";
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
         
            string[] hTxt = { "ID", "机型", "位置","材料MPN","请求数量","申请人","申请日期","状态"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.idTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.not_good_placetextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.materialMpnTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.requestNumbertextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.requestertextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.dateTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
            this.statustextBox.Text  = dataGridView1.SelectedCells[7].Value.ToString(); 
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            loadInfo();
        }

        private void processRequestbutton_Click(object sender, EventArgs e)
        {
            //在处理之前的时候可以使用mpn的字段查询库存中是否有相应的内容，如果没有则提示说此库存没有相关库存，要备料
            FRU_SMT_OutSheetForm frusmtout = new FRU_SMT_OutSheetForm();
            frusmtout.setparamters(this.mb_brieftextBox.Text.Trim(), this.materialMpnTextBox.Text.Trim(),  this.requestNumbertextBox.Text.Trim(), this.idTextBox.Text.Trim());
            frusmtout.Show();
            frusmtout.doRequestUsingMpn();

            //在处理完请求后需要把本条记录状态修改为close或其他状态

        }
    }
}
