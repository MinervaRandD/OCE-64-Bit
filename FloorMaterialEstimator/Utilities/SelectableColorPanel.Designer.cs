namespace Utilities
{
    partial class SelectableColorPanel
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
            this.pnlColorPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlColorPanel
            // 
            this.pnlColorPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlColorPanel.Location = new System.Drawing.Point(4, 4);
            this.pnlColorPanel.Name = "pnlColorPanel";
            this.pnlColorPanel.Size = new System.Drawing.Size(35, 35);
            this.pnlColorPanel.TabIndex = 0;
            // 
            // SelectableColorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlColorPanel);
            this.Name = "SelectableColorPanel";
            this.Size = new System.Drawing.Size(42, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlColorPanel;
    }
}
