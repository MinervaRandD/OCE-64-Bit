namespace TestDriverSnapToGrid
{
    partial class SnapToGridTestForm
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
            this.tslScroll = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.pnlInfoPanel = new System.Windows.Forms.Panel();
            this.lblViewLowr = new System.Windows.Forms.Label();
            this.lblViewUppr = new System.Windows.Forms.Label();
            this.lblViewRght = new System.Windows.Forms.Label();
            this.lblViewLeft = new System.Windows.Forms.Label();
            this.pnlView = new System.Windows.Forms.Panel();
            this.lblHeight = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblZoom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tkbHScroll = new System.Windows.Forms.TrackBar();
            this.process1 = new System.Diagnostics.Process();
            this.tkbVScroll = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).BeginInit();
            this.sssScrollTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.pnlInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbHScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVScroll)).BeginInit();
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
            this.tslScroll});
            this.sssScrollTest.Location = new System.Drawing.Point(0, 1029);
            this.sssScrollTest.Name = "sssScrollTest";
            this.sssScrollTest.Size = new System.Drawing.Size(1452, 22);
            this.sssScrollTest.TabIndex = 8;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Zoom: ";
            // 
            // tslScroll
            // 
            this.tslScroll.AutoSize = false;
            this.tslScroll.Name = "tslScroll";
            this.tslScroll.Size = new System.Drawing.Size(128, 17);
            this.tslScroll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(11, 11);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(0);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(962, 582);
            this.axDrawingControl.TabIndex = 6;
            this.axDrawingControl.UseWaitCursor = true;
            // 
            // pnlInfoPanel
            // 
            this.pnlInfoPanel.Controls.Add(this.lblViewLowr);
            this.pnlInfoPanel.Controls.Add(this.lblViewUppr);
            this.pnlInfoPanel.Controls.Add(this.lblViewRght);
            this.pnlInfoPanel.Controls.Add(this.lblViewLeft);
            this.pnlInfoPanel.Controls.Add(this.pnlView);
            this.pnlInfoPanel.Controls.Add(this.lblHeight);
            this.pnlInfoPanel.Controls.Add(this.label7);
            this.pnlInfoPanel.Controls.Add(this.lblWidth);
            this.pnlInfoPanel.Controls.Add(this.label6);
            this.pnlInfoPanel.Controls.Add(this.lblZoom);
            this.pnlInfoPanel.Controls.Add(this.label2);
            this.pnlInfoPanel.Location = new System.Drawing.Point(1075, 29);
            this.pnlInfoPanel.Name = "pnlInfoPanel";
            this.pnlInfoPanel.Size = new System.Drawing.Size(309, 383);
            this.pnlInfoPanel.TabIndex = 27;
            // 
            // lblViewLowr
            // 
            this.lblViewLowr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewLowr.Location = new System.Drawing.Point(123, 153);
            this.lblViewLowr.Name = "lblViewLowr";
            this.lblViewLowr.Size = new System.Drawing.Size(61, 23);
            this.lblViewLowr.TabIndex = 37;
            this.lblViewLowr.Text = "Lowr";
            this.lblViewLowr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblViewUppr
            // 
            this.lblViewUppr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewUppr.Location = new System.Drawing.Point(114, 15);
            this.lblViewUppr.Name = "lblViewUppr";
            this.lblViewUppr.Size = new System.Drawing.Size(61, 23);
            this.lblViewUppr.TabIndex = 36;
            this.lblViewUppr.Text = "Uppr";
            this.lblViewUppr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblViewRght
            // 
            this.lblViewRght.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewRght.Location = new System.Drawing.Point(229, 81);
            this.lblViewRght.Name = "lblViewRght";
            this.lblViewRght.Size = new System.Drawing.Size(61, 23);
            this.lblViewRght.TabIndex = 35;
            this.lblViewRght.Text = "Rght";
            this.lblViewRght.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblViewLeft
            // 
            this.lblViewLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViewLeft.Location = new System.Drawing.Point(19, 81);
            this.lblViewLeft.Name = "lblViewLeft";
            this.lblViewLeft.Size = new System.Drawing.Size(61, 23);
            this.lblViewLeft.TabIndex = 34;
            this.lblViewLeft.Text = "Left";
            this.lblViewLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlView
            // 
            this.pnlView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlView.Location = new System.Drawing.Point(86, 41);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(137, 100);
            this.pnlView.TabIndex = 33;
            // 
            // lblHeight
            // 
            this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeight.Location = new System.Drawing.Point(147, 301);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(79, 18);
            this.lblHeight.TabIndex = 32;
            this.lblHeight.Text = "Width:";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(85, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 18);
            this.label7.TabIndex = 31;
            this.label7.Text = "Height: ";
            // 
            // lblWidth
            // 
            this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidth.Location = new System.Drawing.Point(147, 258);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(79, 18);
            this.lblWidth.TabIndex = 30;
            this.lblWidth.Text = "Width:";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(85, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Width:";
            // 
            // lblZoom
            // 
            this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoom.Location = new System.Drawing.Point(147, 213);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(79, 18);
            this.lblZoom.TabIndex = 28;
            this.lblZoom.Text = "Zoom: ";
            this.lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(85, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 27;
            this.label2.Text = "Zoom: ";
            // 
            // tkbHScroll
            // 
            this.tkbHScroll.AutoSize = false;
            this.tkbHScroll.BackColor = System.Drawing.Color.Silver;
            this.tkbHScroll.Location = new System.Drawing.Point(25, 612);
            this.tkbHScroll.Maximum = 100;
            this.tkbHScroll.Name = "tkbHScroll";
            this.tkbHScroll.Size = new System.Drawing.Size(930, 40);
            this.tkbHScroll.TabIndex = 28;
            this.tkbHScroll.TabStop = false;
            this.tkbHScroll.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // tkbVScroll
            // 
            this.tkbVScroll.BackColor = System.Drawing.Color.Silver;
            this.tkbVScroll.Location = new System.Drawing.Point(976, 29);
            this.tkbVScroll.Maximum = 100;
            this.tkbVScroll.Name = "tkbVScroll";
            this.tkbVScroll.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tkbVScroll.Size = new System.Drawing.Size(45, 510);
            this.tkbVScroll.TabIndex = 0;
            this.tkbVScroll.TabStop = false;
            this.tkbVScroll.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // ScrollTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 1051);
            this.Controls.Add(this.tkbVScroll);
            this.Controls.Add(this.tkbHScroll);
            this.Controls.Add(this.pnlInfoPanel);
            this.Controls.Add(this.sssScrollTest);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "ScrollTestForm";
            this.Text = "Scroll Test";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).EndInit();
            this.sssScrollTest.ResumeLayout(false);
            this.sssScrollTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.pnlInfoPanel.ResumeLayout(false);
            this.pnlInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbHScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVScroll)).EndInit();
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
        public System.Windows.Forms.ToolStripStatusLabel tslScroll;
        private System.Windows.Forms.Panel pnlInfoPanel;
        private System.Windows.Forms.Label lblViewLowr;
        private System.Windows.Forms.Label lblViewUppr;
        private System.Windows.Forms.Label lblViewRght;
        private System.Windows.Forms.Label lblViewLeft;
        private System.Windows.Forms.Panel pnlView;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tkbHScroll;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.TrackBar tkbVScroll;
    }
}

