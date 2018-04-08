using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SaledServices
{
    public partial class MainForm : Form
    {
        private LoginForm mLoginForm = null;
        private VendorForm mVendorForm = null;
        private ExcelImportForm mExcelForm = null;

        private MBMaterialCompareForm mbFrom = null;

        private ReceiveOrderForm roForm = null;

        private List<Form> allForm = new List<Form>();
        
        public MainForm()
        {
            InitializeComponent();
        }

        public void changeMenu(MenuType menuType)
        {
            //this.AllMenuStrip.Items.Clear();
            //switch (menuType)
            //{
            //    case MenuType.LOGIN_MENU:
            //        this.LoginMenuItem.Name = "LoginMenuItem";
            //        this.LoginMenuItem.Size = new System.Drawing.Size(152, 22);
            //        this.LoginMenuItem.Text = "登录";
            //        this.LoginMenuItem.Click += new System.EventHandler(this.LoginMenuItem_Click);
            //        this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //        this.LoginMenuItem});      
            //        break;
            //    case MenuType.MAIN_MENU:
            //        this.UserManageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //            this.LoginMenuItem,
            //            this.LogoutMenuItem});
            //        this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //            this.UserManageMenuItem,
            //            this.VendorMenuItem,
            //            this.FunctionMenuItem});
            //        break;
            //}

            this.LogoutMenuItem.Enabled = true;
        }

       
        private void LoginMenuItem_Click(object sender, EventArgs e)
        {
            if (mLoginForm == null || mLoginForm.IsDisposed)
            {
                mLoginForm = new LoginForm(this);
                mLoginForm.MdiParent = this;
            }

            mLoginForm.BringToFront();
            mLoginForm.Show();

            allForm.Add(mLoginForm);
        }

        private void VendorChangeMenuItem_Click(object sender, EventArgs e)
        {
            if (mVendorForm == null || mVendorForm.IsDisposed)
            {
                mVendorForm = new VendorForm();
                mVendorForm.MdiParent = this;
            }

            mVendorForm.BringToFront();
            mVendorForm.Show();

            allForm.Add(mVendorForm);
        }

        private void ExcelImportMenuItem_Click(object sender, EventArgs e)
        {
            if (mExcelForm == null || mExcelForm.IsDisposed)
            {
                mExcelForm = new ExcelImportForm();
                mExcelForm.MdiParent = this;
            }

            mExcelForm.BringToFront();
            mExcelForm.Show();

            allForm.Add(mExcelForm);
        }

        private void mBMaterialCompareMenuItem_Click(object sender, EventArgs e)
        {
            if (mbFrom == null || mbFrom.IsDisposed)
            {
                mbFrom = new MBMaterialCompareForm();
                mbFrom.MdiParent = this;
            }

            mbFrom.WindowState = FormWindowState.Maximized;
            mbFrom.BringToFront();
            mbFrom.Show();

            allForm.Add(mbFrom);
        }

        private void receiveOrderMenuItem_Click(object sender, EventArgs e)
        { 
           
        }

        private SourceForm sourceForm;
        private void sourceMenuItem_Click(object sender, EventArgs e)
        {
            if (sourceForm == null || sourceForm.IsDisposed)
            {
                sourceForm = new SourceForm();
                sourceForm.MdiParent = this;
            }

            sourceForm.BringToFront();
            sourceForm.Show();

            allForm.Add(sourceForm);
        }

        private CustomFaultForm customFaultForm;
        private void customFaultMenuItem_Click(object sender, EventArgs e)
        {
            if (customFaultForm == null || customFaultForm.IsDisposed)
            {
                customFaultForm = new CustomFaultForm();
                customFaultForm.MdiParent = this;
            }

            customFaultForm.BringToFront();
            customFaultForm.Show();

            allForm.Add(customFaultForm);
        }

        private GuaranteeForm guaranteeForm;
        private void guaranteeMenuItem_Click(object sender, EventArgs e)
        {
            if (guaranteeForm == null || guaranteeForm.IsDisposed)
            {
                guaranteeForm = new GuaranteeForm();
                guaranteeForm.MdiParent = this;
            }

            guaranteeForm.BringToFront();
            guaranteeForm.Show();

            allForm.Add(guaranteeForm);
        }

        private CustomResponsibilityForm customResponsibilityForm;
        private void customResponsibilityMenuItem_Click(object sender, EventArgs e)
        {
            if (customResponsibilityForm == null || customResponsibilityForm.IsDisposed)
            {
                customResponsibilityForm = new CustomResponsibilityForm();
                customResponsibilityForm.MdiParent = this;
            }

            customResponsibilityForm.BringToFront();
            customResponsibilityForm.Show();

            allForm.Add(customResponsibilityForm);
        }

        private StoreHouseForm storeHouseForm;
        private void storeHouseMenuItem_Click(object sender, EventArgs e)
        {
            if (storeHouseForm == null || storeHouseForm.IsDisposed)
            {
                storeHouseForm = new StoreHouseForm();
                storeHouseForm.MdiParent = this;
            }

            storeHouseForm.BringToFront();
            storeHouseForm.Show();

            allForm.Add(storeHouseForm);
        }

        private void 收货单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (roForm == null || roForm.IsDisposed)
            {
                roForm = new ReceiveOrderForm();
                roForm.MdiParent = this;
            }

            roForm.WindowState = FormWindowState.Maximized;
            roForm.BringToFront();
            roForm.Show();

            allForm.Add(roForm);
        }

        private DeliveredTableForm dtform;
        private void 收货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtform == null || dtform.IsDisposed)
            {
                dtform = new DeliveredTableForm();
                dtform.MdiParent = this;
            }
            dtform.WindowState = FormWindowState.Maximized;
            dtform.BringToFront();
            dtform.Show();

            allForm.Add(dtform);
        }

        private ReturnStoreForm rsForm;
        private void returnStoreMenuItem_Click(object sender, EventArgs e)
        {
            if (rsForm == null || rsForm.IsDisposed)
            {
                rsForm = new ReturnStoreForm();
                rsForm.MdiParent = this;
            }

            rsForm.WindowState = FormWindowState.Maximized;
            rsForm.BringToFront();
            rsForm.Show();

            allForm.Add(rsForm);
        }
        private ReturnStoreStatusForm rssf;
        private void 还货状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rssf == null || rssf.IsDisposed)
            {
                rssf = new ReturnStoreStatusForm();
                rssf.MdiParent = this;
            }

            //rssf.WindowState = FormWindowState.Maximized;
            rssf.BringToFront();
            rssf.Show();

            allForm.Add(rssf);
        }

        private ReturnStoreCustomRespForm rscrf;
        private void 还货客责类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rscrf == null || rscrf.IsDisposed)
            {
                rscrf = new ReturnStoreCustomRespForm();
                rscrf.MdiParent = this;
            }

            //rssf.WindowState = FormWindowState.Maximized;
            rscrf.BringToFront();
            rscrf.Show();

            allForm.Add(rscrf);
        }
        private User.UserSelfForm usf;
        private void 个人信息查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usf == null || usf.IsDisposed)
            {
                usf = new User.UserSelfForm();
                usf.MdiParent = this;
            }

            //rssf.WindowState = FormWindowState.Maximized;
            usf.BringToFront();
            usf.Show();

            allForm.Add(usf);
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in allForm)
            {
                if (form != null && form.IsDisposed == false)
                {
                    form.Close();
                }
            }

            this.LogoutMenuItem.Enabled = false;
        }
    }
}
