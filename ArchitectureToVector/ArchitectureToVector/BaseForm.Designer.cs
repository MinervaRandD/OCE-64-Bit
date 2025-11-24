
namespace ArchitectureToVector
{
    partial class BaseForm
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
            this.pbxInput = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxInput)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxInput
            // 
            this.pbxInput.Location = new System.Drawing.Point(130, 91);
            this.pbxInput.Name = "pbxInput";
            this.pbxInput.Size = new System.Drawing.Size(1439, 849);
            this.pbxInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxInput.TabIndex = 0;
            this.pbxInput.TabStop = false;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 1344);
            this.Controls.Add(this.pbxInput);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbxInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxInput;
    }
}

