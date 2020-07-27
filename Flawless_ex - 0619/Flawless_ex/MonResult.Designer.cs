﻿namespace Flawless_ex
{
    partial class MonResult
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
            this.search1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.choice = new System.Windows.Forms.Button();
            this.return3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.deliveryDateBox = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "日付検索";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label2.Location = new System.Drawing.Point(572, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "～";
            // 
            // search1
            // 
            this.search1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.search1.Location = new System.Drawing.Point(1036, 36);
            this.search1.Name = "search1";
            this.search1.Size = new System.Drawing.Size(125, 74);
            this.search1.TabIndex = 4;
            this.search1.Text = "検索";
            this.search1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.button1.Location = new System.Drawing.Point(851, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(326, 83);
            this.button1.TabIndex = 5;
            this.button1.Text = "全レコード表示";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // choice
            // 
            this.choice.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.choice.Location = new System.Drawing.Point(964, 1059);
            this.choice.Name = "choice";
            this.choice.Size = new System.Drawing.Size(213, 125);
            this.choice.TabIndex = 7;
            this.choice.Text = "選択";
            this.choice.UseVisualStyleBackColor = true;
            this.choice.Click += new System.EventHandler(this.choice_Click);
            // 
            // return3
            // 
            this.return3.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.return3.Location = new System.Drawing.Point(57, 1059);
            this.return3.Name = "return3";
            this.return3.Size = new System.Drawing.Size(213, 125);
            this.return3.TabIndex = 8;
            this.return3.Text = "戻る";
            this.return3.UseVisualStyleBackColor = true;
            this.return3.Click += new System.EventHandler(this.Return3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(85, 235);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 90;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1108, 760);
            this.dataGridView1.TabIndex = 9;
            // 
            // deliveryDateBox
            // 
            this.deliveryDateBox.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.deliveryDateBox.Location = new System.Drawing.Point(209, 47);
            this.deliveryDateBox.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deliveryDateBox.Name = "deliveryDateBox";
            this.deliveryDateBox.Size = new System.Drawing.Size(343, 45);
            this.deliveryDateBox.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.dateTimePicker1.Location = new System.Drawing.Point(632, 47);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(343, 45);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // MonResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1269, 1219);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.deliveryDateBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.return3);
            this.Controls.Add(this.choice);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.search1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MonResult";
            this.Text = "月間成績表一覧";
            this.Load += new System.EventHandler(this.MonResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button search1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button choice;
        private System.Windows.Forms.Button return3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker deliveryDateBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}