namespace SaledServices.Test_Outlook
{
    partial class QueryAllInfoExistForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView_repair = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView_bga = new System.Windows.Forms.DataGridView();
            this.vendor_serial_no_textBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView_subModify = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView_return = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_repair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_bga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_subModify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_return)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.07983F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.92017F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_repair, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_bga, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.vendor_serial_no_textBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_subModify, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_return, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.859459F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.69567F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.17723F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.69567F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.57197F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1006, 512);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "厂商序号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "维修表";
            // 
            // dataGridView_repair
            // 
            this.dataGridView_repair.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_repair.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_repair.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_repair.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_repair.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_repair.Location = new System.Drawing.Point(127, 36);
            this.dataGridView_repair.Name = "dataGridView_repair";
            this.dataGridView_repair.RowTemplate.Height = 23;
            this.dataGridView_repair.Size = new System.Drawing.Size(874, 117);
            this.dataGridView_repair.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "BGA维修";
            // 
            // dataGridView_bga
            // 
            this.dataGridView_bga.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_bga.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_bga.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_bga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_bga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_bga.Location = new System.Drawing.Point(127, 288);
            this.dataGridView_bga.Name = "dataGridView_bga";
            this.dataGridView_bga.RowTemplate.Height = 23;
            this.dataGridView_bga.Size = new System.Drawing.Size(874, 117);
            this.dataGridView_bga.TabIndex = 8;
            // 
            // vendor_serial_no_textBox
            // 
            this.vendor_serial_no_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendor_serial_no_textBox.Location = new System.Drawing.Point(127, 5);
            this.vendor_serial_no_textBox.Name = "vendor_serial_no_textBox";
            this.vendor_serial_no_textBox.Size = new System.Drawing.Size(874, 21);
            this.vendor_serial_no_textBox.TabIndex = 9;
            this.vendor_serial_no_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vendor_serial_no_textBox_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "维修耗件表";
            // 
            // dataGridView_subModify
            // 
            this.dataGridView_subModify.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_subModify.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_subModify.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_subModify.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_subModify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_subModify.Location = new System.Drawing.Point(127, 161);
            this.dataGridView_subModify.Name = "dataGridView_subModify";
            this.dataGridView_subModify.RowTemplate.Height = 23;
            this.dataGridView_subModify.Size = new System.Drawing.Size(874, 119);
            this.dataGridView_subModify.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 410);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "还货表";
            // 
            // dataGridView_return
            // 
            this.dataGridView_return.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView_return.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_return.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_return.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_return.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_return.Location = new System.Drawing.Point(127, 413);
            this.dataGridView_return.Name = "dataGridView_return";
            this.dataGridView_return.RowTemplate.Height = 23;
            this.dataGridView_return.Size = new System.Drawing.Size(874, 94);
            this.dataGridView_return.TabIndex = 7;
            // 
            // QueryAllInfoExistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 512);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "QueryAllInfoExistForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "板子所有信息查询";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_repair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_bga)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_subModify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_return)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView_return;
        private System.Windows.Forms.DataGridView dataGridView_repair;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView_bga;
        private System.Windows.Forms.TextBox vendor_serial_no_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView_subModify;
    }
}