namespace TestDriverSeamMarker
{
    partial class TestSeamMarkerForm
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
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.pnlTestSetup = new System.Windows.Forms.Panel();
            this.btnDrawMarker = new System.Windows.Forms.Button();
            this.btnDrawSeams = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.pnlTestSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(237, 40);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(962, 613);
            this.axDrawingControl.TabIndex = 6;
            // 
            // pnlTestSetup
            // 
            this.pnlTestSetup.Controls.Add(this.btnDrawMarker);
            this.pnlTestSetup.Controls.Add(this.btnDrawSeams);
            this.pnlTestSetup.Location = new System.Drawing.Point(29, 40);
            this.pnlTestSetup.Name = "pnlTestSetup";
            this.pnlTestSetup.Size = new System.Drawing.Size(164, 613);
            this.pnlTestSetup.TabIndex = 7;
            // 
            // btnDrawMarker
            // 
            this.btnDrawMarker.Location = new System.Drawing.Point(38, 24);
            this.btnDrawMarker.Name = "btnDrawMarker";
            this.btnDrawMarker.Size = new System.Drawing.Size(91, 23);
            this.btnDrawMarker.TabIndex = 3;
            this.btnDrawMarker.Text = "Draw Marker";
            this.btnDrawMarker.UseVisualStyleBackColor = true;
            // 
            // btnDrawSeams
            // 
            this.btnDrawSeams.Location = new System.Drawing.Point(38, 91);
            this.btnDrawSeams.Name = "btnDrawSeams";
            this.btnDrawSeams.Size = new System.Drawing.Size(91, 23);
            this.btnDrawSeams.TabIndex = 0;
            this.btnDrawSeams.Text = "Draw Seams";
            this.btnDrawSeams.UseVisualStyleBackColor = true;
            this.btnDrawSeams.Click += new System.EventHandler(this.btnDoCuts_Click);
            // 
            // TestSeamMarkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 749);
            this.Controls.Add(this.pnlTestSetup);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "TestSeamMarkerForm";
            this.Text = "Seams And Cuts";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.pnlTestSetup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Panel pnlTestSetup;
        private System.Windows.Forms.Button btnDrawSeams;
        private System.Windows.Forms.Button btnDrawMarker;
    }
}

