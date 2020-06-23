namespace Flawless_ex
{
    partial class MainMenu
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
            this.Statement_DeliveryButton = new System.Windows.Forms.Button();
            this.CustomerHistoriButton = new System.Windows.Forms.Button();
            this.MonResults = new System.Windows.Forms.Button();
            this.MasterMainte = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.operatelog = new System.Windows.Forms.Button();
            this.Invoice = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Statement_DeliveryButton
            // 
            this.Statement_DeliveryButton.AutoSize = true;
            this.Statement_DeliveryButton.Location = new System.Drawing.Point(12, 53);
            this.Statement_DeliveryButton.Name = "Statement_DeliveryButton";
            this.Statement_DeliveryButton.Size = new System.Drawing.Size(446, 51);
            this.Statement_DeliveryButton.TabIndex = 0;
            this.Statement_DeliveryButton.Text = "計算書/納品書作成";
            this.Statement_DeliveryButton.UseVisualStyleBackColor = true;
            this.Statement_DeliveryButton.Click += new System.EventHandler(this.Statement_DeliveryButton_Click);
            // 
            // CustomerHistoriButton
            // 
            this.CustomerHistoriButton.AutoSize = true;
            this.CustomerHistoriButton.Location = new System.Drawing.Point(12, 124);
            this.CustomerHistoriButton.Name = "CustomerHistoriButton";
            this.CustomerHistoriButton.Size = new System.Drawing.Size(446, 51);
            this.CustomerHistoriButton.TabIndex = 1;
            this.CustomerHistoriButton.Text = "買取販売データ検索";
            this.CustomerHistoriButton.UseVisualStyleBackColor = true;
            this.CustomerHistoriButton.Click += new System.EventHandler(this.CustomerHistoriButton_Click);
            // 
            // MonResults
            // 
            this.MonResults.AutoSize = true;
            this.MonResults.Location = new System.Drawing.Point(12, 194);
            this.MonResults.Name = "MonResults";
            this.MonResults.Size = new System.Drawing.Size(446, 51);
            this.MonResults.TabIndex = 2;
            this.MonResults.Text = "月間成績表";
            this.MonResults.UseVisualStyleBackColor = true;
            this.MonResults.Click += new System.EventHandler(this.MonResults_Click);
            // 
            // MasterMainte
            // 
            this.MasterMainte.AutoSize = true;
            this.MasterMainte.Location = new System.Drawing.Point(12, 492);
            this.MasterMainte.Name = "MasterMainte";
            this.MasterMainte.Size = new System.Drawing.Size(446, 51);
            this.MasterMainte.TabIndex = 3;
            this.MasterMainte.Text = "マスタメンテナンス";
            this.MasterMainte.UseVisualStyleBackColor = true;
            this.MasterMainte.Click += new System.EventHandler(this.MasterMainte_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Location = new System.Drawing.Point(12, 574);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(79, 34);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // operatelog
            // 
            this.operatelog.AutoSize = true;
            this.operatelog.Location = new System.Drawing.Point(12, 418);
            this.operatelog.Name = "operatelog";
            this.operatelog.Size = new System.Drawing.Size(446, 51);
            this.operatelog.TabIndex = 6;
            this.operatelog.Text = "操作履歴";
            this.operatelog.UseVisualStyleBackColor = true;
            this.operatelog.Click += new System.EventHandler(this.Operatelog_Click);
            // 
            // Invoice
            // 
            this.Invoice.AutoSize = true;
            this.Invoice.Location = new System.Drawing.Point(12, 337);
            this.Invoice.Name = "Invoice";
            this.Invoice.Size = new System.Drawing.Size(446, 51);
            this.Invoice.TabIndex = 7;
            this.Invoice.Text = "インボイス";
            this.Invoice.UseVisualStyleBackColor = true;
            this.Invoice.Click += new System.EventHandler(this.Invoice_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.Location = new System.Drawing.Point(12, 266);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(446, 51);
            this.button3.TabIndex = 8;
            this.button3.Text = "卸値/未入力・次月持越し";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 641);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Invoice);
            this.Controls.Add(this.operatelog);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MasterMainte);
            this.Controls.Add(this.MonResults);
            this.Controls.Add(this.CustomerHistoriButton);
            this.Controls.Add(this.Statement_DeliveryButton);
            this.Name = "MainMenu";
            this.Text = "メインメニュー";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Statement_DeliveryButton;
        private System.Windows.Forms.Button CustomerHistoriButton;
        private System.Windows.Forms.Button MonResults;
        private System.Windows.Forms.Button MasterMainte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button operatelog;
        private System.Windows.Forms.Button Invoice;
        private System.Windows.Forms.Button button3;
    }
}