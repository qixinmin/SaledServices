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

       
        private void 报表导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private RepairOperationForm rof;
        private void 维修界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rof == null || rof.IsDisposed)
            {
                rof = new RepairOperationForm();
                rof.MdiParent = this;
            }

            rof.WindowState = FormWindowState.Maximized;
            rof.BringToFront();
            rof.Show();

            allForm.Add(rof);
        }

        private additionForm.RepairFaultTypeForm rftf;
        private void 维修故障类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rftf == null || rftf.IsDisposed)
            {
                rftf = new additionForm.RepairFaultTypeForm();
                rftf.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            rftf.BringToFront();
            rftf.Show();

            allForm.Add(rftf);
        }

       

        private Test_Outlook.Test1Form test1form;
        private void 测试1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (test1form == null || test1form.IsDisposed)
            {
                test1form = new Test_Outlook.Test1Form();
                test1form.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            test1form.BringToFront();
            test1form.Show();

            allForm.Add(test1form);
        }

        private Test_Outlook.Test2Form test2form;
        private void 测试2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (test2form == null || test2form.IsDisposed)
            {
                test2form = new Test_Outlook.Test2Form();
                test2form.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            test2form.BringToFront();
            test2form.Show();

            allForm.Add(test2form);
        }

        private Test_Outlook.TestAllForm testAllform;
        private void 测试12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (testAllform == null || testAllform.IsDisposed)
            {
                testAllform = new Test_Outlook.TestAllForm();
                testAllform.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            testAllform.BringToFront();
            testAllform.Show();

            allForm.Add(testAllform);
        }

        private Test_Outlook.OutLookForm outlookform;
        private void 外观检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outlookform == null || outlookform.IsDisposed)
            {
                outlookform = new Test_Outlook.OutLookForm();
                outlookform.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            outlookform.BringToFront();
            outlookform.Show();

            allForm.Add(outlookform);
        }

        private void 厂商信息ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 收货单ToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private ExportExcelForm eef;
        private void 报表1ToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            if (eef == null || eef.IsDisposed)
            {
                eef = new ExportExcelForm();
                eef.MdiParent = this;
            }

            //eef.WindowState = FormWindowState.Maximized;
            eef.BringToFront();
            eef.Show();

            allForm.Add(eef);
        }

        private BGAInfoInputForm bgaIf;
        private void bGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaIf == null || bgaIf.IsDisposed)
            {
                bgaIf = new BGAInfoInputForm();
                bgaIf.MdiParent = this;
            }

            bgaIf.WindowState = FormWindowState.Maximized;
            bgaIf.BringToFront();
            bgaIf.Show();

            allForm.Add(bgaIf);
        }

        private LCFC_MBBOMForm lcfcf;
        private void lCFCMBBOM查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lcfcf == null || lcfcf.IsDisposed)
            {
                lcfcf = new LCFC_MBBOMForm();
                lcfcf.MdiParent = this;
            }

            lcfcf.WindowState = FormWindowState.Maximized;
            lcfcf.BringToFront();
            lcfcf.Show();

            allForm.Add(lcfcf);
        }
        private COMPAL_MBBOMForm compalf;
        private void cOMPALMBBOM查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (compalf == null || compalf.IsDisposed)
            {
                compalf = new COMPAL_MBBOMForm();
                compalf.MdiParent = this;
            }

            compalf.WindowState = FormWindowState.Maximized;
            compalf.BringToFront();
            compalf.Show();

            allForm.Add(compalf);
        }

        private LCFC71BOMForm lcfc71bomf;
        private void lCFC71BOM表查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lcfc71bomf == null || lcfc71bomf.IsDisposed)
            {
                lcfc71bomf = new LCFC71BOMForm();
                lcfc71bomf.MdiParent = this;
            }

            lcfc71bomf.WindowState = FormWindowState.Maximized;
            lcfc71bomf.BringToFront();
            lcfc71bomf.Show();

            allForm.Add(lcfc71bomf);
        }

        private DPKForm dpkf;
        private void dPKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dpkf == null || dpkf.IsDisposed)
            {
                dpkf = new DPKForm();
                dpkf.MdiParent = this;
            }

            dpkf.WindowState = FormWindowState.Maximized;
            dpkf.BringToFront();
            dpkf.Show();

            allForm.Add(dpkf);
        }

        private RepairFaultTypeForm repairFaultTypef;
        private void 故障代码表查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (repairFaultTypef == null || repairFaultTypef.IsDisposed)
            {
                repairFaultTypef = new RepairFaultTypeForm();
                repairFaultTypef.MdiParent = this;
            }

            repairFaultTypef.WindowState = FormWindowState.Maximized;
            repairFaultTypef.BringToFront();
            repairFaultTypef.Show();

            allForm.Add(repairFaultTypef);
        }

        private BGARepairOperationForm vgaRof;
        private void bGA维修ToolStripMenuItem_Click(object sender, EventArgs e)
        {             
            if (vgaRof == null || vgaRof.IsDisposed)
            {
                vgaRof = new BGARepairOperationForm();
                vgaRof.MdiParent = this;
            }

            vgaRof.WindowState = FormWindowState.Maximized;
            vgaRof.BringToFront();
            vgaRof.Show();

            allForm.Add(vgaRof);        
        }
    }
}
