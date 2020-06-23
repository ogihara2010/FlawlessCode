namespace Flawless_ex
{
    partial class AddSubCategoryMenu
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.subCategoryNameTextBox = new System.Windows.Forms.TextBox();
            this.subCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.meinCategoryNameComboBox = new System.Windows.Forms.ComboBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "大分類名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "小分類名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "小分類コード";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "：";
            // 
            // subCategoryNameTextBox
            // 
            this.subCategoryNameTextBox.Location = new System.Drawing.Point(280, 95);
            this.subCategoryNameTextBox.Name = "subCategoryNameTextBox";
            this.subCategoryNameTextBox.Size = new System.Drawing.Size(173, 31);
            this.subCategoryNameTextBox.TabIndex = 6;
            // 
            // subCategoryCodeTextBox
            // 
            this.subCategoryCodeTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.subCategoryCodeTextBox.Location = new System.Drawing.Point(280, 149);
            this.subCategoryCodeTextBox.Name = "subCategoryCodeTextBox";
            this.subCategoryCodeTextBox.Size = new System.Drawing.Size(173, 31);
            this.subCategoryCodeTextBox.TabIndex = 7;
            // 
            // meinCategoryNameComboBox
            // 
            this.meinCategoryNameComboBox.FormattingEnabled = true;
            this.meinCategoryNameComboBox.Location = new System.Drawing.Point(280, 33);
            this.meinCategoryNameComboBox.Name = "meinCategoryNameComboBox";
            this.meinCategoryNameComboBox.Size = new System.Drawing.Size(173, 32);
            this.meinCategoryNameComboBox.TabIndex = 8;
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(43, 350);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(108, 54);
            this.returnButton.TabIndex = 9;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(345, 350);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(108, 54);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "登録";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // AddSubCategoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 450);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.meinCategoryNameComboBox);
            this.Controls.Add(this.subCategoryCodeTextBox);
            this.Controls.Add(this.subCategoryNameTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddSubCategoryMenu";
            this.Text = "小分類マスタメンテナンス　新規登録";
            this.Load += new System.EventHandler(this.AddSubCategoryMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox subCategoryNameTextBox;
        private System.Windows.Forms.TextBox subCategoryCodeTextBox;
        private System.Windows.Forms.ComboBox meinCategoryNameComboBox;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button addButton;
    }
}