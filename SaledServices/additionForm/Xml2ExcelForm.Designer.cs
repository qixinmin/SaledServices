namespace SaledServices.additionForm
{
    partial class Xml2ExcelForm
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
            this.findFile = new System.Windows.Forms.Button();
            this.filePath = new System.Windows.Forms.TextBox();
            this.importButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // findFile
            // 
            this.findFile.Location = new System.Drawing.Point(443, 73);
            this.findFile.Margin = new System.Windows.Forms.Padding(5);
            this.findFile.Name = "findFile";
            this.findFile.Size = new System.Drawing.Size(125, 38);
            this.findFile.TabIndex = 4;
            this.findFile.Text = "选择文件";
            this.findFile.UseVisualStyleBackColor = true;
            this.findFile.Click += new System.EventHandler(this.findFile_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(14, 83);
            this.filePath.Margin = new System.Windows.Forms.Padding(5);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(419, 21);
            this.filePath.TabIndex = 3;
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(443, 141);
            this.importButton.Margin = new System.Windows.Forms.Padding(5);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(125, 38);
            this.importButton.TabIndex = 5;
            this.importButton.Text = "开始转换";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml,xls,xlsx";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(539, 158);
            this.label1.TabIndex = 6;
            this.label1.Text = "excel sheet设置test，文件设置到 \"D:\\\\导出文件汇总\\\\\";";
            // 
            // Xml2ExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 382);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.findFile);
            this.Controls.Add(this.filePath);
            this.Name = "Xml2ExcelForm";
            this.Text = "XML与Excel转换";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findFile;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
    }
}