
namespace SettingsLib
{
    partial class UCToolTipRadioButtonItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCToolTipRadioButtonItem));
            this.txbToolTip = new System.Windows.Forms.TextBox();
            this.lblRadioButtonText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txbToolTip
            // 
            this.txbToolTip.Location = new System.Drawing.Point(146, 10);
            this.txbToolTip.Name = "txbToolTip";
            this.txbToolTip.Size = new System.Drawing.Size(213, 20);
            this.txbToolTip.TabIndex = 1;
            // 
            // lblRadioButtonText
            // 
            this.lblRadioButtonText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRadioButtonText.Location = new System.Drawing.Point(24, 10);
            this.lblRadioButtonText.Name = "lblRadioButtonText";
            this.lblRadioButtonText.Size = new System.Drawing.Size(116, 20);
            this.lblRadioButtonText.TabIndex = 2;
            this.lblRadioButtonText.Text = "label1";
            this.lblRadioButtonText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 25);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // UCToolTipRadioButtonItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblRadioButtonText);
            this.Controls.Add(this.txbToolTip);
            this.Name = "UCToolTipRadioButtonItem";
            this.Size = new System.Drawing.Size(364, 40);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbToolTip;
        private System.Windows.Forms.Label lblRadioButtonText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
