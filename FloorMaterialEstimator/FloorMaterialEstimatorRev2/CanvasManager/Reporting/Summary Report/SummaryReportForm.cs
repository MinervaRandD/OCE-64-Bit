

namespace CanvasManager.Reports.SummaryReport
{
    using System;
    using FinishesLib;

    using Utilities;

    using System.Windows.Forms;
    using System.IO;
    using FloorMaterialEstimator;
    using FloorMaterialEstimator.CanvasManager.Reporting.Summary_Report;
    using CanvasLib.Counters;
    using System.Collections.Generic;
    using System.Drawing;

    public partial class SummaryReportForm : Form, ICursorManagementForm, IDisposable
    {
        private const string defaultTitleText = "Summary Report";
        private const string setScaleText = " - Set Scale to see Report";

        private AreaSectionListController areaSectionListController;
        private LineSectionListController lineSectionListController;
        private SeamSectionListController seamSectionListController;
        private CntrSectionListController cntrSectionListController;

        private FloorMaterialEstimatorBaseForm baseForm;

        private bool scaleHasBeenSet = false;

        private int areasGrbOrigHght;

        private int linesGrbOrigHght;

        private int seamsGrbOrigHght;

        private int cntrsGrbOrigHght;

        public SummaryReportForm()
        {
            InitializeComponent();

            areasGrbOrigHght = this.grbArea.Height;
            linesGrbOrigHght = this.grpLines.Height;
            seamsGrbOrigHght = this.grpSeams.Height;
            cntrsGrbOrigHght = this.grpCounters.Height;

            this.FormClosed += AreaFilterForm_FormClosed;

            AddToCursorManagementList();

            this.FormClosed += FinishesEditForm_FormClosed;

            this.Disposed += SummaryReportForm_Disposed;

            this.pnlAreasReportTable.AutoScroll = true;
        }


        private void AreaFilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SummaryReportForm_Disposed(null, null);
        }

        private void SummaryReportForm_Disposed(object sender, EventArgs e)
        {
            this.areaSectionListController.Close();
            this.lineSectionListController.Close();
            this.seamSectionListController.Close();
            
            CanvasLib.Scale_Line.ScaleRuleController.SetScaleCompleted -= ScaleRuleController_SetScaleCompleted;

            this.areaSectionListController.TotalsChanged -= AreaSectionListController_TotalsChanged;
            this.lineSectionListController.TotalsChanged -= LineSectionListController_TotalsChanged;
            this.seamSectionListController.TotalsChanged -= SeamSectionListController_TotalsChanged;
            this.cntrSectionListController.CountOrTotalChanged -= CntrSectionListController_CountOrTotalChanged;

        }

        public void Init(
             FloorMaterialEstimatorBaseForm baseForm
            ,ToolStripButton btnSummaryReport
            ,AreaFinishBaseList areaFinishBaseList
            ,LineFinishBaseList lineFinishBaseList
            ,SeamFinishBaseList seamFinishBaseList
            ,CounterList counterList
            ,bool scaleHasBeenSet)
        {
            this.baseForm = baseForm;

            //this.btnSummaryReport = btnSummaryReport;

            //this.scaleHasBeenSet = scaleHasBeenSet; // note for now we are assuming that the scale has beens set when the report is being run

            this.areaSectionListController = new AreaSectionListController(pnlAreasReportTable, new TitleRow(), areaFinishBaseList);
            this.areaSectionListController.TotalsChanged += AreaSectionListController_TotalsChanged;
            this.areaSectionListController.Init();

            this.lineSectionListController = new LineSectionListController(pnlLinesReportTable, new LineTitleRow(), lineFinishBaseList);
            this.lineSectionListController.TotalsChanged += LineSectionListController_TotalsChanged;
            this.lineSectionListController.Init();

            this.seamSectionListController = new SeamSectionListController(pnlSeamsReportTable, new SeamTitleRow(), seamFinishBaseList);
            this.seamSectionListController.TotalsChanged += SeamSectionListController_TotalsChanged;
            this.seamSectionListController.Init();

            this.cntrSectionListController = new CntrSectionListController(pnlCntrsReportTable, new CntrTitleRow(), counterList);
            this.cntrSectionListController.CountOrTotalChanged += CntrSectionListController_CountOrTotalChanged;
            this.cntrSectionListController.Init();

            SetScaleWarning(scaleHasBeenSet);

            CanvasLib.Scale_Line.ScaleRuleController.SetScaleCompleted += ScaleRuleController_SetScaleCompleted;

            setSize();
        }

        private void setSize()
        {
            this.grpLines.Location = new Point(this.grpLines.Location.X, this.grbArea.Location.Y + this.grbArea.Size.Height + 8);
            this.grpCounters.Location = new Point(this.grpCounters.Location.X, this.grpLines.Location.Y + this.grpLines.Size.Height + 8);
            this.grpSeams.Location = new Point(this.grpSeams.Location.X, this.grpCounters.Location.Y + this.grpCounters.Size.Height + 8);

            this.ckbHideZeroQuanity.Location = new Point(this.ckbHideZeroQuanity.Location.X, this.grpSeams.Location.Y + this.grpSeams.Size.Height + 12);
        }
        private void CntrSectionListController_CountOrTotalChanged(Counter counter)
        {

            this.cntrSectionListController.CalculateTotals(this.ckbCounterShowSelected.Checked);

            this.lblCounterCount.Text = cntrSectionListController.Count.ToString();
            this.lblCounterTotal.Text = cntrSectionListController.Total.ToString("#,##0.0");
        }

        private void ckbCounterShowSelected_CheckedChanged(object sender, EventArgs e)
        {

            this.cntrSectionListController.CalculateTotals(this.ckbCounterShowSelected.Checked);

            this.lblCounterCount.Text = cntrSectionListController.Count.ToString();
            this.lblCounterTotal.Text = cntrSectionListController.Total.ToString("#,##0.0");
        }


        private void SeamSectionListController_TotalsChanged(object sender, EventArgs e)
        {
            this.lblSeamsTotalField.Text = (this.seamSectionListController.TotalGross/12).ToString("#,##0") + " l.f.";
            this.lblSeamsSelField.Text = (this.seamSectionListController.SelectedGross/12).ToString("#,##0") + " l.f.";
        }

        private void LineSectionListController_TotalsChanged(object sender, EventArgs e)
        {
            this.lblLinesTotalField.Text = (this.lineSectionListController.TotalGross/12).ToString("#,##0") + " l.f.";
            this.lblLinesSelField.Text = (this.lineSectionListController.SelectedGross/12).ToString("#,##0") + " l.f.";
        }

        private void AreaSectionListController_TotalsChanged(object sender, EventArgs e)
        {
            double totalGross = this.areaSectionListController.TotalGross;
            double totalNet = this.areaSectionListController.TotalNet;
            double selectedGross = this.areaSectionListController.SelectedGross;
            double selectedNet = this.areaSectionListController.SelectedNet;

            this.lblAreaTotalGrossField.Text = totalGross.ToString("#,##0") + " s.f.";
            this.lblAreaTotalNetField.Text = totalNet.ToString("#,##0") + " s.f.";
            this.lblAreaSelGrossField.Text = selectedGross.ToString("#,##0") + " s.f.";
            this.lblAreaSelNetField.Text = selectedNet.ToString("#,##0") + " s.f.";

            this.lblAreaTotalWasteField.Text = (totalNet > 0.0) ? (100.0 * (totalGross / totalNet - 1.0)).ToString("0.00") + "%" : "N/A";
            this.lblAreaSelWasteField.Text = (selectedNet > 0.0) ? (100.0 * (selectedGross / selectedNet - 1.0)).ToString("0.00") + "%" : "N/A";
        }


        private void ScaleRuleController_SetScaleCompleted(bool cancelled)
        {
            if (cancelled)
            {
                return;
            }

            SetScaleWarning(false);

            this.scaleHasBeenSet = true;

            SetScaleWarning(false);

        }

        private void SetScaleWarning(bool setWarning)
        {
            string newTitle;

            if (setWarning)
            {
                newTitle = defaultTitleText + setScaleText;
            } else
            {
                newTitle = defaultTitleText;
            }
            this.Text = newTitle;
        }

#if Report
        private void setupReportExportControls()
        {
            RegistryUtils.SetupCheckBoxFromRegistry("SummaryReportIncludeHeader", this.ckbReportHeader);
            RegistryUtils.SetupCheckBoxFromRegistry("SummaryReportIncludeSummary", this.ckbReportSummary);
            RegistryUtils.SetupCheckBoxFromRegistry("SummaryReportSeparateUnits", this.ckbSeparateUnits);

            ignoreConversionCheckedChanged = true;

            RegistryUtils.SetupCheckBoxFromRegistry("SummaryReportConvertUnitsToFeet", this.ckbConvertAllToSqrFt);
            RegistryUtils.SetupCheckBoxFromRegistry("SummaryReportConvertUnitsToYards", this.ckbConvertAllToSqrFt);

            ignoreConversionCheckedChanged = false;

            ignoreDelimiterCheckedChanged = true;


            RegistryUtils.SetupRadioButtonFromRegistry("SummaryReportTabDelimited", this.rbnTabDelimited);
            RegistryUtils.SetupRadioButtonFromRegistry("SummaryReportCommaDelimited", this.rbnCommaDelimited);
            RegistryUtils.SetupRadioButtonFromRegistry("SummaryReportOtherDelimited", this.rbnOtherDelimited);

            ignoreDelimiterCheckedChanged = false;

            this.txbOtherDelimited.Text = RegistryUtils.GetRegistryStringValue("SummaryReportrbnOtherDelimiter", string.Empty);
        }
#endif

        #region Cursor Management
        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
        }

        public bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }

        public void AddToCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Add(this);
        }

        public void RemoveFromCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Remove(this);
        }
        public bool IsTopMost { get; set; } = false;

        private void ckbHideZeroQuanity_CheckedChanged(object sender, EventArgs e)
        {
            this.areaSectionListController.SetZeroMaterialsMode(ckbHideZeroQuanity.Checked);
            this.lineSectionListController.SetZeroMaterialsMode(ckbHideZeroQuanity.Checked);
            this.seamSectionListController.SetZeroMaterialsMode(ckbHideZeroQuanity.Checked);
            this.cntrSectionListController.SetZeroMaterialsMode(ckbHideZeroQuanity.Checked);
        }

        private void btnExpandAreas_Click(object sender, EventArgs e)
        {
            this.pnlAreasReportTable.SuspendLayout();

            if (this.btnExpandAreas.Text == "-")
            {
                this.grbArea.Size = new System.Drawing.Size(this.grbArea.Width, 38);
                this.btnExpandAreas.Text = "+";
                this.pnlAreasReportTable.Visible = false;

                setSize();

            }

            else
            {
                this.grbArea.Size = new System.Drawing.Size(this.grbArea.Width, areasGrbOrigHght);
                this.btnExpandAreas.Text = "-";
                this.pnlAreasReportTable.Visible = true;

                setSize();
            }

            this.pnlAreasReportTable.ResumeLayout();
        }

        private void btnExpandLines_Click(object sender, EventArgs e)
        {
            if (this.btnExpandLines.Text == "-")
            {
                this.grpLines.Size = new System.Drawing.Size(this.grpLines.Width, 38);
                this.btnExpandLines.Text = "+";
                this.pnlLinesReportTable.Visible = false;

                setSize();

            }

            else
            {
                this.grpLines.Size = new System.Drawing.Size(this.grpLines.Width, linesGrbOrigHght);
                this.btnExpandLines.Text = "-";
                this.pnlLinesReportTable.Visible = true;

                setSize();
            }
        }

        private void btnExpandCntrs_Click(object sender, EventArgs e)
        {
            if (this.btnExpandCntrs.Text == "-")
            {
                this.grpCounters.Size = new System.Drawing.Size(this.grpCounters.Width, 38);
                this.btnExpandCntrs.Text = "+";
                this.pnlCntrsReportTable.Visible = false;

                setSize();

            }

            else
            {
                this.grpCounters.Size = new System.Drawing.Size(this.grpCounters.Width, cntrsGrbOrigHght);
                this.btnExpandCntrs.Text = "-";
                this.pnlCntrsReportTable.Visible = true;

                setSize();
            }
        }

        private void btnExpandSeams_Click(object sender, EventArgs e)
        {
            if (this.btnExpandSeams.Text == "-")
            {
                this.grpSeams.Size = new System.Drawing.Size(this.grpSeams.Width, 38);
                this.btnExpandSeams.Text = "+";
                this.pnlSeamsReportTable.Visible = false;

                setSize();

            }

            else
            {
                this.grpSeams.Size = new System.Drawing.Size(this.grpSeams.Width, seamsGrbOrigHght);
                this.btnExpandSeams.Text = "-";
                this.pnlSeamsReportTable.Visible = true;

                setSize();
            }
        }

        #endregion

#if Report
        private void btnSave_Click(object sender, EventArgs e)
        {
            string rprtFileName = RegistryUtils.GetRegistryStringValue("SummaryReportFileName", string.Empty);
            string rprtFileDir = RegistryUtils.GetRegistryStringValue("SummaryReportFileDir", string.Empty);

            if (string.IsNullOrEmpty(rprtFileName) || string.IsNullOrEmpty(rprtFileDir))
            {
                doReportSaveAs(rprtFileName, rprtFileDir);
            }

            else
            {
                doReportSave(rprtFileName, rprtFileDir);
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            string rprtFileName = RegistryUtils.GetRegistryStringValue("SummaryReportFileName", string.Empty);
            string rprtFileDir = RegistryUtils.GetRegistryStringValue("SummaryReportFileDir", string.Empty);

            doReportSaveAs(rprtFileName, rprtFileDir);
        }

        private void doReportSaveAs(string rprtFileName, string rprtFileDir)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (!string.IsNullOrEmpty(rprtFileName))
            {
                saveFileDialog.FileName = rprtFileName;
            }

            if (!string.IsNullOrEmpty(rprtFileDir))
            {
                saveFileDialog.InitialDirectory = rprtFileDir;
            }

            string filter = string.Empty;

            if (this.rbnTabDelimited.Checked)
            {
                filter = "tsv|*.tsv|All files|*.*";
            }

            else if (this.rbnCommaDelimited.Checked)
            {
                filter = "csv|*.csv";
            }

            else
            {
                filter = "All files|*.*";
            }

            saveFileDialog.Filter = filter;

            saveFileDialog.FilterIndex = 1;

            DialogResult dr = saveFileDialog.ShowDialog(baseForm);

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            rprtFileDir = Path.GetDirectoryName(saveFileDialog.FileName);
            rprtFileName = Path.GetFileName(saveFileDialog.FileName);

            doReportSave(rprtFileName, rprtFileDir);
        }

        private void doReportSave(string rprtFileName, string rprtFileDir)
        {
            string outpFilePath = Path.Combine(rprtFileDir, rprtFileName);
            
            string separator = string.Empty;

            if (this.rbnTabDelimited.Checked)
            {
                separator = "\t";
            }

            else if (this.rbnCommaDelimited.Checked)
            {
                separator = ",";
            }

            else if (this.rbnOtherDelimited.Checked)
            {
                separator = this.txbOtherDelimited.Text;
            }

            string outpReport = generateReport(separator, this.ckbSeparateUnits.Checked, this.ckbConvertAllToSqrFt.Checked, this.ckbReportHeader.Checked, this.ckbReportSummary.Checked);

            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(outpFilePath);
            }

            catch
            {
                MessageBox.Show("Output file is currently in use.");
                return;
            }

            sw.Write(outpReport);

            sw.Flush();

            sw.Close();

            ManagedMessageBox.Show("Report has been saved to " + outpFilePath);

            RegistryUtils.SetRegistryValue("SummaryReportFileName", rprtFileName);
            RegistryUtils.SetRegistryValue("SummaryReportFileDir", rprtFileDir);
        }

        private void btnCopyReportToClipboard_Click(object sender, EventArgs e)
        {
            string separator = string.Empty;

            if (this.rbnTabDelimited.Checked)
            {
                separator = "\t";
            }

            else if (this.rbnCommaDelimited.Checked)
            {
                separator = ",";
            }

            else if (this.rbnOtherDelimited.Checked)
            {
                separator = this.txbOtherDelimited.Text;
            }

            string outpReport = generateReport(separator, this.ckbSeparateUnits.Checked, this.ckbConvertAllToSqrFt.Checked, this.ckbReportHeader.Checked, this.ckbReportSummary.Checked);

            Clipboard.SetText(outpReport);

            ManagedMessageBox.Show("Report has been copied to the clipboard.");
        }


        private string generateReport(string separator, bool separateUnits, bool convertAllToSqrFt, bool reportHeader, bool reportSummary)
        {

            List<string> rprtLines = new List<string>();
#if xxx
            if (reportHeader)
            {
                string rprtHeader = "Total";

                if (separateUnits)
                {
                    rprtHeader += separator;
                }

                rprtHeader += separator + "Tag" + separator + "Type" + separator + "Net";

                if (separateUnits)
                {
                    rprtHeader += separator;
                }

                rprtHeader += separator + "Waste%";

                if (separateUnits)
                {
                    rprtHeader += separator;
                }

                rprtHeader += separator + "Gross";

                if (separateUnits)
                {
                    rprtHeader += separator;
                }

                rprtHeader += separator + "W Repeats";

                if (separateUnits)
                {
                    rprtHeader += separator;
                }

                rprtHeader += separator + "L Repeats";

                rprtLines.Add(rprtHeader);
            }

            string convert = string.Empty;

            if (ckbConvertAllToSqrFt.Checked)
            {
                convert = "feet";
            }

            else if (ckbConvertAllUnitsToYards.Checked)
            {
                convert = "yards";
            }


            foreach (RollRow rollRow in this.rollRowDict.Values.Where(r => r.ReportRowType == ReportRowType.RollRow))
            {
                rprtLines.Add(rollRow.ToString(separator, separateUnits, convert, scaleHasBeenSet));
            }

            foreach (NonRollRow nonRollRow in this.rollRowDict.Values.Where(r => r.ReportRowType == ReportRowType.NonRollRow))
            {
                rprtLines.Add(nonRollRow.ToString(separator, separateUnits, convert, scaleHasBeenSet));
            }

            foreach (LineRow lineRow in this.rollRowDict.Values.Where(r => r.ReportRowType == ReportRowType.LineRow))
            {
                rprtLines.Add(lineRow.ToString(separator, separateUnits, convert, scaleHasBeenSet));
            }

            foreach (SeamRow seamRow in this.seamRowDict.Values.Where(r => r.SeamFinishBase.LengthInInches > 0))
            {
                rprtLines.Add(seamRow.ToString(separator, separateUnits, convert, scaleHasBeenSet));
            }

            if (reportSummary)
            {
                string summaryRow = string.Empty;

                if (ckbConvertAllUnitsToYards.Checked)
                {
                    if (ckbSeparateUnits.Checked)
                    {
                        summaryRow =
                            "Total:" + separator + (total / 9.0).ToString("#,##0") + separator + "s.y." 
                            + separator + "Total Gross:" + separator + (totalGross / 9.0).ToString("#,##0") + separator + "s.y."
                            + separator + "Total Net:" + separator + (totalNet / 9.0).ToString("#,##0") + separator + "s.y."
                            + separator + "Waste%:" + separator + this.lblAreaTotalWasteField.Text;
                    }

                    else
                    {
                        summaryRow =
                            "Total:" + separator + (total / 9.0).ToString("#,##0") + " s.y."
                            + separator + "Total Gross:" + separator + (totalGross / 9.0).ToString("#,##0") + " s.y."
                            + separator + "Total Net:" + separator + (totalNet / 9.0).ToString("#,##0") + " s.y."
                            + separator + "Waste%:" + separator + this.lblAreaTotalWasteField.Text;
                    }
                }

                else
                {
                    if (ckbSeparateUnits.Checked)
                    {
                        summaryRow =
                            "Total:" + separator + total.ToString("#,##0") + separator + "s.f."
                            + separator + "Total Gross:" + separator + totalGross.ToString("#,##0") + separator + "s.f."
                            + separator + "Total Net:" + separator + totalNet.ToString("#,##0") + separator + "s.f."
                            + separator + "Waste%:" + separator + this.lblAreaTotalWasteField.Text;
                    }

                    else
                    {
                        summaryRow =
                            "Total:" + separator + total.ToString("#,##0") + " s.f."
                            + separator + "Total Gross:" + separator + totalGross.ToString("#,##0") + " s.f."
                            + separator + "Total Net:" + separator + totalNet.ToString("#,##0") + " s.f."
                            + separator + "Waste%:" + separator + this.lblAreaTotalWasteField.Text;
                    }
                }

                rprtLines.Add("");
                rprtLines.Add(summaryRow);
            }
#endif
            return string.Join("\n", rprtLines);

        }

        private void ckbReportHeader_CheckedChanged(object sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("SummaryReportIncludeHeader", ckbReportHeader.Checked.ToString());
        }

        private void ckbReportSummary_CheckedChanged(object sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("SummaryReportIncludeSummary", ckbReportSummary.Checked.ToString());
        }

        private void ckbSeparateUnits_CheckedChanged(object sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("SummaryReportSeparateUnits", ckbSeparateUnits.Checked.ToString());
        }

        private bool ignoreConversionCheckedChanged = false;

        private void ckbConvertAllToSqrFt_CheckedChanged(object sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("SummaryReportConvertUnitsToFeet", ckbConvertAllToSqrFt.Checked.ToString());

            if (ignoreConversionCheckedChanged)
            {
                return;
            }

            if (ckbConvertAllToSqrFt.Checked)
            {
                ignoreConversionCheckedChanged = true;

                this.ckbConvertAllUnitsToYards.Checked = false;

                ignoreConversionCheckedChanged = false;
            }
        }

        private void ckbConvertAllUnitsToYards_CheckedChanged(object sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("SummaryReportConvertUnitsToYards", ckbConvertAllUnitsToYards.Checked.ToString());

            if (ignoreConversionCheckedChanged)
            {
                return;
            }

            if (ckbConvertAllUnitsToYards.Checked)
            {
                ignoreConversionCheckedChanged = true;

                this.ckbConvertAllToSqrFt.Checked = false;

                ignoreConversionCheckedChanged = false;
            }
        }

        private bool ignoreDelimiterCheckedChanged = false;

        private void rbnTabDelimited_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreDelimiterCheckedChanged)
            {
                return;
            }

            RegistryUtils.SetRegistryValue("SummaryReportTabDelimited", rbnTabDelimited.Checked.ToString());
        }

        private void rbnCommaDelimited_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreDelimiterCheckedChanged)
            {
                return;
            }

            RegistryUtils.SetRegistryValue("SummaryReportCommaDelimited", rbnCommaDelimited.Checked.ToString());
        }

        private void rbnOtherDelimited_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreDelimiterCheckedChanged)
            {
                return;
            }

            RegistryUtils.SetRegistryValue("SummaryReportrbnOtherDelimited", rbnCommaDelimited.Checked.ToString());
        }

        private void txbOtherDelimited_TextChanged(object sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("SummaryReportrbnOtherDelimiter", txbOtherDelimited.Text);
        }
#endif
    }
}
