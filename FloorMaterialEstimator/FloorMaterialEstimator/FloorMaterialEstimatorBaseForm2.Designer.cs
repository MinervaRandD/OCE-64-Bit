namespace FloorMaterialEstimator
{
    partial class FloorMaterialEstimatorBaseForm2
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
            this.tbcPageAreaLine = new System.Windows.Forms.TabControl();
            this.tbpPages = new System.Windows.Forms.TabPage();
            this.tbpAreas = new System.Windows.Forms.TabPage();
            this.tbpLines = new System.Windows.Forms.TabPage();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.tbcPageAreaLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcPageAreaLine
            // 
            this.tbcPageAreaLine.Controls.Add(this.tbpPages);
            this.tbcPageAreaLine.Controls.Add(this.tbpAreas);
            this.tbcPageAreaLine.Controls.Add(this.tbpLines);
            this.tbcPageAreaLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPageAreaLine.HotTrack = true;
            this.tbcPageAreaLine.Location = new System.Drawing.Point(3, 87);
            this.tbcPageAreaLine.Name = "tbcPageAreaLine";
            this.tbcPageAreaLine.SelectedIndex = 0;
            this.tbcPageAreaLine.Size = new System.Drawing.Size(229, 601);
            this.tbcPageAreaLine.TabIndex = 0;
            // 
            // tbpPages
            // 
            this.tbpPages.Location = new System.Drawing.Point(4, 25);
            this.tbpPages.Name = "tbpPages";
            this.tbpPages.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPages.Size = new System.Drawing.Size(221, 572);
            this.tbpPages.TabIndex = 0;
            this.tbpPages.Text = "Pages";
            this.tbpPages.UseVisualStyleBackColor = true;
            // 
            // tbpAreas
            // 
            this.tbpAreas.Location = new System.Drawing.Point(4, 25);
            this.tbpAreas.Name = "tbpAreas";
            this.tbpAreas.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAreas.Size = new System.Drawing.Size(221, 572);
            this.tbpAreas.TabIndex = 1;
            this.tbpAreas.Text = "Areas";
            this.tbpAreas.UseVisualStyleBackColor = true;
            // 
            // tbpLines
            // 
            this.tbpLines.Location = new System.Drawing.Point(4, 25);
            this.tbpLines.Name = "tbpLines";
            this.tbpLines.Size = new System.Drawing.Size(221, 572);
            this.tbpLines.TabIndex = 2;
            this.tbpLines.Text = "Lines";
            this.tbpLines.UseVisualStyleBackColor = true;
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(234, 112);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(852, 572);
            this.axDrawingControl.TabIndex = 1;
            // 
            // FloorMaterialEstimatorBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1623, 686);
            this.Controls.Add(this.axDrawingControl);
            this.Controls.Add(this.tbcPageAreaLine);
            this.Name = "FloorMaterialEstimatorBaseForm";
            this.Text = "Bruun Estimation Floor Material Planning Estimator";
            this.tbcPageAreaLine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcPageAreaLine;
        private System.Windows.Forms.TabPage tbpPages;
        private System.Windows.Forms.TabPage tbpAreas;
        private System.Windows.Forms.TabPage tbpLines;
        private AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
    }
}

