namespace CanvasLib.Area_and_Seam_Views
{
    partial class UCAreasView
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
            this.dgvAreas = new System.Windows.Forms.DataGridView();
            this.lblFinish = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFinishTotalArea = new System.Windows.Forms.Label();
            this.lblPctAreaSelected = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAreas
            // 
            this.dgvAreas.AllowUserToAddRows = false;
            this.dgvAreas.AllowUserToResizeColumns = false;
            this.dgvAreas.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvAreas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAreas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAreas.Location = new System.Drawing.Point(6, 29);
            this.dgvAreas.Name = "dgvAreas";
            this.dgvAreas.RowHeadersVisible = false;
            this.dgvAreas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAreas.Size = new System.Drawing.Size(200, 168);
            this.dgvAreas.TabIndex = 9;
            // 
            // lblFinish
            // 
            this.lblFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinish.Location = new System.Drawing.Point(15, 4);
            this.lblFinish.Name = "lblFinish";
            this.lblFinish.Size = new System.Drawing.Size(178, 18);
            this.lblFinish.TabIndex = 10;
            this.lblFinish.Text = "Finish";
            this.lblFinish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Finish Total Area";
            // 
            // lblFinishTotalArea
            // 
            this.lblFinishTotalArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinishTotalArea.Location = new System.Drawing.Point(138, 203);
            this.lblFinishTotalArea.Name = "lblFinishTotalArea";
            this.lblFinishTotalArea.Size = new System.Drawing.Size(54, 18);
            this.lblFinishTotalArea.TabIndex = 12;
            this.lblFinishTotalArea.Text = "100";
            this.lblFinishTotalArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPctAreaSelected
            // 
            this.lblPctAreaSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPctAreaSelected.Location = new System.Drawing.Point(135, 225);
            this.lblPctAreaSelected.Name = "lblPctAreaSelected";
            this.lblPctAreaSelected.Size = new System.Drawing.Size(58, 18);
            this.lblPctAreaSelected.TabIndex = 14;
            this.lblPctAreaSelected.Text = "100";
            this.lblPctAreaSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Pct Area Listed";
            // 
            // UCAreasView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblPctAreaSelected);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFinishTotalArea);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFinish);
            this.Controls.Add(this.dgvAreas);
            this.Name = "UCAreasView";
            this.Size = new System.Drawing.Size(212, 250);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvAreas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblFinish;
        public System.Windows.Forms.Label lblFinishTotalArea;
        public System.Windows.Forms.Label lblPctAreaSelected;
    }
}
