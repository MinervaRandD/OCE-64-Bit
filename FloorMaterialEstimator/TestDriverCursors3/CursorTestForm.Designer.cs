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
            this.btnLaunchTestForm = new System.Windows.Forms.Button();
            this.lblCursorPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWithinBounds = new System.Windows.Forms.ToolStripStatusLabel();
            this.sssCursorTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sssCursorTest
            // 
            this.sssCursorTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCursorType,
            this.lblIInputElement,
            this.lblCursorPosition,
            this.lblWithinBounds});
            this.sssCursorTest.Location = new System.Drawing.Point(0, 739);
            this.sssCursorTest.Name = "sssCursorTest";
            this.sssCursorTest.Size = new System.Drawing.Size(984, 22);
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
            this.axDrawingControl.Location = new System.Drawing.Point(13, 46);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(757, 590);
            this.axDrawingControl.TabIndex = 12;
            // 
            // btnLaunchTestForm
            // 
            this.btnLaunchTestForm.Location = new System.Drawing.Point(13, 13);
            this.btnLaunchTestForm.Name = "btnLaunchTestForm";
            this.btnLaunchTestForm.Size = new System.Drawing.Size(108, 23);
            this.btnLaunchTestForm.TabIndex = 14;
            this.btnLaunchTestForm.Text = "Launch Test Form";
            this.btnLaunchTestForm.UseVisualStyleBackColor = true;
            this.btnLaunchTestForm.Click += new System.EventHandler(this.btnLaunchTestForm_Click);
            // 
            // lblCursorPosition
            // 
            this.lblCursorPosition.AutoSize = false;
            this.lblCursorPosition.Name = "lblCursorPosition";
            this.lblCursorPosition.Size = new System.Drawing.Size(128, 17);
            // 
            // lblWithinBounds
            // 
            this.lblWithinBounds.AutoSize = false;
            this.lblWithinBounds.Name = "lblWithinBounds";
            this.lblWithinBounds.Size = new System.Drawing.Size(128, 17);
            // 
            // CursorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.btnLaunchTestForm);
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
        private System.Windows.Forms.Button btnLaunchTestForm;
        private System.Windows.Forms.ToolStripStatusLabel lblCursorPosition;
        private System.Windows.Forms.ToolStripStatusLabel lblWithinBounds;
    }
}

