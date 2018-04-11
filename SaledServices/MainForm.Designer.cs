namespace SaledServices
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AllMenuStrip = new System.Windows.Forms.MenuStrip();
            this.UserManageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.个人信息查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FunctionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExcelImportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mBMaterialCompareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收货单ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.receiveReturnStoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收货ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnStoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.维修ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.维修界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vGA维修界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.外观ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.外观检查ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customFaultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guaranteeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customResponsibilityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeHouseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还货状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还货客责类别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.维修故障类别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.厂商信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllMenuStrip
            // 
            this.AllMenuStrip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserManageMenuItem,
            this.FunctionMenuItem,
            this.receiveReturnStoreMenuItem,
            this.维修ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.外观ToolStripMenuItem,
            this.库存管理ToolStripMenuItem,
            this.additionMenuItem,
            this.报表ToolStripMenuItem});
            this.AllMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AllMenuStrip.Name = "AllMenuStrip";
            this.AllMenuStrip.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.AllMenuStrip.Size = new System.Drawing.Size(927, 37);
            this.AllMenuStrip.TabIndex = 4;
            this.AllMenuStrip.Text = "AllMenu";
            // 
            // UserManageMenuItem
            // 
            this.UserManageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginMenuItem,
            this.个人信息查看ToolStripMenuItem,
            this.LogoutMenuItem});
            this.UserManageMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.UserManageMenuItem.Name = "UserManageMenuItem";
            this.UserManageMenuItem.Size = new System.Drawing.Size(104, 31);
            this.UserManageMenuItem.Text = "用户管理";
            // 
            // LoginMenuItem
            // 
            this.LoginMenuItem.Name = "LoginMenuItem";
            this.LoginMenuItem.Size = new System.Drawing.Size(204, 32);
            this.LoginMenuItem.Text = "登录";
            this.LoginMenuItem.Click += new System.EventHandler(this.LoginMenuItem_Click);
            // 
            // 个人信息查看ToolStripMenuItem
            // 
            this.个人信息查看ToolStripMenuItem.Name = "个人信息查看ToolStripMenuItem";
            this.个人信息查看ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.个人信息查看ToolStripMenuItem.Text = "个人信息查看";
            this.个人信息查看ToolStripMenuItem.Click += new System.EventHandler(this.个人信息查看ToolStripMenuItem_Click);
            // 
            // LogoutMenuItem
            // 
            this.LogoutMenuItem.Enabled = false;
            this.LogoutMenuItem.Name = "LogoutMenuItem";
            this.LogoutMenuItem.Size = new System.Drawing.Size(204, 32);
            this.LogoutMenuItem.Text = "注销";
            this.LogoutMenuItem.Click += new System.EventHandler(this.LogoutMenuItem_Click);
            // 
            // FunctionMenuItem
            // 
            this.FunctionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelImportMenuItem,
            this.mBMaterialCompareMenuItem,
            this.收货单ToolStripMenuItem1});
            this.FunctionMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.FunctionMenuItem.Name = "FunctionMenuItem";
            this.FunctionMenuItem.Size = new System.Drawing.Size(64, 31);
            this.FunctionMenuItem.Text = "功能";
            // 
            // ExcelImportMenuItem
            // 
            this.ExcelImportMenuItem.Name = "ExcelImportMenuItem";
            this.ExcelImportMenuItem.Size = new System.Drawing.Size(257, 32);
            this.ExcelImportMenuItem.Text = "Excel表格导入";
            this.ExcelImportMenuItem.Click += new System.EventHandler(this.ExcelImportMenuItem_Click);
            // 
            // mBMaterialCompareMenuItem
            // 
            this.mBMaterialCompareMenuItem.Name = "mBMaterialCompareMenuItem";
            this.mBMaterialCompareMenuItem.Size = new System.Drawing.Size(257, 32);
            this.mBMaterialCompareMenuItem.Text = "MB物料对照表查看";
            this.mBMaterialCompareMenuItem.Click += new System.EventHandler(this.mBMaterialCompareMenuItem_Click);
            // 
            // 收货单ToolStripMenuItem1
            // 
            this.收货单ToolStripMenuItem1.Name = "收货单ToolStripMenuItem1";
            this.收货单ToolStripMenuItem1.Size = new System.Drawing.Size(257, 32);
            this.收货单ToolStripMenuItem1.Text = "收货单查看";
            this.收货单ToolStripMenuItem1.Click += new System.EventHandler(this.收货单ToolStripMenuItem1_Click);
            // 
            // receiveReturnStoreMenuItem
            // 
            this.receiveReturnStoreMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.收货ToolStripMenuItem,
            this.returnStoreMenuItem});
            this.receiveReturnStoreMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.receiveReturnStoreMenuItem.Name = "receiveReturnStoreMenuItem";
            this.receiveReturnStoreMenuItem.Size = new System.Drawing.Size(84, 31);
            this.receiveReturnStoreMenuItem.Text = "收还货";
            this.receiveReturnStoreMenuItem.Click += new System.EventHandler(this.receiveOrderMenuItem_Click);
            // 
            // 收货ToolStripMenuItem
            // 
            this.收货ToolStripMenuItem.Name = "收货ToolStripMenuItem";
            this.收货ToolStripMenuItem.Size = new System.Drawing.Size(124, 32);
            this.收货ToolStripMenuItem.Text = "收货";
            this.收货ToolStripMenuItem.Click += new System.EventHandler(this.收货ToolStripMenuItem_Click);
            // 
            // returnStoreMenuItem
            // 
            this.returnStoreMenuItem.Name = "returnStoreMenuItem";
            this.returnStoreMenuItem.Size = new System.Drawing.Size(124, 32);
            this.returnStoreMenuItem.Text = "还货";
            this.returnStoreMenuItem.Click += new System.EventHandler(this.returnStoreMenuItem_Click);
            // 
            // 维修ToolStripMenuItem
            // 
            this.维修ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.维修界面ToolStripMenuItem,
            this.vGA维修界面ToolStripMenuItem});
            this.维修ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.维修ToolStripMenuItem.Name = "维修ToolStripMenuItem";
            this.维修ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.维修ToolStripMenuItem.Text = "维修";
            // 
            // 维修界面ToolStripMenuItem
            // 
            this.维修界面ToolStripMenuItem.Name = "维修界面ToolStripMenuItem";
            this.维修界面ToolStripMenuItem.Size = new System.Drawing.Size(207, 32);
            this.维修界面ToolStripMenuItem.Text = "维修界面";
            this.维修界面ToolStripMenuItem.Click += new System.EventHandler(this.维修界面ToolStripMenuItem_Click);
            // 
            // vGA维修界面ToolStripMenuItem
            // 
            this.vGA维修界面ToolStripMenuItem.Name = "vGA维修界面ToolStripMenuItem";
            this.vGA维修界面ToolStripMenuItem.Size = new System.Drawing.Size(206, 32);
            this.vGA维修界面ToolStripMenuItem.Text = "BGA维修界面";
            this.vGA维修界面ToolStripMenuItem.Click += new System.EventHandler(this.vGA维修界面ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.测试1ToolStripMenuItem,
            this.测试2ToolStripMenuItem,
            this.测试12ToolStripMenuItem});
            this.测试ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.测试ToolStripMenuItem.Text = "测试";
            // 
            // 测试1ToolStripMenuItem
            // 
            this.测试1ToolStripMenuItem.Name = "测试1ToolStripMenuItem";
            this.测试1ToolStripMenuItem.Size = new System.Drawing.Size(157, 32);
            this.测试1ToolStripMenuItem.Text = "测试1";
            this.测试1ToolStripMenuItem.Click += new System.EventHandler(this.测试1ToolStripMenuItem_Click);
            // 
            // 测试2ToolStripMenuItem
            // 
            this.测试2ToolStripMenuItem.Name = "测试2ToolStripMenuItem";
            this.测试2ToolStripMenuItem.Size = new System.Drawing.Size(157, 32);
            this.测试2ToolStripMenuItem.Text = "测试2";
            this.测试2ToolStripMenuItem.Click += new System.EventHandler(this.测试2ToolStripMenuItem_Click);
            // 
            // 测试12ToolStripMenuItem
            // 
            this.测试12ToolStripMenuItem.Name = "测试12ToolStripMenuItem";
            this.测试12ToolStripMenuItem.Size = new System.Drawing.Size(157, 32);
            this.测试12ToolStripMenuItem.Text = "测试1_2";
            this.测试12ToolStripMenuItem.Click += new System.EventHandler(this.测试12ToolStripMenuItem_Click);
            // 
            // 外观ToolStripMenuItem
            // 
            this.外观ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.外观检查ToolStripMenuItem});
            this.外观ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.外观ToolStripMenuItem.Name = "外观ToolStripMenuItem";
            this.外观ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.外观ToolStripMenuItem.Text = "外观";
            // 
            // 外观检查ToolStripMenuItem
            // 
            this.外观检查ToolStripMenuItem.Name = "外观检查ToolStripMenuItem";
            this.外观检查ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.外观检查ToolStripMenuItem.Text = "外观检查";
            this.外观检查ToolStripMenuItem.Click += new System.EventHandler(this.外观检查ToolStripMenuItem_Click);
            // 
            // 库存管理ToolStripMenuItem
            // 
            this.库存管理ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.库存管理ToolStripMenuItem.Name = "库存管理ToolStripMenuItem";
            this.库存管理ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.库存管理ToolStripMenuItem.Text = "库存管理";
            // 
            // additionMenuItem
            // 
            this.additionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceMenuItem,
            this.customFaultMenuItem,
            this.guaranteeMenuItem,
            this.customResponsibilityMenuItem,
            this.storeHouseMenuItem,
            this.还货状态ToolStripMenuItem,
            this.还货客责类别ToolStripMenuItem,
            this.维修故障类别ToolStripMenuItem,
            this.厂商信息ToolStripMenuItem});
            this.additionMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.additionMenuItem.Name = "additionMenuItem";
            this.additionMenuItem.Size = new System.Drawing.Size(104, 31);
            this.additionMenuItem.Text = "附加信息";
            // 
            // sourceMenuItem
            // 
            this.sourceMenuItem.Name = "sourceMenuItem";
            this.sourceMenuItem.Size = new System.Drawing.Size(204, 32);
            this.sourceMenuItem.Text = "收货来源";
            this.sourceMenuItem.Click += new System.EventHandler(this.sourceMenuItem_Click);
            // 
            // customFaultMenuItem
            // 
            this.customFaultMenuItem.Name = "customFaultMenuItem";
            this.customFaultMenuItem.Size = new System.Drawing.Size(204, 32);
            this.customFaultMenuItem.Text = "客户故障";
            this.customFaultMenuItem.Click += new System.EventHandler(this.customFaultMenuItem_Click);
            // 
            // guaranteeMenuItem
            // 
            this.guaranteeMenuItem.Name = "guaranteeMenuItem";
            this.guaranteeMenuItem.Size = new System.Drawing.Size(204, 32);
            this.guaranteeMenuItem.Text = "保内/保外";
            this.guaranteeMenuItem.Click += new System.EventHandler(this.guaranteeMenuItem_Click);
            // 
            // customResponsibilityMenuItem
            // 
            this.customResponsibilityMenuItem.Name = "customResponsibilityMenuItem";
            this.customResponsibilityMenuItem.Size = new System.Drawing.Size(204, 32);
            this.customResponsibilityMenuItem.Text = "客责描述";
            this.customResponsibilityMenuItem.Click += new System.EventHandler(this.customResponsibilityMenuItem_Click);
            // 
            // storeHouseMenuItem
            // 
            this.storeHouseMenuItem.Name = "storeHouseMenuItem";
            this.storeHouseMenuItem.Size = new System.Drawing.Size(204, 32);
            this.storeHouseMenuItem.Text = "仓库别";
            this.storeHouseMenuItem.Click += new System.EventHandler(this.storeHouseMenuItem_Click);
            // 
            // 还货状态ToolStripMenuItem
            // 
            this.还货状态ToolStripMenuItem.Name = "还货状态ToolStripMenuItem";
            this.还货状态ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.还货状态ToolStripMenuItem.Text = "还货状态";
            this.还货状态ToolStripMenuItem.Click += new System.EventHandler(this.还货状态ToolStripMenuItem_Click);
            // 
            // 还货客责类别ToolStripMenuItem
            // 
            this.还货客责类别ToolStripMenuItem.Name = "还货客责类别ToolStripMenuItem";
            this.还货客责类别ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.还货客责类别ToolStripMenuItem.Text = "还货客责类别";
            this.还货客责类别ToolStripMenuItem.Click += new System.EventHandler(this.还货客责类别ToolStripMenuItem_Click);
            // 
            // 维修故障类别ToolStripMenuItem
            // 
            this.维修故障类别ToolStripMenuItem.Name = "维修故障类别ToolStripMenuItem";
            this.维修故障类别ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.维修故障类别ToolStripMenuItem.Text = "维修故障类别";
            this.维修故障类别ToolStripMenuItem.Click += new System.EventHandler(this.维修故障类别ToolStripMenuItem_Click);
            // 
            // 厂商信息ToolStripMenuItem
            // 
            this.厂商信息ToolStripMenuItem.Name = "厂商信息ToolStripMenuItem";
            this.厂商信息ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.厂商信息ToolStripMenuItem.Text = "厂商信息";
            this.厂商信息ToolStripMenuItem.Click += new System.EventHandler(this.厂商信息ToolStripMenuItem_Click);
            // 
            // 报表ToolStripMenuItem
            // 
            this.报表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.报表1ToolStripMenuItem});
            this.报表ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 15F);
            this.报表ToolStripMenuItem.Name = "报表ToolStripMenuItem";
            this.报表ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.报表ToolStripMenuItem.Text = "报表";
            // 
            // 报表1ToolStripMenuItem
            // 
            this.报表1ToolStripMenuItem.Name = "报表1ToolStripMenuItem";
            this.报表1ToolStripMenuItem.Size = new System.Drawing.Size(191, 32);
            this.报表1ToolStripMenuItem.Text = "RMA欠货表";
            this.报表1ToolStripMenuItem.Click += new System.EventHandler(this.报表1ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 687);
            this.Controls.Add(this.AllMenuStrip);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.AllMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "进销存系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.AllMenuStrip.ResumeLayout(false);
            this.AllMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AllMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem UserManageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FunctionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExcelImportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mBMaterialCompareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiveReturnStoreMenuItem;
        private System.Windows.Forms.ToolStripMenuItem additionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customFaultMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guaranteeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customResponsibilityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeHouseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收货ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem returnStoreMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还货状态ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还货客责类别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 个人信息查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 维修ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 维修界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 维修故障类别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vGA维修界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 外观ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 外观检查ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试12ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 厂商信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收货单ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表1ToolStripMenuItem;
    }
}

