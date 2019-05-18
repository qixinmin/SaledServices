namespace SaledServices
{
    partial class FruDeliveredTableForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.vendor_material_noTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ordernoComboBox = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.mpn1TextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.customermaterialnoTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.receive_dateTextBox = new System.Windows.Forms.TextBox();
            this.receiverTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.peijian_noTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.customer_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.custom_faultTextBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.guaranteeComboBox = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.machine_typeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.productTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vendorTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.make_datedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.customermaterialdesTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.gurantee_noteTextBox = new System.Windows.Forms.TextBox();
            this.timecheckresult = new System.Windows.Forms.Label();
            this.timechecktextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewWaitToReturn = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWaitToReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(489, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(855, 296);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(68, 5);
            this.add.Margin = new System.Windows.Forms.Padding(4);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(200, 34);
            this.add.TabIndex = 1;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.query.Location = new System.Drawing.Point(404, 5);
            this.query.Margin = new System.Windows.Forms.Padding(4);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(200, 34);
            this.query.TabIndex = 1;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modify.Location = new System.Drawing.Point(790, 6);
            this.modify.Margin = new System.Windows.Forms.Padding(4);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(100, 31);
            this.modify.TabIndex = 1;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Location = new System.Drawing.Point(1127, 6);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 31);
            this.delete.TabIndex = 1;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label20, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.vendor_material_noTextBox, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ordernoComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label28, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.mpn1TextBox, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customermaterialnoTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label29, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.receive_dateTextBox, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.receiverTextBox, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.peijian_noTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.customer_serial_noTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.custom_faultTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label22, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.guaranteeComboBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label18, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.machine_typeTextBox, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.productTextBox, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.vendorTextBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label19, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.make_datedateTimePicker, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.customermaterialdesTextBox, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.gurantee_noteTextBox, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.timecheckresult, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.timechecktextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1346, 363);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 15F);
            this.label6.Location = new System.Drawing.Point(7, 309);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "结果";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("SimSun", 15F);
            this.label20.Location = new System.Drawing.Point(899, 54);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 20);
            this.label20.TabIndex = 4;
            this.label20.Text = "厂商料号";
            // 
            // vendor_material_noTextBox
            // 
            this.vendor_material_noTextBox.Location = new System.Drawing.Point(1122, 58);
            this.vendor_material_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendor_material_noTextBox.Name = "vendor_material_noTextBox";
            this.vendor_material_noTextBox.ReadOnly = true;
            this.vendor_material_noTextBox.Size = new System.Drawing.Size(217, 26);
            this.vendor_material_noTextBox.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 15F);
            this.label5.Location = new System.Drawing.Point(7, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "订单编号";
            // 
            // ordernoComboBox
            // 
            this.ordernoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ordernoComboBox.FormattingEnabled = true;
            this.ordernoComboBox.Location = new System.Drawing.Point(230, 7);
            this.ordernoComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.ordernoComboBox.Name = "ordernoComboBox";
            this.ordernoComboBox.Size = new System.Drawing.Size(212, 24);
            this.ordernoComboBox.TabIndex = 6;
            this.ordernoComboBox.SelectedValueChanged += new System.EventHandler(this.custom_orderComboBox_SelectedValueChanged);
            this.ordernoComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.custom_orderComboBox_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("SimSun", 15F);
            this.label28.Location = new System.Drawing.Point(899, 105);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 20);
            this.label28.TabIndex = 1;
            this.label28.Text = "MPN1";
            // 
            // mpn1TextBox
            // 
            this.mpn1TextBox.Location = new System.Drawing.Point(1122, 109);
            this.mpn1TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpn1TextBox.Name = "mpn1TextBox";
            this.mpn1TextBox.ReadOnly = true;
            this.mpn1TextBox.Size = new System.Drawing.Size(217, 26);
            this.mpn1TextBox.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(7, 54);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "客户料号";
            // 
            // customermaterialnoTextBox
            // 
            this.customermaterialnoTextBox.BackColor = System.Drawing.Color.Lime;
            this.customermaterialnoTextBox.Location = new System.Drawing.Point(230, 58);
            this.customermaterialnoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.customermaterialnoTextBox.Name = "customermaterialnoTextBox";
            this.customermaterialnoTextBox.Size = new System.Drawing.Size(212, 26);
            this.customermaterialnoTextBox.TabIndex = 0;
            this.customermaterialnoTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.custommaterialNoTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 15F);
            this.label7.Location = new System.Drawing.Point(899, 156);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "收货日期";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("SimSun", 15F);
            this.label29.Location = new System.Drawing.Point(899, 207);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(69, 20);
            this.label29.TabIndex = 1;
            this.label29.Text = "收货人";
            // 
            // receive_dateTextBox
            // 
            this.receive_dateTextBox.Location = new System.Drawing.Point(1122, 160);
            this.receive_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.receive_dateTextBox.Name = "receive_dateTextBox";
            this.receive_dateTextBox.ReadOnly = true;
            this.receive_dateTextBox.Size = new System.Drawing.Size(217, 26);
            this.receive_dateTextBox.TabIndex = 0;
            // 
            // receiverTextBox
            // 
            this.receiverTextBox.Location = new System.Drawing.Point(1121, 210);
            this.receiverTextBox.Name = "receiverTextBox";
            this.receiverTextBox.ReadOnly = true;
            this.receiverTextBox.Size = new System.Drawing.Size(219, 26);
            this.receiverTextBox.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 15F);
            this.label12.Location = new System.Drawing.Point(7, 105);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 20);
            this.label12.TabIndex = 4;
            this.label12.Text = "配件序号";
            // 
            // peijian_noTextBox
            // 
            this.peijian_noTextBox.Location = new System.Drawing.Point(230, 109);
            this.peijian_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.peijian_noTextBox.Name = "peijian_noTextBox";
            this.peijian_noTextBox.Size = new System.Drawing.Size(212, 26);
            this.peijian_noTextBox.TabIndex = 0;
            this.peijian_noTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peijian_noTextBox_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("SimSun", 15F);
            this.label13.Location = new System.Drawing.Point(7, 156);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 20);
            this.label13.TabIndex = 4;
            this.label13.Text = "客户序号";
            // 
            // customer_serial_noTextBox
            // 
            this.customer_serial_noTextBox.Location = new System.Drawing.Point(230, 160);
            this.customer_serial_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.customer_serial_noTextBox.Name = "customer_serial_noTextBox";
            this.customer_serial_noTextBox.Size = new System.Drawing.Size(212, 26);
            this.customer_serial_noTextBox.TabIndex = 0;
            this.customer_serial_noTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.customer_serial_noTextBox_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("SimSun", 15F);
            this.label21.Location = new System.Drawing.Point(7, 207);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 20);
            this.label21.TabIndex = 4;
            this.label21.Text = "客户故障";
            // 
            // custom_faultTextBox
            // 
            this.custom_faultTextBox.Location = new System.Drawing.Point(230, 211);
            this.custom_faultTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_faultTextBox.Name = "custom_faultTextBox";
            this.custom_faultTextBox.Size = new System.Drawing.Size(212, 26);
            this.custom_faultTextBox.TabIndex = 0;
            this.custom_faultTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vendor_serail_noTextBox_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("SimSun", 15F);
            this.label22.Location = new System.Drawing.Point(7, 258);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(99, 20);
            this.label22.TabIndex = 4;
            this.label22.Text = "保内/保外";
            // 
            // guaranteeComboBox
            // 
            this.guaranteeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guaranteeComboBox.FormattingEnabled = true;
            this.guaranteeComboBox.Location = new System.Drawing.Point(230, 262);
            this.guaranteeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.guaranteeComboBox.Name = "guaranteeComboBox";
            this.guaranteeComboBox.Size = new System.Drawing.Size(212, 24);
            this.guaranteeComboBox.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("SimSun", 15F);
            this.label18.Location = new System.Drawing.Point(453, 207);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 20);
            this.label18.TabIndex = 4;
            this.label18.Text = "名称";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(676, 211);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(212, 26);
            this.nameTextBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 15F);
            this.label4.Location = new System.Drawing.Point(453, 156);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "机型";
            // 
            // machine_typeTextBox
            // 
            this.machine_typeTextBox.Location = new System.Drawing.Point(676, 160);
            this.machine_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.machine_typeTextBox.Name = "machine_typeTextBox";
            this.machine_typeTextBox.ReadOnly = true;
            this.machine_typeTextBox.Size = new System.Drawing.Size(212, 26);
            this.machine_typeTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 15F);
            this.label2.Location = new System.Drawing.Point(453, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户别";
            // 
            // productTextBox
            // 
            this.productTextBox.Location = new System.Drawing.Point(676, 109);
            this.productTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.productTextBox.Name = "productTextBox";
            this.productTextBox.ReadOnly = true;
            this.productTextBox.Size = new System.Drawing.Size(212, 26);
            this.productTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(453, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "厂商";
            // 
            // vendorTextBox
            // 
            this.vendorTextBox.Location = new System.Drawing.Point(676, 58);
            this.vendorTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorTextBox.Name = "vendorTextBox";
            this.vendorTextBox.ReadOnly = true;
            this.vendorTextBox.Size = new System.Drawing.Size(212, 26);
            this.vendorTextBox.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("SimSun", 15F);
            this.label19.Location = new System.Drawing.Point(453, 3);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 20);
            this.label19.TabIndex = 4;
            this.label19.Text = "生产日期";
            // 
            // make_datedateTimePicker
            // 
            this.make_datedateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.make_datedateTimePicker.Location = new System.Drawing.Point(675, 6);
            this.make_datedateTimePicker.Name = "make_datedateTimePicker";
            this.make_datedateTimePicker.Size = new System.Drawing.Size(200, 26);
            this.make_datedateTimePicker.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 15F);
            this.label9.Location = new System.Drawing.Point(899, 3);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "客户物料描述";
            // 
            // customermaterialdesTextBox
            // 
            this.customermaterialdesTextBox.Location = new System.Drawing.Point(1122, 7);
            this.customermaterialdesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.customermaterialdesTextBox.Name = "customermaterialdesTextBox";
            this.customermaterialdesTextBox.ReadOnly = true;
            this.customermaterialdesTextBox.Size = new System.Drawing.Size(212, 26);
            this.customermaterialdesTextBox.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("SimSun", 15F);
            this.label16.Location = new System.Drawing.Point(453, 258);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 20);
            this.label16.TabIndex = 4;
            this.label16.Text = "备注";
            // 
            // gurantee_noteTextBox
            // 
            this.gurantee_noteTextBox.Location = new System.Drawing.Point(676, 262);
            this.gurantee_noteTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.gurantee_noteTextBox.Name = "gurantee_noteTextBox";
            this.gurantee_noteTextBox.Size = new System.Drawing.Size(212, 26);
            this.gurantee_noteTextBox.TabIndex = 0;
            // 
            // timecheckresult
            // 
            this.timecheckresult.AutoSize = true;
            this.timecheckresult.Font = new System.Drawing.Font("SimSun", 15F);
            this.timecheckresult.Location = new System.Drawing.Point(453, 309);
            this.timecheckresult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timecheckresult.Name = "timecheckresult";
            this.timecheckresult.Size = new System.Drawing.Size(49, 20);
            this.timecheckresult.TabIndex = 10;
            this.timecheckresult.Text = "结果";
            // 
            // timechecktextBox
            // 
            this.timechecktextBox.Location = new System.Drawing.Point(230, 313);
            this.timechecktextBox.Margin = new System.Windows.Forms.Padding(4);
            this.timechecktextBox.Name = "timechecktextBox";
            this.timechecktextBox.Size = new System.Drawing.Size(212, 26);
            this.timechecktextBox.TabIndex = 9;
            this.timechecktextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.timechecktextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15F);
            this.label3.Location = new System.Drawing.Point(676, 309);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 40);
            this.label3.TabIndex = 10;
            this.label3.Text = "可以根据配件序号进行精确查询";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.6008F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.209613F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.05608F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1354, 733);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.add, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.query, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.modify, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.delete, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 375);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1346, 44);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.99722F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.00278F));
            this.tableLayoutPanel4.Controls.Add(this.dataGridView1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.dataGridViewWaitToReturn, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 426);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1348, 304);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // dataGridViewWaitToReturn
            // 
            this.dataGridViewWaitToReturn.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridViewWaitToReturn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewWaitToReturn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewWaitToReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWaitToReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewWaitToReturn.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewWaitToReturn.Name = "dataGridViewWaitToReturn";
            this.dataGridViewWaitToReturn.ReadOnly = true;
            this.dataGridViewWaitToReturn.RowTemplate.Height = 23;
            this.dataGridViewWaitToReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewWaitToReturn.Size = new System.Drawing.Size(479, 298);
            this.dataGridViewWaitToReturn.TabIndex = 1;
            this.dataGridViewWaitToReturn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWaitToReturn_CellClick);
            // 
            // FruDeliveredTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 733);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FruDeliveredTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fru收货界面";
            this.Load += new System.EventHandler(this.DeliveredTableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWaitToReturn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox vendorTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox productTextBox;
        private System.Windows.Forms.TextBox machine_typeTextBox;
        private System.Windows.Forms.TextBox receive_dateTextBox;
        private System.Windows.Forms.TextBox customermaterialdesTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox customermaterialnoTextBox;
        private System.Windows.Forms.TextBox peijian_noTextBox;
        private System.Windows.Forms.TextBox customer_serial_noTextBox;
        private System.Windows.Forms.TextBox custom_faultTextBox;
        private System.Windows.Forms.TextBox gurantee_noteTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox vendor_material_noTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox mpn1TextBox;
        private System.Windows.Forms.ComboBox guaranteeComboBox;
        private System.Windows.Forms.ComboBox ordernoComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox receiverTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dataGridViewWaitToReturn;
        private System.Windows.Forms.DateTimePicker make_datedateTimePicker;
        private System.Windows.Forms.Label timecheckresult;
        private System.Windows.Forms.TextBox timechecktextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}