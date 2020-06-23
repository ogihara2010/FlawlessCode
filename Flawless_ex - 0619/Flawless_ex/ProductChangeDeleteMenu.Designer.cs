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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.productCodeTextBox = new System.Windows.Forms.TextBox();
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.returnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "品名コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "品名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "：";
            // 
            // productCodeTextBox
            // 
            this.productCodeTextBox.Location = new System.Drawing.Point(197, 30);
            this.productCodeTextBox.Name = "productCodeTextBox";
            this.productCodeTextBox.ReadOnly = true;
            this.productCodeTextBox.Size = new System.Drawing.Size(100, 31);
            this.productCodeTextBox.TabIndex = 6;
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.Location = new System.Drawing.Point(197, 70);
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.Size = new System.Drawing.Size(238, 31);
            this.productNameTextBox.TabIndex = 7;
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.Location = new System.Drawing.Point(477, 432);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(86, 34);
            this.updateButton.TabIndex = 16;
            this.updateButton.Text = "更新";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(333, 432);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(107, 34);
            this.removeButton.TabIndex = 15;
            this.removeButton.Text = "削除";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // returnButton
            // 
            this.returnButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.returnButton.Location = new System.Drawing.Point(12, 432);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(86, 34);
            this.returnButton.TabIndex = 14;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // ProductChangeDeleteMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 477);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.productNameTextBox);
            this.Controls.Add(this.productCodeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProductChangeDeleteMenu";
            this.Text = "品名マスタメンテナンス　更新";
            this.Load += new System.EventHandler(this.ProductChangeDeleteMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox productCodeTextBox;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button returnButton;
    }
}