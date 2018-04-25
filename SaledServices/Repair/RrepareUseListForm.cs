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
        private RepairOperationForm mParentForm;
        public RrepareUseListForm(RepairOperationForm parentForm)
        {
            InitializeComponent();
            refreshbutton_Click(null, null);
            this.mParentForm = parentForm;

            if (parentForm != null)
            {
                this.choosebutton.Enabled = true;
            }
            else
            {
                this.choosebutton.Enabled = false;
            }
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // string sqlStr = "select top 100 * from fru_smt_out_stock where requester='"+tester+"'";
                string sqlStr = "select mb_brief,material_mpn,stock_place,realNumber,usedNumber,Id,fromId from request_fru_smt_to_store_table /*where requester='tester'*/";

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

            string[] hTxt = { "机型", "材料mpn", "库位","已有数量","使用过的数量","ID","FromId" };
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
                dataGridView1.Columns[i].Name = hTxt[i];
            }

            //数量要减一， 同时如果变成0，数量不显示出来
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.mb_brieftextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
            this.material_mpntextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
            this.stock_placetextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
            this.realNumbertextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
            this.usedNumbertextBox.Text = dataGridView1.SelectedCells[4].Value.ToString();
            this.idTextBox.Text = dataGridView1.SelectedCells[5].Value.ToString();
            this.fromIdTextBox.Text = dataGridView1.SelectedCells[6].Value.ToString();
        }

        private string totalUseNumber ="";
        //private string totalNumber;
        private void thisNumbertextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                //检查输入的数量不能大于能使用的数量
                if (this.realNumbertextBox.Text == "")
                {
                    MessageBox.Show("存储的数量请点击出来！");
                    return;
                }

                if (this.thisNumbertextBox.Text == "")
                {
                    MessageBox.Show("要使用的数量请填入！");
                    return;
                }

                try 
                {
                    int totalNumber = Int32.Parse(this.realNumbertextBox.Text);
                    int usedNumber = Int32.Parse(this.usedNumbertextBox.Text);
                    int thisTryToUse = Int32.Parse(this.thisNumbertextBox.Text);

                    if (thisTryToUse + usedNumber > totalNumber)
                    {
                        MessageBox.Show("输入的数量不能大于能使用的数量!");
                        this.thisNumbertextBox.Clear();
                        this.thisNumbertextBox.Focus();
                    }
                    else
                    {
                        totalUseNumber = (thisTryToUse + usedNumber).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void choosebutton_Click(object sender, EventArgs e)
        {
            mParentForm.setPrepareUseDetail(idTextBox.Text, mb_brieftextBox.Text, material_mpntextBox.Text, stock_placetextBox.Text, this.thisNumbertextBox.Text, totalUseNumber);
            this.Close();
        }

        private void returnMaterialbutton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO fru_smt_return_store_record VALUES('"
                        + this.material_mpntextBox.Text.Trim() + "','"
                        + (Int32.Parse(this.realNumbertextBox.Text == "" ? "0" : this.realNumbertextBox.Text) - Int32.Parse(this.totalUseNumber == "" ? "0" : this.totalUseNumber)) + "','"
                        + this.stock_placetextBox.Text.Trim() + "','"
                        + "tester" + "','"
                        + DateTime.Now.ToString("yyyy/MM/dd") + "','"
                        + this.fromIdTextBox.Text + "','"
                        + "" + "','"
                        + "" + "','"
                        + "request" + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("发送请求成功，请归还物料到库房, 库房才能消除本条申请！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
