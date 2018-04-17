using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.User;
using RestSharp;


namespace SaledServices
{
    public partial class LoginForm : Form
    {
        private UserDetailForm mUserDetailForm;
        private MainForm mParent;
        public LoginForm(MainForm parent)
        {
            InitializeComponent();
            mParent = parent;
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (this.usernameInput.Text.Trim() == "" || this.passwordInput.Text.Trim() == "")
            {
                MessageBox.Show("用户名和密码不能为空");
                return;
            }

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select Id, username, password, super_manager, bga,repair,test_all ,test1,test2 ,receive_return, store,outlook  from users where username = '" + usernameInput.Text.Trim()
                    + "' and password ='" + this.passwordInput.Text.Trim() + "'";
                cmd.CommandType = CommandType.Text;

                SqlDataReader querySdr = cmd.ExecuteReader();
                string temp = "";
                while (querySdr.Read())
                {
                    temp = querySdr[1].ToString();
                    User.UserSelfForm.username = temp;
                    User.UserSelfForm.super_manager = querySdr[2].ToString();
                    User.UserSelfForm.bga = querySdr[3].ToString();
                    User.UserSelfForm.repair = querySdr[4].ToString();
                    User.UserSelfForm.test_all = querySdr[5].ToString();
                    User.UserSelfForm.test1 = querySdr[6].ToString();
                    User.UserSelfForm.test2 = querySdr[7].ToString();
                    User.UserSelfForm.receive_return = querySdr[8].ToString();
                    User.UserSelfForm.store = querySdr[9].ToString();
                    User.UserSelfForm.outlook = querySdr[10].ToString();
                }
                querySdr.Close();

                if (temp == "")
                {
                    MessageBox.Show("用户名与密码不匹配");
                    mConn.Close();
                    return;
                }
                else
                {
                    this.Hide();
                    if (mUserDetailForm == null || mUserDetailForm.IsDisposed)
                    {
                        mUserDetailForm = new UserDetailForm();
                        mUserDetailForm.MdiParent = mParent;
                    }

                    mUserDetailForm.BringToFront();
                    mUserDetailForm.Show();


                    mParent.changeMenu(MenuType.LOGIN_MENU);
                }

                mConn.Close();
            }
            catch (Exception ex)
            { }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string address = "";
            //string sn = "BOXID(7位)";
            //RestClient client = new RestClient(address);            

            //RestRequest request = new RestRequest("resource/{id}", RestSharp.Method.POST);
            //request.AddBody("SET SN=" + sn + "\r\nSET LINE_NAME=1ASSYA\r\nSET GOURP_NAME=ASSY_ISI");          
            //client.Timeout = 500;
            //try
            //{
            //    IRestResponseresponse = client.Execute(request);
            //    var content = response.Content; // raw content as string

            //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //    }
            //    else if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
            //    {
            //        //网络失败
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //网络失败
            //}
        }
                
    }
}
