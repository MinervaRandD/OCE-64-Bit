namespace CanvasLib.Scale_Line
{
    partial class SetScaleWarningForm
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
            this.lblSetScale = new System.Windows.Forms.Label();
            this.ckbDontRemindAgain = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSetScale
            // 
            this.lblSetScale.AutoSize = true;
            this.lblSetScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetScale.Location = new System.Drawing.Point(12, 9);
            this.lblSetScale.Name = "lblSetScale";
            this.lblSetScale.Size = new System.Drawing.Size(391, 24);
            this.lblSetScale.TabIndex = 0;
            this.lblSetScale.Text = "Please be sure to set the scale for this project.";
            // 
            // ckbDontRemindAgain
            // 
            this.ckbDontRemindAgain.AutoSize = true;
            this.ckbDontRemindAgain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbDontRemindAgain.Location = new System.Drawing.Point(16, 48);
            this.ckbDontRemindAgain.Name = "ckbDontRemindAgain";
            this.ckbDontRemindAgain.Size = new System.Drawing.Size(196, 24);
            this.ckbDontRemindAgain.TabIndex = 1;
            this.ckbDontRemindAgain.Text = "Don\'t Remind Me Again";
            this.ckbDontRemindAgain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbDontRemindAgain.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(183, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SetScaleWarningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 125);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ckbDontRemindAgain);
            this.Controls.Add(this.lblSetScale);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetScaleWarningForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSetScale;
        private System.Windows.Forms.CheckBox ckbDontRemindAgain;
        private System.Windows.Forms.Button btnOK;
    }
}