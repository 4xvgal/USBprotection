namespace USBprotect.SettingExportImport.form
{
    partial class SettingExportImport
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
            this.ExportDirTextBox = new System.Windows.Forms.TextBox();
            this.Export = new System.Windows.Forms.Button();
            this.Import = new System.Windows.Forms.Button();
            this.btnBrowseImport = new System.Windows.Forms.Button();
            this.btnBrowseExport = new System.Windows.Forms.Button();
            this.ImportDirTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExportDirTextBox
            // 
            this.ExportDirTextBox.Location = new System.Drawing.Point(65, 35);
            this.ExportDirTextBox.Name = "ExportDirTextBox";
            this.ExportDirTextBox.Size = new System.Drawing.Size(346, 32);
            this.ExportDirTextBox.TabIndex = 3;
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(561, 37);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(114, 30);
            this.Export.TabIndex = 4;
            this.Export.Text = "내보내기";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Import
            // 
            this.Import.Location = new System.Drawing.Point(561, 103);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(114, 33);
            this.Import.TabIndex = 5;
            this.Import.Text = "불러오기";
            this.Import.UseVisualStyleBackColor = true;
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // btnBrowseImport
            // 
            this.btnBrowseImport.Location = new System.Drawing.Point(417, 108);
            this.btnBrowseImport.Name = "btnBrowseImport";
            this.btnBrowseImport.Size = new System.Drawing.Size(114, 30);
            this.btnBrowseImport.TabIndex = 8;
            this.btnBrowseImport.Text = "파일 찾기";
            this.btnBrowseImport.UseVisualStyleBackColor = true;
            this.btnBrowseImport.Click += new System.EventHandler(this.btnBrowseImport_Click);
            // 
            // btnBrowseExport
            // 
            this.btnBrowseExport.Location = new System.Drawing.Point(417, 37);
            this.btnBrowseExport.Name = "btnBrowseExport";
            this.btnBrowseExport.Size = new System.Drawing.Size(114, 30);
            this.btnBrowseExport.TabIndex = 9;
            this.btnBrowseExport.Text = "파일 찾기";
            this.btnBrowseExport.UseVisualStyleBackColor = true;
            this.btnBrowseExport.Click += new System.EventHandler(this.btnBrowseExport_Click);
            // 
            // ImportDirTextBox
            // 
            this.ImportDirTextBox.Location = new System.Drawing.Point(65, 106);
            this.ImportDirTextBox.Name = "ImportDirTextBox";
            this.ImportDirTextBox.Size = new System.Drawing.Size(346, 32);
            this.ImportDirTextBox.TabIndex = 10;
            this.ImportDirTextBox.TextChanged += new System.EventHandler(this.ImportDirTextBox_TextChanged);
            // 
            // SettingExportImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 199);
            this.Controls.Add(this.ImportDirTextBox);
            this.Controls.Add(this.btnBrowseExport);
            this.Controls.Add(this.btnBrowseImport);
            this.Controls.Add(this.Import);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.ExportDirTextBox);
            this.Name = "SettingExportImport";
            this.Text = "SettingExportImport";
            this.Load += new System.EventHandler(this.SettingExportImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ExportDirTextBox;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Button Import;
        private System.Windows.Forms.Button btnBrowseImport;
        private System.Windows.Forms.Button btnBrowseExport;
        private System.Windows.Forms.TextBox ImportDirTextBox;
    }
}