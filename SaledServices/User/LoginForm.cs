using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            this.Hide();
            if (mUserDetailForm == null || mUserDetailForm.IsDisposed)
            {
                mUserDetailForm = new UserDetailForm();
                mUserDetailForm.MdiParent = mParent;

            }
            //else
            {
                mUserDetailForm.BringToFront();
                mUserDetailForm.Show();
            }


            mParent.changeMenu(MenuType.LOGIN_MENU);
            
        }
    }
}
