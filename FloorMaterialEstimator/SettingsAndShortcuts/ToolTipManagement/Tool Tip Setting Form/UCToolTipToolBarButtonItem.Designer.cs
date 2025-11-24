
namespace SettingsLib
{
    partial class UCToolTipToolBarButtonItem
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
            this.pcbToolStripButtonImage = new System.Windows.Forms.PictureBox();
            this.txbToolTip = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbToolStripButtonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbToolStripButtonImage
            // 
            this.pcbToolStripButtonImage.Location = new System.Drawing.Point(4, 2);
            this.pcbToolStripButtonImage.Name = "pcbToolStripButtonImage";
            this.pcbToolStripButtonImage.Size = new System.Drawing.Size(38, 38);
            this.pcbToolStripButtonImage.TabIndex = 0;
            this.pcbToolStripButtonImage.TabStop = false;
            // 
            // txbToolTip
            // 
            this.txbToolTip.Location = new System.Drawing.Point(48, 11);
            this.txbToolTip.Name = "txbToolTip";
            this.txbToolTip.Size = new System.Drawing.Size(213, 20);
            this.txbToolTip.TabIndex = 1;
            // 
            // ToolTipToolBarButtonItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbToolTip);
            this.Controls.Add(this.pcbToolStripButtonImage);
            this.Name = "ToolTipToolBarButtonItem";
            this.Size = new System.Drawing.Size(264, 40);
            ((System.ComponentModel.ISupportInitialize)(this.pcbToolStripButtonImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbToolStripButtonImage;
        private System.Windows.Forms.TextBox txbToolTip;
    }
}
