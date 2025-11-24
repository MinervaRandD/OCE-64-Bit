namespace TestDriver2
{
    partial class VisioExportImportBaseForm
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
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(245, 24);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(1078, 866);
            this.axDrawingControl.TabIndex = 5;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(39, 132);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(39, 81);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(39, 30);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnImport);
            this.panel1.Location = new System.Drawing.Point(36, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 851);
            this.panel1.TabIndex = 10;
            // 
            // VisioExportImportBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2134, 1261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "VisioExportImportBaseForm";
            this.Text = "Test Driver 2";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel1;
    }
}

