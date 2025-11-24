namespace TestDriverKeyboardTests
{
    partial class KeyboardTestForm
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
            this.lblKeyVal = new System.Windows.Forms.Label();
            this.lblRepeatCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKeyUp = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblF1Pressed = new System.Windows.Forms.Label();
            this.lblGetKeyPressed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblKeyVal
            // 
            this.lblKeyVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKeyVal.Location = new System.Drawing.Point(97, 119);
            this.lblKeyVal.Name = "lblKeyVal";
            this.lblKeyVal.Size = new System.Drawing.Size(100, 23);
            this.lblKeyVal.TabIndex = 9;
            this.lblKeyVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRepeatCount
            // 
            this.lblRepeatCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatCount.Location = new System.Drawing.Point(233, 119);
            this.lblRepeatCount.Name = "lblRepeatCount";
            this.lblRepeatCount.Size = new System.Drawing.Size(78, 23);
            this.lblRepeatCount.TabIndex = 10;
            this.lblRepeatCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(233, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 12;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKeyUp
            // 
            this.lblKeyUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKeyUp.Location = new System.Drawing.Point(97, 174);
            this.lblKeyUp.Name = "lblKeyUp";
            this.lblKeyUp.Size = new System.Drawing.Size(100, 23);
            this.lblKeyUp.TabIndex = 11;
            this.lblKeyUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(110, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "F1 Pressed";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblF1Pressed
            // 
            this.lblF1Pressed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblF1Pressed.Location = new System.Drawing.Point(233, 247);
            this.lblF1Pressed.Name = "lblF1Pressed";
            this.lblF1Pressed.Size = new System.Drawing.Size(78, 23);
            this.lblF1Pressed.TabIndex = 14;
            this.lblF1Pressed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGetKeyPressed
            // 
            this.lblGetKeyPressed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGetKeyPressed.Location = new System.Drawing.Point(110, 318);
            this.lblGetKeyPressed.Name = "lblGetKeyPressed";
            this.lblGetKeyPressed.Size = new System.Drawing.Size(87, 23);
            this.lblGetKeyPressed.TabIndex = 15;
            this.lblGetKeyPressed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // KeyboardTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 679);
            this.Controls.Add(this.lblGetKeyPressed);
            this.Controls.Add(this.lblF1Pressed);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblKeyUp);
            this.Controls.Add(this.lblRepeatCount);
            this.Controls.Add(this.lblKeyVal);
            this.Name = "KeyboardTestForm";
            this.Text = "KeyboardTestForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblKeyVal;
        private System.Windows.Forms.Label lblRepeatCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblKeyUp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblF1Pressed;
        private System.Windows.Forms.Label lblGetKeyPressed;
    }
}