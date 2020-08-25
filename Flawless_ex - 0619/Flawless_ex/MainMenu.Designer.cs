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
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Statement_DeliveryButton
            // 
            this.Statement_DeliveryButton.AutoSize = true;
            this.Statement_DeliveryButton.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Statement_DeliveryButton.Location = new System.Drawing.Point(188, 126);
            this.Statement_DeliveryButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Statement_DeliveryButton.Name = "Statement_DeliveryButton";
            this.Statement_DeliveryButton.Size = new System.Drawing.Size(725, 97);
            this.Statement_DeliveryButton.TabIndex = 0;
            this.Statement_DeliveryButton.Text = "計算書/納品書作成";
            this.Statement_DeliveryButton.UseVisualStyleBackColor = true;
            this.Statement_DeliveryButton.Click += new System.EventHandler(this.Statement_DeliveryButton_Click);
            // 
            // CustomerHistoriButton
            // 
            this.CustomerHistoriButton.AutoSize = true;
            this.CustomerHistoriButton.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CustomerHistoriButton.Location = new System.Drawing.Point(188, 251);
            this.CustomerHistoriButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.CustomerHistoriButton.Name = "CustomerHistoriButton";
            this.CustomerHistoriButton.Size = new System.Drawing.Size(725, 118);
            this.CustomerHistoriButton.TabIndex = 1;
            this.CustomerHistoriButton.Text = "買取販売データ検索";
            this.CustomerHistoriButton.UseVisualStyleBackColor = true;
            this.CustomerHistoriButton.Click += new System.EventHandler(this.CustomerHistoriButton_Click);
            // 
            // MonResults
            // 
            this.MonResults.AutoSize = true;
            this.MonResults.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MonResults.Location = new System.Drawing.Point(188, 400);
            this.MonResults.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MonResults.Name = "MonResults";
            this.MonResults.Size = new System.Drawing.Size(725, 111);
            this.MonResults.TabIndex = 2;
            this.MonResults.Text = "月間成績表";
            this.MonResults.UseVisualStyleBackColor = true;
            this.MonResults.Click += new System.EventHandler(this.MonResults_Click);
            // 
            // MasterMainte
            // 
            this.MasterMainte.AutoSize = true;
            this.MasterMainte.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MasterMainte.Location = new System.Drawing.Point(188, 844);
            this.MasterMainte.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MasterMainte.Name = "MasterMainte";
            this.MasterMainte.Size = new System.Drawing.Size(725, 120);
            this.MasterMainte.TabIndex = 3;
            this.MasterMainte.Text = "マスタメンテナンス";
            this.MasterMainte.UseVisualStyleBackColor = true;
            this.MasterMainte.Click += new System.EventHandler(this.MasterMainte_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(840, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 48);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CloseButton.Location = new System.Drawing.Point(188, 1131);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(217, 136);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // operatelog
            // 
            this.operatelog.AutoSize = true;
            this.operatelog.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.operatelog.Location = new System.Drawing.Point(188, 691);
            this.operatelog.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.operatelog.Name = "operatelog";
            this.operatelog.Size = new System.Drawing.Size(725, 102);
            this.operatelog.TabIndex = 6;
            this.operatelog.Text = "操作履歴";
            this.operatelog.UseVisualStyleBackColor = true;
            this.operatelog.Click += new System.EventHandler(this.Operatelog_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(188, 537);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(725, 110);
            this.button3.TabIndex = 8;
            this.button3.Text = "卸値/未入力・次月持越し";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1102, 1319);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.operatelog);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MasterMainte);
            this.Controls.Add(this.MonResults);
            this.Controls.Add(this.CustomerHistoriButton);
            this.Controls.Add(this.Statement_DeliveryButton);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "メインメニュー";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
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
        private System.Windows.Forms.Button button3;
    }
}