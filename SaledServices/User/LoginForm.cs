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
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections;


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
            this.ControlBox = false;
            this.workIdInput.Focus();
            this.workIdInput.SelectAll();
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (this.workIdInput.Text.Trim() == "" || this.passwordInput.Text.Trim() == "")
            {
                MessageBox.Show("工号和密码不能为空");
                return;
            }

            try
            {
                SqlConnection mConn = new SqlConnection(Constlist.ConStr);
                mConn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "select Id, username, _password, super_manager, bga,repair,test_all ,test1,test2 ,receive_return, store,outlook,running,obe  from users where workId = '" + workIdInput.Text.Trim()
                    + "' and _password ='" + this.passwordInput.Text.Trim() + "'";
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

                    User.UserSelfForm.running = querySdr[12].ToString();
                    User.UserSelfForm.obe = querySdr[13].ToString();
                }
                querySdr.Close();

                if (temp == "")
                {
                    MessageBox.Show("工号与密码不匹配");
                    mConn.Close();
                    return;
                }
                else
                {
                    currentUser = temp;//记录用户名

                    this.Hide();
                  
                    //根据user的权限，添加对应的menu，有多少权限对应多少menu
                    mParent.clearAllMenu();



                    if (User.UserSelfForm.super_manager == "True")
                    {
                        mParent.appendMenu(MenuType.Self);
                        mParent.appendMenu(MenuType.Other);     
                        mParent.appendMenu(MenuType.Repair);                        
                        mParent.appendMenu(MenuType.Bga_Repair);                                          
                        mParent.appendMenu(MenuType.TestALL);                        
                        mParent.appendMenu(MenuType.Test1);                        
                        mParent.appendMenu(MenuType.Test2);      
                        mParent.appendMenu(MenuType.Running);      
                        mParent.appendMenu(MenuType.Recieve_Return);                        
                        mParent.appendMenu(MenuType.Outlook);     
                        mParent.appendMenu(MenuType.Obe);      
                        mParent.appendMenu(MenuType.Store);                          
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
                        if (User.UserSelfForm.running == "True")
                        {
                            mParent.appendMenu(MenuType.Running);
                        }
                        if (User.UserSelfForm.obe == "True")
                        {
                            mParent.appendMenu(MenuType.Obe);
                        }
                    }
                    
                }

                mConn.Close();
            }
            catch (Exception ex)
            { }


            if (User.UserSelfForm.isInTest())
            {
                try
                {
                    string dir = Directory.GetCurrentDirectory() + "\\Files\\";
                    string targetDir = string.Format(dir);//this is where testChange.bat lies
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = targetDir;
                    proc.StartInfo.FileName = "syncTime.bat";
                    proc.Start();
                    proc.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                }
            }

            //调用打印机初始化工作
            //Thread mythread = new Thread(printerInit);
            //mythread.Start();
        }
        
        //private static void printerInit()
        //{           
        //}

        private void parseMesReturn(string content)
        {
            string trimSet = content.Replace("SET ", "");
            MessageBox.Show(":"+trimSet+":");
            string[] keyValue = trimSet.Split(new char[]{'=','\n'});

            Hashtable hstContent = new Hashtable();
            for (int i = 0; i < keyValue.Length; i = i + 2)
            {
                hstContent.Add(keyValue[i].Trim(), keyValue[i + 1].Trim()) ;
            }

            if (hstContent["MSG"].ToString() == "OK")
            {

            }            

                   
            foreach (string str in keyValue)
            {
                MessageBox.Show(str.Trim());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parseMesReturn("SET THRESHOLD=98.99%(对比阀值)\n"+"SET PORTIDBOM=8C16451DE86B");

            string address = "http://192.168.1.1";
            string sn = "BOXID(7位)";
            RestClient client = new RestClient(address);

            RestRequest request = new RestRequest(RestSharp.Method.POST);
            request.AddBody("SET SN=" + sn + "\r\nSET LINE_NAME=1ASSYA\r\nSET GOURP_NAME=ASSY_ISI");
            client.Timeout = 500;//ms
            try
            {
                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //内容解析
                }
                else// if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    //网络失败
                }
            }
            catch (Exception ex)
            {
                //网络失败
            }
        }

        private void workIdInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                this.passwordInput.Focus();
                this.passwordInput.SelectAll();
            }
        }

        private void passwordInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                login_Click(null, null);
            }
        }
                
    }
}
