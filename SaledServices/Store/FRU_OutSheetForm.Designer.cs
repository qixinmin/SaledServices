﻿namespace SaledServices
{
    partial class FRU_OutSheetForm
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
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.vendormaterialNoTextBox = new System.Windows.Forms.TextBox();
            this.mpnTextBox = new System.Windows.Forms.TextBox();
            this.material_typeTextBox = new System.Windows.Forms.TextBox();
            this.buy_typeTextBox = new System.Windows.Forms.TextBox();
            this.describeTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.stock_out_numTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.stock_placetextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.takertextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.inputerTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.input_dateTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.notetextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.use_describetextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.currentStockNumbertextBox = new System.Windows.Forms.TextBox();
            this.vendorcomboBox = new System.Windows.Forms.ComboBox();
            this.productcomboBox = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.material_nameTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pricePerTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.queryStock = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(419, 9);
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
            this.query.Location = new System.Drawing.Point(704, 9);
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
            this.modify.Location = new System.Drawing.Point(950, 9);
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
            this.delete.Location = new System.Drawing.Point(1183, 9);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 31);
            this.delete.TabIndex = 3;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "厂商";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "客户别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "厂商料号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(345, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "材料类别";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(345, 144);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "描述";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(345, 50);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "采购类别";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 191);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "MPN";
            // 
            // vendormaterialNoTextBox
            // 
            this.vendormaterialNoTextBox.Location = new System.Drawing.Point(514, 101);
            this.vendormaterialNoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendormaterialNoTextBox.Name = "vendormaterialNoTextBox";
            this.vendormaterialNoTextBox.ReadOnly = true;
            this.vendormaterialNoTextBox.Size = new System.Drawing.Size(158, 26);
            this.vendormaterialNoTextBox.TabIndex = 5;
            // 
            // mpnTextBox
            // 
            this.mpnTextBox.Location = new System.Drawing.Point(176, 195);
            this.mpnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpnTextBox.Name = "mpnTextBox";
            this.mpnTextBox.ReadOnly = true;
            this.mpnTextBox.Size = new System.Drawing.Size(158, 26);
            this.mpnTextBox.TabIndex = 5;
            this.mpnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mpnTextBox_KeyPress);
            // 
            // material_typeTextBox
            // 
            this.material_typeTextBox.Location = new System.Drawing.Point(514, 7);
            this.material_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.material_typeTextBox.Name = "material_typeTextBox";
            this.material_typeTextBox.ReadOnly = true;
            this.material_typeTextBox.Size = new System.Drawing.Size(158, 26);
            this.material_typeTextBox.TabIndex = 5;
            // 
            // buy_typeTextBox
            // 
            this.buy_typeTextBox.Location = new System.Drawing.Point(514, 54);
            this.buy_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.buy_typeTextBox.Name = "buy_typeTextBox";
            this.buy_typeTextBox.ReadOnly = true;
            this.buy_typeTextBox.Size = new System.Drawing.Size(158, 26);
            this.buy_typeTextBox.TabIndex = 5;
            // 
            // describeTextBox
            // 
            this.describeTextBox.Location = new System.Drawing.Point(514, 148);
            this.describeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.describeTextBox.Name = "describeTextBox";
            this.describeTextBox.ReadOnly = true;
            this.describeTextBox.Size = new System.Drawing.Size(158, 26);
            this.describeTextBox.TabIndex = 5;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.700312F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.00104F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 749);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.mpnTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.vendormaterialNoTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.describeTextBox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.label13, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.stock_out_numTextBox, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label19, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.stock_placetextBox, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.label8, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.takertextBox, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.label15, 4, 4);
            this.tableLayoutPanel3.Controls.Add(this.inputerTextBox, 5, 4);
            this.tableLayoutPanel3.Controls.Add(this.label16, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.input_dateTextBox, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.label14, 6, 3);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 7, 3);
            this.tableLayoutPanel3.Controls.Add(this.label20, 6, 2);
            this.tableLayoutPanel3.Controls.Add(this.notetextBox, 7, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.use_describetextBox, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 6, 4);
            this.tableLayoutPanel3.Controls.Add(this.currentStockNumbertextBox, 7, 4);
            this.tableLayoutPanel3.Controls.Add(this.vendorcomboBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.productcomboBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.mb_brieftextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.material_typeTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.material_nameTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.buy_typeTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.pricePerTextBox, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.button1, 2, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1362, 241);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(683, 3);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 8;
            this.label13.Text = "出库数量";
            // 
            // stock_out_numTextBox
            // 
            this.stock_out_numTextBox.Location = new System.Drawing.Point(852, 7);
            this.stock_out_numTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.stock_out_numTextBox.Name = "stock_out_numTextBox";
            this.stock_out_numTextBox.Size = new System.Drawing.Size(158, 26);
            this.stock_out_numTextBox.TabIndex = 20;
            this.stock_out_numTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stock_out_numTextBox_KeyPress);
            this.stock_out_numTextBox.Leave += new System.EventHandler(this.stock_out_numTextBox_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(683, 97);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 16);
            this.label19.TabIndex = 10;
            this.label19.Text = "库位";
            // 
            // stock_placetextBox
            // 
            this.stock_placetextBox.Location = new System.Drawing.Point(852, 101);
            this.stock_placetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.stock_placetextBox.Name = "stock_placetextBox";
            this.stock_placetextBox.ReadOnly = true;
            this.stock_placetextBox.Size = new System.Drawing.Size(158, 26);
            this.stock_placetextBox.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(683, 144);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "领用人";
            // 
            // takertextBox
            // 
            this.takertextBox.Location = new System.Drawing.Point(852, 148);
            this.takertextBox.Margin = new System.Windows.Forms.Padding(4);
            this.takertextBox.Name = "takertextBox";
            this.takertextBox.Size = new System.Drawing.Size(158, 26);
            this.takertextBox.TabIndex = 22;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(683, 191);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 14;
            this.label15.Text = "出库人";
            // 
            // inputerTextBox
            // 
            this.inputerTextBox.Location = new System.Drawing.Point(852, 195);
            this.inputerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputerTextBox.Name = "inputerTextBox";
            this.inputerTextBox.ReadOnly = true;
            this.inputerTextBox.Size = new System.Drawing.Size(158, 26);
            this.inputerTextBox.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1021, 3);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 16);
            this.label16.TabIndex = 13;
            this.label16.Text = "出库日期";
            // 
            // input_dateTextBox
            // 
            this.input_dateTextBox.Enabled = false;
            this.input_dateTextBox.Location = new System.Drawing.Point(1190, 7);
            this.input_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.input_dateTextBox.Name = "input_dateTextBox";
            this.input_dateTextBox.ReadOnly = true;
            this.input_dateTextBox.Size = new System.Drawing.Size(158, 26);
            this.input_dateTextBox.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1021, 144);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(1190, 148);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(165, 26);
            this.idTextBox.TabIndex = 18;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1021, 97);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 16);
            this.label20.TabIndex = 15;
            this.label20.Text = "备注";
            // 
            // notetextBox
            // 
            this.notetextBox.Location = new System.Drawing.Point(1190, 101);
            this.notetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.notetextBox.Name = "notetextBox";
            this.notetextBox.Size = new System.Drawing.Size(158, 26);
            this.notetextBox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1021, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "用途";
            // 
            // use_describetextBox
            // 
            this.use_describetextBox.Location = new System.Drawing.Point(1190, 54);
            this.use_describetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.use_describetextBox.Name = "use_describetextBox";
            this.use_describetextBox.Size = new System.Drawing.Size(158, 26);
            this.use_describetextBox.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1021, 191);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "库存数量";
            // 
            // currentStockNumbertextBox
            // 
            this.currentStockNumbertextBox.Enabled = false;
            this.currentStockNumbertextBox.Location = new System.Drawing.Point(1190, 195);
            this.currentStockNumbertextBox.Margin = new System.Windows.Forms.Padding(4);
            this.currentStockNumbertextBox.Name = "currentStockNumbertextBox";
            this.currentStockNumbertextBox.ReadOnly = true;
            this.currentStockNumbertextBox.Size = new System.Drawing.Size(165, 26);
            this.currentStockNumbertextBox.TabIndex = 18;
            // 
            // vendorcomboBox
            // 
            this.vendorcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vendorcomboBox.FormattingEnabled = true;
            this.vendorcomboBox.Location = new System.Drawing.Point(175, 6);
            this.vendorcomboBox.Name = "vendorcomboBox";
            this.vendorcomboBox.Size = new System.Drawing.Size(159, 24);
            this.vendorcomboBox.TabIndex = 27;
            this.vendorcomboBox.SelectedIndexChanged += new System.EventHandler(this.vendorcomboBox_SelectedIndexChanged);
            this.vendorcomboBox.SelectedValueChanged += new System.EventHandler(this.vendorcomboBox_SelectedValueChanged);
            // 
            // productcomboBox
            // 
            this.productcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productcomboBox.FormattingEnabled = true;
            this.productcomboBox.Location = new System.Drawing.Point(175, 53);
            this.productcomboBox.Name = "productcomboBox";
            this.productcomboBox.Size = new System.Drawing.Size(160, 24);
            this.productcomboBox.TabIndex = 28;
            this.productcomboBox.SelectedValueChanged += new System.EventHandler(this.queryStock_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 97);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(136, 16);
            this.label18.TabIndex = 11;
            this.label18.Text = "MB简称(模糊查询)";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(176, 101);
            this.mb_brieftextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(158, 26);
            this.mb_brieftextBox.TabIndex = 24;
            this.mb_brieftextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mb_brieftextBox_KeyPress);
            // 
            // material_nameTextBox
            // 
            this.material_nameTextBox.Location = new System.Drawing.Point(176, 148);
            this.material_nameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.material_nameTextBox.Name = "material_nameTextBox";
            this.material_nameTextBox.Size = new System.Drawing.Size(158, 26);
            this.material_nameTextBox.TabIndex = 25;
            this.material_nameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.material_nameTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 144);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "材料名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(683, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "单价";
            // 
            // pricePerTextBox
            // 
            this.pricePerTextBox.Location = new System.Drawing.Point(852, 54);
            this.pricePerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pricePerTextBox.Name = "pricePerTextBox";
            this.pricePerTextBox.ReadOnly = true;
            this.pricePerTextBox.Size = new System.Drawing.Size(158, 26);
            this.pricePerTextBox.TabIndex = 19;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.4849F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.9325F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.85227F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel2.Controls.Add(this.delete, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.modify, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.query, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.add, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.queryStock, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 253);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1362, 49);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // queryStock
            // 
            this.queryStock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.queryStock.Location = new System.Drawing.Point(113, 9);
            this.queryStock.Margin = new System.Windows.Forms.Padding(4);
            this.queryStock.Name = "queryStock";
            this.queryStock.Size = new System.Drawing.Size(100, 31);
            this.queryStock.TabIndex = 0;
            this.queryStock.Text = "查询库存";
            this.queryStock.UseVisualStyleBackColor = true;
            this.queryStock.Click += new System.EventHandler(this.queryStock_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.76246F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.23754F));
            this.tableLayoutPanel4.Controls.Add(this.dataGridView1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.dataGridView2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 309);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1364, 437);
            this.tableLayoutPanel4.TabIndex = 11;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(559, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(801, 429);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(4, 4);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(547, 429);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.VirtualMode = true;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(344, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 41);
            this.button1.TabIndex = 29;
            this.button1.Text = "FRU出库导出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FRU_OutSheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FRU_OutSheetForm";
            this.Text = "FRU出库表";
            this.Load += new System.EventHandler(this.ReceiveOrderForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox vendormaterialNoTextBox;
        private System.Windows.Forms.TextBox mpnTextBox;
        private System.Windows.Forms.TextBox material_typeTextBox;
        private System.Windows.Forms.TextBox buy_typeTextBox;
        private System.Windows.Forms.TextBox describeTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox input_dateTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox inputerTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox material_nameTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox stock_out_numTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox stock_placetextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox notetextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox takertextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox use_describetextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button queryStock;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox currentStockNumbertextBox;
        private System.Windows.Forms.ComboBox vendorcomboBox;
        private System.Windows.Forms.ComboBox productcomboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pricePerTextBox;
        private System.Windows.Forms.Button button1;
    }
}