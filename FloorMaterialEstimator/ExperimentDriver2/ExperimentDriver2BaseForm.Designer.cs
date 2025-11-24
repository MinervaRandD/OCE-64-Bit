namespace ExperimentDriver4
{
    partial class ExperimentDriver2BaseForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpGeometry = new System.Windows.Forms.TabPage();
            this.btnTest4 = new System.Windows.Forms.Button();
            this.btnTest3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnTest1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbpGeometry.SuspendLayout();
            this.SuspendLayout();
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(450, 42);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(1163, 766);
            this.axDrawingControl.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpGeometry);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(53, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(227, 766);
            this.tabControl1.TabIndex = 5;
            // 
            // tbpGeometry
            // 
            this.tbpGeometry.Controls.Add(this.btnTest4);
            this.tbpGeometry.Controls.Add(this.btnTest3);
            this.tbpGeometry.Controls.Add(this.button2);
            this.tbpGeometry.Controls.Add(this.btnTest1);
            this.tbpGeometry.Location = new System.Drawing.Point(4, 22);
            this.tbpGeometry.Name = "tbpGeometry";
            this.tbpGeometry.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeometry.Size = new System.Drawing.Size(219, 740);
            this.tbpGeometry.TabIndex = 0;
            this.tbpGeometry.Text = "Geometry Tests";
            this.tbpGeometry.UseVisualStyleBackColor = true;
            // 
            // btnTest4
            // 
            this.btnTest4.Location = new System.Drawing.Point(61, 191);
            this.btnTest4.Name = "btnTest4";
            this.btnTest4.Size = new System.Drawing.Size(75, 23);
            this.btnTest4.TabIndex = 3;
            this.btnTest4.Text = "Test 4";
            this.btnTest4.UseVisualStyleBackColor = true;
            this.btnTest4.Click += new System.EventHandler(this.btnTest4_Click);
            // 
            // btnTest3
            // 
            this.btnTest3.Location = new System.Drawing.Point(60, 136);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(75, 23);
            this.btnTest3.TabIndex = 2;
            this.btnTest3.Text = "Test 3";
            this.btnTest3.UseVisualStyleBackColor = true;
            this.btnTest3.Click += new System.EventHandler(this.btnTest3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(60, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Inner - Outer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(60, 34);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(90, 23);
            this.btnTest1.TabIndex = 0;
            this.btnTest1.Text = "Outer - Inner";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(219, 740);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ExperimentDriver2BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1626, 850);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "ExperimentDriver2BaseForm";
            this.Text = "Test Driver 1";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbpGeometry.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpGeometry;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnTest3;
        private System.Windows.Forms.Button btnTest4;
    }
}

