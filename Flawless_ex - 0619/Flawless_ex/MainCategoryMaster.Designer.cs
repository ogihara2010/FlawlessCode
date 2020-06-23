namespace Flawless_ex
{
    partial class MainCategoryMaster
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
            this.updateButton = new System.Windows.Forms.Button();
            this.addbutton = new System.Windows.Forms.Button();
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
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(648, 307);
            this.dataGridView1.TabIndex = 0;
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(12, 430);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(116, 45);
            this.returnButton.TabIndex = 1;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButtonClick);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(374, 430);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(141, 45);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "変更・削除";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButtonClick);
            // 
            // addbutton
            // 
            this.addbutton.Location = new System.Drawing.Point(544, 430);
            this.addbutton.Name = "addbutton";
            this.addbutton.Size = new System.Drawing.Size(116, 45);
            this.addbutton.TabIndex = 3;
            this.addbutton.Text = "新規登録";
            this.addbutton.UseVisualStyleBackColor = true;
            this.addbutton.Click += new System.EventHandler(this.addButton);
            // 
            // MainCategoryMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 521);
            this.Controls.Add(this.addbutton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainCategoryMaster";
            this.Text = "大分類マスタ";
            this.Load += new System.EventHandler(this.MainCategoryMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button addbutton;
    }
}