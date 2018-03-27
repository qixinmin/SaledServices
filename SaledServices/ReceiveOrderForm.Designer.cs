namespace SaledServices
{
    partial class ReceiveOrderForm
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
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.vendorTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.productTextBox = new System.Windows.Forms.TextBox();
            this.ordernoTextBox = new System.Windows.Forms.TextBox();
            this.custom_materialNoTextBox = new System.Windows.Forms.TextBox();
            this.custom_material_describeTextBox = new System.Windows.Forms.TextBox();
            this.ordernumTextBox = new System.Windows.Forms.TextBox();
            this.mb_briefTextBox = new System.Windows.Forms.TextBox();
            this.vendor_materialNoTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.ordertimeTextBox = new System.Windows.Forms.TextBox();
            this.receivedNumTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.receivedateTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(46, 231);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 0;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(176, 231);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(75, 23);
            this.query.TabIndex = 1;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Location = new System.Drawing.Point(346, 231);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(75, 23);
            this.modify.TabIndex = 2;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(500, 231);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 3;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "厂商";
            // 
            // vendorTextBox
            // 
            this.vendorTextBox.Location = new System.Drawing.Point(104, 19);
            this.vendorTextBox.Name = "vendorTextBox";
            this.vendorTextBox.Size = new System.Drawing.Size(100, 21);
            this.vendorTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "客户别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "订单编号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "制单时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "制单人";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "客户物料描述";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "收货数量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(275, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "厂商料号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "MB简称";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(275, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "订单数量";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "客户料号";
            // 
            // productTextBox
            // 
            this.productTextBox.Location = new System.Drawing.Point(104, 50);
            this.productTextBox.Name = "productTextBox";
            this.productTextBox.Size = new System.Drawing.Size(100, 21);
            this.productTextBox.TabIndex = 5;
            // 
            // ordernoTextBox
            // 
            this.ordernoTextBox.Location = new System.Drawing.Point(104, 84);
            this.ordernoTextBox.Name = "ordernoTextBox";
            this.ordernoTextBox.Size = new System.Drawing.Size(100, 21);
            this.ordernoTextBox.TabIndex = 5;
            // 
            // custom_materialNoTextBox
            // 
            this.custom_materialNoTextBox.Location = new System.Drawing.Point(104, 124);
            this.custom_materialNoTextBox.Name = "custom_materialNoTextBox";
            this.custom_materialNoTextBox.Size = new System.Drawing.Size(100, 21);
            this.custom_materialNoTextBox.TabIndex = 5;
            // 
            // custom_material_describeTextBox
            // 
            this.custom_material_describeTextBox.Location = new System.Drawing.Point(104, 165);
            this.custom_material_describeTextBox.Name = "custom_material_describeTextBox";
            this.custom_material_describeTextBox.Size = new System.Drawing.Size(100, 21);
            this.custom_material_describeTextBox.TabIndex = 5;
            // 
            // ordernumTextBox
            // 
            this.ordernumTextBox.Location = new System.Drawing.Point(346, 12);
            this.ordernumTextBox.Name = "ordernumTextBox";
            this.ordernumTextBox.Size = new System.Drawing.Size(100, 21);
            this.ordernumTextBox.TabIndex = 5;
            // 
            // mb_briefTextBox
            // 
            this.mb_briefTextBox.Location = new System.Drawing.Point(346, 50);
            this.mb_briefTextBox.Name = "mb_briefTextBox";
            this.mb_briefTextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_briefTextBox.TabIndex = 5;
            // 
            // vendor_materialNoTextBox
            // 
            this.vendor_materialNoTextBox.Location = new System.Drawing.Point(346, 87);
            this.vendor_materialNoTextBox.Name = "vendor_materialNoTextBox";
            this.vendor_materialNoTextBox.Size = new System.Drawing.Size(100, 21);
            this.vendor_materialNoTextBox.TabIndex = 5;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(346, 121);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(100, 21);
            this.usernameTextBox.TabIndex = 5;
            // 
            // ordertimeTextBox
            // 
            this.ordertimeTextBox.Location = new System.Drawing.Point(346, 165);
            this.ordertimeTextBox.Name = "ordertimeTextBox";
            this.ordertimeTextBox.Size = new System.Drawing.Size(100, 21);
            this.ordertimeTextBox.TabIndex = 5;
            // 
            // receivedNumTextBox
            // 
            this.receivedNumTextBox.Location = new System.Drawing.Point(547, 12);
            this.receivedNumTextBox.Name = "receivedNumTextBox";
            this.receivedNumTextBox.Size = new System.Drawing.Size(100, 21);
            this.receivedNumTextBox.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(484, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "收货日期";
            // 
            // receivedateTextBox
            // 
            this.receivedateTextBox.Location = new System.Drawing.Point(547, 44);
            this.receivedateTextBox.Name = "receivedateTextBox";
            this.receivedateTextBox.Size = new System.Drawing.Size(100, 21);
            this.receivedateTextBox.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(484, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "订单状态";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(547, 78);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(100, 21);
            this.statusTextBox.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(36, 296);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(644, 209);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(484, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(547, 112);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 21);
            this.idTextBox.TabIndex = 5;
            // 
            // ReceiveOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 517);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.custom_material_describeTextBox);
            this.Controls.Add(this.custom_materialNoTextBox);
            this.Controls.Add(this.ordernoTextBox);
            this.Controls.Add(this.productTextBox);
            this.Controls.Add(this.ordertimeTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.vendor_materialNoTextBox);
            this.Controls.Add(this.mb_briefTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.receivedateTextBox);
            this.Controls.Add(this.receivedNumTextBox);
            this.Controls.Add(this.ordernumTextBox);
            this.Controls.Add(this.vendorTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.add);
            this.Name = "ReceiveOrderForm";
            this.Text = "收还货";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox vendorTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox productTextBox;
        private System.Windows.Forms.TextBox ordernoTextBox;
        private System.Windows.Forms.TextBox custom_materialNoTextBox;
        private System.Windows.Forms.TextBox custom_material_describeTextBox;
        private System.Windows.Forms.TextBox ordernumTextBox;
        private System.Windows.Forms.TextBox mb_briefTextBox;
        private System.Windows.Forms.TextBox vendor_materialNoTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox ordertimeTextBox;
        private System.Windows.Forms.TextBox receivedNumTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox receivedateTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox idTextBox;
    }
}