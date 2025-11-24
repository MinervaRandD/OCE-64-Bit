namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCCustomColorPallet
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
            this.btnSystemColors = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSystemColors
            // 
            this.btnSystemColors.Location = new System.Drawing.Point(60, 230);
            this.btnSystemColors.Name = "btnSystemColors";
            this.btnSystemColors.Size = new System.Drawing.Size(96, 23);
            this.btnSystemColors.TabIndex = 0;
            this.btnSystemColors.Text = "System Colors";
            this.btnSystemColors.UseVisualStyleBackColor = true;
            this.btnSystemColors.Click += new System.EventHandler(this.BtnSystemColors_Click);
            // 
            // UCCustomColorPallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnSystemColors);
            this.Name = "UCCustomColorPallet";
            this.Size = new System.Drawing.Size(238, 264);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSystemColors;

    }
}
