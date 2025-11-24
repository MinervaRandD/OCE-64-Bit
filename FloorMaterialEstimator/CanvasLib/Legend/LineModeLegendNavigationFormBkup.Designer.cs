
namespace CanvasLib.Legend
{
    partial class LineModeLegendNavigationFormBkup
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
            this.lblLegendSize = new System.Windows.Forms.Label();
            this.grbLocation = new System.Windows.Forms.GroupBox();
            this.grbLegendDisplay = new System.Windows.Forms.GroupBox();
            this.CkbShowLegendInLineMode = new System.Windows.Forms.CheckBox();
            this.CkbShowLegendInAreaMode = new System.Windows.Forms.CheckBox();
            this.btnSaveLegendSizeAsDefault = new System.Windows.Forms.Button();
            this.grbNotes = new System.Windows.Forms.GroupBox();
            this.btnUpdateNotes = new System.Windows.Forms.Button();
            this.txbNotes = new System.Windows.Forms.TextBox();
            this.btnShowFilters = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trbLegendSize)).BeginInit();
            this.grbLocation.SuspendLayout();
            this.grbLegendDisplay.SuspendLayout();
            this.grbNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowLeft
            // 
            this.btnShowLeft.Location = new System.Drawing.Point(19, 32);
            this.btnShowLeft.Name = "btnShowLeft";
            this.btnShowLeft.Size = new System.Drawing.Size(75, 23);
            this.btnShowLeft.TabIndex = 0;
            this.btnShowLeft.Text = "Show Left";
            this.btnShowLeft.UseVisualStyleBackColor = true;
            this.btnShowLeft.Click += new System.EventHandler(this.btnShowLeft_Click);
            // 
            // btnShowRight
            // 
            this.btnShowRight.Location = new System.Drawing.Point(140, 32);
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
            this.ckbLocateOnClick.Checked = true;
            this.ckbLocateOnClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbLocateOnClick.Location = new System.Drawing.Point(254, 36);
            this.ckbLocateOnClick.Name = "ckbLocateOnClick";
            this.ckbLocateOnClick.Size = new System.Drawing.Size(101, 17);
            this.ckbLocateOnClick.TabIndex = 2;
            this.ckbLocateOnClick.Text = "Locate To Click";
            this.ckbLocateOnClick.UseVisualStyleBackColor = true;
            this.ckbLocateOnClick.CheckedChanged += new System.EventHandler(this.ckbLocateOnClick_CheckedChanged);
            // 
            // trbLegendSize
            // 
            this.trbLegendSize.Location = new System.Drawing.Point(239, 15);
            this.trbLegendSize.Maximum = 400;
            this.trbLegendSize.Name = "trbLegendSize";
            this.trbLegendSize.Size = new System.Drawing.Size(104, 45);
            this.trbLegendSize.TabIndex = 4;
            this.trbLegendSize.Value = 20;
            this.trbLegendSize.Scroll += new System.EventHandler(this.trbLegendSize_Scroll);
            // 
            // lblLegendSize
            // 
            this.lblLegendSize.AutoSize = true;
            this.lblLegendSize.Location = new System.Drawing.Point(167, 27);
            this.lblLegendSize.Name = "lblLegendSize";
            this.lblLegendSize.Size = new System.Drawing.Size(66, 13);
            this.lblLegendSize.TabIndex = 5;
            this.lblLegendSize.Text = "Legend Size";
            this.lblLegendSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grbLocation
            // 
            this.grbLocation.Controls.Add(this.btnShowLeft);
            this.grbLocation.Controls.Add(this.btnShowRight);
            this.grbLocation.Controls.Add(this.ckbLocateOnClick);
            this.grbLocation.Location = new System.Drawing.Point(27, 42);
            this.grbLocation.Name = "grbLocation";
            this.grbLocation.Size = new System.Drawing.Size(371, 71);
            this.grbLocation.TabIndex = 6;
            this.grbLocation.TabStop = false;
            this.grbLocation.Text = "Legend Location";
            // 
            // grbLegendDisplay
            // 
            this.grbLegendDisplay.Controls.Add(this.CkbShowLegendInLineMode);
            this.grbLegendDisplay.Controls.Add(this.CkbShowLegendInAreaMode);
            this.grbLegendDisplay.Controls.Add(this.btnSaveLegendSizeAsDefault);
            this.grbLegendDisplay.Controls.Add(this.trbLegendSize);
            this.grbLegendDisplay.Controls.Add(this.lblLegendSize);
            this.grbLegendDisplay.Location = new System.Drawing.Point(27, 125);
            this.grbLegendDisplay.Name = "grbLegendDisplay";
            this.grbLegendDisplay.Size = new System.Drawing.Size(371, 90);
            this.grbLegendDisplay.TabIndex = 7;
            this.grbLegendDisplay.TabStop = false;
            this.grbLegendDisplay.Text = "Legend Display";
            // 
            // CkbShowLegendInLineMode
            // 
            this.CkbShowLegendInLineMode.AutoSize = true;
            this.CkbShowLegendInLineMode.Location = new System.Drawing.Point(35, 53);
            this.CkbShowLegendInLineMode.Name = "CkbShowLegendInLineMode";
            this.CkbShowLegendInLineMode.Size = new System.Drawing.Size(118, 17);
            this.CkbShowLegendInLineMode.TabIndex = 7;
            this.CkbShowLegendInLineMode.Text = "Show In Line Mode";
            this.CkbShowLegendInLineMode.UseVisualStyleBackColor = true;
            // 
            // CkbShowLegendInAreaMode
            // 
            this.CkbShowLegendInAreaMode.AutoSize = true;
            this.CkbShowLegendInAreaMode.Location = new System.Drawing.Point(35, 28);
            this.CkbShowLegendInAreaMode.Name = "CkbShowLegendInAreaMode";
            this.CkbShowLegendInAreaMode.Size = new System.Drawing.Size(120, 17);
            this.CkbShowLegendInAreaMode.TabIndex = 6;
            this.CkbShowLegendInAreaMode.Text = "Show In Area Mode";
            this.CkbShowLegendInAreaMode.UseVisualStyleBackColor = true;
            // 
            // btnSaveLegendSizeAsDefault
            // 
            this.btnSaveLegendSizeAsDefault.Location = new System.Drawing.Point(170, 52);
            this.btnSaveLegendSizeAsDefault.Name = "btnSaveLegendSizeAsDefault";
            this.btnSaveLegendSizeAsDefault.Size = new System.Drawing.Size(160, 23);
            this.btnSaveLegendSizeAsDefault.TabIndex = 5;
            this.btnSaveLegendSizeAsDefault.Text = "Save Legend Size As Default";
            this.btnSaveLegendSizeAsDefault.UseVisualStyleBackColor = true;
            this.btnSaveLegendSizeAsDefault.Click += new System.EventHandler(this.btnSaveLegendSizeAsDefault_Click);
            // 
            // grbNotes
            // 
            this.grbNotes.Controls.Add(this.btnUpdateNotes);
            this.grbNotes.Controls.Add(this.txbNotes);
            this.grbNotes.Location = new System.Drawing.Point(27, 223);
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
            this.btnShowFilters.Location = new System.Drawing.Point(167, 343);
            this.btnShowFilters.Name = "btnShowFilters";
            this.btnShowFilters.Size = new System.Drawing.Size(93, 23);
            this.btnShowFilters.TabIndex = 9;
            this.btnShowFilters.Text = "Show Filters";
            this.btnShowFilters.UseVisualStyleBackColor = true;
            this.btnShowFilters.Click += new System.EventHandler(this.btnShowFilters_Click);
            // 
            // AreaModeLegendNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 382);
            this.Controls.Add(this.btnShowFilters);
            this.Controls.Add(this.grbNotes);
            this.Controls.Add(this.grbLegendDisplay);
            this.Controls.Add(this.grbLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AreaModeLegendNavigationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Area Mode Legend";
            ((System.ComponentModel.ISupportInitialize)(this.trbLegendSize)).EndInit();
            this.grbLocation.ResumeLayout(false);
            this.grbLocation.PerformLayout();
            this.grbLegendDisplay.ResumeLayout(false);
            this.grbLegendDisplay.PerformLayout();
            this.grbNotes.ResumeLayout(false);
            this.grbNotes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowLeft;
        private System.Windows.Forms.Button btnShowRight;
        private System.Windows.Forms.CheckBox ckbLocateOnClick;
        private System.Windows.Forms.TrackBar trbLegendSize;
        private System.Windows.Forms.Label lblLegendSize;
        private System.Windows.Forms.GroupBox grbLocation;
        private System.Windows.Forms.GroupBox grbLegendDisplay;
        private System.Windows.Forms.GroupBox grbNotes;
        private System.Windows.Forms.TextBox txbNotes;
        private System.Windows.Forms.Button btnUpdateNotes;
        private System.Windows.Forms.Button btnSaveLegendSizeAsDefault;
        private System.Windows.Forms.Button btnShowFilters;
        private System.Windows.Forms.CheckBox CkbShowLegendInAreaMode;
        private System.Windows.Forms.CheckBox CkbShowLegendInLineMode;
    }
}