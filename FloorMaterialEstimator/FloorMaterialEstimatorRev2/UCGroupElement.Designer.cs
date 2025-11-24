namespace FloorMaterialEstimator
{
    partial class UCGroupElement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGroupBaseNumber = new System.Windows.Forms.Label();
            this.lblGroupNmbrs = new System.Windows.Forms.Label();
            this.btnShowOptions = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGroupBaseNumber
            // 
            this.lblGroupBaseNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupBaseNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupBaseNumber.Location = new System.Drawing.Point(1, 1);
            this.lblGroupBaseNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroupBaseNumber.Name = "lblGroupBaseNumber";
            this.lblGroupBaseNumber.Size = new System.Drawing.Size(30, 24);
            this.lblGroupBaseNumber.TabIndex = 0;
            this.lblGroupBaseNumber.Text = "1";
            this.lblGroupBaseNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupNmbrs
            // 
            this.lblGroupNmbrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupNmbrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupNmbrs.Location = new System.Drawing.Point(32, 1);
            this.lblGroupNmbrs.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroupNmbrs.Name = "lblGroupNmbrs";
            this.lblGroupNmbrs.Size = new System.Drawing.Size(137, 24);
            this.lblGroupNmbrs.TabIndex = 1;
            this.lblGroupNmbrs.Text = "Group Numbers";
            this.lblGroupNmbrs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnShowOptions
            // 
            this.btnShowOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowOptions.Location = new System.Drawing.Point(171, 1);
            this.btnShowOptions.Margin = new System.Windows.Forms.Padding(0);
            this.btnShowOptions.Name = "btnShowOptions";
            this.btnShowOptions.Size = new System.Drawing.Size(33, 24);
            this.btnShowOptions.TabIndex = 2;
            this.btnShowOptions.UseVisualStyleBackColor = true;
            this.btnShowOptions.Click += new System.EventHandler(this.btnShowOptions_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteGroup.Location = new System.Drawing.Point(207, 1);
            this.btnDeleteGroup.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(45, 24);
            this.btnDeleteGroup.TabIndex = 3;
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // UCGroupElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteGroup);
            this.Controls.Add(this.btnShowOptions);
            this.Controls.Add(this.lblGroupNmbrs);
            this.Controls.Add(this.lblGroupBaseNumber);
            this.Name = "UCGroupElement";
            this.Size = new System.Drawing.Size(260, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGroupBaseNumber;
        private System.Windows.Forms.Label lblGroupNmbrs;
        private System.Windows.Forms.Button btnShowOptions;
        private System.Windows.Forms.Button btnDeleteGroup;
    }
}
