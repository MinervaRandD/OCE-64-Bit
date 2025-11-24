namespace CanvasLib.Counters
{
    partial class CounterRowHeader
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSize = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCount.Location = new System.Drawing.Point(34, 6);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(43, 23);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "Count";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTag
            // 
            this.lblTag.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(82, 6);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(33, 23);
            this.lblTag.TabIndex = 1;
            this.lblTag.Text = "Tag";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(480, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Show";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(525, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Color";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbSize
            // 
            this.txbSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbSize.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txbSize.Location = new System.Drawing.Point(351, 6);
            this.txbSize.Name = "txbSize";
            this.txbSize.Size = new System.Drawing.Size(60, 23);
            this.txbSize.TabIndex = 7;
            this.txbSize.Text = "Size";
            this.txbSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Location = new System.Drawing.Point(425, 6);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(50, 23);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CounterRowHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txbSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblCount);
            this.Name = "CounterRowHeader";
            this.Size = new System.Drawing.Size(571, 36);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label txbSize;
        public System.Windows.Forms.Label lblTotal;
    }
}
