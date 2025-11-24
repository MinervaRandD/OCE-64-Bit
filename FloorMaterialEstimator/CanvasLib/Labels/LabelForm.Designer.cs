
namespace CanvasLib.Labels
{
    partial class LabelForm
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
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpOutline = new System.Windows.Forms.GroupBox();
            this.rdbRectangle = new System.Windows.Forms.RadioButton();
            this.rdbCircle = new System.Windows.Forms.RadioButton();
            this.rdbNone = new System.Windows.Forms.RadioButton();
            this.btnColor = new System.Windows.Forms.Button();
            this.grpDefaults = new System.Windows.Forms.GroupBox();
            this.btnCounterLarge = new System.Windows.Forms.Button();
            this.btnCounterMedium = new System.Windows.Forms.Button();
            this.btnCounterSmall = new System.Windows.Forms.Button();
            this.btnCutsIndex = new System.Windows.Forms.Button();
            this.nmPointSize = new System.Windows.Forms.NumericUpDown();
            this.lblPointSize = new System.Windows.Forms.Label();
            this.grpOutline.SuspendLayout();
            this.grpDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPointSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(44, 16);
            this.txtLabel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(139, 20);
            this.txtLabel.TabIndex = 0;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Label:";
            // 
            // grpOutline
            // 
            this.grpOutline.Controls.Add(this.rdbRectangle);
            this.grpOutline.Controls.Add(this.rdbCircle);
            this.grpOutline.Controls.Add(this.rdbNone);
            this.grpOutline.Location = new System.Drawing.Point(8, 43);
            this.grpOutline.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpOutline.Name = "grpOutline";
            this.grpOutline.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpOutline.Size = new System.Drawing.Size(110, 96);
            this.grpOutline.TabIndex = 3;
            this.grpOutline.TabStop = false;
            this.grpOutline.Text = "Outline";
            // 
            // rdbRectangle
            // 
            this.rdbRectangle.AutoSize = true;
            this.rdbRectangle.Location = new System.Drawing.Point(11, 64);
            this.rdbRectangle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbRectangle.Name = "rdbRectangle";
            this.rdbRectangle.Size = new System.Drawing.Size(74, 17);
            this.rdbRectangle.TabIndex = 2;
            this.rdbRectangle.TabStop = true;
            this.rdbRectangle.Text = "Rectangle";
            this.rdbRectangle.UseVisualStyleBackColor = true;
            this.rdbRectangle.CheckedChanged += new System.EventHandler(this.rdbRectangle_CheckedChanged);
            // 
            // rdbCircle
            // 
            this.rdbCircle.AutoSize = true;
            this.rdbCircle.Location = new System.Drawing.Point(11, 44);
            this.rdbCircle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbCircle.Name = "rdbCircle";
            this.rdbCircle.Size = new System.Drawing.Size(51, 17);
            this.rdbCircle.TabIndex = 1;
            this.rdbCircle.TabStop = true;
            this.rdbCircle.Text = "Circle";
            this.rdbCircle.UseVisualStyleBackColor = true;
            this.rdbCircle.CheckedChanged += new System.EventHandler(this.rdbCircle_CheckedChanged);
            // 
            // rdbNone
            // 
            this.rdbNone.AutoSize = true;
            this.rdbNone.Location = new System.Drawing.Point(11, 25);
            this.rdbNone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdbNone.Name = "rdbNone";
            this.rdbNone.Size = new System.Drawing.Size(51, 17);
            this.rdbNone.TabIndex = 0;
            this.rdbNone.TabStop = true;
            this.rdbNone.Text = "None";
            this.rdbNone.UseVisualStyleBackColor = true;
            this.rdbNone.CheckedChanged += new System.EventHandler(this.rdbNone_CheckedChanged);
            // 
            // btnColor
            // 
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColor.Location = new System.Drawing.Point(211, 14);
            this.btnColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 23);
            this.btnColor.TabIndex = 5;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // grpDefaults
            // 
            this.grpDefaults.Controls.Add(this.btnCounterLarge);
            this.grpDefaults.Controls.Add(this.btnCounterMedium);
            this.grpDefaults.Controls.Add(this.btnCounterSmall);
            this.grpDefaults.Controls.Add(this.btnCutsIndex);
            this.grpDefaults.Location = new System.Drawing.Point(8, 149);
            this.grpDefaults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpDefaults.Name = "grpDefaults";
            this.grpDefaults.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpDefaults.Size = new System.Drawing.Size(125, 131);
            this.grpDefaults.TabIndex = 6;
            this.grpDefaults.TabStop = false;
            this.grpDefaults.Text = "Set Defaults";
            // 
            // btnCounterLarge
            // 
            this.btnCounterLarge.Location = new System.Drawing.Point(11, 94);
            this.btnCounterLarge.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCounterLarge.Name = "btnCounterLarge";
            this.btnCounterLarge.Size = new System.Drawing.Size(99, 22);
            this.btnCounterLarge.TabIndex = 3;
            this.btnCounterLarge.Text = "Counter Large";
            this.btnCounterLarge.UseVisualStyleBackColor = true;
            this.btnCounterLarge.Click += new System.EventHandler(this.btnCounterLarge_Click);
            // 
            // btnCounterMedium
            // 
            this.btnCounterMedium.Location = new System.Drawing.Point(11, 68);
            this.btnCounterMedium.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCounterMedium.Name = "btnCounterMedium";
            this.btnCounterMedium.Size = new System.Drawing.Size(99, 22);
            this.btnCounterMedium.TabIndex = 2;
            this.btnCounterMedium.Text = "Counter Medium";
            this.btnCounterMedium.UseVisualStyleBackColor = true;
            this.btnCounterMedium.Click += new System.EventHandler(this.btnCounterMedium_Click);
            // 
            // btnCounterSmall
            // 
            this.btnCounterSmall.Location = new System.Drawing.Point(11, 42);
            this.btnCounterSmall.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCounterSmall.Name = "btnCounterSmall";
            this.btnCounterSmall.Size = new System.Drawing.Size(99, 22);
            this.btnCounterSmall.TabIndex = 1;
            this.btnCounterSmall.Text = "Counter Small";
            this.btnCounterSmall.UseVisualStyleBackColor = true;
            this.btnCounterSmall.Click += new System.EventHandler(this.btnCounterSmall_Click);
            // 
            // btnCutsIndex
            // 
            this.btnCutsIndex.Location = new System.Drawing.Point(11, 16);
            this.btnCutsIndex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCutsIndex.Name = "btnCutsIndex";
            this.btnCutsIndex.Size = new System.Drawing.Size(99, 22);
            this.btnCutsIndex.TabIndex = 0;
            this.btnCutsIndex.Text = "Cut Numbers";
            this.btnCutsIndex.UseVisualStyleBackColor = true;
            this.btnCutsIndex.Click += new System.EventHandler(this.btnCutsIndex_Click);
            // 
            // nmPointSize
            // 
            this.nmPointSize.Location = new System.Drawing.Point(146, 87);
            this.nmPointSize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nmPointSize.Name = "nmPointSize";
            this.nmPointSize.Size = new System.Drawing.Size(80, 20);
            this.nmPointSize.TabIndex = 7;
            this.nmPointSize.ValueChanged += new System.EventHandler(this.nmPointSize_ValueChanged);
            // 
            // lblPointSize
            // 
            this.lblPointSize.AutoSize = true;
            this.lblPointSize.Location = new System.Drawing.Point(143, 60);
            this.lblPointSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPointSize.Name = "lblPointSize";
            this.lblPointSize.Size = new System.Drawing.Size(54, 13);
            this.lblPointSize.TabIndex = 8;
            this.lblPointSize.Text = "Point Size";
            // 
            // LabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 287);
            this.Controls.Add(this.lblPointSize);
            this.Controls.Add(this.nmPointSize);
            this.Controls.Add(this.grpDefaults);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.grpOutline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LabelForm";
            this.Text = "Label";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LabelForm_FormClosed);
            this.grpOutline.ResumeLayout(false);
            this.grpOutline.PerformLayout();
            this.grpDefaults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmPointSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpOutline;
        private System.Windows.Forms.RadioButton rdbRectangle;
        private System.Windows.Forms.RadioButton rdbCircle;
        private System.Windows.Forms.RadioButton rdbNone;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.GroupBox grpDefaults;
        private System.Windows.Forms.Button btnCounterLarge;
        private System.Windows.Forms.Button btnCounterMedium;
        private System.Windows.Forms.Button btnCounterSmall;
        private System.Windows.Forms.Button btnCutsIndex;
        private System.Windows.Forms.NumericUpDown nmPointSize;
        private System.Windows.Forms.Label lblPointSize;
    }
}