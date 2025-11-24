namespace FloorMaterialEstimator
{
    partial class LoadingProgressControl
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
            this.progressBarAdv1 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.progressBarAdv1.Location = new System.Drawing.Point(304, 47);
            this.progressBarAdv1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBarAdv1.MaximumSize = new System.Drawing.Size(490, 23);
            this.progressBarAdv1.MinimumSize = new System.Drawing.Size(490, 23);
            this.progressBarAdv1.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.Name = "progressBarAdv1";
            this.progressBarAdv1.SegmentWidth = 12;
            this.progressBarAdv1.Size = new System.Drawing.Size(490, 23);
            this.progressBarAdv1.TabIndex = 1;
            this.progressBarAdv1.Text = "pgbLoadProgress";
            this.progressBarAdv1.Value = 0;
            this.progressBarAdv1.WaitingGradientWidth = 400;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(54, 47);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.MaximumSize = new System.Drawing.Size(190, 23);
            this.lblStatus.MinimumSize = new System.Drawing.Size(190, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(190, 23);
            this.lblStatus.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.MaximumSize = new System.Drawing.Size(790, 30);
            this.panel1.MinimumSize = new System.Drawing.Size(790, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 30);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.MaximumSize = new System.Drawing.Size(120, 13);
            this.label1.MinimumSize = new System.Drawing.Size(120, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading Large Project...";
            // 
            // LoadingProgressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBarAdv1);
            this.Name = "LoadingProgressControl";
            this.Size = new System.Drawing.Size(800, 100);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarAdv1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}
