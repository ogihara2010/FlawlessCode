﻿namespace Flawless_ex
{
    partial class ItemMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.signUpButton = new System.Windows.Forms.Button();
            this.changeDeleteButton = new System.Windows.Forms.Button();
            this.mainCategoryMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 20.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(20, 22);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(778, 767);
            this.dataGridView1.TabIndex = 7;
            // 
            // ReturnButton
            // 
            this.ReturnButton.AutoSize = true;
            this.ReturnButton.BackColor = System.Drawing.Color.MistyRose;
            this.ReturnButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReturnButton.Location = new System.Drawing.Point(12, 804);
            this.ReturnButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(114, 84);
            this.ReturnButton.TabIndex = 4;
            this.ReturnButton.Text = "戻る";
            this.ReturnButton.UseVisualStyleBackColor = false;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // signUpButton
            // 
            this.signUpButton.AutoSize = true;
            this.signUpButton.BackColor = System.Drawing.Color.LightCyan;
            this.signUpButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.signUpButton.Location = new System.Drawing.Point(344, 804);
            this.signUpButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.signUpButton.Name = "signUpButton";
            this.signUpButton.Size = new System.Drawing.Size(158, 84);
            this.signUpButton.TabIndex = 6;
            this.signUpButton.Text = "新規登録";
            this.signUpButton.UseVisualStyleBackColor = false;
            this.signUpButton.Click += new System.EventHandler(this.signUpButton_Click);
            // 
            // changeDeleteButton
            // 
            this.changeDeleteButton.AutoSize = true;
            this.changeDeleteButton.BackColor = System.Drawing.Color.LightCyan;
            this.changeDeleteButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.changeDeleteButton.Location = new System.Drawing.Point(150, 804);
            this.changeDeleteButton.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.changeDeleteButton.Name = "changeDeleteButton";
            this.changeDeleteButton.Size = new System.Drawing.Size(176, 84);
            this.changeDeleteButton.TabIndex = 5;
            this.changeDeleteButton.Text = "変更・無効";
            this.changeDeleteButton.UseVisualStyleBackColor = false;
            this.changeDeleteButton.Click += new System.EventHandler(this.changeDeleteButton_Click);
            // 
            // mainCategoryMenu
            // 
            this.mainCategoryMenu.AutoSize = true;
            this.mainCategoryMenu.BackColor = System.Drawing.Color.LightCyan;
            this.mainCategoryMenu.Font = new System.Drawing.Font("MS UI Gothic", 20.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainCategoryMenu.Location = new System.Drawing.Point(573, 805);
            this.mainCategoryMenu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.mainCategoryMenu.Name = "mainCategoryMenu";
            this.mainCategoryMenu.Size = new System.Drawing.Size(225, 84);
            this.mainCategoryMenu.TabIndex = 8;
            this.mainCategoryMenu.Text = "大分類マスタ一覧";
            this.mainCategoryMenu.UseVisualStyleBackColor = false;
            this.mainCategoryMenu.Click += new System.EventHandler(this.mainCategoryMenu_Click);
            // 
            // ItemMaster
            // 
            this.AcceptButton = this.ReturnButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(820, 910);
            this.Controls.Add(this.mainCategoryMenu);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.signUpButton);
            this.Controls.Add(this.changeDeleteButton);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "ItemMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "品名マスタメンテナンス";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ItemMaster_FormClosed);
            this.Load += new System.EventHandler(this.ProductNameMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button signUpButton;
        private System.Windows.Forms.Button changeDeleteButton;
        private System.Windows.Forms.Button mainCategoryMenu;
    }
}