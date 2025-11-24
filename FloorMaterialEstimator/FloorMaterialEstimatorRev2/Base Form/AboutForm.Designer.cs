namespace FloorMaterialEstimator
{
    partial class AboutForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReleaseDateLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblErrorLogFilePath = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMessageLogFilePath = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Floor Materials Estimator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(133, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bruun Estimating, LLC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(348, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Developed by: Minerva Research and Development, LLC";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ReleaseDateLabel);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.VersionLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(70, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 100);
            this.panel1.TabIndex = 7;
            // 
            // ReleaseDateLabel
            // 
            this.ReleaseDateLabel.AutoSize = true;
            this.ReleaseDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReleaseDateLabel.Location = new System.Drawing.Point(112, 64);
            this.ReleaseDateLabel.Name = "ReleaseDateLabel";
            this.ReleaseDateLabel.Size = new System.Drawing.Size(80, 18);
            this.ReleaseDateLabel.TabIndex = 9;
            this.ReleaseDateLabel.Text = "06/01/2025";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Release Date:";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(104, 18);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(100, 18);
            this.VersionLabel.TabIndex = 7;
            this.VersionLabel.Text = "2025.06.01.01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Version:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(169, 395);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Messages are logged to:";
            // 
            // lblErrorLogFilePath
            // 
            this.lblErrorLogFilePath.Location = new System.Drawing.Point(151, 265);
            this.lblErrorLogFilePath.Name = "lblErrorLogFilePath";
            this.lblErrorLogFilePath.Size = new System.Drawing.Size(260, 19);
            this.lblErrorLogFilePath.TabIndex = 10;
            this.lblErrorLogFilePath.Text = "errorlog.txt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Errors are logged to:";
            // 
            // lblMessageLogFilePath
            // 
            this.lblMessageLogFilePath.Location = new System.Drawing.Point(152, 231);
            this.lblMessageLogFilePath.Name = "lblMessageLogFilePath";
            this.lblMessageLogFilePath.Size = new System.Drawing.Size(260, 19);
            this.lblMessageLogFilePath.TabIndex = 12;
            this.lblMessageLogFilePath.Text = "messagelog.txt";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 460);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessageLogFilePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblErrorLogFilePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AboutForm";
            this.Text = "About Floor Materials Estimator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ReleaseDateLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblErrorLogFilePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessageLogFilePath;
    }
}