namespace CanvasManager.Reports.SummaryReport
{
    partial class CntrTitleRow
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
            this.lblSize = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSize
            // 
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(412, 0);
            this.lblSize.Margin = new System.Windows.Forms.Padding(0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(70, 20);
            this.lblSize.TabIndex = 14;
            this.lblSize.Text = "Size";
            this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(183, 0);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(223, 20);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(124, 1);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(53, 20);
            this.lblCount.TabIndex = 18;
            this.lblCount.Text = "Count";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(488, 1);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(66, 20);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(76, 1);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(42, 20);
            this.lblTag.TabIndex = 20;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CntrTitleRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDescription);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1024, 21);
            this.MinimumSize = new System.Drawing.Size(1024, 21);
            this.Name = "CntrTitleRow";
            this.Size = new System.Drawing.Size(1024, 21);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTag;
    }
}
