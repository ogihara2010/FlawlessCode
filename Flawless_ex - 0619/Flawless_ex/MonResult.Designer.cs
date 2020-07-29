namespace Flawless_ex
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label1.Location = new System.Drawing.Point(44, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "日付検索";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label2.Location = new System.Drawing.Point(694, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "～";
            // 
            // search1
            // 
            this.search1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.search1.Location = new System.Drawing.Point(1226, 78);
            this.search1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.search1.Name = "search1";
            this.search1.Size = new System.Drawing.Size(161, 88);
            this.search1.TabIndex = 4;
            this.search1.Text = "検索";
            this.search1.UseVisualStyleBackColor = true;
            this.search1.Click += new System.EventHandler(this.Search1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.button1.Location = new System.Drawing.Point(1500, 75);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(385, 90);
            this.button1.TabIndex = 5;
            this.button1.Text = "全レコード表示";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // choice
            // 
            this.choice.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.choice.Location = new System.Drawing.Point(1759, 1216);
            this.choice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.choice.Name = "choice";
            this.choice.Size = new System.Drawing.Size(252, 143);
            this.choice.TabIndex = 7;
            this.choice.Text = "選択";
            this.choice.UseVisualStyleBackColor = true;
            this.choice.Click += new System.EventHandler(this.choice_Click);
            // 
            // return3
            // 
            this.return3.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.return3.Location = new System.Drawing.Point(123, 1216);
            this.return3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.return3.Name = "return3";
            this.return3.Size = new System.Drawing.Size(252, 143);
            this.return3.TabIndex = 8;
            this.return3.Text = "戻る";
            this.return3.UseVisualStyleBackColor = true;
            this.return3.Click += new System.EventHandler(this.Return3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(51, 241);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 90;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(2000, 869);
            this.dataGridView1.TabIndex = 9;
            // 
            // deliveryDateBox
            // 
            this.deliveryDateBox.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.deliveryDateBox.Location = new System.Drawing.Point(254, 121);
            this.deliveryDateBox.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.deliveryDateBox.Name = "deliveryDateBox";
            this.deliveryDateBox.Size = new System.Drawing.Size(405, 45);
            this.deliveryDateBox.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.dateTimePicker1.Location = new System.Drawing.Point(771, 120);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(405, 45);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.label3.Location = new System.Drawing.Point(44, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 38);
            this.label3.TabIndex = 14;
            this.label3.Text = "担当者：";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.textBox1.Location = new System.Drawing.Point(254, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(405, 45);
            this.textBox1.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(771, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 80);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.radioButton1.Location = new System.Drawing.Point(17, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(110, 37);
            this.radioButton1.TabIndex = 17;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "法人";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.radioButton2.Location = new System.Drawing.Point(198, 30);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(110, 37);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "個人";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // MonResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(2132, 1460);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.deliveryDateBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.return3);
            this.Controls.Add(this.choice);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.search1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MonResult";
            this.Text = "月間成績表一覧";
            this.Load += new System.EventHandler(this.MonResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}