namespace CombosOld
{
    partial class CombosBaseForm
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
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.grbProcessGroups = new System.Windows.Forms.GroupBox();
            this.flpGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.grbDefineGroups = new System.Windows.Forms.GroupBox();
            this.pnlComboElemHeader = new System.Windows.Forms.Panel();
            this.lblGroupNumber = new System.Windows.Forms.Label();
            this.lblFlipped = new System.Windows.Forms.Label();
            this.lblElementNumber = new System.Windows.Forms.Label();
            this.flpCutChoice = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCombine = new System.Windows.Forms.Button();
            this.btnClearSelections = new System.Windows.Forms.Button();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.pnlGroupsHeader = new System.Windows.Forms.Panel();
            this.lblCombinedGroupNumber = new System.Windows.Forms.Label();
            this.lblGroupElements = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSelection.SuspendLayout();
            this.grbProcessGroups.SuspendLayout();
            this.grbDefineGroups.SuspendLayout();
            this.pnlComboElemHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.pnlGroupsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSelection
            // 
            this.pnlSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelection.Controls.Add(this.grbProcessGroups);
            this.pnlSelection.Controls.Add(this.grbDefineGroups);
            this.pnlSelection.Location = new System.Drawing.Point(12, 12);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(305, 745);
            this.pnlSelection.TabIndex = 9;
            // 
            // grbProcessGroups
            // 
            this.grbProcessGroups.Controls.Add(this.pnlGroupsHeader);
            this.grbProcessGroups.Controls.Add(this.flpGroups);
            this.grbProcessGroups.Location = new System.Drawing.Point(11, 543);
            this.grbProcessGroups.Name = "grbProcessGroups";
            this.grbProcessGroups.Size = new System.Drawing.Size(278, 168);
            this.grbProcessGroups.TabIndex = 8;
            this.grbProcessGroups.TabStop = false;
            this.grbProcessGroups.Text = "Process Groups";
            // 
            // flpGroups
            // 
            this.flpGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpGroups.Location = new System.Drawing.Point(9, 69);
            this.flpGroups.Name = "flpGroups";
            this.flpGroups.Size = new System.Drawing.Size(261, 85);
            this.flpGroups.TabIndex = 7;
            // 
            // grbDefineGroups
            // 
            this.grbDefineGroups.Controls.Add(this.pnlComboElemHeader);
            this.grbDefineGroups.Controls.Add(this.flpCutChoice);
            this.grbDefineGroups.Controls.Add(this.btnCombine);
            this.grbDefineGroups.Controls.Add(this.btnClearSelections);
            this.grbDefineGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDefineGroups.Location = new System.Drawing.Point(11, 13);
            this.grbDefineGroups.Name = "grbDefineGroups";
            this.grbDefineGroups.Size = new System.Drawing.Size(278, 513);
            this.grbDefineGroups.TabIndex = 7;
            this.grbDefineGroups.TabStop = false;
            this.grbDefineGroups.Text = "Define Groups";
            // 
            // pnlComboElemHeader
            // 
            this.pnlComboElemHeader.Controls.Add(this.lblGroupNumber);
            this.pnlComboElemHeader.Controls.Add(this.lblFlipped);
            this.pnlComboElemHeader.Controls.Add(this.lblElementNumber);
            this.pnlComboElemHeader.Location = new System.Drawing.Point(129, 23);
            this.pnlComboElemHeader.Name = "pnlComboElemHeader";
            this.pnlComboElemHeader.Size = new System.Drawing.Size(120, 26);
            this.pnlComboElemHeader.TabIndex = 10;
            // 
            // lblGroupNumber
            // 
            this.lblGroupNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupNumber.Location = new System.Drawing.Point(35, 1);
            this.lblGroupNumber.Name = "lblGroupNumber";
            this.lblGroupNumber.Size = new System.Drawing.Size(49, 24);
            this.lblGroupNumber.TabIndex = 6;
            this.lblGroupNumber.Text = "Group";
            this.lblGroupNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFlipped
            // 
            this.lblFlipped.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFlipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlipped.Location = new System.Drawing.Point(86, 1);
            this.lblFlipped.Name = "lblFlipped";
            this.lblFlipped.Size = new System.Drawing.Size(30, 24);
            this.lblFlipped.TabIndex = 5;
            this.lblFlipped.Text = "Flip";
            this.lblFlipped.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblElementNumber
            // 
            this.lblElementNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblElementNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElementNumber.Location = new System.Drawing.Point(2, 1);
            this.lblElementNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblElementNumber.Name = "lblElementNumber";
            this.lblElementNumber.Size = new System.Drawing.Size(32, 24);
            this.lblElementNumber.TabIndex = 4;
            this.lblElementNumber.Text = "Cut";
            this.lblElementNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpCutChoice
            // 
            this.flpCutChoice.AutoScroll = true;
            this.flpCutChoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCutChoice.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCutChoice.Location = new System.Drawing.Point(129, 46);
            this.flpCutChoice.Margin = new System.Windows.Forms.Padding(0);
            this.flpCutChoice.Name = "flpCutChoice";
            this.flpCutChoice.Size = new System.Drawing.Size(119, 447);
            this.flpCutChoice.TabIndex = 9;
            // 
            // btnCombine
            // 
            this.btnCombine.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCombine.Location = new System.Drawing.Point(17, 90);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(93, 23);
            this.btnCombine.TabIndex = 8;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = false;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // btnClearSelections
            // 
            this.btnClearSelections.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClearSelections.Location = new System.Drawing.Point(17, 49);
            this.btnClearSelections.Name = "btnClearSelections";
            this.btnClearSelections.Size = new System.Drawing.Size(93, 23);
            this.btnClearSelections.TabIndex = 7;
            this.btnClearSelections.Text = "Clear Selections";
            this.btnClearSelections.UseVisualStyleBackColor = false;
            this.btnClearSelections.Click += new System.EventHandler(this.BtnClearSelections_Click);
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(763, 22);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(387, 702);
            this.axDrawingControl.TabIndex = 4;
            // 
            // pnlGroupsHeader
            // 
            this.pnlGroupsHeader.Controls.Add(this.label2);
            this.pnlGroupsHeader.Controls.Add(this.label1);
            this.pnlGroupsHeader.Controls.Add(this.lblGroupElements);
            this.pnlGroupsHeader.Controls.Add(this.lblCombinedGroupNumber);
            this.pnlGroupsHeader.Location = new System.Drawing.Point(9, 20);
            this.pnlGroupsHeader.Name = "pnlGroupsHeader";
            this.pnlGroupsHeader.Size = new System.Drawing.Size(261, 43);
            this.pnlGroupsHeader.TabIndex = 8;
            // 
            // lblCombinedGroupNumber
            // 
            this.lblCombinedGroupNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCombinedGroupNumber.Location = new System.Drawing.Point(4, 4);
            this.lblCombinedGroupNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblCombinedGroupNumber.Name = "lblCombinedGroupNumber";
            this.lblCombinedGroupNumber.Size = new System.Drawing.Size(30, 32);
            this.lblCombinedGroupNumber.TabIndex = 0;
            this.lblCombinedGroupNumber.Text = "Grp\r\nNbr";
            this.lblCombinedGroupNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupElements
            // 
            this.lblGroupElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupElements.Location = new System.Drawing.Point(36, 5);
            this.lblGroupElements.Name = "lblGroupElements";
            this.lblGroupElements.Size = new System.Drawing.Size(126, 32);
            this.lblGroupElements.TabIndex = 1;
            this.lblGroupElements.Text = "Group Elements";
            this.lblGroupElements.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(167, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Show\r\nOptions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(217, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Delete\r\nGroup";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CombosBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 814);
            this.Controls.Add(this.pnlSelection);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "CombosBaseForm";
            this.Text = "Combinations";
            this.pnlSelection.ResumeLayout(false);
            this.grbProcessGroups.ResumeLayout(false);
            this.grbDefineGroups.ResumeLayout(false);
            this.pnlComboElemHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.pnlGroupsHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.GroupBox grbProcessGroups;
        private System.Windows.Forms.FlowLayoutPanel flpGroups;
        private System.Windows.Forms.GroupBox grbDefineGroups;
        private System.Windows.Forms.Panel pnlComboElemHeader;
        public System.Windows.Forms.Label lblGroupNumber;
        public System.Windows.Forms.Label lblFlipped;
        private System.Windows.Forms.Label lblElementNumber;
        private System.Windows.Forms.FlowLayoutPanel flpCutChoice;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.Button btnClearSelections;
        private System.Windows.Forms.Panel pnlGroupsHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGroupElements;
        private System.Windows.Forms.Label lblCombinedGroupNumber;
    }
}