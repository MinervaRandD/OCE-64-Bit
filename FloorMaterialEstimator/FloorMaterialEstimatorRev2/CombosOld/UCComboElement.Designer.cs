namespace CombosOld
{
    partial class UCComboElement
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
            this.lblElementNumber = new System.Windows.Forms.Label();
            this.lblFlipped = new System.Windows.Forms.Label();
            this.lblGroupNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblElementNumber
            // 
            this.lblElementNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblElementNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElementNumber.Location = new System.Drawing.Point(1, 1);
            this.lblElementNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblElementNumber.Name = "lblElementNumber";
            this.lblElementNumber.Size = new System.Drawing.Size(32, 24);
            this.lblElementNumber.TabIndex = 0;
            this.lblElementNumber.Text = "Cut";
            this.lblElementNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFlipped
            // 
            this.lblFlipped.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFlipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlipped.Location = new System.Drawing.Point(85, 1);
            this.lblFlipped.Margin = new System.Windows.Forms.Padding(0);
            this.lblFlipped.Name = "lblFlipped";
            this.lblFlipped.Size = new System.Drawing.Size(30, 24);
            this.lblFlipped.TabIndex = 2;
            this.lblFlipped.Text = "X";
            this.lblFlipped.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupNumber
            // 
            this.lblGroupNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupNumber.Location = new System.Drawing.Point(34, 1);
            this.lblGroupNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroupNumber.Name = "lblGroupNumber";
            this.lblGroupNumber.Size = new System.Drawing.Size(49, 24);
            this.lblGroupNumber.TabIndex = 3;
            this.lblGroupNumber.Text = "Grp";
            this.lblGroupNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCComboElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblGroupNumber);
            this.Controls.Add(this.lblFlipped);
            this.Controls.Add(this.lblElementNumber);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCComboElement";
            this.Size = new System.Drawing.Size(116, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblElementNumber;
        public System.Windows.Forms.Label lblFlipped;
        public System.Windows.Forms.Label lblGroupNumber;
    }
}
