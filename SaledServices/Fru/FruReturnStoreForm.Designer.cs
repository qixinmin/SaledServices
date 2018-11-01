namespace SaledServices
{
    partial class FruReturnStoreForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewToReturn = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.vendorComboBox = new System.Windows.Forms.ComboBox();
            this.productComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridViewReturnedDetail = new System.Windows.Forms.DataGridView();
            this.returnStore = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.peijian_noTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.customer_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.custom_faultTextBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.machine_typeTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.customermaterialdesTextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.mpn1TextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.receive_dateTextBox = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.receiverTextBox = new System.Windows.Forms.TextBox();
            this.ordernotextBox = new System.Windows.Forms.TextBox();
            this.makedatetextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tattextBox = new System.Windows.Forms.TextBox();
            this.guaranteeTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.customermaterialnoTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.vendor_material_noTextBox = new System.Windows.Forms.TextBox();
            this.gurantee_noteTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnedDetail)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewToReturn
            // 
            this.dataGridViewToReturn.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridViewToReturn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewToReturn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewToReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewToReturn.Location = new System.Drawing.Point(660, 4);
            this.dataGridViewToReturn.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewToReturn.Name = "dataGridViewToReturn";
            this.dataGridViewToReturn.ReadOnly = true;
            this.dataGridViewToReturn.RowTemplate.Height = 23;
            this.dataGridViewToReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewToReturn.Size = new System.Drawing.Size(706, 427);
            this.dataGridViewToReturn.TabIndex = 0;
            this.dataGridViewToReturn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewToReturn_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(989, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "待还货界面";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 15F);
            this.label2.Location = new System.Drawing.Point(7, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "厂商";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15F);
            this.label3.Location = new System.Drawing.Point(7, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "客户别";
            // 
            // vendorComboBox
            // 
            this.vendorComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vendorComboBox.FormattingEnabled = true;
            this.vendorComboBox.Location = new System.Drawing.Point(125, 7);
            this.vendorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorComboBox.Name = "vendorComboBox";
            this.vendorComboBox.Size = new System.Drawing.Size(138, 24);
            this.vendorComboBox.TabIndex = 0;
            this.vendorComboBox.SelectedValueChanged += new System.EventHandler(this.vendorComboBox_SelectedValueChanged);
            // 
            // productComboBox
            // 
            this.productComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productComboBox.FormattingEnabled = true;
            this.productComboBox.Location = new System.Drawing.Point(125, 49);
            this.productComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.productComboBox.Name = "productComboBox";
            this.productComboBox.Size = new System.Drawing.Size(138, 24);
            this.productComboBox.TabIndex = 1;
            this.productComboBox.SelectedValueChanged += new System.EventHandler(this.productComboBox_SelectedValueChanged);
            // 
            // dataGridViewReturnedDetail
            // 
            this.dataGridViewReturnedDetail.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridViewReturnedDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewReturnedDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewReturnedDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReturnedDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReturnedDetail.Location = new System.Drawing.Point(660, 493);
            this.dataGridViewReturnedDetail.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewReturnedDetail.Name = "dataGridViewReturnedDetail";
            this.dataGridViewReturnedDetail.ReadOnly = true;
            this.dataGridViewReturnedDetail.RowTemplate.Height = 23;
            this.dataGridViewReturnedDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReturnedDetail.Size = new System.Drawing.Size(706, 252);
            this.dataGridViewReturnedDetail.TabIndex = 15;
            // 
            // returnStore
            // 
            this.returnStore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.returnStore.Location = new System.Drawing.Point(13, 7);
            this.returnStore.Margin = new System.Windows.Forms.Padding(4);
            this.returnStore.Name = "returnStore";
            this.returnStore.Size = new System.Drawing.Size(149, 31);
            this.returnStore.TabIndex = 7;
            this.returnStore.Text = "还货";
            this.returnStore.UseVisualStyleBackColor = true;
            this.returnStore.Click += new System.EventHandler(this.returnStore_Click);
            // 
            // query
            // 
            this.query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.query.Location = new System.Drawing.Point(189, 7);
            this.query.Margin = new System.Windows.Forms.Padding(4);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(149, 31);
            this.query.TabIndex = 8;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modify.Enabled = false;
            this.modify.Location = new System.Drawing.Point(390, 7);
            this.modify.Margin = new System.Windows.Forms.Padding(4);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(100, 31);
            this.modify.TabIndex = 16;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(567, 7);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 31);
            this.delete.TabIndex = 16;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.9562F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.0438F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewToReturn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewReturnedDetail, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.07744F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.209613F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.57944F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 749);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.20128F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.13019F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.17054F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.43411F));
            this.tableLayoutPanel3.Controls.Add(this.statusComboBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.vendorComboBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.productComboBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.peijian_noTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label13, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.customer_serial_noTextBox, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.label21, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.custom_faultTextBox, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.label22, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.label19, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.machine_typeTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label18, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.nameTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.customermaterialdesTextBox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.label28, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.mpn1TextBox, 3, 5);
            this.tableLayoutPanel3.Controls.Add(this.label7, 2, 6);
            this.tableLayoutPanel3.Controls.Add(this.receive_dateTextBox, 3, 6);
            this.tableLayoutPanel3.Controls.Add(this.label29, 2, 7);
            this.tableLayoutPanel3.Controls.Add(this.receiverTextBox, 3, 7);
            this.tableLayoutPanel3.Controls.Add(this.ordernotextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.makedatetextBox, 1, 8);
            this.tableLayoutPanel3.Controls.Add(this.label6, 2, 8);
            this.tableLayoutPanel3.Controls.Add(this.tattextBox, 3, 8);
            this.tableLayoutPanel3.Controls.Add(this.guaranteeTextBox, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.customermaterialnoTextBox, 1, 9);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label20, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label16, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.vendor_material_noTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.gurantee_noteTextBox, 3, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(648, 427);
            this.tableLayoutPanel3.TabIndex = 19;
            // 
            // statusComboBox
            // 
            this.statusComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "良品",
            "不良品"});
            this.statusComboBox.Location = new System.Drawing.Point(125, 133);
            this.statusComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(138, 24);
            this.statusComboBox.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 15F);
            this.label5.Location = new System.Drawing.Point(7, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "订单编号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 15F);
            this.label12.Location = new System.Drawing.Point(7, 171);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 20);
            this.label12.TabIndex = 10;
            this.label12.Text = "配件序号";
            // 
            // peijian_noTextBox
            // 
            this.peijian_noTextBox.Location = new System.Drawing.Point(125, 175);
            this.peijian_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.peijian_noTextBox.Name = "peijian_noTextBox";
            this.peijian_noTextBox.Size = new System.Drawing.Size(138, 26);
            this.peijian_noTextBox.TabIndex = 11;
            this.peijian_noTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peijian_noTextBox_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("SimSun", 15F);
            this.label13.Location = new System.Drawing.Point(7, 213);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 20);
            this.label13.TabIndex = 13;
            this.label13.Text = "客户序号";
            // 
            // customer_serial_noTextBox
            // 
            this.customer_serial_noTextBox.Location = new System.Drawing.Point(125, 217);
            this.customer_serial_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.customer_serial_noTextBox.Name = "customer_serial_noTextBox";
            this.customer_serial_noTextBox.ReadOnly = true;
            this.customer_serial_noTextBox.Size = new System.Drawing.Size(138, 26);
            this.customer_serial_noTextBox.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("SimSun", 15F);
            this.label21.Location = new System.Drawing.Point(7, 255);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 20);
            this.label21.TabIndex = 15;
            this.label21.Text = "客户故障";
            // 
            // custom_faultTextBox
            // 
            this.custom_faultTextBox.Location = new System.Drawing.Point(125, 259);
            this.custom_faultTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_faultTextBox.Name = "custom_faultTextBox";
            this.custom_faultTextBox.ReadOnly = true;
            this.custom_faultTextBox.Size = new System.Drawing.Size(138, 26);
            this.custom_faultTextBox.TabIndex = 14;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("SimSun", 15F);
            this.label22.Location = new System.Drawing.Point(7, 297);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(99, 20);
            this.label22.TabIndex = 16;
            this.label22.Text = "保内/保外";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("SimSun", 15F);
            this.label19.Location = new System.Drawing.Point(7, 339);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 20);
            this.label19.TabIndex = 18;
            this.label19.Text = "生产日期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 15F);
            this.label4.Location = new System.Drawing.Point(274, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "机型";
            // 
            // machine_typeTextBox
            // 
            this.machine_typeTextBox.Location = new System.Drawing.Point(417, 7);
            this.machine_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.machine_typeTextBox.Name = "machine_typeTextBox";
            this.machine_typeTextBox.ReadOnly = true;
            this.machine_typeTextBox.Size = new System.Drawing.Size(212, 26);
            this.machine_typeTextBox.TabIndex = 20;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("SimSun", 15F);
            this.label18.Location = new System.Drawing.Point(274, 45);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 20);
            this.label18.TabIndex = 23;
            this.label18.Text = "名称";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(417, 49);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(212, 26);
            this.nameTextBox.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 15F);
            this.label9.Location = new System.Drawing.Point(274, 129);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 20);
            this.label9.TabIndex = 27;
            this.label9.Text = "客户物料描述";
            // 
            // customermaterialdesTextBox
            // 
            this.customermaterialdesTextBox.Location = new System.Drawing.Point(417, 133);
            this.customermaterialdesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.customermaterialdesTextBox.Name = "customermaterialdesTextBox";
            this.customermaterialdesTextBox.ReadOnly = true;
            this.customermaterialdesTextBox.Size = new System.Drawing.Size(212, 26);
            this.customermaterialdesTextBox.TabIndex = 26;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("SimSun", 15F);
            this.label28.Location = new System.Drawing.Point(274, 213);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 20);
            this.label28.TabIndex = 31;
            this.label28.Text = "MPN1";
            // 
            // mpn1TextBox
            // 
            this.mpn1TextBox.Location = new System.Drawing.Point(417, 217);
            this.mpn1TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpn1TextBox.Name = "mpn1TextBox";
            this.mpn1TextBox.ReadOnly = true;
            this.mpn1TextBox.Size = new System.Drawing.Size(217, 26);
            this.mpn1TextBox.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 15F);
            this.label7.Location = new System.Drawing.Point(274, 255);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 33;
            this.label7.Text = "还货日期";
            // 
            // receive_dateTextBox
            // 
            this.receive_dateTextBox.Location = new System.Drawing.Point(417, 259);
            this.receive_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.receive_dateTextBox.Name = "receive_dateTextBox";
            this.receive_dateTextBox.ReadOnly = true;
            this.receive_dateTextBox.Size = new System.Drawing.Size(217, 26);
            this.receive_dateTextBox.TabIndex = 32;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("SimSun", 15F);
            this.label29.Location = new System.Drawing.Point(274, 297);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(69, 20);
            this.label29.TabIndex = 34;
            this.label29.Text = "还货人";
            // 
            // receiverTextBox
            // 
            this.receiverTextBox.Location = new System.Drawing.Point(416, 300);
            this.receiverTextBox.Name = "receiverTextBox";
            this.receiverTextBox.ReadOnly = true;
            this.receiverTextBox.Size = new System.Drawing.Size(219, 26);
            this.receiverTextBox.TabIndex = 35;
            // 
            // ordernotextBox
            // 
            this.ordernotextBox.Location = new System.Drawing.Point(124, 90);
            this.ordernotextBox.Name = "ordernotextBox";
            this.ordernotextBox.ReadOnly = true;
            this.ordernotextBox.Size = new System.Drawing.Size(140, 26);
            this.ordernotextBox.TabIndex = 36;
            // 
            // makedatetextBox
            // 
            this.makedatetextBox.Location = new System.Drawing.Point(124, 342);
            this.makedatetextBox.Name = "makedatetextBox";
            this.makedatetextBox.ReadOnly = true;
            this.makedatetextBox.Size = new System.Drawing.Size(139, 26);
            this.makedatetextBox.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 15F);
            this.label6.Location = new System.Drawing.Point(274, 339);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "TAT";
            // 
            // tattextBox
            // 
            this.tattextBox.Location = new System.Drawing.Point(416, 342);
            this.tattextBox.Name = "tattextBox";
            this.tattextBox.ReadOnly = true;
            this.tattextBox.Size = new System.Drawing.Size(219, 26);
            this.tattextBox.TabIndex = 35;
            // 
            // guaranteeTextBox
            // 
            this.guaranteeTextBox.Location = new System.Drawing.Point(124, 300);
            this.guaranteeTextBox.Name = "guaranteeTextBox";
            this.guaranteeTextBox.ReadOnly = true;
            this.guaranteeTextBox.Size = new System.Drawing.Size(139, 26);
            this.guaranteeTextBox.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(7, 381);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "客户料号";
            // 
            // customermaterialnoTextBox
            // 
            this.customermaterialnoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.customermaterialnoTextBox.Location = new System.Drawing.Point(125, 385);
            this.customermaterialnoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.customermaterialnoTextBox.Name = "customermaterialnoTextBox";
            this.customermaterialnoTextBox.ReadOnly = true;
            this.customermaterialnoTextBox.Size = new System.Drawing.Size(138, 26);
            this.customermaterialnoTextBox.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 15F);
            this.label8.Location = new System.Drawing.Point(7, 129);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "状态";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("SimSun", 15F);
            this.label20.Location = new System.Drawing.Point(274, 87);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 20);
            this.label20.TabIndex = 29;
            this.label20.Text = "厂商料号";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("SimSun", 15F);
            this.label16.Location = new System.Drawing.Point(274, 171);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 20);
            this.label16.TabIndex = 25;
            this.label16.Text = "备注";
            // 
            // vendor_material_noTextBox
            // 
            this.vendor_material_noTextBox.Location = new System.Drawing.Point(417, 91);
            this.vendor_material_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendor_material_noTextBox.Name = "vendor_material_noTextBox";
            this.vendor_material_noTextBox.ReadOnly = true;
            this.vendor_material_noTextBox.Size = new System.Drawing.Size(217, 26);
            this.vendor_material_noTextBox.TabIndex = 28;
            // 
            // gurantee_noteTextBox
            // 
            this.gurantee_noteTextBox.Location = new System.Drawing.Point(417, 175);
            this.gurantee_noteTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.gurantee_noteTextBox.Name = "gurantee_noteTextBox";
            this.gurantee_noteTextBox.Size = new System.Drawing.Size(212, 26);
            this.gurantee_noteTextBox.TabIndex = 24;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(660, 439);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(706, 46);
            this.tableLayoutPanel2.TabIndex = 19;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.84615F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.84615F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.53846F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.07692F));
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 438);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(650, 48);
            this.tableLayoutPanel4.TabIndex = 20;
            // 
            // FruReturnStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FruReturnStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fru还货界面";
            this.Load += new System.EventHandler(this.ReturnStoreForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnedDetail)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.DataGridView dataGridViewReturnedDetail;
        private System.Windows.Forms.Button returnStore;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox customermaterialnoTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox peijian_noTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox customer_serial_noTextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox custom_faultTextBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox machine_typeTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox gurantee_noteTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox customermaterialdesTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox vendor_material_noTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox mpn1TextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox receive_dateTextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox receiverTextBox;
        private System.Windows.Forms.TextBox ordernotextBox;
        private System.Windows.Forms.TextBox makedatetextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tattextBox;
        private System.Windows.Forms.TextBox guaranteeTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox statusComboBox;
    }
}