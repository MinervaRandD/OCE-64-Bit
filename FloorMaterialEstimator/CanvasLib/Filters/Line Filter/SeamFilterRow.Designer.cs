namespace CanvasLib.Filters.Line_Filter
{
    partial class SeamFilterRow
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
            this.ckbFinishFilter = new System.Windows.Forms.CheckBox();
            this.pnlSeamType = new System.Windows.Forms.Panel();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ckbFinishFilter
            // 
            this.ckbFinishFilter.AutoSize = true;
            this.ckbFinishFilter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbFinishFilter.Location = new System.Drawing.Point(4, 5);
            this.ckbFinishFilter.Name = "ckbFinishFilter";
            this.ckbFinishFilter.Size = new System.Drawing.Size(15, 14);
            this.ckbFinishFilter.TabIndex = 0;
            this.ckbFinishFilter.UseVisualStyleBackColor = true;
            // 
            // pnlLineType
            // 
            this.pnlSeamType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSeamType.Location = new System.Drawing.Point(31, 3);
            this.pnlSeamType.Name = "pnlLineType";
            this.pnlSeamType.Size = new System.Drawing.Size(210, 18);
            this.pnlSeamType.TabIndex = 1;
            // 
            // lblTag
            // 
            this.lblTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(238, 3);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(207, 18);
            this.lblTag.TabIndex = 2;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(442, 3);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(95, 18);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LineFilterRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.pnlSeamType);
            this.Controls.Add(this.ckbFinishFilter);
            this.Name = "LineFilterRow";
            this.Size = new System.Drawing.Size(540, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbFinishFilter;
        private System.Windows.Forms.Panel pnlSeamType;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblTotal;
    }
}
