namespace SaledServices.CustomsExport
{
    partial class OpeningStockForm
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
            this.exportxmlbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exportxmlbutton
            // 
            this.exportxmlbutton.Location = new System.Drawing.Point(244, 171);
            this.exportxmlbutton.Name = "exportxmlbutton";
            this.exportxmlbutton.Size = new System.Drawing.Size(75, 23);
            this.exportxmlbutton.TabIndex = 0;
            this.exportxmlbutton.Text = "导出";
            this.exportxmlbutton.UseVisualStyleBackColor = true;
            this.exportxmlbutton.Click += new System.EventHandler(this.exportxmlbutton_Click);
            // 
            // OpeningStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 327);
            this.Controls.Add(this.exportxmlbutton);
            this.Name = "OpeningStockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "期初库存";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exportxmlbutton;
    }
}