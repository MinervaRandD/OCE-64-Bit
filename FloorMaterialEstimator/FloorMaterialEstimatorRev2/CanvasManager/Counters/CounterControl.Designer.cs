namespace CanvasLib.Counters
{
    partial class CounterControl
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
            this.lblCountersHeader = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.txbDescription = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCountersHeader
            // 
            this.lblCountersHeader.AutoSize = true;
            this.lblCountersHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountersHeader.Location = new System.Drawing.Point(-2, 4);
            this.lblCountersHeader.Name = "lblCountersHeader";
            this.lblCountersHeader.Size = new System.Drawing.Size(69, 18);
            this.lblCountersHeader.TabIndex = 0;
            this.lblCountersHeader.Text = "Counters";
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(71, 1);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(84, 26);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "Count = ";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-2, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tag";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(6, 63);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(17, 16);
            this.lblTag.TabIndex = 4;
            this.lblTag.Text = "A";
            // 
            // txbDescription
            // 
            this.txbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDescription.Location = new System.Drawing.Point(31, 60);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(123, 22);
            this.txbDescription.TabIndex = 5;
            // 
            // btnActivate
            // 
            this.btnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivate.Location = new System.Drawing.Point(1, 98);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(71, 23);
            this.btnActivate.TabIndex = 6;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            // 
            // btnChange
            // 
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChange.Location = new System.Drawing.Point(83, 98);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(71, 23);
            this.btnChange.TabIndex = 7;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // CounterControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblCountersHeader);
            this.Name = "CounterControl2";
            this.Size = new System.Drawing.Size(163, 126);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCountersHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblCount;
        public System.Windows.Forms.Label lblTag;
        public System.Windows.Forms.TextBox txbDescription;
        public System.Windows.Forms.Button btnActivate;
        public System.Windows.Forms.Button btnChange;
    }
}
