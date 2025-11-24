
using Globals;
using SummaryReport;

namespace SummaryReport.Exporters
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using CanvasLib.Counters;
    using FinishesLib;
    using Utilities;

    using Microsoft.Office.Interop.Excel;

    using Excel = Microsoft.Office.Interop.Excel;

    public partial class ExcelExportSummaryReportForm : Form
    {
        AreaSectionListController areaSectionListController;
        

        public ExcelExportSummaryReportForm()
        {
            InitializeComponent();

            this.FormClosed += ExcelExportReportForm_FormClosed;
        }

        private void ExcelExportReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryUtils.SetRegistryValue("ExcelExportSummaryReportNonZeroItemsOnly", ckbNonZeroItemsOnly.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportSummaryReportCheckedItemsOnly", ckbCheckedItemsOnly.Checked.ToString());

            RegistryUtils.SetRegistryValue("ExcelExportSummaryReportNewWorkbook", rbnCreateNewWorkbook.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportSummaryReportOpenWorkbook", rbnExportToOpenWorkbook.Checked.ToString());

            RegistryUtils.SetRegistryValue("ExcelExportSummaryReportIncludeTotals", ckbIncludeTotals.Checked.ToString()) ;
            RegistryUtils.SetRegistryValue("ExcelExportSummaryReportIncludeHeaders", ckbIncludeHeaders.Checked.ToString()) ;
        }

        public void Init(
            AreaSectionListController areaSectionListController
            )
        {
            this.areaSectionListController = areaSectionListController;
          
            ckbNonZeroItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportSummaryReportNonZeroItemsOnly", "False") == "True";
            ckbCheckedItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportSummaryReportCheckedItemsOnly", "False") == "True";

            rbnCreateNewWorkbook.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportSummaryReportNewWorkbook", "True") == "True";
            rbnExportToOpenWorkbook.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportSummaryReportOpenWorkbook", "False") == "True";

            ckbIncludeTotals.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportSummaryReportIncludeTotals", "False") == "True";
            ckbIncludeHeaders.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportSummaryReportIncludeHeaders", "False") == "True";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.ckbCheckedItemsOnly.Checked)
            {
                if (noItemsChecked())
                {
                    MessageBox.Show("There are no checked items.");
                    return;
                }
            }

            if (this.ckbNonZeroItemsOnly.Checked)
            {
                if (noNonZeroItems())
                {
                    MessageBox.Show("There are no non-zero items.");
                    return;
                }
            }

            generateReportMtrx();

            exportReport();

     
        }


        int outpRow = 2;
        double excelTotlGross = 0;
        double excelTotlNet = 0;


        List<ExcelSummaryMtrxRow> rprtMtrx = new List<ExcelSummaryMtrxRow>();

        private void generateReportMtrx()
        {

            outpRow = 2;
            excelTotlGross = 0;
            excelTotlNet = 0;

            rprtMtrx.Clear();

            foreach (IReportRow iReportRow in areaSectionListController.itemRowDict.Values)
            {
                if (this.ckbCheckedItemsOnly.Checked && !iReportRow.Selected)
                {
                    continue;
                }

                if (this.ckbNonZeroItemsOnly.Checked && isZeroItem(iReportRow))
                {
                    continue;
                }

                if (iReportRow is RollRow)
                {
                    RollRow rollRow = (RollRow)iReportRow;

                    if (rollRow.TotalGrossInSqrFeet <= 0 && this.ckbNonZeroItemsOnly.Checked)
                    {
                        continue;
                    }

                    rprtMtrx.Add(generateReportRow(rollRow));

                    continue;
                }

                if (iReportRow is TileRow)
                {
                    TileRow tileRow = (TileRow)iReportRow;

                    if (tileRow.TotalNetInSqrFeet <= 0 && this.ckbNonZeroItemsOnly.Checked)
                    {
                        continue;
                    }

                    rprtMtrx.Add(generateReportRow(tileRow));

                    continue;
                }
            }

            double? wastePct = null;

            if (this.ckbIncludeTotals.Checked)
            {
                if (excelTotlNet > 0)
                {
                    wastePct = (100.0 * (excelTotlGross / excelTotlNet - 1.0));
                }

                rprtMtrx.Add(new ExcelSummaryMtrxRow(
                    ExcelSummaryMtrxRowType.totlRow
                    , "Total"
                    , excelTotlNet
                    , excelTotlGross
                    , wastePct));
            }
        }

        private ExcelSummaryMtrxRow generateReportRow(RollRow rollRow)
        {
            AreaFinishBase areaFinishBase = rollRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            excelTotlNet += netAreaInSqrFeet;

            if (grossAreaInSqrFeet.HasValue)
            {
                excelTotlGross += grossAreaInSqrFeet.Value;
            }

            ExcelSummaryMtrxRow rtrnValu = new ExcelSummaryMtrxRow(
                ExcelSummaryMtrxRowType.dataRow
                , areaFinishBase.AreaName
                , netAreaInSqrFeet
                , grossAreaInSqrFeet
                , wastePct);

            return rtrnValu;
        }

        private ExcelSummaryMtrxRow generateReportRow(TileRow tileRow)
        {
            AreaFinishBase areaFinishBase = tileRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            excelTotlNet += netAreaInSqrFeet;

            if (grossAreaInSqrFeet.HasValue)
            {
                excelTotlGross += grossAreaInSqrFeet.Value;
            }

            ExcelSummaryMtrxRow rtrnValu = new ExcelSummaryMtrxRow(
                ExcelSummaryMtrxRowType.dataRow
                , areaFinishBase.AreaName
                , netAreaInSqrFeet
                , grossAreaInSqrFeet
                , wastePct);

            return rtrnValu;
        }

        private void exportReport()
        {
            if (this.rbnExportToOpenWorkbook.Checked)
            {
                exportToOpenWorkbook();

                return;
            }

            if (this.rbnCreateNewWorkbook.Checked)
            {
                createNewWorkbook();

                return;
            }
        }

        private void exportToOpenWorkbook()
        {
            Excel.Application exApp;

            exApp = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");

            try
            {
                List<string> openWkbkNames = new List<string>();

                foreach (Workbook wb in exApp.Workbooks)
                {
                    openWkbkNames.Add(wb.Name);
                }

                if (openWkbkNames.Count <= 0)
                {
                    MessageBox.Show("No open excel workbooks found.");
                    return;
                }

                SelectOpenWorkbookForm selectOpenWorkbookForm = new SelectOpenWorkbookForm();

                selectOpenWorkbookForm.Init(openWkbkNames);

                DialogResult dr = selectOpenWorkbookForm.ShowDialog();

                if (dr != DialogResult.OK)
                {
                    return;
                }

                string workbookName = selectOpenWorkbookForm.SelectedWorkbookName;

                string worksheetName = selectOpenWorkbookForm.SelectedWorksheetName;

                Workbook exportWorkbook = exApp.Workbooks.get_Item(workbookName);

                Workbook workbook = exApp.Workbooks.get_Item(workbookName);

                Worksheet worksheet = ExcelInteropUtils.GetWorksheetByName(workbook, worksheetName);

                if (worksheet is null)
                {
                    worksheet = workbook.Worksheets.Add();

                    worksheet.Name = worksheetName;
                }

                transferDataToWorksheet(worksheet);

                workbook.Activate();

            }
            catch (Exception)
            {

            }

        }
        private void createNewWorkbook()
        {
            try
            {
                Excel.Application exApp;

                exApp = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");

                Excel.Workbook newWorkbook = exApp.Application.Workbooks.Add();

                Excel.Worksheet workSheet = newWorkbook.Sheets["Sheet1"];

                transferDataToWorksheet(workSheet);


                newWorkbook.Activate();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Attempt to create new workbook failed.");
                return;
            }
        }


        private void transferDataToWorksheet(Worksheet worksheet)
        {
            outpRow = 1;

            worksheet.UsedRange.Clear();

            if (ckbIncludeHeaders.Checked)
            {
                transferAreaHeaderToWorksheet(worksheet);
                outpRow++;
            }

            transferAreaDataToWorksheet(worksheet);
        }

        private void transferAreaHeaderToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Material";
            cells[outpRow, 2] = "Net";
            cells[outpRow, 3] = "Gross";
            cells[outpRow, 4] = "% Waste";


            for (int i = 1; i <= 4; i++)
            {
                cells[outpRow, i].Font.Bold = true;
                cells[outpRow, i].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }

        }

        private void transferAreaDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            foreach (ExcelSummaryMtrxRow rprtRow in rprtMtrx)
            {
                cells[outpRow, 1] = rprtRow.title;
                cells[outpRow, 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                cells[outpRow, 1].Font.Bold = true;

                if (rprtRow.net.HasValue  ) cells[outpRow, 2] = rprtRow.net.Value; else cells[outpRow, 2] = "";
                if (rprtRow.gross.HasValue) cells[outpRow, 3] = rprtRow.gross.Value; else cells[outpRow, 3] = "";
                if (rprtRow.waste.HasValue) cells[outpRow, 4] = rprtRow.waste.Value; else cells[outpRow, 4] = "";

                for (int col = 2; col <= 4; col++)
                {
                    cells[outpRow, col].NumberFormat = "#,##0.0";
                    cells[outpRow, col].HorizontalAlignment = XlHAlign.xlHAlignRight;

                    if (rprtRow.rowType == ExcelSummaryMtrxRowType.dataRow)
                    {
                        cells[outpRow, col].Font.Bold = false;
                    }

                    else if (rprtRow.rowType == ExcelSummaryMtrxRowType.totlRow)
                    {
                        cells[outpRow, col].Font.Bold = true;
                    }
                }

                outpRow++;
            }
        }

        private bool noItemsChecked()
        {
            foreach (IReportRow iReportRow in areaSectionListController.itemRowDict.Values)
            {
                if (iReportRow.Selected)
                {
                    return false;
                }
            }

            return true;
        }

        private bool noNonZeroItems()
        {
            foreach (IReportRow iReportRow in areaSectionListController.itemRowDict.Values)
            {
                if (isZeroItem(iReportRow))
                {
                    return false;
                }
            }

            return true;
        }

        private bool isZeroItem(IReportRow iReportRow)
        {
            if (iReportRow is RollRow)
            {
                RollRow rollRow = (RollRow)iReportRow;

                if (rollRow.TotalNetInSqrFeet != 0.0)
                {
                    return false;
                }
            }

            else if (iReportRow is TileRow)
            {
                TileRow tileRow = (TileRow)iReportRow;

                if (tileRow.TotalNetInSqrFeet != 0.0)
                {
                    return false;
                }
            }

            return true;
        }

    }

    public enum ExcelSummaryMtrxRowType
    {
        None = 0
        , headRow = 1
        , dataRow = 2
        , totlRow = 3
    }

    public struct ExcelSummaryMtrxRow
    {
        public ExcelSummaryMtrxRowType rowType;
        public string title;
        public double? net;
        public double? gross;
        public double? waste;

        public ExcelSummaryMtrxRow(
            ExcelSummaryMtrxRowType rowType
            , string title
            , double? net
            , double? gross
            , double? waste)
        {
            this.rowType = rowType;
            this.title = title;
            this.net = net;
            this.gross = gross;
            this.waste = waste;
        }
    }
}
