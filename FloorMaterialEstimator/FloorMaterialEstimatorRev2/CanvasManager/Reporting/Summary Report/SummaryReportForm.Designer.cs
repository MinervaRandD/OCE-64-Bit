namespace CanvasManager.Reports.SummaryReport
{
    partial class SummaryReportForm
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
            this.pnlAreasReportTable = new System.Windows.Forms.Panel();
            this.grbArea = new System.Windows.Forms.GroupBox();
            this.btnExpandAreas = new System.Windows.Forms.Button();
            this.grpAreaSelected = new System.Windows.Forms.GroupBox();
            this.lblAreaSelGrossField = new System.Windows.Forms.Label();
            this.lblAreaSelGross = new System.Windows.Forms.Label();
            this.lblAreaSelWasteField = new System.Windows.Forms.Label();
            this.lblAreaSelNet = new System.Windows.Forms.Label();
            this.lblAreaSelNetField = new System.Windows.Forms.Label();
            this.lblAreaSelWaste = new System.Windows.Forms.Label();
            this.grpAreaTotal = new System.Windows.Forms.GroupBox();
            this.lblAreaTotalGrossField = new System.Windows.Forms.Label();
            this.lblAreaTotalGross = new System.Windows.Forms.Label();
            this.lblAreaTotalWasteField = new System.Windows.Forms.Label();
            this.lblAreaTotalNet = new System.Windows.Forms.Label();
            this.lblAreaTotalNetField = new System.Windows.Forms.Label();
            this.lblAreaTotalWaste = new System.Windows.Forms.Label();
            this.pnlLinesReportTable = new System.Windows.Forms.Panel();
            this.grpLines = new System.Windows.Forms.GroupBox();
            this.btnExpandLines = new System.Windows.Forms.Button();
            this.lblLinesSelField = new System.Windows.Forms.Label();
            this.lblLinesTotal = new System.Windows.Forms.Label();
            this.lblLinesTotalField = new System.Windows.Forms.Label();
            this.lblLinesSel = new System.Windows.Forms.Label();
            this.grpSeams = new System.Windows.Forms.GroupBox();
            this.btnExpandSeams = new System.Windows.Forms.Button();
            this.pnlSeamsReportTable = new System.Windows.Forms.Panel();
            this.lblSeamsSelField = new System.Windows.Forms.Label();
            this.lblSeamsTotal = new System.Windows.Forms.Label();
            this.lblSeamsTotalField = new System.Windows.Forms.Label();
            this.lblSeamsSel = new System.Windows.Forms.Label();
            this.ckbHideZeroQuanity = new System.Windows.Forms.CheckBox();
            this.grpCounters = new System.Windows.Forms.GroupBox();
            this.btnExpandCntrs = new System.Windows.Forms.Button();
            this.ckbCounterShowSelected = new System.Windows.Forms.CheckBox();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblCounterTotal = new System.Windows.Forms.Label();
            this.pnlCntrsReportTable = new System.Windows.Forms.Panel();
            this.lblCountTitle = new System.Windows.Forms.Label();
            this.lblCounterCount = new System.Windows.Forms.Label();
            this.grbArea.SuspendLayout();
            this.grpAreaSelected.SuspendLayout();
            this.grpAreaTotal.SuspendLayout();
            this.grpLines.SuspendLayout();
            this.grpSeams.SuspendLayout();
            this.grpCounters.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAreasReportTable
            // 
            this.pnlAreasReportTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlAreasReportTable.AutoScroll = true;
            this.pnlAreasReportTable.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.pnlAreasReportTable.AutoScrollMinSize = new System.Drawing.Size(900, 600);
            this.pnlAreasReportTable.Location = new System.Drawing.Point(15, 141);
            this.pnlAreasReportTable.Name = "pnlAreasReportTable";
            this.pnlAreasReportTable.Size = new System.Drawing.Size(1073, 200);
            this.pnlAreasReportTable.TabIndex = 3;
            // 
            // grbArea
            // 
            this.grbArea.Controls.Add(this.btnExpandAreas);
            this.grbArea.Controls.Add(this.grpAreaSelected);
            this.grbArea.Controls.Add(this.grpAreaTotal);
            this.grbArea.Controls.Add(this.pnlAreasReportTable);
            this.grbArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbArea.Location = new System.Drawing.Point(14, 1);
            this.grbArea.Name = "grbArea";
            this.grbArea.Size = new System.Drawing.Size(1100, 347);
            this.grbArea.TabIndex = 6;
            this.grbArea.TabStop = false;
            this.grbArea.Text = "Areas";
            // 
            // btnExpandAreas
            // 
            this.btnExpandAreas.Location = new System.Drawing.Point(1059, 11);
            this.btnExpandAreas.Name = "btnExpandAreas";
            this.btnExpandAreas.Size = new System.Drawing.Size(24, 23);
            this.btnExpandAreas.TabIndex = 15;
            this.btnExpandAreas.Text = "-";
            this.btnExpandAreas.UseVisualStyleBackColor = true;
            this.btnExpandAreas.Click += new System.EventHandler(this.btnExpandAreas_Click);
            // 
            // grpAreaSelected
            // 
            this.grpAreaSelected.Controls.Add(this.lblAreaSelGrossField);
            this.grpAreaSelected.Controls.Add(this.lblAreaSelGross);
            this.grpAreaSelected.Controls.Add(this.lblAreaSelWasteField);
            this.grpAreaSelected.Controls.Add(this.lblAreaSelNet);
            this.grpAreaSelected.Controls.Add(this.lblAreaSelNetField);
            this.grpAreaSelected.Controls.Add(this.lblAreaSelWaste);
            this.grpAreaSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAreaSelected.Location = new System.Drawing.Point(553, 47);
            this.grpAreaSelected.Name = "grpAreaSelected";
            this.grpAreaSelected.Size = new System.Drawing.Size(535, 65);
            this.grpAreaSelected.TabIndex = 14;
            this.grpAreaSelected.TabStop = false;
            this.grpAreaSelected.Text = "Selected";
            // 
            // lblAreaSelGrossField
            // 
            this.lblAreaSelGrossField.BackColor = System.Drawing.Color.LightGray;
            this.lblAreaSelGrossField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaSelGrossField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAreaSelGrossField.Location = new System.Drawing.Point(62, 26);
            this.lblAreaSelGrossField.Name = "lblAreaSelGrossField";
            this.lblAreaSelGrossField.Size = new System.Drawing.Size(108, 23);
            this.lblAreaSelGrossField.TabIndex = 10;
            this.lblAreaSelGrossField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaSelGross
            // 
            this.lblAreaSelGross.AutoSize = true;
            this.lblAreaSelGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaSelGross.Location = new System.Drawing.Point(6, 28);
            this.lblAreaSelGross.Name = "lblAreaSelGross";
            this.lblAreaSelGross.Size = new System.Drawing.Size(50, 18);
            this.lblAreaSelGross.TabIndex = 7;
            this.lblAreaSelGross.Text = "Gross";
            // 
            // lblAreaSelWasteField
            // 
            this.lblAreaSelWasteField.BackColor = System.Drawing.Color.LightGray;
            this.lblAreaSelWasteField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaSelWasteField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAreaSelWasteField.Location = new System.Drawing.Point(409, 26);
            this.lblAreaSelWasteField.Name = "lblAreaSelWasteField";
            this.lblAreaSelWasteField.Size = new System.Drawing.Size(108, 23);
            this.lblAreaSelWasteField.TabIndex = 12;
            this.lblAreaSelWasteField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaSelNet
            // 
            this.lblAreaSelNet.AutoSize = true;
            this.lblAreaSelNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaSelNet.Location = new System.Drawing.Point(176, 31);
            this.lblAreaSelNet.Name = "lblAreaSelNet";
            this.lblAreaSelNet.Size = new System.Drawing.Size(31, 18);
            this.lblAreaSelNet.TabIndex = 8;
            this.lblAreaSelNet.Text = "Net";
            // 
            // lblAreaSelNetField
            // 
            this.lblAreaSelNetField.BackColor = System.Drawing.Color.LightGray;
            this.lblAreaSelNetField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaSelNetField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAreaSelNetField.Location = new System.Drawing.Point(219, 26);
            this.lblAreaSelNetField.Name = "lblAreaSelNetField";
            this.lblAreaSelNetField.Size = new System.Drawing.Size(108, 23);
            this.lblAreaSelNetField.TabIndex = 11;
            this.lblAreaSelNetField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaSelWaste
            // 
            this.lblAreaSelWaste.AutoSize = true;
            this.lblAreaSelWaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaSelWaste.Location = new System.Drawing.Point(337, 28);
            this.lblAreaSelWaste.Name = "lblAreaSelWaste";
            this.lblAreaSelWaste.Size = new System.Drawing.Size(68, 18);
            this.lblAreaSelWaste.TabIndex = 9;
            this.lblAreaSelWaste.Text = "Waste %";
            // 
            // grpAreaTotal
            // 
            this.grpAreaTotal.Controls.Add(this.lblAreaTotalGrossField);
            this.grpAreaTotal.Controls.Add(this.lblAreaTotalGross);
            this.grpAreaTotal.Controls.Add(this.lblAreaTotalWasteField);
            this.grpAreaTotal.Controls.Add(this.lblAreaTotalNet);
            this.grpAreaTotal.Controls.Add(this.lblAreaTotalNetField);
            this.grpAreaTotal.Controls.Add(this.lblAreaTotalWaste);
            this.grpAreaTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAreaTotal.Location = new System.Drawing.Point(12, 47);
            this.grpAreaTotal.Name = "grpAreaTotal";
            this.grpAreaTotal.Size = new System.Drawing.Size(535, 65);
            this.grpAreaTotal.TabIndex = 13;
            this.grpAreaTotal.TabStop = false;
            this.grpAreaTotal.Text = "Total";
            // 
            // lblAreaTotalGrossField
            // 
            this.lblAreaTotalGrossField.BackColor = System.Drawing.Color.LightGray;
            this.lblAreaTotalGrossField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaTotalGrossField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAreaTotalGrossField.Location = new System.Drawing.Point(62, 26);
            this.lblAreaTotalGrossField.Name = "lblAreaTotalGrossField";
            this.lblAreaTotalGrossField.Size = new System.Drawing.Size(108, 23);
            this.lblAreaTotalGrossField.TabIndex = 10;
            this.lblAreaTotalGrossField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaTotalGross
            // 
            this.lblAreaTotalGross.AutoSize = true;
            this.lblAreaTotalGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaTotalGross.Location = new System.Drawing.Point(6, 28);
            this.lblAreaTotalGross.Name = "lblAreaTotalGross";
            this.lblAreaTotalGross.Size = new System.Drawing.Size(50, 18);
            this.lblAreaTotalGross.TabIndex = 7;
            this.lblAreaTotalGross.Text = "Gross";
            // 
            // lblAreaTotalWasteField
            // 
            this.lblAreaTotalWasteField.BackColor = System.Drawing.Color.LightGray;
            this.lblAreaTotalWasteField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaTotalWasteField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAreaTotalWasteField.Location = new System.Drawing.Point(409, 26);
            this.lblAreaTotalWasteField.Name = "lblAreaTotalWasteField";
            this.lblAreaTotalWasteField.Size = new System.Drawing.Size(108, 23);
            this.lblAreaTotalWasteField.TabIndex = 12;
            this.lblAreaTotalWasteField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaTotalNet
            // 
            this.lblAreaTotalNet.AutoSize = true;
            this.lblAreaTotalNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaTotalNet.Location = new System.Drawing.Point(182, 28);
            this.lblAreaTotalNet.Name = "lblAreaTotalNet";
            this.lblAreaTotalNet.Size = new System.Drawing.Size(31, 18);
            this.lblAreaTotalNet.TabIndex = 8;
            this.lblAreaTotalNet.Text = "Net";
            // 
            // lblAreaTotalNetField
            // 
            this.lblAreaTotalNetField.BackColor = System.Drawing.Color.LightGray;
            this.lblAreaTotalNetField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaTotalNetField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAreaTotalNetField.Location = new System.Drawing.Point(219, 26);
            this.lblAreaTotalNetField.Name = "lblAreaTotalNetField";
            this.lblAreaTotalNetField.Size = new System.Drawing.Size(108, 23);
            this.lblAreaTotalNetField.TabIndex = 11;
            this.lblAreaTotalNetField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAreaTotalWaste
            // 
            this.lblAreaTotalWaste.AutoSize = true;
            this.lblAreaTotalWaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaTotalWaste.Location = new System.Drawing.Point(337, 28);
            this.lblAreaTotalWaste.Name = "lblAreaTotalWaste";
            this.lblAreaTotalWaste.Size = new System.Drawing.Size(68, 18);
            this.lblAreaTotalWaste.TabIndex = 9;
            this.lblAreaTotalWaste.Text = "Waste %";
            // 
            // pnlLinesReportTable
            // 
            this.pnlLinesReportTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLinesReportTable.AutoScroll = true;
            this.pnlLinesReportTable.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.pnlLinesReportTable.AutoScrollMinSize = new System.Drawing.Size(900, 600);
            this.pnlLinesReportTable.Location = new System.Drawing.Point(15, 90);
            this.pnlLinesReportTable.Name = "pnlLinesReportTable";
            this.pnlLinesReportTable.Size = new System.Drawing.Size(1073, 206);
            this.pnlLinesReportTable.TabIndex = 13;
            // 
            // grpLines
            // 
            this.grpLines.Controls.Add(this.btnExpandLines);
            this.grpLines.Controls.Add(this.pnlLinesReportTable);
            this.grpLines.Controls.Add(this.lblLinesSelField);
            this.grpLines.Controls.Add(this.lblLinesTotal);
            this.grpLines.Controls.Add(this.lblLinesTotalField);
            this.grpLines.Controls.Add(this.lblLinesSel);
            this.grpLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLines.Location = new System.Drawing.Point(14, 353);
            this.grpLines.Name = "grpLines";
            this.grpLines.Size = new System.Drawing.Size(1100, 306);
            this.grpLines.TabIndex = 15;
            this.grpLines.TabStop = false;
            this.grpLines.Text = "Lines";
            // 
            // btnExpandLines
            // 
            this.btnExpandLines.Location = new System.Drawing.Point(1059, 13);
            this.btnExpandLines.Name = "btnExpandLines";
            this.btnExpandLines.Size = new System.Drawing.Size(24, 23);
            this.btnExpandLines.TabIndex = 16;
            this.btnExpandLines.Text = "-";
            this.btnExpandLines.UseVisualStyleBackColor = true;
            this.btnExpandLines.Click += new System.EventHandler(this.btnExpandLines_Click);
            // 
            // lblLinesSelField
            // 
            this.lblLinesSelField.BackColor = System.Drawing.Color.LightGray;
            this.lblLinesSelField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinesSelField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLinesSelField.Location = new System.Drawing.Point(345, 42);
            this.lblLinesSelField.Name = "lblLinesSelField";
            this.lblLinesSelField.Size = new System.Drawing.Size(108, 23);
            this.lblLinesSelField.TabIndex = 10;
            this.lblLinesSelField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLinesTotal
            // 
            this.lblLinesTotal.AutoSize = true;
            this.lblLinesTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinesTotal.Location = new System.Drawing.Point(9, 44);
            this.lblLinesTotal.Name = "lblLinesTotal";
            this.lblLinesTotal.Size = new System.Drawing.Size(41, 18);
            this.lblLinesTotal.TabIndex = 5;
            this.lblLinesTotal.Text = "Total";
            // 
            // lblLinesTotalField
            // 
            this.lblLinesTotalField.BackColor = System.Drawing.Color.LightGray;
            this.lblLinesTotalField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinesTotalField.Location = new System.Drawing.Point(78, 42);
            this.lblLinesTotalField.Name = "lblLinesTotalField";
            this.lblLinesTotalField.Size = new System.Drawing.Size(108, 23);
            this.lblLinesTotalField.TabIndex = 4;
            this.lblLinesTotalField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLinesSel
            // 
            this.lblLinesSel.AutoSize = true;
            this.lblLinesSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinesSel.Location = new System.Drawing.Point(230, 44);
            this.lblLinesSel.Name = "lblLinesSel";
            this.lblLinesSel.Size = new System.Drawing.Size(65, 18);
            this.lblLinesSel.TabIndex = 7;
            this.lblLinesSel.Text = "Selected";
            // 
            // grpSeams
            // 
            this.grpSeams.Controls.Add(this.btnExpandSeams);
            this.grpSeams.Controls.Add(this.pnlSeamsReportTable);
            this.grpSeams.Controls.Add(this.lblSeamsSelField);
            this.grpSeams.Controls.Add(this.lblSeamsTotal);
            this.grpSeams.Controls.Add(this.lblSeamsTotalField);
            this.grpSeams.Controls.Add(this.lblSeamsSel);
            this.grpSeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSeams.Location = new System.Drawing.Point(14, 1001);
            this.grpSeams.Name = "grpSeams";
            this.grpSeams.Size = new System.Drawing.Size(1100, 241);
            this.grpSeams.TabIndex = 16;
            this.grpSeams.TabStop = false;
            this.grpSeams.Text = "Seams";
            // 
            // btnExpandSeams
            // 
            this.btnExpandSeams.Location = new System.Drawing.Point(1059, 15);
            this.btnExpandSeams.Name = "btnExpandSeams";
            this.btnExpandSeams.Size = new System.Drawing.Size(24, 23);
            this.btnExpandSeams.TabIndex = 18;
            this.btnExpandSeams.Text = "-";
            this.btnExpandSeams.UseVisualStyleBackColor = true;
            this.btnExpandSeams.Click += new System.EventHandler(this.btnExpandSeams_Click);
            // 
            // pnlSeamsReportTable
            // 
            this.pnlSeamsReportTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlSeamsReportTable.AutoScroll = true;
            this.pnlSeamsReportTable.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.pnlSeamsReportTable.AutoScrollMinSize = new System.Drawing.Size(900, 600);
            this.pnlSeamsReportTable.Location = new System.Drawing.Point(15, 85);
            this.pnlSeamsReportTable.Name = "pnlSeamsReportTable";
            this.pnlSeamsReportTable.Size = new System.Drawing.Size(1073, 147);
            this.pnlSeamsReportTable.TabIndex = 13;
            // 
            // lblSeamsSelField
            // 
            this.lblSeamsSelField.BackColor = System.Drawing.Color.LightGray;
            this.lblSeamsSelField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamsSelField.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSeamsSelField.Location = new System.Drawing.Point(345, 43);
            this.lblSeamsSelField.Name = "lblSeamsSelField";
            this.lblSeamsSelField.Size = new System.Drawing.Size(108, 23);
            this.lblSeamsSelField.TabIndex = 10;
            this.lblSeamsSelField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSeamsTotal
            // 
            this.lblSeamsTotal.AutoSize = true;
            this.lblSeamsTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamsTotal.Location = new System.Drawing.Point(9, 45);
            this.lblSeamsTotal.Name = "lblSeamsTotal";
            this.lblSeamsTotal.Size = new System.Drawing.Size(41, 18);
            this.lblSeamsTotal.TabIndex = 5;
            this.lblSeamsTotal.Text = "Total";
            // 
            // lblSeamsTotalField
            // 
            this.lblSeamsTotalField.BackColor = System.Drawing.Color.LightGray;
            this.lblSeamsTotalField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamsTotalField.Location = new System.Drawing.Point(78, 43);
            this.lblSeamsTotalField.Name = "lblSeamsTotalField";
            this.lblSeamsTotalField.Size = new System.Drawing.Size(108, 23);
            this.lblSeamsTotalField.TabIndex = 4;
            this.lblSeamsTotalField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSeamsSel
            // 
            this.lblSeamsSel.AutoSize = true;
            this.lblSeamsSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeamsSel.Location = new System.Drawing.Point(230, 45);
            this.lblSeamsSel.Name = "lblSeamsSel";
            this.lblSeamsSel.Size = new System.Drawing.Size(65, 18);
            this.lblSeamsSel.TabIndex = 7;
            this.lblSeamsSel.Text = "Selected";
            // 
            // ckbHideZeroQuanity
            // 
            this.ckbHideZeroQuanity.AutoSize = true;
            this.ckbHideZeroQuanity.Location = new System.Drawing.Point(14, 1278);
            this.ckbHideZeroQuanity.Name = "ckbHideZeroQuanity";
            this.ckbHideZeroQuanity.Size = new System.Drawing.Size(219, 24);
            this.ckbHideZeroQuanity.TabIndex = 17;
            this.ckbHideZeroQuanity.Text = "Hide zero quanity materials";
            this.ckbHideZeroQuanity.UseVisualStyleBackColor = true;
            this.ckbHideZeroQuanity.CheckedChanged += new System.EventHandler(this.ckbHideZeroQuanity_CheckedChanged);
            // 
            // grpCounters
            // 
            this.grpCounters.Controls.Add(this.btnExpandCntrs);
            this.grpCounters.Controls.Add(this.ckbCounterShowSelected);
            this.grpCounters.Controls.Add(this.lblTotalTitle);
            this.grpCounters.Controls.Add(this.lblCounterTotal);
            this.grpCounters.Controls.Add(this.pnlCntrsReportTable);
            this.grpCounters.Controls.Add(this.lblCountTitle);
            this.grpCounters.Controls.Add(this.lblCounterCount);
            this.grpCounters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCounters.Location = new System.Drawing.Point(14, 664);
            this.grpCounters.Name = "grpCounters";
            this.grpCounters.Size = new System.Drawing.Size(1100, 306);
            this.grpCounters.TabIndex = 18;
            this.grpCounters.TabStop = false;
            this.grpCounters.Text = "Counters";
            // 
            // btnExpandCntrs
            // 
            this.btnExpandCntrs.Location = new System.Drawing.Point(1059, 15);
            this.btnExpandCntrs.Name = "btnExpandCntrs";
            this.btnExpandCntrs.Size = new System.Drawing.Size(24, 23);
            this.btnExpandCntrs.TabIndex = 17;
            this.btnExpandCntrs.Text = "-";
            this.btnExpandCntrs.UseVisualStyleBackColor = true;
            this.btnExpandCntrs.Click += new System.EventHandler(this.btnExpandCntrs_Click);
            // 
            // ckbCounterShowSelected
            // 
            this.ckbCounterShowSelected.AutoSize = true;
            this.ckbCounterShowSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCounterShowSelected.Location = new System.Drawing.Point(326, 41);
            this.ckbCounterShowSelected.Name = "ckbCounterShowSelected";
            this.ckbCounterShowSelected.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbCounterShowSelected.Size = new System.Drawing.Size(118, 22);
            this.ckbCounterShowSelected.TabIndex = 16;
            this.ckbCounterShowSelected.Text = "Selected Only";
            this.ckbCounterShowSelected.UseVisualStyleBackColor = true;
            this.ckbCounterShowSelected.CheckedChanged += new System.EventHandler(this.ckbCounterShowSelected_CheckedChanged);
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTitle.Location = new System.Drawing.Point(162, 41);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(41, 18);
            this.lblTotalTitle.TabIndex = 15;
            this.lblTotalTitle.Text = "Total";
            // 
            // lblCounterTotal
            // 
            this.lblCounterTotal.BackColor = System.Drawing.Color.LightGray;
            this.lblCounterTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounterTotal.Location = new System.Drawing.Point(231, 39);
            this.lblCounterTotal.Name = "lblCounterTotal";
            this.lblCounterTotal.Size = new System.Drawing.Size(54, 23);
            this.lblCounterTotal.TabIndex = 14;
            this.lblCounterTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCntrsReportTable
            // 
            this.pnlCntrsReportTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCntrsReportTable.AutoScroll = true;
            this.pnlCntrsReportTable.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.pnlCntrsReportTable.AutoScrollMinSize = new System.Drawing.Size(900, 600);
            this.pnlCntrsReportTable.Location = new System.Drawing.Point(15, 90);
            this.pnlCntrsReportTable.Name = "pnlCntrsReportTable";
            this.pnlCntrsReportTable.Size = new System.Drawing.Size(1073, 206);
            this.pnlCntrsReportTable.TabIndex = 13;
            // 
            // lblCountTitle
            // 
            this.lblCountTitle.AutoSize = true;
            this.lblCountTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountTitle.Location = new System.Drawing.Point(9, 41);
            this.lblCountTitle.Name = "lblCountTitle";
            this.lblCountTitle.Size = new System.Drawing.Size(48, 18);
            this.lblCountTitle.TabIndex = 5;
            this.lblCountTitle.Text = "Count";
            // 
            // lblCounterCount
            // 
            this.lblCounterCount.BackColor = System.Drawing.Color.LightGray;
            this.lblCounterCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounterCount.Location = new System.Drawing.Point(78, 39);
            this.lblCounterCount.Name = "lblCounterCount";
            this.lblCounterCount.Size = new System.Drawing.Size(54, 23);
            this.lblCounterCount.TabIndex = 4;
            this.lblCounterCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SummaryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1165, 1329);
            this.Controls.Add(this.grpCounters);
            this.Controls.Add(this.ckbHideZeroQuanity);
            this.Controls.Add(this.grpSeams);
            this.Controls.Add(this.grpLines);
            this.Controls.Add(this.grbArea);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SummaryReportForm";
            this.Text = "Summary Report";
            this.grbArea.ResumeLayout(false);
            this.grpAreaSelected.ResumeLayout(false);
            this.grpAreaSelected.PerformLayout();
            this.grpAreaTotal.ResumeLayout(false);
            this.grpAreaTotal.PerformLayout();
            this.grpLines.ResumeLayout(false);
            this.grpLines.PerformLayout();
            this.grpSeams.ResumeLayout(false);
            this.grpSeams.PerformLayout();
            this.grpCounters.ResumeLayout(false);
            this.grpCounters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlAreasReportTable;
        private System.Windows.Forms.GroupBox grbArea;
        private System.Windows.Forms.Label lblAreaTotalWasteField;
        private System.Windows.Forms.Label lblAreaTotalNetField;
        private System.Windows.Forms.Label lblAreaTotalGrossField;
        private System.Windows.Forms.Label lblAreaTotalWaste;
        private System.Windows.Forms.Label lblAreaTotalNet;
        private System.Windows.Forms.Label lblAreaTotalGross;
        private System.Windows.Forms.Panel pnlLinesReportTable;
        private System.Windows.Forms.GroupBox grpLines;
        private System.Windows.Forms.Label lblLinesSelField;
        private System.Windows.Forms.Label lblLinesTotal;
        private System.Windows.Forms.Label lblLinesTotalField;
        private System.Windows.Forms.Label lblLinesSel;
        private System.Windows.Forms.GroupBox grpAreaTotal;
        private System.Windows.Forms.GroupBox grpAreaSelected;
        private System.Windows.Forms.Label lblAreaSelGrossField;
        private System.Windows.Forms.Label lblAreaSelGross;
        private System.Windows.Forms.Label lblAreaSelWasteField;
        private System.Windows.Forms.Label lblAreaSelNet;
        private System.Windows.Forms.Label lblAreaSelNetField;
        private System.Windows.Forms.Label lblAreaSelWaste;
        private System.Windows.Forms.GroupBox grpSeams;
        private System.Windows.Forms.Panel pnlSeamsReportTable;
        private System.Windows.Forms.Label lblSeamsSelField;
        private System.Windows.Forms.Label lblSeamsTotal;
        private System.Windows.Forms.Label lblSeamsTotalField;
        private System.Windows.Forms.Label lblSeamsSel;
        private System.Windows.Forms.CheckBox ckbHideZeroQuanity;
        private System.Windows.Forms.GroupBox grpCounters;
        private System.Windows.Forms.Panel pnlCntrsReportTable;
        private System.Windows.Forms.Label lblCountTitle;
        private System.Windows.Forms.Label lblCounterCount;
        private System.Windows.Forms.CheckBox ckbCounterShowSelected;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblCounterTotal;
        private System.Windows.Forms.Button btnExpandAreas;
        private System.Windows.Forms.Button btnExpandLines;
        private System.Windows.Forms.Button btnExpandCntrs;
        private System.Windows.Forms.Button btnExpandSeams;
    }
}