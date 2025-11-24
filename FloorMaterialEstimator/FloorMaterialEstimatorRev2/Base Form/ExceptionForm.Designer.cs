
namespace FloorMaterialEstimator
{
    partial class ExceptionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbException = new System.Windows.Forms.TextBox();
            this.txbCallStack = new System.Windows.Forms.TextBox();
            this.btnCopyInformationToClipboard = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveInformationToFile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveProject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(172, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "The System Has Thrown An Exception";
            // 
            // txbException
            // 
            this.txbException.Location = new System.Drawing.Point(70, 113);
            this.txbException.Multiline = true;
            this.txbException.Name = "txbException";
            this.txbException.Size = new System.Drawing.Size(578, 215);
            this.txbException.TabIndex = 1;
            // 
            // txbCallStack
            // 
            this.txbCallStack.Location = new System.Drawing.Point(70, 390);
            this.txbCallStack.Multiline = true;
            this.txbCallStack.Name = "txbCallStack";
            this.txbCallStack.Size = new System.Drawing.Size(578, 215);
            this.txbCallStack.TabIndex = 2;
            // 
            // btnCopyInformationToClipboard
            // 
            this.btnCopyInformationToClipboard.Location = new System.Drawing.Point(73, 647);
            this.btnCopyInformationToClipboard.Name = "btnCopyInformationToClipboard";
            this.btnCopyInformationToClipboard.Size = new System.Drawing.Size(113, 23);
            this.btnCopyInformationToClipboard.TabIndex = 3;
            this.btnCopyInformationToClipboard.Text = "CopyTo Clipboard";
            this.btnCopyInformationToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyInformationToClipboard.Click += new System.EventHandler(this.btnCopyInformationToClipboard_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Exception";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Call Stack";
            // 
            // btnSaveInformationToFile
            // 
            this.btnSaveInformationToFile.Location = new System.Drawing.Point(233, 647);
            this.btnSaveInformationToFile.Name = "btnSaveInformationToFile";
            this.btnSaveInformationToFile.Size = new System.Drawing.Size(93, 23);
            this.btnSaveInformationToFile.TabIndex = 6;
            this.btnSaveInformationToFile.Text = "Save To File";
            this.btnSaveInformationToFile.UseVisualStyleBackColor = true;
            this.btnSaveInformationToFile.Click += new System.EventHandler(this.btnSaveInformationToFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(552, 647);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.Location = new System.Drawing.Point(375, 647);
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(126, 23);
            this.btnSaveProject.TabIndex = 8;
            this.btnSaveProject.Text = "Save Project";
            this.btnSaveProject.UseVisualStyleBackColor = true;
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 704);
            this.Controls.Add(this.btnSaveProject);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveInformationToFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCopyInformationToClipboard);
            this.Controls.Add(this.txbCallStack);
            this.Controls.Add(this.txbException);
            this.Controls.Add(this.label1);
            this.Name = "ExceptionForm";
            this.Text = "Exception Thrown";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbException;
        private System.Windows.Forms.TextBox txbCallStack;
        private System.Windows.Forms.Button btnCopyInformationToClipboard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveInformationToFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaveProject;
    }
}