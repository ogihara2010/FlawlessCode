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
            this.return4.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.return4.Location = new System.Drawing.Point(26, 547);
            this.return4.Margin = new System.Windows.Forms.Padding(2);
            this.return4.Name = "return4";
            this.return4.Size = new System.Drawing.Size(167, 60);
            this.return4.TabIndex = 1;
            this.return4.Text = "戻る";
            this.return4.UseVisualStyleBackColor = true;
            this.return4.Click += new System.EventHandler(this.Return4_Click);
            // 
            // choice2
            // 
            this.choice2.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.choice2.Location = new System.Drawing.Point(664, 547);
            this.choice2.Margin = new System.Windows.Forms.Padding(2);
            this.choice2.Name = "choice2";
            this.choice2.Size = new System.Drawing.Size(188, 60);
            this.choice2.TabIndex = 2;
            this.choice2.Text = "選択";
            this.choice2.UseVisualStyleBackColor = true;
            this.choice2.Click += new System.EventHandler(this.Choice2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(256, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "未入力・次月持ち越し一覧";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 64);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(826, 464);
            this.dataGridView1.TabIndex = 4;
            // 
            // NextMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(890, 617);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.choice2);
            this.Controls.Add(this.return4);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NextMonth";
            this.Text = "未入力・次月持ち越し一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NextMonth_FormClosed);
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