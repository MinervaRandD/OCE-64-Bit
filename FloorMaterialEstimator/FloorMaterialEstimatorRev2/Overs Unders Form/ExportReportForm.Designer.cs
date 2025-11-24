
namespace FloorMaterialEstimator.OversUndersForm
{
    partial class ExportReportForm
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
            this.ckbExportCuts = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbExportUnders = new System.Windows.Forms.CheckBox();
            this.ckbExportOvers = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnTSVFormat = new System.Windows.Forms.RadioButton();
            this.rbnFormattedTextFormat = new System.Windows.Forms.RadioButton();
            this.rbnCSVFormat = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckbIncludeHeader = new System.Windows.Forms.CheckBox();
            this.rbnCopyToClipboard = new System.Windows.Forms.RadioButton();
            this.rbnExportToFile = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbExportCuts
            // 
            this.ckbExportCuts.AutoSize = true;
            this.ckbExportCuts.Checked = true;
            this.ckbExportCuts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbExportCuts.Location = new System.Drawing.Point(30, 41);
            this.ckbExportCuts.Name = "ckbExportCuts";
            this.ckbExportCuts.Size = new System.Drawing.Size(47, 17);
            this.ckbExportCuts.TabIndex = 0;
            this.ckbExportCuts.Text = "Cuts";
            this.ckbExportCuts.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbExportUnders);
            this.groupBox1.Controls.Add(this.ckbExportOvers);
            this.groupBox1.Controls.Add(this.ckbExportCuts);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elements To Export";
            // 
            // ckbExportUnders
            // 
            this.ckbExportUnders.AutoSize = true;
            this.ckbExportUnders.Location = new System.Drawing.Point(180, 41);
            this.ckbExportUnders.Name = "ckbExportUnders";
            this.ckbExportUnders.Size = new System.Drawing.Size(60, 17);
            this.ckbExportUnders.TabIndex = 2;
            this.ckbExportUnders.Text = "Unders";
            this.ckbExportUnders.UseVisualStyleBackColor = true;
            // 
            // ckbExportOvers
            // 
            this.ckbExportOvers.AutoSize = true;
            this.ckbExportOvers.Location = new System.Drawing.Point(106, 41);
            this.ckbExportOvers.Name = "ckbExportOvers";
            this.ckbExportOvers.Size = new System.Drawing.Size(54, 17);
            this.ckbExportOvers.TabIndex = 1;
            this.ckbExportOvers.Text = "Overs";
            this.ckbExportOvers.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(65, 313);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(192, 313);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbnTSVFormat);
            this.groupBox2.Controls.Add(this.rbnFormattedTextFormat);
            this.groupBox2.Controls.Add(this.rbnCSVFormat);
            this.groupBox2.Location = new System.Drawing.Point(13, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 72);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Format";
            // 
            // rbnTSVFormat
            // 
            this.rbnTSVFormat.AutoSize = true;
            this.rbnTSVFormat.Location = new System.Drawing.Point(105, 29);
            this.rbnTSVFormat.Name = "rbnTSVFormat";
            this.rbnTSVFormat.Size = new System.Drawing.Size(46, 17);
            this.rbnTSVFormat.TabIndex = 2;
            this.rbnTSVFormat.Text = "TSV";
            this.rbnTSVFormat.UseVisualStyleBackColor = true;
            // 
            // rbnFormattedTextFormat
            // 
            this.rbnFormattedTextFormat.AutoSize = true;
            this.rbnFormattedTextFormat.Location = new System.Drawing.Point(179, 29);
            this.rbnFormattedTextFormat.Name = "rbnFormattedTextFormat";
            this.rbnFormattedTextFormat.Size = new System.Drawing.Size(96, 17);
            this.rbnFormattedTextFormat.TabIndex = 1;
            this.rbnFormattedTextFormat.Text = "Formatted Text";
            this.rbnFormattedTextFormat.UseVisualStyleBackColor = true;
            // 
            // rbnCSVFormat
            // 
            this.rbnCSVFormat.AutoSize = true;
            this.rbnCSVFormat.Checked = true;
            this.rbnCSVFormat.Location = new System.Drawing.Point(24, 29);
            this.rbnCSVFormat.Name = "rbnCSVFormat";
            this.rbnCSVFormat.Size = new System.Drawing.Size(46, 17);
            this.rbnCSVFormat.TabIndex = 0;
            this.rbnCSVFormat.TabStop = true;
            this.rbnCSVFormat.Text = "CSV";
            this.rbnCSVFormat.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckbIncludeHeader);
            this.groupBox3.Controls.Add(this.rbnCopyToClipboard);
            this.groupBox3.Controls.Add(this.rbnExportToFile);
            this.groupBox3.Location = new System.Drawing.Point(13, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 72);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export To";
            // 
            // ckbIncludeHeader
            // 
            this.ckbIncludeHeader.AutoSize = true;
            this.ckbIncludeHeader.Location = new System.Drawing.Point(179, 30);
            this.ckbIncludeHeader.Name = "ckbIncludeHeader";
            this.ckbIncludeHeader.Size = new System.Drawing.Size(104, 17);
            this.ckbIncludeHeader.TabIndex = 3;
            this.ckbIncludeHeader.Text = "Include Headers";
            this.ckbIncludeHeader.UseVisualStyleBackColor = true;
            // 
            // rbnCopyToClipboard
            // 
            this.rbnCopyToClipboard.AutoSize = true;
            this.rbnCopyToClipboard.Location = new System.Drawing.Point(90, 29);
            this.rbnCopyToClipboard.Name = "rbnCopyToClipboard";
            this.rbnCopyToClipboard.Size = new System.Drawing.Size(69, 17);
            this.rbnCopyToClipboard.TabIndex = 2;
            this.rbnCopyToClipboard.Text = "Clipboard";
            this.rbnCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // rbnExportToFile
            // 
            this.rbnExportToFile.AutoSize = true;
            this.rbnExportToFile.Checked = true;
            this.rbnExportToFile.Location = new System.Drawing.Point(29, 29);
            this.rbnExportToFile.Name = "rbnExportToFile";
            this.rbnExportToFile.Size = new System.Drawing.Size(41, 17);
            this.rbnExportToFile.TabIndex = 0;
            this.rbnExportToFile.TabStop = true;
            this.rbnExportToFile.Text = "File";
            this.rbnExportToFile.UseVisualStyleBackColor = true;
            // 
            // ExportReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 373);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ExportReportForm";
            this.Text = "Export Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbExportCuts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbExportUnders;
        private System.Windows.Forms.CheckBox ckbExportOvers;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnTSVFormat;
        private System.Windows.Forms.RadioButton rbnFormattedTextFormat;
        private System.Windows.Forms.RadioButton rbnCSVFormat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbnCopyToClipboard;
        private System.Windows.Forms.RadioButton rbnExportToFile;
        private System.Windows.Forms.CheckBox ckbIncludeHeader;
    }
}