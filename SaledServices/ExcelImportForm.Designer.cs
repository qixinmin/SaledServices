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
            this.importTargetComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(83, 205);
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
            // importTargetComboBox
            // 
            this.importTargetComboBox.FormattingEnabled = true;
            this.importTargetComboBox.Items.AddRange(new object[] {
            "MB物料对照表",
            "收货单"});
            this.importTargetComboBox.Location = new System.Drawing.Point(12, 116);
            this.importTargetComboBox.Name = "importTargetComboBox";
            this.importTargetComboBox.Size = new System.Drawing.Size(121, 20);
            this.importTargetComboBox.TabIndex = 3;
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 375);
            this.Controls.Add(this.importTargetComboBox);
            this.Controls.Add(this.findFile);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.importButton);
            this.Name = "ExcelImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel表格导入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button findFile;
        private System.Windows.Forms.ComboBox importTargetComboBox;
    }
}