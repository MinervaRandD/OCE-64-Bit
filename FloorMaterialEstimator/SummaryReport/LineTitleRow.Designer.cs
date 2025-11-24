namespace SummaryReport
{
    partial class LineTitleRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblType = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblTotalLF = new System.Windows.Forms.Label();
            this.lblTotalSF = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(409, 0);
            this.lblType.Margin = new System.Windows.Forms.Padding(0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(70, 23);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(175, 0);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(223, 23);
            this.lblTag.TabIndex = 13;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalLF
            // 
            this.lblTotalLF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLF.Location = new System.Drawing.Point(68, 1);
            this.lblTotalLF.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalLF.Name = "lblTotalLF";
            this.lblTotalLF.Size = new System.Drawing.Size(66, 23);
            this.lblTotalLF.TabIndex = 18;
            this.lblTotalLF.Text = "Total L.F.";
            this.lblTotalLF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalSF
            // 
            this.lblTotalSF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSF.Location = new System.Drawing.Point(156, 1);
            this.lblTotalSF.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalSF.Name = "lblTotalSF";
            this.lblTotalSF.Size = new System.Drawing.Size(66, 23);
            this.lblTotalSF.TabIndex = 19;
            this.lblTotalSF.Text = "Total S.F.";
            this.lblTotalSF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LineTitleRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.lblTotalSF);
            this.Controls.Add(this.lblTotalLF);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblTag);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1024, 25);
            this.MinimumSize = new System.Drawing.Size(1024, 24);
            this.Name = "LineTitleRow";
            this.Size = new System.Drawing.Size(1024, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblTotalLF;
        private System.Windows.Forms.Label lblTotalSF;
    }
}
