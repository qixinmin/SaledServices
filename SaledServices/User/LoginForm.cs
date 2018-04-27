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
        public static string currentUser = "defaultUser";
      
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
                    User.UserSelfForm.super_manager = querySdr[3].ToString();
                    User.UserSelfForm.bga = querySdr[4].ToString();
                    User.UserSelfForm.repair = querySdr[5].ToString();
                    User.UserSelfForm.test_all = querySdr[6].ToString();
                    User.UserSelfForm.test1 = querySdr[7].ToString();
                    User.UserSelfForm.test2 = querySdr[8].ToString();
                    User.UserSelfForm.receive_return = querySdr[9].ToString();
                    User.UserSelfForm.store = querySdr[10].ToString();
                    User.UserSelfForm.outlook = querySdr[11].ToString();
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
                    currentUser = this.usernameInput.Text.Trim();//记录用户名

                    this.Hide();
                  
                    //根据user的权限，添加对应的menu，有多少权限对应多少menu
                    mParent.clearAllMenu();

                    if (User.UserSelfForm.super_manager == "True")
                    {
                        mParent.appendMenu(MenuType.Self);  
                        mParent.appendMenu(MenuType.Repair);                        
                        mParent.appendMenu(MenuType.Bga_Repair);                       
                        mParent.appendMenu(MenuType.TestALL);                       
                        mParent.appendMenu(MenuType.TestALL);                        
                        mParent.appendMenu(MenuType.Test1);                        
                        mParent.appendMenu(MenuType.Test2);                        
                        mParent.appendMenu(MenuType.Recieve_Return);                        
                        mParent.appendMenu(MenuType.Outlook);                        
                        mParent.appendMenu(MenuType.Store);
                        mParent.appendMenu(MenuType.Other);       
                    }
                    else
                    {
                        mParent.appendMenu(MenuType.Self);  
                        if (User.UserSelfForm.repair == "True")
                        {
                            mParent.appendMenu(MenuType.Repair);
                        }
                        if (User.UserSelfForm.bga == "True")
                        {
                            mParent.appendMenu(MenuType.Bga_Repair);
                        }
                        if (User.UserSelfForm.test_all == "True")
                        {
                            mParent.appendMenu(MenuType.TestALL);
                        }
                        if (User.UserSelfForm.test_all == "True")
                        {
                            mParent.appendMenu(MenuType.TestALL);
                        }
                        if (User.UserSelfForm.test1 == "True")
                        {
                            mParent.appendMenu(MenuType.Test1);
                        }
                        if (User.UserSelfForm.test2 == "True")
                        {
                            mParent.appendMenu(MenuType.Test2);
                        }
                        if (User.UserSelfForm.receive_return == "True")
                        {
                            mParent.appendMenu(MenuType.Recieve_Return);
                        }
                        if (User.UserSelfForm.outlook == "True")
                        {
                            mParent.appendMenu(MenuType.Outlook);
                        }
                        if (User.UserSelfForm.store == "True")
                        {
                            mParent.appendMenu(MenuType.Store);
                        }
                    }
                    
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
