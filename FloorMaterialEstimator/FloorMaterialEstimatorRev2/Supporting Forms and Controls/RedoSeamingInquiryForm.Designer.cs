
namespace FloorMaterialEstimator
{
    partial class RedoSeamingInquiryForm
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
            this.btnRedoAllSeamsForCurrentFinish = new System.Windows.Forms.Button();
            this.btnClearAllSeamsForTheCurrentFinish = new System.Windows.Forms.Button();
            this.btnClearAllSeamsForAllFinishes = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ckbIncludeManualSeams = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnRedoAllSeamsForCurrentFinish
            // 
            this.btnRedoAllSeamsForCurrentFinish.Location = new System.Drawing.Point(24, 24);
            this.btnRedoAllSeamsForCurrentFinish.Name = "btnRedoAllSeamsForCurrentFinish";
            this.btnRedoAllSeamsForCurrentFinish.Size = new System.Drawing.Size(267, 23);
            this.btnRedoAllSeamsForCurrentFinish.TabIndex = 0;
            this.btnRedoAllSeamsForCurrentFinish.Text = "Redo all seams for the current finish type";
            this.btnRedoAllSeamsForCurrentFinish.UseVisualStyleBackColor = true;
            this.btnRedoAllSeamsForCurrentFinish.Click += new System.EventHandler(this.btnRedoAllSeamsForCurrentFinish_Click);
            // 
            // btnClearAllSeamsForTheCurrentFinish
            // 
            this.btnClearAllSeamsForTheCurrentFinish.Location = new System.Drawing.Point(24, 81);
            this.btnClearAllSeamsForTheCurrentFinish.Name = "btnClearAllSeamsForTheCurrentFinish";
            this.btnClearAllSeamsForTheCurrentFinish.Size = new System.Drawing.Size(267, 23);
            this.btnClearAllSeamsForTheCurrentFinish.TabIndex = 1;
            this.btnClearAllSeamsForTheCurrentFinish.Text = "Clear all seams for the current finish type";
            this.btnClearAllSeamsForTheCurrentFinish.UseVisualStyleBackColor = true;
            this.btnClearAllSeamsForTheCurrentFinish.Click += new System.EventHandler(this.btnClearAllSeamsForTheCurrentFinish_Click);
            // 
            // btnClearAllSeamsForAllFinishes
            // 
            this.btnClearAllSeamsForAllFinishes.Location = new System.Drawing.Point(24, 138);
            this.btnClearAllSeamsForAllFinishes.Name = "btnClearAllSeamsForAllFinishes";
            this.btnClearAllSeamsForAllFinishes.Size = new System.Drawing.Size(267, 23);
            this.btnClearAllSeamsForAllFinishes.TabIndex = 2;
            this.btnClearAllSeamsForAllFinishes.Text = "Clear all seams for all finish types";
            this.btnClearAllSeamsForAllFinishes.UseVisualStyleBackColor = true;
            this.btnClearAllSeamsForAllFinishes.Click += new System.EventHandler(this.btnClearAllSeamsForAllFinishes_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(120, 269);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ckbIncludeManualSeams
            // 
            this.ckbIncludeManualSeams.AutoSize = true;
            this.ckbIncludeManualSeams.Checked = true;
            this.ckbIncludeManualSeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbIncludeManualSeams.Location = new System.Drawing.Point(80, 192);
            this.ckbIncludeManualSeams.Name = "ckbIncludeManualSeams";
            this.ckbIncludeManualSeams.Size = new System.Drawing.Size(134, 17);
            this.ckbIncludeManualSeams.TabIndex = 4;
            this.ckbIncludeManualSeams.Text = "Include Manual Seams";
            this.ckbIncludeManualSeams.UseVisualStyleBackColor = true;
            // 
            // RedoSeamingInquiryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 330);
            this.Controls.Add(this.ckbIncludeManualSeams);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClearAllSeamsForAllFinishes);
            this.Controls.Add(this.btnClearAllSeamsForTheCurrentFinish);
            this.Controls.Add(this.btnRedoAllSeamsForCurrentFinish);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RedoSeamingInquiryForm";
            this.Text = "Redo Seaming Inquiry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRedoAllSeamsForCurrentFinish;
        private System.Windows.Forms.Button btnClearAllSeamsForTheCurrentFinish;
        private System.Windows.Forms.Button btnClearAllSeamsForAllFinishes;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox ckbIncludeManualSeams;
    }
}