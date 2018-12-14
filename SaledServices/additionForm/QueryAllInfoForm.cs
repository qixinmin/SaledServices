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

                querybytracknos("trackno");
            }
        }

        private void querybytracknos(string type)
        {
            try
            {
                mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandType = CommandType.Text;

                    List<string> tracknos = new List<string>();
                    string result = "", querycolumn = "", querycontent = "";
                    switch (type)
                    {
                        case "trackno":
                            querycolumn = "track_serial_no";
                            querycontent = this.tracker_bar_textBox.Text.Trim();
                            break;
                        case "_8s":
                            querycolumn = "custom_serial_no";
                            querycontent = this._8sCodetextBox.Text.Trim();
                            break;
                        case "vendor_serial_no":
                            querycolumn = "vendor_serail_no";
                            querycontent = this.vendor_serial_no_textBox.Text.Trim();
                            break;
                    }
                    cmd.CommandText = "select distinct(track_serial_no) from  DeliveredTable where " + querycolumn + "='" + querycontent + "'";
                    SqlDataReader querySdr = cmd.ExecuteReader();
                    while (querySdr.Read())
                    {
                        tracknos.Add(querySdr[0].ToString());
                    }
                    querySdr.Close();

                    if (tracknos.Count <= 0)
                    {
                        MessageBox.Show("没有内容，请检查输入！");
                        mConn.Close();
                        return;
                    }
                    //where track_serial_no in ('RMLB181014010005','RMLB181014010022')
                    result = "('";
                    for (int i = 0; i < tracknos.Count; i++)//(string str in tracknos)
                    {
                        if (i < tracknos.Count - 1)
                        {
                            result += tracknos[i] + "','";
                        }
                        else if (i == tracknos.Count - 1)
                        {
                            result += tracknos[i] + "')";
                        }
                    }

                    sda = new SqlDataAdapter();

                    cmd.CommandText = "select * from  DeliveredTable where track_serial_no  in " + result;

                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "DeliveredTable");
                    this.dataGridView_receiveOrder.DataSource = ds.Tables[0];
                    dataGridView_receiveOrder.RowHeadersVisible = false;

                    cmd.CommandText = "select * from  repair_record_table where track_serial_no in " + result;

                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "repair_record_table");
                    this.dataGridView_repair.DataSource = ds.Tables[0];
                    dataGridView_repair.RowHeadersVisible = false;

                    cmd.CommandText = "select * from  bga_repair_record_table where track_serial_no in " + result;

                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "bga_repair_record_table");
                    this.dataGridView_bga.DataSource = ds.Tables[0];
                    dataGridView_bga.RowHeadersVisible = false;

                    cmd.CommandText = "select * from  test1table where track_serial_no in " + result;

                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "test1table");
                    this.dataGridView_test1.DataSource = ds.Tables[0];
                    dataGridView_test1.RowHeadersVisible = false;

                    cmd.CommandText = "select * from  test2table where track_serial_no in " + result;

                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "test2table");
                    this.dataGridView_test2.DataSource = ds.Tables[0];
                    dataGridView_test2.RowHeadersVisible = false;

                    cmd.CommandText = "select * from  testalltable where track_serial_no in " + result;

                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "testalltable");
                    this.dataGridView_testall.DataSource = ds.Tables[0];
                    dataGridView_testall.RowHeadersVisible = false;


                    cmd.CommandText = "select * from  returnStore where track_serial_no in " + result;

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

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void _8sCodetextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this._8sCodetextBox.Text.Trim() == "")
                {
                    this._8sCodetextBox.Focus();
                    MessageBox.Show("内容为空，请检查！");
                    return;
                }

                querybytracknos("_8s");
            }
        }

        private void vendor_serial_no_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                if (this.vendor_serial_no_textBox.Text.Trim() == "")
                {
                    this.vendor_serial_no_textBox.Focus();
                    MessageBox.Show("内容为空，请检查！");
                    return;
                }

                querybytracknos("vendor_serial_no");
            }
        }
    }
}
