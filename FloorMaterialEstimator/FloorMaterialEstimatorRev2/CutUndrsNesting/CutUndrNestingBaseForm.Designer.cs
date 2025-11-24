
namespace FloorMaterialEstimator
{
    partial class CutUndrsNestingBaseForm
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
            this.tbcCutOversNesting = new System.Windows.Forms.TabControl();
            this.tbpSelection = new System.Windows.Forms.TabPage();
            this.ucCutOversNestingSelectionControl = new FloorMaterialEstimator.UCCutUndrsNestingSelectionControl();
            this.tbsNesting = new System.Windows.Forms.TabPage();
            this.ucCutUndrsNestingNestControl = new FloorMaterialEstimator.UCCutUndrsNestingNestControl();
            this.tbcCutOversNesting.SuspendLayout();
            this.tbpSelection.SuspendLayout();
            this.tbsNesting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcCutOversNesting
            // 
            this.tbcCutOversNesting.Controls.Add(this.tbpSelection);
            this.tbcCutOversNesting.Controls.Add(this.tbsNesting);
            this.tbcCutOversNesting.Location = new System.Drawing.Point(3, 12);
            this.tbcCutOversNesting.Name = "tbcCutOversNesting";
            this.tbcCutOversNesting.SelectedIndex = 0;
            this.tbcCutOversNesting.Size = new System.Drawing.Size(1101, 788);
            this.tbcCutOversNesting.TabIndex = 0;
            // 
            // tbpSelection
            // 
            this.tbpSelection.Controls.Add(this.ucCutOversNestingSelectionControl);
            this.tbpSelection.Location = new System.Drawing.Point(4, 22);
            this.tbpSelection.Name = "tbpSelection";
            this.tbpSelection.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSelection.Size = new System.Drawing.Size(1093, 762);
            this.tbpSelection.TabIndex = 0;
            this.tbpSelection.Text = "Selection";
            this.tbpSelection.UseVisualStyleBackColor = true;
            // 
            // ucCutOversNestingSelectionControl
            // 
            this.ucCutOversNestingSelectionControl.Location = new System.Drawing.Point(6, 16);
            this.ucCutOversNestingSelectionControl.Name = "ucCutOversNestingSelectionControl";
            this.ucCutOversNestingSelectionControl.page = null;
            this.ucCutOversNestingSelectionControl.Size = new System.Drawing.Size(1084, 750);
            this.ucCutOversNestingSelectionControl.TabIndex = 0;
            this.ucCutOversNestingSelectionControl.VsoDocument = null;
            this.ucCutOversNestingSelectionControl.VsoWindow = null;
            this.ucCutOversNestingSelectionControl.window = null;
            // 
            // tbsNesting
            // 
            this.tbsNesting.Controls.Add(this.ucCutUndrsNestingNestControl);
            this.tbsNesting.Location = new System.Drawing.Point(4, 22);
            this.tbsNesting.Name = "tbsNesting";
            this.tbsNesting.Padding = new System.Windows.Forms.Padding(3);
            this.tbsNesting.Size = new System.Drawing.Size(1093, 762);
            this.tbsNesting.TabIndex = 1;
            this.tbsNesting.Text = "Nesting";
            this.tbsNesting.UseVisualStyleBackColor = true;
            // 
            // ucCutOversNestingNestControl
            // 
            this.ucCutUndrsNestingNestControl.Location = new System.Drawing.Point(6, 6);
            this.ucCutUndrsNestingNestControl.Name = "ucCutOversNestingNestControl";
            this.ucCutUndrsNestingNestControl.page = null;
            this.ucCutUndrsNestingNestControl.Size = new System.Drawing.Size(1207, 814);
            this.ucCutUndrsNestingNestControl.TabIndex = 0;
            this.ucCutUndrsNestingNestControl.VsoDocument = null;
            this.ucCutUndrsNestingNestControl.VsoWindow = null;
            this.ucCutUndrsNestingNestControl.window = null;
            // 
            // CutOversNestingBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 803);
            this.Controls.Add(this.tbcCutOversNesting);
            this.Name = "CutOversNestingBaseForm";
            this.Text = "Cut Overs Nesting";
            this.tbcCutOversNesting.ResumeLayout(false);
            this.tbpSelection.ResumeLayout(false);
            this.tbsNesting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcCutOversNesting;
        private System.Windows.Forms.TabPage tbpSelection;
        private System.Windows.Forms.TabPage tbsNesting;
        private UCCutUndrsNestingSelectionControl ucCutOversNestingSelectionControl;
        private UCCutUndrsNestingNestControl ucCutUndrsNestingNestControl;
    }
}