namespace FloorMaterialEstimator.Supporting_Forms
{
    partial class CounterRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.txbDescription = new System.Windows.Forms.TextBox();
            this.ckbShow = new System.Windows.Forms.CheckBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCount.Location = new System.Drawing.Point(1, 6);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(43, 23);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTag
            // 
            this.lblTag.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(51, 6);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(26, 23);
            this.lblTag.TabIndex = 1;
            this.lblTag.Text = "A";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbDescription
            // 
            this.txbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDescription.Location = new System.Drawing.Point(84, 7);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(248, 22);
            this.txbDescription.TabIndex = 0;
            this.txbDescription.TabStop = false;
            // 
            // ckbShow
            // 
            this.ckbShow.AutoSize = true;
            this.ckbShow.Location = new System.Drawing.Point(339, 11);
            this.ckbShow.Name = "ckbShow";
            this.ckbShow.Size = new System.Drawing.Size(15, 14);
            this.ckbShow.TabIndex = 3;
            this.ckbShow.TabStop = false;
            this.ckbShow.UseVisualStyleBackColor = true;
            // 
            // btnColor
            // 
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColor.Location = new System.Drawing.Point(366, 7);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 23);
            this.btnColor.TabIndex = 4;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // CounterRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.ckbShow);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblCount);
            this.Name = "CounterRow";
            this.Size = new System.Drawing.Size(404, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.CheckBox ckbShow;
        public System.Windows.Forms.TextBox txbDescription;
        private System.Windows.Forms.Button btnColor;
    }
}
