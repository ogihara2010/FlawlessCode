﻿namespace Flawless_ex
{
    partial class TaxMaster
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
            this.back = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.taxPercent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(58, 150);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(536, 108);
            this.label1.TabIndex = 0;
            this.label1.Text = "消費税率：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // back
            // 
            this.back.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.back.Location = new System.Drawing.Point(102, 457);
            this.back.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(356, 210);
            this.back.TabIndex = 1;
            this.back.Text = "戻る";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // update
            // 
            this.update.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.update.Location = new System.Drawing.Point(723, 457);
            this.update.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(396, 210);
            this.update.TabIndex = 2;
            this.update.Text = "変更";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // taxPercent
            // 
            this.taxPercent.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.taxPercent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.taxPercent.Location = new System.Drawing.Point(574, 147);
            this.taxPercent.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.taxPercent.Name = "taxPercent";
            this.taxPercent.Size = new System.Drawing.Size(356, 115);
            this.taxPercent.TabIndex = 3;
            this.taxPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.taxPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taxPercent_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(960, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 108);
            this.label2.TabIndex = 4;
            this.label2.Text = "％";
            // 
            // TaxMaster
            // 
            this.AcceptButton = this.update;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 737);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.taxPercent);
            this.Controls.Add(this.update);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "TaxMaster";
            this.Text = "消費税マスタ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaxMaster_FormClosed);
            this.Load += new System.EventHandler(this.TaxMaster_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.TextBox taxPercent;
        private System.Windows.Forms.Label label2;
    }
}