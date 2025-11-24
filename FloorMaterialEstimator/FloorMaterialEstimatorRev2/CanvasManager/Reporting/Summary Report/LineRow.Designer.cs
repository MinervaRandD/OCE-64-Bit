namespace CanvasManager.Reports.SummaryReport
{
    partial class LineRow
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
            this.lblType = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.ckbFinishFilter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pnlColor
            // 
            this.pnlColor.Location = new System.Drawing.Point(35, 0);
            this.pnlColor.Margin = new System.Windows.Forms.Padding(0);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(33, 23);
            this.pnlColor.TabIndex = 17;
            // 
            // lblType
            // 
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(409, 0);
            this.lblType.Margin = new System.Windows.Forms.Padding(0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(70, 21);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Line";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTag
            // 
            this.lblTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(175, 0);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(223, 21);
            this.lblTag.TabIndex = 13;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(68, 1);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(66, 21);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbFinishFilter
            // 
            this.ckbFinishFilter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbFinishFilter.Location = new System.Drawing.Point(9, 0);
            this.ckbFinishFilter.Margin = new System.Windows.Forms.Padding(0);
            this.ckbFinishFilter.MaximumSize = new System.Drawing.Size(22, 23);
            this.ckbFinishFilter.MinimumSize = new System.Drawing.Size(22, 23);
            this.ckbFinishFilter.Name = "ckbFinishFilter";
            this.ckbFinishFilter.Size = new System.Drawing.Size(22, 23);
            this.ckbFinishFilter.TabIndex = 20;
            this.ckbFinishFilter.UseVisualStyleBackColor = true;
            this.ckbFinishFilter.CheckedChanged += new System.EventHandler(this.ckbFinishFilter_CheckedChanged);
            // 
            // LineRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.ckbFinishFilter);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblTag);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1024, 21);
            this.MinimumSize = new System.Drawing.Size(1024, 21);
            this.Name = "LineRow";
            this.Size = new System.Drawing.Size(1024, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox ckbFinishFilter;
    }
}
