namespace SaledServices
{
    partial class CompalMonthlyExportExcel
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerend = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerstart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.vendorComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.productComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 250);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "导出到Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "结束日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "开始日期";
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Location = new System.Drawing.Point(167, 173);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(187, 26);
            this.dateTimePickerend.TabIndex = 21;
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Location = new System.Drawing.Point(167, 125);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(187, 26);
            this.dateTimePickerstart.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15F);
            this.label3.Location = new System.Drawing.Point(71, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "厂商";
            // 
            // vendorComboBox
            // 
            this.vendorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vendorComboBox.FormattingEnabled = true;
            this.vendorComboBox.Location = new System.Drawing.Point(159, 30);
            this.vendorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorComboBox.Name = "vendorComboBox";
            this.vendorComboBox.Size = new System.Drawing.Size(138, 24);
            this.vendorComboBox.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 15F);
            this.label5.Location = new System.Drawing.Point(71, 60);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "客户别";
            // 
            // productComboBox
            // 
            this.productComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productComboBox.FormattingEnabled = true;
            this.productComboBox.Location = new System.Drawing.Point(159, 60);
            this.productComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.productComboBox.Name = "productComboBox";
            this.productComboBox.Size = new System.Drawing.Size(138, 24);
            this.productComboBox.TabIndex = 26;
            // 
            // CompalMonthlyExportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 384);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vendorComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.productComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerend);
            this.Controls.Add(this.dateTimePickerstart);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CompalMonthlyExportExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compal月报";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerend;
        private System.Windows.Forms.DateTimePicker dateTimePickerstart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox vendorComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox productComboBox;
    }
}