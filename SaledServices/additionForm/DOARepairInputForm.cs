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
    public partial class DOARepairInputForm : Form
    {
        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;
        private String tableName = "DeliveredTable";

        public DOARepairInputForm()
        {
            InitializeComponent();
        }

        private void faultIndexTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this._8sTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
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
                cmd.CommandText = "select mb_brief, vendor_serail_no,custom_serial_no,customFault,fault_type,mbfa1,not_good_place,repair_date,repairer from repair_record_table where  custom_serial_no='" + this._8sTextBox.Text.Trim() + "'";
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

            string[] hTxt = { "MB简称","厂商序号","客户序号","客户故障","故障类型","FA分析","维修位置","维修时间","维修人"};
            for (int i = 0; i < hTxt.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = hTxt[i];
            }

            MessageBox.Show("查询完成！");
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            if (this._8sTextBox.Text.Trim() == "" || this.responseComboBox.Text.Trim() == ""
                || this.analysisStepTextBox.Text.Trim() == ""
                || this.improveActionTextBox.Text.Trim() == "")
            {
                MessageBox.Show("输入的内容为空");
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
                    cmd.CommandText = "INSERT INTO doa_analysis VALUES('" 
                        + this._8sTextBox.Text.Trim() + "','"
                        + this.responseComboBox.Text.Trim() + "','"
                        + this.analysisStepTextBox.Text.Trim() + "','"
                        + this.improveActionTextBox.Text.Trim() + "','"
                        + LoginForm.currentUser + "','"
                        + DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("新增成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DOARepairInputForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.GetType().
          GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
          SetValue(tableLayoutPanel1, true, null);
        }
    }
}
