namespace SaledServices
{
    partial class ReturnStoreForm
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
            this.dataGridViewToReturn = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.vendorComboBox = new System.Windows.Forms.ComboBox();
            this.productComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxMakeNew = new System.Windows.Forms.CheckBox();
            this.return_file_noTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.storehouseTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.return_dateTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ordernoTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.custommaterialNoTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dpkpnTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.track_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.custom_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.vendormaterialNoTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.custom_res_typeTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.response_describeTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tatTextBox = new System.Windows.Forms.TextBox();
            this.dataGridViewReturnedDetail = new System.Windows.Forms.DataGridView();
            this.returnStore = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.vendor_serail_noTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnedDetail)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewToReturn
            // 
            this.dataGridViewToReturn.AllowUserToAddRows = false;
            this.dataGridViewToReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewToReturn.Location = new System.Drawing.Point(625, 3);
            this.dataGridViewToReturn.Name = "dataGridViewToReturn";
            this.dataGridViewToReturn.RowTemplate.Height = 23;
            this.dataGridViewToReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewToReturn.Size = new System.Drawing.Size(616, 422);
            this.dataGridViewToReturn.TabIndex = 0;
            this.dataGridViewToReturn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewToReturn_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(742, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "待还货界面";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "厂商";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "客户别";
            // 
            // vendorComboBox
            // 
            this.vendorComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vendorComboBox.FormattingEnabled = true;
            this.vendorComboBox.Location = new System.Drawing.Point(118, 6);
            this.vendorComboBox.Name = "vendorComboBox";
            this.vendorComboBox.Size = new System.Drawing.Size(239, 20);
            this.vendorComboBox.TabIndex = 1;
            this.vendorComboBox.SelectedValueChanged += new System.EventHandler(this.vendorComboBox_SelectedValueChanged);
            // 
            // productComboBox
            // 
            this.productComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productComboBox.FormattingEnabled = true;
            this.productComboBox.Location = new System.Drawing.Point(118, 47);
            this.productComboBox.Name = "productComboBox";
            this.productComboBox.Size = new System.Drawing.Size(239, 20);
            this.productComboBox.TabIndex = 2;
            this.productComboBox.SelectedValueChanged += new System.EventHandler(this.productComboBox_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "还货文件编号";
            // 
            // checkBoxMakeNew
            // 
            this.checkBoxMakeNew.AutoSize = true;
            this.checkBoxMakeNew.Location = new System.Drawing.Point(106, 3);
            this.checkBoxMakeNew.Name = "checkBoxMakeNew";
            this.checkBoxMakeNew.Size = new System.Drawing.Size(120, 16);
            this.checkBoxMakeNew.TabIndex = 4;
            this.checkBoxMakeNew.Text = "生成新的还货编号";
            this.checkBoxMakeNew.UseVisualStyleBackColor = true;
            // 
            // return_file_noTextBox
            // 
            this.return_file_noTextBox.Location = new System.Drawing.Point(3, 3);
            this.return_file_noTextBox.Name = "return_file_noTextBox";
            this.return_file_noTextBox.Size = new System.Drawing.Size(97, 21);
            this.return_file_noTextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "客户库别";
            // 
            // storehouseTextBox
            // 
            this.storehouseTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storehouseTextBox.Location = new System.Drawing.Point(118, 129);
            this.storehouseTextBox.Name = "storehouseTextBox";
            this.storehouseTextBox.Size = new System.Drawing.Size(239, 21);
            this.storehouseTextBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "还货时间";
            // 
            // return_dateTextBox
            // 
            this.return_dateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.return_dateTextBox.Location = new System.Drawing.Point(118, 170);
            this.return_dateTextBox.Name = "return_dateTextBox";
            this.return_dateTextBox.Size = new System.Drawing.Size(239, 21);
            this.return_dateTextBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "订单编号";
            // 
            // ordernoTextBox
            // 
            this.ordernoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordernoTextBox.Location = new System.Drawing.Point(118, 211);
            this.ordernoTextBox.Name = "ordernoTextBox";
            this.ordernoTextBox.Size = new System.Drawing.Size(239, 21);
            this.ordernoTextBox.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "客户料号";
            // 
            // custommaterialNoTextBox
            // 
            this.custommaterialNoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custommaterialNoTextBox.Location = new System.Drawing.Point(118, 252);
            this.custommaterialNoTextBox.Name = "custommaterialNoTextBox";
            this.custommaterialNoTextBox.Size = new System.Drawing.Size(239, 21);
            this.custommaterialNoTextBox.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 290);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "DPK状态";
            // 
            // dpkpnTextBox
            // 
            this.dpkpnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpkpnTextBox.Location = new System.Drawing.Point(118, 293);
            this.dpkpnTextBox.Name = "dpkpnTextBox";
            this.dpkpnTextBox.Size = new System.Drawing.Size(239, 21);
            this.dpkpnTextBox.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 331);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "跟踪条码";
            // 
            // track_serial_noTextBox
            // 
            this.track_serial_noTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track_serial_noTextBox.Location = new System.Drawing.Point(118, 334);
            this.track_serial_noTextBox.Name = "track_serial_noTextBox";
            this.track_serial_noTextBox.Size = new System.Drawing.Size(239, 21);
            this.track_serial_noTextBox.TabIndex = 10;
            this.track_serial_noTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.track_serial_noTextBox_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 372);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "客户序号";
            // 
            // custom_serial_noTextBox
            // 
            this.custom_serial_noTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_serial_noTextBox.Location = new System.Drawing.Point(118, 375);
            this.custom_serial_noTextBox.Name = "custom_serial_noTextBox";
            this.custom_serial_noTextBox.Size = new System.Drawing.Size(239, 21);
            this.custom_serial_noTextBox.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(366, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "厂商料号";
            // 
            // vendormaterialNoTextBox
            // 
            this.vendormaterialNoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendormaterialNoTextBox.Location = new System.Drawing.Point(481, 47);
            this.vendormaterialNoTextBox.Name = "vendormaterialNoTextBox";
            this.vendormaterialNoTextBox.Size = new System.Drawing.Size(129, 21);
            this.vendormaterialNoTextBox.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(366, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 13;
            this.label13.Text = "状态";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTextBox.Location = new System.Drawing.Point(481, 88);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(129, 21);
            this.statusTextBox.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(366, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "客责类别";
            // 
            // custom_res_typeTextBox
            // 
            this.custom_res_typeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_res_typeTextBox.Location = new System.Drawing.Point(481, 129);
            this.custom_res_typeTextBox.Name = "custom_res_typeTextBox";
            this.custom_res_typeTextBox.Size = new System.Drawing.Size(129, 21);
            this.custom_res_typeTextBox.TabIndex = 15;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(366, 167);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "客责描述";
            // 
            // response_describeTextBox
            // 
            this.response_describeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.response_describeTextBox.Location = new System.Drawing.Point(481, 170);
            this.response_describeTextBox.Name = "response_describeTextBox";
            this.response_describeTextBox.Size = new System.Drawing.Size(129, 21);
            this.response_describeTextBox.TabIndex = 16;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(366, 208);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 12);
            this.label16.TabIndex = 13;
            this.label16.Text = "TAT";
            // 
            // tatTextBox
            // 
            this.tatTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tatTextBox.Location = new System.Drawing.Point(481, 211);
            this.tatTextBox.Name = "tatTextBox";
            this.tatTextBox.Size = new System.Drawing.Size(129, 21);
            this.tatTextBox.TabIndex = 17;
            // 
            // dataGridViewReturnedDetail
            // 
            this.dataGridViewReturnedDetail.AllowUserToAddRows = false;
            this.dataGridViewReturnedDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReturnedDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReturnedDetail.Location = new System.Drawing.Point(625, 469);
            this.dataGridViewReturnedDetail.Name = "dataGridViewReturnedDetail";
            this.dataGridViewReturnedDetail.RowTemplate.Height = 23;
            this.dataGridViewReturnedDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReturnedDetail.Size = new System.Drawing.Size(616, 318);
            this.dataGridViewReturnedDetail.TabIndex = 15;
            // 
            // returnStore
            // 
            this.returnStore.Location = new System.Drawing.Point(3, 3);
            this.returnStore.Name = "returnStore";
            this.returnStore.Size = new System.Drawing.Size(75, 23);
            this.returnStore.TabIndex = 16;
            this.returnStore.Text = "还货";
            this.returnStore.UseVisualStyleBackColor = true;
            this.returnStore.Click += new System.EventHandler(this.returnStore_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(157, 3);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(75, 23);
            this.query.TabIndex = 16;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Enabled = false;
            this.modify.Location = new System.Drawing.Point(311, 3);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(75, 23);
            this.modify.TabIndex = 16;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(465, 3);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 16;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(366, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 13;
            this.label17.Text = "厂商序号";
            // 
            // vendor_serail_noTextBox
            // 
            this.vendor_serail_noTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendor_serail_noTextBox.Location = new System.Drawing.Point(481, 6);
            this.vendor_serail_noTextBox.Name = "vendor_serail_noTextBox";
            this.vendor_serail_noTextBox.Size = new System.Drawing.Size(129, 21);
            this.vendor_serail_noTextBox.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(366, 249);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idTextBox.Location = new System.Drawing.Point(481, 252);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(129, 21);
            this.idTextBox.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewToReturn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewReturnedDetail, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.24837F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.936709F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.88607F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1244, 790);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.20128F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.8472F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.7602F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.3491F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.vendorComboBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 3, 6);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label18, 2, 6);
            this.tableLayoutPanel3.Controls.Add(this.productComboBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label16, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.response_describeTextBox, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label15, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.storehouseTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.custom_res_typeTextBox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label14, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.return_dateTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.statusTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label13, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.ordernoTextBox, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.vendormaterialNoTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.label12, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.custommaterialNoTextBox, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.vendor_serail_noTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.label17, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.dpkpnTextBox, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.custom_serial_noTextBox, 1, 9);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.track_serial_noTextBox, 1, 8);
            this.tableLayoutPanel3.Controls.Add(this.tatTextBox, 3, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 10;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(616, 422);
            this.tableLayoutPanel3.TabIndex = 19;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.return_file_noTextBox);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxMakeNew);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(118, 88);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(239, 32);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.returnStore, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.query, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.delete, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.modify, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(625, 431);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(616, 32);
            this.tableLayoutPanel2.TabIndex = 19;
            // 
            // ReturnStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 790);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Name = "ReturnStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "还货界面";
            this.Load += new System.EventHandler(this.ReturnStoreForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnedDetail)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewToReturn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox vendorComboBox;
        private System.Windows.Forms.ComboBox productComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxMakeNew;
        private System.Windows.Forms.TextBox return_file_noTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox storehouseTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox return_dateTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ordernoTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox custommaterialNoTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox dpkpnTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox track_serial_noTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox custom_serial_noTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox vendormaterialNoTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox custom_res_typeTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox response_describeTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tatTextBox;
        private System.Windows.Forms.DataGridView dataGridViewReturnedDetail;
        private System.Windows.Forms.Button returnStore;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox vendor_serail_noTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}