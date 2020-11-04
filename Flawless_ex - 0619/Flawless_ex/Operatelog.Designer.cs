namespace Flawless_ex
{
    partial class Operatelog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.returnButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.changeTableComboBox = new System.Windows.Forms.ComboBox();
            this.changeTableLabel = new System.Windows.Forms.Label();
            this.changeContentLabel = new System.Windows.Forms.Label();
            this.changerLabel = new System.Windows.Forms.Label();
            this.changeDateLabel = new System.Windows.Forms.Label();
            this.changeDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.changerComboBox = new System.Windows.Forms.ComboBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.changeBeforeLabel = new System.Windows.Forms.Label();
            this.changeBeforeTextBox = new System.Windows.Forms.TextBox();
            this.changeAfterLabel = new System.Windows.Forms.Label();
            this.changeAfterTextBox = new System.Windows.Forms.TextBox();
            this.changeReasonLabel = new System.Windows.Forms.Label();
            this.changeReasonTextBox = new System.Windows.Forms.TextBox();
            this.changeTargetComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // returnButton
            // 
            this.returnButton.BackColor = System.Drawing.Color.LavenderBlush;
            this.returnButton.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.returnButton.Location = new System.Drawing.Point(40, 761);
            this.returnButton.Margin = new System.Windows.Forms.Padding(2);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(163, 73);
            this.returnButton.TabIndex = 0;
            this.returnButton.Text = "戻る";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.Return1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(739, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 48);
            this.label2.TabIndex = 3;
            this.label2.Text = "操作一覧";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Location = new System.Drawing.Point(26, 148);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 100;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1647, 590);
            this.dataGridView1.TabIndex = 5;
            // 
            // changeTableComboBox
            // 
            this.changeTableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.changeTableComboBox.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.changeTableComboBox.FormattingEnabled = true;
            this.changeTableComboBox.Location = new System.Drawing.Point(26, 102);
            this.changeTableComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.changeTableComboBox.Name = "changeTableComboBox";
            this.changeTableComboBox.Size = new System.Drawing.Size(177, 27);
            this.changeTableComboBox.TabIndex = 6;
            this.changeTableComboBox.SelectedIndexChanged += new System.EventHandler(this.changeTableComboBox_SelectedIndexChanged);
            // 
            // changeTableLabel
            // 
            this.changeTableLabel.AutoSize = true;
            this.changeTableLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeTableLabel.Location = new System.Drawing.Point(24, 71);
            this.changeTableLabel.Name = "changeTableLabel";
            this.changeTableLabel.Size = new System.Drawing.Size(134, 24);
            this.changeTableLabel.TabIndex = 7;
            this.changeTableLabel.Text = "変更テーブル";
            // 
            // changeContentLabel
            // 
            this.changeContentLabel.AutoSize = true;
            this.changeContentLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeContentLabel.Location = new System.Drawing.Point(213, 71);
            this.changeContentLabel.Name = "changeContentLabel";
            this.changeContentLabel.Size = new System.Drawing.Size(106, 24);
            this.changeContentLabel.TabIndex = 8;
            this.changeContentLabel.Text = "変更対象";
            // 
            // changerLabel
            // 
            this.changerLabel.AutoSize = true;
            this.changerLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changerLabel.Location = new System.Drawing.Point(743, 70);
            this.changerLabel.Name = "changerLabel";
            this.changerLabel.Size = new System.Drawing.Size(82, 24);
            this.changerLabel.TabIndex = 8;
            this.changerLabel.Text = "変更者";
            // 
            // changeDateLabel
            // 
            this.changeDateLabel.AutoSize = true;
            this.changeDateLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeDateLabel.Location = new System.Drawing.Point(522, 70);
            this.changeDateLabel.Name = "changeDateLabel";
            this.changeDateLabel.Size = new System.Drawing.Size(106, 24);
            this.changeDateLabel.TabIndex = 8;
            this.changeDateLabel.Text = "変更日時";
            // 
            // changeDateDateTimePicker
            // 
            this.changeDateDateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeDateDateTimePicker.Location = new System.Drawing.Point(526, 101);
            this.changeDateDateTimePicker.Name = "changeDateDateTimePicker";
            this.changeDateDateTimePicker.Size = new System.Drawing.Size(200, 28);
            this.changeDateDateTimePicker.TabIndex = 10;
            // 
            // changerComboBox
            // 
            this.changerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.changerComboBox.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.changerComboBox.FormattingEnabled = true;
            this.changerComboBox.Items.AddRange(new object[] {
            "担当者マスタ",
            "品名マスタ",
            "大分類マスタ",
            "消費税マスタ",
            "顧客マスタ　法人",
            "顧客マスタ　個人"});
            this.changerComboBox.Location = new System.Drawing.Point(747, 101);
            this.changerComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.changerComboBox.Name = "changerComboBox";
            this.changerComboBox.Size = new System.Drawing.Size(177, 27);
            this.changerComboBox.TabIndex = 6;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.LightCyan;
            this.searchButton.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchButton.Location = new System.Drawing.Point(1498, 11);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(163, 64);
            this.searchButton.TabIndex = 11;
            this.searchButton.Text = "検索";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.idLabel.Location = new System.Drawing.Point(419, 70);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(87, 24);
            this.idLabel.TabIndex = 8;
            this.idLabel.Text = "変更 ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.idTextBox.Location = new System.Drawing.Point(423, 102);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(83, 28);
            this.idTextBox.TabIndex = 9;
            // 
            // changeBeforeLabel
            // 
            this.changeBeforeLabel.AutoSize = true;
            this.changeBeforeLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeBeforeLabel.Location = new System.Drawing.Point(946, 69);
            this.changeBeforeLabel.Name = "changeBeforeLabel";
            this.changeBeforeLabel.Size = new System.Drawing.Size(82, 24);
            this.changeBeforeLabel.TabIndex = 8;
            this.changeBeforeLabel.Text = "変更前";
            // 
            // changeBeforeTextBox
            // 
            this.changeBeforeTextBox.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeBeforeTextBox.Location = new System.Drawing.Point(950, 99);
            this.changeBeforeTextBox.Name = "changeBeforeTextBox";
            this.changeBeforeTextBox.Size = new System.Drawing.Size(184, 28);
            this.changeBeforeTextBox.TabIndex = 9;
            // 
            // changeAfterLabel
            // 
            this.changeAfterLabel.AutoSize = true;
            this.changeAfterLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeAfterLabel.Location = new System.Drawing.Point(1148, 69);
            this.changeAfterLabel.Name = "changeAfterLabel";
            this.changeAfterLabel.Size = new System.Drawing.Size(82, 24);
            this.changeAfterLabel.TabIndex = 8;
            this.changeAfterLabel.Text = "変更後";
            // 
            // changeAfterTextBox
            // 
            this.changeAfterTextBox.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeAfterTextBox.Location = new System.Drawing.Point(1152, 99);
            this.changeAfterTextBox.Name = "changeAfterTextBox";
            this.changeAfterTextBox.Size = new System.Drawing.Size(184, 28);
            this.changeAfterTextBox.TabIndex = 9;
            // 
            // changeReasonLabel
            // 
            this.changeReasonLabel.AutoSize = true;
            this.changeReasonLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeReasonLabel.Location = new System.Drawing.Point(1349, 69);
            this.changeReasonLabel.Name = "changeReasonLabel";
            this.changeReasonLabel.Size = new System.Drawing.Size(106, 24);
            this.changeReasonLabel.TabIndex = 8;
            this.changeReasonLabel.Text = "変更理由";
            // 
            // changeReasonTextBox
            // 
            this.changeReasonTextBox.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeReasonTextBox.Location = new System.Drawing.Point(1353, 99);
            this.changeReasonTextBox.Multiline = true;
            this.changeReasonTextBox.Name = "changeReasonTextBox";
            this.changeReasonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.changeReasonTextBox.Size = new System.Drawing.Size(319, 31);
            this.changeReasonTextBox.TabIndex = 9;
            // 
            // changeTargetComboBox
            // 
            this.changeTargetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.changeTargetComboBox.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.changeTargetComboBox.FormattingEnabled = true;
            this.changeTargetComboBox.Items.AddRange(new object[] {
            "社員名",
            "品名",
            "大分類名",
            "顧客番号",
            "伝票番号",
            "管理番号",
            "成績番号",
            "消費税"});
            this.changeTargetComboBox.Location = new System.Drawing.Point(217, 102);
            this.changeTargetComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.changeTargetComboBox.Name = "changeTargetComboBox";
            this.changeTargetComboBox.Size = new System.Drawing.Size(177, 27);
            this.changeTargetComboBox.TabIndex = 6;
            // 
            // Operatelog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1684, 861);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.changeDateDateTimePicker);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.changeReasonTextBox);
            this.Controls.Add(this.changeAfterTextBox);
            this.Controls.Add(this.changeBeforeTextBox);
            this.Controls.Add(this.changeDateLabel);
            this.Controls.Add(this.changeReasonLabel);
            this.Controls.Add(this.changeAfterLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.changeBeforeLabel);
            this.Controls.Add(this.changerLabel);
            this.Controls.Add(this.changeContentLabel);
            this.Controls.Add(this.changeTableLabel);
            this.Controls.Add(this.changeTargetComboBox);
            this.Controls.Add(this.changerComboBox);
            this.Controls.Add(this.changeTableComboBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.returnButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Operatelog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作履歴";
            this.Load += new System.EventHandler(this.Operatelog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox changeTableComboBox;
        private System.Windows.Forms.Label changeTableLabel;
        private System.Windows.Forms.Label changeContentLabel;
        private System.Windows.Forms.Label changerLabel;
        private System.Windows.Forms.Label changeDateLabel;
        private System.Windows.Forms.DateTimePicker changeDateDateTimePicker;
        private System.Windows.Forms.ComboBox changerComboBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label changeBeforeLabel;
        private System.Windows.Forms.TextBox changeBeforeTextBox;
        private System.Windows.Forms.Label changeAfterLabel;
        private System.Windows.Forms.TextBox changeAfterTextBox;
        private System.Windows.Forms.Label changeReasonLabel;
        private System.Windows.Forms.TextBox changeReasonTextBox;
        private System.Windows.Forms.ComboBox changeTargetComboBox;
    }
}