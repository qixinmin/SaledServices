namespace SaledServices
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
            this.mbmaterial = new System.Windows.Forms.RadioButton();
            this.receiveOrder = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(128, 242);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "开始导入";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(12, 49);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(253, 21);
            this.filePath.TabIndex = 1;
            // 
            // findFile
            // 
            this.findFile.Location = new System.Drawing.Point(282, 49);
            this.findFile.Name = "findFile";
            this.findFile.Size = new System.Drawing.Size(75, 23);
            this.findFile.TabIndex = 2;
            this.findFile.Text = "文件";
            this.findFile.UseVisualStyleBackColor = true;
            this.findFile.Click += new System.EventHandler(this.findFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.receiveOrder);
            this.panel1.Controls.Add(this.mbmaterial);
            this.panel1.Location = new System.Drawing.Point(12, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 4;
            // 
            // mbmaterial
            // 
            this.mbmaterial.AutoSize = true;
            this.mbmaterial.Location = new System.Drawing.Point(13, 20);
            this.mbmaterial.Name = "mbmaterial";
            this.mbmaterial.Size = new System.Drawing.Size(95, 16);
            this.mbmaterial.TabIndex = 0;
            this.mbmaterial.Text = "MB物料对照表";
            this.mbmaterial.UseVisualStyleBackColor = true;
            // 
            // receiveOrder
            // 
            this.receiveOrder.AutoSize = true;
            this.receiveOrder.Checked = true;
            this.receiveOrder.Location = new System.Drawing.Point(13, 61);
            this.receiveOrder.Name = "receiveOrder";
            this.receiveOrder.Size = new System.Drawing.Size(59, 16);
            this.receiveOrder.TabIndex = 1;
            this.receiveOrder.TabStop = true;
            this.receiveOrder.Text = "收货单";
            this.receiveOrder.UseVisualStyleBackColor = true;
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 375);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.findFile);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.importButton);
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
    }
}