namespace Flawless_ex
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
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.label1.Location = new System.Drawing.Point(56, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "消費税率：";
            // 
            // back
            // 
            this.back.Location = new System.Drawing.Point(76, 197);
            this.back.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(68, 42);
            this.back.TabIndex = 1;
            this.back.Text = "戻る";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(250, 197);
            this.update.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(68, 42);
            this.update.TabIndex = 2;
            this.update.Text = "更新";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // taxPercent
            // 
            this.taxPercent.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.taxPercent.Location = new System.Drawing.Point(165, 80);
            this.taxPercent.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.taxPercent.Name = "taxPercent";
            this.taxPercent.Size = new System.Drawing.Size(79, 31);
            this.taxPercent.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.label2.Location = new System.Drawing.Point(265, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "％";
            // 
            // TaxMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 280);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.taxPercent);
            this.Controls.Add(this.update);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "TaxMaster";
            this.Text = "消費税マスタ";
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