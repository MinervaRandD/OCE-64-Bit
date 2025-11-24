namespace ImageReplacer
{
    using System.Windows.Forms;

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
            this.grbOutputVisioProject = new System.Windows.Forms.GroupBox();
            this.txbOutputVisioProject = new System.Windows.Forms.TextBox();
            this.btnBrowseOutputVisioProject = new System.Windows.Forms.Button();
            this.grbReplacementImage = new System.Windows.Forms.GroupBox();
            this.txbReplacementImage = new System.Windows.Forms.TextBox();
            this.btnBrowseReplacementImage = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveAndLaunchVisio = new System.Windows.Forms.Button();
            this.grbOutputVisioProject.SuspendLayout();
            this.grbReplacementImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbOutputVisioProject
            // 
            this.grbOutputVisioProject.Controls.Add(this.txbOutputVisioProject);
            this.grbOutputVisioProject.Controls.Add(this.btnBrowseOutputVisioProject);
            this.grbOutputVisioProject.Location = new System.Drawing.Point(60, 27);
            this.grbOutputVisioProject.Name = "grbOutputVisioProject";
            this.grbOutputVisioProject.Size = new System.Drawing.Size(668, 71);
            this.grbOutputVisioProject.TabIndex = 1;
            this.grbOutputVisioProject.TabStop = false;
            this.grbOutputVisioProject.Text = "Output Visio Project";
            // 
            // txbOutputVisioProject
            // 
            this.txbOutputVisioProject.Location = new System.Drawing.Point(11, 19);
            this.txbOutputVisioProject.Multiline = true;
            this.txbOutputVisioProject.Name = "txbOutputVisioProject";
            this.txbOutputVisioProject.Size = new System.Drawing.Size(552, 37);
            this.txbOutputVisioProject.TabIndex = 6;
            // 
            // btnBrowseOutputVisioProject
            // 
            this.btnBrowseOutputVisioProject.Location = new System.Drawing.Point(578, 18);
            this.btnBrowseOutputVisioProject.Name = "btnBrowseOutputVisioProject";
            this.btnBrowseOutputVisioProject.Size = new System.Drawing.Size(64, 20);
            this.btnBrowseOutputVisioProject.TabIndex = 5;
            this.btnBrowseOutputVisioProject.Text = "Browse";
            this.btnBrowseOutputVisioProject.UseVisualStyleBackColor = true;
            this.btnBrowseOutputVisioProject.Click += new System.EventHandler(this.btnBrowseOutputVisioProject_Click);
            // 
            // grbReplacementImage
            // 
            this.grbReplacementImage.Controls.Add(this.txbReplacementImage);
            this.grbReplacementImage.Controls.Add(this.btnBrowseReplacementImage);
            this.grbReplacementImage.Location = new System.Drawing.Point(60, 137);
            this.grbReplacementImage.Name = "grbReplacementImage";
            this.grbReplacementImage.Size = new System.Drawing.Size(668, 77);
            this.grbReplacementImage.TabIndex = 2;
            this.grbReplacementImage.TabStop = false;
            this.grbReplacementImage.Text = "Original Image";
            // 
            // txbReplacementImage
            // 
            this.txbReplacementImage.Location = new System.Drawing.Point(11, 19);
            this.txbReplacementImage.Multiline = true;
            this.txbReplacementImage.Name = "txbReplacementImage";
            this.txbReplacementImage.Size = new System.Drawing.Size(552, 38);
            this.txbReplacementImage.TabIndex = 7;
            // 
            // btnBrowseReplacementImage
            // 
            this.btnBrowseReplacementImage.Location = new System.Drawing.Point(578, 18);
            this.btnBrowseReplacementImage.Name = "btnBrowseReplacementImage";
            this.btnBrowseReplacementImage.Size = new System.Drawing.Size(64, 20);
            this.btnBrowseReplacementImage.TabIndex = 6;
            this.btnBrowseReplacementImage.Text = "Browse";
            this.btnBrowseReplacementImage.UseVisualStyleBackColor = true;
            this.btnBrowseReplacementImage.Click += new System.EventHandler(this.btnBrowseReplacementImage_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(139, 248);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(64, 20);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Save";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(573, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 20);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveAndLaunchVisio
            // 
            this.btnSaveAndLaunchVisio.Location = new System.Drawing.Point(271, 248);
            this.btnSaveAndLaunchVisio.Name = "btnSaveAndLaunchVisio";
            this.btnSaveAndLaunchVisio.Size = new System.Drawing.Size(148, 20);
            this.btnSaveAndLaunchVisio.TabIndex = 5;
            this.btnSaveAndLaunchVisio.Text = "Save And Launch Visio";
            this.btnSaveAndLaunchVisio.UseVisualStyleBackColor = true;
            this.btnSaveAndLaunchVisio.Click += new System.EventHandler(this.btnSaveAndLaunchVisio_Click);
            // 
            // ImageReplacerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 298);
            this.Controls.Add(this.btnSaveAndLaunchVisio);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grbReplacementImage);
            this.Controls.Add(this.grbOutputVisioProject);
            this.Name = "ImageReplacerForm";
            this.Text = "Image Replacer";
            this.grbOutputVisioProject.ResumeLayout(false);
            this.grbOutputVisioProject.PerformLayout();
            this.grbReplacementImage.ResumeLayout(false);
            this.grbReplacementImage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox grbOutputVisioProject;
        private TextBox txbOutputVisioProject;
        private Button btnBrowseOutputVisioProject;
        private GroupBox grbReplacementImage;
        private TextBox txbReplacementImage;
        private Button btnBrowseReplacementImage;
        private Button btnUpdate;
        private Button btnCancel;
        private Button btnSaveAndLaunchVisio;
    }
}