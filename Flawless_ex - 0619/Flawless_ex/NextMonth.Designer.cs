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
            this.return4 = new System.Windows.Forms.Button();
            this.choice2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // return4
            // 
            this.return4.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.return4.Location = new System.Drawing.Point(93, 1141);
            this.return4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.return4.Name = "return4";
            this.return4.Size = new System.Drawing.Size(245, 121);
            this.return4.TabIndex = 1;
            this.return4.Text = "戻る";
            this.return4.UseVisualStyleBackColor = true;
            this.return4.Click += new System.EventHandler(this.Return4_Click);
            // 
            // choice2
            // 
            this.choice2.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.choice2.Location = new System.Drawing.Point(1340, 1141);
            this.choice2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.choice2.Name = "choice2";
            this.choice2.Size = new System.Drawing.Size(245, 121);
            this.choice2.TabIndex = 2;
            this.choice2.Text = "選択";
            this.choice2.UseVisualStyleBackColor = true;
            this.choice2.Click += new System.EventHandler(this.Choice2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label1.Location = new System.Drawing.Point(594, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "未入力・次月持ち越し一覧";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(186, 145);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1328, 927);
            this.dataGridView1.TabIndex = 4;
            // 
            // NextMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1691, 1306);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.choice2);
            this.Controls.Add(this.return4);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "NextMonth";
            this.Text = "未入力・次月持ち越し一覧";
            this.Load += new System.EventHandler(this.NextMonth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button return4;
        private System.Windows.Forms.Button choice2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}