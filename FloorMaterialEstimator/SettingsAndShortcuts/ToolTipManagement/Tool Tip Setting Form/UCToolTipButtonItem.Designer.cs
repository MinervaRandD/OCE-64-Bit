
namespace SettingsLib
{
    partial class UCToolTipButtonItem
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
            this.txbToolTip = new System.Windows.Forms.TextBox();
            this.btnToolTipButtonItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbToolTip
            // 
            this.txbToolTip.Location = new System.Drawing.Point(116, 14);
            this.txbToolTip.Name = "txbToolTip";
            this.txbToolTip.Size = new System.Drawing.Size(213, 20);
            this.txbToolTip.TabIndex = 1;
            // 
            // btnToolTipButtonItem
            // 
            this.btnToolTipButtonItem.Location = new System.Drawing.Point(4, 14);
            this.btnToolTipButtonItem.Name = "btnToolTipButtonItem";
            this.btnToolTipButtonItem.Size = new System.Drawing.Size(106, 23);
            this.btnToolTipButtonItem.TabIndex = 2;
            this.btnToolTipButtonItem.Text = "button1";
            this.btnToolTipButtonItem.UseVisualStyleBackColor = true;
            // 
            // UCToolTipButtonItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnToolTipButtonItem);
            this.Controls.Add(this.txbToolTip);
            this.Name = "UCToolTipButtonItem";
            this.Size = new System.Drawing.Size(332, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbToolTip;
        private System.Windows.Forms.Button btnToolTipButtonItem;
    }
}
