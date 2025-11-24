namespace TestDriverPartitionAllocator
{
    partial class PartionaAllocatorForm
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
            this.lsbAllocations = new System.Windows.Forms.ListBox();
            this.txbPartitonDefinition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateAllocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsbAllocations
            // 
            this.lsbAllocations.FormattingEnabled = true;
            this.lsbAllocations.Location = new System.Drawing.Point(47, 168);
            this.lsbAllocations.Name = "lsbAllocations";
            this.lsbAllocations.Size = new System.Drawing.Size(366, 238);
            this.lsbAllocations.TabIndex = 0;
            // 
            // txbPartitonDefinition
            // 
            this.txbPartitonDefinition.Location = new System.Drawing.Point(161, 56);
            this.txbPartitonDefinition.Name = "txbPartitonDefinition";
            this.txbPartitonDefinition.Size = new System.Drawing.Size(149, 20);
            this.txbPartitonDefinition.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Partition";
            // 
            // btnGenerateAllocation
            // 
            this.btnGenerateAllocation.Location = new System.Drawing.Point(118, 108);
            this.btnGenerateAllocation.Name = "btnGenerateAllocation";
            this.btnGenerateAllocation.Size = new System.Drawing.Size(192, 23);
            this.btnGenerateAllocation.TabIndex = 3;
            this.btnGenerateAllocation.Text = "Generate Allocation";
            this.btnGenerateAllocation.UseVisualStyleBackColor = true;
            this.btnGenerateAllocation.Click += new System.EventHandler(this.btnGenerateAllocation_Click);
            // 
            // PartionaAllocatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 450);
            this.Controls.Add(this.btnGenerateAllocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbPartitonDefinition);
            this.Controls.Add(this.lsbAllocations);
            this.Name = "PartionaAllocatorForm";
            this.Text = "Partition Allocator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbAllocations;
        private System.Windows.Forms.TextBox txbPartitonDefinition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateAllocation;
    }
}

