namespace SummaryReport
{
    partial class CntrRow
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
            this.pnlColor = new System.Windows.Forms.Panel();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.ckbFinishFilter = new System.Windows.Forms.CheckBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlColor
            // 
            this.pnlColor.Location = new System.Drawing.Point(37, 0);
            this.pnlColor.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(33, 23);
            this.pnlColor.TabIndex = 17;
            // 
            // lblSize
            // 
            this.lblSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(412, 0);
            this.lblSize.Margin = new System.Windows.Forms.Padding(0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(70, 20);
            this.lblSize.TabIndex = 14;
            this.lblSize.Text = "Size";
            this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(183, 0);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(223, 20);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbFinishFilter
            // 
            this.ckbFinishFilter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbFinishFilter.Location = new System.Drawing.Point(9, 0);
            this.ckbFinishFilter.Margin = new System.Windows.Forms.Padding(0);
            this.ckbFinishFilter.MaximumSize = new System.Drawing.Size(22, 20);
            this.ckbFinishFilter.MinimumSize = new System.Drawing.Size(22, 20);
            this.ckbFinishFilter.Name = "ckbFinishFilter";
            this.ckbFinishFilter.Size = new System.Drawing.Size(22, 20);
            this.ckbFinishFilter.TabIndex = 12;
            this.ckbFinishFilter.UseVisualStyleBackColor = true;
            this.ckbFinishFilter.CheckedChanged += new System.EventHandler(this.ckbFinishFilter_CheckedChanged);
            // 
            // lblCount
            // 
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(124, 1);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(53, 20);
            this.lblCount.TabIndex = 18;
            this.lblCount.Text = "Count";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(488, 1);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(66, 20);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTag
            // 
            this.lblTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(76, 1);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(42, 20);
            this.lblTag.TabIndex = 20;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CounterRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.ckbFinishFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1024, 21);
            this.MinimumSize = new System.Drawing.Size(1024, 21);
            this.Name = "CounterRow";
            this.Size = new System.Drawing.Size(1024, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckBox ckbFinishFilter;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTag;
    }
}
