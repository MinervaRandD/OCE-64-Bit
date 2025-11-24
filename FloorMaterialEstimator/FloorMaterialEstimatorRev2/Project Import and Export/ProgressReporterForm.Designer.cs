namespace FloorMaterialEstimator
{
    partial class ProgressReporterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBarAdv1 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarAdv1
            // 
            this.progressBarAdv1.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.BackSegments = false;
            this.progressBarAdv1.CustomText = null;
            this.progressBarAdv1.CustomWaitingRender = false;
            this.progressBarAdv1.ForegroundImage = null;
            this.progressBarAdv1.Location = new System.Drawing.Point(276, 21);
            this.progressBarAdv1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBarAdv1.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.Name = "progressBarAdv1";
            this.progressBarAdv1.SegmentWidth = 12;
            this.progressBarAdv1.Size = new System.Drawing.Size(467, 23);
            this.progressBarAdv1.TabIndex = 0;
            this.progressBarAdv1.Text = "pgbLoadProgress";
            this.progressBarAdv1.Value = 0;
            this.progressBarAdv1.WaitingGradientWidth = 400;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(41, 21);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(186, 23);
            this.lblStatus.TabIndex = 1;
            // 
            // ProgressReporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 61);
            this.ControlBox = false;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBarAdv1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ProgressReporterForm";
            this.Text = "Loading Large Project...";
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarAdv1;
        private System.Windows.Forms.Label lblStatus;
    }
}