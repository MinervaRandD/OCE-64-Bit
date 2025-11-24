namespace TestDriverVoronoiAlgo
{
    partial class VoronoiAlgoTestForm
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
            this.btnTest1 = new System.Windows.Forms.Button();
            this.grbTests = new System.Windows.Forms.GroupBox();
            this.btnTest3 = new System.Windows.Forms.Button();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.btnTest4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.grbTests.SuspendLayout();
            this.SuspendLayout();
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(226, 40);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(935, 613);
            this.axDrawingControl.TabIndex = 6;
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(30, 34);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(64, 23);
            this.btnTest1.TabIndex = 7;
            this.btnTest1.Text = "Test 1";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // grbTests
            // 
            this.grbTests.Controls.Add(this.btnTest4);
            this.grbTests.Controls.Add(this.btnTest3);
            this.grbTests.Controls.Add(this.btnTest2);
            this.grbTests.Controls.Add(this.btnTest1);
            this.grbTests.Location = new System.Drawing.Point(13, 40);
            this.grbTests.Name = "grbTests";
            this.grbTests.Size = new System.Drawing.Size(120, 613);
            this.grbTests.TabIndex = 8;
            this.grbTests.TabStop = false;
            this.grbTests.Text = "Tests";
            // 
            // btnTest3
            // 
            this.btnTest3.Location = new System.Drawing.Point(30, 168);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(64, 23);
            this.btnTest3.TabIndex = 9;
            this.btnTest3.Text = "Test 3";
            this.btnTest3.UseVisualStyleBackColor = true;
            this.btnTest3.Click += new System.EventHandler(this.btnTest3_Click);
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(30, 101);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(64, 23);
            this.btnTest2.TabIndex = 8;
            this.btnTest2.Text = "Test 2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // btnTest4
            // 
            this.btnTest4.Location = new System.Drawing.Point(30, 235);
            this.btnTest4.Name = "btnTest4";
            this.btnTest4.Size = new System.Drawing.Size(64, 23);
            this.btnTest4.TabIndex = 10;
            this.btnTest4.Text = "Test 4";
            this.btnTest4.UseVisualStyleBackColor = true;
            this.btnTest4.Click += new System.EventHandler(this.btnTest4_Click);
            // 
            // VoronoiAlgoTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 701);
            this.Controls.Add(this.grbTests);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "VoronoiAlgoTestForm";
            this.Text = "Seams And Cuts";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.grbTests.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.GroupBox grbTests;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Button btnTest3;
        private System.Windows.Forms.Button btnTest4;
    }
}

