
namespace CanvasLib.Legend
{
    partial class AreaLegendNavigationForm
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
            this.btnShowLeft = new System.Windows.Forms.Button();
            this.btnShowRight = new System.Windows.Forms.Button();
            this.ckbLocateOnClick = new System.Windows.Forms.CheckBox();
            this.trbLegendSize = new System.Windows.Forms.TrackBar();
            this.grbLocation = new System.Windows.Forms.GroupBox();
            this.grbLegendDisplay = new System.Windows.Forms.GroupBox();
            this.CkbIncludeNotes = new System.Windows.Forms.CheckBox();
            this.CkbIncludeCounters = new System.Windows.Forms.CheckBox();
            this.CkbIncludeFinishes = new System.Windows.Forms.CheckBox();
            this.CkbShowLegendInLineMode = new System.Windows.Forms.CheckBox();
            this.CkbShowLegendInAreaMode = new System.Windows.Forms.CheckBox();
            this.btnSaveLegendSizeAsDefault = new System.Windows.Forms.Button();
            this.grbNotes = new System.Windows.Forms.GroupBox();
            this.btnUpdateNotes = new System.Windows.Forms.Button();
            this.txbNotes = new System.Windows.Forms.TextBox();
            this.btnShowFilters = new System.Windows.Forms.Button();
            this.grbLegendSize = new System.Windows.Forms.GroupBox();
            this.LblLegendSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trbLegendSize)).BeginInit();
            this.grbLocation.SuspendLayout();
            this.grbLegendDisplay.SuspendLayout();
            this.grbNotes.SuspendLayout();
            this.grbLegendSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowLeft
            // 
            this.btnShowLeft.Location = new System.Drawing.Point(19, 26);
            this.btnShowLeft.Name = "btnShowLeft";
            this.btnShowLeft.Size = new System.Drawing.Size(75, 23);
            this.btnShowLeft.TabIndex = 0;
            this.btnShowLeft.Text = "Show Left";
            this.btnShowLeft.UseVisualStyleBackColor = true;
            this.btnShowLeft.Click += new System.EventHandler(this.btnShowLeft_Click);
            // 
            // btnShowRight
            // 
            this.btnShowRight.Location = new System.Drawing.Point(123, 26);
            this.btnShowRight.Name = "btnShowRight";
            this.btnShowRight.Size = new System.Drawing.Size(75, 23);
            this.btnShowRight.TabIndex = 1;
            this.btnShowRight.Text = "Show Right";
            this.btnShowRight.UseVisualStyleBackColor = true;
            this.btnShowRight.Click += new System.EventHandler(this.btnShowRight_Click);
            // 
            // ckbLocateOnClick
            // 
            this.ckbLocateOnClick.AutoSize = true;
            this.ckbLocateOnClick.Location = new System.Drawing.Point(227, 29);
            this.ckbLocateOnClick.Name = "ckbLocateOnClick";
            this.ckbLocateOnClick.Size = new System.Drawing.Size(125, 17);
            this.ckbLocateOnClick.TabIndex = 2;
            this.ckbLocateOnClick.Text = "Locate To Shift-Click";
            this.ckbLocateOnClick.UseVisualStyleBackColor = true;
            this.ckbLocateOnClick.CheckedChanged += new System.EventHandler(this.ckbLocateOnClick_CheckedChanged);
            // 
            // trbLegendSize
            // 
            this.trbLegendSize.Location = new System.Drawing.Point(42, 22);
            this.trbLegendSize.Maximum = 100;
            this.trbLegendSize.Minimum = 10;
            this.trbLegendSize.Name = "trbLegendSize";
            this.trbLegendSize.Size = new System.Drawing.Size(104, 45);
            this.trbLegendSize.TabIndex = 4;
            this.trbLegendSize.Value = 25;
            this.trbLegendSize.Scroll += new System.EventHandler(this.trbLegendSize_Scroll);
            // 
            // grbLocation
            // 
            this.grbLocation.Controls.Add(this.btnShowLeft);
            this.grbLocation.Controls.Add(this.btnShowRight);
            this.grbLocation.Controls.Add(this.ckbLocateOnClick);
            this.grbLocation.Location = new System.Drawing.Point(19, 145);
            this.grbLocation.Name = "grbLocation";
            this.grbLocation.Size = new System.Drawing.Size(371, 71);
            this.grbLocation.TabIndex = 6;
            this.grbLocation.TabStop = false;
            this.grbLocation.Text = "Legend Location";
            // 
            // grbLegendDisplay
            // 
            this.grbLegendDisplay.Controls.Add(this.CkbIncludeNotes);
            this.grbLegendDisplay.Controls.Add(this.CkbIncludeCounters);
            this.grbLegendDisplay.Controls.Add(this.CkbIncludeFinishes);
            this.grbLegendDisplay.Controls.Add(this.CkbShowLegendInLineMode);
            this.grbLegendDisplay.Controls.Add(this.CkbShowLegendInAreaMode);
            this.grbLegendDisplay.Location = new System.Drawing.Point(20, 42);
            this.grbLegendDisplay.Name = "grbLegendDisplay";
            this.grbLegendDisplay.Size = new System.Drawing.Size(370, 90);
            this.grbLegendDisplay.TabIndex = 7;
            this.grbLegendDisplay.TabStop = false;
            this.grbLegendDisplay.Text = "Legend Display";
            // 
            // CkbIncludeNotes
            // 
            this.CkbIncludeNotes.AutoSize = true;
            this.CkbIncludeNotes.Location = new System.Drawing.Point(251, 58);
            this.CkbIncludeNotes.Name = "CkbIncludeNotes";
            this.CkbIncludeNotes.Size = new System.Drawing.Size(92, 17);
            this.CkbIncludeNotes.TabIndex = 10;
            this.CkbIncludeNotes.Text = "Include Notes";
            this.CkbIncludeNotes.UseVisualStyleBackColor = true;
            // 
            // CkbIncludeCounters
            // 
            this.CkbIncludeCounters.AutoSize = true;
            this.CkbIncludeCounters.Location = new System.Drawing.Point(130, 58);
            this.CkbIncludeCounters.Name = "CkbIncludeCounters";
            this.CkbIncludeCounters.Size = new System.Drawing.Size(106, 17);
            this.CkbIncludeCounters.TabIndex = 9;
            this.CkbIncludeCounters.Text = "Include Counters";
            this.CkbIncludeCounters.UseVisualStyleBackColor = true;
            // 
            // CkbIncludeFinishes
            // 
            this.CkbIncludeFinishes.AutoSize = true;
            this.CkbIncludeFinishes.Location = new System.Drawing.Point(13, 58);
            this.CkbIncludeFinishes.Name = "CkbIncludeFinishes";
            this.CkbIncludeFinishes.Size = new System.Drawing.Size(102, 17);
            this.CkbIncludeFinishes.TabIndex = 8;
            this.CkbIncludeFinishes.Text = "Include Finishes";
            this.CkbIncludeFinishes.UseVisualStyleBackColor = true;
            // 
            // CkbShowLegendInLineMode
            // 
            this.CkbShowLegendInLineMode.AutoSize = true;
            this.CkbShowLegendInLineMode.Location = new System.Drawing.Point(193, 28);
            this.CkbShowLegendInLineMode.Name = "CkbShowLegendInLineMode";
            this.CkbShowLegendInLineMode.Size = new System.Drawing.Size(118, 17);
            this.CkbShowLegendInLineMode.TabIndex = 7;
            this.CkbShowLegendInLineMode.Text = "Show In Line Mode";
            this.CkbShowLegendInLineMode.UseVisualStyleBackColor = true;
            // 
            // CkbShowLegendInAreaMode
            // 
            this.CkbShowLegendInAreaMode.AutoSize = true;
            this.CkbShowLegendInAreaMode.Location = new System.Drawing.Point(42, 28);
            this.CkbShowLegendInAreaMode.Name = "CkbShowLegendInAreaMode";
            this.CkbShowLegendInAreaMode.Size = new System.Drawing.Size(120, 17);
            this.CkbShowLegendInAreaMode.TabIndex = 6;
            this.CkbShowLegendInAreaMode.Text = "Show In Area Mode";
            this.CkbShowLegendInAreaMode.UseVisualStyleBackColor = true;
            // 
            // btnSaveLegendSizeAsDefault
            // 
            this.btnSaveLegendSizeAsDefault.Location = new System.Drawing.Point(260, 17);
            this.btnSaveLegendSizeAsDefault.Name = "btnSaveLegendSizeAsDefault";
            this.btnSaveLegendSizeAsDefault.Size = new System.Drawing.Size(92, 45);
            this.btnSaveLegendSizeAsDefault.TabIndex = 5;
            this.btnSaveLegendSizeAsDefault.Text = "Save Legend Size As Default";
            this.btnSaveLegendSizeAsDefault.UseVisualStyleBackColor = true;
            this.btnSaveLegendSizeAsDefault.Click += new System.EventHandler(this.btnSaveLegendSizeAsDefault_Click);
            // 
            // grbNotes
            // 
            this.grbNotes.Controls.Add(this.btnUpdateNotes);
            this.grbNotes.Controls.Add(this.txbNotes);
            this.grbNotes.Location = new System.Drawing.Point(19, 313);
            this.grbNotes.Name = "grbNotes";
            this.grbNotes.Size = new System.Drawing.Size(371, 100);
            this.grbNotes.TabIndex = 8;
            this.grbNotes.TabStop = false;
            this.grbNotes.Text = "Notes";
            // 
            // btnUpdateNotes
            // 
            this.btnUpdateNotes.Location = new System.Drawing.Point(306, 33);
            this.btnUpdateNotes.Name = "btnUpdateNotes";
            this.btnUpdateNotes.Size = new System.Drawing.Size(57, 39);
            this.btnUpdateNotes.TabIndex = 4;
            this.btnUpdateNotes.Text = "Update Notes";
            this.btnUpdateNotes.UseVisualStyleBackColor = true;
            this.btnUpdateNotes.Click += new System.EventHandler(this.btnUpdateNotes_Click);
            // 
            // txbNotes
            // 
            this.txbNotes.AcceptsReturn = true;
            this.txbNotes.AcceptsTab = true;
            this.txbNotes.Location = new System.Drawing.Point(13, 22);
            this.txbNotes.Multiline = true;
            this.txbNotes.Name = "txbNotes";
            this.txbNotes.Size = new System.Drawing.Size(287, 62);
            this.txbNotes.TabIndex = 0;
            // 
            // btnShowFilters
            // 
            this.btnShowFilters.Location = new System.Drawing.Point(159, 430);
            this.btnShowFilters.Name = "btnShowFilters";
            this.btnShowFilters.Size = new System.Drawing.Size(93, 23);
            this.btnShowFilters.TabIndex = 9;
            this.btnShowFilters.Text = "Show Filters";
            this.btnShowFilters.UseVisualStyleBackColor = true;
            this.btnShowFilters.Click += new System.EventHandler(this.btnShowFilters_Click);
            // 
            // grbLegendSize
            // 
            this.grbLegendSize.Controls.Add(this.LblLegendSize);
            this.grbLegendSize.Controls.Add(this.trbLegendSize);
            this.grbLegendSize.Controls.Add(this.btnSaveLegendSizeAsDefault);
            this.grbLegendSize.Location = new System.Drawing.Point(19, 229);
            this.grbLegendSize.Name = "grbLegendSize";
            this.grbLegendSize.Size = new System.Drawing.Size(371, 71);
            this.grbLegendSize.TabIndex = 10;
            this.grbLegendSize.TabStop = false;
            this.grbLegendSize.Text = "Legend Size";
            // 
            // LblLegendSize
            // 
            this.LblLegendSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblLegendSize.Location = new System.Drawing.Point(169, 33);
            this.LblLegendSize.Name = "LblLegendSize";
            this.LblLegendSize.Size = new System.Drawing.Size(36, 18);
            this.LblLegendSize.TabIndex = 6;
            this.LblLegendSize.Text = "25%";
            this.LblLegendSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AreaLegendNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 467);
            this.Controls.Add(this.grbLegendSize);
            this.Controls.Add(this.btnShowFilters);
            this.Controls.Add(this.grbNotes);
            this.Controls.Add(this.grbLegendDisplay);
            this.Controls.Add(this.grbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AreaLegendNavigationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Area Mode Legend";
            ((System.ComponentModel.ISupportInitialize)(this.trbLegendSize)).EndInit();
            this.grbLocation.ResumeLayout(false);
            this.grbLocation.PerformLayout();
            this.grbLegendDisplay.ResumeLayout(false);
            this.grbLegendDisplay.PerformLayout();
            this.grbNotes.ResumeLayout(false);
            this.grbNotes.PerformLayout();
            this.grbLegendSize.ResumeLayout(false);
            this.grbLegendSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowLeft;
        private System.Windows.Forms.Button btnShowRight;
        private System.Windows.Forms.CheckBox ckbLocateOnClick;
        private System.Windows.Forms.TrackBar trbLegendSize;
        private System.Windows.Forms.GroupBox grbLocation;
        private System.Windows.Forms.GroupBox grbLegendDisplay;
        private System.Windows.Forms.GroupBox grbNotes;
        private System.Windows.Forms.TextBox txbNotes;
        private System.Windows.Forms.Button btnUpdateNotes;
        private System.Windows.Forms.Button btnSaveLegendSizeAsDefault;
        private System.Windows.Forms.Button btnShowFilters;
        private System.Windows.Forms.CheckBox CkbShowLegendInAreaMode;
        private System.Windows.Forms.CheckBox CkbShowLegendInLineMode;
        private System.Windows.Forms.GroupBox grbLegendSize;
        private System.Windows.Forms.CheckBox CkbIncludeNotes;
        private System.Windows.Forms.CheckBox CkbIncludeCounters;
        public System.Windows.Forms.CheckBox CkbIncludeFinishes;
        public System.Windows.Forms.Label LblLegendSize;
    }
}