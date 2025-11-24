namespace TestDriverZoomControl
{
    partial class ZoomTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoomTestForm));
            this.tslMainToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tlbZoomPct = new System.Windows.Forms.ToolStripDropDownButton();
            this.tlsZoomToFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsZoomForm = new System.Windows.Forms.StatusStrip();
            this.lblCenter = new System.Windows.Forms.ToolStripStatusLabel();
            this.trbHorizontal = new System.Windows.Forms.TrackBar();
            this.trbVertical = new System.Windows.Forms.TrackBar();
            this.lblIgnoreScrollEvent = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.lblMouseScroll = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslMainToolStrip.SuspendLayout();
            this.stsZoomForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // tslMainToolStrip
            // 
            this.tslMainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoomIn,
            this.btnZoomOut,
            this.tlbZoomPct,
            this.tlsZoomToFit});
            this.tslMainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.tslMainToolStrip.Name = "tslMainToolStrip";
            this.tslMainToolStrip.Size = new System.Drawing.Size(1094, 35);
            this.tslMainToolStrip.TabIndex = 8;
            this.tslMainToolStrip.Text = "toolStrip1";
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.AutoSize = false;
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(32, 32);
            this.btnZoomIn.Text = "toolStripButton1";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.AutoSize = false;
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(32, 32);
            this.btnZoomOut.Text = "toolStripButton1";
            // 
            // tlbZoomPct
            // 
            this.tlbZoomPct.AutoSize = false;
            this.tlbZoomPct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlbZoomPct.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.customToolStripMenuItem});
            this.tlbZoomPct.Image = ((System.Drawing.Image)(resources.GetObject("tlbZoomPct.Image")));
            this.tlbZoomPct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbZoomPct.Name = "tlbZoomPct";
            this.tlbZoomPct.Size = new System.Drawing.Size(128, 32);
            // 
            // tlsZoomToFit
            // 
            this.tlsZoomToFit.AutoSize = false;
            this.tlsZoomToFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsZoomToFit.Image = ((System.Drawing.Image)(resources.GetObject("tlsZoomToFit.Image")));
            this.tlsZoomToFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsZoomToFit.Name = "tlsZoomToFit";
            this.tlsZoomToFit.Size = new System.Drawing.Size(32, 32);
            this.tlsZoomToFit.Text = "toolStripButton1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem2.Text = "25%";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem3.Text = "50%";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem4.Text = "75%";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem5.Text = "100%";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem6.Text = "115%";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem7.Text = "125%";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem8.Text = "150%";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem9.Text = "200%";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem10.Text = "400%";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem11.Text = "600%";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem12.Text = "800%";
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.customToolStripMenuItem.Text = "Custom";
            // 
            // stsZoomForm
            // 
            this.stsZoomForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCenter,
            this.lblIgnoreScrollEvent,
            this.lblMouseScroll});
            this.stsZoomForm.Location = new System.Drawing.Point(0, 699);
            this.stsZoomForm.Name = "stsZoomForm";
            this.stsZoomForm.Size = new System.Drawing.Size(1094, 22);
            this.stsZoomForm.TabIndex = 9;
            this.stsZoomForm.Text = "statusStrip1";
            // 
            // lblCenter
            // 
            this.lblCenter.AutoSize = false;
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(256, 17);
            // 
            // trbHorizontal
            // 
            this.trbHorizontal.Location = new System.Drawing.Point(121, 640);
            this.trbHorizontal.Name = "trbHorizontal";
            this.trbHorizontal.Size = new System.Drawing.Size(572, 45);
            this.trbHorizontal.TabIndex = 10;
            // 
            // trbVertical
            // 
            this.trbVertical.Location = new System.Drawing.Point(700, 49);
            this.trbVertical.Name = "trbVertical";
            this.trbVertical.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbVertical.Size = new System.Drawing.Size(45, 601);
            this.trbVertical.TabIndex = 11;
            // 
            // lblIgnoreScrollEvent
            // 
            this.lblIgnoreScrollEvent.AutoSize = false;
            this.lblIgnoreScrollEvent.Name = "lblIgnoreScrollEvent";
            this.lblIgnoreScrollEvent.Size = new System.Drawing.Size(118, 17);
            this.lblIgnoreScrollEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(121, 49);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(572, 613);
            this.axDrawingControl.TabIndex = 7;
            // 
            // lblMouseScroll
            // 
            this.lblMouseScroll.AutoSize = false;
            this.lblMouseScroll.Name = "lblMouseScroll";
            this.lblMouseScroll.Size = new System.Drawing.Size(128, 17);
            // 
            // ZoomTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 721);
            this.Controls.Add(this.trbVertical);
            this.Controls.Add(this.trbHorizontal);
            this.Controls.Add(this.stsZoomForm);
            this.Controls.Add(this.tslMainToolStrip);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "ZoomTestForm";
            this.Text = "Scroll Test";
            this.tslMainToolStrip.ResumeLayout(false);
            this.tslMainToolStrip.PerformLayout();
            this.stsZoomForm.ResumeLayout(false);
            this.stsZoomForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.ToolStrip tslMainToolStrip;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripDropDownButton tlbZoomPct;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tlsZoomToFit;
        private System.Windows.Forms.StatusStrip stsZoomForm;
        private System.Windows.Forms.ToolStripStatusLabel lblCenter;
        private System.Windows.Forms.TrackBar trbHorizontal;
        private System.Windows.Forms.TrackBar trbVertical;
        private System.Windows.Forms.ToolStripStatusLabel lblIgnoreScrollEvent;
        private System.Windows.Forms.ToolStripStatusLabel lblMouseScroll;
    }
}

