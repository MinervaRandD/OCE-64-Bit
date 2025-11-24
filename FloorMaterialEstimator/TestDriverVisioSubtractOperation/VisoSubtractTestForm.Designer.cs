namespace TestDriverVisioSubtractOperation
{
    partial class VisioSubtractTestForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.dgvUnderages = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvOverages = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.sssScrollTest = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).BeginInit();
            this.sssScrollTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // dgvUnderages
            // 
            this.dgvUnderages.Location = new System.Drawing.Point(0, 0);
            this.dgvUnderages.Name = "dgvUnderages";
            this.dgvUnderages.Size = new System.Drawing.Size(240, 150);
            this.dgvUnderages.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 0;
            // 
            // dgvOverages
            // 
            this.dgvOverages.Location = new System.Drawing.Point(0, 0);
            this.dgvOverages.Name = "dgvOverages";
            this.dgvOverages.Size = new System.Drawing.Size(240, 150);
            this.dgvOverages.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // sssScrollTest
            // 
            this.sssScrollTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tslZoom});
            this.sssScrollTest.Location = new System.Drawing.Point(0, 679);
            this.sssScrollTest.Name = "sssScrollTest";
            this.sssScrollTest.Size = new System.Drawing.Size(1061, 22);
            this.sssScrollTest.TabIndex = 8;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Zoom: ";
            // 
            // tslZoom
            // 
            this.tslZoom.AutoSize = false;
            this.tslZoom.Name = "tslZoom";
            this.tslZoom.Size = new System.Drawing.Size(64, 17);
            this.tslZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(30, 16);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(997, 659);
            this.axDrawingControl.TabIndex = 6;
            // 
            // VisioSubtractTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 701);
            this.Controls.Add(this.sssScrollTest);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "VisioSubtractTestForm";
            this.Text = "Scroll Test";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).EndInit();
            this.sssScrollTest.ResumeLayout(false);
            this.sssScrollTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvOverages;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvUnderages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.StatusStrip sssScrollTest;
        public System.Windows.Forms.ToolStripStatusLabel tslZoom;
    }
}

