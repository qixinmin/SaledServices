namespace SaledServices.Repair
{
    partial class RrepareUseListForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.choosebutton = new System.Windows.Forms.Button();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.material_mpntextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.realNumbertextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.thisNumbertextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.stock_placetextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 185);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(722, 365);
            this.dataGridView1.TabIndex = 0;
            // 
            // choosebutton
            // 
            this.choosebutton.Location = new System.Drawing.Point(763, 185);
            this.choosebutton.Name = "choosebutton";
            this.choosebutton.Size = new System.Drawing.Size(75, 23);
            this.choosebutton.TabIndex = 1;
            this.choosebutton.Text = "使用";
            this.choosebutton.UseVisualStyleBackColor = true;
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(763, 241);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(75, 23);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "刷新";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "机型";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(65, 59);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_brieftextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "物料MPN";
            // 
            // material_mpntextBox
            // 
            this.material_mpntextBox.Location = new System.Drawing.Point(263, 59);
            this.material_mpntextBox.Name = "material_mpntextBox";
            this.material_mpntextBox.Size = new System.Drawing.Size(100, 21);
            this.material_mpntextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "已有数量";
            // 
            // realNumbertextBox
            // 
            this.realNumbertextBox.Location = new System.Drawing.Point(439, 59);
            this.realNumbertextBox.Name = "realNumbertextBox";
            this.realNumbertextBox.ReadOnly = true;
            this.realNumbertextBox.Size = new System.Drawing.Size(100, 21);
            this.realNumbertextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(571, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "本次使用数量";
            // 
            // thisNumbertextBox
            // 
            this.thisNumbertextBox.Location = new System.Drawing.Point(654, 62);
            this.thisNumbertextBox.Name = "thisNumbertextBox";
            this.thisNumbertextBox.Size = new System.Drawing.Size(100, 21);
            this.thisNumbertextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "库位";
            // 
            // stock_placetextBox
            // 
            this.stock_placetextBox.Location = new System.Drawing.Point(65, 128);
            this.stock_placetextBox.Name = "stock_placetextBox";
            this.stock_placetextBox.ReadOnly = true;
            this.stock_placetextBox.Size = new System.Drawing.Size(100, 21);
            this.stock_placetextBox.TabIndex = 3;
            // 
            // RrepareUseListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 637);
            this.Controls.Add(this.thisNumbertextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stock_placetextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.realNumbertextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.material_mpntextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mb_brieftextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.choosebutton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "RrepareUseListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预领料列表";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button choosebutton;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox material_mpntextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox realNumbertextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox thisNumbertextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox stock_placetextBox;
    }
}