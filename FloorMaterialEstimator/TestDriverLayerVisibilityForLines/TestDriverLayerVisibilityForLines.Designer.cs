namespace TestDriverLayerVisibilityForLines
{
    partial class TestDriverLayerVisibilityForLinesForm
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
            this.btnMakeVisible = new System.Windows.Forms.Button();
            this.btnMakeInvisible = new System.Windows.Forms.Button();
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
            this.axDrawingControl.Size = new System.Drawing.Size(887, 613);
            this.axDrawingControl.TabIndex = 6;
            // 
            // pnlTestSetup
            // 
            this.pnlTestSetup.Controls.Add(this.btnMakeInvisible);
            this.pnlTestSetup.Controls.Add(this.btnMakeVisible);
            this.pnlTestSetup.Location = new System.Drawing.Point(29, 40);
            this.pnlTestSetup.Name = "pnlTestSetup";
            this.pnlTestSetup.Size = new System.Drawing.Size(164, 613);
            this.pnlTestSetup.TabIndex = 7;
            // 
            // btnMakeVisible
            // 
            this.btnMakeVisible.Location = new System.Drawing.Point(41, 125);
            this.btnMakeVisible.Name = "btnMakeVisible";
            this.btnMakeVisible.Size = new System.Drawing.Size(105, 23);
            this.btnMakeVisible.TabIndex = 0;
            this.btnMakeVisible.Text = "Make Visible";
            this.btnMakeVisible.UseVisualStyleBackColor = true;
            this.btnMakeVisible.Click += new System.EventHandler(this.btnMakeVisible_Click);
            // 
            // btnMakeInvisible
            // 
            this.btnMakeInvisible.Location = new System.Drawing.Point(41, 212);
            this.btnMakeInvisible.Name = "btnMakeInvisible";
            this.btnMakeInvisible.Size = new System.Drawing.Size(105, 23);
            this.btnMakeInvisible.TabIndex = 1;
            this.btnMakeInvisible.Text = "Make Invisible";
            this.btnMakeInvisible.UseVisualStyleBackColor = true;
            this.btnMakeInvisible.Click += new System.EventHandler(this.btnMakeInvisible_Click);
            // 
            // TestDriverLayerVisibilityForLinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 701);
            this.Controls.Add(this.pnlTestSetup);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "TestDriverLayerVisibilityForLinesForm";
            this.Text = "Visibility For Lines";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.pnlTestSetup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Panel pnlTestSetup;
        private System.Windows.Forms.Button btnMakeInvisible;
        private System.Windows.Forms.Button btnMakeVisible;
    }
}

