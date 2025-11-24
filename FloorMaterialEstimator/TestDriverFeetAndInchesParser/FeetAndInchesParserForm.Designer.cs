namespace TestDriverFeetAndInchesParser
{
    partial class FeetAndInchesParserForm
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
            this.txbFeetAndInches = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.ckbAllowNegatives = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txbFeetAndInches
            // 
            this.txbFeetAndInches.Location = new System.Drawing.Point(26, 26);
            this.txbFeetAndInches.Name = "txbFeetAndInches";
            this.txbFeetAndInches.Size = new System.Drawing.Size(111, 20);
            this.txbFeetAndInches.TabIndex = 0;
            this.txbFeetAndInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Result: ";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(166, 26);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(166, 71);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(29, 13);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "True";
            // 
            // ckbAllowNegatives
            // 
            this.ckbAllowNegatives.AutoSize = true;
            this.ckbAllowNegatives.Location = new System.Drawing.Point(265, 31);
            this.ckbAllowNegatives.Name = "ckbAllowNegatives";
            this.ckbAllowNegatives.Size = new System.Drawing.Size(102, 17);
            this.ckbAllowNegatives.TabIndex = 4;
            this.ckbAllowNegatives.Text = "Allow Negatives";
            this.ckbAllowNegatives.UseVisualStyleBackColor = true;
            // 
            // FeetAndInchesParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 93);
            this.Controls.Add(this.ckbAllowNegatives);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbFeetAndInches);
            this.Name = "FeetAndInchesParserForm";
            this.Text = "Feet and Inch Parser Testing Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbFeetAndInches;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.CheckBox ckbAllowNegatives;
    }
}

