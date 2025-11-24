namespace FloorMaterialEstimator.Dialog_Boxes
{
    partial class NewBlankProjectWarning
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
            this.btnOpenAnyway = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(105, 69);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnOpenAnyway
            // 
            this.btnOpenAnyway.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOpenAnyway.Location = new System.Drawing.Point(230, 69);
            this.btnOpenAnyway.Name = "btnOpenAnyway";
            this.btnOpenAnyway.Size = new System.Drawing.Size(83, 23);
            this.btnOpenAnyway.TabIndex = 1;
            this.btnOpenAnyway.Text = "Open Anyway";
            this.btnOpenAnyway.UseVisualStyleBackColor = true;
            this.btnOpenAnyway.Click += new System.EventHandler(this.btnOpenAnyway_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 45);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please complete the current drawing operation before creating a blank project.";
            // 
            // NewBlankProjectWarning
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOpenAnyway;
            this.ClientSize = new System.Drawing.Size(344, 104);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenAnyway);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewBlankProjectWarning";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnOpenAnyway;
        private System.Windows.Forms.Label label1;
    }
}