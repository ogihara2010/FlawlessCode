namespace Flawless_ex
{
    partial class ProductAddMenu
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
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mainCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.itemCodeTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 27.9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(614, 193);
            this.label1.TabIndex = 0;
            this.label1.Text = "品名コード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 27.9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(2, 193);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(614, 193);
            this.label2.TabIndex = 1;
            this.label2.Text = "品名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productNameTextBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.productNameTextBox.Location = new System.Drawing.Point(620, 198);
            this.productNameTextBox.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.productNameTextBox.Multiline = true;
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.productNameTextBox.Size = new System.Drawing.Size(1438, 183);
            this.productNameTextBox.TabIndex = 5;
            // 
            // returnButton
            // 
            this.returnButton.AutoSize = true;
            this.returnButton.BackColor = System.Drawing.Color.LightCyan;
            this.returnButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.returnButton.Location = new System.Drawing.Point(50, 673);
            this.returnButton.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(515, 213);
            this.returnButton.TabIndex = 6;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // addButton
            // 
            this.addButton.AutoSize = true;
            this.addButton.BackColor = System.Drawing.Color.LightCyan;
            this.addButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addButton.Location = new System.Drawing.Point(1605, 673);
            this.addButton.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(505, 213);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "登録";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 27.9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(2, 386);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(614, 195);
            this.label5.TabIndex = 9;
            this.label5.Text = "大分類名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainCategoryComboBox
            // 
            this.mainCategoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainCategoryComboBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainCategoryComboBox.FormattingEnabled = true;
            this.mainCategoryComboBox.Location = new System.Drawing.Point(620, 391);
            this.mainCategoryComboBox.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.mainCategoryComboBox.Name = "mainCategoryComboBox";
            this.mainCategoryComboBox.Size = new System.Drawing.Size(1438, 128);
            this.mainCategoryComboBox.TabIndex = 10;
            // 
            // itemCodeTextBox
            // 
            this.itemCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemCodeTextBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.itemCodeTextBox.Location = new System.Drawing.Point(620, 5);
            this.itemCodeTextBox.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.itemCodeTextBox.Multiline = true;
            this.itemCodeTextBox.Name = "itemCodeTextBox";
            this.itemCodeTextBox.ReadOnly = true;
            this.itemCodeTextBox.Size = new System.Drawing.Size(1438, 183);
            this.itemCodeTextBox.TabIndex = 12;
            this.itemCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.itemCodeTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mainCategoryComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.productNameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(50, 30);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2060, 581);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // ProductAddMenu
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(2176, 942);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.addButton);
            this.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.Name = "ProductAddMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "品名マスタメンテナンス　新規登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductAddMenu_FormClosed);
            this.Load += new System.EventHandler(this.ProductAddMenu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mainCategoryComboBox;
        private System.Windows.Forms.TextBox itemCodeTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}