namespace CanvasLib.Filters.Line_Filter
{
    partial class LineFilterForm
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
            this.pnlLineFilterRows = new System.Windows.Forms.Panel();
            this.btnLinesNone = new System.Windows.Forms.Button();
            this.btnLinesAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSeamsNone = new System.Windows.Forms.Button();
            this.btnSeamsAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSeamFilterRows = new System.Windows.Forms.Panel();
            this.BtnLinesGT0 = new System.Windows.Forms.Button();
            this.btnSeamsGT0 = new System.Windows.Forms.Button();
            this.combinedTotalRow1 = new CanvasLib.Filters.Line_Filter.CombinedTotalRow();
            this.SuspendLayout();
            // 
            // pnlLineFilterRows
            // 
            this.pnlLineFilterRows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLineFilterRows.Location = new System.Drawing.Point(12, 38);
            this.pnlLineFilterRows.Name = "pnlLineFilterRows";
            this.pnlLineFilterRows.Size = new System.Drawing.Size(550, 308);
            this.pnlLineFilterRows.TabIndex = 0;
            // 
            // btnLinesNone
            // 
            this.btnLinesNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinesNone.Location = new System.Drawing.Point(361, 363);
            this.btnLinesNone.Name = "btnLinesNone";
            this.btnLinesNone.Size = new System.Drawing.Size(88, 23);
            this.btnLinesNone.TabIndex = 5;
            this.btnLinesNone.Text = "Show None";
            this.btnLinesNone.UseVisualStyleBackColor = true;
            this.btnLinesNone.Click += new System.EventHandler(this.btnLinesNone_Click);
            // 
            // btnLinesAll
            // 
            this.btnLinesAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinesAll.Location = new System.Drawing.Point(119, 363);
            this.btnLinesAll.Name = "btnLinesAll";
            this.btnLinesAll.Size = new System.Drawing.Size(75, 23);
            this.btnLinesAll.TabIndex = 4;
            this.btnLinesAll.Text = "Show All";
            this.btnLinesAll.UseVisualStyleBackColor = true;
            this.btnLinesAll.Click += new System.EventHandler(this.btnLinesAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Include:";
            // 
            // btnSeamsNone
            // 
            this.btnSeamsNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeamsNone.Location = new System.Drawing.Point(361, 788);
            this.btnSeamsNone.Name = "btnSeamsNone";
            this.btnSeamsNone.Size = new System.Drawing.Size(88, 23);
            this.btnSeamsNone.TabIndex = 9;
            this.btnSeamsNone.Text = "Show None";
            this.btnSeamsNone.UseVisualStyleBackColor = true;
            this.btnSeamsNone.Click += new System.EventHandler(this.btnSeamsNone_Click);
            // 
            // btnSeamsAll
            // 
            this.btnSeamsAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeamsAll.Location = new System.Drawing.Point(119, 788);
            this.btnSeamsAll.Name = "btnSeamsAll";
            this.btnSeamsAll.Size = new System.Drawing.Size(75, 23);
            this.btnSeamsAll.TabIndex = 8;
            this.btnSeamsAll.Text = "Show All";
            this.btnSeamsAll.UseVisualStyleBackColor = true;
            this.btnSeamsAll.Click += new System.EventHandler(this.btnSeamsAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 408);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Include:";
            // 
            // pnlSeamFilterRows
            // 
            this.pnlSeamFilterRows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSeamFilterRows.Location = new System.Drawing.Point(15, 438);
            this.pnlSeamFilterRows.Name = "pnlSeamFilterRows";
            this.pnlSeamFilterRows.Size = new System.Drawing.Size(550, 326);
            this.pnlSeamFilterRows.TabIndex = 6;
            // 
            // BtnLinesGT0
            // 
            this.BtnLinesGT0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLinesGT0.Location = new System.Drawing.Point(240, 363);
            this.BtnLinesGT0.Name = "BtnLinesGT0";
            this.BtnLinesGT0.Size = new System.Drawing.Size(75, 23);
            this.BtnLinesGT0.TabIndex = 11;
            this.BtnLinesGT0.Text = "Show > 0";
            this.BtnLinesGT0.UseVisualStyleBackColor = true;
            this.BtnLinesGT0.Click += new System.EventHandler(this.btnLinesGT0_Click);
            // 
            // btnSeamsGT0
            // 
            this.btnSeamsGT0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeamsGT0.Location = new System.Drawing.Point(240, 788);
            this.btnSeamsGT0.Name = "btnSeamsGT0";
            this.btnSeamsGT0.Size = new System.Drawing.Size(75, 23);
            this.btnSeamsGT0.TabIndex = 12;
            this.btnSeamsGT0.Text = "Show > 0";
            this.btnSeamsGT0.UseVisualStyleBackColor = true;
            this.btnSeamsGT0.Click += new System.EventHandler(this.btnSeamsGT0_Click);
            // 
            // combinedTotalRow1
            // 
            this.combinedTotalRow1.Location = new System.Drawing.Point(244, 825);
            this.combinedTotalRow1.Name = "combinedTotalRow1";
            this.combinedTotalRow1.Size = new System.Drawing.Size(326, 25);
            this.combinedTotalRow1.TabIndex = 10;
            // 
            // LineFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 863);
            this.Controls.Add(this.btnSeamsGT0);
            this.Controls.Add(this.BtnLinesGT0);
            this.Controls.Add(this.combinedTotalRow1);
            this.Controls.Add(this.btnSeamsNone);
            this.Controls.Add(this.btnSeamsAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlSeamFilterRows);
            this.Controls.Add(this.btnLinesNone);
            this.Controls.Add(this.btnLinesAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlLineFilterRows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LineFilterForm";
            this.Text = "LineFilterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLineFilterRows;
        private System.Windows.Forms.Button btnLinesNone;
        private System.Windows.Forms.Button btnLinesAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeamsNone;
        private System.Windows.Forms.Button btnSeamsAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlSeamFilterRows;
        private CombinedTotalRow combinedTotalRow1;
        private System.Windows.Forms.Button btnSeamsGT0;
        public System.Windows.Forms.Button BtnLinesGT0;
    }
}