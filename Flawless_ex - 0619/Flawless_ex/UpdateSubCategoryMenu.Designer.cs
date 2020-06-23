namespace Flawless_ex
{
    partial class updateSubCategoryMenu
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.subCategoryTextBox = new System.Windows.Forms.TextBox();
            this.mainCategoryComboBox1 = new System.Windows.Forms.ComboBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.subCategory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "大分類";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "小分類";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "：";
            // 
            // subCategoryTextBox
            // 
            this.subCategoryTextBox.Location = new System.Drawing.Point(192, 71);
            this.subCategoryTextBox.Name = "subCategoryTextBox";
            this.subCategoryTextBox.Size = new System.Drawing.Size(165, 31);
            this.subCategoryTextBox.TabIndex = 5;
            // 
            // mainCategoryComboBox1
            // 
            this.mainCategoryComboBox1.FormattingEnabled = true;
            this.mainCategoryComboBox1.Location = new System.Drawing.Point(192, 127);
            this.mainCategoryComboBox1.Name = "mainCategoryComboBox1";
            this.mainCategoryComboBox1.Size = new System.Drawing.Size(165, 32);
            this.mainCategoryComboBox1.TabIndex = 6;
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(28, 312);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(108, 54);
            this.returnButton.TabIndex = 7;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(236, 312);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(108, 54);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "無効";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(378, 312);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(108, 54);
            this.updateButton.TabIndex = 9;
            this.updateButton.Text = "更新";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "小分類コード";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "：";
            // 
            // subCategory
            // 
            this.subCategory.Location = new System.Drawing.Point(192, 17);
            this.subCategory.Name = "subCategory";
            this.subCategory.ReadOnly = true;
            this.subCategory.Size = new System.Drawing.Size(165, 31);
            this.subCategory.TabIndex = 12;
            // 
            // updateSubCategoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 434);
            this.Controls.Add(this.subCategory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.mainCategoryComboBox1);
            this.Controls.Add(this.subCategoryTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "updateSubCategoryMenu";
            this.Text = "小分類マスタメンテナンス　更新";
            this.Load += new System.EventHandler(this.updateSubCategoryMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox subCategoryTextBox;
        private System.Windows.Forms.ComboBox mainCategoryComboBox1;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox subCategory;
    }
}