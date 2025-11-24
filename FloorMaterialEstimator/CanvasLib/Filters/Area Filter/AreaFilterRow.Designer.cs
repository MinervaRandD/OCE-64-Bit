namespace CanvasLib.Filters.Area_Filter
{
    partial class AreaFilterRow
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
            this.lblWastePct = new System.Windows.Forms.Label();
            this.lblGross = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
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
            // lblWastePct
            // 
            this.lblWastePct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWastePct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWastePct.Location = new System.Drawing.Point(479, 0);
            this.lblWastePct.Margin = new System.Windows.Forms.Padding(0);
            this.lblWastePct.Name = "lblWastePct";
            this.lblWastePct.Size = new System.Drawing.Size(58, 23);
            this.lblWastePct.TabIndex = 16;
            this.lblWastePct.Text = "waste";
            this.lblWastePct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGross
            // 
            this.lblGross.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGross.Location = new System.Drawing.Point(389, 0);
            this.lblGross.Margin = new System.Windows.Forms.Padding(0);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(90, 23);
            this.lblGross.TabIndex = 15;
            this.lblGross.Text = "gross";
            this.lblGross.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNet
            // 
            this.lblNet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNet.Location = new System.Drawing.Point(299, 0);
            this.lblNet.Margin = new System.Windows.Forms.Padding(0);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(90, 23);
            this.lblNet.TabIndex = 14;
            this.lblNet.Text = "net";
            this.lblNet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTag
            // 
            this.lblTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(76, 0);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(223, 23);
            this.lblTag.TabIndex = 13;
            this.lblTag.Text = "tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbFinishFilter
            // 
            this.ckbFinishFilter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbFinishFilter.Checked = true;
            this.ckbFinishFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFinishFilter.Location = new System.Drawing.Point(9, 0);
            this.ckbFinishFilter.Margin = new System.Windows.Forms.Padding(0);
            this.ckbFinishFilter.Name = "ckbFinishFilter";
            this.ckbFinishFilter.Size = new System.Drawing.Size(22, 23);
            this.ckbFinishFilter.TabIndex = 12;
            this.ckbFinishFilter.UseVisualStyleBackColor = true;
            this.ckbFinishFilter.CheckedChanged += new System.EventHandler(this.ckbFinishFilter_CheckedChanged);
            // 
            // AreaFilterRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.lblWastePct);
            this.Controls.Add(this.lblGross);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.ckbFinishFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(540, 25);
            this.Name = "AreaFilterRow";
            this.Size = new System.Drawing.Size(540, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label lblWastePct;
        private System.Windows.Forms.Label lblGross;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label lblTag;
        public System.Windows.Forms.CheckBox ckbFinishFilter;
    }
}
