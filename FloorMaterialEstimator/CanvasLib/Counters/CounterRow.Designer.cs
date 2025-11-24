namespace CanvasLib.Counters
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
            this.txbSize = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCount.Location = new System.Drawing.Point(41, 4);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(33, 23);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTag
            // 
            this.lblTag.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(82, 4);
            this.lblTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(26, 23);
            this.lblTag.TabIndex = 1;
            this.lblTag.Text = "A";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbDescription
            // 
            this.txbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDescription.Location = new System.Drawing.Point(118, 5);
            this.txbDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(214, 22);
            this.txbDescription.TabIndex = 0;
            this.txbDescription.TabStop = false;
            // 
            // ckbShow
            // 
            this.ckbShow.AutoSize = true;
            this.ckbShow.Location = new System.Drawing.Point(490, 8);
            this.ckbShow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ckbShow.Name = "ckbShow";
            this.ckbShow.Size = new System.Drawing.Size(15, 14);
            this.ckbShow.TabIndex = 3;
            this.ckbShow.TabStop = false;
            this.ckbShow.UseVisualStyleBackColor = true;
            // 
            // btnColor
            // 
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColor.Location = new System.Drawing.Point(516, 4);
            this.btnColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 23);
            this.btnColor.TabIndex = 4;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // txbSize
            // 
            this.txbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbSize.Location = new System.Drawing.Point(345, 5);
            this.txbSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbSize.Name = "txbSize";
            this.txbSize.Size = new System.Drawing.Size(65, 22);
            this.txbSize.TabIndex = 5;
            this.txbSize.TabStop = false;
            this.txbSize.Text = "9,999.123";
            this.txbSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Location = new System.Drawing.Point(420, 4);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(60, 23);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "99,999.9";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(7, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(24, 23);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "+";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // CounterRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txbSize);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.ckbShow);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblCount);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CounterRow";
            this.Size = new System.Drawing.Size(542, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.CheckBox ckbShow;
        public System.Windows.Forms.TextBox txbDescription;
        private System.Windows.Forms.Button btnColor;
        public System.Windows.Forms.TextBox txbSize;
        public System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnSelect;
    }
}
