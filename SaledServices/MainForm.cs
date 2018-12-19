using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaledServices.additionForm;
using System.Diagnostics;
using System.IO;
using SaledServices.Export;
using SaledServices.Test_Outlook;
using SaledServices.queryform;
using SaledServices.Store;

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

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;//执行间隔时间,单位为毫秒  
            timer.Start();
            //timer.Enabled判断timer是否在运行
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);

            Version ApplicationVersion = new Version(Application.ProductVersion);
            this.Text += ApplicationVersion.ToString();
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 得到 hour minute second  如果等于某个值就开始执行某个程序。  
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            // 定制时间； 比如 在10：30 ：00 的时候执行某个函数  
            int iHour = 6;
            int iMinute = 45;
            int iSecond = 00;
            // 设置　 每分钟的开始执行一次  
            //if (intSecond == iSecond)
            //{
            //    Console.WriteLine("每分钟的开始执行一次！");
            //}
            //// 设置　每个小时的３０分钟开始执行  
            //if (intMinute == iMinute && intSecond == iSecond)
            //{
            //    Console.WriteLine("每个小时的３０分钟开始执行一次！");
            //}            

            if (intHour == 11 && intMinute == 40 && intSecond == 00)
            {
                // Console.WriteLine("在每天１０点３０分开始执行！");
                if (Utils.GetAddressIP() == "192.168.5.222")
                {
                    new DatabaseForm(false).button1_Click(null, null);
                }
            }

            // 设置　每天的17:20开始执行程序  
            if (intHour == 17 && intMinute == 20 && intSecond == 00)
            {
               // Console.WriteLine("在每天１０点３０分开始执行！");
                if (Utils.GetAddressIP() == "192.168.5.222")
                {
                    new DatabaseForm(false).button1_Click(null, null);
                }
            }        
        }

        public void clearAllMenu()
        {
            this.AllMenuStrip.Items.Clear();
        }

        public void appendMenu(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Bga_Repair:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.bGA维修ToolStripMenuItem
                    });
                    break;
                case MenuType.Repair:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.维修ToolStripMenuItem
                    });
                    break;
                case MenuType.Recieve_Return:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.receiveReturnStoreMenuItem,
                   });
                    break;
                case MenuType.TestALL:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.测试ToolStripMenuItem,
                   });
                    break;
                case MenuType.Test1:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.测试1ToolStripMenuItem1,
                   });
                    break;
                case MenuType.Test2:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.测试2ToolStripMenuItem1,
                   });
                    break;
                case MenuType.Running:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.runningToolStripMenuItem,
                   });
                    break;
                case MenuType.Outlook:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.外观ToolStripMenuItem,
                   });
                    break;
                case MenuType.Obe:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.oBEToolStripMenuItem,
                   });
                    break;
                case MenuType.Store:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.库存管理ToolStripMenuItem,
                     this.报表ToolStripMenuItem,       
                   });
                    break;
                case MenuType.Self:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.UserManageMenuItem,
                   });
                    break;
                case MenuType.Other:
                    this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.FunctionMenuItem,           
                    this.additionMenuItem,
                    this.报表ToolStripMenuItem,                   
                    });

                    break;
            }

            this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.查询ToolStripMenuItem
                    });

            this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                   
                    this.bGA维修ToolStripMenuItem
                    });

            this.LogoutMenuItem.Enabled = true;
        }
       
        private void LoginMenuItem_Click(object sender, EventArgs e)
        {           
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
            LoginForm.currentUser = "";

            this.LogoutMenuItem.Enabled = false;
            MainForm_Load(null, null);
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

        private RMAExportExcel eef;
        private void 报表1ToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            if (eef == null || eef.IsDisposed)
            {
                eef = new RMAExportExcel();
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

        
        private void bGA维修ToolStripMenuItem_Click(object sender, EventArgs e)
        {             
            
        }

        private StockInSheetForm sisf;
        private void 材料入库单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sisf == null || sisf.IsDisposed)
            {
                sisf = new StockInSheetForm();
                sisf.MdiParent = this;
            }

            sisf.WindowState = FormWindowState.Maximized;
            sisf.BringToFront();
            sisf.Show();

            allForm.Add(sisf);  
        }


        private FRU_SMT_InSheetForm frusmtinform;
        private void fRUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frusmtinform == null || frusmtinform.IsDisposed)
            {
                frusmtinform = new FRU_SMT_InSheetForm();
                frusmtinform.MdiParent = this;
            }

            frusmtinform.WindowState = FormWindowState.Maximized;
            frusmtinform.BringToFront();
            frusmtinform.Show();

            allForm.Add(frusmtinform);  
        }

        private FRU_OutSheetForm fruoutform;
        private void fRUSMT入库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fruoutform == null || fruoutform.IsDisposed)
            {
                fruoutform = new FRU_OutSheetForm();
                fruoutform.MdiParent = this;
            }

            fruoutform.WindowState = FormWindowState.Maximized;
            fruoutform.BringToFront();
            fruoutform.Show();

            allForm.Add(fruoutform);  
        }

        private Store.CheckRequestForm checkrequestform;
        private void 出库请求查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkrequestform == null || checkrequestform.IsDisposed)
            {
                checkrequestform = new Store.CheckRequestForm();
                checkrequestform.MdiParent = this;
            }

            checkrequestform.WindowState = FormWindowState.Maximized;
            checkrequestform.BringToFront();
            checkrequestform.Show();

            allForm.Add(checkrequestform);  
        }


        private void 库房领料申请ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Store.RequestFRUSMTStoreForm rtsf = new Store.RequestFRUSMTStoreForm();
           // rtsf.setParameters(this.track_serial_noTextBox.Text, this.material_mpntextBox.Text, this.material_71pntextBox.Text);
            rtsf.MdiParent = this;
            rtsf.Show();
        }

        private void 还货请求查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Store.ProcessReturnStoreForm prsf = new Store.ProcessReturnStoreForm();
            prsf.MdiParent = this;
            prsf.Show();
        }

        private void bGA领料申请ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private BGARepairOperationForm vgaRof;
        private void bGA维修界面ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private StoreHouseInnerForm shif;
        private void 库房储位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shif == null || shif.IsDisposed)
            {
                shif = new StoreHouseInnerForm();
                shif.MdiParent = this;
            }

            //shif.WindowState = FormWindowState.Maximized;
            shif.BringToFront();
            shif.Show();

            allForm.Add(shif);     
        }

        private BGA_InSheetForm bgainform;
        private void bGA入库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgainform == null || bgainform.IsDisposed)
            {
                bgainform = new BGA_InSheetForm();
                bgainform.MdiParent = this;
            }

            bgainform.WindowState = FormWindowState.Maximized;
            bgainform.BringToFront();
            bgainform.Show();

            allForm.Add(bgainform);    
        }

        private BGA_OutSheetForm bgaoutform;
        private void bGA出库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaoutform == null || bgaoutform.IsDisposed)
            {
                bgaoutform = new BGA_OutSheetForm();
                bgaoutform.MdiParent = this;
            }

            bgaoutform.WindowState = FormWindowState.Maximized;
            bgaoutform.BringToFront();
            bgaoutform.Show();

            allForm.Add(bgaoutform);    
        }
        private MB_InSheetForm mbinform;
        private void mBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbinform == null || mbinform.IsDisposed)
            {
                mbinform = new MB_InSheetForm();
                mbinform.MdiParent = this;
            }

            mbinform.WindowState = FormWindowState.Maximized;
            mbinform.BringToFront();
            mbinform.Show();

            allForm.Add(mbinform);  
        }

        private MB_OutSheetForm mboutform;
        private void mBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mboutform == null || mboutform.IsDisposed)
            {
                mboutform = new MB_OutSheetForm();
                mboutform.MdiParent = this;
            }

            mboutform.WindowState = FormWindowState.Maximized;
            mboutform.BringToFront();
            mboutform.Show();

            allForm.Add(mboutform);   
        }

        private UserDetailForm mUserDetailForm;
        private void 员工管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mUserDetailForm == null || mUserDetailForm.IsDisposed)
            {
                mUserDetailForm = new UserDetailForm();
                mUserDetailForm.MdiParent = this;
            }

            mUserDetailForm.BringToFront();
            mUserDetailForm.Show();

            allForm.Add(mUserDetailForm);   
        }

        private Test_Outlook.Test1Form test1form;
        private void 测试1ToolStripMenuItem1_Click(object sender, EventArgs e)
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
        private void 测试2ToolStripMenuItem1_Click(object sender, EventArgs e)
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
        
        private Test_Outlook.RunningForm runningform;
        private void runningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (runningform == null || runningform.IsDisposed)
            {
                runningform = new Test_Outlook.RunningForm();
                runningform.MdiParent = this;
            }

            //runningform.WindowState = FormWindowState.Maximized;
            runningform.BringToFront();
            runningform.Show();

            allForm.Add(runningform);
        }

        private Test_Outlook.ObeForm obeform;
        private void oBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obeform == null || obeform.IsDisposed)
            {
                obeform = new Test_Outlook.ObeForm();
                obeform.MdiParent = this;
            }

            //obeform.WindowState = FormWindowState.Maximized;
            obeform.BringToFront();
            obeform.Show();

            allForm.Add(obeform);
        }

      

        private void MainForm_Load(object sender, EventArgs e)
        {
            clearAllMenu();

            if (mLoginForm == null || mLoginForm.IsDisposed)
            {
                mLoginForm = new LoginForm(this);
                mLoginForm.MdiParent = this;
            }

            mLoginForm.BringToFront();
            mLoginForm.Show();

            allForm.Add(mLoginForm);
        }

        private MaterialNameForm materialNameForm;
        private void 材料名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (materialNameForm == null || materialNameForm.IsDisposed)
            {
                materialNameForm = new MaterialNameForm();
                materialNameForm.MdiParent = this;
            }

            materialNameForm.WindowState = FormWindowState.Maximized;
            materialNameForm.BringToFront();
            materialNameForm.Show();

            allForm.Add(materialNameForm);
        }
        
        private TimerCheckForm timerCheckForm;
        private void 定时任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerCheckForm == null || timerCheckForm.IsDisposed)
            {
                timerCheckForm = new TimerCheckForm();
                timerCheckForm.MdiParent = this;
            }

           // timerCheckForm.WindowState = FormWindowState.Maximized;
            timerCheckForm.BringToFront();
            timerCheckForm.Show();

            allForm.Add(timerCheckForm);
        }

        private FilesUpdateForm filesUpdateForm;
        private void 文件数据库操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesUpdateForm == null || filesUpdateForm.IsDisposed)
            {
                filesUpdateForm = new FilesUpdateForm();
                filesUpdateForm.MdiParent = this;
            }

            // filesUpdateForm.WindowState = FormWindowState.Maximized;
            filesUpdateForm.BringToFront();
            filesUpdateForm.Show();

            allForm.Add(filesUpdateForm);
        }

        private InputCIDShanghaiForm cidInputshanghaiForm;
        private void cID操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cidInputshanghaiForm == null || cidInputshanghaiForm.IsDisposed)
            {
                cidInputshanghaiForm = new InputCIDShanghaiForm();
                cidInputshanghaiForm.MdiParent = this;
            }

            cidInputshanghaiForm.WindowState = FormWindowState.Maximized;
            cidInputshanghaiForm.BringToFront();
            cidInputshanghaiForm.Show();

            allForm.Add(cidInputshanghaiForm);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cID操作ToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cID操作ToolStripMenuItem_Click(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PrintUtils.disposePrinter();
        }

        private ReceiveOrderExport roexport;
        private void 收货信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (roexport == null || roexport.IsDisposed)
            {
                roexport = new ReceiveOrderExport();
                roexport.MdiParent = this;
            }

           // roexport.WindowState = FormWindowState.Maximized;
            roexport.BringToFront();
            roexport.Show();

            allForm.Add(roexport);
        }
        private DatabaseForm databaseForm;
        private void 数据库备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (databaseForm == null || databaseForm.IsDisposed)
            {
                databaseForm = new DatabaseForm();
                databaseForm.MdiParent = this;
            }

          //  databaseForm.WindowState = FormWindowState.Maximized;
            databaseForm.BringToFront();
            databaseForm.Show();

            allForm.Add(databaseForm);
        }

        private StationCheckForm stationCheckForm;
        private void 站别查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stationCheckForm == null || stationCheckForm.IsDisposed)
            {
                stationCheckForm = new StationCheckForm();
                stationCheckForm.MdiParent = this;
            }

            //  databaseForm.WindowState = FormWindowState.Maximized;
            stationCheckForm.BringToFront();
            stationCheckForm.Show();

            allForm.Add(stationCheckForm);
        }

        private StoreHouseInnerNGForm storeHouseInnerNGForm;
        private void 不良品库房储位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storeHouseInnerNGForm == null || storeHouseInnerNGForm.IsDisposed)
            {
                storeHouseInnerNGForm = new StoreHouseInnerNGForm();
                storeHouseInnerNGForm.MdiParent = this;
            }

            //  databaseForm.WindowState = FormWindowState.Maximized;
            storeHouseInnerNGForm.BringToFront();
            storeHouseInnerNGForm.Show();

            allForm.Add(storeHouseInnerNGForm);
        }

        private FaultBatchMBInStoreForm faultBatchMBInStoreForm;
        private void mB不良品批量入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultBatchMBInStoreForm == null || faultBatchMBInStoreForm.IsDisposed)
            {
                faultBatchMBInStoreForm = new FaultBatchMBInStoreForm();
                faultBatchMBInStoreForm.MdiParent = this;
            }

            //  databaseForm.WindowState = FormWindowState.Maximized;
            faultBatchMBInStoreForm.BringToFront();
            faultBatchMBInStoreForm.Show();

            allForm.Add(faultBatchMBInStoreForm);
        }

        private FaultBatchMBOutStoreForm faultBatchMBOutStoreForm;
        private void mB不良品批量出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultBatchMBOutStoreForm == null || faultBatchMBOutStoreForm.IsDisposed)
            {
                faultBatchMBOutStoreForm = new FaultBatchMBOutStoreForm();
                faultBatchMBOutStoreForm.MdiParent = this;
            }

            //databaseForm.WindowState = FormWindowState.Maximized;
            faultBatchMBOutStoreForm.BringToFront();
            faultBatchMBOutStoreForm.Show();

            allForm.Add(faultBatchMBOutStoreForm);
        }

        private FaultMBConfirmForm faultMBConfirmForm;
        private void mB报废判定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultMBConfirmForm == null || faultMBConfirmForm.IsDisposed)
            {
                faultMBConfirmForm = new FaultMBConfirmForm();
                faultMBConfirmForm.MdiParent = this;
            }

            faultMBConfirmForm.WindowState = FormWindowState.Maximized;
            faultMBConfirmForm.BringToFront();
            faultMBConfirmForm.Show();

            allForm.Add(faultMBConfirmForm);
        }

        private FaultMBRecordForm faultMBRecordForm;
        private void mB不良品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultMBRecordForm == null || faultMBRecordForm.IsDisposed)
            {
                faultMBRecordForm = new FaultMBRecordForm();
                faultMBRecordForm.MdiParent = this;
            }

            faultMBRecordForm.WindowState = FormWindowState.Maximized;
            faultMBRecordForm.BringToFront();
            faultMBRecordForm.Show();

            allForm.Add(faultMBRecordForm);
        }

        private MultiReturnQueryForm multiReturnQueryForm;
        private void dOA查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (multiReturnQueryForm == null || multiReturnQueryForm.IsDisposed)
            {
                multiReturnQueryForm = new MultiReturnQueryForm();
                multiReturnQueryForm.MdiParent = this;
            }

            multiReturnQueryForm.WindowState = FormWindowState.Maximized;
            multiReturnQueryForm.BringToFront();
            multiReturnQueryForm.Show();

            allForm.Add(multiReturnQueryForm);
        }

        private ReturnOrderExport reorexport;
        private void 还货信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reorexport == null || reorexport.IsDisposed)
            {
                reorexport = new ReturnOrderExport();
                reorexport.MdiParent = this;
            }

            // reorexport.WindowState = FormWindowState.Maximized;
            reorexport.BringToFront();
            reorexport.Show();

            allForm.Add(reorexport);
        }

        private BgaInExport bgaInExport;
        private void bGA收货信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaInExport == null || bgaInExport.IsDisposed)
            {
                bgaInExport = new BgaInExport();
                bgaInExport.MdiParent = this;
            }

            // bgaInExport.WindowState = FormWindowState.Maximized;
            bgaInExport.BringToFront();
            bgaInExport.Show();

            allForm.Add(bgaInExport);
        }

        private BgaOutExport bgaOutExport;
        private void bGA出库信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaOutExport == null || bgaOutExport.IsDisposed)
            {
                bgaOutExport = new BgaOutExport();
                bgaOutExport.MdiParent = this;
            }

            // bgaOutExport.WindowState = FormWindowState.Maximized;
            bgaOutExport.BringToFront();
            bgaOutExport.Show();

            allForm.Add(bgaOutExport);
        }

        private MBInExport mbInExport;
        private void mB入库信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbInExport == null || mbInExport.IsDisposed)
            {
                mbInExport = new MBInExport();
                mbInExport.MdiParent = this;
            }

            // mbInExport.WindowState = FormWindowState.Maximized;
            mbInExport.BringToFront();
            mbInExport.Show();

            allForm.Add(mbInExport);
        }

        private MBOutExport mbOutExport;
        private void mB出库信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbOutExport == null || mbOutExport.IsDisposed)
            {
                mbOutExport = new MBOutExport();
                mbOutExport.MdiParent = this;
            }

            // mbOutExport.WindowState = FormWindowState.Maximized;
            mbOutExport.BringToFront();
            mbOutExport.Show();

            allForm.Add(mbOutExport);
        }

        private StoreHouseStatusExport storeHouseStatusExport;
        private void 库房信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storeHouseStatusExport == null || storeHouseStatusExport.IsDisposed)
            {
                storeHouseStatusExport = new StoreHouseStatusExport();
                storeHouseStatusExport.MdiParent = this;
            }

            // storeHouseStatusExport.WindowState = FormWindowState.Maximized;
            storeHouseStatusExport.BringToFront();
            storeHouseStatusExport.Show();

            allForm.Add(storeHouseStatusExport);
        }

        private StoreHouseStatisticsExport storeHouseStatisticsExport;
        private void 库房平衡表信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storeHouseStatisticsExport == null || storeHouseStatisticsExport.IsDisposed)
            {
                storeHouseStatisticsExport = new StoreHouseStatisticsExport();
                storeHouseStatisticsExport.MdiParent = this;
            }

            // storeHouseStatisticsExport.WindowState = FormWindowState.Maximized;
            storeHouseStatisticsExport.BringToFront();
            storeHouseStatisticsExport.Show();

            allForm.Add(storeHouseStatisticsExport);
        }

        private MBTransferToFaultForm mbTransferToFaultForm;
        private void mB良品批量转不良品库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbTransferToFaultForm == null || mbTransferToFaultForm.IsDisposed)
            {
                mbTransferToFaultForm = new MBTransferToFaultForm();
                mbTransferToFaultForm.MdiParent = this;
            }

            // mbTransferToFaultForm.WindowState = FormWindowState.Maximized;
            mbTransferToFaultForm.BringToFront();
            mbTransferToFaultForm.Show();

            allForm.Add(mbTransferToFaultForm);
        }

        private DPKExport dPKExport;
        private void dPK报表导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dPKExport == null || dPKExport.IsDisposed)
            {
                dPKExport = new DPKExport();
                dPKExport.MdiParent = this;
            }

            // dPKExport.WindowState = FormWindowState.Maximized;
            dPKExport.BringToFront();
            dPKExport.Show();

            allForm.Add(dPKExport);
        }

        private CIDExport cidExport;
        private void cID报表导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cidExport == null || cidExport.IsDisposed)
            {
                cidExport = new CIDExport();
                cidExport.MdiParent = this;
            }

            // cidExport.WindowState = FormWindowState.Maximized;
            cidExport.BringToFront();
            cidExport.Show();

            allForm.Add(cidExport);
        }

        private RepairRecordExport repairRecordExport;
        private void 维修报表导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (repairRecordExport == null || repairRecordExport.IsDisposed)
            {
                repairRecordExport = new RepairRecordExport();
                repairRecordExport.MdiParent = this;
            }

            // repairRecordExport.WindowState = FormWindowState.Maximized;
            repairRecordExport.BringToFront();
            repairRecordExport.Show();

            allForm.Add(repairRecordExport);
        }

        private CheckInfoForm checkInfoForm;
        private void 信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkInfoForm == null || checkInfoForm.IsDisposed)
            {
                checkInfoForm = new CheckInfoForm();
                checkInfoForm.MdiParent = this;
            }

            //checkInfoForm.WindowState = FormWindowState.Maximized;
            checkInfoForm.BringToFront();
            checkInfoForm.Show();

            allForm.Add(checkInfoForm);
        }

        private FruReceiveOrderForm fruroForm = null;
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (fruroForm == null || fruroForm.IsDisposed)
            {
                fruroForm = new FruReceiveOrderForm();
                fruroForm.MdiParent = this;
            }

            fruroForm.WindowState = FormWindowState.Maximized;
            fruroForm.BringToFront();
            fruroForm.Show();

            allForm.Add(fruroForm);
        }

        private FruDeliveredTableForm frudtform;
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (frudtform == null || frudtform.IsDisposed)
            {
                frudtform = new FruDeliveredTableForm();
                frudtform.MdiParent = this;
            }
            frudtform.WindowState = FormWindowState.Maximized;
            frudtform.BringToFront();
            frudtform.Show();

            allForm.Add(frudtform);
        }

        private FruReturnStoreForm frursForm;        
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (frursForm == null || frursForm.IsDisposed)
            {
                frursForm = new FruReturnStoreForm();
                frursForm.MdiParent = this;
            }

            frursForm.WindowState = FormWindowState.Maximized;
            frursForm.BringToFront();
            frursForm.Show();

            allForm.Add(frursForm);
        }

        private RenBaoExportExcel renBaoExportExcel;      
        private void 仁宝大数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (renBaoExportExcel == null || renBaoExportExcel.IsDisposed)
            {
                renBaoExportExcel = new RenBaoExportExcel();
                renBaoExportExcel.MdiParent = this;
            }

            renBaoExportExcel.WindowState = FormWindowState.Maximized;
            renBaoExportExcel.BringToFront();
            renBaoExportExcel.Show();

            allForm.Add(renBaoExportExcel);
        }
        private HefeiExportExcel hefeiExportExcel;      
        private void 合肥报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hefeiExportExcel == null || hefeiExportExcel.IsDisposed)
            {
                hefeiExportExcel = new HefeiExportExcel();
                hefeiExportExcel.MdiParent = this;
            }

            hefeiExportExcel.WindowState = FormWindowState.Maximized;
            hefeiExportExcel.BringToFront();
            hefeiExportExcel.Show();

            allForm.Add(hefeiExportExcel);
        }

        private AllBossExport allBossExport; 
        private void 总报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (allBossExport == null || allBossExport.IsDisposed)
            {
                allBossExport = new AllBossExport();
                allBossExport.MdiParent = this;
            }

            allBossExport.WindowState = FormWindowState.Maximized;
            allBossExport.BringToFront();
            allBossExport.Show();

            allForm.Add(allBossExport);
        }
        private FruExport fruExport; 
        private void fRU收还货信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fruExport == null || fruExport.IsDisposed)
            {
                fruExport = new FruExport();
                fruExport.MdiParent = this;
            }

            fruExport.WindowState = FormWindowState.Maximized;
            fruExport.BringToFront();
            fruExport.Show();

            allForm.Add(fruExport);
        }
        private ReturnQueryByInfo returnQueryByInfo; 
        private void 查询板子站别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (returnQueryByInfo == null || returnQueryByInfo.IsDisposed)
            {
                returnQueryByInfo = new ReturnQueryByInfo();
                returnQueryByInfo.MdiParent = this;
            }

            returnQueryByInfo.WindowState = FormWindowState.Maximized;
            returnQueryByInfo.BringToFront();
            returnQueryByInfo.Show();

            allForm.Add(returnQueryByInfo);
        }

        private void mB报废判定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mB报废判定ToolStripMenuItem_Click(null, null);
        }

        private DeliveredTableTransferForm deliveredTableTransferForm; 
        private void mB转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deliveredTableTransferForm == null || deliveredTableTransferForm.IsDisposed)
            {
                deliveredTableTransferForm = new DeliveredTableTransferForm();
                deliveredTableTransferForm.MdiParent = this;
            }

            deliveredTableTransferForm.WindowState = FormWindowState.Maximized;
            deliveredTableTransferForm.BringToFront();
            deliveredTableTransferForm.Show();

            allForm.Add(deliveredTableTransferForm);
        }

        private AdjustStoreHouseForm adjustStoreHouseForm; 
        private void 库房料转移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adjustStoreHouseForm == null || adjustStoreHouseForm.IsDisposed)
            {
                adjustStoreHouseForm = new AdjustStoreHouseForm();
                adjustStoreHouseForm.MdiParent = this;
            }

            adjustStoreHouseForm.WindowState = FormWindowState.Maximized;
            adjustStoreHouseForm.BringToFront();
            adjustStoreHouseForm.Show();

            allForm.Add(adjustStoreHouseForm);
        }

        private FaultMBRecordExport faultMBRecordExport; 
        private void mB报废信息导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (faultMBRecordExport == null || faultMBRecordExport.IsDisposed)
            {
                faultMBRecordExport = new FaultMBRecordExport();
                faultMBRecordExport.MdiParent = this;
            }

            // faultMBRecordExport.WindowState = FormWindowState.Maximized;
            faultMBRecordExport.BringToFront();
            faultMBRecordExport.Show();

            allForm.Add(faultMBRecordExport);
        }

        private BGAWaitMaterialForm bgaWaitMaterialForm; 
        private void bGA待料输入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaWaitMaterialForm == null || bgaWaitMaterialForm.IsDisposed)
            {
                bgaWaitMaterialForm = new BGAWaitMaterialForm();
                bgaWaitMaterialForm.MdiParent = this;
            }

            bgaWaitMaterialForm.WindowState = FormWindowState.Maximized;
            bgaWaitMaterialForm.BringToFront();
            bgaWaitMaterialForm.Show();

            allForm.Add(bgaWaitMaterialForm);
        }

        private BgaWaitMaterialExport bgaWaitMaterialExport; 
        private void bGA待料报表导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaWaitMaterialExport == null || bgaWaitMaterialExport.IsDisposed)
            {
                bgaWaitMaterialExport = new BgaWaitMaterialExport();
                bgaWaitMaterialExport.MdiParent = this;
            }

            bgaWaitMaterialExport.WindowState = FormWindowState.Maximized;
            bgaWaitMaterialExport.BringToFront();
            bgaWaitMaterialExport.Show();

            allForm.Add(bgaWaitMaterialExport);
        }

        private BgaUsedExport bgaUsedExport; 
        private void bGA更换机滤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bgaUsedExport == null || bgaUsedExport.IsDisposed)
            {
                bgaUsedExport = new BgaUsedExport();
                bgaUsedExport.MdiParent = this;
            }

            bgaUsedExport.WindowState = FormWindowState.Maximized;
            bgaUsedExport.BringToFront();
            bgaUsedExport.Show();

            allForm.Add(bgaUsedExport);
        }

        private MBBgaMaterialAllExport mbBgaMaterialAllExport; 
        private void mBBga材料一览表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mbBgaMaterialAllExport == null || mbBgaMaterialAllExport.IsDisposed)
            {
                mbBgaMaterialAllExport = new MBBgaMaterialAllExport();
                mbBgaMaterialAllExport.MdiParent = this;
            }

            mbBgaMaterialAllExport.WindowState = FormWindowState.Maximized;
            mbBgaMaterialAllExport.BringToFront();
            mbBgaMaterialAllExport.Show();

            allForm.Add(mbBgaMaterialAllExport);
        }

        private QueryAllInfoForm queryAllInfoForm; 
        private void 板子所有记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (queryAllInfoForm == null || queryAllInfoForm.IsDisposed)
            {
                queryAllInfoForm = new QueryAllInfoForm();
                queryAllInfoForm.MdiParent = this;
            }

            queryAllInfoForm.WindowState = FormWindowState.Maximized;
            queryAllInfoForm.BringToFront();
            queryAllInfoForm.Show();

            allForm.Add(queryAllInfoForm);
        }

        private WholeMachineExport wholeMachineExport; 
        private void 整机出货量的报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wholeMachineExport == null || wholeMachineExport.IsDisposed)
            {
                wholeMachineExport = new WholeMachineExport();
                wholeMachineExport.MdiParent = this;
            }

            wholeMachineExport.WindowState = FormWindowState.Maximized;
            wholeMachineExport.BringToFront();
            wholeMachineExport.Show();

            allForm.Add(wholeMachineExport);
        }

        private TransferOrSoldForm transferOrSoldForm; 
        private void 报废转卖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transferOrSoldForm == null || transferOrSoldForm.IsDisposed)
            {
                transferOrSoldForm = new TransferOrSoldForm();
                transferOrSoldForm.MdiParent = this;
            }

            transferOrSoldForm.WindowState = FormWindowState.Maximized;
            transferOrSoldForm.BringToFront();
            transferOrSoldForm.Show();

            allForm.Add(transferOrSoldForm);
        }

        private ReturnGT2RepairRecordExport returnGT2RepairRecordExport; 
        private void 二返报表格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (returnGT2RepairRecordExport == null || returnGT2RepairRecordExport.IsDisposed)
            {
                returnGT2RepairRecordExport = new ReturnGT2RepairRecordExport();
                returnGT2RepairRecordExport.MdiParent = this;
            }

            returnGT2RepairRecordExport.WindowState = FormWindowState.Maximized;
            returnGT2RepairRecordExport.BringToFront();
            returnGT2RepairRecordExport.Show();

            allForm.Add(returnGT2RepairRecordExport);
        }
    }
}
