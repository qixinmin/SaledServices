namespace SaledServices
{
    partial class FaultMBRecordForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.add = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.track_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PCHbrieftextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.vendorSnTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.stockplacetextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.statustextBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.input_datetextBox = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.inputertextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CPUbrieTtextBox = new System.Windows.Forms.TextBox();
            this.VGAbreiftextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.mpntextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.mbdescribeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.producttextBox = new System.Windows.Forms.TextBox();
            this.vendorTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.64752F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.019151F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1222, 733);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.add, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.modify, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.delete, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.query, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 433);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1205, 50);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(66, 5);
            this.add.Margin = new System.Windows.Forms.Padding(4);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(168, 40);
            this.add.TabIndex = 0;
            this.add.Text = "不良品入库";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // modify
            // 
            this.modify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modify.Enabled = false;
            this.modify.Location = new System.Drawing.Point(654, 5);
            this.modify.Margin = new System.Windows.Forms.Padding(4);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(197, 40);
            this.modify.TabIndex = 1;
            this.modify.Text = "入库修改";
            this.modify.UseVisualStyleBackColor = true;
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(959, 5);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(189, 40);
            this.delete.TabIndex = 1;
            this.delete.Text = "入库删除";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // query
            // 
            this.query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.query.Location = new System.Drawing.Point(366, 5);
            this.query.Margin = new System.Windows.Forms.Padding(4);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(171, 40);
            this.query.TabIndex = 1;
            this.query.Text = "入库查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 490);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1216, 240);
            this.dataGridView1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.77645F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5294F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.65293F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.65293F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.57049F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.8178F));
            this.tableLayoutPanel2.Controls.Add(this.track_serial_noTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.PCHbrieftextBox, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.vendorSnTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.stockplacetextBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.statustextBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label30, 4, 4);
            this.tableLayoutPanel2.Controls.Add(this.input_datetextBox, 5, 4);
            this.tableLayoutPanel2.Controls.Add(this.label29, 4, 3);
            this.tableLayoutPanel2.Controls.Add(this.inputertextBox, 5, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.CPUbrieTtextBox, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.VGAbreiftextBox, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.mb_brieftextBox, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this.label11, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.mpntextBox, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.label16, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.mbdescribeTextBox, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.producttextBox, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.vendorTextBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.08451F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.08451F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.08451F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.08451F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.08451F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1214, 421);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // track_serial_noTextBox
            // 
            this.track_serial_noTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track_serial_noTextBox.Location = new System.Drawing.Point(197, 6);
            this.track_serial_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.track_serial_noTextBox.Name = "track_serial_noTextBox";
            this.track_serial_noTextBox.Size = new System.Drawing.Size(202, 26);
            this.track_serial_noTextBox.TabIndex = 1;
            this.track_serial_noTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.track_serial_noTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "跟踪条码";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(811, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "PCH简述";
            // 
            // PCHbrieftextBox
            // 
            this.PCHbrieftextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PCHbrieftextBox.Location = new System.Drawing.Point(1011, 6);
            this.PCHbrieftextBox.Margin = new System.Windows.Forms.Padding(4);
            this.PCHbrieftextBox.Name = "PCHbrieftextBox";
            this.PCHbrieftextBox.ReadOnly = true;
            this.PCHbrieftextBox.Size = new System.Drawing.Size(197, 26);
            this.PCHbrieftextBox.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "厂商SN";
            // 
            // vendorSnTextBox
            // 
            this.vendorSnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorSnTextBox.Location = new System.Drawing.Point(197, 89);
            this.vendorSnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorSnTextBox.Name = "vendorSnTextBox";
            this.vendorSnTextBox.Size = new System.Drawing.Size(202, 26);
            this.vendorSnTextBox.TabIndex = 43;
            this.vendorSnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vendorSnTextBox_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 251);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "库位";
            // 
            // stockplacetextBox
            // 
            this.stockplacetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockplacetextBox.Location = new System.Drawing.Point(197, 255);
            this.stockplacetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.stockplacetextBox.Name = "stockplacetextBox";
            this.stockplacetextBox.Size = new System.Drawing.Size(202, 26);
            this.stockplacetextBox.TabIndex = 43;
            this.stockplacetextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stockplacetextBox_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 168);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "状态";
            // 
            // statustextBox
            // 
            this.statustextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statustextBox.Location = new System.Drawing.Point(197, 172);
            this.statustextBox.Margin = new System.Windows.Forms.Padding(4);
            this.statustextBox.Name = "statustextBox";
            this.statustextBox.Size = new System.Drawing.Size(202, 26);
            this.statustextBox.TabIndex = 43;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(811, 334);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 16);
            this.label30.TabIndex = 41;
            this.label30.Text = "录入日期";
            // 
            // input_datetextBox
            // 
            this.input_datetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input_datetextBox.Location = new System.Drawing.Point(1011, 338);
            this.input_datetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.input_datetextBox.Name = "input_datetextBox";
            this.input_datetextBox.ReadOnly = true;
            this.input_datetextBox.Size = new System.Drawing.Size(197, 26);
            this.input_datetextBox.TabIndex = 43;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(811, 251);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(56, 16);
            this.label29.TabIndex = 40;
            this.label29.Text = "入库人";
            // 
            // inputertextBox
            // 
            this.inputertextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputertextBox.Location = new System.Drawing.Point(1011, 255);
            this.inputertextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputertextBox.Name = "inputertextBox";
            this.inputertextBox.ReadOnly = true;
            this.inputertextBox.Size = new System.Drawing.Size(197, 26);
            this.inputertextBox.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(811, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "CPU简述";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(811, 168);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "VGA简述";
            // 
            // CPUbrieTtextBox
            // 
            this.CPUbrieTtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CPUbrieTtextBox.Location = new System.Drawing.Point(1011, 89);
            this.CPUbrieTtextBox.Margin = new System.Windows.Forms.Padding(4);
            this.CPUbrieTtextBox.Name = "CPUbrieTtextBox";
            this.CPUbrieTtextBox.ReadOnly = true;
            this.CPUbrieTtextBox.Size = new System.Drawing.Size(197, 26);
            this.CPUbrieTtextBox.TabIndex = 43;
            // 
            // VGAbreiftextBox
            // 
            this.VGAbreiftextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VGAbreiftextBox.Location = new System.Drawing.Point(1011, 172);
            this.VGAbreiftextBox.Margin = new System.Windows.Forms.Padding(4);
            this.VGAbreiftextBox.Name = "VGAbreiftextBox";
            this.VGAbreiftextBox.ReadOnly = true;
            this.VGAbreiftextBox.Size = new System.Drawing.Size(197, 26);
            this.VGAbreiftextBox.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(409, 334);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "MB简称";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_brieftextBox.Location = new System.Drawing.Point(610, 338);
            this.mb_brieftextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.ReadOnly = true;
            this.mb_brieftextBox.Size = new System.Drawing.Size(191, 26);
            this.mb_brieftextBox.TabIndex = 43;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(409, 251);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "MPN";
            // 
            // mpntextBox
            // 
            this.mpntextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpntextBox.Location = new System.Drawing.Point(610, 255);
            this.mpntextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpntextBox.Name = "mpntextBox";
            this.mpntextBox.ReadOnly = true;
            this.mpntextBox.Size = new System.Drawing.Size(191, 26);
            this.mpntextBox.TabIndex = 43;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(409, 168);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 16);
            this.label16.TabIndex = 11;
            this.label16.Text = "MB描述";
            // 
            // mbdescribeTextBox
            // 
            this.mbdescribeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbdescribeTextBox.Location = new System.Drawing.Point(610, 172);
            this.mbdescribeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mbdescribeTextBox.Name = "mbdescribeTextBox";
            this.mbdescribeTextBox.ReadOnly = true;
            this.mbdescribeTextBox.Size = new System.Drawing.Size(191, 26);
            this.mbdescribeTextBox.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "客户别";
            // 
            // producttextBox
            // 
            this.producttextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.producttextBox.Location = new System.Drawing.Point(610, 89);
            this.producttextBox.Margin = new System.Windows.Forms.Padding(4);
            this.producttextBox.Name = "producttextBox";
            this.producttextBox.ReadOnly = true;
            this.producttextBox.Size = new System.Drawing.Size(191, 26);
            this.producttextBox.TabIndex = 43;
            // 
            // vendorTextBox
            // 
            this.vendorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorTextBox.Location = new System.Drawing.Point(610, 6);
            this.vendorTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorTextBox.Name = "vendorTextBox";
            this.vendorTextBox.ReadOnly = true;
            this.vendorTextBox.Size = new System.Drawing.Size(191, 26);
            this.vendorTextBox.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "厂商";
            // 
            // FaultMBRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 733);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FaultMBRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MB不良品入库";
            this.Load += new System.EventHandler(this.RepairOperationForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox track_serial_noTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox vendorTextBox;
        private System.Windows.Forms.TextBox producttextBox;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.TextBox statustextBox;
        private System.Windows.Forms.TextBox PCHbrieftextBox;
        private System.Windows.Forms.TextBox mpntextBox;
        private System.Windows.Forms.TextBox input_datetextBox;
        private System.Windows.Forms.TextBox inputertextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox stockplacetextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox mbdescribeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox vendorSnTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CPUbrieTtextBox;
        private System.Windows.Forms.TextBox VGAbreiftextBox;
    }
}