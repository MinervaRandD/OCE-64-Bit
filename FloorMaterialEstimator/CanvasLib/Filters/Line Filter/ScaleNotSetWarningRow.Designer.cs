namespace CanvasLib.Filters.Line_Filter
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
            this.lblScaleNotSet = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblScaleNotSet
            // 
            this.lblScaleNotSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScaleNotSet.ForeColor = System.Drawing.Color.Red;
            this.lblScaleNotSet.Location = new System.Drawing.Point(3, 3);
            this.lblScaleNotSet.Name = "lblScaleNotSet";
            this.lblScaleNotSet.Size = new System.Drawing.Size(534, 18);
            this.lblScaleNotSet.TabIndex = 3;
            this.lblScaleNotSet.Text = "Scale Not Set";
            this.lblScaleNotSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScaleNotSetWarningRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblScaleNotSet);
            this.Name = "ScaleNotSetWarningRow";
            this.Size = new System.Drawing.Size(540, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblScaleNotSet;
    }
}
