using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProcessManager
{
    partial class ProcessManagerMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            lblCasesFound = new Label();
            btnEndAll = new Button();
            btnScan = new Button();
            grbLaunchProcess = new GroupBox();
            button1 = new Button();
            btnBrowse = new Button();
            txbPathToExecutable = new TextBox();
            groupBox1.SuspendLayout();
            grbLaunchProcess.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblCasesFound);
            groupBox1.Controls.Add(btnEndAll);
            groupBox1.Controls.Add(btnScan);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(704, 81);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Running Applications";
            // 
            // lblCasesFound
            // 
            lblCasesFound.BackColor = SystemColors.ButtonHighlight;
            lblCasesFound.BorderStyle = BorderStyle.FixedSingle;
            lblCasesFound.Location = new Point(275, 37);
            lblCasesFound.Name = "lblCasesFound";
            lblCasesFound.Size = new Size(100, 20);
            lblCasesFound.TabIndex = 3;
            lblCasesFound.Text = "0 Cases Found";
            lblCasesFound.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEndAll
            // 
            btnEndAll.Location = new Point(446, 37);
            btnEndAll.Name = "btnEndAll";
            btnEndAll.Size = new Size(174, 23);
            btnEndAll.TabIndex = 2;
            btnEndAll.Text = "End All Executing Processes";
            btnEndAll.UseVisualStyleBackColor = true;
            btnEndAll.Click += btnEndAll_Click;
            // 
            // btnScan
            // 
            btnScan.Location = new Point(142, 34);
            btnScan.Name = "btnScan";
            btnScan.Size = new Size(75, 23);
            btnScan.TabIndex = 0;
            btnScan.Text = "Scan";
            btnScan.UseVisualStyleBackColor = true;
            btnScan.Click += btnScan_Click;
            // 
            // grbLaunchProcess
            // 
            grbLaunchProcess.Controls.Add(button1);
            grbLaunchProcess.Controls.Add(btnBrowse);
            grbLaunchProcess.Controls.Add(txbPathToExecutable);
            grbLaunchProcess.Location = new Point(12, 104);
            grbLaunchProcess.Name = "grbLaunchProcess";
            grbLaunchProcess.Size = new Size(704, 121);
            grbLaunchProcess.TabIndex = 1;
            grbLaunchProcess.TabStop = false;
            grbLaunchProcess.Text = "Launch Process";
            // 
            // button1
            // 
            button1.Location = new Point(357, 83);
            button1.Name = "button1";
            button1.Size = new Size(99, 23);
            button1.TabIndex = 2;
            button1.Text = "Launch";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(224, 83);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(99, 23);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txbPathToExecutable
            // 
            txbPathToExecutable.Location = new Point(31, 32);
            txbPathToExecutable.Multiline = true;
            txbPathToExecutable.Name = "txbPathToExecutable";
            txbPathToExecutable.Size = new Size(643, 45);
            txbPathToExecutable.TabIndex = 0;
            // 
            // ProcessManagerMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 245);
            Controls.Add(grbLaunchProcess);
            Controls.Add(groupBox1);
            Name = "ProcessManagerMainForm";
            Text = "Process Manager";
            groupBox1.ResumeLayout(false);
            grbLaunchProcess.ResumeLayout(false);
            grbLaunchProcess.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnEndAll;
        private Button btnScan;
        private GroupBox grbLaunchProcess;
        private Button btnBrowse;
        private TextBox txbPathToExecutable;
        private Button button1;
        private Label lblCasesFound;
    }
}
