namespace SaledServices
{
    partial class LifeCycleQueryForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.querytrackno = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tracknoTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._8sCode = new System.Windows.Forms.Label();
            this.query8s = new System.Windows.Forms.Button();
            this._8sCodetextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerstart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerend = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 131);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(950, 613);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // querytrackno
            // 
            this.querytrackno.Font = new System.Drawing.Font("SimSun", 15F);
            this.querytrackno.Location = new System.Drawing.Point(425, 8);
            this.querytrackno.Margin = new System.Windows.Forms.Padding(5);
            this.querytrackno.Name = "querytrackno";
            this.querytrackno.Size = new System.Drawing.Size(150, 35);
            this.querytrackno.TabIndex = 3;
            this.querytrackno.Text = "跟踪条码查询";
            this.querytrackno.UseVisualStyleBackColor = true;
            this.querytrackno.Click += new System.EventHandler(this.query_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15F);
            this.label3.Location = new System.Drawing.Point(8, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "跟踪条码";
            // 
            // tracknoTextBox
            // 
            this.tracknoTextBox.Location = new System.Drawing.Point(170, 8);
            this.tracknoTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.tracknoTextBox.Name = "tracknoTextBox";
            this.tracknoTextBox.Size = new System.Drawing.Size(226, 30);
            this.tracknoTextBox.TabIndex = 2;
            this.tracknoTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tracknoTextBox_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.95594F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.04406F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 749);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.6423F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.3577F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 364F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.querytrackno, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this._8sCode, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.query8s, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.tracknoTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._8sCodetextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.button1, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(954, 120);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // _8sCode
            // 
            this._8sCode.AutoSize = true;
            this._8sCode.Location = new System.Drawing.Point(6, 72);
            this._8sCode.Name = "_8sCode";
            this._8sCode.Size = new System.Drawing.Size(49, 20);
            this._8sCode.TabIndex = 4;
            this._8sCode.Text = "8s码";
            // 
            // query8s
            // 
            this.query8s.Font = new System.Drawing.Font("SimSun", 15F);
            this.query8s.Location = new System.Drawing.Point(425, 77);
            this.query8s.Margin = new System.Windows.Forms.Padding(5);
            this.query8s.Name = "query8s";
            this.query8s.Size = new System.Drawing.Size(150, 31);
            this.query8s.TabIndex = 3;
            this.query8s.Text = "8s查询";
            this.query8s.UseVisualStyleBackColor = true;
            this.query8s.Click += new System.EventHandler(this.query8s_Click);
            // 
            // _8sCodetextBox
            // 
            this._8sCodetextBox.Location = new System.Drawing.Point(170, 77);
            this._8sCodetextBox.Margin = new System.Windows.Forms.Padding(5);
            this._8sCodetextBox.Name = "_8sCodetextBox";
            this._8sCodetextBox.Size = new System.Drawing.Size(226, 30);
            this._8sCodetextBox.TabIndex = 2;
            this._8sCodetextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._8sCodetextBox_KeyPress);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(589, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(359, 39);
            this.button1.TabIndex = 5;
            this.button1.Text = "生命周期报表导出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.91453F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.08547F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.dateTimePickerstart, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.dateTimePickerend, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(589, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(359, 60);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "结束日期";
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerstart.Location = new System.Drawing.Point(113, 6);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(240, 30);
            this.dateTimePickerstart.TabIndex = 17;
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerend.Location = new System.Drawing.Point(113, 34);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(240, 30);
            this.dateTimePickerend.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "开始日期";
            // 
            // LifeCycleQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "LifeCycleQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生命周期查询";
            this.Load += new System.EventHandler(this.LifeCycleQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button querytrackno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tracknoTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label _8sCode;
        private System.Windows.Forms.TextBox _8sCodetextBox;
        private System.Windows.Forms.Button query8s;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DateTimePicker dateTimePickerstart;
        private System.Windows.Forms.DateTimePicker dateTimePickerend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}