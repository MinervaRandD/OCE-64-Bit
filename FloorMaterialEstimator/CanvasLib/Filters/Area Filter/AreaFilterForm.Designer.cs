namespace CanvasLib.Filters.Area_Filter
{
    partial class AreaFilterForm
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
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.pnlAreaFilterRows = new System.Windows.Forms.Panel();
            this.grpMaterialFiler = new System.Windows.Forms.GroupBox();
            this.BtnGTZero = new System.Windows.Forms.Button();
            this.grpMaterialFiler.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAll
            // 
            this.btnAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll.Location = new System.Drawing.Point(132, 598);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "Show All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNone.Location = new System.Drawing.Point(364, 598);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(87, 23);
            this.btnNone.TabIndex = 2;
            this.btnNone.Text = "Show None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // pnlAreaFilterRows
            // 
            this.pnlAreaFilterRows.Location = new System.Drawing.Point(20, 25);
            this.pnlAreaFilterRows.Name = "pnlAreaFilterRows";
            this.pnlAreaFilterRows.Size = new System.Drawing.Size(550, 555);
            this.pnlAreaFilterRows.TabIndex = 3;
            // 
            // grpMaterialFiler
            // 
            this.grpMaterialFiler.Controls.Add(this.BtnGTZero);
            this.grpMaterialFiler.Controls.Add(this.pnlAreaFilterRows);
            this.grpMaterialFiler.Controls.Add(this.btnAll);
            this.grpMaterialFiler.Controls.Add(this.btnNone);
            this.grpMaterialFiler.Location = new System.Drawing.Point(12, 12);
            this.grpMaterialFiler.Name = "grpMaterialFiler";
            this.grpMaterialFiler.Size = new System.Drawing.Size(576, 637);
            this.grpMaterialFiler.TabIndex = 5;
            this.grpMaterialFiler.TabStop = false;
            this.grpMaterialFiler.Text = "Materials to Show";
            // 
            // BtnGTZero
            // 
            this.BtnGTZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGTZero.Location = new System.Drawing.Point(248, 598);
            this.BtnGTZero.Name = "BtnGTZero";
            this.BtnGTZero.Size = new System.Drawing.Size(75, 23);
            this.BtnGTZero.TabIndex = 4;
            this.BtnGTZero.Text = "Show > 0";
            this.BtnGTZero.UseVisualStyleBackColor = true;
            this.BtnGTZero.Click += new System.EventHandler(this.btnGTZero_Click);
            // 
            // AreaFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 664);
            this.Controls.Add(this.grpMaterialFiler);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AreaFilterForm";
            this.Text = "Filter Areas";
            this.grpMaterialFiler.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Panel pnlAreaFilterRows;
        private System.Windows.Forms.GroupBox grpMaterialFiler;
        public System.Windows.Forms.Button BtnGTZero;
    }
}