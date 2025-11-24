namespace CanvasLib.Filters.Area_Filter
{
    partial class ScaleNotSetWarningRow
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
            this.lblTag = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.ForeColor = System.Drawing.Color.Red;
            this.lblTag.Location = new System.Drawing.Point(76, 1);
            this.lblTag.Margin = new System.Windows.Forms.Padding(0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(455, 23);
            this.lblTag.TabIndex = 19;
            this.lblTag.Text = "Scale Has Not Been Set";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScaleNotSetWarningRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTag);
            this.Name = "ScaleNotSetWarningRow";
            this.Size = new System.Drawing.Size(540, 25);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblTag;
    }
}
