namespace Flawless_ex
{
    partial class Invoice
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.return5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(276, 258);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1185, 724);
            this.listBox1.TabIndex = 0;
            // 
            // return5
            // 
            this.return5.Location = new System.Drawing.Point(1562, 1006);
            this.return5.Name = "return5";
            this.return5.Size = new System.Drawing.Size(120, 68);
            this.return5.TabIndex = 1;
            this.return5.Text = "戻る";
            this.return5.UseVisualStyleBackColor = true;
            this.return5.Click += new System.EventHandler(this.Return5_Click);
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1736, 1358);
            this.Controls.Add(this.return5);
            this.Controls.Add(this.listBox1);
            this.Name = "Invoice";
            this.Text = "インボイス";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button return5;
    }
}