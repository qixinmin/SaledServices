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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AllMenuStrip = new System.Windows.Forms.MenuStrip();
            this.UserManageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VendorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VendorChangeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FunctionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExcelImportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mBMaterialCompareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiveReturnStoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customFaultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guaranteeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customResponsibilityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeHouseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.收货单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收货ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "connectionTest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(339, 188);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // AllMenuStrip
            // 
            this.AllMenuStrip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.AllMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserManageMenuItem,
            this.VendorMenuItem,
            this.FunctionMenuItem,
            this.receiveReturnStoreMenuItem,
            this.additionMenuItem});
            this.AllMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AllMenuStrip.Name = "AllMenuStrip";
            this.AllMenuStrip.Size = new System.Drawing.Size(695, 25);
            this.AllMenuStrip.TabIndex = 4;
            this.AllMenuStrip.Text = "AllMenu";
            // 
            // UserManageMenuItem
            // 
            this.UserManageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginMenuItem,
            this.LogoutMenuItem});
            this.UserManageMenuItem.Name = "UserManageMenuItem";
            this.UserManageMenuItem.Size = new System.Drawing.Size(68, 21);
            this.UserManageMenuItem.Text = "用户管理";
            // 
            // LoginMenuItem
            // 
            this.LoginMenuItem.Name = "LoginMenuItem";
            this.LoginMenuItem.Size = new System.Drawing.Size(100, 22);
            this.LoginMenuItem.Text = "登录";
            this.LoginMenuItem.Click += new System.EventHandler(this.LoginMenuItem_Click);
            // 
            // LogoutMenuItem
            // 
            this.LogoutMenuItem.Name = "LogoutMenuItem";
            this.LogoutMenuItem.Size = new System.Drawing.Size(100, 22);
            this.LogoutMenuItem.Text = "注销";
            // 
            // VendorMenuItem
            // 
            this.VendorMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VendorChangeMenuItem});
            this.VendorMenuItem.Name = "VendorMenuItem";
            this.VendorMenuItem.Size = new System.Drawing.Size(68, 21);
            this.VendorMenuItem.Text = "厂商信息";
            // 
            // VendorChangeMenuItem
            // 
            this.VendorChangeMenuItem.Name = "VendorChangeMenuItem";
            this.VendorChangeMenuItem.Size = new System.Drawing.Size(124, 22);
            this.VendorChangeMenuItem.Text = "信息变更";
            this.VendorChangeMenuItem.Click += new System.EventHandler(this.VendorChangeMenuItem_Click);
            // 
            // FunctionMenuItem
            // 
            this.FunctionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelImportMenuItem,
            this.mBMaterialCompareMenuItem});
            this.FunctionMenuItem.Name = "FunctionMenuItem";
            this.FunctionMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FunctionMenuItem.Text = "功能";
            // 
            // ExcelImportMenuItem
            // 
            this.ExcelImportMenuItem.Name = "ExcelImportMenuItem";
            this.ExcelImportMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ExcelImportMenuItem.Text = "Excel表格导入";
            this.ExcelImportMenuItem.Click += new System.EventHandler(this.ExcelImportMenuItem_Click);
            // 
            // mBMaterialCompareMenuItem
            // 
            this.mBMaterialCompareMenuItem.Name = "mBMaterialCompareMenuItem";
            this.mBMaterialCompareMenuItem.Size = new System.Drawing.Size(156, 22);
            this.mBMaterialCompareMenuItem.Text = "MB物料对照表";
            this.mBMaterialCompareMenuItem.Click += new System.EventHandler(this.mBMaterialCompareMenuItem_Click);
            // 
            // receiveReturnStoreMenuItem
            // 
            this.receiveReturnStoreMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.收货单ToolStripMenuItem,
            this.收货ToolStripMenuItem});
            this.receiveReturnStoreMenuItem.Name = "receiveReturnStoreMenuItem";
            this.receiveReturnStoreMenuItem.Size = new System.Drawing.Size(56, 21);
            this.receiveReturnStoreMenuItem.Text = "收还货";
            this.receiveReturnStoreMenuItem.Click += new System.EventHandler(this.receiveOrderMenuItem_Click);
            // 
            // additionMenuItem
            // 
            this.additionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceMenuItem,
            this.customFaultMenuItem,
            this.guaranteeMenuItem,
            this.customResponsibilityMenuItem,
            this.storeHouseMenuItem});
            this.additionMenuItem.Name = "additionMenuItem";
            this.additionMenuItem.Size = new System.Drawing.Size(68, 21);
            this.additionMenuItem.Text = "附加信息";
            // 
            // sourceMenuItem
            // 
            this.sourceMenuItem.Name = "sourceMenuItem";
            this.sourceMenuItem.Size = new System.Drawing.Size(129, 22);
            this.sourceMenuItem.Text = "收货来源";
            this.sourceMenuItem.Click += new System.EventHandler(this.sourceMenuItem_Click);
            // 
            // customFaultMenuItem
            // 
            this.customFaultMenuItem.Name = "customFaultMenuItem";
            this.customFaultMenuItem.Size = new System.Drawing.Size(129, 22);
            this.customFaultMenuItem.Text = "客户故障";
            this.customFaultMenuItem.Click += new System.EventHandler(this.customFaultMenuItem_Click);
            // 
            // guaranteeMenuItem
            // 
            this.guaranteeMenuItem.Name = "guaranteeMenuItem";
            this.guaranteeMenuItem.Size = new System.Drawing.Size(129, 22);
            this.guaranteeMenuItem.Text = "保内/保外";
            this.guaranteeMenuItem.Click += new System.EventHandler(this.guaranteeMenuItem_Click);
            // 
            // customResponsibilityMenuItem
            // 
            this.customResponsibilityMenuItem.Name = "customResponsibilityMenuItem";
            this.customResponsibilityMenuItem.Size = new System.Drawing.Size(129, 22);
            this.customResponsibilityMenuItem.Text = "客责描述";
            this.customResponsibilityMenuItem.Click += new System.EventHandler(this.customResponsibilityMenuItem_Click);
            // 
            // storeHouseMenuItem
            // 
            this.storeHouseMenuItem.Name = "storeHouseMenuItem";
            this.storeHouseMenuItem.Size = new System.Drawing.Size(129, 22);
            this.storeHouseMenuItem.Text = "仓库别";
            this.storeHouseMenuItem.Click += new System.EventHandler(this.storeHouseMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(238, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 24);
            this.button2.TabIndex = 6;
            this.button2.Text = "OpenDB";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(238, 186);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "QueryButton";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(238, 258);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 25);
            this.button4.TabIndex = 8;
            this.button4.Text = "CloseButton";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(465, 112);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(121, 97);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(346, 292);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 10;
            // 
            // 收货单ToolStripMenuItem
            // 
            this.收货单ToolStripMenuItem.Name = "收货单ToolStripMenuItem";
            this.收货单ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.收货单ToolStripMenuItem.Text = "收货单";
            this.收货单ToolStripMenuItem.Click += new System.EventHandler(this.收货单ToolStripMenuItem_Click);
            // 
            // 收货ToolStripMenuItem
            // 
            this.收货ToolStripMenuItem.Name = "收货ToolStripMenuItem";
            this.收货ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.收货ToolStripMenuItem.Text = "收货";
            this.收货ToolStripMenuItem.Click += new System.EventHandler(this.收货ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 515);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AllMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.AllMenuStrip;
            this.Name = "MainForm";
            this.Text = "进销存系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.AllMenuStrip.ResumeLayout(false);
            this.AllMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip AllMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem UserManageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VendorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VendorChangeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FunctionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExcelImportMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem mBMaterialCompareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiveReturnStoreMenuItem;
        private System.Windows.Forms.ToolStripMenuItem additionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customFaultMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guaranteeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customResponsibilityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeHouseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收货单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收货ToolStripMenuItem;
    }
}

