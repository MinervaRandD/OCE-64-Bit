namespace FloorMaterialEstimator.Dialog_Boxes
{
    partial class ChooseCompleteLSolution
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
            this.btnSelectMinimumArea = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectMaximumArea = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectMinimumArea
            // 
            this.btnSelectMinimumArea.Location = new System.Drawing.Point(61, 71);
            this.btnSelectMinimumArea.Name = "btnSelectMinimumArea";
            this.btnSelectMinimumArea.Size = new System.Drawing.Size(158, 23);
            this.btnSelectMinimumArea.TabIndex = 0;
            this.btnSelectMinimumArea.Text = "Select Minimum Area - '-*'";
            this.btnSelectMinimumArea.UseVisualStyleBackColor = true;
            this.btnSelectMinimumArea.Click += new System.EventHandler(this.btnSelectMinimumArea_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(61, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(158, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 45);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please choose between two possible complete-L solutions";
            // 
            // btnSelectMaximumArea
            // 
            this.btnSelectMaximumArea.Location = new System.Drawing.Point(61, 108);
            this.btnSelectMaximumArea.Name = "btnSelectMaximumArea";
            this.btnSelectMaximumArea.Size = new System.Drawing.Size(158, 23);
            this.btnSelectMaximumArea.TabIndex = 3;
            this.btnSelectMaximumArea.Text = "Select Maximum Area - '+*'";
            this.btnSelectMaximumArea.UseVisualStyleBackColor = true;
            this.btnSelectMaximumArea.Click += new System.EventHandler(this.btnSelectMaximumArea_Click);
            // 
            // ChooseCompleteLSolution
            // 
            this.AcceptButton = this.btnSelectMinimumArea;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 207);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelectMaximumArea);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectMinimumArea);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseCompleteLSolution";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectMinimumArea;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectMaximumArea;
    }
}