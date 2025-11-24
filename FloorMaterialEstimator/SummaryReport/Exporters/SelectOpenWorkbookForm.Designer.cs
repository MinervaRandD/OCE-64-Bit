
namespace SummaryReport.Exporters
{
    partial class SelectOpenWorkbookForm
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
            this.btnSelectWorkbook = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lsbAvailableWorkbooks = new System.Windows.Forms.ListBox();
            this.grbSelectWorkbook = new System.Windows.Forms.GroupBox();
            this.grbSelectWorksheet = new System.Windows.Forms.GroupBox();
            this.btnSelectWorksheet = new System.Windows.Forms.Button();
            this.txbSheetName = new System.Windows.Forms.TextBox();
            this.lsbAvailableWorksheets = new System.Windows.Forms.ListBox();
            this.grbSelectWorkbook.SuspendLayout();
            this.grbSelectWorksheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectWorkbook
            // 
            this.btnSelectWorkbook.Location = new System.Drawing.Point(64, 372);
            this.btnSelectWorkbook.Name = "btnSelectWorkbook";
            this.btnSelectWorkbook.Size = new System.Drawing.Size(108, 23);
            this.btnSelectWorkbook.TabIndex = 0;
            this.btnSelectWorkbook.Text = "Select Workbook";
            this.btnSelectWorkbook.UseVisualStyleBackColor = true;
            this.btnSelectWorkbook.Click += new System.EventHandler(this.btnSelectWorkbook_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 466);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lsbAvailableWorkbooks
            // 
            this.lsbAvailableWorkbooks.FormattingEnabled = true;
            this.lsbAvailableWorkbooks.HorizontalScrollbar = true;
            this.lsbAvailableWorkbooks.ItemHeight = 15;
            this.lsbAvailableWorkbooks.Location = new System.Drawing.Point(29, 37);
            this.lsbAvailableWorkbooks.Name = "lsbAvailableWorkbooks";
            this.lsbAvailableWorkbooks.Size = new System.Drawing.Size(174, 304);
            this.lsbAvailableWorkbooks.TabIndex = 2;
            // 
            // grbSelectWorkbook
            // 
            this.grbSelectWorkbook.Controls.Add(this.lsbAvailableWorkbooks);
            this.grbSelectWorkbook.Controls.Add(this.btnSelectWorkbook);
            this.grbSelectWorkbook.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSelectWorkbook.Location = new System.Drawing.Point(12, 15);
            this.grbSelectWorkbook.Name = "grbSelectWorkbook";
            this.grbSelectWorkbook.Size = new System.Drawing.Size(228, 423);
            this.grbSelectWorkbook.TabIndex = 3;
            this.grbSelectWorkbook.TabStop = false;
            this.grbSelectWorkbook.Text = "Select Open Workbook";
            // 
            // grbSelectWorksheet
            // 
            this.grbSelectWorksheet.Controls.Add(this.btnSelectWorksheet);
            this.grbSelectWorksheet.Controls.Add(this.txbSheetName);
            this.grbSelectWorksheet.Controls.Add(this.lsbAvailableWorksheets);
            this.grbSelectWorksheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSelectWorksheet.Location = new System.Drawing.Point(261, 15);
            this.grbSelectWorksheet.Name = "grbSelectWorksheet";
            this.grbSelectWorksheet.Size = new System.Drawing.Size(228, 423);
            this.grbSelectWorksheet.TabIndex = 4;
            this.grbSelectWorksheet.TabStop = false;
            this.grbSelectWorksheet.Text = "Select Worksheet";
            // 
            // btnSelectWorksheet
            // 
            this.btnSelectWorksheet.Location = new System.Drawing.Point(58, 372);
            this.btnSelectWorksheet.Name = "btnSelectWorksheet";
            this.btnSelectWorksheet.Size = new System.Drawing.Size(117, 23);
            this.btnSelectWorksheet.TabIndex = 4;
            this.btnSelectWorksheet.Text = "Select Worksheet";
            this.btnSelectWorksheet.UseVisualStyleBackColor = true;
            this.btnSelectWorksheet.Click += new System.EventHandler(this.btnSelectWorksheet_Click);
            // 
            // txbSheetName
            // 
            this.txbSheetName.Location = new System.Drawing.Point(20, 320);
            this.txbSheetName.Name = "txbSheetName";
            this.txbSheetName.Size = new System.Drawing.Size(180, 21);
            this.txbSheetName.TabIndex = 3;
            // 
            // lsbAvailableWorksheets
            // 
            this.lsbAvailableWorksheets.FormattingEnabled = true;
            this.lsbAvailableWorksheets.HorizontalScrollbar = true;
            this.lsbAvailableWorksheets.ItemHeight = 15;
            this.lsbAvailableWorksheets.Location = new System.Drawing.Point(26, 37);
            this.lsbAvailableWorksheets.Name = "lsbAvailableWorksheets";
            this.lsbAvailableWorksheets.Size = new System.Drawing.Size(174, 259);
            this.lsbAvailableWorksheets.TabIndex = 2;
            // 
            // SelectOpenWorkbookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 518);
            this.Controls.Add(this.grbSelectWorksheet);
            this.Controls.Add(this.grbSelectWorkbook);
            this.Controls.Add(this.btnCancel);
            this.Name = "SelectOpenWorkbookForm";
            this.Text = "Select Workbook and Worksheet";
            this.grbSelectWorkbook.ResumeLayout(false);
            this.grbSelectWorksheet.ResumeLayout(false);
            this.grbSelectWorksheet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectWorkbook;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lsbAvailableWorkbooks;
        private System.Windows.Forms.GroupBox grbSelectWorkbook;
        private System.Windows.Forms.GroupBox grbSelectWorksheet;
        private System.Windows.Forms.ListBox lsbAvailableWorksheets;
        private System.Windows.Forms.TextBox txbSheetName;
        private System.Windows.Forms.Button btnSelectWorksheet;
    }
}