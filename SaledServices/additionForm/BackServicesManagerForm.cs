using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.additionForm
{
    public partial class BackServicesManagerForm : Form
    {
        public BackServicesManagerForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           //修改数据库
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select able from functionControltable where funtion='test1_2_jump'";

                SqlDataReader querySdr = cmd.ExecuteReader();
                string able = "";
                while (querySdr.Read())
                {
                    able = querySdr[0].ToString();
                }
                querySdr.Close();

                if (able == "")//不存在，插入
                {
                    if (test1_2combobox.Text == "跳过")
                    {
                        cmd.CommandText = "insert into  functionControltable values('test1_2_jump','0', '')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "insert into  functionControltable values('test1_2_jump','1', '')";
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    //存在，更新
                    if (test1_2combobox.Text == "跳过")
                    {
                        cmd.CommandText = "update functionControltable set able ='0' where funtion='test1_2_jump'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "update functionControltable set able ='1' where funtion='test1_2_jump'";
                        cmd.ExecuteNonQuery();
                    }
                }

                mConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show("修改成功！");
        }

        private void BackServicesManagerForm_Load(object sender, EventArgs e)
        {
            //查询数据库
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select able from functionControltable where funtion='test1_2_jump'";

                SqlDataReader querySdr = cmd.ExecuteReader();
                string able = "";
                while (querySdr.Read())
                {
                    able = querySdr[0].ToString();
                }
                querySdr.Close();
                if (able == "" || able == "0")
                {                   
                    test1_2combobox.Text = "跳过";
                }
                else {
                    test1_2combobox.Text = "不跳过";
                }

                cmd.CommandText = "select able from functionControltable where funtion='test2_jump'";

                querySdr = cmd.ExecuteReader();
                able = "";
                while (querySdr.Read())
                {
                    able = querySdr[0].ToString();
                }
                querySdr.Close();
                if (able == "" || able == "0")
                {
                    this.test2comboBox.Text = "跳过";
                }
                else
                {
                    test2comboBox.Text = "不跳过";
                }

                mConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void test2comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //修改数据库
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select able from functionControltable where funtion='test2_jump'";

                SqlDataReader querySdr = cmd.ExecuteReader();
                string able = "";
                while (querySdr.Read())
                {
                    able = querySdr[0].ToString();
                }
                querySdr.Close();

                if (able == "")//不存在，插入
                {
                    if (test2comboBox.Text == "跳过")
                    {
                        cmd.CommandText = "insert into  functionControltable values('test2_jump','0', '')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "insert into  functionControltable values('test2_jump','1', '')";
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    //存在，更新
                    if (test2comboBox.Text == "跳过")
                    {
                        cmd.CommandText = "update functionControltable set able ='0' where funtion='test2_jump'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = "update functionControltable set able ='1' where funtion='test2_jump'";
                        cmd.ExecuteNonQuery();
                    }
                }

                mConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show("修改成功！");
        }

    }
}
