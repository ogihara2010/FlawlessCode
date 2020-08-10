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
            this.TaxMaster = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Font = new System.Drawing.Font("MS UI Gothic", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CloseButton.Location = new System.Drawing.Point(28, 880);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(321, 129);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "メインメニュー";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // staffMasterButton
            // 
            this.staffMasterButton.AutoSize = true;
            this.staffMasterButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.staffMasterButton.Location = new System.Drawing.Point(28, 50);
            this.staffMasterButton.Name = "staffMasterButton";
            this.staffMasterButton.Size = new System.Drawing.Size(850, 136);
            this.staffMasterButton.TabIndex = 1;
            this.staffMasterButton.Text = "担当者マスタ";
            this.staffMasterButton.UseVisualStyleBackColor = true;
            this.staffMasterButton.Click += new System.EventHandler(this.staffMasterButtonClick);
            // 
            // clientMasterButton
            // 
            this.clientMasterButton.AutoSize = true;
            this.clientMasterButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.clientMasterButton.Location = new System.Drawing.Point(28, 224);
            this.clientMasterButton.Name = "clientMasterButton";
            this.clientMasterButton.Size = new System.Drawing.Size(850, 162);
            this.clientMasterButton.TabIndex = 2;
            this.clientMasterButton.Text = "顧客マスタ";
            this.clientMasterButton.UseVisualStyleBackColor = true;
            this.clientMasterButton.Click += new System.EventHandler(this.clientMasterButton_Click);
            // 
            // itemMasterButton
            // 
            this.itemMasterButton.AutoSize = true;
            this.itemMasterButton.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.itemMasterButton.Location = new System.Drawing.Point(28, 430);
            this.itemMasterButton.Name = "itemMasterButton";
            this.itemMasterButton.Size = new System.Drawing.Size(850, 168);
            this.itemMasterButton.TabIndex = 3;
            this.itemMasterButton.Text = "品名マスタ";
            this.itemMasterButton.UseVisualStyleBackColor = true;
            this.itemMasterButton.Click += new System.EventHandler(this.itemMasterButtonClick);
            // 
            // TaxMaster
            // 
            this.TaxMaster.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TaxMaster.Location = new System.Drawing.Point(28, 648);
            this.TaxMaster.Name = "TaxMaster";
            this.TaxMaster.Size = new System.Drawing.Size(850, 167);
            this.TaxMaster.TabIndex = 6;
            this.TaxMaster.Text = "消費税マスタ";
            this.TaxMaster.UseVisualStyleBackColor = true;
            this.TaxMaster.Click += new System.EventHandler(this.TaxMaster_Click);
            // 
            // MasterMaintenanceMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 1050);
            this.Controls.Add(this.TaxMaster);
            this.Controls.Add(this.itemMasterButton);
            this.Controls.Add(this.clientMasterButton);
            this.Controls.Add(this.staffMasterButton);
            this.Controls.Add(this.CloseButton);
            this.Name = "MasterMaintenanceMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "マスタメンテナンスメニュー";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MasterMaintenanceMenu_FormClosed);
            this.Load += new System.EventHandler(this.MasterMaintenanceMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button staffMasterButton;
        private System.Windows.Forms.Button clientMasterButton;
        private System.Windows.Forms.Button itemMasterButton;
        private System.Windows.Forms.Button TaxMaster;
    }
}