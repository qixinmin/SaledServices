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
    public partial class RequestToStoreForm : Form
    {
        public RequestToStoreForm()
        {
            InitializeComponent();
        }

        string currentType = "FRUSMT";//
        string status = "request";
        public void setParameters(string track_serial_no, string material_mpn, string material_71pn)
        {
            this.track_serial_notextBox.Text = track_serial_no;
            this.material_mpntextBox.Text = material_mpn;
            this.material_71pntextBox.Text = material_71pn;
            this.FRUSMT.Checked = true;
        }

        private void requestbutton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO request_material_to_store_table VALUES('" 
                        + this.track_serial_notextBox.Text.Trim() + "','"
                        + this.currentType + "','"
                        + this.material_mpntextBox.Text.Trim() + "','"
                        + this.material_71pntextBox.Text.Trim() + "','"
                        + this.numberTextBox.Text.Trim() + "','"
                        + this.status + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("发送请求成功，请到库房领料！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
