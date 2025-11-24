
namespace Utilities.User_Controls
{
    partial class SelectionHighlight
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
            this.pnlHighlightBorder = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlHighlightBorder
            // 
            this.pnlHighlightBorder.Location = new System.Drawing.Point(16, 14);
            this.pnlHighlightBorder.Name = "pnlHighlightBorder";
            this.pnlHighlightBorder.Size = new System.Drawing.Size(559, 398);
            this.pnlHighlightBorder.TabIndex = 0;
            // 
            // SelectionHighlight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlHighlightBorder);
            this.Name = "SelectionHighlight";
            this.Size = new System.Drawing.Size(594, 432);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHighlightBorder;
    }
}
