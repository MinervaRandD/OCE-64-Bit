
namespace FloorMaterialEstimator.Finish_Controls
{
    partial class AreaFinishesChangedConfirmForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.flpChangedAreaFinishes = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(118, 402);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // flpChangedAreaFinishes
            // 
            this.flpChangedAreaFinishes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpChangedAreaFinishes.Location = new System.Drawing.Point(58, 73);
            this.flpChangedAreaFinishes.Name = "flpChangedAreaFinishes";
            this.flpChangedAreaFinishes.Size = new System.Drawing.Size(305, 268);
            this.flpChangedAreaFinishes.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(55, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 34);
            this.label1.TabIndex = 2;
            this.label1.Text = "The following area finishes are seamed and have changed. The seams will be remove" +
    "d.";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(234, 402);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AreaFinishesChangedConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpChangedAreaFinishes);
            this.Controls.Add(this.btnOK);
            this.Name = "AreaFinishesChangedConfirmForm";
            this.Text = "Area Finishes Changed";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FlowLayoutPanel flpChangedAreaFinishes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
    }
}