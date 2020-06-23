namespace Flawless_ex
{
    partial class MasterMaintenanceMenu_BC
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
            this.itemMasterButton = new System.Windows.Forms.Button();
            this.clientMasterButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.mainCategoryButton = new System.Windows.Forms.Button();
            this.subCategoryButton = new System.Windows.Forms.Button();
            this.TaxMaster = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(496, -26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // itemMasterButton
            // 
            this.itemMasterButton.AutoSize = true;
            this.itemMasterButton.Location = new System.Drawing.Point(12, 93);
            this.itemMasterButton.Name = "itemMasterButton";
            this.itemMasterButton.Size = new System.Drawing.Size(448, 49);
            this.itemMasterButton.TabIndex = 14;
            this.itemMasterButton.Text = "品名マスタ";
            this.itemMasterButton.UseVisualStyleBackColor = true;
            this.itemMasterButton.Click += new System.EventHandler(this.itemMasterButton_Click);
            // 
            // clientMasterButton
            // 
            this.clientMasterButton.AutoSize = true;
            this.clientMasterButton.Location = new System.Drawing.Point(12, 12);
            this.clientMasterButton.Name = "clientMasterButton";
            this.clientMasterButton.Size = new System.Drawing.Size(448, 49);
            this.clientMasterButton.TabIndex = 13;
            this.clientMasterButton.Text = "顧客マスタ";
            this.clientMasterButton.UseVisualStyleBackColor = true;
            this.clientMasterButton.Click += new System.EventHandler(this.clientMasterButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Location = new System.Drawing.Point(20, 423);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(115, 57);
            this.CloseButton.TabIndex = 11;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // mainCategoryButton
            // 
            this.mainCategoryButton.Location = new System.Drawing.Point(12, 170);
            this.mainCategoryButton.Name = "mainCategoryButton";
            this.mainCategoryButton.Size = new System.Drawing.Size(448, 49);
            this.mainCategoryButton.TabIndex = 15;
            this.mainCategoryButton.Text = "大分類マスタ";
            this.mainCategoryButton.UseVisualStyleBackColor = true;
            // 
            // subCategoryButton
            // 
            this.subCategoryButton.Location = new System.Drawing.Point(12, 245);
            this.subCategoryButton.Name = "subCategoryButton";
            this.subCategoryButton.Size = new System.Drawing.Size(448, 49);
            this.subCategoryButton.TabIndex = 16;
            this.subCategoryButton.Text = "小分類マスタ";
            this.subCategoryButton.UseVisualStyleBackColor = true;
            // 
            // TaxMaster
            // 
            this.TaxMaster.Location = new System.Drawing.Point(12, 322);
            this.TaxMaster.Name = "TaxMaster";
            this.TaxMaster.Size = new System.Drawing.Size(448, 51);
            this.TaxMaster.TabIndex = 17;
            this.TaxMaster.Text = "消費税マスタ";
            this.TaxMaster.UseVisualStyleBackColor = true;
            // 
            // MasterMaintenanceMenu_BC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 527);
            this.Controls.Add(this.TaxMaster);
            this.Controls.Add(this.subCategoryButton);
            this.Controls.Add(this.mainCategoryButton);
            this.Controls.Add(this.itemMasterButton);
            this.Controls.Add(this.clientMasterButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Name = "MasterMaintenanceMenu_BC";
            this.Text = "MasterMaintenanceMenu_BC";
            this.Load += new System.EventHandler(this.MasterMaintenanceMenu_BC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button itemMasterButton;
        private System.Windows.Forms.Button clientMasterButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button mainCategoryButton;
        private System.Windows.Forms.Button subCategoryButton;
        private System.Windows.Forms.Button TaxMaster;
    }
}