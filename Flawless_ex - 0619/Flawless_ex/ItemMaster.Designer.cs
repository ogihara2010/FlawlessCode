namespace Flawless_ex
{
    partial class ItemMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.signUpButton = new System.Windows.Forms.Button();
            this.changeDeleteButton = new System.Windows.Forms.Button();
            this.mainCategoryMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(43, 39);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1751, 694);
            this.dataGridView1.TabIndex = 7;
            // 
            // ReturnButton
            // 
            this.ReturnButton.AutoSize = true;
            this.ReturnButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReturnButton.Location = new System.Drawing.Point(937, 829);
            this.ReturnButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(265, 189);
            this.ReturnButton.TabIndex = 4;
            this.ReturnButton.Text = "戻る";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // signUpButton
            // 
            this.signUpButton.AutoSize = true;
            this.signUpButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.signUpButton.Location = new System.Drawing.Point(43, 829);
            this.signUpButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.signUpButton.Name = "signUpButton";
            this.signUpButton.Size = new System.Drawing.Size(338, 189);
            this.signUpButton.TabIndex = 6;
            this.signUpButton.Text = "新規登録";
            this.signUpButton.UseVisualStyleBackColor = true;
            this.signUpButton.Click += new System.EventHandler(this.signUpButton_Click);
            // 
            // changeDeleteButton
            // 
            this.changeDeleteButton.AutoSize = true;
            this.changeDeleteButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeDeleteButton.Location = new System.Drawing.Point(446, 829);
            this.changeDeleteButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.changeDeleteButton.Name = "changeDeleteButton";
            this.changeDeleteButton.Size = new System.Drawing.Size(411, 189);
            this.changeDeleteButton.TabIndex = 5;
            this.changeDeleteButton.Text = "変更・無効";
            this.changeDeleteButton.UseVisualStyleBackColor = true;
            this.changeDeleteButton.Click += new System.EventHandler(this.changeDeleteButton_Click);
            // 
            // mainCategoryMenu
            // 
            this.mainCategoryMenu.AutoSize = true;
            this.mainCategoryMenu.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainCategoryMenu.Location = new System.Drawing.Point(1326, 832);
            this.mainCategoryMenu.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mainCategoryMenu.Name = "mainCategoryMenu";
            this.mainCategoryMenu.Size = new System.Drawing.Size(468, 189);
            this.mainCategoryMenu.TabIndex = 8;
            this.mainCategoryMenu.Text = "大分類マスタ一覧";
            this.mainCategoryMenu.UseVisualStyleBackColor = true;
            this.mainCategoryMenu.Click += new System.EventHandler(this.mainCategoryMenu_Click);
            // 
            // ItemMaster
            // 
            this.AcceptButton = this.ReturnButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1857, 1054);
            this.Controls.Add(this.mainCategoryMenu);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.signUpButton);
            this.Controls.Add(this.changeDeleteButton);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "ItemMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "品名マスタメンテナンス";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ItemMaster_FormClosed);
            this.Load += new System.EventHandler(this.ProductNameMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button signUpButton;
        private System.Windows.Forms.Button changeDeleteButton;
        private System.Windows.Forms.Button mainCategoryMenu;
    }
}