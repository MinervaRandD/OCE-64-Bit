namespace TestDriverPolygonDistanceGenerator
{
    partial class PolygonDistanceGeneratorMainForm
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
            this.lblTest1Distance = new System.Windows.Forms.Label();
            this.lblTest2Distance = new System.Windows.Forms.Label();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.lblTest3Distance = new System.Windows.Forms.Label();
            this.btnTest3 = new System.Windows.Forms.Button();
            this.lblTest4Distance = new System.Windows.Forms.Label();
            this.btnTest4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(28, 44);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(75, 23);
            this.btnTest1.TabIndex = 0;
            this.btnTest1.Text = "Test 1";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // lblTest1Distance
            // 
            this.lblTest1Distance.Location = new System.Drawing.Point(163, 44);
            this.lblTest1Distance.Name = "lblTest1Distance";
            this.lblTest1Distance.Size = new System.Drawing.Size(100, 23);
            this.lblTest1Distance.TabIndex = 1;
            this.lblTest1Distance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTest2Distance
            // 
            this.lblTest2Distance.Location = new System.Drawing.Point(164, 93);
            this.lblTest2Distance.Name = "lblTest2Distance";
            this.lblTest2Distance.Size = new System.Drawing.Size(100, 23);
            this.lblTest2Distance.TabIndex = 3;
            this.lblTest2Distance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(29, 93);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(75, 23);
            this.btnTest2.TabIndex = 2;
            this.btnTest2.Text = "Test 2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // lblTest3Distance
            // 
            this.lblTest3Distance.Location = new System.Drawing.Point(166, 155);
            this.lblTest3Distance.Name = "lblTest3Distance";
            this.lblTest3Distance.Size = new System.Drawing.Size(100, 23);
            this.lblTest3Distance.TabIndex = 5;
            this.lblTest3Distance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTest3
            // 
            this.btnTest3.Location = new System.Drawing.Point(31, 155);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(75, 23);
            this.btnTest3.TabIndex = 4;
            this.btnTest3.Text = "Test 3";
            this.btnTest3.UseVisualStyleBackColor = true;
            this.btnTest3.Click += new System.EventHandler(this.btnTest3_Click);
            // 
            // lblTest4Distance
            // 
            this.lblTest4Distance.Location = new System.Drawing.Point(167, 214);
            this.lblTest4Distance.Name = "lblTest4Distance";
            this.lblTest4Distance.Size = new System.Drawing.Size(100, 23);
            this.lblTest4Distance.TabIndex = 7;
            this.lblTest4Distance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTest4
            // 
            this.btnTest4.Location = new System.Drawing.Point(32, 214);
            this.btnTest4.Name = "btnTest4";
            this.btnTest4.Size = new System.Drawing.Size(75, 23);
            this.btnTest4.TabIndex = 6;
            this.btnTest4.Text = "Test 4";
            this.btnTest4.UseVisualStyleBackColor = true;
            this.btnTest4.Click += new System.EventHandler(this.btnTest4_Click);
            // 
            // PolygonDistanceGeneratorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 450);
            this.Controls.Add(this.lblTest4Distance);
            this.Controls.Add(this.btnTest4);
            this.Controls.Add(this.lblTest3Distance);
            this.Controls.Add(this.btnTest3);
            this.Controls.Add(this.lblTest2Distance);
            this.Controls.Add(this.btnTest2);
            this.Controls.Add(this.lblTest1Distance);
            this.Controls.Add(this.btnTest1);
            this.Name = "PolygonDistanceGeneratorMainForm";
            this.Text = "Polygon Distance Generator Test Harness";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.Label lblTest1Distance;
        private System.Windows.Forms.Label lblTest2Distance;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Label lblTest3Distance;
        private System.Windows.Forms.Button btnTest3;
        private System.Windows.Forms.Label lblTest4Distance;
        private System.Windows.Forms.Button btnTest4;
    }
}

