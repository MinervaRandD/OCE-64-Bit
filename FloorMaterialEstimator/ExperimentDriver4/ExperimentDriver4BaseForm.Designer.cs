namespace ExperimentDriver4
{
    partial class ExperimentDriver4BaseForm
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
            this.sssMainForm = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCursorPosn = new System.Windows.Forms.ToolStripStatusLabel();
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txbWinRecY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbWinRecX = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txbCursorPosnY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txbCursorPosnX = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txbPixelOffsetY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txbPixelOffsetX = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txbVisioCoordY = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbVisioCoordX = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbCursorMinusPTSY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbCursorMinusPTSX = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txbPointToScreenY = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txbPointToScreenX = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txbMouseCoordY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbMouseCoordX = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txbYPixelOffset = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txbXPixelOffset = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txbEstCursorY = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txbEstCursorX = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txbEstCursorDeltaY = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txbEstCursorDeltaX = new System.Windows.Forms.TextBox();
            this.sssMainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // sssMainForm
            // 
            this.sssMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssCursorPosn});
            this.sssMainForm.Location = new System.Drawing.Point(0, 1139);
            this.sssMainForm.Name = "sssMainForm";
            this.sssMainForm.Size = new System.Drawing.Size(1784, 22);
            this.sssMainForm.TabIndex = 7;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(94, 17);
            this.toolStripStatusLabel1.Text = "Cursor Position: ";
            // 
            // tssCursorPosn
            // 
            this.tssCursorPosn.AutoSize = false;
            this.tssCursorPosn.Name = "tssCursorPosn";
            this.tssCursorPosn.Size = new System.Drawing.Size(128, 17);
            this.tssCursorPosn.Text = "toolStripStatusLabel1";
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(242, 13);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(1483, 942);
            this.axDrawingControl.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txbWinRecY);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txbWinRecX);
            this.groupBox3.Location = new System.Drawing.Point(20, 214);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 82);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Window Rectangle";
            // 
            // txbWinRecY
            // 
            this.txbWinRecY.Location = new System.Drawing.Point(99, 49);
            this.txbWinRecY.Name = "txbWinRecY";
            this.txbWinRecY.Size = new System.Drawing.Size(69, 20);
            this.txbWinRecY.TabIndex = 12;
            this.txbWinRecY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "X";
            // 
            // txbWinRecX
            // 
            this.txbWinRecX.Location = new System.Drawing.Point(99, 19);
            this.txbWinRecX.Name = "txbWinRecX";
            this.txbWinRecX.Size = new System.Drawing.Size(69, 20);
            this.txbWinRecX.TabIndex = 0;
            this.txbWinRecX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txbCursorPosnY);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txbCursorPosnX);
            this.groupBox4.Location = new System.Drawing.Point(20, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(192, 82);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cursor Position";
            // 
            // txbCursorPosnY
            // 
            this.txbCursorPosnY.Location = new System.Drawing.Point(99, 49);
            this.txbCursorPosnY.Name = "txbCursorPosnY";
            this.txbCursorPosnY.Size = new System.Drawing.Size(69, 20);
            this.txbCursorPosnY.TabIndex = 12;
            this.txbCursorPosnY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "X";
            // 
            // txbCursorPosnX
            // 
            this.txbCursorPosnX.Location = new System.Drawing.Point(99, 19);
            this.txbCursorPosnX.Name = "txbCursorPosnX";
            this.txbCursorPosnX.Size = new System.Drawing.Size(69, 20);
            this.txbCursorPosnX.TabIndex = 0;
            this.txbCursorPosnX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txbPixelOffsetY);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txbPixelOffsetX);
            this.groupBox5.Location = new System.Drawing.Point(20, 120);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(192, 82);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Form Pixel Offset Of Cursor";
            // 
            // txbPixelOffsetY
            // 
            this.txbPixelOffsetY.Location = new System.Drawing.Point(99, 49);
            this.txbPixelOffsetY.Name = "txbPixelOffsetY";
            this.txbPixelOffsetY.Size = new System.Drawing.Size(69, 20);
            this.txbPixelOffsetY.TabIndex = 12;
            this.txbPixelOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "X";
            // 
            // txbPixelOffsetX
            // 
            this.txbPixelOffsetX.Location = new System.Drawing.Point(99, 19);
            this.txbPixelOffsetX.Name = "txbPixelOffsetX";
            this.txbPixelOffsetX.Size = new System.Drawing.Size(69, 20);
            this.txbPixelOffsetX.TabIndex = 0;
            this.txbPixelOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txbVisioCoordY);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.txbVisioCoordX);
            this.groupBox6.Location = new System.Drawing.Point(20, 488);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(192, 82);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Visio Coordinates";
            // 
            // txbVisioCoordY
            // 
            this.txbVisioCoordY.Location = new System.Drawing.Point(99, 49);
            this.txbVisioCoordY.Name = "txbVisioCoordY";
            this.txbVisioCoordY.Size = new System.Drawing.Size(69, 20);
            this.txbVisioCoordY.TabIndex = 12;
            this.txbVisioCoordY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Y";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "X";
            // 
            // txbVisioCoordX
            // 
            this.txbVisioCoordX.Location = new System.Drawing.Point(99, 19);
            this.txbVisioCoordX.Name = "txbVisioCoordX";
            this.txbVisioCoordX.Size = new System.Drawing.Size(69, 20);
            this.txbVisioCoordX.TabIndex = 0;
            this.txbVisioCoordX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbCursorMinusPTSY);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txbCursorMinusPTSX);
            this.groupBox1.Location = new System.Drawing.Point(20, 396);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 82);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cursor - Point To Screen";
            // 
            // txbCursorMinusPTSY
            // 
            this.txbCursorMinusPTSY.Location = new System.Drawing.Point(99, 49);
            this.txbCursorMinusPTSY.Name = "txbCursorMinusPTSY";
            this.txbCursorMinusPTSY.Size = new System.Drawing.Size(69, 20);
            this.txbCursorMinusPTSY.TabIndex = 12;
            this.txbCursorMinusPTSY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "X";
            // 
            // txbCursorMinusPTSX
            // 
            this.txbCursorMinusPTSX.Location = new System.Drawing.Point(99, 19);
            this.txbCursorMinusPTSX.Name = "txbCursorMinusPTSX";
            this.txbCursorMinusPTSX.Size = new System.Drawing.Size(69, 20);
            this.txbCursorMinusPTSX.TabIndex = 0;
            this.txbCursorMinusPTSX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txbPointToScreenY);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.txbPointToScreenX);
            this.groupBox7.Location = new System.Drawing.Point(20, 305);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(192, 82);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Point To Screen";
            // 
            // txbPointToScreenY
            // 
            this.txbPointToScreenY.Location = new System.Drawing.Point(99, 49);
            this.txbPointToScreenY.Name = "txbPointToScreenY";
            this.txbPointToScreenY.Size = new System.Drawing.Size(69, 20);
            this.txbPointToScreenY.TabIndex = 12;
            this.txbPointToScreenY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Y";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(32, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "X";
            // 
            // txbPointToScreenX
            // 
            this.txbPointToScreenX.Location = new System.Drawing.Point(99, 19);
            this.txbPointToScreenX.Name = "txbPointToScreenX";
            this.txbPointToScreenX.Size = new System.Drawing.Size(69, 20);
            this.txbPointToScreenX.TabIndex = 0;
            this.txbPointToScreenX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txbMouseCoordY);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txbMouseCoordX);
            this.groupBox2.Location = new System.Drawing.Point(20, 583);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 82);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mouse Coordinates";
            // 
            // txbMouseCoordY
            // 
            this.txbMouseCoordY.Location = new System.Drawing.Point(99, 49);
            this.txbMouseCoordY.Name = "txbMouseCoordY";
            this.txbMouseCoordY.Size = new System.Drawing.Size(69, 20);
            this.txbMouseCoordY.TabIndex = 12;
            this.txbMouseCoordY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "X";
            // 
            // txbMouseCoordX
            // 
            this.txbMouseCoordX.Location = new System.Drawing.Point(99, 19);
            this.txbMouseCoordX.Name = "txbMouseCoordX";
            this.txbMouseCoordX.Size = new System.Drawing.Size(69, 20);
            this.txbMouseCoordX.TabIndex = 0;
            this.txbMouseCoordX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txbYPixelOffset);
            this.groupBox8.Controls.Add(this.label15);
            this.groupBox8.Controls.Add(this.label16);
            this.groupBox8.Controls.Add(this.txbXPixelOffset);
            this.groupBox8.Location = new System.Drawing.Point(20, 674);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(192, 82);
            this.groupBox8.TabIndex = 17;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Offset";
            // 
            // txbYPixelOffset
            // 
            this.txbYPixelOffset.Location = new System.Drawing.Point(99, 49);
            this.txbYPixelOffset.Name = "txbYPixelOffset";
            this.txbYPixelOffset.Size = new System.Drawing.Size(69, 20);
            this.txbYPixelOffset.TabIndex = 12;
            this.txbYPixelOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Y Pixel Offset";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "X Pixel Offset";
            // 
            // txbXPixelOffset
            // 
            this.txbXPixelOffset.Location = new System.Drawing.Point(99, 19);
            this.txbXPixelOffset.Name = "txbXPixelOffset";
            this.txbXPixelOffset.Size = new System.Drawing.Size(69, 20);
            this.txbXPixelOffset.TabIndex = 0;
            this.txbXPixelOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txbEstCursorY);
            this.groupBox9.Controls.Add(this.label17);
            this.groupBox9.Controls.Add(this.label18);
            this.groupBox9.Controls.Add(this.txbEstCursorX);
            this.groupBox9.Location = new System.Drawing.Point(20, 775);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(192, 82);
            this.groupBox9.TabIndex = 18;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Estimated Cursor Position";
            // 
            // txbEstCursorY
            // 
            this.txbEstCursorY.Location = new System.Drawing.Point(99, 49);
            this.txbEstCursorY.Name = "txbEstCursorY";
            this.txbEstCursorY.Size = new System.Drawing.Size(69, 20);
            this.txbEstCursorY.TabIndex = 12;
            this.txbEstCursorY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(31, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(31, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "X";
            // 
            // txbEstCursorX
            // 
            this.txbEstCursorX.Location = new System.Drawing.Point(99, 19);
            this.txbEstCursorX.Name = "txbEstCursorX";
            this.txbEstCursorX.Size = new System.Drawing.Size(69, 20);
            this.txbEstCursorX.TabIndex = 0;
            this.txbEstCursorX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txbEstCursorDeltaY);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Controls.Add(this.txbEstCursorDeltaX);
            this.groupBox10.Location = new System.Drawing.Point(20, 878);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(192, 82);
            this.groupBox10.TabIndex = 19;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Estimated Delta";
            // 
            // txbEstCursorDeltaY
            // 
            this.txbEstCursorDeltaY.Location = new System.Drawing.Point(99, 49);
            this.txbEstCursorDeltaY.Name = "txbEstCursorDeltaY";
            this.txbEstCursorDeltaY.Size = new System.Drawing.Size(69, 20);
            this.txbEstCursorDeltaY.TabIndex = 12;
            this.txbEstCursorDeltaY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(31, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 13);
            this.label19.TabIndex = 11;
            this.label19.Text = "Y";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(31, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "X";
            // 
            // txbEstCursorDeltaX
            // 
            this.txbEstCursorDeltaX.Location = new System.Drawing.Point(99, 19);
            this.txbEstCursorDeltaX.Name = "txbEstCursorDeltaX";
            this.txbEstCursorDeltaX.Size = new System.Drawing.Size(69, 20);
            this.txbEstCursorDeltaX.TabIndex = 0;
            this.txbEstCursorDeltaX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ExperimentDriver4BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1784, 1161);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.sssMainForm);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "ExperimentDriver4BaseForm";
            this.Text = "Test Driver 4";
            this.sssMainForm.ResumeLayout(false);
            this.sssMainForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private System.Windows.Forms.StatusStrip sssMainForm;
        private System.Windows.Forms.ToolStripStatusLabel tssCursorPosn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txbWinRecY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbWinRecX;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txbCursorPosnY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbCursorPosnX;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txbPixelOffsetY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txbPixelOffsetX;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txbVisioCoordY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbVisioCoordX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbCursorMinusPTSY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbCursorMinusPTSX;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txbPointToScreenY;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txbPointToScreenX;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txbMouseCoordY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbMouseCoordX;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txbYPixelOffset;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txbXPixelOffset;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txbEstCursorY;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txbEstCursorX;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txbEstCursorDeltaY;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txbEstCursorDeltaX;
    }
}

