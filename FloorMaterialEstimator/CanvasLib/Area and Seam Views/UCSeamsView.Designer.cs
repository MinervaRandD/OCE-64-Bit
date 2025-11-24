namespace CanvasLib.Area_and_Seam_Views
{
    partial class UCSeamsView
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
            this.dgvSeams = new System.Windows.Forms.DataGridView();
            this.lblSeam = new System.Windows.Forms.Label();
            this.lblSeamsTotalLength = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeams)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSeams
            // 
            this.dgvSeams.AllowUserToAddRows = false;
            this.dgvSeams.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvSeams.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSeams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeams.ColumnHeadersVisible = false;
            this.dgvSeams.Location = new System.Drawing.Point(16, 38);
            this.dgvSeams.Name = "dgvSeams";
            this.dgvSeams.RowHeadersVisible = false;
            this.dgvSeams.Size = new System.Drawing.Size(181, 146);
            this.dgvSeams.TabIndex = 12;
            // 
            // lblSeam
            // 
            this.lblSeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeam.Location = new System.Drawing.Point(16, 7);
            this.lblSeam.Name = "lblSeam";
            this.lblSeam.Size = new System.Drawing.Size(181, 18);
            this.lblSeam.TabIndex = 13;
            this.lblSeam.Text = "Seam";
            this.lblSeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSeamsTotalLength
            // 
            this.lblSeamsTotalLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamsTotalLength.Location = new System.Drawing.Point(154, 201);
            this.lblSeamsTotalLength.Name = "lblSeamsTotalLength";
            this.lblSeamsTotalLength.Size = new System.Drawing.Size(46, 18);
            this.lblSeamsTotalLength.TabIndex = 15;
            this.lblSeamsTotalLength.Text = "100";
            this.lblSeamsTotalLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Seam Total Length";
            // 
            // UCSeamsView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblSeamsTotalLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSeam);
            this.Controls.Add(this.dgvSeams);
            this.Name = "UCSeamsView";
            this.Size = new System.Drawing.Size(212, 250);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvSeams;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblSeam;
        public System.Windows.Forms.Label lblSeamsTotalLength;
    }
}
