namespace TestDriverBorderManager
{
    partial class TestDriverBorderManagerForm
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
            this.sssBaseStatusStrip = new System.Windows.Forms.StatusStrip();
            this.lblDrawingShape = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.btnDeleteLast = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.sssBaseStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sssBaseStatusStrip
            // 
            this.sssBaseStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDrawingShape});
            this.sssBaseStatusStrip.Location = new System.Drawing.Point(0, 759);
            this.sssBaseStatusStrip.Name = "sssBaseStatusStrip";
            this.sssBaseStatusStrip.Size = new System.Drawing.Size(1094, 22);
            this.sssBaseStatusStrip.TabIndex = 8;
            this.sssBaseStatusStrip.Text = "statusStrip1";
            // 
            // lblDrawingShape
            // 
            this.lblDrawingShape.AutoSize = false;
            this.lblDrawingShape.Name = "lblDrawingShape";
            this.lblDrawingShape.Size = new System.Drawing.Size(118, 17);
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(25, 13);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(1041, 649);
            this.axDrawingControl.TabIndex = 7;
            // 
            // btnDeleteLast
            // 
            this.btnDeleteLast.Location = new System.Drawing.Point(25, 709);
            this.btnDeleteLast.Name = "btnDeleteLast";
            this.btnDeleteLast.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteLast.TabIndex = 9;
            this.btnDeleteLast.Text = "Delete Last";
            this.btnDeleteLast.UseVisualStyleBackColor = true;
            this.btnDeleteLast.Click += new System.EventHandler(this.btnDeleteLast_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(130, 709);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAll.TabIndex = 10;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // TestDriverBorderManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 781);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDeleteLast);
            this.Controls.Add(this.sssBaseStatusStrip);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "TestDriverBorderManagerForm";
            this.Text = "Scroll Test";
            this.sssBaseStatusStrip.ResumeLayout(false);
            this.sssBaseStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.StatusStrip sssBaseStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblDrawingShape;
        private System.Windows.Forms.Button btnDeleteLast;
        private System.Windows.Forms.Button btnDeleteAll;
    }
}

