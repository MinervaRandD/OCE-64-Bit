namespace TestDriverDragAndDrop
{
    partial class DragAndDropTestForm
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
            this.btnTest1 = new System.Windows.Forms.Button();
            this.grbTests = new System.Windows.Forms.GroupBox();
            this.btnTest3 = new System.Windows.Forms.Button();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.grbTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(38, 28);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(64, 23);
            this.btnTest1.TabIndex = 7;
            this.btnTest1.Text = "Test 1";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // grbTests
            // 
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
            this.btnTest3.Location = new System.Drawing.Point(38, 157);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(64, 23);
            this.btnTest3.TabIndex = 9;
            this.btnTest3.Text = "Test 3";
            this.btnTest3.UseVisualStyleBackColor = true;
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(38, 88);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(64, 23);
            this.btnTest2.TabIndex = 8;
            this.btnTest2.Text = "Test 2";
            this.btnTest2.UseVisualStyleBackColor = true;
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
            // DragAndDropTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 701);
            this.Controls.Add(this.grbTests);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "DragAndDropTestForm";
            this.Text = "Drag and Drop";
            this.grbTests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.GroupBox grbTests;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Button btnTest3;
    }
}

