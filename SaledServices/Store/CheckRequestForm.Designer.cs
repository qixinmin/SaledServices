namespace SaledServices.Store
{
    partial class CheckRequestForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.processRequestbutton = new System.Windows.Forms.Button();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.requestertextBox = new System.Windows.Forms.TextBox();
            this.not_good_placetextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statustextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.requestNumbertextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.materialMpnTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(368, 219);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(511, 319);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(75, 23);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "刷新请求";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // processRequestbutton
            // 
            this.processRequestbutton.Location = new System.Drawing.Point(511, 164);
            this.processRequestbutton.Name = "processRequestbutton";
            this.processRequestbutton.Size = new System.Drawing.Size(75, 23);
            this.processRequestbutton.TabIndex = 1;
            this.processRequestbutton.Text = "处理请求";
            this.processRequestbutton.UseVisualStyleBackColor = true;
            this.processRequestbutton.Click += new System.EventHandler(this.processRequestbutton_Click);
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(102, 27);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.ReadOnly = true;
            this.mb_brieftextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_brieftextBox.TabIndex = 13;
            // 
            // requestertextBox
            // 
            this.requestertextBox.Location = new System.Drawing.Point(102, 84);
            this.requestertextBox.Name = "requestertextBox";
            this.requestertextBox.ReadOnly = true;
            this.requestertextBox.Size = new System.Drawing.Size(100, 21);
            this.requestertextBox.TabIndex = 11;
            // 
            // not_good_placetextBox
            // 
            this.not_good_placetextBox.Location = new System.Drawing.Point(299, 30);
            this.not_good_placetextBox.Name = "not_good_placetextBox";
            this.not_good_placetextBox.ReadOnly = true;
            this.not_good_placetextBox.Size = new System.Drawing.Size(100, 21);
            this.not_good_placetextBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "申请人";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "机型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "不良位置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "状态";
            // 
            // statustextBox
            // 
            this.statustextBox.Location = new System.Drawing.Point(486, 87);
            this.statustextBox.Name = "statustextBox";
            this.statustextBox.ReadOnly = true;
            this.statustextBox.Size = new System.Drawing.Size(100, 21);
            this.statustextBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(615, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "请求数量";
            // 
            // requestNumbertextBox
            // 
            this.requestNumbertextBox.Location = new System.Drawing.Point(686, 31);
            this.requestNumbertextBox.Name = "requestNumbertextBox";
            this.requestNumbertextBox.ReadOnly = true;
            this.requestNumbertextBox.Size = new System.Drawing.Size(100, 21);
            this.requestNumbertextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "日期";
            // 
            // dateTextBox
            // 
            this.dateTextBox.Location = new System.Drawing.Point(299, 84);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.ReadOnly = true;
            this.dateTextBox.Size = new System.Drawing.Size(100, 21);
            this.dateTextBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(413, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "材料MPN";
            // 
            // materialMpnTextBox
            // 
            this.materialMpnTextBox.Location = new System.Drawing.Point(486, 31);
            this.materialMpnTextBox.Name = "materialMpnTextBox";
            this.materialMpnTextBox.ReadOnly = true;
            this.materialMpnTextBox.Size = new System.Drawing.Size(100, 21);
            this.materialMpnTextBox.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(615, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(688, 93);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(100, 21);
            this.idTextBox.TabIndex = 11;
            // 
            // CheckRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 462);
            this.Controls.Add(this.mb_brieftextBox);
            this.Controls.Add(this.dateTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.statustextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.requestertextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.materialMpnTextBox);
            this.Controls.Add(this.requestNumbertextBox);
            this.Controls.Add(this.not_good_placetextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processRequestbutton);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CheckRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处理请求界面";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Button processRequestbutton;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.TextBox requestertextBox;
        private System.Windows.Forms.TextBox not_good_placetextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox statustextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox requestNumbertextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dateTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox materialMpnTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox idTextBox;
    }
}