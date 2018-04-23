namespace SaledServices.Store
{
    partial class CheckRequestForm
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
            this.refreshbutton = new System.Windows.Forms.Button();
            this.processRequestbutton = new System.Windows.Forms.Button();
            this.track_serial_notextBox = new System.Windows.Forms.TextBox();
            this.material_71pntextBox = new System.Windows.Forms.TextBox();
            this.request_typetextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statustextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.material_mpntextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(368, 219);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(511, 319);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(75, 23);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "刷新请求";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // processRequestbutton
            // 
            this.processRequestbutton.Location = new System.Drawing.Point(511, 164);
            this.processRequestbutton.Name = "processRequestbutton";
            this.processRequestbutton.Size = new System.Drawing.Size(75, 23);
            this.processRequestbutton.TabIndex = 1;
            this.processRequestbutton.Text = "处理请求";
            this.processRequestbutton.UseVisualStyleBackColor = true;
            this.processRequestbutton.Click += new System.EventHandler(this.processRequestbutton_Click);
            // 
            // track_serial_notextBox
            // 
            this.track_serial_notextBox.Location = new System.Drawing.Point(102, 27);
            this.track_serial_notextBox.Name = "track_serial_notextBox";
            this.track_serial_notextBox.ReadOnly = true;
            this.track_serial_notextBox.Size = new System.Drawing.Size(100, 21);
            this.track_serial_notextBox.TabIndex = 13;
            // 
            // material_71pntextBox
            // 
            this.material_71pntextBox.Location = new System.Drawing.Point(102, 84);
            this.material_71pntextBox.Name = "material_71pntextBox";
            this.material_71pntextBox.ReadOnly = true;
            this.material_71pntextBox.Size = new System.Drawing.Size(100, 21);
            this.material_71pntextBox.TabIndex = 11;
            // 
            // request_typetextBox
            // 
            this.request_typetextBox.Location = new System.Drawing.Point(299, 30);
            this.request_typetextBox.Name = "request_typetextBox";
            this.request_typetextBox.ReadOnly = true;
            this.request_typetextBox.Size = new System.Drawing.Size(100, 21);
            this.request_typetextBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "材料71PN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "跟踪条码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "请求类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "状态";
            // 
            // statustextBox
            // 
            this.statustextBox.Location = new System.Drawing.Point(486, 87);
            this.statustextBox.Name = "statustextBox";
            this.statustextBox.ReadOnly = true;
            this.statustextBox.Size = new System.Drawing.Size(100, 21);
            this.statustextBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "材料MPN";
            // 
            // material_mpntextBox
            // 
            this.material_mpntextBox.Location = new System.Drawing.Point(486, 31);
            this.material_mpntextBox.Name = "material_mpntextBox";
            this.material_mpntextBox.ReadOnly = true;
            this.material_mpntextBox.Size = new System.Drawing.Size(100, 21);
            this.material_mpntextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "数量";
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(299, 84);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.ReadOnly = true;
            this.numberTextBox.Size = new System.Drawing.Size(100, 21);
            this.numberTextBox.TabIndex = 11;
            // 
            // CheckRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 413);
            this.Controls.Add(this.track_serial_notextBox);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.statustextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.material_71pntextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.material_mpntextBox);
            this.Controls.Add(this.request_typetextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processRequestbutton);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CheckRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处理请求界面";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Button processRequestbutton;
        private System.Windows.Forms.TextBox track_serial_notextBox;
        private System.Windows.Forms.TextBox material_71pntextBox;
        private System.Windows.Forms.TextBox request_typetextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox statustextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox material_mpntextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox numberTextBox;
    }
}