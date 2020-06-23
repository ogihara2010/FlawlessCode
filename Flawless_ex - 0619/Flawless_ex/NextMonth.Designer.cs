namespace Flawless_ex
{
    partial class NextMonth
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
            this.return4 = new System.Windows.Forms.Button();
            this.choice2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(177, 130);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1326, 988);
            this.listBox1.TabIndex = 0;
            // 
            // return4
            // 
            this.return4.Location = new System.Drawing.Point(158, 1141);
            this.return4.Name = "return4";
            this.return4.Size = new System.Drawing.Size(193, 102);
            this.return4.TabIndex = 1;
            this.return4.Text = "戻る";
            this.return4.UseVisualStyleBackColor = true;
            this.return4.Click += new System.EventHandler(this.Return4_Click);
            // 
            // choice2
            // 
            this.choice2.Location = new System.Drawing.Point(1376, 1141);
            this.choice2.Name = "choice2";
            this.choice2.Size = new System.Drawing.Size(227, 102);
            this.choice2.TabIndex = 2;
            this.choice2.Text = "選択";
            this.choice2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label1.Location = new System.Drawing.Point(576, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "未入力・次月持ち越し一覧";
            // 
            // NextMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1691, 1306);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.choice2);
            this.Controls.Add(this.return4);
            this.Controls.Add(this.listBox1);
            this.Name = "NextMonth";
            this.Text = "未入力・次月持ち越し一覧";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button return4;
        private System.Windows.Forms.Button choice2;
        private System.Windows.Forms.Label label1;
    }
}