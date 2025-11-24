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
            this.BtnActivate = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.rbnSmall = new System.Windows.Forms.RadioButton();
            this.rbnMedium = new System.Windows.Forms.RadioButton();
            this.rbnLarge = new System.Windows.Forms.RadioButton();
            this.ckbHideCircles = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCountersHeader
            // 
            this.lblCountersHeader.AutoSize = true;
            this.lblCountersHeader.BackColor = System.Drawing.Color.Orange;
            this.lblCountersHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountersHeader.Location = new System.Drawing.Point(2, 4);
            this.lblCountersHeader.Name = "lblCountersHeader";
            this.lblCountersHeader.Size = new System.Drawing.Size(52, 18);
            this.lblCountersHeader.TabIndex = 0;
            this.lblCountersHeader.Text = "Count:";
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(50, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(47, 26);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "Count = ";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tag";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.BackColor = System.Drawing.SystemColors.Control;
            this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblTag.Location = new System.Drawing.Point(26, 49);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(17, 16);
            this.lblTag.TabIndex = 4;
            this.lblTag.Text = "A";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnActivate
            // 
            this.BtnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnActivate.Location = new System.Drawing.Point(3, 124);
            this.BtnActivate.Name = "BtnActivate";
            this.BtnActivate.Size = new System.Drawing.Size(86, 23);
            this.BtnActivate.TabIndex = 6;
            this.BtnActivate.Text = "Activate";
            this.BtnActivate.UseVisualStyleBackColor = true;
            // 
            // btnChange
            // 
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChange.Location = new System.Drawing.Point(95, 124);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(94, 23);
            this.btnChange.TabIndex = 7;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblDescription.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescription.Location = new System.Drawing.Point(79, 48);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(110, 18);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "Description";
            // 
            // rbnSmall
            // 
            this.rbnSmall.AutoSize = true;
            this.rbnSmall.Location = new System.Drawing.Point(5, 73);
            this.rbnSmall.Name = "rbnSmall";
            this.rbnSmall.Size = new System.Drawing.Size(50, 17);
            this.rbnSmall.TabIndex = 9;
            this.rbnSmall.TabStop = true;
            this.rbnSmall.Text = "Small";
            this.rbnSmall.UseVisualStyleBackColor = true;
            // 
            // rbnMedium
            // 
            this.rbnMedium.AutoSize = true;
            this.rbnMedium.Location = new System.Drawing.Point(67, 73);
            this.rbnMedium.Name = "rbnMedium";
            this.rbnMedium.Size = new System.Drawing.Size(62, 17);
            this.rbnMedium.TabIndex = 10;
            this.rbnMedium.TabStop = true;
            this.rbnMedium.Text = "Medium";
            this.rbnMedium.UseVisualStyleBackColor = true;
            // 
            // rbnLarge
            // 
            this.rbnLarge.AutoSize = true;
            this.rbnLarge.Location = new System.Drawing.Point(137, 73);
            this.rbnLarge.Name = "rbnLarge";
            this.rbnLarge.Size = new System.Drawing.Size(52, 17);
            this.rbnLarge.TabIndex = 11;
            this.rbnLarge.TabStop = true;
            this.rbnLarge.Text = "Large";
            this.rbnLarge.UseVisualStyleBackColor = true;
            // 
            // ckbHideCircles
            // 
            this.ckbHideCircles.AutoSize = true;
            this.ckbHideCircles.Location = new System.Drawing.Point(66, 97);
            this.ckbHideCircles.Name = "ckbHideCircles";
            this.ckbHideCircles.Size = new System.Drawing.Size(82, 17);
            this.ckbHideCircles.TabIndex = 12;
            this.ckbHideCircles.Text = "Hide Circles";
            this.ckbHideCircles.UseVisualStyleBackColor = true;
            this.ckbHideCircles.CheckedChanged += new System.EventHandler(this.ckbHideCircles_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(67, 27);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(78, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(122, 27);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(156, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(47, 26);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "Tot";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalHeader
            // 
            this.lblTotalHeader.AutoSize = true;
            this.lblTotalHeader.BackColor = System.Drawing.Color.Orange;
            this.lblTotalHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHeader.Location = new System.Drawing.Point(106, 4);
            this.lblTotalHeader.Name = "lblTotalHeader";
            this.lblTotalHeader.Size = new System.Drawing.Size(45, 18);
            this.lblTotalHeader.TabIndex = 16;
            this.lblTotalHeader.Text = "Total:";
            // 
            // CounterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalHeader);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.ckbHideCircles);
            this.Controls.Add(this.rbnLarge);
            this.Controls.Add(this.rbnMedium);
            this.Controls.Add(this.rbnSmall);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.BtnActivate);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblCountersHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "CounterControl";
            this.Size = new System.Drawing.Size(203, 155);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCountersHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblCount;
        public System.Windows.Forms.Label lblTag;
        public System.Windows.Forms.Button BtnActivate;
        public System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RadioButton rbnSmall;
        private System.Windows.Forms.RadioButton rbnMedium;
        private System.Windows.Forms.RadioButton rbnLarge;
        private System.Windows.Forms.CheckBox ckbHideCircles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalHeader;
    }
}
