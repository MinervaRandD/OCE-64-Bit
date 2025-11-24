namespace TestGeneratorComboGenerator
{
    partial class PartitionGeneratorTesterForm
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
            this.btnGeneratePartitions = new System.Windows.Forms.Button();
            this.txbElementCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lsbPartitions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnGeneratePartitions
            // 
            this.btnGeneratePartitions.Location = new System.Drawing.Point(124, 103);
            this.btnGeneratePartitions.Name = "btnGeneratePartitions";
            this.btnGeneratePartitions.Size = new System.Drawing.Size(121, 23);
            this.btnGeneratePartitions.TabIndex = 0;
            this.btnGeneratePartitions.Text = "Generate Partitions";
            this.btnGeneratePartitions.UseVisualStyleBackColor = true;
            this.btnGeneratePartitions.Click += new System.EventHandler(this.btnGeneratePartitions_Click);
            // 
            // txbElementCount
            // 
            this.txbElementCount.Location = new System.Drawing.Point(39, 103);
            this.txbElementCount.Name = "txbElementCount";
            this.txbElementCount.Size = new System.Drawing.Size(48, 20);
            this.txbElementCount.TabIndex = 1;
            this.txbElementCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Elements";
            // 
            // lsbPartitions
            // 
            this.lsbPartitions.FormattingEnabled = true;
            this.lsbPartitions.Location = new System.Drawing.Point(30, 162);
            this.lsbPartitions.Name = "lsbPartitions";
            this.lsbPartitions.Size = new System.Drawing.Size(215, 264);
            this.lsbPartitions.TabIndex = 3;
            // 
            // PartitionGeneratorTesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 486);
            this.Controls.Add(this.lsbPartitions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbElementCount);
            this.Controls.Add(this.btnGeneratePartitions);
            this.Name = "PartitionGeneratorTesterForm";
            this.Text = "Partition Generator Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGeneratePartitions;
        private System.Windows.Forms.TextBox txbElementCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lsbPartitions;
    }
}

