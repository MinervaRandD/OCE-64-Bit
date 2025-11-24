namespace TestDriverComboGenerator
{
    partial class ComboGeneratorTestForm
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
            this.btnGenerateCombos = new System.Windows.Forms.Button();
            this.txbElementCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lsbCombinations = new System.Windows.Forms.ListBox();
            this.txbSubsetSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerateCombos
            // 
            this.btnGenerateCombos.Location = new System.Drawing.Point(124, 68);
            this.btnGenerateCombos.Name = "btnGenerateCombos";
            this.btnGenerateCombos.Size = new System.Drawing.Size(121, 35);
            this.btnGenerateCombos.TabIndex = 0;
            this.btnGenerateCombos.Text = "Generate Combinations";
            this.btnGenerateCombos.UseVisualStyleBackColor = true;
            this.btnGenerateCombos.Click += new System.EventHandler(this.btnGenerateCombinations_Click);
            // 
            // txbElementCount
            // 
            this.txbElementCount.Location = new System.Drawing.Point(43, 68);
            this.txbElementCount.Name = "txbElementCount";
            this.txbElementCount.Size = new System.Drawing.Size(48, 20);
            this.txbElementCount.TabIndex = 1;
            this.txbElementCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(36, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Element Mask";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lsbCombinations
            // 
            this.lsbCombinations.FormattingEnabled = true;
            this.lsbCombinations.Location = new System.Drawing.Point(30, 162);
            this.lsbCombinations.Name = "lsbCombinations";
            this.lsbCombinations.Size = new System.Drawing.Size(215, 264);
            this.lsbCombinations.TabIndex = 3;
            // 
            // txbSubsetSize
            // 
            this.txbSubsetSize.Location = new System.Drawing.Point(43, 125);
            this.txbSubsetSize.Name = "txbSubsetSize";
            this.txbSubsetSize.Size = new System.Drawing.Size(48, 20);
            this.txbSubsetSize.TabIndex = 4;
            this.txbSubsetSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Subset Size";
            // 
            // ComboGeneratorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 486);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbSubsetSize);
            this.Controls.Add(this.lsbCombinations);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbElementCount);
            this.Controls.Add(this.btnGenerateCombos);
            this.Name = "ComboGeneratorTestForm";
            this.Text = "Partition Generator Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateCombos;
        private System.Windows.Forms.TextBox txbElementCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lsbCombinations;
        private System.Windows.Forms.TextBox txbSubsetSize;
        private System.Windows.Forms.Label label2;
    }
}

