﻿namespace SaledServices.Test_Outlook
{
    partial class CheckInfoForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tracker_bar_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox8s = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stationlabel = new System.Windows.Forms.Label();
            this.dpkstatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tracker_bar_textBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox8s, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.stationlabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.dpkstatus, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(62, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 182);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "追踪条码";
            // 
            // tracker_bar_textBox
            // 
            this.tracker_bar_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tracker_bar_textBox.Location = new System.Drawing.Point(144, 3);
            this.tracker_bar_textBox.Name = "tracker_bar_textBox";
            this.tracker_bar_textBox.Size = new System.Drawing.Size(323, 21);
            this.tracker_bar_textBox.TabIndex = 0;
            this.tracker_bar_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tracker_bar_textBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "8S码";
            // 
            // textBox8s
            // 
            this.textBox8s.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox8s.Location = new System.Drawing.Point(144, 48);
            this.textBox8s.Name = "textBox8s";
            this.textBox8s.Size = new System.Drawing.Size(323, 21);
            this.textBox8s.TabIndex = 4;
            this.textBox8s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox8s_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "DPK状态";
            // 
            // stationlabel
            // 
            this.stationlabel.AutoSize = true;
            this.stationlabel.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stationlabel.Location = new System.Drawing.Point(144, 131);
            this.stationlabel.Name = "stationlabel";
            this.stationlabel.Size = new System.Drawing.Size(0, 21);
            this.stationlabel.TabIndex = 3;
            // 
            // dpkstatus
            // 
            this.dpkstatus.AutoSize = true;
            this.dpkstatus.Location = new System.Drawing.Point(144, 90);
            this.dpkstatus.Name = "dpkstatus";
            this.dpkstatus.Size = new System.Drawing.Size(0, 12);
            this.dpkstatus.TabIndex = 6;
            // 
            // CheckInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 315);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CheckInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主板信息查询";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tracker_bar_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label stationlabel;
        private System.Windows.Forms.TextBox textBox8s;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label dpkstatus;
    }
}