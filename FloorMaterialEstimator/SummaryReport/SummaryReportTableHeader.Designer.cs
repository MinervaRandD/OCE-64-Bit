namespace SummaryReport
{
    partial class AreaFilterTitleRow
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
            this.lblWastePct = new System.Windows.Forms.Label();
            this.lblGross = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWastePct
            // 
            this.lblWastePct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWastePct.Location = new System.Drawing.Point(475, -1);
            this.lblWastePct.Margin = new System.Windows.Forms.Padding(0);
            this.lblWastePct.Name = "lblWastePct";
            this.lblWastePct.Size = new System.Drawing.Size(61, 33);
            this.lblWastePct.TabIndex = 20;
            this.lblWastePct.Text = "Waste %";
            this.lblWastePct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGross
            // 
            this.lblGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGross.Location = new System.Drawing.Point(398, 4);
            this.lblGross.Margin = new System.Windows.Forms.Padding(0);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(75, 23);
            this.lblGross.TabIndex = 19;
            this.lblGross.Text = "Gross";
            this.lblGross.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNet
            // 
            this.lblNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNet.Location = new System.Drawing.Point(309, 4);
            this.lblNet.Margin = new System.Windows.Forms.Padding(0);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(75, 23);
            this.lblNet.TabIndex = 18;
            this.lblNet.Text = "Net";
            this.lblNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(74, 4);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(187, 23);
            this.lblTag.TabIndex = 17;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AreaFilterTitleRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblWastePct);
            this.Controls.Add(this.lblGross);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.lblTag);
            this.Name = "AreaFilterTitleRow";
            this.Size = new System.Drawing.Size(540, 34);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWastePct;
        private System.Windows.Forms.Label lblGross;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label lblTag;
    }
}
