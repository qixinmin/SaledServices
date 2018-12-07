namespace SaledServices
{
    partial class TransferOrSoldForm
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
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.vendorComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mb_briefTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.material_typecomboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.receivertextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.notetextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.inputerTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.input_dateTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.outreasontextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statuscomboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.describeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.otherbriefTextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VGA = new System.Windows.Forms.RadioButton();
            this.PCH = new System.Windows.Forms.RadioButton();
            this.CPU = new System.Windows.Forms.RadioButton();
            this.mpnTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(120, 6);
            this.add.Margin = new System.Windows.Forms.Padding(4);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(100, 31);
            this.add.TabIndex = 0;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.query.Location = new System.Drawing.Point(460, 6);
            this.query.Margin = new System.Windows.Forms.Padding(4);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(100, 31);
            this.query.TabIndex = 1;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modify.Location = new System.Drawing.Point(800, 6);
            this.modify.Margin = new System.Windows.Forms.Padding(4);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(100, 31);
            this.modify.TabIndex = 2;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Location = new System.Drawing.Point(1141, 6);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 31);
            this.delete.TabIndex = 3;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.64753F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.94259F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.27637F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 749);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.69533F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.69533F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.69533F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.88698F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.3317F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.69533F));
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.vendorComboBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.button1, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.mb_briefTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.material_typecomboBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.receivertextBox, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label20, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.notetextBox, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.label14, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.label15, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.inputerTextBox, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.label16, 4, 4);
            this.tableLayoutPanel3.Controls.Add(this.input_dateTextBox, 5, 4);
            this.tableLayoutPanel3.Controls.Add(this.label19, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.outreasontextBox, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.numberTextBox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.statuscomboBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.describeTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.otherbriefTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label21, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.mpnTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label8, 1, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1362, 259);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 3);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "厂商";
            // 
            // vendorComboBox
            // 
            this.vendorComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorComboBox.FormattingEnabled = true;
            this.vendorComboBox.Location = new System.Drawing.Point(232, 6);
            this.vendorComboBox.Name = "vendorComboBox";
            this.vendorComboBox.Size = new System.Drawing.Size(217, 24);
            this.vendorComboBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 167);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "MB简称";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(459, 212);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "导出内容到Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mb_briefTextBox
            // 
            this.mb_briefTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_briefTextBox.Location = new System.Drawing.Point(233, 171);
            this.mb_briefTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mb_briefTextBox.Name = "mb_briefTextBox";
            this.mb_briefTextBox.ReadOnly = true;
            this.mb_briefTextBox.Size = new System.Drawing.Size(215, 26);
            this.mb_briefTextBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "材料类别";
            // 
            // material_typecomboBox
            // 
            this.material_typecomboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "MB",
            "BGA",
            "FRU",
            "SMT"});
            this.material_typecomboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.material_typecomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.material_typecomboBox.FormattingEnabled = true;
            this.material_typecomboBox.Items.AddRange(new object[] {
            "MB",
            "BGA",
            "FRU",
            "SMT"});
            this.material_typecomboBox.Location = new System.Drawing.Point(232, 47);
            this.material_typecomboBox.Name = "material_typecomboBox";
            this.material_typecomboBox.Size = new System.Drawing.Size(217, 24);
            this.material_typecomboBox.TabIndex = 6;
            this.material_typecomboBox.SelectedIndexChanged += new System.EventHandler(this.material_typecomboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(927, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "收货商名称";
            // 
            // receivertextBox
            // 
            this.receivertextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receivertextBox.Location = new System.Drawing.Point(1135, 7);
            this.receivertextBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivertextBox.Name = "receivertextBox";
            this.receivertextBox.Size = new System.Drawing.Size(220, 26);
            this.receivertextBox.TabIndex = 17;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(927, 44);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 16);
            this.label20.TabIndex = 15;
            this.label20.Text = "备注";
            // 
            // notetextBox
            // 
            this.notetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notetextBox.Location = new System.Drawing.Point(1135, 48);
            this.notetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.notetextBox.Name = "notetextBox";
            this.notetextBox.Size = new System.Drawing.Size(220, 26);
            this.notetextBox.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(927, 85);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(1135, 89);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(220, 26);
            this.idTextBox.TabIndex = 18;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(927, 126);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 14;
            this.label15.Text = "输入人";
            // 
            // inputerTextBox
            // 
            this.inputerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputerTextBox.Location = new System.Drawing.Point(1135, 130);
            this.inputerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputerTextBox.Name = "inputerTextBox";
            this.inputerTextBox.ReadOnly = true;
            this.inputerTextBox.Size = new System.Drawing.Size(220, 26);
            this.inputerTextBox.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(927, 167);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 16);
            this.label16.TabIndex = 13;
            this.label16.Text = "日期";
            // 
            // input_dateTextBox
            // 
            this.input_dateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input_dateTextBox.Enabled = false;
            this.input_dateTextBox.Location = new System.Drawing.Point(1135, 171);
            this.input_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.input_dateTextBox.Name = "input_dateTextBox";
            this.input_dateTextBox.ReadOnly = true;
            this.input_dateTextBox.Size = new System.Drawing.Size(220, 26);
            this.input_dateTextBox.TabIndex = 23;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(459, 167);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 16);
            this.label19.TabIndex = 10;
            this.label19.Text = "出库原因";
            // 
            // outreasontextBox
            // 
            this.outreasontextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outreasontextBox.Location = new System.Drawing.Point(685, 171);
            this.outreasontextBox.Margin = new System.Windows.Forms.Padding(4);
            this.outreasontextBox.Name = "outreasontextBox";
            this.outreasontextBox.Size = new System.Drawing.Size(231, 26);
            this.outreasontextBox.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 126);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "数量";
            // 
            // numberTextBox
            // 
            this.numberTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numberTextBox.Location = new System.Drawing.Point(685, 130);
            this.numberTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(231, 26);
            this.numberTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(459, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态";
            // 
            // statuscomboBox
            // 
            this.statuscomboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "MB",
            "BGA",
            "FRU",
            "SMT"});
            this.statuscomboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statuscomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statuscomboBox.FormattingEnabled = true;
            this.statuscomboBox.Items.AddRange(new object[] {
            "良品",
            "不良品"});
            this.statuscomboBox.Location = new System.Drawing.Point(684, 88);
            this.statuscomboBox.Name = "statuscomboBox";
            this.statuscomboBox.Size = new System.Drawing.Size(233, 24);
            this.statuscomboBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(459, 44);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "描述";
            // 
            // describeTextBox
            // 
            this.describeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.describeTextBox.Location = new System.Drawing.Point(685, 48);
            this.describeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.describeTextBox.Multiline = true;
            this.describeTextBox.Name = "describeTextBox";
            this.describeTextBox.ReadOnly = true;
            this.describeTextBox.Size = new System.Drawing.Size(231, 30);
            this.describeTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(459, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "简述";
            // 
            // otherbriefTextBox
            // 
            this.otherbriefTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.otherbriefTextBox.Location = new System.Drawing.Point(685, 7);
            this.otherbriefTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.otherbriefTextBox.Name = "otherbriefTextBox";
            this.otherbriefTextBox.ReadOnly = true;
            this.otherbriefTextBox.Size = new System.Drawing.Size(231, 26);
            this.otherbriefTextBox.TabIndex = 5;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 126);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 16);
            this.label21.TabIndex = 4;
            this.label21.Text = "BGA类型";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 85);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "MPN";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.VGA);
            this.panel1.Controls.Add(this.PCH);
            this.panel1.Controls.Add(this.CPU);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(232, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 32);
            this.panel1.TabIndex = 27;
            // 
            // VGA
            // 
            this.VGA.AutoSize = true;
            this.VGA.Location = new System.Drawing.Point(107, 7);
            this.VGA.Name = "VGA";
            this.VGA.Size = new System.Drawing.Size(50, 20);
            this.VGA.TabIndex = 0;
            this.VGA.TabStop = true;
            this.VGA.Text = "VGA";
            this.VGA.UseVisualStyleBackColor = true;
            this.VGA.CheckedChanged += new System.EventHandler(this.CPU_CheckedChanged);
            // 
            // PCH
            // 
            this.PCH.AutoSize = true;
            this.PCH.Location = new System.Drawing.Point(55, 7);
            this.PCH.Name = "PCH";
            this.PCH.Size = new System.Drawing.Size(50, 20);
            this.PCH.TabIndex = 0;
            this.PCH.TabStop = true;
            this.PCH.Text = "PCH";
            this.PCH.UseVisualStyleBackColor = true;
            this.PCH.CheckedChanged += new System.EventHandler(this.CPU_CheckedChanged);
            // 
            // CPU
            // 
            this.CPU.AutoSize = true;
            this.CPU.Location = new System.Drawing.Point(3, 7);
            this.CPU.Name = "CPU";
            this.CPU.Size = new System.Drawing.Size(50, 20);
            this.CPU.TabIndex = 0;
            this.CPU.TabStop = true;
            this.CPU.Text = "CPU";
            this.CPU.UseVisualStyleBackColor = true;
            this.CPU.CheckedChanged += new System.EventHandler(this.CPU_CheckedChanged);
            // 
            // mpnTextBox
            // 
            this.mpnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpnTextBox.Location = new System.Drawing.Point(233, 89);
            this.mpnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpnTextBox.Name = "mpnTextBox";
            this.mpnTextBox.Size = new System.Drawing.Size(215, 26);
            this.mpnTextBox.TabIndex = 5;
            this.mpnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mpnTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(7, 208);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 32);
            this.label7.TabIndex = 4;
            this.label7.Text = "出库原因为RTV的备注栏需填写RMA号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(233, 208);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "报废转卖的需填写核示号";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.add, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.query, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.modify, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.delete, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 271);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1362, 44);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.91496F));
            this.tableLayoutPanel4.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 322);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1364, 424);
            this.tableLayoutPanel4.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1356, 416);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // TransferOrSoldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TransferOrSoldForm";
            this.Text = "报废转卖";
            this.Load += new System.EventHandler(this.ReceiveOrderForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox vendorComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mb_briefTextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton VGA;
        private System.Windows.Forms.RadioButton PCH;
        private System.Windows.Forms.RadioButton CPU;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox material_typecomboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox mpnTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox receivertextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox notetextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox inputerTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox input_dateTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox outreasontextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox statuscomboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox describeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox otherbriefTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}