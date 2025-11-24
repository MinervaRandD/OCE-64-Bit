namespace FloorMaterialEstimator.Finish_Controls
{
    partial class UCSeamFinishPallet
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
            this.grbSelection = new System.Windows.Forms.GroupBox();
            this.rbnSeamSelection = new System.Windows.Forms.RadioButton();
            this.rbnAreaSelection = new System.Windows.Forms.RadioButton();
            this.pnlLineFinish = new System.Windows.Forms.Panel();
            this.grbSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSeamList
            // 
            this.pnlSeamList.AutoScroll = true;
            this.pnlSeamList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSeamList.Location = new System.Drawing.Point(25, 18);
            this.pnlSeamList.Name = "pnlSeamList";
            this.pnlSeamList.Size = new System.Drawing.Size(126, 543);
            this.pnlSeamList.TabIndex = 1;
            // 
            // pnlAreaList
            // 
            this.pnlAreaList.AutoScroll = true;
            this.pnlAreaList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAreaList.Location = new System.Drawing.Point(180, 18);
            this.pnlAreaList.Name = "pnlAreaList";
            this.pnlAreaList.Size = new System.Drawing.Size(126, 543);
            this.pnlAreaList.TabIndex = 2;
            // 
            // grbSelection
            // 
            this.grbSelection.Controls.Add(this.rbnSeamSelection);
            this.grbSelection.Controls.Add(this.rbnAreaSelection);
            this.grbSelection.Location = new System.Drawing.Point(25, 578);
            this.grbSelection.Name = "grbSelection";
            this.grbSelection.Size = new System.Drawing.Size(235, 47);
            this.grbSelection.TabIndex = 3;
            this.grbSelection.TabStop = false;
            // 
            // rbnSeamSelection
            // 
            this.rbnSeamSelection.AutoSize = true;
            this.rbnSeamSelection.Location = new System.Drawing.Point(130, 19);
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
            this.rbnAreaSelection.Location = new System.Drawing.Point(34, 19);
            this.rbnAreaSelection.Name = "rbnAreaSelection";
            this.rbnAreaSelection.Size = new System.Drawing.Size(47, 17);
            this.rbnAreaSelection.TabIndex = 0;
            this.rbnAreaSelection.TabStop = true;
            this.rbnAreaSelection.Text = "Area";
            this.rbnAreaSelection.UseVisualStyleBackColor = true;
            // 
            // pnlLineFinish
            // 
            this.pnlLineFinish.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLineFinish.Location = new System.Drawing.Point(40, 652);
            this.pnlLineFinish.Name = "pnlLineFinish";
            this.pnlLineFinish.Size = new System.Drawing.Size(220, 104);
            this.pnlLineFinish.TabIndex = 4;
            // 
            // UCSeamFinishPallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlLineFinish);
            this.Controls.Add(this.grbSelection);
            this.Controls.Add(this.pnlAreaList);
            this.Controls.Add(this.pnlSeamList);
            this.Name = "UCSeamFinishPallet";
            this.Size = new System.Drawing.Size(359, 795);
            this.grbSelection.ResumeLayout(false);
            this.grbSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSeamList;
        private System.Windows.Forms.Panel pnlAreaList;
        private System.Windows.Forms.GroupBox grbSelection;
        private System.Windows.Forms.RadioButton rbnSeamSelection;
        private System.Windows.Forms.RadioButton rbnAreaSelection;
        private System.Windows.Forms.Panel pnlLineFinish;
    }
}
