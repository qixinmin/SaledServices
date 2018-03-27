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
    public partial class UserDetailForm : Form
    {
        
        public UserDetailForm()
        {
            InitializeComponent();
        }

        private void UserDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm form = this.MdiParent as MainForm;
            form.changeMenu(MenuType.MAIN_MENU);

        }
    }
}
