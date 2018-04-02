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
        
        public MainForm()
        {
            InitializeComponent();
        }

        public void changeMenu(MenuType menuType)
        {
            this.AllMenuStrip.Items.Clear();
            switch (menuType)
            {
                case MenuType.LOGIN_MENU:
                    this.LoginMenuItem.Name = "LoginMenuItem";
                    this.LoginMenuItem.Size = new System.Drawing.Size(152, 22);
                    this.LoginMenuItem.Text = "登录";
                    this.LoginMenuItem.Click += new System.EventHandler(this.LoginMenuItem_Click);
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.LoginMenuItem});      
                    break;
                case MenuType.MAIN_MENU:
                    this.UserManageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.LoginMenuItem,
                        this.LogoutMenuItem});
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.UserManageMenuItem,
                        this.VendorMenuItem,
                        this.FunctionMenuItem});
                    break;
            }            
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
        }
    }
}
