namespace TestDriverMessageBoxAdv
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLaunchMessageBox = new Button();
            SuspendLayout();
            // 
            // btnLaunchMessageBox
            // 
            btnLaunchMessageBox.Location = new Point(330, 180);
            btnLaunchMessageBox.Name = "btnLaunchMessageBox";
            btnLaunchMessageBox.Size = new Size(142, 23);
            btnLaunchMessageBox.TabIndex = 0;
            btnLaunchMessageBox.Text = "Launch Messge Box";
            btnLaunchMessageBox.UseVisualStyleBackColor = true;
            btnLaunchMessageBox.Click += btnLaunchMessageBox_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLaunchMessageBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLaunchMessageBox;
    }
}
