namespace SaledServices.Store
{
    partial class AdjustStoreHouseForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.not_good_placeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.materialMpnTextBox = new System.Windows.Forms.TextBox();
            this.materialDescribetextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.materialDestextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.releasePlacebutton = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.mpnlefttextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridViewleft = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.transferNumtextBox = new System.Windows.Forms.TextBox();
            this.dataGridViewright = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.mpnrighttextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.storeTargetTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewleft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewright)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机型(MB简称)";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(102, 63);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_brieftextBox.TabIndex = 6;
            this.mb_brieftextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mb_brieftextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "维修位置(回车)";
            // 
            // not_good_placeTextBox
            // 
            this.not_good_placeTextBox.Location = new System.Drawing.Point(102, 104);
            this.not_good_placeTextBox.Name = "not_good_placeTextBox";
            this.not_good_placeTextBox.Size = new System.Drawing.Size(100, 21);
            this.not_good_placeTextBox.TabIndex = 6;
            this.not_good_placeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.not_good_placeTextBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "材料MPN";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(219, 47);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(604, 218);
            this.dataGridView.TabIndex = 20;
            this.dataGridView.VirtualMode = true;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // materialMpnTextBox
            // 
            this.materialMpnTextBox.Location = new System.Drawing.Point(272, 20);
            this.materialMpnTextBox.Name = "materialMpnTextBox";
            this.materialMpnTextBox.ReadOnly = true;
            this.materialMpnTextBox.Size = new System.Drawing.Size(116, 21);
            this.materialMpnTextBox.TabIndex = 6;
            // 
            // materialDescribetextBox
            // 
            this.materialDescribetextBox.Location = new System.Drawing.Point(453, 20);
            this.materialDescribetextBox.Name = "materialDescribetextBox";
            this.materialDescribetextBox.ReadOnly = true;
            this.materialDescribetextBox.Size = new System.Drawing.Size(208, 21);
            this.materialDescribetextBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(394, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "材料描述";
            // 
            // materialDestextBox
            // 
            this.materialDestextBox.Location = new System.Drawing.Point(102, 26);
            this.materialDestextBox.Name = "materialDestextBox";
            this.materialDestextBox.Size = new System.Drawing.Size(100, 21);
            this.materialDestextBox.TabIndex = 22;
            this.materialDestextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.materialDestextBox_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "材料描述(模糊)";
            // 
            // releasePlacebutton
            // 
            this.releasePlacebutton.Font = new System.Drawing.Font("SimSun", 15F);
            this.releasePlacebutton.Location = new System.Drawing.Point(423, 391);
            this.releasePlacebutton.Margin = new System.Windows.Forms.Padding(5);
            this.releasePlacebutton.Name = "releasePlacebutton";
            this.releasePlacebutton.Size = new System.Drawing.Size(114, 55);
            this.releasePlacebutton.TabIndex = 33;
            this.releasePlacebutton.Text = "转移=>";
            this.releasePlacebutton.UseVisualStyleBackColor = true;
            this.releasePlacebutton.Click += new System.EventHandler(this.releasePlacebutton_Click);
            // 
            // query
            // 
            this.query.Font = new System.Drawing.Font("SimSun", 15F);
            this.query.Location = new System.Drawing.Point(197, 304);
            this.query.Margin = new System.Windows.Forms.Padding(5);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(132, 33);
            this.query.TabIndex = 34;
            this.query.Text = "查询左侧";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // mpnlefttextBox
            // 
            this.mpnlefttextBox.Location = new System.Drawing.Point(14, 314);
            this.mpnlefttextBox.Margin = new System.Windows.Forms.Padding(5);
            this.mpnlefttextBox.Name = "mpnlefttextBox";
            this.mpnlefttextBox.Size = new System.Drawing.Size(175, 21);
            this.mpnlefttextBox.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("SimSun", 15F);
            this.label11.Location = new System.Drawing.Point(12, 289);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 20);
            this.label11.TabIndex = 24;
            this.label11.Text = "存储料号";
            // 
            // dataGridViewleft
            // 
            this.dataGridViewleft.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridViewleft.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewleft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewleft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewleft.Location = new System.Drawing.Point(14, 347);
            this.dataGridViewleft.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewleft.Name = "dataGridViewleft";
            this.dataGridViewleft.ReadOnly = true;
            this.dataGridViewleft.RowTemplate.Height = 23;
            this.dataGridViewleft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewleft.Size = new System.Drawing.Size(399, 99);
            this.dataGridViewleft.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 15F);
            this.label2.Location = new System.Drawing.Point(433, 335);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "转移数量";
            // 
            // transferNumtextBox
            // 
            this.transferNumtextBox.Location = new System.Drawing.Point(437, 360);
            this.transferNumtextBox.Margin = new System.Windows.Forms.Padding(5);
            this.transferNumtextBox.Name = "transferNumtextBox";
            this.transferNumtextBox.Size = new System.Drawing.Size(85, 21);
            this.transferNumtextBox.TabIndex = 30;
            // 
            // dataGridViewright
            // 
            this.dataGridViewright.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridViewright.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewright.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewright.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewright.Location = new System.Drawing.Point(547, 335);
            this.dataGridViewright.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewright.Name = "dataGridViewright";
            this.dataGridViewright.ReadOnly = true;
            this.dataGridViewright.RowTemplate.Height = 23;
            this.dataGridViewright.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewright.Size = new System.Drawing.Size(393, 114);
            this.dataGridViewright.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 15F);
            this.label4.Location = new System.Drawing.Point(543, 279);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "存储料号";
            // 
            // mpnrighttextBox
            // 
            this.mpnrighttextBox.Location = new System.Drawing.Point(544, 304);
            this.mpnrighttextBox.Margin = new System.Windows.Forms.Padding(5);
            this.mpnrighttextBox.Name = "mpnrighttextBox";
            this.mpnrighttextBox.Size = new System.Drawing.Size(130, 21);
            this.mpnrighttextBox.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimSun", 15F);
            this.button1.Location = new System.Drawing.Point(820, 294);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 33);
            this.button1.TabIndex = 34;
            this.button1.Text = "查询右侧";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 15F);
            this.label5.Location = new System.Drawing.Point(676, 279);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "库房位置";
            // 
            // storeTargetTextBox
            // 
            this.storeTargetTextBox.Location = new System.Drawing.Point(680, 304);
            this.storeTargetTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.storeTargetTextBox.Name = "storeTargetTextBox";
            this.storeTargetTextBox.Size = new System.Drawing.Size(130, 21);
            this.storeTargetTextBox.TabIndex = 28;
            // 
            // AdjustStoreHouseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 474);
            this.Controls.Add(this.releasePlacebutton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.storeTargetTextBox);
            this.Controls.Add(this.mpnrighttextBox);
            this.Controls.Add(this.query);
            this.Controls.Add(this.mpnlefttextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.transferNumtextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridViewright);
            this.Controls.Add(this.dataGridViewleft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.materialDestextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.not_good_placeTextBox);
            this.Controls.Add(this.materialDescribetextBox);
            this.Controls.Add(this.materialMpnTextBox);
            this.Controls.Add(this.mb_brieftextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "AdjustStoreHouseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库房转移";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewleft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewright)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox not_good_placeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox materialMpnTextBox;
        private System.Windows.Forms.TextBox materialDescribetextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox materialDestextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button releasePlacebutton;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.TextBox mpnlefttextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridViewleft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox transferNumtextBox;
        private System.Windows.Forms.DataGridView dataGridViewright;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mpnrighttextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox storeTargetTextBox;
    }
}