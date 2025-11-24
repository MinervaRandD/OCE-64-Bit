namespace FloorMaterialEstimator.ShortcutsAndSettings
{
    partial class GlobalSettingsForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbcSettings = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.grbLineDrawoutMode = new System.Windows.Forms.GroupBox();
            this.rbnHideLineDrawout = new System.Windows.Forms.RadioButton();
            this.rbnShowLineDrawout = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbOperatingMode = new System.Windows.Forms.GroupBox();
            this.rbnDevelopmentOperatingMode = new System.Windows.Forms.RadioButton();
            this.rbnNormalOperatingMode = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbpDevelopment = new System.Windows.Forms.TabPage();
            this.txbGridLineOffset = new System.Windows.Forms.TextBox();
            this.lblGridLineOffset = new System.Windows.Forms.Label();
            this.lblYGridLineCount = new System.Windows.Forms.Label();
            this.txbYGridLineCount = new System.Windows.Forms.TextBox();
            this.cbxShowGridCoordinates = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbSnapToAxis = new System.Windows.Forms.CheckBox();
            this.txbSnapToAxisResolutionInDegrees = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbcSettings.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.grbLineDrawoutMode.SuspendLayout();
            this.grbOperatingMode.SuspendLayout();
            this.tbpDevelopment.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(37, 487);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(168, 487);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbcSettings
            // 
            this.tbcSettings.Controls.Add(this.tbpGeneral);
            this.tbcSettings.Controls.Add(this.tbpDevelopment);
            this.tbcSettings.Location = new System.Drawing.Point(17, 12);
            this.tbcSettings.Name = "tbcSettings";
            this.tbcSettings.SelectedIndex = 0;
            this.tbcSettings.Size = new System.Drawing.Size(277, 434);
            this.tbcSettings.TabIndex = 4;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.groupBox1);
            this.tbpGeneral.Controls.Add(this.grbLineDrawoutMode);
            this.tbpGeneral.Controls.Add(this.grbOperatingMode);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(269, 408);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // grbLineDrawoutMode
            // 
            this.grbLineDrawoutMode.Controls.Add(this.rbnHideLineDrawout);
            this.grbLineDrawoutMode.Controls.Add(this.rbnShowLineDrawout);
            this.grbLineDrawoutMode.Controls.Add(this.groupBox3);
            this.grbLineDrawoutMode.Location = new System.Drawing.Point(19, 145);
            this.grbLineDrawoutMode.Name = "grbLineDrawoutMode";
            this.grbLineDrawoutMode.Size = new System.Drawing.Size(200, 100);
            this.grbLineDrawoutMode.TabIndex = 4;
            this.grbLineDrawoutMode.TabStop = false;
            this.grbLineDrawoutMode.Text = "Line Drawout Mode";
            // 
            // rbnHideLineDrawout
            // 
            this.rbnHideLineDrawout.AutoSize = true;
            this.rbnHideLineDrawout.Location = new System.Drawing.Point(19, 66);
            this.rbnHideLineDrawout.Name = "rbnHideLineDrawout";
            this.rbnHideLineDrawout.Size = new System.Drawing.Size(107, 17);
            this.rbnHideLineDrawout.TabIndex = 7;
            this.rbnHideLineDrawout.Text = "Hide line drawout";
            this.rbnHideLineDrawout.UseVisualStyleBackColor = true;
            // 
            // rbnShowLineDrawout
            // 
            this.rbnShowLineDrawout.AutoSize = true;
            this.rbnShowLineDrawout.Checked = true;
            this.rbnShowLineDrawout.Location = new System.Drawing.Point(19, 29);
            this.rbnShowLineDrawout.Name = "rbnShowLineDrawout";
            this.rbnShowLineDrawout.Size = new System.Drawing.Size(112, 17);
            this.rbnShowLineDrawout.TabIndex = 6;
            this.rbnShowLineDrawout.TabStop = true;
            this.rbnShowLineDrawout.Text = "Show line drawout";
            this.rbnShowLineDrawout.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // grbOperatingMode
            // 
            this.grbOperatingMode.Controls.Add(this.rbnDevelopmentOperatingMode);
            this.grbOperatingMode.Controls.Add(this.rbnNormalOperatingMode);
            this.grbOperatingMode.Controls.Add(this.groupBox2);
            this.grbOperatingMode.Location = new System.Drawing.Point(16, 20);
            this.grbOperatingMode.Name = "grbOperatingMode";
            this.grbOperatingMode.Size = new System.Drawing.Size(203, 100);
            this.grbOperatingMode.TabIndex = 3;
            this.grbOperatingMode.TabStop = false;
            this.grbOperatingMode.Text = "Operating Mode";
            // 
            // rbnDevelopmentOperatingMode
            // 
            this.rbnDevelopmentOperatingMode.AutoSize = true;
            this.rbnDevelopmentOperatingMode.Location = new System.Drawing.Point(22, 64);
            this.rbnDevelopmentOperatingMode.Name = "rbnDevelopmentOperatingMode";
            this.rbnDevelopmentOperatingMode.Size = new System.Drawing.Size(88, 17);
            this.rbnDevelopmentOperatingMode.TabIndex = 5;
            this.rbnDevelopmentOperatingMode.Text = "Development";
            this.rbnDevelopmentOperatingMode.UseVisualStyleBackColor = true;
            // 
            // rbnNormalOperatingMode
            // 
            this.rbnNormalOperatingMode.AutoSize = true;
            this.rbnNormalOperatingMode.Checked = true;
            this.rbnNormalOperatingMode.Location = new System.Drawing.Point(22, 28);
            this.rbnNormalOperatingMode.Name = "rbnNormalOperatingMode";
            this.rbnNormalOperatingMode.Size = new System.Drawing.Size(58, 17);
            this.rbnNormalOperatingMode.TabIndex = 4;
            this.rbnNormalOperatingMode.TabStop = true;
            this.rbnNormalOperatingMode.Text = "Normal";
            this.rbnNormalOperatingMode.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(3, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // tbpDevelopment
            // 
            this.tbpDevelopment.Controls.Add(this.txbGridLineOffset);
            this.tbpDevelopment.Controls.Add(this.lblGridLineOffset);
            this.tbpDevelopment.Controls.Add(this.lblYGridLineCount);
            this.tbpDevelopment.Controls.Add(this.txbYGridLineCount);
            this.tbpDevelopment.Controls.Add(this.cbxShowGridCoordinates);
            this.tbpDevelopment.Location = new System.Drawing.Point(4, 22);
            this.tbpDevelopment.Name = "tbpDevelopment";
            this.tbpDevelopment.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDevelopment.Size = new System.Drawing.Size(269, 408);
            this.tbpDevelopment.TabIndex = 1;
            this.tbpDevelopment.Text = "Development";
            this.tbpDevelopment.UseVisualStyleBackColor = true;
            // 
            // txbGridLineOffset
            // 
            this.txbGridLineOffset.Location = new System.Drawing.Point(130, 93);
            this.txbGridLineOffset.Name = "txbGridLineOffset";
            this.txbGridLineOffset.Size = new System.Drawing.Size(46, 20);
            this.txbGridLineOffset.TabIndex = 4;
            this.txbGridLineOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGridLineOffset
            // 
            this.lblGridLineOffset.AutoSize = true;
            this.lblGridLineOffset.Location = new System.Drawing.Point(21, 93);
            this.lblGridLineOffset.Name = "lblGridLineOffset";
            this.lblGridLineOffset.Size = new System.Drawing.Size(74, 13);
            this.lblGridLineOffset.TabIndex = 3;
            this.lblGridLineOffset.Text = "Grid line offset";
            // 
            // lblYGridLineCount
            // 
            this.lblYGridLineCount.AutoSize = true;
            this.lblYGridLineCount.Location = new System.Drawing.Point(21, 43);
            this.lblYGridLineCount.Name = "lblYGridLineCount";
            this.lblYGridLineCount.Size = new System.Drawing.Size(85, 13);
            this.lblYGridLineCount.TabIndex = 2;
            this.lblYGridLineCount.Text = "Y Grid line count";
            // 
            // txbYGridLineCount
            // 
            this.txbYGridLineCount.Location = new System.Drawing.Point(130, 40);
            this.txbYGridLineCount.Name = "txbYGridLineCount";
            this.txbYGridLineCount.Size = new System.Drawing.Size(46, 20);
            this.txbYGridLineCount.TabIndex = 1;
            this.txbYGridLineCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbxShowGridCoordinates
            // 
            this.cbxShowGridCoordinates.AutoSize = true;
            this.cbxShowGridCoordinates.Location = new System.Drawing.Point(24, 175);
            this.cbxShowGridCoordinates.Name = "cbxShowGridCoordinates";
            this.cbxShowGridCoordinates.Size = new System.Drawing.Size(134, 17);
            this.cbxShowGridCoordinates.TabIndex = 0;
            this.cbxShowGridCoordinates.Text = "Show Grid Coordinates";
            this.cbxShowGridCoordinates.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbSnapToAxisResolutionInDegrees);
            this.groupBox1.Controls.Add(this.ckbSnapToAxis);
            this.groupBox1.Location = new System.Drawing.Point(19, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 128);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Snap to Axis";
            // 
            // ckbSnapToAxis
            // 
            this.ckbSnapToAxis.AutoSize = true;
            this.ckbSnapToAxis.Location = new System.Drawing.Point(19, 31);
            this.ckbSnapToAxis.Name = "ckbSnapToAxis";
            this.ckbSnapToAxis.Size = new System.Drawing.Size(84, 17);
            this.ckbSnapToAxis.TabIndex = 0;
            this.ckbSnapToAxis.Text = "Snap to axis";
            this.ckbSnapToAxis.UseVisualStyleBackColor = true;
            // 
            // txbSnapToAxisResolutionInDegrees
            // 
            this.txbSnapToAxisResolutionInDegrees.Location = new System.Drawing.Point(73, 98);
            this.txbSnapToAxisResolutionInDegrees.Name = "txbSnapToAxisResolutionInDegrees";
            this.txbSnapToAxisResolutionInDegrees.Size = new System.Drawing.Size(53, 20);
            this.txbSnapToAxisResolutionInDegrees.TabIndex = 1;
            this.txbSnapToAxisResolutionInDegrees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Snap to axis resolution in degrees";
            // 
            // GlobalSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 585);
            this.ControlBox = false;
            this.Controls.Add(this.tbcSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "GlobalSettingsForm";
            this.Text = "Settings";
            this.tbcSettings.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.grbLineDrawoutMode.ResumeLayout(false);
            this.grbLineDrawoutMode.PerformLayout();
            this.grbOperatingMode.ResumeLayout(false);
            this.grbOperatingMode.PerformLayout();
            this.tbpDevelopment.ResumeLayout(false);
            this.tbpDevelopment.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tbcSettings;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.GroupBox grbLineDrawoutMode;
        public System.Windows.Forms.RadioButton rbnHideLineDrawout;
        public System.Windows.Forms.RadioButton rbnShowLineDrawout;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grbOperatingMode;
        public System.Windows.Forms.RadioButton rbnDevelopmentOperatingMode;
        public System.Windows.Forms.RadioButton rbnNormalOperatingMode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tbpDevelopment;
        public System.Windows.Forms.TextBox txbGridLineOffset;
        private System.Windows.Forms.Label lblGridLineOffset;
        private System.Windows.Forms.Label lblYGridLineCount;
        public System.Windows.Forms.TextBox txbYGridLineCount;
        public System.Windows.Forms.CheckBox cbxShowGridCoordinates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox ckbSnapToAxis;
        public System.Windows.Forms.TextBox txbSnapToAxisResolutionInDegrees;
    }
}