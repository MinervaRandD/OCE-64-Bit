
namespace CanvasLib.Legend
{
    partial class LegendNotesForm
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
            this.txbNotes = new System.Windows.Forms.TextBox();
            this.lblLegendNotesHeader = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbNotes
            // 
            this.txbNotes.AcceptsReturn = true;
            this.txbNotes.Location = new System.Drawing.Point(12, 58);
            this.txbNotes.Multiline = true;
            this.txbNotes.Name = "txbNotes";
            this.txbNotes.Size = new System.Drawing.Size(361, 91);
            this.txbNotes.TabIndex = 0;
            // 
            // lblLegendNotesHeader
            // 
            this.lblLegendNotesHeader.AutoSize = true;
            this.lblLegendNotesHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegendNotesHeader.Location = new System.Drawing.Point(69, 28);
            this.lblLegendNotesHeader.Name = "lblLegendNotesHeader";
            this.lblLegendNotesHeader.Size = new System.Drawing.Size(279, 18);
            this.lblLegendNotesHeader.TabIndex = 1;
            this.lblLegendNotesHeader.Text = "Enter Notes To Be Added To The Legend";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(72, 184);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // LegendNotesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(397, 235);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLegendNotesHeader);
            this.Controls.Add(this.txbNotes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LegendNotesForm";
            this.Text = "LegendNotesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbNotes;
        private System.Windows.Forms.Label lblLegendNotesHeader;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}