using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices.User
{
    public partial class UserSelfForm : Form
    {
        private String tableName = "users";

        public static string username = "";

        public UserSelfForm()
        {
            InitializeComponent();

            loadUserInfo();
        }

        private void loadUserInfo()
        {
            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  " + tableName +" where username = '"+username+"'";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();

                while (querySdr.Read())
                {
                    string temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.usernameTextBox.Text = querySdr[1].ToString();
                        //this.passwordTextBox.Text = querySdr[2].ToString();

                        if (querySdr[3].ToString() == "True")
                        {
                            this.super_checkBox.Checked = true;
                        }
                        else
                        {
                            this.super_checkBox.Checked = false;
                        }


                        if (querySdr[4].ToString() == "True")
                        {
                            this.bgaCheckBox.Checked = true;
                        }
                        else
                        {
                            this.bgaCheckBox.Checked = false;
                        }


                        if (querySdr[5].ToString() == "True")
                        {
                            this.repairCheckBox.Checked = true;
                        }
                        else
                        {
                            this.repairCheckBox.Checked = false;
                        }


                        if (querySdr[6].ToString() == "True")
                        {
                            this.test_allCheckBox.Checked = true;
                        }
                        else
                        {
                            this.test_allCheckBox.Checked = false;
                        }

                        if (querySdr[7].ToString() == "True")
                        {
                            this.test1CheckBox.Checked = true;
                        }
                        else
                        {
                            this.test1CheckBox.Checked = false;
                        }

                        if (querySdr[8].ToString() == "True")
                        {
                            this.test2CheckBox.Checked = true;
                        }
                        else
                        {
                            this.test2CheckBox.Checked = false;
                        }

                        if (querySdr[9].ToString() == "True")
                        {
                            this.receive_returnCheckBox.Checked = true;
                        }
                        else
                        {
                            this.receive_returnCheckBox.Checked = false;
                        }

                        if (querySdr[10].ToString() == "True")
                        {
                            this.storeCheckBox.Checked = true;
                        }
                        else
                        {
                            this.storeCheckBox.Checked = false;
                        }

                        if (querySdr[11].ToString() == "True")
                        {
                            this.outlookCheckBox.Checked = true;
                        }
                        else
                        {
                            this.outlookCheckBox.Checked = false;
                        }     
                    }
                }
                querySdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void modifyPassButton_Click(object sender, EventArgs e)
        {
            //修改密码，首先判断用户名和密码的组合是否正确，否则报错，更新数据库，权限的内容只能看不能改

            if (this.newpassTextBox.Text.Trim() != this.confirmPassTextBox.Text.Trim())
            {
                MessageBox.Show("2次输入的密码不一样！");
                return;
            }

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select * from  " + tableName + " where username = '" + username
                    + "' and password ='" + this.oripasswordTextBox.Text.Trim() + "'";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string temp = "";
                while (querySdr.Read())
                {
                    temp = querySdr[0].ToString();
                    if (temp != "")
                    {
                        this.usernameTextBox.Text = querySdr[1].ToString();                        
                    }
                }
                querySdr.Close();
                if (temp == "")
                {
                    MessageBox.Show("用户名与密码不匹配");
                    return;
                }

                //更新密码，并提示
                cmd.CommandText = "update users set password = '" + this.confirmPassTextBox.Text.Trim()
                               + "' where username = '" + username + "'";
                cmd.ExecuteNonQuery();

                mConn.Close();
                MessageBox.Show("更新密码成功,请退出后重新登录！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
