namespace SaledServices.Repair
{
    partial class RequestBGAForm
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
            this.cancel = new System.Windows.Forms.Button();
            this.requestbutton = new System.Windows.Forms.Button();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.not_good_placeTextBox = new System.Windows.Forms.TextBox();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.requesterTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(428, 355);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(76, 23);
            this.cancel.TabIndex = 16;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // requestbutton
            // 
            this.requestbutton.Location = new System.Drawing.Point(313, 355);
            this.requestbutton.Name = "requestbutton";
            this.requestbutton.Size = new System.Drawing.Size(76, 23);
            this.requestbutton.TabIndex = 17;
            this.requestbutton.Text = "发出请求";
            this.requestbutton.UseVisualStyleBackColor = true;
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(325, 177);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.ReadOnly = true;
            this.numberTextBox.Size = new System.Drawing.Size(100, 21);
            this.numberTextBox.TabIndex = 14;
            this.numberTextBox.Text = "1";
            // 
            // not_good_placeTextBox
            // 
            this.not_good_placeTextBox.Location = new System.Drawing.Point(325, 141);
            this.not_good_placeTextBox.Name = "not_good_placeTextBox";
            this.not_good_placeTextBox.Size = new System.Drawing.Size(100, 21);
            this.not_good_placeTextBox.TabIndex = 15;
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(325, 108);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_brieftextBox.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "请求数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "维修位置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "机型(MB简称)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "请求人";
            // 
            // requesterTextBox
            // 
            this.requesterTextBox.Location = new System.Drawing.Point(325, 220);
            this.requesterTextBox.Name = "requesterTextBox";
            this.requesterTextBox.ReadOnly = true;
            this.requesterTextBox.Size = new System.Drawing.Size(100, 21);
            this.requesterTextBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "请求日期";
            // 
            // dateTextBox
            // 
            this.dateTextBox.Location = new System.Drawing.Point(325, 256);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.ReadOnly = true;
            this.dateTextBox.Size = new System.Drawing.Size(100, 21);
            this.dateTextBox.TabIndex = 14;
            // 
            // RequestBGAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 486);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.requestbutton);
            this.Controls.Add(this.dateTextBox);
            this.Controls.Add(this.requesterTextBox);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.not_good_placeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mb_brieftextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "RequestBGAForm";
            this.Text = "RequestBGAForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button requestbutton;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.TextBox not_good_placeTextBox;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox requesterTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dateTextBox;
    }
}