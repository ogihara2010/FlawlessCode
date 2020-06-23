namespace Flawless_ex
{
    partial class MasterMaintenanceMenu
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.staffMasterButton = new System.Windows.Forms.Button();
            this.clientMasterButton = new System.Windows.Forms.Button();
            this.itemMasterButton = new System.Windows.Forms.Button();
            this.mainCategoryMasterButton = new System.Windows.Forms.Button();
            this.subCategoryMasterButton = new System.Windows.Forms.Button();
            this.TaxMaster = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Location = new System.Drawing.Point(28, 545);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(115, 57);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // staffMasterButton
            // 
            this.staffMasterButton.AutoSize = true;
            this.staffMasterButton.Location = new System.Drawing.Point(28, 48);
            this.staffMasterButton.Name = "staffMasterButton";
            this.staffMasterButton.Size = new System.Drawing.Size(448, 49);
            this.staffMasterButton.TabIndex = 1;
            this.staffMasterButton.Text = "担当者マスタ";
            this.staffMasterButton.UseVisualStyleBackColor = true;
            this.staffMasterButton.Click += new System.EventHandler(this.staffMasterButtonClick);
            // 
            // clientMasterButton
            // 
            this.clientMasterButton.AutoSize = true;
            this.clientMasterButton.Location = new System.Drawing.Point(28, 125);
            this.clientMasterButton.Name = "clientMasterButton";
            this.clientMasterButton.Size = new System.Drawing.Size(448, 49);
            this.clientMasterButton.TabIndex = 2;
            this.clientMasterButton.Text = "顧客マスタ";
            this.clientMasterButton.UseVisualStyleBackColor = true;
            this.clientMasterButton.Click += new System.EventHandler(this.clientMasterButton_Click);
            // 
            // itemMasterButton
            // 
            this.itemMasterButton.AutoSize = true;
            this.itemMasterButton.Location = new System.Drawing.Point(28, 204);
            this.itemMasterButton.Name = "itemMasterButton";
            this.itemMasterButton.Size = new System.Drawing.Size(448, 49);
            this.itemMasterButton.TabIndex = 3;
            this.itemMasterButton.Text = "品名マスタ";
            this.itemMasterButton.UseVisualStyleBackColor = true;
            this.itemMasterButton.Click += new System.EventHandler(this.itemMasterButtonClick);
            // 
            // mainCategoryMasterButton
            // 
            this.mainCategoryMasterButton.Location = new System.Drawing.Point(28, 283);
            this.mainCategoryMasterButton.Name = "mainCategoryMasterButton";
            this.mainCategoryMasterButton.Size = new System.Drawing.Size(448, 49);
            this.mainCategoryMasterButton.TabIndex = 4;
            this.mainCategoryMasterButton.Text = "大分類マスタ";
            this.mainCategoryMasterButton.UseVisualStyleBackColor = true;
            this.mainCategoryMasterButton.Click += new System.EventHandler(this.mainCategoryMasterButton_Click);
            // 
            // subCategoryMasterButton
            // 
            this.subCategoryMasterButton.Location = new System.Drawing.Point(28, 355);
            this.subCategoryMasterButton.Name = "subCategoryMasterButton";
            this.subCategoryMasterButton.Size = new System.Drawing.Size(448, 49);
            this.subCategoryMasterButton.TabIndex = 5;
            this.subCategoryMasterButton.Text = "小分類マスタ";
            this.subCategoryMasterButton.UseVisualStyleBackColor = true;
            this.subCategoryMasterButton.Click += new System.EventHandler(this.subCategoryMasterButton_Click);
            // 
            // TaxMaster
            // 
            this.TaxMaster.Location = new System.Drawing.Point(28, 433);
            this.TaxMaster.Name = "TaxMaster";
            this.TaxMaster.Size = new System.Drawing.Size(448, 52);
            this.TaxMaster.TabIndex = 6;
            this.TaxMaster.Text = "消費税マスタ";
            this.TaxMaster.UseVisualStyleBackColor = true;
            this.TaxMaster.Click += new System.EventHandler(this.TaxMaster_Click);
            // 
            // MasterMaintenanceMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 636);
            this.Controls.Add(this.TaxMaster);
            this.Controls.Add(this.subCategoryMasterButton);
            this.Controls.Add(this.mainCategoryMasterButton);
            this.Controls.Add(this.itemMasterButton);
            this.Controls.Add(this.clientMasterButton);
            this.Controls.Add(this.staffMasterButton);
            this.Controls.Add(this.CloseButton);
            this.Name = "MasterMaintenanceMenu";
            this.Text = "マスタメンテナンスメニュー";
            this.Load += new System.EventHandler(this.MasterMaintenanceMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button staffMasterButton;
        private System.Windows.Forms.Button clientMasterButton;
        private System.Windows.Forms.Button itemMasterButton;
        private System.Windows.Forms.Button mainCategoryMasterButton;
        private System.Windows.Forms.Button subCategoryMasterButton;
        private System.Windows.Forms.Button TaxMaster;
    }
}