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
    public partial class QueryAllInfoForm : Form
    {
        private String tableName = "stationInformation";

        private SqlConnection mConn;
        private DataSet ds;
        private SqlDataAdapter sda;

        public QueryAllInfoForm()
        {
            InitializeComponent();

            this.tracker_bar_textBox.Focus();
        }

        private void tracker_bar_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.tracker_bar_textBox.Text.Trim() == "")
                {
                    this.tracker_bar_textBox.Focus();
                    MessageBox.Show("内容为空，请检查！");
                    return;
                }

                try
                {
                    mConn = new SqlConnection(Constlist.ConStr);
                   
                    if (this.tracker_bar_textBox.Text.Trim() != "")
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = mConn;
                            cmd.CommandType = CommandType.Text;

                            sda = new SqlDataAdapter();

                            cmd.CommandText = "select * from  DeliveredTable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "DeliveredTable");
                            this.dataGridView_receiveOrder.DataSource = ds.Tables[0];
                            dataGridView_receiveOrder.RowHeadersVisible = false;


                            cmd.CommandText = "select * from  repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "repair_record_table");
                            this.dataGridView_repair.DataSource = ds.Tables[0];
                            dataGridView_repair.RowHeadersVisible = false;

                            cmd.CommandText = "select * from  bga_repair_record_table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "bga_repair_record_table");
                            this.dataGridView_bga.DataSource = ds.Tables[0];
                            dataGridView_bga.RowHeadersVisible = false;

                            cmd.CommandText = "select * from  test1table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "test1table");
                            this.dataGridView_test1.DataSource = ds.Tables[0];
                            dataGridView_test1.RowHeadersVisible = false;

                            cmd.CommandText = "select * from  test2table where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "test2table");
                            this.dataGridView_test2.DataSource = ds.Tables[0];
                            dataGridView_test2.RowHeadersVisible = false;

                            cmd.CommandText = "select * from  testalltable where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "testalltable");
                            this.dataGridView_testall.DataSource = ds.Tables[0];
                            dataGridView_testall.RowHeadersVisible = false;


                            cmd.CommandText = "select * from  returnStore where track_serial_no='" + this.tracker_bar_textBox.Text.Trim() + "'";

                            sda.SelectCommand = cmd;
                            ds = new DataSet();
                            sda.Fill(ds, "returnStore");
                            this.dataGridView_return.DataSource = ds.Tables[0];
                            dataGridView_return.RowHeadersVisible = false;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
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
