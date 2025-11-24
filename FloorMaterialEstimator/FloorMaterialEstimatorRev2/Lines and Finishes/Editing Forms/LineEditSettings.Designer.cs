namespace FloorMaterialEstimator.Supporting_Forms
{
    partial class LineEditSettings
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSetAsDefault = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trbIntensity = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.nudIntensity = new Utilities.CustomNumericUpDown();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trbIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(34, 470);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSetAsDefault
            // 
            this.btnSetAsDefault.Location = new System.Drawing.Point(125, 470);
            this.btnSetAsDefault.Name = "btnSetAsDefault";
            this.btnSetAsDefault.Size = new System.Drawing.Size(91, 23);
            this.btnSetAsDefault.TabIndex = 2;
            this.btnSetAsDefault.Text = "Set As Default";
            this.btnSetAsDefault.UseVisualStyleBackColor = true;
            this.btnSetAsDefault.Click += new System.EventHandler(this.btnSetAsDefault_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(233, 470);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Line Edit Settings";
            // 
            // trbIntensity
            // 
            this.trbIntensity.Location = new System.Drawing.Point(35, 410);
            this.trbIntensity.Maximum = 100;
            this.trbIntensity.Name = "trbIntensity";
            this.trbIntensity.Size = new System.Drawing.Size(283, 45);
            this.trbIntensity.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Intensity";
            // 
            // nudIntensity
            // 
            this.nudIntensity.Location = new System.Drawing.Point(246, 378);
            this.nudIntensity.Name = "nudIntensity";
            this.nudIntensity.Size = new System.Drawing.Size(62, 20);
            this.nudIntensity.TabIndex = 9;
            this.nudIntensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnColor1
            // 
            this.btnColor1.Location = new System.Drawing.Point(38, 292);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(58, 54);
            this.btnColor1.TabIndex = 13;
            this.btnColor1.UseVisualStyleBackColor = true;
            // 
            // btnColor2
            // 
            this.btnColor2.Location = new System.Drawing.Point(125, 292);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(58, 54);
            this.btnColor2.TabIndex = 14;
            this.btnColor2.UseVisualStyleBackColor = true;
            // 
            // LineEditSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(339, 527);
            this.ControlBox = false;
            this.Controls.Add(this.btnColor2);
            this.Controls.Add(this.btnColor1);
            this.Controls.Add(this.nudIntensity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trbIntensity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetAsDefault);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LineEditSettings";
            ((System.ComponentModel.ISupportInitialize)(this.trbIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSetAsDefault;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trbIntensity;
        private System.Windows.Forms.Label label2;
        private Utilities.CustomNumericUpDown nudIntensity;
        private System.Windows.Forms.Button btnColor1;
        private System.Windows.Forms.Button btnColor2;
    }
}