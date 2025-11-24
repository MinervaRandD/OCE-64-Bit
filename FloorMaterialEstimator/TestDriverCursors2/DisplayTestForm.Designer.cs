namespace TestDriverCursors
{
    partial class DisplayTestForm
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
            this.lblCursorLocation = new System.Windows.Forms.Label();
            this.lblWithinBounds = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cursor Position";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCursorLocation
            // 
            this.lblCursorLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCursorLocation.Location = new System.Drawing.Point(171, 52);
            this.lblCursorLocation.Name = "lblCursorLocation";
            this.lblCursorLocation.Size = new System.Drawing.Size(130, 23);
            this.lblCursorLocation.TabIndex = 1;
            this.lblCursorLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWithinBounds
            // 
            this.lblWithinBounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWithinBounds.Location = new System.Drawing.Point(171, 111);
            this.lblWithinBounds.Name = "lblWithinBounds";
            this.lblWithinBounds.Size = new System.Drawing.Size(130, 23);
            this.lblWithinBounds.TabIndex = 3;
            this.lblWithinBounds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Within Bounds";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 448);
            this.Controls.Add(this.lblWithinBounds);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCursorLocation);
            this.Controls.Add(this.label1);
            this.Name = "TestForm1";
            this.Text = "TestForm1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCursorLocation;
        private System.Windows.Forms.Label lblWithinBounds;
        private System.Windows.Forms.Label label3;
    }
}