
namespace SummaryReport.Exporters
{
    partial class TextExportFullReportForm
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
            this.grbFilters = new System.Windows.Forms.GroupBox();
            this.ckbCntrs = new System.Windows.Forms.CheckBox();
            this.ckbSeams = new System.Windows.Forms.CheckBox();
            this.ckbLines = new System.Windows.Forms.CheckBox();
            this.ckbAreas = new System.Windows.Forms.CheckBox();
            this.ckbCheckedItemsOnly = new System.Windows.Forms.CheckBox();
            this.ckbNonZeroItemsOnly = new System.Windows.Forms.CheckBox();
            this.grbOutputFormat = new System.Windows.Forms.GroupBox();
            this.txbOtherSeparator = new System.Windows.Forms.TextBox();
            this.rbnColumnFormatted = new System.Windows.Forms.RadioButton();
            this.rbnOtherSeparator = new System.Windows.Forms.RadioButton();
            this.rbnTSV = new System.Windows.Forms.RadioButton();
            this.rbnCSV = new System.Windows.Forms.RadioButton();
            this.grbExportTo = new System.Windows.Forms.GroupBox();
            this.rbnExportToClipboard = new System.Windows.Forms.RadioButton();
            this.rbnExportToFile = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.ckbIncludeHeaders = new System.Windows.Forms.CheckBox();
            this.ckbIncludeTotals = new System.Windows.Forms.CheckBox();
            this.grbFilters.SuspendLayout();
            this.grbOutputFormat.SuspendLayout();
            this.grbExportTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbFilters
            // 
            this.grbFilters.Controls.Add(this.ckbCntrs);
            this.grbFilters.Controls.Add(this.ckbSeams);
            this.grbFilters.Controls.Add(this.ckbLines);
            this.grbFilters.Controls.Add(this.ckbAreas);
            this.grbFilters.Controls.Add(this.ckbCheckedItemsOnly);
            this.grbFilters.Controls.Add(this.ckbNonZeroItemsOnly);
            this.grbFilters.Location = new System.Drawing.Point(31, 28);
            this.grbFilters.Name = "grbFilters";
            this.grbFilters.Size = new System.Drawing.Size(353, 127);
            this.grbFilters.TabIndex = 1;
            this.grbFilters.TabStop = false;
            this.grbFilters.Text = "Filters";
            // 
            // ckbCntrs
            // 
            this.ckbCntrs.AutoSize = true;
            this.ckbCntrs.Location = new System.Drawing.Point(263, 34);
            this.ckbCntrs.Name = "ckbCntrs";
            this.ckbCntrs.Size = new System.Drawing.Size(68, 17);
            this.ckbCntrs.TabIndex = 5;
            this.ckbCntrs.Text = "Counters";
            this.ckbCntrs.UseVisualStyleBackColor = true;
            // 
            // ckbSeams
            // 
            this.ckbSeams.AutoSize = true;
            this.ckbSeams.Location = new System.Drawing.Point(183, 34);
            this.ckbSeams.Name = "ckbSeams";
            this.ckbSeams.Size = new System.Drawing.Size(58, 17);
            this.ckbSeams.TabIndex = 4;
            this.ckbSeams.Text = "Seams";
            this.ckbSeams.UseVisualStyleBackColor = true;
            // 
            // ckbLines
            // 
            this.ckbLines.AutoSize = true;
            this.ckbLines.Location = new System.Drawing.Point(110, 34);
            this.ckbLines.Name = "ckbLines";
            this.ckbLines.Size = new System.Drawing.Size(51, 17);
            this.ckbLines.TabIndex = 3;
            this.ckbLines.Text = "Lines";
            this.ckbLines.UseVisualStyleBackColor = true;
            // 
            // ckbAreas
            // 
            this.ckbAreas.AutoSize = true;
            this.ckbAreas.Location = new System.Drawing.Point(35, 34);
            this.ckbAreas.Name = "ckbAreas";
            this.ckbAreas.Size = new System.Drawing.Size(53, 17);
            this.ckbAreas.TabIndex = 2;
            this.ckbAreas.Text = "Areas";
            this.ckbAreas.UseVisualStyleBackColor = true;
            // 
            // ckbCheckedItemsOnly
            // 
            this.ckbCheckedItemsOnly.AutoSize = true;
            this.ckbCheckedItemsOnly.Location = new System.Drawing.Point(193, 84);
            this.ckbCheckedItemsOnly.Name = "ckbCheckedItemsOnly";
            this.ckbCheckedItemsOnly.Size = new System.Drawing.Size(121, 17);
            this.ckbCheckedItemsOnly.TabIndex = 1;
            this.ckbCheckedItemsOnly.Text = "Checked Items Only";
            this.ckbCheckedItemsOnly.UseVisualStyleBackColor = true;
            // 
            // ckbNonZeroItemsOnly
            // 
            this.ckbNonZeroItemsOnly.AutoSize = true;
            this.ckbNonZeroItemsOnly.Location = new System.Drawing.Point(40, 84);
            this.ckbNonZeroItemsOnly.Name = "ckbNonZeroItemsOnly";
            this.ckbNonZeroItemsOnly.Size = new System.Drawing.Size(121, 17);
            this.ckbNonZeroItemsOnly.TabIndex = 0;
            this.ckbNonZeroItemsOnly.Text = "Non-zero Items Only";
            this.ckbNonZeroItemsOnly.UseVisualStyleBackColor = true;
            // 
            // grbOutputFormat
            // 
            this.grbOutputFormat.Controls.Add(this.ckbIncludeHeaders);
            this.grbOutputFormat.Controls.Add(this.ckbIncludeTotals);
            this.grbOutputFormat.Controls.Add(this.txbOtherSeparator);
            this.grbOutputFormat.Controls.Add(this.rbnColumnFormatted);
            this.grbOutputFormat.Controls.Add(this.rbnOtherSeparator);
            this.grbOutputFormat.Controls.Add(this.rbnTSV);
            this.grbOutputFormat.Controls.Add(this.rbnCSV);
            this.grbOutputFormat.Location = new System.Drawing.Point(31, 185);
            this.grbOutputFormat.Name = "grbOutputFormat";
            this.grbOutputFormat.Size = new System.Drawing.Size(353, 129);
            this.grbOutputFormat.TabIndex = 2;
            this.grbOutputFormat.TabStop = false;
            this.grbOutputFormat.Text = "Output Format";
            // 
            // txbOtherSeparator
            // 
            this.txbOtherSeparator.Location = new System.Drawing.Point(210, 64);
            this.txbOtherSeparator.Name = "txbOtherSeparator";
            this.txbOtherSeparator.Size = new System.Drawing.Size(52, 20);
            this.txbOtherSeparator.TabIndex = 4;
            // 
            // rbnColumnFormatted
            // 
            this.rbnColumnFormatted.AutoSize = true;
            this.rbnColumnFormatted.Location = new System.Drawing.Point(221, 30);
            this.rbnColumnFormatted.Name = "rbnColumnFormatted";
            this.rbnColumnFormatted.Size = new System.Drawing.Size(110, 17);
            this.rbnColumnFormatted.TabIndex = 3;
            this.rbnColumnFormatted.TabStop = true;
            this.rbnColumnFormatted.Text = "Column Formatted";
            this.rbnColumnFormatted.UseVisualStyleBackColor = true;
            // 
            // rbnOtherSeparator
            // 
            this.rbnOtherSeparator.AutoSize = true;
            this.rbnOtherSeparator.Location = new System.Drawing.Point(88, 64);
            this.rbnOtherSeparator.Name = "rbnOtherSeparator";
            this.rbnOtherSeparator.Size = new System.Drawing.Size(100, 17);
            this.rbnOtherSeparator.TabIndex = 2;
            this.rbnOtherSeparator.TabStop = true;
            this.rbnOtherSeparator.Text = "Other Separator";
            this.rbnOtherSeparator.UseVisualStyleBackColor = true;
            // 
            // rbnTSV
            // 
            this.rbnTSV.AutoSize = true;
            this.rbnTSV.Location = new System.Drawing.Point(142, 30);
            this.rbnTSV.Name = "rbnTSV";
            this.rbnTSV.Size = new System.Drawing.Size(46, 17);
            this.rbnTSV.TabIndex = 1;
            this.rbnTSV.TabStop = true;
            this.rbnTSV.Text = "TSV";
            this.rbnTSV.UseVisualStyleBackColor = true;
            // 
            // rbnCSV
            // 
            this.rbnCSV.AutoSize = true;
            this.rbnCSV.Location = new System.Drawing.Point(35, 30);
            this.rbnCSV.Name = "rbnCSV";
            this.rbnCSV.Size = new System.Drawing.Size(46, 17);
            this.rbnCSV.TabIndex = 0;
            this.rbnCSV.TabStop = true;
            this.rbnCSV.Text = "CSV";
            this.rbnCSV.UseVisualStyleBackColor = true;
            // 
            // grbExportTo
            // 
            this.grbExportTo.Controls.Add(this.rbnExportToClipboard);
            this.grbExportTo.Controls.Add(this.rbnExportToFile);
            this.grbExportTo.Location = new System.Drawing.Point(31, 320);
            this.grbExportTo.Name = "grbExportTo";
            this.grbExportTo.Size = new System.Drawing.Size(353, 83);
            this.grbExportTo.TabIndex = 5;
            this.grbExportTo.TabStop = false;
            this.grbExportTo.Text = "Export To";
            // 
            // rbnExportToClipboard
            // 
            this.rbnExportToClipboard.AutoSize = true;
            this.rbnExportToClipboard.Location = new System.Drawing.Point(195, 30);
            this.rbnExportToClipboard.Name = "rbnExportToClipboard";
            this.rbnExportToClipboard.Size = new System.Drawing.Size(118, 17);
            this.rbnExportToClipboard.TabIndex = 1;
            this.rbnExportToClipboard.TabStop = true;
            this.rbnExportToClipboard.Text = "Export To Clipboard";
            this.rbnExportToClipboard.UseVisualStyleBackColor = true;
            // 
            // rbnExportToFile
            // 
            this.rbnExportToFile.AutoSize = true;
            this.rbnExportToFile.Location = new System.Drawing.Point(40, 30);
            this.rbnExportToFile.Name = "rbnExportToFile";
            this.rbnExportToFile.Size = new System.Drawing.Size(90, 17);
            this.rbnExportToFile.TabIndex = 0;
            this.rbnExportToFile.TabStop = true;
            this.rbnExportToFile.Text = "Export To File";
            this.rbnExportToFile.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(173, 429);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ckbIncludeHeaders
            // 
            this.ckbIncludeHeaders.AutoSize = true;
            this.ckbIncludeHeaders.Location = new System.Drawing.Point(188, 99);
            this.ckbIncludeHeaders.Name = "ckbIncludeHeaders";
            this.ckbIncludeHeaders.Size = new System.Drawing.Size(104, 17);
            this.ckbIncludeHeaders.TabIndex = 8;
            this.ckbIncludeHeaders.Text = "Include Headers";
            this.ckbIncludeHeaders.UseVisualStyleBackColor = true;
            // 
            // ckbIncludeTotals
            // 
            this.ckbIncludeTotals.AutoSize = true;
            this.ckbIncludeTotals.Location = new System.Drawing.Point(35, 99);
            this.ckbIncludeTotals.Name = "ckbIncludeTotals";
            this.ckbIncludeTotals.Size = new System.Drawing.Size(93, 17);
            this.ckbIncludeTotals.TabIndex = 7;
            this.ckbIncludeTotals.Text = "Include Totals";
            this.ckbIncludeTotals.UseVisualStyleBackColor = true;
            // 
            // TextExportFullReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 481);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.grbExportTo);
            this.Controls.Add(this.grbOutputFormat);
            this.Controls.Add(this.grbFilters);
            this.Name = "TextExportFullReportForm";
            this.Text = "Export Report To Text";
            this.grbFilters.ResumeLayout(false);
            this.grbFilters.PerformLayout();
            this.grbOutputFormat.ResumeLayout(false);
            this.grbOutputFormat.PerformLayout();
            this.grbExportTo.ResumeLayout(false);
            this.grbExportTo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbFilters;
        private System.Windows.Forms.CheckBox ckbCntrs;
        private System.Windows.Forms.CheckBox ckbSeams;
        private System.Windows.Forms.CheckBox ckbLines;
        private System.Windows.Forms.CheckBox ckbAreas;
        private System.Windows.Forms.CheckBox ckbCheckedItemsOnly;
        private System.Windows.Forms.CheckBox ckbNonZeroItemsOnly;
        private System.Windows.Forms.GroupBox grbOutputFormat;
        private System.Windows.Forms.TextBox txbOtherSeparator;
        private System.Windows.Forms.RadioButton rbnColumnFormatted;
        private System.Windows.Forms.RadioButton rbnOtherSeparator;
        private System.Windows.Forms.RadioButton rbnTSV;
        private System.Windows.Forms.RadioButton rbnCSV;
        private System.Windows.Forms.GroupBox grbExportTo;
        private System.Windows.Forms.RadioButton rbnExportToClipboard;
        private System.Windows.Forms.RadioButton rbnExportToFile;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox ckbIncludeHeaders;
        private System.Windows.Forms.CheckBox ckbIncludeTotals;
    }
}