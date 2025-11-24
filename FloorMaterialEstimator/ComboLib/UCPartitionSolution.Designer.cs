namespace FloorMaterialEstimator
{
    partial class UCPartitionSolution
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
            this.lblPartitionDefinition = new System.Windows.Forms.Label();
            this.lblTotalLength = new System.Windows.Forms.Label();
            this.flpPartitionDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblPartitionDefinition
            // 
            this.lblPartitionDefinition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPartitionDefinition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartitionDefinition.Location = new System.Drawing.Point(3, 9);
            this.lblPartitionDefinition.Name = "lblPartitionDefinition";
            this.lblPartitionDefinition.Size = new System.Drawing.Size(95, 23);
            this.lblPartitionDefinition.TabIndex = 0;
            this.lblPartitionDefinition.Text = "1,1,1,1";
            this.lblPartitionDefinition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalLength
            // 
            this.lblTotalLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLength.Location = new System.Drawing.Point(3, 41);
            this.lblTotalLength.Name = "lblTotalLength";
            this.lblTotalLength.Size = new System.Drawing.Size(95, 23);
            this.lblTotalLength.TabIndex = 1;
            this.lblTotalLength.Text = "12 x 75.25";
            this.lblTotalLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpPartitionDisplay
            // 
            this.flpPartitionDisplay.AutoScroll = true;
            this.flpPartitionDisplay.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flpPartitionDisplay.Location = new System.Drawing.Point(107, 0);
            this.flpPartitionDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.flpPartitionDisplay.Name = "flpPartitionDisplay";
            this.flpPartitionDisplay.Size = new System.Drawing.Size(735, 86);
            this.flpPartitionDisplay.TabIndex = 2;
            this.flpPartitionDisplay.WrapContents = false;
            // 
            // UCPartitionSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.flpPartitionDisplay);
            this.Controls.Add(this.lblTotalLength);
            this.Controls.Add(this.lblPartitionDefinition);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCPartitionSolution";
            this.Size = new System.Drawing.Size(847, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPartitionDefinition;
        private System.Windows.Forms.Label lblTotalLength;
        private System.Windows.Forms.FlowLayoutPanel flpPartitionDisplay;
    }
}
