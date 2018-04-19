namespace SaledServices.Store
{
    partial class RequestToStoreForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FRUSMT = new System.Windows.Forms.RadioButton();
            this.BGA = new System.Windows.Forms.RadioButton();
            this.MB = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.material_mpntextBox = new System.Windows.Forms.TextBox();
            this.track_serial_notextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.material_71pntextBox = new System.Windows.Forms.TextBox();
            this.requestbutton = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "材料MPN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "跟踪条码";
            // 
            // FRUSMT
            // 
            this.FRUSMT.AutoSize = true;
            this.FRUSMT.Checked = true;
            this.FRUSMT.Location = new System.Drawing.Point(48, 15);
            this.FRUSMT.Name = "FRUSMT";
            this.FRUSMT.Size = new System.Drawing.Size(65, 16);
            this.FRUSMT.TabIndex = 2;
            this.FRUSMT.TabStop = true;
            this.FRUSMT.Text = "FRU/SMT";
            this.FRUSMT.UseVisualStyleBackColor = true;
            // 
            // BGA
            // 
            this.BGA.AutoSize = true;
            this.BGA.Location = new System.Drawing.Point(48, 37);
            this.BGA.Name = "BGA";
            this.BGA.Size = new System.Drawing.Size(41, 16);
            this.BGA.TabIndex = 3;
            this.BGA.Text = "BGA";
            this.BGA.UseVisualStyleBackColor = true;
            // 
            // MB
            // 
            this.MB.AutoSize = true;
            this.MB.Location = new System.Drawing.Point(48, 59);
            this.MB.Name = "MB";
            this.MB.Size = new System.Drawing.Size(35, 16);
            this.MB.TabIndex = 4;
            this.MB.Text = "MB";
            this.MB.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FRUSMT);
            this.panel1.Controls.Add(this.MB);
            this.panel1.Controls.Add(this.BGA);
            this.panel1.Location = new System.Drawing.Point(104, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 128);
            this.panel1.TabIndex = 5;
            // 
            // material_mpntextBox
            // 
            this.material_mpntextBox.Location = new System.Drawing.Point(182, 69);
            this.material_mpntextBox.Name = "material_mpntextBox";
            this.material_mpntextBox.ReadOnly = true;
            this.material_mpntextBox.Size = new System.Drawing.Size(100, 21);
            this.material_mpntextBox.TabIndex = 6;
            // 
            // track_serial_notextBox
            // 
            this.track_serial_notextBox.Location = new System.Drawing.Point(182, 36);
            this.track_serial_notextBox.Name = "track_serial_notextBox";
            this.track_serial_notextBox.ReadOnly = true;
            this.track_serial_notextBox.Size = new System.Drawing.Size(100, 21);
            this.track_serial_notextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "材料71PN";
            // 
            // material_71pntextBox
            // 
            this.material_71pntextBox.Location = new System.Drawing.Point(182, 102);
            this.material_71pntextBox.Name = "material_71pntextBox";
            this.material_71pntextBox.ReadOnly = true;
            this.material_71pntextBox.Size = new System.Drawing.Size(100, 21);
            this.material_71pntextBox.TabIndex = 6;
            // 
            // requestbutton
            // 
            this.requestbutton.Location = new System.Drawing.Point(170, 316);
            this.requestbutton.Name = "requestbutton";
            this.requestbutton.Size = new System.Drawing.Size(76, 23);
            this.requestbutton.TabIndex = 8;
            this.requestbutton.Text = "发出请求";
            this.requestbutton.UseVisualStyleBackColor = true;
            this.requestbutton.Click += new System.EventHandler(this.requestbutton_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(285, 316);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(76, 23);
            this.cancel.TabIndex = 8;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // RequestToStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 377);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.requestbutton);
            this.Controls.Add(this.track_serial_notextBox);
            this.Controls.Add(this.material_71pntextBox);
            this.Controls.Add(this.material_mpntextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RequestToStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "向库房发送请求";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton FRUSMT;
        private System.Windows.Forms.RadioButton BGA;
        private System.Windows.Forms.RadioButton MB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox material_mpntextBox;
        private System.Windows.Forms.TextBox track_serial_notextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox material_71pntextBox;
        private System.Windows.Forms.Button requestbutton;
        private System.Windows.Forms.Button cancel;
    }
}