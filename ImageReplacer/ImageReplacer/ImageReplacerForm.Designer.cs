namespace ImageReplacer
{
    partial class ImageReplacerForm
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
            this.grbInputVisioProject = new System.Windows.Forms.GroupBox();
            this.grbOutputVisioProject = new System.Windows.Forms.GroupBox();
            this.grbReplacementImage = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnBrowseInputVisioProject = new System.Windows.Forms.Button();
            this.btnBrowseOutputVisioProject = new System.Windows.Forms.Button();
            this.btnBrowseReplacementImage = new System.Windows.Forms.Button();
            this.txbInputVisioProject = new System.Windows.Forms.TextBox();
            this.txbOutputVisioProject = new System.Windows.Forms.TextBox();
            this.txbReplacementImage = new System.Windows.Forms.TextBox();
            this.grbInputVisioProject.SuspendLayout();
            this.grbOutputVisioProject.SuspendLayout();
            this.grbReplacementImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbInputVisioProject
            // 
            this.grbInputVisioProject.Controls.Add(this.txbInputVisioProject);
            this.grbInputVisioProject.Controls.Add(this.btnBrowseInputVisioProject);
            this.grbInputVisioProject.Location = new System.Drawing.Point(70, 41);
            this.grbInputVisioProject.Name = "grbInputVisioProject";
            this.grbInputVisioProject.Size = new System.Drawing.Size(668, 58);
            this.grbInputVisioProject.TabIndex = 0;
            this.grbInputVisioProject.TabStop = false;
            this.grbInputVisioProject.Text = "Input Visio Project";
            // 
            // grbOutputVisioProject
            // 
            this.grbOutputVisioProject.Controls.Add(this.txbOutputVisioProject);
            this.grbOutputVisioProject.Controls.Add(this.btnBrowseOutputVisioProject);
            this.grbOutputVisioProject.Location = new System.Drawing.Point(70, 127);
            this.grbOutputVisioProject.Name = "grbOutputVisioProject";
            this.grbOutputVisioProject.Size = new System.Drawing.Size(668, 58);
            this.grbOutputVisioProject.TabIndex = 1;
            this.grbOutputVisioProject.TabStop = false;
            this.grbOutputVisioProject.Text = "Output Visio Project";
            // 
            // grbReplacementImage
            // 
            this.grbReplacementImage.Controls.Add(this.txbReplacementImage);
            this.grbReplacementImage.Controls.Add(this.btnBrowseReplacementImage);
            this.grbReplacementImage.Location = new System.Drawing.Point(70, 231);
            this.grbReplacementImage.Name = "grbReplacementImage";
            this.grbReplacementImage.Size = new System.Drawing.Size(668, 58);
            this.grbReplacementImage.TabIndex = 2;
            this.grbReplacementImage.TabStop = false;
            this.grbReplacementImage.Text = "Replacement Image";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(376, 336);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnBrowseInputVisioProject
            // 
            this.btnBrowseInputVisioProject.Location = new System.Drawing.Point(565, 22);
            this.btnBrowseInputVisioProject.Name = "btnBrowseInputVisioProject";
            this.btnBrowseInputVisioProject.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInputVisioProject.TabIndex = 4;
            this.btnBrowseInputVisioProject.Text = "Browse";
            this.btnBrowseInputVisioProject.UseVisualStyleBackColor = true;
            this.btnBrowseInputVisioProject.Click += new System.EventHandler(this.btnBrowseInputVisioProject_Click);
            // 
            // btnBrowseOutputVisioProject
            // 
            this.btnBrowseOutputVisioProject.Location = new System.Drawing.Point(565, 22);
            this.btnBrowseOutputVisioProject.Name = "btnBrowseOutputVisioProject";
            this.btnBrowseOutputVisioProject.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutputVisioProject.TabIndex = 5;
            this.btnBrowseOutputVisioProject.Text = "Browse";
            this.btnBrowseOutputVisioProject.UseVisualStyleBackColor = true;
            this.btnBrowseOutputVisioProject.Click += new System.EventHandler(this.btnBrowseOutputVisioProject_Click);
            // 
            // btnBrowseReplacementImage
            // 
            this.btnBrowseReplacementImage.Location = new System.Drawing.Point(565, 22);
            this.btnBrowseReplacementImage.Name = "btnBrowseReplacementImage";
            this.btnBrowseReplacementImage.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseReplacementImage.TabIndex = 6;
            this.btnBrowseReplacementImage.Text = "Browse";
            this.btnBrowseReplacementImage.UseVisualStyleBackColor = true;
            this.btnBrowseReplacementImage.Click += new System.EventHandler(this.btnBrowseReplacementImage_Click);
            // 
            // txbInputVisioProject
            // 
            this.txbInputVisioProject.Location = new System.Drawing.Point(13, 23);
            this.txbInputVisioProject.Name = "txbInputVisioProject";
            this.txbInputVisioProject.Size = new System.Drawing.Size(535, 23);
            this.txbInputVisioProject.TabIndex = 5;
            // 
            // txbOutputVisioProject
            // 
            this.txbOutputVisioProject.Location = new System.Drawing.Point(13, 22);
            this.txbOutputVisioProject.Name = "txbOutputVisioProject";
            this.txbOutputVisioProject.Size = new System.Drawing.Size(535, 23);
            this.txbOutputVisioProject.TabIndex = 6;
            // 
            // txbReplacementImage
            // 
            this.txbReplacementImage.Location = new System.Drawing.Point(13, 22);
            this.txbReplacementImage.Name = "txbReplacementImage";
            this.txbReplacementImage.Size = new System.Drawing.Size(535, 23);
            this.txbReplacementImage.TabIndex = 7;
            // 
            // ImageReplacerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 409);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grbReplacementImage);
            this.Controls.Add(this.grbOutputVisioProject);
            this.Controls.Add(this.grbInputVisioProject);
            this.Name = "ImageReplacerForm";
            this.Text = "Image Replacer";
            this.grbInputVisioProject.ResumeLayout(false);
            this.grbInputVisioProject.PerformLayout();
            this.grbOutputVisioProject.ResumeLayout(false);
            this.grbOutputVisioProject.PerformLayout();
            this.grbReplacementImage.ResumeLayout(false);
            this.grbReplacementImage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grbInputVisioProject;
        private TextBox txbInputVisioProject;
        private Button btnBrowseInputVisioProject;
        private GroupBox grbOutputVisioProject;
        private TextBox txbOutputVisioProject;
        private Button btnBrowseOutputVisioProject;
        private GroupBox grbReplacementImage;
        private TextBox txbReplacementImage;
        private Button btnBrowseReplacementImage;
        private Button btnUpdate;
    }
}