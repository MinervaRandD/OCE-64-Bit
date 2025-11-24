namespace FloorMaterialEstimator
{
    partial class ComboOptionsForm
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
            this.flpCombOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpCombOptions
            // 
            this.flpCombOptions.BackColor = System.Drawing.SystemColors.Control;
            this.flpCombOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCombOptions.Location = new System.Drawing.Point(25, 13);
            this.flpCombOptions.Name = "flpCombOptions";
            this.flpCombOptions.Size = new System.Drawing.Size(1188, 911);
            this.flpCombOptions.TabIndex = 0;
            this.flpCombOptions.WrapContents = false;
            // 
            // ComboOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 936);
            this.Controls.Add(this.flpCombOptions);
            this.Name = "ComboOptionsForm";
            this.Text = "Combination Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpCombOptions;
    }
}