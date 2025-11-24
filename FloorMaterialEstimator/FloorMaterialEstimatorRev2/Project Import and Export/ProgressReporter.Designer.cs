namespace FloorMaterialEstimator
{
    partial class ProgressReporter
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
            this.prgImportProgress = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            ((System.ComponentModel.ISupportInitialize)(this.prgImportProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // prgImportProgress
            // 
            this.prgImportProgress.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.prgImportProgress.BackSegments = false;
            this.prgImportProgress.CustomText = null;
            this.prgImportProgress.CustomWaitingRender = false;
            this.prgImportProgress.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.prgImportProgress.ForegroundImage = null;
            this.prgImportProgress.Location = new System.Drawing.Point(72, 5);
            this.prgImportProgress.MaximumSize = new System.Drawing.Size(400, 23);
            this.prgImportProgress.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.prgImportProgress.Name = "prgImportProgress";
            this.prgImportProgress.SegmentWidth = 12;
            this.prgImportProgress.Size = new System.Drawing.Size(400, 23);
            this.prgImportProgress.TabIndex = 0;
            this.prgImportProgress.Text = "progressBarAdv1";
            this.prgImportProgress.Value = 0;
            this.prgImportProgress.WaitingGradientWidth = 400;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.Location = new System.Drawing.Point(4, 7);
            this.autoLabel1.MaximumSize = new System.Drawing.Size(63, 19);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(63, 19);
            this.autoLabel1.TabIndex = 1;
            this.autoLabel1.Text = "Loading";
            // 
            // ProgressReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.prgImportProgress);
            this.MaximumSize = new System.Drawing.Size(500, 28);
            this.MinimumSize = new System.Drawing.Size(500, 28);
            this.Name = "ProgressReporter";
            this.Size = new System.Drawing.Size(496, 24);
            ((System.ComponentModel.ISupportInitialize)(this.prgImportProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv prgImportProgress;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
    }
}
