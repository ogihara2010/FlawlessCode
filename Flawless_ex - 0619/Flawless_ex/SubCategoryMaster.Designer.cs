namespace Flawless_ex
{
    partial class SubCategoryMaster
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
            this.returnButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(798, 274);
            this.dataGridView1.TabIndex = 0;
            // 
            // returnButton
            // 
            this.returnButton.AutoSize = true;
            this.returnButton.Location = new System.Drawing.Point(12, 423);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(141, 45);
            this.returnButton.TabIndex = 1;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(422, 423);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 45);
            this.button2.TabIndex = 2;
            this.button2.Text = "変更・削除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // addButton
            // 
            this.addButton.AutoSize = true;
            this.addButton.Location = new System.Drawing.Point(646, 423);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(141, 45);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "新規登録";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // SubCategoryMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 522);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SubCategoryMaster";
            this.Text = "小分類マスタメンテナンス";
            this.Load += new System.EventHandler(this.SubCategoryMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button addButton;
    }
}