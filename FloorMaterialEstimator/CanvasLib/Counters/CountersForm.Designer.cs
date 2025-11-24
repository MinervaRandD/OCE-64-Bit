namespace CanvasLib.Counters
{
    partial class wnfCounters
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
            this.pnlCounters = new System.Windows.Forms.Panel();
            this.grbButtons = new System.Windows.Forms.GroupBox();
            this.btnSaveAsDefault = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnShowNone = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnClearAllCounts = new System.Windows.Forms.Button();
            this.btnClearSelectedCount = new System.Windows.Forms.Button();
            this.counterRowHeader1 = new CanvasLib.Counters.CounterRowHeader();
            this.grbButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCounters
            // 
            this.pnlCounters.AutoScroll = true;
            this.pnlCounters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCounters.Location = new System.Drawing.Point(23, 50);
            this.pnlCounters.Name = "pnlCounters";
            this.pnlCounters.Size = new System.Drawing.Size(580, 359);
            this.pnlCounters.TabIndex = 0;
            // 
            // grbButtons
            // 
            this.grbButtons.Controls.Add(this.btnSaveAsDefault);
            this.grbButtons.Controls.Add(this.btnUpdate);
            this.grbButtons.Controls.Add(this.btnShowNone);
            this.grbButtons.Controls.Add(this.btnShowAll);
            this.grbButtons.Controls.Add(this.btnClearAllCounts);
            this.grbButtons.Controls.Add(this.btnClearSelectedCount);
            this.grbButtons.Location = new System.Drawing.Point(23, 432);
            this.grbButtons.Name = "grbButtons";
            this.grbButtons.Size = new System.Drawing.Size(438, 85);
            this.grbButtons.TabIndex = 2;
            this.grbButtons.TabStop = false;
            // 
            // btnSaveAsDefault
            // 
            this.btnSaveAsDefault.Location = new System.Drawing.Point(163, 48);
            this.btnSaveAsDefault.Name = "btnSaveAsDefault";
            this.btnSaveAsDefault.Size = new System.Drawing.Size(99, 23);
            this.btnSaveAsDefault.TabIndex = 5;
            this.btnSaveAsDefault.Text = "Save As Default";
            this.btnSaveAsDefault.UseVisualStyleBackColor = true;
            this.btnSaveAsDefault.Click += new System.EventHandler(this.btnSaveAsDefault_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(173, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnShowNone
            // 
            this.btnShowNone.Location = new System.Drawing.Point(283, 48);
            this.btnShowNone.Name = "btnShowNone";
            this.btnShowNone.Size = new System.Drawing.Size(121, 23);
            this.btnShowNone.TabIndex = 3;
            this.btnShowNone.Text = "Show None";
            this.btnShowNone.UseVisualStyleBackColor = true;
            this.btnShowNone.Click += new System.EventHandler(this.btnShowNone_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(283, 19);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(121, 23);
            this.btnShowAll.TabIndex = 2;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnClearAllCounts
            // 
            this.btnClearAllCounts.Location = new System.Drawing.Point(11, 48);
            this.btnClearAllCounts.Name = "btnClearAllCounts";
            this.btnClearAllCounts.Size = new System.Drawing.Size(138, 23);
            this.btnClearAllCounts.TabIndex = 1;
            this.btnClearAllCounts.Text = "Clear All Counts";
            this.btnClearAllCounts.UseVisualStyleBackColor = true;
            this.btnClearAllCounts.Click += new System.EventHandler(this.btnClearAllCounts_Click);
            // 
            // btnClearSelectedCount
            // 
            this.btnClearSelectedCount.Location = new System.Drawing.Point(11, 19);
            this.btnClearSelectedCount.Name = "btnClearSelectedCount";
            this.btnClearSelectedCount.Size = new System.Drawing.Size(138, 23);
            this.btnClearSelectedCount.TabIndex = 0;
            this.btnClearSelectedCount.Text = "Clear Selected Count";
            this.btnClearSelectedCount.UseVisualStyleBackColor = true;
            this.btnClearSelectedCount.Click += new System.EventHandler(this.btnClearSelectedCount_Click);
            // 
            // counterRowHeader1
            // 
            this.counterRowHeader1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.counterRowHeader1.Location = new System.Drawing.Point(25, 10);
            this.counterRowHeader1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.counterRowHeader1.Name = "counterRowHeader1";
            this.counterRowHeader1.Size = new System.Drawing.Size(578, 36);
            this.counterRowHeader1.TabIndex = 1;
            // 
            // wnfCounters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 535);
            this.Controls.Add(this.grbButtons);
            this.Controls.Add(this.counterRowHeader1);
            this.Controls.Add(this.pnlCounters);
            this.Name = "wnfCounters";
            this.Text = "Counters";
            this.grbButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCounters;
        private CounterRowHeader counterRowHeader1;
        private System.Windows.Forms.GroupBox grbButtons;
        private System.Windows.Forms.Button btnShowNone;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnClearAllCounts;
        private System.Windows.Forms.Button btnClearSelectedCount;
        private System.Windows.Forms.Button btnSaveAsDefault;
        private System.Windows.Forms.Button btnUpdate;
    }
}