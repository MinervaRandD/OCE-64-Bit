namespace CanvasLib.Area_and_Seam_Views
{
    partial class UCRemnantViewElement
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
            this.dgvRemnant = new System.Windows.Forms.DataGridView();
            this.lblWasteFactorTitle = new System.Windows.Forms.Label();
            this.lblWasteFactor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRemnant)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRemnant
            // 
            this.dgvRemnant.AllowUserToAddRows = false;
            this.dgvRemnant.AllowUserToDeleteRows = false;
            this.dgvRemnant.AllowUserToResizeColumns = false;
            this.dgvRemnant.AllowUserToResizeRows = false;
            this.dgvRemnant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRemnant.Location = new System.Drawing.Point(3, 4);
            this.dgvRemnant.Name = "dgvRemnant";
            this.dgvRemnant.RowHeadersVisible = false;
            this.dgvRemnant.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRemnant.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRemnant.Size = new System.Drawing.Size(187, 211);
            this.dgvRemnant.TabIndex = 0;
            // 
            // lblWasteFactorTitle
            // 
            this.lblWasteFactorTitle.AutoSize = true;
            this.lblWasteFactorTitle.Location = new System.Drawing.Point(13, 277);
            this.lblWasteFactorTitle.Name = "lblWasteFactorTitle";
            this.lblWasteFactorTitle.Size = new System.Drawing.Size(71, 13);
            this.lblWasteFactorTitle.TabIndex = 1;
            this.lblWasteFactorTitle.Text = "Waste Factor";
            // 
            // lblWasteFactor
            // 
            this.lblWasteFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWasteFactor.Location = new System.Drawing.Point(123, 272);
            this.lblWasteFactor.Name = "lblWasteFactor";
            this.lblWasteFactor.Size = new System.Drawing.Size(50, 23);
            this.lblWasteFactor.TabIndex = 2;
            this.lblWasteFactor.Text = "0.0%";
            this.lblWasteFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCRemnantViewElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblWasteFactor);
            this.Controls.Add(this.lblWasteFactorTitle);
            this.Controls.Add(this.dgvRemnant);
            this.Name = "UCRemnantViewElement";
            this.Size = new System.Drawing.Size(212, 302);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRemnant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRemnant;
        private System.Windows.Forms.Label lblWasteFactorTitle;
        private System.Windows.Forms.Label lblWasteFactor;
    }
}
