namespace CanvasLib.Scale_Line
{
    partial class ScaleForm
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
            this.lblClickFirstPoint = new System.Windows.Forms.Label();
            this.lblClickSecondPoint = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.txbFeet = new System.Windows.Forms.TextBox();
            this.txbInches = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFeet = new System.Windows.Forms.Label();
            this.lblInches = new System.Windows.Forms.Label();
            this.gbxSetListEndpoints = new System.Windows.Forms.GroupBox();
            this.gbxSetLineLength = new System.Windows.Forms.GroupBox();
            this.grbActionOnExistingSeams = new System.Windows.Forms.GroupBox();
            this.CkbReseamAllAreas = new System.Windows.Forms.CheckBox();
            this.CkbRemoveAllSeams = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbLengthInFeet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbxSetListEndpoints.SuspendLayout();
            this.gbxSetLineLength.SuspendLayout();
            this.grbActionOnExistingSeams.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClickFirstPoint
            // 
            this.lblClickFirstPoint.AutoSize = true;
            this.lblClickFirstPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickFirstPoint.Location = new System.Drawing.Point(17, 35);
            this.lblClickFirstPoint.Name = "lblClickFirstPoint";
            this.lblClickFirstPoint.Size = new System.Drawing.Size(138, 20);
            this.lblClickFirstPoint.TabIndex = 0;
            this.lblClickFirstPoint.Text = "Click the first point";
            // 
            // lblClickSecondPoint
            // 
            this.lblClickSecondPoint.AutoSize = true;
            this.lblClickSecondPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickSecondPoint.Location = new System.Drawing.Point(17, 70);
            this.lblClickSecondPoint.Name = "lblClickSecondPoint";
            this.lblClickSecondPoint.Size = new System.Drawing.Size(164, 20);
            this.lblClickSecondPoint.TabIndex = 2;
            this.lblClickSecondPoint.Text = "Click the second point";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(168, 267);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 31);
            this.btnReset.TabIndex = 3;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // txbFeet
            // 
            this.txbFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbFeet.Location = new System.Drawing.Point(18, 60);
            this.txbFeet.Name = "txbFeet";
            this.txbFeet.Size = new System.Drawing.Size(77, 26);
            this.txbFeet.TabIndex = 0;
            this.txbFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbFeet.MouseEnter += new System.EventHandler(this.txbFeet_MouseEnter);
            // 
            // txbInches
            // 
            this.txbInches.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbInches.Location = new System.Drawing.Point(119, 60);
            this.txbInches.Name = "txbInches";
            this.txbInches.Size = new System.Drawing.Size(53, 26);
            this.txbInches.TabIndex = 1;
            this.txbInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(30, 267);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 31);
            this.btnOK.TabIndex = 2;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBack.Location = new System.Drawing.Point(230, 45);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(111, 31);
            this.btnGoBack.TabIndex = 8;
            this.btnGoBack.TabStop = false;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(304, 267);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblFeet
            // 
            this.lblFeet.AutoSize = true;
            this.lblFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeet.Location = new System.Drawing.Point(35, 33);
            this.lblFeet.Name = "lblFeet";
            this.lblFeet.Size = new System.Drawing.Size(42, 20);
            this.lblFeet.TabIndex = 10;
            this.lblFeet.Text = "Feet";
            // 
            // lblInches
            // 
            this.lblInches.AutoSize = true;
            this.lblInches.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInches.Location = new System.Drawing.Point(117, 33);
            this.lblInches.Name = "lblInches";
            this.lblInches.Size = new System.Drawing.Size(57, 20);
            this.lblInches.TabIndex = 11;
            this.lblInches.Text = "Inches";
            // 
            // gbxSetListEndpoints
            // 
            this.gbxSetListEndpoints.Controls.Add(this.lblClickFirstPoint);
            this.gbxSetListEndpoints.Controls.Add(this.lblClickSecondPoint);
            this.gbxSetListEndpoints.Controls.Add(this.btnGoBack);
            this.gbxSetListEndpoints.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbxSetListEndpoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSetListEndpoints.Location = new System.Drawing.Point(23, 13);
            this.gbxSetListEndpoints.Name = "gbxSetListEndpoints";
            this.gbxSetListEndpoints.Size = new System.Drawing.Size(378, 109);
            this.gbxSetListEndpoints.TabIndex = 12;
            this.gbxSetListEndpoints.TabStop = false;
            this.gbxSetListEndpoints.Text = "Select Scale Line Endpoints";
            // 
            // gbxSetLineLength
            // 
            this.gbxSetLineLength.Controls.Add(this.label2);
            this.gbxSetLineLength.Controls.Add(this.label1);
            this.gbxSetLineLength.Controls.Add(this.txbLengthInFeet);
            this.gbxSetLineLength.Controls.Add(this.lblFeet);
            this.gbxSetLineLength.Controls.Add(this.txbFeet);
            this.gbxSetLineLength.Controls.Add(this.lblInches);
            this.gbxSetLineLength.Controls.Add(this.txbInches);
            this.gbxSetLineLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSetLineLength.Location = new System.Drawing.Point(23, 139);
            this.gbxSetLineLength.Name = "gbxSetLineLength";
            this.gbxSetLineLength.Size = new System.Drawing.Size(378, 100);
            this.gbxSetLineLength.TabIndex = 13;
            this.gbxSetLineLength.TabStop = false;
            this.gbxSetLineLength.Text = "Set Line Length";
            // 
            // grbActionOnExistingSeams
            // 
            this.grbActionOnExistingSeams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grbActionOnExistingSeams.Controls.Add(this.CkbReseamAllAreas);
            this.grbActionOnExistingSeams.Controls.Add(this.CkbRemoveAllSeams);
            this.grbActionOnExistingSeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbActionOnExistingSeams.Location = new System.Drawing.Point(23, 339);
            this.grbActionOnExistingSeams.Name = "grbActionOnExistingSeams";
            this.grbActionOnExistingSeams.Size = new System.Drawing.Size(378, 71);
            this.grbActionOnExistingSeams.TabIndex = 14;
            this.grbActionOnExistingSeams.TabStop = false;
            this.grbActionOnExistingSeams.Text = "Select Action On Existing Seams";
            // 
            // CkbReseamAllAreas
            // 
            this.CkbReseamAllAreas.AutoSize = true;
            this.CkbReseamAllAreas.Location = new System.Drawing.Point(204, 29);
            this.CkbReseamAllAreas.Name = "CkbReseamAllAreas";
            this.CkbReseamAllAreas.Size = new System.Drawing.Size(155, 24);
            this.CkbReseamAllAreas.TabIndex = 1;
            this.CkbReseamAllAreas.Text = "Reseam All Areas";
            this.CkbReseamAllAreas.UseVisualStyleBackColor = true;
            // 
            // CkbRemoveAllSeams
            // 
            this.CkbRemoveAllSeams.AutoSize = true;
            this.CkbRemoveAllSeams.Location = new System.Drawing.Point(21, 29);
            this.CkbRemoveAllSeams.Name = "CkbRemoveAllSeams";
            this.CkbRemoveAllSeams.Size = new System.Drawing.Size(162, 24);
            this.CkbRemoveAllSeams.TabIndex = 0;
            this.CkbRemoveAllSeams.Text = "Remove All Seams";
            this.CkbRemoveAllSeams.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(235, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Length (Feet)";
            // 
            // txbLengthInFeet
            // 
            this.txbLengthInFeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLengthInFeet.Location = new System.Drawing.Point(249, 60);
            this.txbLengthInFeet.Name = "txbLengthInFeet";
            this.txbLengthInFeet.Size = new System.Drawing.Size(77, 26);
            this.txbLengthInFeet.TabIndex = 12;
            this.txbLengthInFeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(196, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Or";
            // 
            // ScaleForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 431);
            this.ControlBox = false;
            this.Controls.Add(this.grbActionOnExistingSeams);
            this.Controls.Add(this.gbxSetLineLength);
            this.Controls.Add(this.gbxSetListEndpoints);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnReset);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScaleForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Set Scale";
            this.gbxSetListEndpoints.ResumeLayout(false);
            this.gbxSetListEndpoints.PerformLayout();
            this.gbxSetLineLength.ResumeLayout(false);
            this.gbxSetLineLength.PerformLayout();
            this.grbActionOnExistingSeams.ResumeLayout(false);
            this.grbActionOnExistingSeams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblClickFirstPoint;
        private System.Windows.Forms.Label lblClickSecondPoint;
        private System.Windows.Forms.Label lblFeet;
        private System.Windows.Forms.Label lblInches;
        public System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox gbxSetListEndpoints;
        private System.Windows.Forms.GroupBox gbxSetLineLength;
        public System.Windows.Forms.Button btnGoBack;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox txbFeet;
        public System.Windows.Forms.TextBox txbInches;
        private System.Windows.Forms.GroupBox grbActionOnExistingSeams;
        public System.Windows.Forms.CheckBox CkbRemoveAllSeams;
        public System.Windows.Forms.CheckBox CkbReseamAllAreas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txbLengthInFeet;
    }
}