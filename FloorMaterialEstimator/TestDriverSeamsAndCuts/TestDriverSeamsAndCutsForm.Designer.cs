namespace TestDriverRolloutOversAndUnders
{
    partial class TestSeamsAndCutsForm
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
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.pnlTestSetup = new System.Windows.Forms.Panel();
            this.ckbDrawUnderages = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbBaseLine = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTestNumber = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDrawShape = new System.Windows.Forms.Button();
            this.txbRollWidth = new System.Windows.Forms.TextBox();
            this.btnDoCuts = new System.Windows.Forms.Button();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvUnderages = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvOverages = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvCuts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.pnlTestSetup.SuspendLayout();
            this.pnlResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuts)).BeginInit();
            this.SuspendLayout();
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(237, 40);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(572, 613);
            this.axDrawingControl.TabIndex = 6;
            // 
            // pnlTestSetup
            // 
            this.pnlTestSetup.Controls.Add(this.ckbDrawUnderages);
            this.pnlTestSetup.Controls.Add(this.label2);
            this.pnlTestSetup.Controls.Add(this.txbBaseLine);
            this.pnlTestSetup.Controls.Add(this.label3);
            this.pnlTestSetup.Controls.Add(this.cmbTestNumber);
            this.pnlTestSetup.Controls.Add(this.label1);
            this.pnlTestSetup.Controls.Add(this.btnDrawShape);
            this.pnlTestSetup.Controls.Add(this.txbRollWidth);
            this.pnlTestSetup.Controls.Add(this.btnDoCuts);
            this.pnlTestSetup.Location = new System.Drawing.Point(29, 40);
            this.pnlTestSetup.Name = "pnlTestSetup";
            this.pnlTestSetup.Size = new System.Drawing.Size(164, 613);
            this.pnlTestSetup.TabIndex = 7;
            // 
            // ckbDrawUnderages
            // 
            this.ckbDrawUnderages.AutoSize = true;
            this.ckbDrawUnderages.Location = new System.Drawing.Point(21, 298);
            this.ckbDrawUnderages.Name = "ckbDrawUnderages";
            this.ckbDrawUnderages.Size = new System.Drawing.Size(108, 17);
            this.ckbDrawUnderages.TabIndex = 7;
            this.ckbDrawUnderages.Text = "Show Underages";
            this.ckbDrawUnderages.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Base Line";
            // 
            // txbBaseLine
            // 
            this.txbBaseLine.Location = new System.Drawing.Point(77, 88);
            this.txbBaseLine.Name = "txbBaseLine";
            this.txbBaseLine.Size = new System.Drawing.Size(52, 20);
            this.txbBaseLine.TabIndex = 5;
            this.txbBaseLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Test Number";
            // 
            // cmbTestNumber
            // 
            this.cmbTestNumber.FormattingEnabled = true;
            this.cmbTestNumber.Location = new System.Drawing.Point(77, 47);
            this.cmbTestNumber.Name = "cmbTestNumber";
            this.cmbTestNumber.Size = new System.Drawing.Size(52, 21);
            this.cmbTestNumber.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Roll Width";
            // 
            // btnDrawShape
            // 
            this.btnDrawShape.Location = new System.Drawing.Point(38, 187);
            this.btnDrawShape.Name = "btnDrawShape";
            this.btnDrawShape.Size = new System.Drawing.Size(75, 23);
            this.btnDrawShape.TabIndex = 3;
            this.btnDrawShape.Text = "Draw Shape";
            this.btnDrawShape.UseVisualStyleBackColor = true;
            // 
            // txbRollWidth
            // 
            this.txbRollWidth.Location = new System.Drawing.Point(77, 133);
            this.txbRollWidth.Name = "txbRollWidth";
            this.txbRollWidth.Size = new System.Drawing.Size(52, 20);
            this.txbRollWidth.TabIndex = 1;
            this.txbRollWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDoCuts
            // 
            this.btnDoCuts.Location = new System.Drawing.Point(38, 237);
            this.btnDoCuts.Name = "btnDoCuts";
            this.btnDoCuts.Size = new System.Drawing.Size(75, 23);
            this.btnDoCuts.TabIndex = 0;
            this.btnDoCuts.Text = "Do Cuts";
            this.btnDoCuts.UseVisualStyleBackColor = true;
            this.btnDoCuts.Click += new System.EventHandler(this.btnDoCuts_Click);
            // 
            // pnlResults
            // 
            this.pnlResults.Controls.Add(this.dgvCuts);
            this.pnlResults.Controls.Add(this.label6);
            this.pnlResults.Controls.Add(this.label5);
            this.pnlResults.Controls.Add(this.dgvUnderages);
            this.pnlResults.Controls.Add(this.label4);
            this.pnlResults.Controls.Add(this.dgvOverages);
            this.pnlResults.Location = new System.Drawing.Point(816, 40);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(312, 613);
            this.pnlResults.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 421);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Underages";
            // 
            // dgvUnderages
            // 
            this.dgvUnderages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnderages.Location = new System.Drawing.Point(28, 443);
            this.dgvUnderages.Name = "dgvUnderages";
            this.dgvUnderages.Size = new System.Drawing.Size(256, 160);
            this.dgvUnderages.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cuts";
            // 
            // dgvOverages
            // 
            this.dgvOverages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOverages.Location = new System.Drawing.Point(28, 246);
            this.dgvOverages.Name = "dgvOverages";
            this.dgvOverages.Size = new System.Drawing.Size(256, 160);
            this.dgvOverages.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Overages";
            // 
            // dgvCuts
            // 
            this.dgvCuts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuts.Location = new System.Drawing.Point(28, 37);
            this.dgvCuts.Name = "dgvCuts";
            this.dgvCuts.Size = new System.Drawing.Size(256, 160);
            this.dgvCuts.TabIndex = 5;
            // 
            // TestSeamsAndCutsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 701);
            this.Controls.Add(this.pnlResults);
            this.Controls.Add(this.pnlTestSetup);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "TestSeamsAndCutsForm";
            this.Text = "Seams And Cuts";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.pnlTestSetup.ResumeLayout(false);
            this.pnlTestSetup.PerformLayout();
            this.pnlResults.ResumeLayout(false);
            this.pnlResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Panel pnlTestSetup;
        private System.Windows.Forms.Button btnDoCuts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTestNumber;
        private System.Windows.Forms.TextBox txbBaseLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDrawShape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbRollWidth;
        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvOverages;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvUnderages;
        private System.Windows.Forms.CheckBox ckbDrawUnderages;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvCuts;
    }
}

