namespace FloorMaterialEstimator
{
    partial class UCKeyboardAndMouseActionsElement
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
            this.lblShortcut = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblShortcut
            // 
            this.lblShortcut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShortcut.Location = new System.Drawing.Point(4, 0);
            this.lblShortcut.Name = "lblShortcut";
            this.lblShortcut.Size = new System.Drawing.Size(160, 23);
            this.lblShortcut.TabIndex = 0;
            this.lblShortcut.Text = "Shortcut";
            this.lblShortcut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAction
            // 
            this.lblAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAction.Location = new System.Drawing.Point(160, 0);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(440, 23);
            this.lblAction.TabIndex = 1;
            this.lblAction.Text = "Action";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCFixedShortcutElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.lblShortcut);
            this.Name = "UCFixedShortcutElement";
            this.Size = new System.Drawing.Size(604, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblShortcut;
        private System.Windows.Forms.Label lblAction;
    }
}
