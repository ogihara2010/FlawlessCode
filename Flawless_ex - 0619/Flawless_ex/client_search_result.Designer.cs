namespace Flawless_ex
{
    partial class client_search_result
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
            this.label1 = new System.Windows.Forms.Label();
            this.selectionButton = new System.Windows.Forms.Button();
            this.returnButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 141);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1625, 599);
            this.dataGridView1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label1.Location = new System.Drawing.Point(664, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 38);
            this.label1.TabIndex = 7;
            this.label1.Text = "検索結果一覧";
            // 
            // selectionButton
            // 
            this.selectionButton.Location = new System.Drawing.Point(1425, 985);
            this.selectionButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.selectionButton.Name = "selectionButton";
            this.selectionButton.Size = new System.Drawing.Size(212, 114);
            this.selectionButton.TabIndex = 6;
            this.selectionButton.Text = "選択";
            this.selectionButton.UseVisualStyleBackColor = true;
            this.selectionButton.Click += new System.EventHandler(this.selectionButton_Click);
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(13, 985);
            this.returnButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(209, 114);
            this.returnButton.TabIndex = 5;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // client_search_result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1680, 1128);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectionButton);
            this.Controls.Add(this.returnButton);
            this.Name = "client_search_result";
            this.Text = "顧客選択メニュー　検索結果一覧";
            this.Load += new System.EventHandler(this.client_search_result_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectionButton;
        private System.Windows.Forms.Button returnButton;
    }
}