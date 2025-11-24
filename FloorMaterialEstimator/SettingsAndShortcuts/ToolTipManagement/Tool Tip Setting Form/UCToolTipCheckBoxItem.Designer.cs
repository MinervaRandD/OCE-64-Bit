
namespace SettingsLib
{
    partial class UCToolTipCheckBoxItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCToolTipCheckBoxItem));
            this.txbToolTip = new System.Windows.Forms.TextBox();
            this.pbxCheckBox = new System.Windows.Forms.PictureBox();
            this.lblCheckBoxText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCheckBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txbToolTip
            // 
            this.txbToolTip.Location = new System.Drawing.Point(134, 13);
            this.txbToolTip.Name = "txbToolTip";
            this.txbToolTip.Size = new System.Drawing.Size(213, 20);
            this.txbToolTip.TabIndex = 1;
            // 
            // pbxCheckBox
            // 
            this.pbxCheckBox.Image = ((System.Drawing.Image)(resources.GetObject("pbxCheckBox.Image")));
            this.pbxCheckBox.Location = new System.Drawing.Point(4, 11);
            this.pbxCheckBox.Name = "pbxCheckBox";
            this.pbxCheckBox.Size = new System.Drawing.Size(26, 23);
            this.pbxCheckBox.TabIndex = 2;
            this.pbxCheckBox.TabStop = false;
            // 
            // lblCheckBoxText
            // 
            this.lblCheckBoxText.Location = new System.Drawing.Point(28, 12);
            this.lblCheckBoxText.Name = "lblCheckBoxText";
            this.lblCheckBoxText.Size = new System.Drawing.Size(100, 23);
            this.lblCheckBoxText.TabIndex = 3;
            this.lblCheckBoxText.Text = "label1";
            this.lblCheckBoxText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCToolTipCheckBoxItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCheckBoxText);
            this.Controls.Add(this.pbxCheckBox);
            this.Controls.Add(this.txbToolTip);
            this.Name = "UCToolTipCheckBoxItem";
            this.Size = new System.Drawing.Size(364, 40);
            ((System.ComponentModel.ISupportInitialize)(this.pbxCheckBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbToolTip;
        private System.Windows.Forms.PictureBox pbxCheckBox;
        private System.Windows.Forms.Label lblCheckBoxText;
    }
}
