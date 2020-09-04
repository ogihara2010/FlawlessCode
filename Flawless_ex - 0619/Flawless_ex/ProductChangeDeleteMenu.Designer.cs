namespace Flawless_ex
{
    partial class ProductChangeDeleteMenu
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
            this.productCodeTextBox = new System.Windows.Forms.TextBox();
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.returnButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.mainCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.reasonText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "品名コード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(1, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 76);
            this.label2.TabIndex = 1;
            this.label2.Text = "品名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // productCodeTextBox
            // 
            this.productCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productCodeTextBox.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.productCodeTextBox.Location = new System.Drawing.Point(280, 2);
            this.productCodeTextBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.productCodeTextBox.Multiline = true;
            this.productCodeTextBox.Name = "productCodeTextBox";
            this.productCodeTextBox.ReadOnly = true;
            this.productCodeTextBox.Size = new System.Drawing.Size(649, 72);
            this.productCodeTextBox.TabIndex = 6;
            this.productCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productNameTextBox.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.productNameTextBox.Location = new System.Drawing.Point(280, 78);
            this.productNameTextBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.productNameTextBox.Multiline = true;
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.Size = new System.Drawing.Size(649, 72);
            this.productNameTextBox.TabIndex = 7;
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.LightCyan;
            this.updateButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.updateButton.Location = new System.Drawing.Point(725, 358);
            this.updateButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(237, 121);
            this.updateButton.TabIndex = 16;
            this.updateButton.Text = "変更";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.Color.LightCyan;
            this.removeButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.removeButton.Location = new System.Drawing.Point(356, 358);
            this.removeButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(250, 121);
            this.removeButton.TabIndex = 15;
            this.removeButton.Text = "削除";
            this.removeButton.UseVisualStyleBackColor = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // returnButton
            // 
            this.returnButton.BackColor = System.Drawing.Color.LightCyan;
            this.returnButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.returnButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.returnButton.Location = new System.Drawing.Point(32, 358);
            this.returnButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(204, 121);
            this.returnButton.TabIndex = 14;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(1, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 76);
            this.label4.TabIndex = 17;
            this.label4.Text = "大分類名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainCategoryComboBox
            // 
            this.mainCategoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainCategoryComboBox.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainCategoryComboBox.FormattingEnabled = true;
            this.mainCategoryComboBox.Location = new System.Drawing.Point(280, 154);
            this.mainCategoryComboBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.mainCategoryComboBox.Name = "mainCategoryComboBox";
            this.mainCategoryComboBox.Size = new System.Drawing.Size(649, 51);
            this.mainCategoryComboBox.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(2, 228);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(275, 78);
            this.label7.TabIndex = 20;
            this.label7.Text = "変更理由";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reasonText
            // 
            this.reasonText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reasonText.Font = new System.Drawing.Font("MS UI Gothic", 26.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.reasonText.Location = new System.Drawing.Point(281, 230);
            this.reasonText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reasonText.Multiline = true;
            this.reasonText.Name = "reasonText";
            this.reasonText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.reasonText.Size = new System.Drawing.Size(647, 74);
            this.reasonText.TabIndex = 22;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.reasonText, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.mainCategoryComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.productCodeTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.productNameTextBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(930, 306);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // ProductChangeDeleteMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.returnButton;
            this.ClientSize = new System.Drawing.Size(962, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.returnButton);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "ProductChangeDeleteMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "品名マスタメンテナンス　変更・削除";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductChangeDeleteMenu_FormClosed);
            this.Load += new System.EventHandler(this.ProductChangeDeleteMenu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox productCodeTextBox;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox mainCategoryComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox reasonText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}