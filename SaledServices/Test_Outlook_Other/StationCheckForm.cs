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
    public partial class StationCheckForm : Form
    {
        private String tableName = "stationInformation";
        public StationCheckForm()
        {
            InitializeComponent();

            this.tracker_bar_textBox.Focus();

            if (User.UserSelfForm.isSuperManager() == false)
            {
                this.stationComboBox.Visible = false;
                this.modify.Visible = false;
            }
            else
            {
                this.stationComboBox.Visible = true;
                this.modify.Visible = true;
            }
        }

        private void tracker_bar_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.tracker_bar_textBox.Text.Trim() == "")
                {
                    this.tracker_bar_textBox.Focus();
                    MessageBox.Show("追踪条码的内容为空，请检查！");
                    return;
                }

                try
                {
                    SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                    mConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select station from stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                    SqlDataReader querySdr = cmd.ExecuteReader();
                    string station = "";
                    while (querySdr.Read())
                    {
                        station = querySdr[0].ToString();                        
                    }
                    querySdr.Close();

                    if (station != "")
                    {
                        this.stationlabel.Text = station;
                    }
                    else
                    {
                        this.stationlabel.Text = "没有站别信息！";
                    }
                    mConn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT station FROM stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        MessageBox.Show("没有相关站别信息，请检查序列号是否正确");
                        querySdr.Close();
                        conn.Close();
                        return;
                    }
                    querySdr.Close();
                    cmd.CommandText = "update stationInformation set station = '"+this.stationComboBox.Text.Trim()+"', updateDate = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' "
                              + "where track_serial_no = '" + this.tracker_bar_textBox.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("修改站别数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Constlist.ConStr);
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT station FROM stationInformation where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    if (querySdr.HasRows == false)
                    {
                        querySdr.Close();
                        cmd.CommandText = "INSERT INTO stationInformation VALUES('"
                       + this.tracker_bar_textBox.Text.ToUpper().Trim() + "','收货','"
                       + DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("序列号已经存在，不能用新增功能");                      
                        querySdr.Close();
                        conn.Close();
                        return;
                    }                    
                }
                else
                {
                    MessageBox.Show("SaledService is not opened");
                }

                conn.Close();
                MessageBox.Show("新增站别数据OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
