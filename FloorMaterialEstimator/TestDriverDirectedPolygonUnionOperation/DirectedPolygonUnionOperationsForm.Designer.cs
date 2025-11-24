namespace TestDriverDirectedPolygonUnionOperations
{
    partial class DirectedPolygonUnionOperationsForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.dgvUnderages = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvOverages = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.sssScrollTest = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTestCase2 = new System.Windows.Forms.Button();
            this.btnTestCase1 = new System.Windows.Forms.Button();
            this.btnTestCase3 = new System.Windows.Forms.Button();
            this.btnTestCase4 = new System.Windows.Forms.Button();
            this.btnTestCase5 = new System.Windows.Forms.Button();
            this.btnTestCase6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).BeginInit();
            this.sssScrollTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // dgvUnderages
            // 
            this.dgvUnderages.Location = new System.Drawing.Point(0, 0);
            this.dgvUnderages.Name = "dgvUnderages";
            this.dgvUnderages.Size = new System.Drawing.Size(240, 150);
            this.dgvUnderages.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 0;
            // 
            // dgvOverages
            // 
            this.dgvOverages.Location = new System.Drawing.Point(0, 0);
            this.dgvOverages.Name = "dgvOverages";
            this.dgvOverages.Size = new System.Drawing.Size(240, 150);
            this.dgvOverages.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // sssScrollTest
            // 
            this.sssScrollTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tslZoom});
            this.sssScrollTest.Location = new System.Drawing.Point(0, 755);
            this.sssScrollTest.Name = "sssScrollTest";
            this.sssScrollTest.Size = new System.Drawing.Size(1321, 22);
            this.sssScrollTest.TabIndex = 8;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Zoom: ";
            // 
            // tslZoom
            // 
            this.tslZoom.AutoSize = false;
            this.tslZoom.Name = "tslZoom";
            this.tslZoom.Size = new System.Drawing.Size(64, 17);
            this.tslZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(191, 16);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(1095, 712);
            this.axDrawingControl.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTestCase6);
            this.panel1.Controls.Add(this.btnTestCase5);
            this.panel1.Controls.Add(this.btnTestCase4);
            this.panel1.Controls.Add(this.btnTestCase3);
            this.panel1.Controls.Add(this.btnTestCase2);
            this.panel1.Controls.Add(this.btnTestCase1);
            this.panel1.Location = new System.Drawing.Point(13, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 712);
            this.panel1.TabIndex = 9;
            // 
            // btnTestCase2
            // 
            this.btnTestCase2.Location = new System.Drawing.Point(30, 76);
            this.btnTestCase2.Name = "btnTestCase2";
            this.btnTestCase2.Size = new System.Drawing.Size(108, 23);
            this.btnTestCase2.TabIndex = 1;
            this.btnTestCase2.Text = "Test Case 2";
            this.btnTestCase2.UseVisualStyleBackColor = true;
            this.btnTestCase2.Click += new System.EventHandler(this.btnTestCase2_Click);
            // 
            // btnTestCase1
            // 
            this.btnTestCase1.Location = new System.Drawing.Point(30, 27);
            this.btnTestCase1.Name = "btnTestCase1";
            this.btnTestCase1.Size = new System.Drawing.Size(108, 23);
            this.btnTestCase1.TabIndex = 0;
            this.btnTestCase1.Text = "Test Case 1";
            this.btnTestCase1.UseVisualStyleBackColor = true;
            this.btnTestCase1.Click += new System.EventHandler(this.btnTestCase1_Click);
            // 
            // btnTestCase3
            // 
            this.btnTestCase3.Location = new System.Drawing.Point(30, 125);
            this.btnTestCase3.Name = "btnTestCase3";
            this.btnTestCase3.Size = new System.Drawing.Size(108, 23);
            this.btnTestCase3.TabIndex = 2;
            this.btnTestCase3.Text = "Test Case 3";
            this.btnTestCase3.UseVisualStyleBackColor = true;
            this.btnTestCase3.Click += new System.EventHandler(this.btnTestCase3_Click);
            // 
            // btnTestCase4
            // 
            this.btnTestCase4.Location = new System.Drawing.Point(30, 174);
            this.btnTestCase4.Name = "btnTestCase4";
            this.btnTestCase4.Size = new System.Drawing.Size(108, 23);
            this.btnTestCase4.TabIndex = 3;
            this.btnTestCase4.Text = "Test Case 4";
            this.btnTestCase4.UseVisualStyleBackColor = true;
            this.btnTestCase4.Click += new System.EventHandler(this.btnTestCase4_Click);
            // 
            // btnTestCase5
            // 
            this.btnTestCase5.Location = new System.Drawing.Point(30, 223);
            this.btnTestCase5.Name = "btnTestCase5";
            this.btnTestCase5.Size = new System.Drawing.Size(108, 23);
            this.btnTestCase5.TabIndex = 4;
            this.btnTestCase5.Text = "Test Case 5";
            this.btnTestCase5.UseVisualStyleBackColor = true;
            this.btnTestCase5.Click += new System.EventHandler(this.btnTestCase5_Click);
            // 
            // btnTestCase6
            // 
            this.btnTestCase6.Location = new System.Drawing.Point(30, 272);
            this.btnTestCase6.Name = "btnTestCase6";
            this.btnTestCase6.Size = new System.Drawing.Size(108, 23);
            this.btnTestCase6.TabIndex = 5;
            this.btnTestCase6.Text = "Test Case 6";
            this.btnTestCase6.UseVisualStyleBackColor = true;
            this.btnTestCase6.Click += new System.EventHandler(this.btnTestCase6_Click);
            // 
            // DirectedPolygonUnionOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 777);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sssScrollTest);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "DirectedPolygonUnionOperationsForm";
            this.Text = "Scroll Test";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnderages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverages)).EndInit();
            this.sssScrollTest.ResumeLayout(false);
            this.sssScrollTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvOverages;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvUnderages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.StatusStrip sssScrollTest;
        public System.Windows.Forms.ToolStripStatusLabel tslZoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTestCase1;
        private System.Windows.Forms.Button btnTestCase2;
        private System.Windows.Forms.Button btnTestCase3;
        private System.Windows.Forms.Button btnTestCase4;
        private System.Windows.Forms.Button btnTestCase5;
        private System.Windows.Forms.Button btnTestCase6;
    }
}

