
namespace SummaryReport.Exporters
{
    partial class ExcelExportFullReportForm
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
            this.rbnExportToOpenWorkbook = new System.Windows.Forms.RadioButton();
            this.rbnCreateNewWorkbook = new System.Windows.Forms.RadioButton();
            this.grbDestination = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.grbFormat = new System.Windows.Forms.GroupBox();
            this.ckbIncludeHeaders = new System.Windows.Forms.CheckBox();
            this.ckbIncludeTotals = new System.Windows.Forms.CheckBox();
            this.grbFilters.SuspendLayout();
            this.grbDestination.SuspendLayout();
            this.grbFormat.SuspendLayout();
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
            this.grbFilters.Location = new System.Drawing.Point(43, 21);
            this.grbFilters.Name = "grbFilters";
            this.grbFilters.Size = new System.Drawing.Size(353, 127);
            this.grbFilters.TabIndex = 0;
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
            this.ckbLines.Checked = true;
            this.ckbLines.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.ckbAreas.Checked = true;
            this.ckbAreas.CheckState = System.Windows.Forms.CheckState.Checked;
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
            // rbnExportToOpenWorkbook
            // 
            this.rbnExportToOpenWorkbook.AutoSize = true;
            this.rbnExportToOpenWorkbook.Checked = true;
            this.rbnExportToOpenWorkbook.Location = new System.Drawing.Point(20, 28);
            this.rbnExportToOpenWorkbook.Name = "rbnExportToOpenWorkbook";
            this.rbnExportToOpenWorkbook.Size = new System.Drawing.Size(153, 17);
            this.rbnExportToOpenWorkbook.TabIndex = 1;
            this.rbnExportToOpenWorkbook.TabStop = true;
            this.rbnExportToOpenWorkbook.Text = "Export To Open Workbook";
            this.rbnExportToOpenWorkbook.UseVisualStyleBackColor = true;
            // 
            // rbnCreateNewWorkbook
            // 
            this.rbnCreateNewWorkbook.AutoSize = true;
            this.rbnCreateNewWorkbook.Enabled = true;
            this.rbnCreateNewWorkbook.Location = new System.Drawing.Point(197, 28);
            this.rbnCreateNewWorkbook.Name = "rbnCreateNewWorkbook";
            this.rbnCreateNewWorkbook.Size = new System.Drawing.Size(134, 17);
            this.rbnCreateNewWorkbook.TabIndex = 3;
            this.rbnCreateNewWorkbook.Text = "Create New Workbook";
            this.rbnCreateNewWorkbook.UseVisualStyleBackColor = true;
            // 
            // grbDestination
            // 
            this.grbDestination.Controls.Add(this.rbnExportToOpenWorkbook);
            this.grbDestination.Controls.Add(this.rbnCreateNewWorkbook);
            this.grbDestination.Location = new System.Drawing.Point(43, 161);
            this.grbDestination.Name = "grbDestination";
            this.grbDestination.Size = new System.Drawing.Size(353, 67);
            this.grbDestination.TabIndex = 4;
            this.grbDestination.TabStop = false;
            this.grbDestination.Text = "Destination";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(175, 333);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // grbFormat
            // 
            this.grbFormat.Controls.Add(this.ckbIncludeHeaders);
            this.grbFormat.Controls.Add(this.ckbIncludeTotals);
            this.grbFormat.Location = new System.Drawing.Point(47, 250);
            this.grbFormat.Name = "grbFormat";
            this.grbFormat.Size = new System.Drawing.Size(349, 52);
            this.grbFormat.TabIndex = 6;
            this.grbFormat.TabStop = false;
            this.grbFormat.Text = "Format";
            // 
            // ckbIncludeHeaders
            // 
            this.ckbIncludeHeaders.AutoSize = true;
            this.ckbIncludeHeaders.Location = new System.Drawing.Point(190, 18);
            this.ckbIncludeHeaders.Name = "ckbIncludeHeaders";
            this.ckbIncludeHeaders.Size = new System.Drawing.Size(104, 17);
            this.ckbIncludeHeaders.TabIndex = 3;
            this.ckbIncludeHeaders.Text = "Include Headers";
            this.ckbIncludeHeaders.UseVisualStyleBackColor = true;
            // 
            // ckbIncludeTotals
            // 
            this.ckbIncludeTotals.AutoSize = true;
            this.ckbIncludeTotals.Location = new System.Drawing.Point(37, 18);
            this.ckbIncludeTotals.Name = "ckbIncludeTotals";
            this.ckbIncludeTotals.Size = new System.Drawing.Size(93, 17);
            this.ckbIncludeTotals.TabIndex = 2;
            this.ckbIncludeTotals.Text = "Include Totals";
            this.ckbIncludeTotals.UseVisualStyleBackColor = true;
            // 
            // ExcelExportFullReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 375);
            this.Controls.Add(this.grbFormat);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.grbDestination);
            this.Controls.Add(this.grbFilters);
            this.Name = "ExcelExportFullReportForm";
            this.Text = "Export Report To Excel";
            this.grbFilters.ResumeLayout(false);
            this.grbFilters.PerformLayout();
            this.grbDestination.ResumeLayout(false);
            this.grbDestination.PerformLayout();
            this.grbFormat.ResumeLayout(false);
            this.grbFormat.PerformLayout();
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
        private System.Windows.Forms.RadioButton rbnExportToOpenWorkbook;
        private System.Windows.Forms.RadioButton rbnCreateNewWorkbook;
        private System.Windows.Forms.GroupBox grbDestination;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox grbFormat;
        private System.Windows.Forms.CheckBox ckbIncludeHeaders;
        private System.Windows.Forms.CheckBox ckbIncludeTotals;
    }
}