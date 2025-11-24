namespace CanvasLib.Area_and_Seam_Views
{
    partial class UCRemnantsView
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
            this.lblWasteViewTitle = new System.Windows.Forms.Label();
            this.lblOverallWasteFactor = new System.Windows.Forms.Label();
            this.tbcRemant = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // lblWasteViewTitle
            // 
            this.lblWasteViewTitle.AutoSize = true;
            this.lblWasteViewTitle.Location = new System.Drawing.Point(20, 215);
            this.lblWasteViewTitle.Name = "lblWasteViewTitle";
            this.lblWasteViewTitle.Size = new System.Drawing.Size(86, 26);
            this.lblWasteViewTitle.TabIndex = 1;
            this.lblWasteViewTitle.Text = "Overall Remnant\r\nWaste Factor";
            // 
            // lblOverallWasteFactor
            // 
            this.lblOverallWasteFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOverallWasteFactor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOverallWasteFactor.Location = new System.Drawing.Point(118, 221);
            this.lblOverallWasteFactor.Name = "lblOverallWasteFactor";
            this.lblOverallWasteFactor.Size = new System.Drawing.Size(58, 19);
            this.lblOverallWasteFactor.TabIndex = 2;
            this.lblOverallWasteFactor.Text = "0.0%";
            this.lblOverallWasteFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbcRemant
            // 
            this.tbcRemant.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbcRemant.Location = new System.Drawing.Point(23, 14);
            this.tbcRemant.Name = "tbcRemant";
            this.tbcRemant.SelectedIndex = 0;
            this.tbcRemant.Size = new System.Drawing.Size(158, 193);
            this.tbcRemant.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbcRemant.TabIndex = 3;
            // 
            // UCRemnantsView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tbcRemant);
            this.Controls.Add(this.lblOverallWasteFactor);
            this.Controls.Add(this.lblWasteViewTitle);
            this.Name = "UCRemnantsView";
            this.Size = new System.Drawing.Size(210, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblWasteViewTitle;
        private System.Windows.Forms.Label lblOverallWasteFactor;
        private System.Windows.Forms.TabControl tbcRemant;
    }
}
