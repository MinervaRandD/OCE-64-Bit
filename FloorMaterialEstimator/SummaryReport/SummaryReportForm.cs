

namespace SummaryReport
{
    using System;
    using FinishesLib;
    using CanvasManager.FinishManager;
    using Utilities;

    using System.Windows.Forms;
    using CanvasLib.Counters;
    using SummaryReport.Exporters;
    using System.Drawing;
    using CanvasManagerLib.FinishManager;

    //using CanvasManagerLib.FinishManager;

    public partial class SummaryReportForm : Form, IDisposable
    {
        private const string defaultTitleText = "Summary Report";
        private const string setScaleText = " - Set Scale to see Report";

        private AreaSectionListController areaSectionListController;
        private LineSectionListController lineSectionListController;
        private SeamSectionListController seamSectionListController;
        private CntrSectionListController cntrSectionListController;

        private AreaFinishBaseList areaFinishBaseList;
        private LineFinishBaseList lineFinishBaseList;
        private SeamFinishBaseList seamFinishBaseList;

        Action<int> updateOversUndersStats;

        //private FloorMaterialEstimatorBaseForm baseForm;

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

            //AddToCursorManagementList();

            //this.FormClosed += FinishesEditForm_FormClosed;

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
            AreaFinishBaseList areaFinishBaseList
            , LineFinishBaseList lineFinishBaseList
            ,SeamFinishBaseList seamFinishBaseList
            ,CounterList counterList
            ,bool scaleHasBeenSet
            , Action<int> UpdateOversUndersStats)
        {

            this.areaFinishBaseList= areaFinishBaseList;
            this.lineFinishBaseList= lineFinishBaseList;
            this.seamFinishBaseList= seamFinishBaseList;

            this.updateOversUndersStats = UpdateOversUndersStats;

            InitializeOversUnders();

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

        private void InitializeOversUnders()
        {
            
            for (int i = 0; i < areaFinishBaseList.Count; i++)
            {
                AreaFinishBase areaFinishBase = areaFinishBaseList[i];

                if (areaFinishBase.MaterialsType != MaterialsType.Rolls) continue;

                updateOversUndersStats(i);
            }
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

        #region Cursor Management

#if false
        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
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

#endif

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

        private void tsbExportAsText_Click(object sender, EventArgs e)
        {

        }

        private void tsbExportTextFullReport_Click(object sender, EventArgs e)
        {
            TextExportFullReportForm textExportFullReportForm = new TextExportFullReportForm();

            textExportFullReportForm.Init(
                this.areaSectionListController
                , this.lineSectionListController
                , this.seamSectionListController
                , this.cntrSectionListController);

            textExportFullReportForm.AutoValidate = AutoValidate.EnableAllowFocusChange;

            textExportFullReportForm.ShowDialog(this);

            textExportFullReportForm.Activate();

            textExportFullReportForm.Focus();

            this.SendToBack();

            textExportFullReportForm.BringToFront();
        }

        private void tsbExportTextSummaryReport_Click(object sender, EventArgs e)
        {
            TextExportSummaryReportForm textExportSummaryReportForm = new TextExportSummaryReportForm();

            textExportSummaryReportForm.Init(
                this.areaSectionListController
                );

            textExportSummaryReportForm.Show(this);
        }

        private void tsbExportToExcel_Click(object sender, EventArgs e)
        {
           
        }

        private void tsbExportToExcelFullReport_Click(object sender, EventArgs e)
        {
            ExcelExportFullReportForm excelExportFullReportForm = new ExcelExportFullReportForm();

            excelExportFullReportForm.Init(
                this.areaSectionListController
                , this.lineSectionListController
                , this.seamSectionListController
                , this.cntrSectionListController);

            excelExportFullReportForm.AutoValidate = AutoValidate.EnableAllowFocusChange;

            excelExportFullReportForm.ShowDialog(this);

            excelExportFullReportForm.Activate();

            excelExportFullReportForm.Focus();

            this.SendToBack();

            excelExportFullReportForm.BringToFront();
        }

        private void tsbExportToExcelSummaryReport_Click(object sender, EventArgs e)
        {
            ExcelExportSummaryReportForm excelExportSummaryReportForm = new ExcelExportSummaryReportForm();

            excelExportSummaryReportForm.Init(
                this.areaSectionListController
                );

            excelExportSummaryReportForm.AutoValidate = AutoValidate.EnableAllowFocusChange;

            excelExportSummaryReportForm.Show(this);

            excelExportSummaryReportForm.Activate();

            excelExportSummaryReportForm.Focus();

            this.SendToBack();

            excelExportSummaryReportForm.BringToFront();
        }

        #endregion
    }
}
