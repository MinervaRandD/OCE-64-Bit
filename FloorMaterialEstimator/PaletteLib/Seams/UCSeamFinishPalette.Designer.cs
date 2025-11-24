namespace PaletteLib
{
    partial class UCSeamFinishPalette
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
            this.pnlSeamList = new System.Windows.Forms.Panel();
            this.pnlAreaList = new System.Windows.Forms.Panel();
            this.pnlLineFinish = new System.Windows.Forms.Panel();
            this.grbSelection = new System.Windows.Forms.GroupBox();
            this.rbnSeamSelection = new System.Windows.Forms.RadioButton();
            this.rbnAreaSelection = new System.Windows.Forms.RadioButton();
            this.pnlLineFinish.SuspendLayout();
            this.grbSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSeamList
            // 
            this.pnlSeamList.AutoScroll = true;
            this.pnlSeamList.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSeamList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSeamList.Location = new System.Drawing.Point(1, 0);
            this.pnlSeamList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSeamList.Name = "pnlSeamList";
            this.pnlSeamList.Size = new System.Drawing.Size(73, 481);
            this.pnlSeamList.TabIndex = 1;
            // 
            // pnlAreaList
            // 
            this.pnlAreaList.AutoScroll = true;
            this.pnlAreaList.BackColor = System.Drawing.SystemColors.Window;
            this.pnlAreaList.Location = new System.Drawing.Point(82, 0);
            this.pnlAreaList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAreaList.Name = "pnlAreaList";
            this.pnlAreaList.Size = new System.Drawing.Size(73, 481);
            this.pnlAreaList.TabIndex = 2;
            // 
            // pnlLineFinish
            // 
            this.pnlLineFinish.Controls.Add(this.grbSelection);
            this.pnlLineFinish.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineFinish.Location = new System.Drawing.Point(0, 481);
            this.pnlLineFinish.Name = "pnlLineFinish";
            this.pnlLineFinish.Size = new System.Drawing.Size(218, 156);
            this.pnlLineFinish.TabIndex = 4;
            // 
            // grbSelection
            // 
            this.grbSelection.Controls.Add(this.rbnSeamSelection);
            this.grbSelection.Controls.Add(this.rbnAreaSelection);
            this.grbSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbSelection.Location = new System.Drawing.Point(0, 0);
            this.grbSelection.Name = "grbSelection";
            this.grbSelection.Size = new System.Drawing.Size(218, 48);
            this.grbSelection.TabIndex = 4;
            this.grbSelection.TabStop = false;
            // 
            // rbnSeamSelection
            // 
            this.rbnSeamSelection.AutoSize = true;
            this.rbnSeamSelection.Location = new System.Drawing.Point(116, 19);
            this.rbnSeamSelection.Name = "rbnSeamSelection";
            this.rbnSeamSelection.Size = new System.Drawing.Size(57, 17);
            this.rbnSeamSelection.TabIndex = 1;
            this.rbnSeamSelection.Text = "Seams";
            this.rbnSeamSelection.UseVisualStyleBackColor = true;
            // 
            // rbnAreaSelection
            // 
            this.rbnAreaSelection.AutoSize = true;
            this.rbnAreaSelection.Checked = true;
            this.rbnAreaSelection.Location = new System.Drawing.Point(20, 19);
            this.rbnAreaSelection.Name = "rbnAreaSelection";
            this.rbnAreaSelection.Size = new System.Drawing.Size(47, 17);
            this.rbnAreaSelection.TabIndex = 0;
            this.rbnAreaSelection.TabStop = true;
            this.rbnAreaSelection.Text = "Area";
            this.rbnAreaSelection.UseVisualStyleBackColor = true;
            // 
            // UCSeamFinishPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLineFinish);
            this.Controls.Add(this.pnlAreaList);
            this.Controls.Add(this.pnlSeamList);
            this.Name = "UCSeamFinishPalette";
            this.Size = new System.Drawing.Size(218, 637);
            this.pnlLineFinish.ResumeLayout(false);
            this.grbSelection.ResumeLayout(false);
            this.grbSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSeamList;
        private System.Windows.Forms.Panel pnlAreaList;
        private System.Windows.Forms.Panel pnlLineFinish;
        private System.Windows.Forms.GroupBox grbSelection;
        private System.Windows.Forms.RadioButton rbnSeamSelection;
        private System.Windows.Forms.RadioButton rbnAreaSelection;
    }
}
