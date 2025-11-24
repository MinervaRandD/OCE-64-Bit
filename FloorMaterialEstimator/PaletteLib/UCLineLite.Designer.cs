namespace PaletteLib
{
    partial class UCLineLite
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
            this.lblLineName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineName.Location = new System.Drawing.Point(13, 4);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(100, 18);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "Line";
            // 
            // UCLineLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLineName);
            this.Name = "UCLineLite";
            this.Size = new System.Drawing.Size(190, 70);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
    }
}
