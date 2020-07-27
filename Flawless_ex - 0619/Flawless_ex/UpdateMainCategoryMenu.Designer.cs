namespace Flawless_ex
{
    partial class UpdateMainCategoryMenu
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
            this.mainCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.mainCategoryNameTextBox = new System.Windows.Forms.TextBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "大分類コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "大分類名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "：";
            // 
            // mainCategoryCodeTextBox
            // 
            this.mainCategoryCodeTextBox.Location = new System.Drawing.Point(154, 24);
            this.mainCategoryCodeTextBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mainCategoryCodeTextBox.Name = "mainCategoryCodeTextBox";
            this.mainCategoryCodeTextBox.ReadOnly = true;
            this.mainCategoryCodeTextBox.Size = new System.Drawing.Size(103, 28);
            this.mainCategoryCodeTextBox.TabIndex = 4;
            // 
            // mainCategoryNameTextBox
            // 
            this.mainCategoryNameTextBox.Location = new System.Drawing.Point(154, 74);
            this.mainCategoryNameTextBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mainCategoryNameTextBox.Name = "mainCategoryNameTextBox";
            this.mainCategoryNameTextBox.Size = new System.Drawing.Size(167, 28);
            this.mainCategoryNameTextBox.TabIndex = 5;
            // 
            // returnButton
            // 
            this.returnButton.AutoSize = true;
            this.returnButton.Location = new System.Drawing.Point(18, 158);
            this.returnButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(132, 65);
            this.returnButton.TabIndex = 6;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.AutoSize = true;
            this.removeButton.Location = new System.Drawing.Point(183, 158);
            this.removeButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(141, 65);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "無効";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.AutoSize = true;
            this.updateButton.Location = new System.Drawing.Point(328, 158);
            this.updateButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(141, 65);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "更新";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 21);
            this.label5.TabIndex = 9;
            this.label5.Text = "label5";
            // 
            // UpdateMainCategoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 238);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.mainCategoryNameTextBox);
            this.Controls.Add(this.mainCategoryCodeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "UpdateMainCategoryMenu";
            this.Text = "大分類マスタメンテナンス　更新";
            this.Load += new System.EventHandler(this.UpdateMainCategoryMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mainCategoryCodeTextBox;
        private System.Windows.Forms.TextBox mainCategoryNameTextBox;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label label5;
    }
}