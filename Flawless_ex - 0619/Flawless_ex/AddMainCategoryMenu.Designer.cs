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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mainCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.mainCategoryNameTextBox = new System.Windows.Forms.TextBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "大分類コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "大分類名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "：";
            // 
            // mainCategoryCodeTextBox
            // 
            this.mainCategoryCodeTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mainCategoryCodeTextBox.Location = new System.Drawing.Point(214, 27);
            this.mainCategoryCodeTextBox.Name = "mainCategoryCodeTextBox";
            this.mainCategoryCodeTextBox.Size = new System.Drawing.Size(167, 31);
            this.mainCategoryCodeTextBox.TabIndex = 4;
            // 
            // mainCategoryNameTextBox
            // 
            this.mainCategoryNameTextBox.Location = new System.Drawing.Point(214, 74);
            this.mainCategoryNameTextBox.Name = "mainCategoryNameTextBox";
            this.mainCategoryNameTextBox.Size = new System.Drawing.Size(167, 31);
            this.mainCategoryNameTextBox.TabIndex = 5;
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(25, 278);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(108, 54);
            this.returnButton.TabIndex = 6;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(493, 278);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(108, 54);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "登録";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // AddMainCategoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 375);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.mainCategoryNameTextBox);
            this.Controls.Add(this.mainCategoryCodeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddMainCategoryMenu";
            this.Text = "大分類マスタメンテナンス　新規登録";
            this.Load += new System.EventHandler(this.AddMainCategoryMenu_Load);
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
        private System.Windows.Forms.Button addButton;
    }
}