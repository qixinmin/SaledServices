﻿namespace SaledServices
{
    partial class ExcelImportForm
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
            this.importButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.filePath = new System.Windows.Forms.TextBox();
            this.findFile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.machineImportRadioButton = new System.Windows.Forms.RadioButton();
            this.FruBom = new System.Windows.Forms.RadioButton();
            this.guranteCheckRadioButton = new System.Windows.Forms.RadioButton();
            this.userInputRadioButton = new System.Windows.Forms.RadioButton();
            this.ngstoreInfoImport = new System.Windows.Forms.RadioButton();
            this.storeInfoImportradioButton = new System.Windows.Forms.RadioButton();
            this.stock_in_sheetradioButton = new System.Windows.Forms.RadioButton();
            this.ymrenbaocodecompare = new System.Windows.Forms.RadioButton();
            this.faultTableRadioButton = new System.Windows.Forms.RadioButton();
            this.DPKradioButton = new System.Windows.Forms.RadioButton();
            this.LCFC71BOMRadioButton = new System.Windows.Forms.RadioButton();
            this.LCFC_MBBOMradioButton = new System.Windows.Forms.RadioButton();
            this.frureceiveOrder = new System.Windows.Forms.RadioButton();
            this.receiveOrder = new System.Windows.Forms.RadioButton();
            this.mbmaterial = new System.Windows.Forms.RadioButton();
            this.updateDBRadio = new System.Windows.Forms.RadioButton();
            this.mbcheckimport = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(639, 446);
            this.importButton.Margin = new System.Windows.Forms.Padding(5);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(125, 38);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "开始导入";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(20, 82);
            this.filePath.Margin = new System.Windows.Forms.Padding(5);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(419, 30);
            this.filePath.TabIndex = 1;
            // 
            // findFile
            // 
            this.findFile.Location = new System.Drawing.Point(470, 82);
            this.findFile.Margin = new System.Windows.Forms.Padding(5);
            this.findFile.Name = "findFile";
            this.findFile.Size = new System.Drawing.Size(125, 38);
            this.findFile.TabIndex = 2;
            this.findFile.Text = "文件";
            this.findFile.UseVisualStyleBackColor = true;
            this.findFile.Click += new System.EventHandler(this.findFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mbcheckimport);
            this.panel1.Controls.Add(this.machineImportRadioButton);
            this.panel1.Controls.Add(this.FruBom);
            this.panel1.Controls.Add(this.guranteCheckRadioButton);
            this.panel1.Controls.Add(this.userInputRadioButton);
            this.panel1.Controls.Add(this.ngstoreInfoImport);
            this.panel1.Controls.Add(this.storeInfoImportradioButton);
            this.panel1.Controls.Add(this.stock_in_sheetradioButton);
            this.panel1.Controls.Add(this.ymrenbaocodecompare);
            this.panel1.Controls.Add(this.faultTableRadioButton);
            this.panel1.Controls.Add(this.DPKradioButton);
            this.panel1.Controls.Add(this.LCFC71BOMRadioButton);
            this.panel1.Controls.Add(this.LCFC_MBBOMradioButton);
            this.panel1.Controls.Add(this.frureceiveOrder);
            this.panel1.Controls.Add(this.receiveOrder);
            this.panel1.Controls.Add(this.mbmaterial);
            this.panel1.Location = new System.Drawing.Point(20, 149);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 512);
            this.panel1.TabIndex = 4;
            // 
            // machineImportRadioButton
            // 
            this.machineImportRadioButton.AutoSize = true;
            this.machineImportRadioButton.Location = new System.Drawing.Point(240, 297);
            this.machineImportRadioButton.Name = "machineImportRadioButton";
            this.machineImportRadioButton.Size = new System.Drawing.Size(167, 24);
            this.machineImportRadioButton.TabIndex = 7;
            this.machineImportRadioButton.Text = "整机出货量导入";
            this.machineImportRadioButton.UseVisualStyleBackColor = true;
            // 
            // FruBom
            // 
            this.FruBom.AutoSize = true;
            this.FruBom.Location = new System.Drawing.Point(22, 173);
            this.FruBom.Name = "FruBom";
            this.FruBom.Size = new System.Drawing.Size(87, 24);
            this.FruBom.TabIndex = 6;
            this.FruBom.Text = "FruBOM";
            this.FruBom.UseVisualStyleBackColor = true;
            // 
            // guranteCheckRadioButton
            // 
            this.guranteCheckRadioButton.AutoSize = true;
            this.guranteCheckRadioButton.Location = new System.Drawing.Point(22, 444);
            this.guranteCheckRadioButton.Name = "guranteCheckRadioButton";
            this.guranteCheckRadioButton.Size = new System.Drawing.Size(147, 24);
            this.guranteCheckRadioButton.TabIndex = 5;
            this.guranteCheckRadioButton.Text = "保修日期检查";
            this.guranteCheckRadioButton.UseVisualStyleBackColor = true;
            // 
            // userInputRadioButton
            // 
            this.userInputRadioButton.AutoSize = true;
            this.userInputRadioButton.Location = new System.Drawing.Point(22, 405);
            this.userInputRadioButton.Name = "userInputRadioButton";
            this.userInputRadioButton.Size = new System.Drawing.Size(147, 24);
            this.userInputRadioButton.TabIndex = 4;
            this.userInputRadioButton.Text = "用户资料导入";
            this.userInputRadioButton.UseVisualStyleBackColor = true;
            // 
            // ngstoreInfoImport
            // 
            this.ngstoreInfoImport.AutoSize = true;
            this.ngstoreInfoImport.Location = new System.Drawing.Point(22, 365);
            this.ngstoreInfoImport.Name = "ngstoreInfoImport";
            this.ngstoreInfoImport.Size = new System.Drawing.Size(207, 24);
            this.ngstoreInfoImport.TabIndex = 4;
            this.ngstoreInfoImport.Text = "不良品库房储位导入";
            this.ngstoreInfoImport.UseVisualStyleBackColor = true;
            // 
            // storeInfoImportradioButton
            // 
            this.storeInfoImportradioButton.AutoSize = true;
            this.storeInfoImportradioButton.Location = new System.Drawing.Point(22, 326);
            this.storeInfoImportradioButton.Name = "storeInfoImportradioButton";
            this.storeInfoImportradioButton.Size = new System.Drawing.Size(187, 24);
            this.storeInfoImportradioButton.TabIndex = 4;
            this.storeInfoImportradioButton.Text = "良品库房储位导入";
            this.storeInfoImportradioButton.UseVisualStyleBackColor = true;
            // 
            // stock_in_sheetradioButton
            // 
            this.stock_in_sheetradioButton.AutoSize = true;
            this.stock_in_sheetradioButton.Location = new System.Drawing.Point(22, 286);
            this.stock_in_sheetradioButton.Name = "stock_in_sheetradioButton";
            this.stock_in_sheetradioButton.Size = new System.Drawing.Size(127, 24);
            this.stock_in_sheetradioButton.TabIndex = 4;
            this.stock_in_sheetradioButton.Text = "材料入库单";
            this.stock_in_sheetradioButton.UseVisualStyleBackColor = true;
            // 
            // ymrenbaocodecompare
            // 
            this.ymrenbaocodecompare.AutoSize = true;
            this.ymrenbaocodecompare.Location = new System.Drawing.Point(240, 243);
            this.ymrenbaocodecompare.Name = "ymrenbaocodecompare";
            this.ymrenbaocodecompare.Size = new System.Drawing.Size(247, 24);
            this.ymrenbaocodecompare.TabIndex = 4;
            this.ymrenbaocodecompare.Text = "一麦仁宝故障代码对照表";
            this.ymrenbaocodecompare.UseVisualStyleBackColor = true;
            // 
            // faultTableRadioButton
            // 
            this.faultTableRadioButton.AutoSize = true;
            this.faultTableRadioButton.Location = new System.Drawing.Point(22, 243);
            this.faultTableRadioButton.Name = "faultTableRadioButton";
            this.faultTableRadioButton.Size = new System.Drawing.Size(127, 24);
            this.faultTableRadioButton.TabIndex = 4;
            this.faultTableRadioButton.Text = "故障代码表";
            this.faultTableRadioButton.UseVisualStyleBackColor = true;
            // 
            // DPKradioButton
            // 
            this.DPKradioButton.AutoSize = true;
            this.DPKradioButton.Location = new System.Drawing.Point(22, 208);
            this.DPKradioButton.Name = "DPKradioButton";
            this.DPKradioButton.Size = new System.Drawing.Size(57, 24);
            this.DPKradioButton.TabIndex = 4;
            this.DPKradioButton.Text = "DPK";
            this.DPKradioButton.UseVisualStyleBackColor = true;
            // 
            // LCFC71BOMRadioButton
            // 
            this.LCFC71BOMRadioButton.AutoSize = true;
            this.LCFC71BOMRadioButton.Location = new System.Drawing.Point(22, 142);
            this.LCFC71BOMRadioButton.Name = "LCFC71BOMRadioButton";
            this.LCFC71BOMRadioButton.Size = new System.Drawing.Size(117, 24);
            this.LCFC71BOMRadioButton.TabIndex = 4;
            this.LCFC71BOMRadioButton.Text = "LCFC71BOM";
            this.LCFC71BOMRadioButton.UseVisualStyleBackColor = true;
            // 
            // LCFC_MBBOMradioButton
            // 
            this.LCFC_MBBOMradioButton.AutoSize = true;
            this.LCFC_MBBOMradioButton.Location = new System.Drawing.Point(22, 106);
            this.LCFC_MBBOMradioButton.Name = "LCFC_MBBOMradioButton";
            this.LCFC_MBBOMradioButton.Size = new System.Drawing.Size(257, 24);
            this.LCFC_MBBOMradioButton.TabIndex = 2;
            this.LCFC_MBBOMradioButton.Text = "LCFC_MBBOM&&COMPAL_MBBOM";
            this.LCFC_MBBOMradioButton.UseVisualStyleBackColor = true;
            // 
            // frureceiveOrder
            // 
            this.frureceiveOrder.AutoSize = true;
            this.frureceiveOrder.Location = new System.Drawing.Point(162, 173);
            this.frureceiveOrder.Margin = new System.Windows.Forms.Padding(5);
            this.frureceiveOrder.Name = "frureceiveOrder";
            this.frureceiveOrder.Size = new System.Drawing.Size(117, 24);
            this.frureceiveOrder.TabIndex = 1;
            this.frureceiveOrder.Text = "Fru收货单";
            this.frureceiveOrder.UseVisualStyleBackColor = true;
            // 
            // receiveOrder
            // 
            this.receiveOrder.AutoSize = true;
            this.receiveOrder.Checked = true;
            this.receiveOrder.Location = new System.Drawing.Point(22, 67);
            this.receiveOrder.Margin = new System.Windows.Forms.Padding(5);
            this.receiveOrder.Name = "receiveOrder";
            this.receiveOrder.Size = new System.Drawing.Size(87, 24);
            this.receiveOrder.TabIndex = 1;
            this.receiveOrder.TabStop = true;
            this.receiveOrder.Text = "收货单";
            this.receiveOrder.UseVisualStyleBackColor = true;
            // 
            // mbmaterial
            // 
            this.mbmaterial.AutoSize = true;
            this.mbmaterial.Location = new System.Drawing.Point(22, 14);
            this.mbmaterial.Margin = new System.Windows.Forms.Padding(5);
            this.mbmaterial.Name = "mbmaterial";
            this.mbmaterial.Size = new System.Drawing.Size(147, 24);
            this.mbmaterial.TabIndex = 0;
            this.mbmaterial.Text = "MB物料对照表";
            this.mbmaterial.UseVisualStyleBackColor = true;
            // 
            // updateDBRadio
            // 
            this.updateDBRadio.AutoSize = true;
            this.updateDBRadio.Location = new System.Drawing.Point(816, 587);
            this.updateDBRadio.Name = "updateDBRadio";
            this.updateDBRadio.Size = new System.Drawing.Size(107, 24);
            this.updateDBRadio.TabIndex = 5;
            this.updateDBRadio.TabStop = true;
            this.updateDBRadio.Text = "更新库存";
            this.updateDBRadio.UseVisualStyleBackColor = true;
            // 
            // mbcheckimport
            // 
            this.mbcheckimport.AutoSize = true;
            this.mbcheckimport.Location = new System.Drawing.Point(240, 345);
            this.mbcheckimport.Name = "mbcheckimport";
            this.mbcheckimport.Size = new System.Drawing.Size(187, 24);
            this.mbcheckimport.TabIndex = 7;
            this.mbcheckimport.Text = "主板拦截信息导入";
            this.mbcheckimport.UseVisualStyleBackColor = true;
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 675);
            this.Controls.Add(this.updateDBRadio);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.findFile);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.importButton);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ExcelImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel表格导入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button findFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton receiveOrder;
        private System.Windows.Forms.RadioButton mbmaterial;
        private System.Windows.Forms.RadioButton LCFC71BOMRadioButton;
        private System.Windows.Forms.RadioButton LCFC_MBBOMradioButton;
        private System.Windows.Forms.RadioButton DPKradioButton;
        private System.Windows.Forms.RadioButton faultTableRadioButton;
        private System.Windows.Forms.RadioButton stock_in_sheetradioButton;
        private System.Windows.Forms.RadioButton storeInfoImportradioButton;
        private System.Windows.Forms.RadioButton userInputRadioButton;
        private System.Windows.Forms.RadioButton ngstoreInfoImport;
        private System.Windows.Forms.RadioButton updateDBRadio;
        private System.Windows.Forms.RadioButton guranteCheckRadioButton;
        private System.Windows.Forms.RadioButton FruBom;
        private System.Windows.Forms.RadioButton frureceiveOrder;
        private System.Windows.Forms.RadioButton ymrenbaocodecompare;
        private System.Windows.Forms.RadioButton machineImportRadioButton;
        private System.Windows.Forms.RadioButton mbcheckimport;
    }
}