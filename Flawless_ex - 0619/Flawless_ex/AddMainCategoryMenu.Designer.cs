namespace Flawless_ex
{
    partial class AddMainCategoryMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mainCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.mainCategoryNameTextBox = new System.Windows.Forms.TextBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 208);
            this.label1.TabIndex = 0;
            this.label1.Text = "大分類コード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(2, 208);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(518, 209);
            this.label2.TabIndex = 1;
            this.label2.Text = "大分類名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainCategoryCodeTextBox
            // 
            this.mainCategoryCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainCategoryCodeTextBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainCategoryCodeTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mainCategoryCodeTextBox.Location = new System.Drawing.Point(524, 4);
            this.mainCategoryCodeTextBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mainCategoryCodeTextBox.Multiline = true;
            this.mainCategoryCodeTextBox.Name = "mainCategoryCodeTextBox";
            this.mainCategoryCodeTextBox.ReadOnly = true;
            this.mainCategoryCodeTextBox.Size = new System.Drawing.Size(1217, 200);
            this.mainCategoryCodeTextBox.TabIndex = 4;
            this.mainCategoryCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainCategoryNameTextBox
            // 
            this.mainCategoryNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainCategoryNameTextBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainCategoryNameTextBox.Location = new System.Drawing.Point(524, 212);
            this.mainCategoryNameTextBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mainCategoryNameTextBox.Multiline = true;
            this.mainCategoryNameTextBox.Name = "mainCategoryNameTextBox";
            this.mainCategoryNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mainCategoryNameTextBox.Size = new System.Drawing.Size(1217, 201);
            this.mainCategoryNameTextBox.TabIndex = 5;
            // 
            // returnButton
            // 
            this.returnButton.BackColor = System.Drawing.Color.LightCyan;
            this.returnButton.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.returnButton.Location = new System.Drawing.Point(55, 533);
            this.returnButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(519, 210);
            this.returnButton.TabIndex = 6;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.LightCyan;
            this.addButton.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addButton.Location = new System.Drawing.Point(1276, 533);
            this.addButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(522, 210);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "登録";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mainCategoryCodeTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mainCategoryNameTextBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(55, 56);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1743, 417);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // AddMainCategoryMenu
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1868, 790);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.returnButton);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "AddMainCategoryMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "大分類マスタメンテナンス　新規登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddMainCategoryMenu_FormClosed);
            this.Load += new System.EventHandler(this.AddMainCategoryMenu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mainCategoryCodeTextBox;
        private System.Windows.Forms.TextBox mainCategoryNameTextBox;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}