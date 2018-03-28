namespace SaledServices
{
    partial class DeliveredTableForm
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
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.vendorTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.productTextBox = new System.Windows.Forms.TextBox();
            this.storehouseTextBox = new System.Windows.Forms.TextBox();
            this.order_out_dateTextBox = new System.Windows.Forms.TextBox();
            this.order_receive_dateTextBox = new System.Windows.Forms.TextBox();
            this.custom_machine_typeTextBox = new System.Windows.Forms.TextBox();
            this.mb_briefTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.custommaterialNoTextBox = new System.Windows.Forms.TextBox();
            this.dpk_statusTextBox = new System.Windows.Forms.TextBox();
            this.track_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.custom_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.vendor_serail_noTextBox = new System.Windows.Forms.TextBox();
            this.uuidTextBox = new System.Windows.Forms.TextBox();
            this.macTextBox = new System.Windows.Forms.TextBox();
            this.vendormaterialNoTextBox = new System.Windows.Forms.TextBox();
            this.mb_describeTextBox = new System.Windows.Forms.TextBox();
            this.mb_make_dateTextBox = new System.Windows.Forms.TextBox();
            this.warranty_periodTextBox = new System.Windows.Forms.TextBox();
            this.lenovo_custom_service_noTextBox = new System.Windows.Forms.TextBox();
            this.lenovo_maintenance_noTextBox = new System.Windows.Forms.TextBox();
            this.lenovo_repair_noTextBox = new System.Windows.Forms.TextBox();
            this.whole_machine_noTextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.source_briefComboBox = new System.Windows.Forms.ComboBox();
            this.custom_faultComboBox = new System.Windows.Forms.ComboBox();
            this.guaranteeComboBox = new System.Windows.Forms.ComboBox();
            this.customResponsibilityComboBox = new System.Windows.Forms.ComboBox();
            this.custom_orderComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(63, 397);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(750, 247);
            this.dataGridView1.TabIndex = 0;
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(119, 340);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 1;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(302, 340);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(75, 23);
            this.query.TabIndex = 1;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Location = new System.Drawing.Point(465, 340);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(75, 23);
            this.modify.TabIndex = 1;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(652, 340);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 1;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.vendorTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.productTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.storehouseTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.order_out_dateTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.order_receive_dateTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.custom_machine_typeTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.mb_briefTextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label15, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label17, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label18, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.label19, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label20, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label21, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label22, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label23, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.label24, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.label25, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.label26, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.label27, 4, 8);
            this.tableLayoutPanel1.Controls.Add(this.custommaterialNoTextBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dpk_statusTextBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.track_serial_noTextBox, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.custom_serial_noTextBox, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.vendor_serail_noTextBox, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.uuidTextBox, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.macTextBox, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.vendormaterialNoTextBox, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.mb_describeTextBox, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.mb_make_dateTextBox, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.warranty_periodTextBox, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lenovo_custom_service_noTextBox, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.lenovo_maintenance_noTextBox, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.lenovo_repair_noTextBox, 5, 7);
            this.tableLayoutPanel1.Controls.Add(this.whole_machine_noTextBox, 5, 8);
            this.tableLayoutPanel1.Controls.Add(this.label28, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.numTextBox, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.source_briefComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.custom_faultComboBox, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.guaranteeComboBox, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.customResponsibilityComboBox, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.custom_orderComboBox, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(63, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(750, 313);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(252, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "客户料号";
            // 
            // vendorTextBox
            // 
            this.vendorTextBox.Location = new System.Drawing.Point(127, 3);
            this.vendorTextBox.Name = "vendorTextBox";
            this.vendorTextBox.Size = new System.Drawing.Size(100, 21);
            this.vendorTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "厂商";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "来源";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "库别";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "订单编号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "客户出库日期";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "收货日期";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "客户机型";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "MB简称";
            // 
            // productTextBox
            // 
            this.productTextBox.Location = new System.Drawing.Point(127, 35);
            this.productTextBox.Name = "productTextBox";
            this.productTextBox.Size = new System.Drawing.Size(100, 21);
            this.productTextBox.TabIndex = 0;
            // 
            // storehouseTextBox
            // 
            this.storehouseTextBox.Location = new System.Drawing.Point(127, 99);
            this.storehouseTextBox.Name = "storehouseTextBox";
            this.storehouseTextBox.Size = new System.Drawing.Size(100, 21);
            this.storehouseTextBox.TabIndex = 0;
            // 
            // order_out_dateTextBox
            // 
            this.order_out_dateTextBox.Location = new System.Drawing.Point(127, 163);
            this.order_out_dateTextBox.Name = "order_out_dateTextBox";
            this.order_out_dateTextBox.Size = new System.Drawing.Size(100, 21);
            this.order_out_dateTextBox.TabIndex = 0;
            // 
            // order_receive_dateTextBox
            // 
            this.order_receive_dateTextBox.Location = new System.Drawing.Point(127, 195);
            this.order_receive_dateTextBox.Name = "order_receive_dateTextBox";
            this.order_receive_dateTextBox.Size = new System.Drawing.Size(100, 21);
            this.order_receive_dateTextBox.TabIndex = 0;
            // 
            // custom_machine_typeTextBox
            // 
            this.custom_machine_typeTextBox.Location = new System.Drawing.Point(127, 227);
            this.custom_machine_typeTextBox.Name = "custom_machine_typeTextBox";
            this.custom_machine_typeTextBox.Size = new System.Drawing.Size(100, 21);
            this.custom_machine_typeTextBox.TabIndex = 0;
            // 
            // mb_briefTextBox
            // 
            this.mb_briefTextBox.Location = new System.Drawing.Point(127, 259);
            this.mb_briefTextBox.Name = "mb_briefTextBox";
            this.mb_briefTextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_briefTextBox.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(252, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "DPK状态";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(252, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "跟踪条码";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(252, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "客户序号";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(252, 128);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "厂商序号";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(252, 160);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 4;
            this.label15.Text = "UUID";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(252, 192);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "MAC";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(252, 224);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "厂商料号";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(252, 256);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 4;
            this.label18.Text = "MB描述";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(502, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 4;
            this.label19.Text = "MB生产日期";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(502, 32);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 4;
            this.label20.Text = "保修期";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(502, 64);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 4;
            this.label21.Text = "客户故障";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(502, 96);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 12);
            this.label22.TabIndex = 4;
            this.label22.Text = "保内/保外";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(502, 128);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 4;
            this.label23.Text = "客责描述";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(502, 160);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 12);
            this.label24.TabIndex = 4;
            this.label24.Text = "联想客服序号";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(502, 192);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(89, 12);
            this.label25.TabIndex = 4;
            this.label25.Text = "联想维修站编号";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(502, 224);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(89, 12);
            this.label26.TabIndex = 4;
            this.label26.Text = "联想维修单编号";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(502, 256);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 4;
            this.label27.Text = "整机序号";
            // 
            // custommaterialNoTextBox
            // 
            this.custommaterialNoTextBox.Location = new System.Drawing.Point(377, 3);
            this.custommaterialNoTextBox.Name = "custommaterialNoTextBox";
            this.custommaterialNoTextBox.Size = new System.Drawing.Size(100, 21);
            this.custommaterialNoTextBox.TabIndex = 0;
            this.custommaterialNoTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.custom_orderComboBox_KeyPress);
            // 
            // dpk_statusTextBox
            // 
            this.dpk_statusTextBox.Location = new System.Drawing.Point(377, 35);
            this.dpk_statusTextBox.Name = "dpk_statusTextBox";
            this.dpk_statusTextBox.Size = new System.Drawing.Size(100, 21);
            this.dpk_statusTextBox.TabIndex = 0;
            // 
            // track_serial_noTextBox
            // 
            this.track_serial_noTextBox.Location = new System.Drawing.Point(377, 67);
            this.track_serial_noTextBox.Name = "track_serial_noTextBox";
            this.track_serial_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.track_serial_noTextBox.TabIndex = 0;
            // 
            // custom_serial_noTextBox
            // 
            this.custom_serial_noTextBox.Location = new System.Drawing.Point(377, 99);
            this.custom_serial_noTextBox.Name = "custom_serial_noTextBox";
            this.custom_serial_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.custom_serial_noTextBox.TabIndex = 0;
            this.custom_serial_noTextBox.TextChanged += new System.EventHandler(this.custom_serial_noTextBox_TextChanged);
            // 
            // vendor_serail_noTextBox
            // 
            this.vendor_serail_noTextBox.Location = new System.Drawing.Point(377, 131);
            this.vendor_serail_noTextBox.Name = "vendor_serail_noTextBox";
            this.vendor_serail_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.vendor_serail_noTextBox.TabIndex = 0;
            // 
            // uuidTextBox
            // 
            this.uuidTextBox.Location = new System.Drawing.Point(377, 163);
            this.uuidTextBox.Name = "uuidTextBox";
            this.uuidTextBox.Size = new System.Drawing.Size(100, 21);
            this.uuidTextBox.TabIndex = 0;
            // 
            // macTextBox
            // 
            this.macTextBox.Location = new System.Drawing.Point(377, 195);
            this.macTextBox.Name = "macTextBox";
            this.macTextBox.Size = new System.Drawing.Size(100, 21);
            this.macTextBox.TabIndex = 0;
            // 
            // vendormaterialNoTextBox
            // 
            this.vendormaterialNoTextBox.Location = new System.Drawing.Point(377, 227);
            this.vendormaterialNoTextBox.Name = "vendormaterialNoTextBox";
            this.vendormaterialNoTextBox.Size = new System.Drawing.Size(100, 21);
            this.vendormaterialNoTextBox.TabIndex = 0;
            // 
            // mb_describeTextBox
            // 
            this.mb_describeTextBox.Location = new System.Drawing.Point(377, 259);
            this.mb_describeTextBox.Name = "mb_describeTextBox";
            this.mb_describeTextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_describeTextBox.TabIndex = 0;
            // 
            // mb_make_dateTextBox
            // 
            this.mb_make_dateTextBox.Location = new System.Drawing.Point(627, 3);
            this.mb_make_dateTextBox.Name = "mb_make_dateTextBox";
            this.mb_make_dateTextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_make_dateTextBox.TabIndex = 0;
            // 
            // warranty_periodTextBox
            // 
            this.warranty_periodTextBox.Location = new System.Drawing.Point(627, 35);
            this.warranty_periodTextBox.Name = "warranty_periodTextBox";
            this.warranty_periodTextBox.Size = new System.Drawing.Size(100, 21);
            this.warranty_periodTextBox.TabIndex = 0;
            // 
            // lenovo_custom_service_noTextBox
            // 
            this.lenovo_custom_service_noTextBox.Location = new System.Drawing.Point(627, 163);
            this.lenovo_custom_service_noTextBox.Name = "lenovo_custom_service_noTextBox";
            this.lenovo_custom_service_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.lenovo_custom_service_noTextBox.TabIndex = 0;
            // 
            // lenovo_maintenance_noTextBox
            // 
            this.lenovo_maintenance_noTextBox.Location = new System.Drawing.Point(627, 195);
            this.lenovo_maintenance_noTextBox.Name = "lenovo_maintenance_noTextBox";
            this.lenovo_maintenance_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.lenovo_maintenance_noTextBox.TabIndex = 0;
            // 
            // lenovo_repair_noTextBox
            // 
            this.lenovo_repair_noTextBox.Location = new System.Drawing.Point(627, 227);
            this.lenovo_repair_noTextBox.Name = "lenovo_repair_noTextBox";
            this.lenovo_repair_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.lenovo_repair_noTextBox.TabIndex = 0;
            // 
            // whole_machine_noTextBox
            // 
            this.whole_machine_noTextBox.Location = new System.Drawing.Point(627, 259);
            this.whole_machine_noTextBox.Name = "whole_machine_noTextBox";
            this.whole_machine_noTextBox.Size = new System.Drawing.Size(100, 21);
            this.whole_machine_noTextBox.TabIndex = 0;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 288);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 12);
            this.label28.TabIndex = 1;
            this.label28.Text = "ID";
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(127, 291);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.ReadOnly = true;
            this.numTextBox.Size = new System.Drawing.Size(100, 21);
            this.numTextBox.TabIndex = 0;
            // 
            // source_briefComboBox
            // 
            this.source_briefComboBox.FormattingEnabled = true;
            this.source_briefComboBox.Location = new System.Drawing.Point(127, 67);
            this.source_briefComboBox.Name = "source_briefComboBox";
            this.source_briefComboBox.Size = new System.Drawing.Size(100, 20);
            this.source_briefComboBox.TabIndex = 5;
            // 
            // custom_faultComboBox
            // 
            this.custom_faultComboBox.FormattingEnabled = true;
            this.custom_faultComboBox.Location = new System.Drawing.Point(627, 67);
            this.custom_faultComboBox.Name = "custom_faultComboBox";
            this.custom_faultComboBox.Size = new System.Drawing.Size(100, 20);
            this.custom_faultComboBox.TabIndex = 5;
            // 
            // guaranteeComboBox
            // 
            this.guaranteeComboBox.FormattingEnabled = true;
            this.guaranteeComboBox.Location = new System.Drawing.Point(627, 99);
            this.guaranteeComboBox.Name = "guaranteeComboBox";
            this.guaranteeComboBox.Size = new System.Drawing.Size(100, 20);
            this.guaranteeComboBox.TabIndex = 5;
            // 
            // customResponsibilityComboBox
            // 
            this.customResponsibilityComboBox.FormattingEnabled = true;
            this.customResponsibilityComboBox.Location = new System.Drawing.Point(627, 131);
            this.customResponsibilityComboBox.Name = "customResponsibilityComboBox";
            this.customResponsibilityComboBox.Size = new System.Drawing.Size(100, 20);
            this.customResponsibilityComboBox.TabIndex = 5;
            // 
            // custom_orderComboBox
            // 
            this.custom_orderComboBox.FormattingEnabled = true;
            this.custom_orderComboBox.Location = new System.Drawing.Point(127, 131);
            this.custom_orderComboBox.Name = "custom_orderComboBox";
            this.custom_orderComboBox.Size = new System.Drawing.Size(100, 20);
            this.custom_orderComboBox.TabIndex = 6;
            this.custom_orderComboBox.SelectedValueChanged += new System.EventHandler(this.custom_orderComboBox_SelectedValueChanged);
            this.custom_orderComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.custom_orderComboBox_KeyPress);
            // 
            // DeliveredTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 675);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.add);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DeliveredTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收货界面";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox productTextBox;
        private System.Windows.Forms.TextBox storehouseTextBox;
        private System.Windows.Forms.TextBox order_out_dateTextBox;
        private System.Windows.Forms.TextBox order_receive_dateTextBox;
        private System.Windows.Forms.TextBox custom_machine_typeTextBox;
        private System.Windows.Forms.TextBox mb_briefTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox custommaterialNoTextBox;
        private System.Windows.Forms.TextBox dpk_statusTextBox;
        private System.Windows.Forms.TextBox track_serial_noTextBox;
        private System.Windows.Forms.TextBox custom_serial_noTextBox;
        private System.Windows.Forms.TextBox vendor_serail_noTextBox;
        private System.Windows.Forms.TextBox uuidTextBox;
        private System.Windows.Forms.TextBox macTextBox;
        private System.Windows.Forms.TextBox vendormaterialNoTextBox;
        private System.Windows.Forms.TextBox mb_describeTextBox;
        private System.Windows.Forms.TextBox mb_make_dateTextBox;
        private System.Windows.Forms.TextBox warranty_periodTextBox;
        private System.Windows.Forms.TextBox lenovo_custom_service_noTextBox;
        private System.Windows.Forms.TextBox lenovo_maintenance_noTextBox;
        private System.Windows.Forms.TextBox lenovo_repair_noTextBox;
        private System.Windows.Forms.TextBox whole_machine_noTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.ComboBox source_briefComboBox;
        private System.Windows.Forms.ComboBox custom_faultComboBox;
        private System.Windows.Forms.ComboBox guaranteeComboBox;
        private System.Windows.Forms.ComboBox customResponsibilityComboBox;
        private System.Windows.Forms.ComboBox custom_orderComboBox;
    }
}