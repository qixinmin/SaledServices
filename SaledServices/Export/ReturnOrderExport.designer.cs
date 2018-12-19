namespace SaledServices.Export
{
    partial class ReturnOrderExport
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerend = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerstart = new System.Windows.Forms.DateTimePicker();
            this.exportxmlbutton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.returndetail = new System.Windows.Forms.RadioButton();
            this.goodsum = new System.Windows.Forms.RadioButton();
            this.cidsum = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "默认输出路径D:\\导出文件汇总\\";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "结束日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "开始日期";
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Location = new System.Drawing.Point(97, 120);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(187, 21);
            this.dateTimePickerend.TabIndex = 17;
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Location = new System.Drawing.Point(97, 72);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(187, 21);
            this.dateTimePickerstart.TabIndex = 16;
            // 
            // exportxmlbutton
            // 
            this.exportxmlbutton.Location = new System.Drawing.Point(151, 187);
            this.exportxmlbutton.Name = "exportxmlbutton";
            this.exportxmlbutton.Size = new System.Drawing.Size(133, 25);
            this.exportxmlbutton.TabIndex = 15;
            this.exportxmlbutton.Text = "导出还货信息Excel";
            this.exportxmlbutton.UseVisualStyleBackColor = true;
            this.exportxmlbutton.Click += new System.EventHandler(this.exportxmlbutton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cidsum);
            this.panel1.Controls.Add(this.goodsum);
            this.panel1.Controls.Add(this.returndetail);
            this.panel1.Location = new System.Drawing.Point(28, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 54);
            this.panel1.TabIndex = 21;
            // 
            // returndetail
            // 
            this.returndetail.AutoSize = true;
            this.returndetail.Checked = true;
            this.returndetail.Location = new System.Drawing.Point(13, 21);
            this.returndetail.Name = "returndetail";
            this.returndetail.Size = new System.Drawing.Size(71, 16);
            this.returndetail.TabIndex = 0;
            this.returndetail.TabStop = true;
            this.returndetail.Text = "还货明细";
            this.returndetail.UseVisualStyleBackColor = true;
            // 
            // goodsum
            // 
            this.goodsum.AutoSize = true;
            this.goodsum.Location = new System.Drawing.Point(99, 21);
            this.goodsum.Name = "goodsum";
            this.goodsum.Size = new System.Drawing.Size(71, 16);
            this.goodsum.TabIndex = 0;
            this.goodsum.Text = "良品汇总";
            this.goodsum.UseVisualStyleBackColor = true;
            // 
            // cidsum
            // 
            this.cidsum.AutoSize = true;
            this.cidsum.Location = new System.Drawing.Point(176, 21);
            this.cidsum.Name = "cidsum";
            this.cidsum.Size = new System.Drawing.Size(65, 16);
            this.cidsum.TabIndex = 0;
            this.cidsum.Text = "CID汇总";
            this.cidsum.UseVisualStyleBackColor = true;
            // 
            // ReturnOrderExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 308);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerend);
            this.Controls.Add(this.dateTimePickerstart);
            this.Controls.Add(this.exportxmlbutton);
            this.Name = "ReturnOrderExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "还货信息导出";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerend;
        private System.Windows.Forms.DateTimePicker dateTimePickerstart;
        private System.Windows.Forms.Button exportxmlbutton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton cidsum;
        private System.Windows.Forms.RadioButton goodsum;
        private System.Windows.Forms.RadioButton returndetail;
    }
}