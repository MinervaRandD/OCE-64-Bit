namespace TestDriverCursors
{
    partial class CursorTestForm
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
            this.sssCursorTest = new System.Windows.Forms.StatusStrip();
            this.lblCursorType = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblIInputElement = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sssCursorTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sssCursorTest
            // 
            this.sssCursorTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCursorType,
            this.lblIInputElement});
            this.sssCursorTest.Location = new System.Drawing.Point(0, 1039);
            this.sssCursorTest.Name = "sssCursorTest";
            this.sssCursorTest.Size = new System.Drawing.Size(1827, 22);
            this.sssCursorTest.TabIndex = 13;
            // 
            // lblCursorType
            // 
            this.lblCursorType.AutoSize = false;
            this.lblCursorType.Name = "lblCursorType";
            this.lblCursorType.Size = new System.Drawing.Size(120, 17);
            // 
            // lblIInputElement
            // 
            this.lblIInputElement.AutoSize = false;
            this.lblIInputElement.Name = "lblIInputElement";
            this.lblIInputElement.Size = new System.Drawing.Size(256, 17);
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(310, 119);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(757, 590);
            this.axDrawingControl.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(59, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 597);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(320, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(746, 93);
            this.panel2.TabIndex = 15;
            // 
            // CursorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 1061);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sssCursorTest);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "CursorTestForm";
            this.Text = "Cursor Test";
            this.sssCursorTest.ResumeLayout(false);
            this.sssCursorTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.StatusStrip sssCursorTest;
        private System.Windows.Forms.ToolStripStatusLabel lblCursorType;
        private System.Windows.Forms.ToolStripStatusLabel lblIInputElement;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

