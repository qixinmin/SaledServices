using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.Test_Outlook
{
    public partial class CheckInfoForm : Form
    {
        private String tableName = "stationInformation";
        public CheckInfoForm()
        {
            InitializeComponent();

            this.tracker_bar_textBox.Focus();
        }

        private void tracker_bar_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.tracker_bar_textBox.Text.Trim() == "" && this.textBox8s.Text.Trim() == "")
                {
                    this.tracker_bar_textBox.Focus();
                    MessageBox.Show("内容为空，请检查！");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    if (this.tracker_bar_textBox.Text.Trim() != "")
                    {
                        cmd.CommandText = "select custom_serial_no,dpk_status from DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string textBox8s = "", dpk_status="";
                        while (querySdr.Read())
                        {
                            textBox8s = querySdr[0].ToString();
                            dpk_status = querySdr[1].ToString();
                        }
                        querySdr.Close();

                        if (textBox8s != "")
                        {
                            this.textBox8s.Text = textBox8s;
                            this.dpkstatus.Text = dpk_status;
                        }
                        else
                        {
                            cmd.CommandText = "select MB11S from CSD_old_data where 跟踪条码='" + this.tracker_bar_textBox.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();
                            textBox8s = "";
                            while (querySdr.Read())
                            {
                                textBox8s = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            this.textBox8s.Text = textBox8s;
                        }
                    }
                    else if(this.textBox8s.Text.Trim() != "")
                    {
                        cmd.CommandText = "select track_serial_no,dpk_status from DeliveredTable where custom_serial_no='" + this.textBox8s.Text.Trim() + "'";
                        SqlDataReader querySdr = cmd.ExecuteReader();
                        string track_serial_no = "",dpkstatus="";
                        while (querySdr.Read())
                        {
                            track_serial_no = querySdr[0].ToString();
                            dpkstatus = querySdr[1].ToString();
                        }
                        querySdr.Close();

                        if (track_serial_no != "")
                        {
                            this.tracker_bar_textBox.Text = track_serial_no;
                            this.dpkstatus.Text = dpkstatus;
                        }
                        else
                        {
                            cmd.CommandText = "select 跟踪条码 from CSD_old_data where MB11S='" + this.textBox8s.Text.Trim() + "'";
                            querySdr = cmd.ExecuteReader();
                            track_serial_no = "";
                            while (querySdr.Read())
                            {
                                track_serial_no = querySdr[0].ToString();
                            }
                            querySdr.Close();
                            this.tracker_bar_textBox.Text = track_serial_no;
                        }
                    }

                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void textBox8s_KeyPress(object sender, KeyPressEventArgs e)
        {
            tracker_bar_textBox_KeyPress(sender, e);
        }
    }
}
