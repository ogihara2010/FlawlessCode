namespace Flawless_ex
{
    partial class StaffAddStaff
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
            this.returnButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mainCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.parsonCodeText = new System.Windows.Forms.TextBox();
            this.accessButton = new System.Windows.Forms.ComboBox();
            this.passwordReText = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.parsonNamt2Text = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.parsonNameText = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // returnButton
            // 
            this.returnButton.AutoSize = true;
            this.returnButton.Location = new System.Drawing.Point(46, 334);
            this.returnButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(178, 104);
            this.returnButton.TabIndex = 0;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // addButton
            // 
            this.addButton.AutoSize = true;
            this.addButton.Location = new System.Drawing.Point(358, 334);
            this.addButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(186, 104);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "登録";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-4, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "　担当者コード　";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "担当者名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "担当者カナ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 188);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "パスワード";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 228);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 24);
            this.label6.TabIndex = 7;
            this.label6.Text = "パスワード再入力";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 268);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 24);
            this.label7.TabIndex = 8;
            this.label7.Text = "アクセス権限";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mainCategoryComboBox);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.returnButton);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.parsonCodeText);
            this.groupBox1.Controls.Add(this.accessButton);
            this.groupBox1.Controls.Add(this.passwordReText);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.passwordText);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.parsonNamt2Text);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.parsonNameText);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox1.Size = new System.Drawing.Size(579, 468);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // mainCategoryComboBox
            // 
            this.mainCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainCategoryComboBox.FormattingEnabled = true;
            this.mainCategoryComboBox.Location = new System.Drawing.Point(299, 148);
            this.mainCategoryComboBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.mainCategoryComboBox.Name = "mainCategoryComboBox";
            this.mainCategoryComboBox.Size = new System.Drawing.Size(160, 32);
            this.mainCategoryComboBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(260, 148);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 24);
            this.label11.TabIndex = 24;
            this.label11.Text = "：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "大分類(初期値の設定)";
            // 
            // parsonCodeText
            // 
            this.parsonCodeText.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.parsonCodeText.Location = new System.Drawing.Point(299, 30);
            this.parsonCodeText.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.parsonCodeText.Name = "parsonCodeText";
            this.parsonCodeText.ReadOnly = true;
            this.parsonCodeText.Size = new System.Drawing.Size(160, 31);
            this.parsonCodeText.TabIndex = 10;
            this.parsonCodeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // accessButton
            // 
            this.accessButton.AutoCompleteCustomSource.AddRange(new string[] {
            "A",
            "B",
            "C"});
            this.accessButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accessButton.FormattingEnabled = true;
            this.accessButton.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.accessButton.Location = new System.Drawing.Point(299, 264);
            this.accessButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.accessButton.Name = "accessButton";
            this.accessButton.Size = new System.Drawing.Size(121, 32);
            this.accessButton.TabIndex = 23;
            // 
            // passwordReText
            // 
            this.passwordReText.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.passwordReText.Location = new System.Drawing.Point(299, 224);
            this.passwordReText.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.passwordReText.Name = "passwordReText";
            this.passwordReText.Size = new System.Drawing.Size(160, 31);
            this.passwordReText.TabIndex = 21;
            this.passwordReText.UseSystemPasswordChar = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(260, 268);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 24);
            this.label15.TabIndex = 20;
            this.label15.Text = "：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(260, 228);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 24);
            this.label14.TabIndex = 19;
            this.label14.Text = "：";
            // 
            // passwordText
            // 
            this.passwordText.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.passwordText.Location = new System.Drawing.Point(299, 184);
            this.passwordText.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(160, 31);
            this.passwordText.TabIndex = 18;
            this.passwordText.UseSystemPasswordChar = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(260, 188);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 24);
            this.label12.TabIndex = 16;
            this.label12.Text = "：";
            // 
            // parsonNamt2Text
            // 
            this.parsonNamt2Text.Location = new System.Drawing.Point(299, 108);
            this.parsonNamt2Text.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.parsonNamt2Text.Name = "parsonNamt2Text";
            this.parsonNamt2Text.Size = new System.Drawing.Size(160, 31);
            this.parsonNamt2Text.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(260, 110);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 24);
            this.label10.TabIndex = 13;
            this.label10.Text = "：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 70);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 24);
            this.label9.TabIndex = 12;
            this.label9.Text = "：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(260, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 24);
            this.label8.TabIndex = 11;
            this.label8.Text = "：";
            // 
            // parsonNameText
            // 
            this.parsonNameText.Location = new System.Drawing.Point(299, 68);
            this.parsonNameText.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.parsonNameText.Name = "parsonNameText";
            this.parsonNameText.Size = new System.Drawing.Size(160, 31);
            this.parsonNameText.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-4, 30);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(168, 24);
            this.label13.TabIndex = 2;
            this.label13.Text = "　担当者コード　";
            // 
            // StaffAddStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 476);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "StaffAddStaff";
            this.Text = "担当者マスタメンテナンス　新規登録";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StaffAddStaff_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StaffAddStaff_FormClosed);
            this.Load += new System.EventHandler(this.StaffAddStaff_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox accessButton;
        private System.Windows.Forms.TextBox passwordReText;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox parsonNamt2Text;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox parsonNameText;
        private System.Windows.Forms.TextBox parsonCodeText;
        private System.Windows.Forms.ComboBox mainCategoryComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
    }
}