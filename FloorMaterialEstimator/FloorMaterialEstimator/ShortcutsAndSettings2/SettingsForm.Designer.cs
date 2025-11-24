namespace FloorMaterialEstimator.ShortcutsAndSettings
{
    partial class SettingsForm
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
            this.grbShowLineDrawOut = new System.Windows.Forms.GroupBox();
            this.rbtHideLineDrawout = new System.Windows.Forms.RadioButton();
            this.rbtShowLineDrawout = new System.Windows.Forms.RadioButton();
            this.grbOperatingMode = new System.Windows.Forms.GroupBox();
            this.rbnHideLineDrawout = new System.Windows.Forms.RadioButton();
            this.rbnNormalOperatingMode = new System.Windows.Forms.RadioButton();
            this.grbShowLineDrawOut.SuspendLayout();
            this.grbOperatingMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbShowLineDrawOut
            // 
            this.grbShowLineDrawOut.Controls.Add(this.rbtHideLineDrawout);
            this.grbShowLineDrawOut.Controls.Add(this.rbtShowLineDrawout);
            this.grbShowLineDrawOut.Location = new System.Drawing.Point(42, 145);
            this.grbShowLineDrawOut.Name = "grbShowLineDrawOut";
            this.grbShowLineDrawOut.Size = new System.Drawing.Size(144, 99);
            this.grbShowLineDrawOut.TabIndex = 0;
            this.grbShowLineDrawOut.TabStop = false;
            this.grbShowLineDrawOut.Text = "Line Drawout Mode";
            // 
            // rbtHideLineDrawout
            // 
            this.rbtHideLineDrawout.AutoSize = true;
            this.rbtHideLineDrawout.Location = new System.Drawing.Point(15, 67);
            this.rbtHideLineDrawout.Name = "rbtHideLineDrawout";
            this.rbtHideLineDrawout.Size = new System.Drawing.Size(113, 17);
            this.rbtHideLineDrawout.TabIndex = 1;
            this.rbtHideLineDrawout.Text = "Hide Line Drawout";
            this.rbtHideLineDrawout.UseVisualStyleBackColor = true;
            this.rbtHideLineDrawout.CheckedChanged += new System.EventHandler(this.rbnHideLineDrawout_CheckedChanged);
            // 
            // rbtShowLineDrawout
            // 
            this.rbtShowLineDrawout.AutoSize = true;
            this.rbtShowLineDrawout.Checked = true;
            this.rbtShowLineDrawout.Location = new System.Drawing.Point(15, 27);
            this.rbtShowLineDrawout.Name = "rbtShowLineDrawout";
            this.rbtShowLineDrawout.Size = new System.Drawing.Size(118, 17);
            this.rbtShowLineDrawout.TabIndex = 0;
            this.rbtShowLineDrawout.TabStop = true;
            this.rbtShowLineDrawout.Text = "Show Line Drawout";
            this.rbtShowLineDrawout.UseVisualStyleBackColor = true;
            this.rbtShowLineDrawout.CheckedChanged += new System.EventHandler(this.rbnShowLineDrawout_CheckedChanged);
            // 
            // grbOperatingMode
            // 
            this.grbOperatingMode.Controls.Add(this.rbnHideLineDrawout);
            this.grbOperatingMode.Controls.Add(this.rbnNormalOperatingMode);
            this.grbOperatingMode.Location = new System.Drawing.Point(37, 20);
            this.grbOperatingMode.Name = "grbOperatingMode";
            this.grbOperatingMode.Size = new System.Drawing.Size(149, 106);
            this.grbOperatingMode.TabIndex = 1;
            this.grbOperatingMode.TabStop = false;
            this.grbOperatingMode.Text = "Operating Mode";
            // 
            // rbnShowLineDrawout
            // 
            this.rbnHideLineDrawout.AutoSize = true;
            this.rbnHideLineDrawout.Location = new System.Drawing.Point(15, 63);
            this.rbnHideLineDrawout.Name = "rbnShowLineDrawout";
            this.rbnHideLineDrawout.Size = new System.Drawing.Size(88, 17);
            this.rbnHideLineDrawout.TabIndex = 1;
            this.rbnHideLineDrawout.Text = "Development";
            this.rbnHideLineDrawout.UseVisualStyleBackColor = true;
            this.rbnHideLineDrawout.CheckedChanged += new System.EventHandler(this.rbnDevelopmentOperatingMode_CheckedChanged);
            // 
            // rbnNormalOperatingMode
            // 
            this.rbnNormalOperatingMode.AutoSize = true;
            this.rbnNormalOperatingMode.Checked = true;
            this.rbnNormalOperatingMode.Location = new System.Drawing.Point(15, 29);
            this.rbnNormalOperatingMode.Name = "rbnNormalOperatingMode";
            this.rbnNormalOperatingMode.Size = new System.Drawing.Size(58, 17);
            this.rbnNormalOperatingMode.TabIndex = 0;
            this.rbnNormalOperatingMode.TabStop = true;
            this.rbnNormalOperatingMode.Text = "Normal";
            this.rbnNormalOperatingMode.UseVisualStyleBackColor = true;
            this.rbnNormalOperatingMode.CheckedChanged += new System.EventHandler(this.rbnNormalOperatingMode_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 421);
            this.Controls.Add(this.grbOperatingMode);
            this.Controls.Add(this.grbShowLineDrawOut);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.grbShowLineDrawOut.ResumeLayout(false);
            this.grbShowLineDrawOut.PerformLayout();
            this.grbOperatingMode.ResumeLayout(false);
            this.grbOperatingMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbShowLineDrawOut;
        private System.Windows.Forms.RadioButton rbtHideLineDrawout;
        private System.Windows.Forms.RadioButton rbtShowLineDrawout;
        private System.Windows.Forms.GroupBox grbOperatingMode;
        private System.Windows.Forms.RadioButton rbnHideLineDrawout;
        private System.Windows.Forms.RadioButton rbnNormalOperatingMode;
    }
}