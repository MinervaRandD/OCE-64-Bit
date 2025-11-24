
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

    public partial class ExcelExportFullReportForm : Form
    {
        AreaSectionListController areaSectionListController;
        LineSectionListController lineSectionListController;
        SeamSectionListController seamSectionListController;
        CntrSectionListController cntrSectionListController;

        public ExcelExportFullReportForm()
        {
            InitializeComponent();

            this.FormClosed += ExcelExportFullReportForm_FormClosed;
        }

        private void ExcelExportFullReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryUtils.SetRegistryValue("ExcelExportFullAreas", ckbAreas.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportFullLines", ckbLines.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportFullCntrs", ckbCntrs.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportFullSeams", ckbSeams.Checked.ToString());

            RegistryUtils.SetRegistryValue("ExcelExportFullNonZeroItemsOnly", ckbNonZeroItemsOnly.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportFullCheckedItemsOnly", ckbCheckedItemsOnly.Checked.ToString());

            RegistryUtils.SetRegistryValue("ExcelExportFullToOpenWorkbook", rbnExportToOpenWorkbook.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportFullToNewWorkbook", rbnCreateNewWorkbook.Checked.ToString());

            RegistryUtils.SetRegistryValue("ExcelExportFullIncludeTotals", ckbIncludeTotals.Checked.ToString());
            RegistryUtils.SetRegistryValue("ExcelExportFullIncludeHeaders", ckbIncludeHeaders.Checked.ToString());
        }

        public void Init(
            AreaSectionListController areaSectionListController
            ,LineSectionListController lineSectionListController
            ,SeamSectionListController seamSectionListController
            ,CntrSectionListController cntrSectionListController)
        {
            this.areaSectionListController = areaSectionListController;
            this.lineSectionListController = lineSectionListController;
            this.seamSectionListController = seamSectionListController;
            this.cntrSectionListController = cntrSectionListController;

            ckbAreas.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullAreas", "False") == "True" ;
            ckbLines.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullLines", "False") == "True" ;
            ckbCntrs.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullCntrs", "False") == "True" ;
            ckbSeams.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullSeams", "False") == "True" ;

            ckbNonZeroItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullNonZeroItemsOnly", "False") == "True";
            ckbCheckedItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullCheckedItemsOnly", "False") == "True";

            rbnExportToOpenWorkbook.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullToOpenWorkbook", "True") == "True";
            rbnCreateNewWorkbook.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullToNewWorkbook", "False") == "True";

            ckbIncludeTotals.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullIncludeTotals", "False") == "True";
            ckbIncludeHeaders.Checked = RegistryUtils.GetRegistryStringValue("ExcelExportFullIncludeHeaders", "False") == "True";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!ckbAreas.Checked && !ckbLines.Checked && !ckbSeams.Checked && !ckbCntrs.Checked)
            {
                MessageBox.Show("No elements selected to report on.");
                return;
            }

            if (this.rbnExportToOpenWorkbook.Checked)
            {
                exportToOpenWorkbook();

                return;
            }

            if (this.rbnCreateNewWorkbook.Checked)
            {
                exportToNewWorkbook();

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

            }
            catch (Exception)
            {
               
            }

        }
        private void exportToNewWorkbook()
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

        int outpRow;

        private void transferDataToWorksheet(Worksheet worksheet)
        {
            outpRow = 1;

            worksheet.UsedRange.Clear();

            if (this.ckbAreas.Checked)
            {
                transferAreaDataToWorksheet(worksheet);
            }

            if (this.ckbLines.Checked)
            {
                transferLineDataToWorksheet(worksheet);
            }

            if (this.ckbCntrs.Checked)
            {
                transferCntrDataToWorksheet(worksheet);
            }

            if (this.ckbSeams.Checked)
            {
                transferSeamDataToWorksheet(worksheet);
            }
        }

        private void transferAreaDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Areas";

            cells[outpRow, 1].Font.Bold = true;

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                TransferAreaTotalDataToWorksheet(worksheet);
            }
 
            if (this.ckbIncludeHeaders.Checked)
            {
                transferAreaHeaderToWorksheet(worksheet);
            }

            foreach (IReportRow iReportRow in areaSectionListController.itemRowDict.Values)
            {
                if (!iReportRow.Selected && this.ckbCheckedItemsOnly.Checked)
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

                    transferRollDataToWorksheet(worksheet, rollRow);

                    continue;
                }

                if (iReportRow is TileRow)
                {
                    TileRow tileRow = (TileRow)iReportRow;

                    if (tileRow.TotalNetInSqrFeet <= 0 && this.ckbNonZeroItemsOnly.Checked)
                    {
                        continue;
                    }

                    transferTileDataToWorksheet(worksheet, tileRow);

                    continue;
                }
            }

            outpRow += 1;
        }

        private void TransferAreaTotalDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Total";

            cells[outpRow, 1].Font.Bold = true;

            cells[outpRow, 7] = "Selected";

            cells[outpRow, 7].Font.Bold = true;

            outpRow++;

            cells[outpRow, 1] = "Gross";

            cells[outpRow, 1].Font.Bold = true;

            cells[outpRow, 2] = areaSectionListController.TotalGross;

            cells[outpRow, 2].NumberFormat = "#,##0.0";

            cells[outpRow, 3] = "Net";

            cells[outpRow, 3].Font.Bold = true;

            cells[outpRow, 4] = areaSectionListController.TotalNet;

            cells[outpRow, 4].NumberFormat = "#,##0.0";

            cells[outpRow, 5] = "Waste %";

            cells[outpRow, 5].Font.Bold = true;

            if (areaSectionListController.TotalNet > 0.0)
            {
                cells[outpRow, 6] =
                (100.0 * (areaSectionListController.TotalGross / areaSectionListController.TotalNet - 1.0)).ToString("0.00") + "%" ;
            }

            else
            {
                cells[outpRow, 6] = "N/A";
            }

            cells[outpRow, 7] = "Gross";

            cells[outpRow, 7].Font.Bold = true;

            cells[outpRow, 8] = areaSectionListController.SelectedGross;

            cells[outpRow, 8].NumberFormat = "#,##0.0";
                
            cells[outpRow, 9] = "Net";

            cells[outpRow, 9].Font.Bold = true;

            cells[outpRow, 10] = areaSectionListController.SelectedNet;

            cells[outpRow, 10].NumberFormat = "#,##0.0";

            cells[outpRow, 11] = "Waste %";

            cells[outpRow, 11].Font.Bold = true;

            cells[outpRow, 12] = (areaSectionListController.SelectedNet > 0.0) ?
                (100.0 * (areaSectionListController.SelectedNet / areaSectionListController.SelectedGross - 1.0)).ToString("0.00") + "%" :
                "N/A";

            outpRow += 2;
        }

        private void transferAreaHeaderToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Total";
            cells[outpRow, 2] = "Tag";
            cells[outpRow, 3] = "Type";
            cells[outpRow, 4] = "Net";
            cells[outpRow, 5] = "Gross";
            cells[outpRow, 6] = "% Waste";
            cells[outpRow, 7] = "W-Repeats";
            cells[outpRow, 8] = "L-Repeats";
            cells[outpRow, 9] = "Tot Repeats";

            for (int i = 1; i <= 9; i++)
            {
                cells[outpRow, i].Font.Bold = true;
                cells[outpRow, i].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }

            outpRow = outpRow + 1;
           
        }

        private void transferRollDataToWorksheet(Worksheet worksheet, RollRow rollRow)
        {
            AreaFinishBase areaFinishBase = rollRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            Range cells = worksheet.Cells;

            cells[outpRow, 1] = grossAreaInSqrFeet;

            cells[outpRow, 1].NumberFormat = "#,##0.0";

            cells[outpRow, 2] = areaFinishBase.AreaName;

            cells[outpRow, 3] = "Roll";

            cells[outpRow, 4] = netAreaInSqrFeet;

            cells[outpRow, 4].NumberFormat = "#,##0.0";

            cells[outpRow, 5] = grossAreaInSqrFeet;

            cells[outpRow, 5].NumberFormat = "#,##0.0";

            if (wastePct.HasValue)
            {
                cells[outpRow, 6] = wastePct.Value * 100.0;

                cells[outpRow, 6].NumberFormat = "0.00";
            }

            else
            {
                cells[outpRow, 6] = string.Empty;
            }

            cells[outpRow, 7] = areaFinishBase.WRepeatsInSqrFeet;
            cells[outpRow, 8] = areaFinishBase.LRepeatsInSqrFeet;
            cells[outpRow, 9] = areaFinishBase.WRepeatsInSqrFeet + areaFinishBase.LRepeatsInSqrFeet;

            outpRow++;
        }

        private void transferTileDataToWorksheet(Worksheet worksheet, TileRow tileRow)
        {
            AreaFinishBase areaFinishBase = tileRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            Range cells = worksheet.Cells;

            cells[outpRow, 1] = grossAreaInSqrFeet;

            cells[outpRow, 1].NumberFormat = "#,##0.0";

            cells[outpRow, 2] = areaFinishBase.AreaName;

            cells[outpRow, 3] = "Tile";

            cells[outpRow, 4] = netAreaInSqrFeet;

            cells[outpRow, 4].NumberFormat = "#,##0.0";

            cells[outpRow, 5] = grossAreaInSqrFeet;

            cells[outpRow, 5].NumberFormat = "#,##0.0";

            outpRow++;
        }

        private void transferLineDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Lines";

            cells[outpRow, 1].Font.Bold = true;

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                TransferLineTotalDataToWorksheet(worksheet);
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                transferLineHeaderToWorksheet(worksheet);
            }

            foreach (IReportRow iReportRow in lineSectionListController.itemRowDict.Values)
            {
                if (!iReportRow.Selected && this.ckbCheckedItemsOnly.Checked)
                {
                    continue;
                }

                LineRow lineRow = (LineRow)iReportRow;

                if (lineRow.LengthInInches <= 0 && this.ckbNonZeroItemsOnly.Checked)
                {
                    continue;
                }

                transferLineDataToWorksheet(worksheet, lineRow);

                continue;
                
            }

            outpRow += 1;
        }

        private void TransferLineTotalDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Total L.F.";

            cells[outpRow, 1].Font.Bold = true;

            cells[outpRow, 2] = lineSectionListController.TotalGrossLF;

            cells[outpRow, 2].NumberFormat = "#,##0.0";

            cells[outpRow, 3] = "Total S.F.";

            cells[outpRow, 3].Font.Bold = true;

            if (lineSectionListController.TotalGrossSF.HasValue)
            {
                cells[outpRow, 4] = lineSectionListController.TotalGrossSF;

                cells[outpRow, 4].NumberFormat = "#,##0.0";

            }

            else
            {
                cells[outpRow, 4] = string.Empty;
            }

            cells[outpRow, 5] = "Selected L.F.";

            cells[outpRow, 5].Font.Bold = true;

            cells[outpRow, 6] = lineSectionListController.SelectedGrossLF;

            cells[outpRow, 6].NumberFormat = "#,##0.0";

            cells[outpRow, 7] = "Selected S.F.";

            cells[outpRow, 7].Font.Bold = true;

            if (lineSectionListController.SelectedGrossSF.HasValue)
            {
                cells[outpRow, 8] = lineSectionListController.SelectedGrossSF;

                cells[outpRow, 8].NumberFormat = "#,##0.0";
            }

            else
            {
                cells[outpRow, 8] = string.Empty;
            }
            
            outpRow += 2;
        }

        private void transferLineHeaderToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Total L.F.";
            cells[outpRow, 2] = "Total S.F.";
            cells[outpRow, 3] = "Tag";
            cells[outpRow, 4] = "Type";

            for (int i = 1; i <= 4; i++)
            {
                cells[outpRow, i].Font.Bold = true;
                cells[outpRow, i].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }
            
            outpRow = outpRow + 1;

        }

        private void transferLineDataToWorksheet(Worksheet worksheet, LineRow lineRow)
        {
            LineFinishBase lineFinishBase = lineRow.LineFinishBase;

            double LengthInFeet = lineFinishBase.LgthInFeet;
            
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = LengthInFeet;

            cells[outpRow, 1].NumberFormat = "#,##0.0";

            string title = string.Empty;

            if (lineFinishBase.IsWallLine)
            { 
                cells[outpRow, 2] = LengthInFeet * lineFinishBase.WallHeightInFeet;
                title = "Wall Line";
            }

            else
            {
                cells[outpRow, 2] = "";
                title = "Line";
            }

            cells[outpRow, 2].NumberFormat = "#,##0.0";

            cells[outpRow, 3] = lineFinishBase.LineName;

            cells[outpRow, 4] = title;

            outpRow++;
        }


        private void transferCntrDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Counters";

            cells[outpRow, 1].Font.Bold = true;

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                TransferCntrTotalDataToWorksheet(worksheet);
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                transferCntrHeaderToWorksheet(worksheet);
            }

            foreach (IReportRow iReportRow in cntrSectionListController.itemRowDict.Values)
            {
                if (!iReportRow.Selected && this.ckbCheckedItemsOnly.Checked)
                {
                    continue;
                }

                CntrRow cntrRow = (CntrRow)iReportRow;

                if (cntrRow.Count <= 0 && this.ckbNonZeroItemsOnly.Checked)
                {
                    continue;
                }

                transferCntrDataToWorksheet(worksheet, cntrRow);

                continue;

            }

            outpRow += 1;
        }

        private void TransferCntrTotalDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Count";

            cells[outpRow, 1].Font.Bold = true;

            cells[outpRow, 2] = cntrSectionListController.Count;

            cells[outpRow, 3] = "Total";

            cells[outpRow, 3].Font.Bold = true;

            cells[outpRow, 4] = cntrSectionListController.Total;

            outpRow += 2;
        }

        private void transferCntrHeaderToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Tag";
            cells[outpRow, 2] = "Count";
            cells[outpRow, 3] = "Description";
            cells[outpRow, 4] = "Size";
            cells[outpRow, 5] = "Total";

            for (int i = 1; i <= 5; i++)
            {
                cells[outpRow, i].Font.Bold = true;
                cells[outpRow, i].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }

            outpRow = outpRow + 1;
        }

        private void transferCntrDataToWorksheet(Worksheet worksheet, CntrRow cntrRow)
        {
            Counter counter = cntrRow.Counter;

            worksheet.Cells[outpRow, 1] = ((char) ( counter.Tag)).ToString();
            worksheet.Cells[outpRow, 2] = counter.Count;
            worksheet.Cells[outpRow, 3] = counter.Description;
            worksheet.Cells[outpRow, 4] = counter.Size;
            worksheet.Cells[outpRow, 5] = counter.Size * counter.Count;

            outpRow++;
        }

        private void transferSeamDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Seams";
            cells[outpRow, 1].Font.Bold = true;

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                TransferSeamTotalDataToWorksheet(worksheet);
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                transferSeamHeaderToWorksheet(worksheet);
            }

            foreach (IReportRow iReportRow in seamSectionListController.itemRowDict.Values)
            {
                if (!iReportRow.Selected && this.ckbCheckedItemsOnly.Checked)
                {
                    continue;
                }

                SeamRow seamRow = (SeamRow)iReportRow;

                if (seamRow.LengthInInches <= 0 && this.ckbNonZeroItemsOnly.Checked)
                {
                    continue;
                }

                transferSeamDataToWorksheet(worksheet, seamRow);

                continue;

            }
        }

        private void TransferSeamTotalDataToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Total";

            cells[outpRow, 1].Font.Bold = true;

            cells[outpRow, 2] = seamSectionListController.TotalGross;

            cells[outpRow, 2].NumberFormat = "#,##0.0";

            cells[outpRow, 3] = "Selected";

            cells[outpRow, 3].Font.Bold = true;

            cells[outpRow, 4] = seamSectionListController.SelectedGross;

            cells[outpRow, 4].NumberFormat = "#,##0.0";

            outpRow += 2;
        }

        private void transferSeamHeaderToWorksheet(Worksheet worksheet)
        {
            Range cells = worksheet.Cells;

            cells[outpRow, 1] = "Total";
            cells[outpRow, 2] = "Tag";
            cells[outpRow, 3] = "Type";

            for (int i = 1; i <= 3; i++)
            {
                cells[outpRow, i].Font.Bold = true;
                cells[outpRow, i].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }

            outpRow = outpRow + 1;

        }

        private void transferSeamDataToWorksheet(Worksheet worksheet, SeamRow seamRow)
        {
            SeamFinishBase seamFinishBase = seamRow.SeamFinishBase;

            double LengthInFeet = seamFinishBase.LengthInInches / 12.0;

            Range cells = worksheet.Cells;

            cells[outpRow, 1] = LengthInFeet;

            cells[outpRow, 1].NumberFormat = "#,##0.0";

            cells[outpRow, 2] = seamFinishBase.SeamName;

            cells[outpRow, 3] = "Seam";

            outpRow++;
        }

    }
}
