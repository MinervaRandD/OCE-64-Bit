namespace FloorMaterialEstimator
{
    partial class UCCutUndrsNestingSelectionControl
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
            this.grbSelectUnders = new System.Windows.Forms.GroupBox();
            this.flpUndrChoice = new System.Windows.Forms.FlowLayoutPanel();
            this.grbSelectCut = new System.Windows.Forms.GroupBox();
            this.flpCutChoice = new System.Windows.Forms.FlowLayoutPanel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.pnlSelection.SuspendLayout();
            this.grbSelectUnders.SuspendLayout();
            this.grbSelectCut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSelection
            // 
            this.pnlSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelection.Controls.Add(this.grbSelectUnders);
            this.pnlSelection.Controls.Add(this.grbSelectCut);
            this.pnlSelection.Location = new System.Drawing.Point(12, 12);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(254, 745);
            this.pnlSelection.TabIndex = 9;
            // 
            // grbSelectUnders
            // 
            this.grbSelectUnders.Controls.Add(this.flpUndrChoice);
            this.grbSelectUnders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSelectUnders.Location = new System.Drawing.Point(129, 13);
            this.grbSelectUnders.Name = "grbSelectUnders";
            this.grbSelectUnders.Size = new System.Drawing.Size(103, 682);
            this.grbSelectUnders.TabIndex = 8;
            this.grbSelectUnders.TabStop = false;
            this.grbSelectUnders.Text = "Select Unders";
            // 
            // flpUndrChoice
            // 
            this.flpUndrChoice.AutoScroll = true;
            this.flpUndrChoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpUndrChoice.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpUndrChoice.Location = new System.Drawing.Point(20, 35);
            this.flpUndrChoice.Margin = new System.Windows.Forms.Padding(0);
            this.flpUndrChoice.Name = "flpUndrChoice";
            this.flpUndrChoice.Size = new System.Drawing.Size(62, 630);
            this.flpUndrChoice.TabIndex = 9;
            // 
            // grbSelectCut
            // 
            this.grbSelectCut.Controls.Add(this.flpCutChoice);
            this.grbSelectCut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSelectCut.Location = new System.Drawing.Point(11, 13);
            this.grbSelectCut.Name = "grbSelectCut";
            this.grbSelectCut.Size = new System.Drawing.Size(103, 682);
            this.grbSelectCut.TabIndex = 7;
            this.grbSelectCut.TabStop = false;
            this.grbSelectCut.Text = "Select Cut";
            // 
            // flpCutChoice
            // 
            this.flpCutChoice.AutoScroll = true;
            this.flpCutChoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCutChoice.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCutChoice.Location = new System.Drawing.Point(20, 35);
            this.flpCutChoice.Margin = new System.Windows.Forms.Padding(0);
            this.flpCutChoice.Name = "flpCutChoice";
            this.flpCutChoice.Size = new System.Drawing.Size(62, 630);
            this.flpCutChoice.TabIndex = 9;
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(298, 13);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(387, 702);
            this.axDrawingControl.TabIndex = 4;
            // 
            // UCCutOversNestingSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSelection);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "UCCutOversNestingSelectionControl";
            this.Size = new System.Drawing.Size(1207, 814);
            this.pnlSelection.ResumeLayout(false);
            this.grbSelectUnders.ResumeLayout(false);
            this.grbSelectCut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.GroupBox grbSelectCut;
        private System.Windows.Forms.FlowLayoutPanel flpCutChoice;
        private System.Windows.Forms.GroupBox grbSelectUnders;
        private System.Windows.Forms.FlowLayoutPanel flpUndrChoice;
    }
}