﻿namespace SaledServices.Test_Outlook
{
    partial class Test2Form
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
            this.testerTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.testdatetextBox = new System.Windows.Forms.TextBox();
            this.confirmbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.failDescribe = new System.Windows.Forms.RichTextBox();
            this.repairedLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.326007F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.37363F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.25641F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.34432F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tracker_bar_textBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.testerTextBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.testdatetextBox, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.confirmbutton, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.failDescribe, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.repairedLabel, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(821, 438);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "追踪条码";
            // 
            // tracker_bar_textBox
            // 
            this.tracker_bar_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tracker_bar_textBox.Location = new System.Drawing.Point(66, 5);
            this.tracker_bar_textBox.Name = "tracker_bar_textBox";
            this.tracker_bar_textBox.Size = new System.Drawing.Size(207, 21);
            this.tracker_bar_textBox.TabIndex = 1;
            this.tracker_bar_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tracker_bar_textBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "测试人";
            // 
            // testerTextBox
            // 
            this.testerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testerTextBox.Location = new System.Drawing.Point(366, 5);
            this.testerTextBox.Name = "testerTextBox";
            this.testerTextBox.ReadOnly = true;
            this.testerTextBox.Size = new System.Drawing.Size(174, 21);
            this.testerTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(548, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "测试时间";
            // 
            // testdatetextBox
            // 
            this.testdatetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testdatetextBox.Location = new System.Drawing.Point(684, 5);
            this.testdatetextBox.Name = "testdatetextBox";
            this.testdatetextBox.ReadOnly = true;
            this.testdatetextBox.Size = new System.Drawing.Size(132, 21);
            this.testdatetextBox.TabIndex = 1;
            // 
            // confirmbutton
            // 
            this.confirmbutton.Enabled = false;
            this.confirmbutton.Location = new System.Drawing.Point(548, 223);
            this.confirmbutton.Name = "confirmbutton";
            this.confirmbutton.Size = new System.Drawing.Size(72, 23);
            this.confirmbutton.TabIndex = 1;
            this.confirmbutton.Text = "确认OK";
            this.confirmbutton.UseVisualStyleBackColor = true;
            this.confirmbutton.Click += new System.EventHandler(this.confirmbutton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(684, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确认Fail";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // failDescribe
            // 
            this.failDescribe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.failDescribe.Location = new System.Drawing.Point(366, 223);
            this.failDescribe.Name = "failDescribe";
            this.failDescribe.Size = new System.Drawing.Size(174, 210);
            this.failDescribe.TabIndex = 4;
            this.failDescribe.Text = "";
            this.failDescribe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.failDescribe_KeyPress);
            // 
            // repairedLabel
            // 
            this.repairedLabel.AutoSize = true;
            this.repairedLabel.Location = new System.Drawing.Point(66, 220);
            this.repairedLabel.Name = "repairedLabel";
            this.repairedLabel.Size = new System.Drawing.Size(53, 12);
            this.repairedLabel.TabIndex = 3;
            this.repairedLabel.Text = "不良现象";
            // 
            // Test2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 438);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Test2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测试2界面";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tracker_bar_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox testerTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox testdatetextBox;
        private System.Windows.Forms.Button confirmbutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label repairedLabel;
        private System.Windows.Forms.RichTextBox failDescribe;
    }
}