using CanvasLib.Counters;
using FinishesLib;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace SummaryReport.Exporters
{
    public partial class TextExportFullReportForm : Form
    {
        AreaSectionListController areaSectionListController;
        LineSectionListController lineSectionListController;
        SeamSectionListController seamSectionListController;
        CntrSectionListController cntrSectionListController;

        public TextExportFullReportForm()
        {
            InitializeComponent();

            this.FormClosed += TextExportFullReportForm_FormClosed;
        }
        private void TextExportFullReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryUtils.SetRegistryValue("TextExportFullAreas", ckbAreas.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportFullLines", ckbLines.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportFullCntrs", ckbCntrs.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportFullSeams", ckbSeams.Checked.ToString());

            RegistryUtils.SetRegistryValue("TextExportFullNonZeroItemsOnly", ckbNonZeroItemsOnly.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportFullCheckedItemsOnly", ckbCheckedItemsOnly.Checked.ToString());
            
             string outpFmt = "CSV";

            if (rbnCSV.Checked)
            {
                outpFmt = "CSV";
            }

            else if (rbnTSV.Checked)
            {
                outpFmt = "TSV";
            }

            else if (rbnColumnFormatted.Checked)
            {
                outpFmt = "ColumnFormatted";
            }

            else if (rbnOtherSeparator.Checked)
            {
                outpFmt = "OtherSeparator";
            }

            RegistryUtils.SetRegistryValue("TextExportFullOutpFmt", outpFmt);

            RegistryUtils.SetRegistryValue("TextExportFullOtherSeparator", txbOtherSeparator.Text.Trim());

            RegistryUtils.SetRegistryValue("TextExportFullToFile", rbnExportToFile.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportFullToClipboard", rbnExportToClipboard.Checked.ToString());

            RegistryUtils.SetRegistryValue("TextExportFullIncludeTotals", ckbIncludeTotals.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportIncludeHeaders", ckbIncludeHeaders.Checked.ToString());
        }

        public void Init(
         AreaSectionListController areaSectionListController
         , LineSectionListController lineSectionListController
         , SeamSectionListController seamSectionListController
         , CntrSectionListController cntrSectionListController)
        {
            this.areaSectionListController = areaSectionListController;
            this.lineSectionListController = lineSectionListController;
            this.seamSectionListController = seamSectionListController;
            this.cntrSectionListController = cntrSectionListController;

            ckbAreas.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullAreas", "False") == "True";
            ckbLines.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullLines", "False") == "True";
            ckbCntrs.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullCntrs", "False") == "True";
            ckbSeams.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullSeams", "False") == "True";

            ckbNonZeroItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullNonZeroItemsOnly", "False") == "True";
            ckbCheckedItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullCheckedItemsOnly", "False") == "True";

            string outpFmt = RegistryUtils.GetRegistryStringValue("TextExportFullOutpFmt", "CSV");

            switch (outpFmt)
            {
                case "CSV": rbnCSV.Checked = true; break;
                case "TSV": rbnTSV.Checked = true; break;
                case "ColumnFormatted": rbnColumnFormatted.Checked = true; break;
                case "OtherSeparator": rbnOtherSeparator.Checked = true; break;
                default: rbnCSV.Checked = true; break;
            }

            txbOtherSeparator.Text = RegistryUtils.GetRegistryStringValue("TextExportFullOtherSeparator", "");

            rbnExportToFile.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullToFile", "True") == "True";
            rbnExportToClipboard.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullToClipboard", "False") == "True";

            ckbIncludeTotals.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullIncludeTotals", "False") == "True";
            ckbIncludeHeaders.Checked = RegistryUtils.GetRegistryStringValue("TextExportFullIncludeHeaders", "False") == "True";
        }

        SortedDictionary<int, Tuple<int, TextFullRowType, List<string>>> rprtMtrx = new SortedDictionary<int, Tuple<int, TextFullRowType, List<string>>>();
    
        int outpRow = 1;

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!ckbAreas.Checked && !ckbLines.Checked && !ckbSeams.Checked && !ckbCntrs.Checked)
            {
                MessageBox.Show("No elements selected to report on.");
                return;
            }

            rprtMtrx.Clear();

            generateReportMtrx();

            if (rprtMtrx.Count== 0)
            {
                MessageBox.Show("No data generated for this report. Check selected options.");
                return;
            }

            exportReport();
        }

        private void generateReportMtrx()
        {
            outpRow= 1;

            if (this.ckbAreas.Checked)
            {
                generateAreaData();
            }

            if (this.ckbLines.Checked)
            {
                generateLineData();
            }

            if (this.ckbCntrs.Checked)
            {
                generateCntrData();
            }

            if (this.ckbSeams.Checked)
            {
                generateSeamData();
            }
        }

        private void generateAreaData()
        {
            List<string> areaHeaderString1 = new List<string>()
            {
                "Areas"
            };

            rprtMtrx.Add(outpRow, new Tuple<int, TextFullRowType, List<string>>(outpRow, TextFullRowType.HeaderRow, areaHeaderString1));

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                generateAreaTotalData();
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                generateAreaHeaderData();
            }

            generateAreaSpecHeader();
            
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

                    generateRollData(rollRow);

                    continue;
                }

                if (iReportRow is TileRow)
                {
                    TileRow tileRow = (TileRow)iReportRow;

                    if (tileRow.TotalNetInSqrFeet <= 0 && this.ckbNonZeroItemsOnly.Checked)
                    {
                        continue;
                    }

                    generateTileData(tileRow);

                    continue;
                }
            }

        }

        // Generate the header for the tiles and rolls specification section.

        private void generateAreaSpecHeader()
        {
            // We have to do things this way because depending on what is being generated, the position of
            // the spec header information might change.

            if (outpRow <= 3)
            {
                return;
            }

            if (!rprtMtrx.ContainsKey(outpRow - 2))
            {
                addLineToRprtMtrx(outpRow - 2, TextFullRowType.DataRow, new List<string>());
            }

            if (!rprtMtrx.ContainsKey(outpRow - 1))
            {
                addLineToRprtMtrx(outpRow - 1, TextFullRowType.DataRow, new List<string>());
            }

            var line1 = rprtMtrx[outpRow - 2].Item3;
            var line2 = rprtMtrx[outpRow - 1].Item3;

            fillListOutToColumn(line1, 13);
            fillListOutToColumn(line2, 13);

            line1.AddRange(new List<string>() { "Color", "","","", "Fill" });
            
            line2.AddRange(new List<string>(){"A", "R", "G", "B", "Type"});
            
        }
        private void generateAreaTotalData()
        {
            addLineToRprtMtrx(
                TextFullRowType.TotalRow
                ,new List<string>()
                {
                    "Total"
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , "Selected"
                }
           );

            outpRow++;

            addLineToRprtMtrx(
                TextFullRowType.DataRow
                ,new List<string>()
                {
                    "Gross"
                    , areaSectionListController.TotalGross.ToString("#,##0.0")
                    , "Net"
                    , areaSectionListController.TotalNet.ToString("#,##0.0")
                    , "Waste %"
                    , areaSectionListController.TotalNet > 0 ?
                            (100.0 * (areaSectionListController.TotalGross / areaSectionListController.TotalNet - 1.0)).ToString("0.00") + "%" :
                            "N/A"
                    , "Gross"
                    , areaSectionListController.SelectedGross.ToString("#,##0.0")
                    , "Net"
                    , areaSectionListController.SelectedNet.ToString("#,##0.0")
                    , "Waste %"
                    , (areaSectionListController.SelectedNet > 0.0) ?
                            (100.0 * (areaSectionListController.SelectedNet / areaSectionListController.SelectedGross - 1.0)).ToString("0.00") + "%" :
                            "N/A"
                    
                }
            );


            outpRow += 2;
        }

        private void generateAreaHeaderData()
        {
            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>()
                {
                    "Total"
                    , "Tag"
                    , "Type"
                    , "Net"
                    , "Gross"
                    , "% Waste"
                    , "W-Repeats"
                    , "L-Repeats"
                    , "Tot Repeats"
                }
            );
        
            outpRow++;
        }

        private void generateRollData(RollRow rollRow)
        {
            AreaFinishBase areaFinishBase = rollRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            addLineToRprtMtrx(
                TextFullRowType.DataRow,
                new List<string>()
                {
                    grossAreaInSqrFeet.HasValue ? grossAreaInSqrFeet.Value.ToString("#,##0.0") : ""
                    , areaFinishBase.AreaName
                    , "Roll"
                    , netAreaInSqrFeet.ToString("#,##0.0")
                    , grossAreaInSqrFeet.HasValue ? grossAreaInSqrFeet.Value.ToString("#,##0.0") : ""
                    , wastePct.HasValue ? wastePct.Value.ToString("#,##0.0") : ""
                    , areaFinishBase.WRepeatsInSqrFeet.ToString("#,##0.0")
                    , areaFinishBase.LRepeatsInSqrFeet.ToString("#,##0.0")
                    , (areaFinishBase.WRepeatsInSqrFeet + areaFinishBase.LRepeatsInSqrFeet).ToString("#,##0.0")
                    , ""
                    , ""
                    , ""
                    , ""
                    , areaFinishBase.Color.A.ToString()
                    , areaFinishBase.Color.R.ToString()
                    , areaFinishBase.Color.G.ToString()
                    , areaFinishBase.Color.B.ToString()
                    , "0"
                }
            );

            outpRow++;
        }

        private void generateTileData(TileRow tileRow)
        {
            AreaFinishBase areaFinishBase = tileRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            addLineToRprtMtrx(
                TextFullRowType.DataRow,
                new List<string>()
                {
                    grossAreaInSqrFeet.HasValue ? grossAreaInSqrFeet.Value.ToString("#,##0.0") : ""
                    , areaFinishBase.AreaName
                    , "Tile"
                    , netAreaInSqrFeet.ToString("#,##0.0")
                    , grossAreaInSqrFeet.HasValue ? grossAreaInSqrFeet.Value.ToString("#,##0.0") : ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , areaFinishBase.Color.A.ToString()
                    , areaFinishBase.Color.R.ToString()
                    , areaFinishBase.Color.G.ToString()
                    , areaFinishBase.Color.B.ToString()
                    , areaFinishBase.Pattern.ToString()
                }
            );

            outpRow++;
        }

        private void generateLineData()
        {
            outpRow++;

            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>() { "Lines" }
                );
            

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                generateLineTotalData();
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                generateLineHeaderData();
            }

            generateLineSpecHeader();

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

                generateLineRowData(lineRow);

                continue;

            }

            outpRow += 1;
        }

        private void generateLineSpecHeader()
        {
            // We have to do things this way because depending on what is being generated, the position of
            // the spec header information might change.

            if (outpRow <= 3)
            {
                return;
            }

            if (!rprtMtrx.ContainsKey(outpRow - 2))
            {
                addLineToRprtMtrx(outpRow - 2, TextFullRowType.DataRow, new List<string>());
            }

            if (!rprtMtrx.ContainsKey(outpRow - 1))
            {
                addLineToRprtMtrx(outpRow - 1, TextFullRowType.DataRow, new List<string>());
            }

            var line1 = rprtMtrx[outpRow - 2].Item3;
            var line2 = rprtMtrx[outpRow - 1].Item3;

            fillListOutToColumn(line1, 13);
            fillListOutToColumn(line2, 13);

            line1.AddRange(new List<string>() { "Color", "", "", "", "Line", "Line" });

            line2.AddRange(new List<string>() { "A", "R", "G", "B", "Width", "Pattern" });

        }
        private void generateLineTotalData()
        {
            lineSectionListController.CalculateTotals1();

            string totalGrossLFStr = lineSectionListController.TotalGrossLF.ToString("#,##0.0");
            string totalGrossSFStr = string.Empty;

            if (lineSectionListController.TotalGrossSF.HasValue)
            {
                totalGrossSFStr = lineSectionListController.TotalGrossSF.Value.ToString("#,##0.0");
            }

            string selectedGrossLFStr = lineSectionListController.SelectedGrossLF.ToString("#,##0.0");
            string selectedGrossSFStr = string.Empty;

            if (lineSectionListController.SelectedGrossSF.HasValue)
            {
                selectedGrossSFStr = lineSectionListController.SelectedGrossSF.Value.ToString("#,##0.0");
            }

            addLineToRprtMtrx(
                TextFullRowType.TotalRow,
                new List<string>()
                {
                    "Total"
                    , totalGrossLFStr
                    , totalGrossSFStr
                    , "Selected"
                    , selectedGrossLFStr
                    , selectedGrossSFStr
                    , lineSectionListController.SelectedGross.ToString("#,##0.0")
                   
                }
            );

            outpRow += 2;
        }

        private void generateLineHeaderData()
        {
            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>()
                {
                    "Total L.F."
                    , "Total S.F."
                    , "Tag"
                    , "Type"
                    
                }
            );

            outpRow = outpRow + 1;
        }

        private void generateLineRowData(LineRow lineRow)
        {
            LineFinishBase lineFinishBase = lineRow.LineFinishBase;

            double lgthInFeet = lineFinishBase.LengthInInches / 12.0;

            string lgthInFeetStr = lgthInFeet.ToString("#,##0.0");

            double? areaInSqFeet = lineFinishBase.sqrAreaInFeet;

            string type = "Line";

            string areaInSqrFeetStr = string.Empty;

            if (areaInSqFeet != null)
            {
                areaInSqrFeetStr += areaInSqFeet.Value.ToString("#,##0.0");

                type = "Wall Line";
            }

            addLineToRprtMtrx(
                TextFullRowType.DataRow,
                new List<string>()
                {
                    lgthInFeetStr
                    , areaInSqrFeetStr
                    , lineFinishBase.LineName
                    , type
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , lineFinishBase.LineColor.A.ToString()
                    , lineFinishBase.LineColor.R.ToString()
                    , lineFinishBase.LineColor.G.ToString()
                    , lineFinishBase.LineColor.B.ToString()
                    , lineFinishBase.LineWidthInPts.ToString()
                    , lineFinishBase.VisioLineType.ToString()
                }
            );

            outpRow++;
        }

        private void generateCntrData()
        {

            List<string> cntrData = new List<string>();

            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>() { "Counters"}
            );

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                generateCntrTotalData();
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                generateCntrHeaderData();
            }

            generateCntrSpecHeader();

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

                generateCntrDataRowData(cntrRow);

                continue;

            }

            outpRow += 1;
        }

        private void generateCntrSpecHeader()
        {
       
            // We have to do things this way because depending on what is being generated, the position of
            // the spec header information might change.

            if (outpRow <= 3)
            {
                return;
            }

            if (!rprtMtrx.ContainsKey(outpRow - 2))
            {
                addLineToRprtMtrx(outpRow - 2, TextFullRowType.DataRow, new List<string>());
            }

            if (!rprtMtrx.ContainsKey(outpRow - 1))
            {
                addLineToRprtMtrx(outpRow - 1, TextFullRowType.DataRow, new List<string>());
            }

            var line1 = rprtMtrx[outpRow - 2].Item3;
            var line2 = rprtMtrx[outpRow - 1].Item3;

            fillListOutToColumn(line1, 13);
            fillListOutToColumn(line2, 13);

            line1.AddRange(new List<string>() { "Color" });

            line2.AddRange(new List<string>() { "A", "R", "G", "B" });

        }

        private void generateCntrTotalData()
        {
            addLineToRprtMtrx(
                TextFullRowType.TotalRow,
                new List<string>()
                {
                    "Count"
                    , cntrSectionListController.Count.ToString("#,##0")
                    , "Total"
                    , cntrSectionListController.Total.ToString("#,##0")
                }
            );

            outpRow += 2;
        }

        private void generateCntrHeaderData()
        {
            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>()
                {
                    "Tag"
                    , "Count"
                    , "Description"
                    , "Size"
                    , "Total"
                }
             );

            outpRow = outpRow + 1;
        }

        private void generateCntrDataRowData(CntrRow cntrRow)
        {
            Counter counter = cntrRow.Counter;

            addLineToRprtMtrx(
                TextFullRowType.DataRow,
                new List<string>()
                {
                    ((char)(counter.Tag)).ToString()
                    , counter.Count.ToString()
                    , counter.Description
                    , counter.Size.ToString("0.0")
                    , (counter.Size * counter.Count).ToString("#,##0.0")
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , counter.Color.A.ToString()
                    , counter.Color.R.ToString()
                    , counter.Color.G.ToString()
                    , counter.Color.B.ToString()
                }
                );
           
            outpRow++;
        }

        private void generateSeamData()
        {
            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>()
                {
                    "Seams"
                }
            );

            outpRow += 2;

            if (this.ckbIncludeTotals.Checked)
            {
                generateSeamTotalData();
            }

            if (this.ckbIncludeHeaders.Checked)
            {
                generateSeamHeaderData();
            }

            generateSeamSpecHeader();

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

                generateSeamRowData(seamRow);

                continue;

            }
        }

        private void generateSeamSpecHeader()
        {

            // We have to do things this way because depending on what is being generated, the position of
            // the spec header information might change.

            if (outpRow <= 3)
            {
                return;
            }

            if (!rprtMtrx.ContainsKey(outpRow - 2))
            {
                addLineToRprtMtrx(outpRow - 2, TextFullRowType.DataRow, new List<string>());
            }

            if (!rprtMtrx.ContainsKey(outpRow - 1))
            {
                addLineToRprtMtrx(outpRow - 1, TextFullRowType.DataRow, new List<string>());
            }

            var line1 = rprtMtrx[outpRow - 2].Item3;
            var line2 = rprtMtrx[outpRow - 1].Item3;

            fillListOutToColumn(line1, 13);
            fillListOutToColumn(line2, 13);

            line1.AddRange(new List<string>() { "Color", "", "", "", "Line", "Line" });

            line2.AddRange(new List<string>() { "A", "R", "G", "B", "Width", "Type" });
        }

        private void generateSeamTotalData()
        {
            addLineToRprtMtrx(
                TextFullRowType.TotalRow,
                new List<string>()
                {
                    "Total"
                    , seamSectionListController.TotalGross.ToString("#,##0.0")
                    , "Selected"
                    , seamSectionListController.SelectedGross.ToString("#,##0.0")
                }
            );

            outpRow += 2;
        }

        private void generateSeamHeaderData()
        {
            addLineToRprtMtrx(
                TextFullRowType.HeaderRow,
                new List<string>()
                {
                    "Total"
                    , "Tag"
                    , "Type"
                }
            );

            outpRow = outpRow + 1;
        }

        private void generateSeamRowData(SeamRow seamRow)
        {
            SeamFinishBase seamFinishBase = seamRow.SeamFinishBase;

            double LengthInFeet = seamFinishBase.LengthInInches / 12.0;

            addLineToRprtMtrx(
                TextFullRowType.DataRow,
                new List<string>()
                {
                    LengthInFeet.ToString("#,##0.0")
                    , seamFinishBase.SeamName
                    , "Seam"
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , ""
                    , seamFinishBase.SeamColor.A.ToString()
                    , seamFinishBase.SeamColor.R.ToString()
                    , seamFinishBase.SeamColor.G.ToString()
                    , seamFinishBase.SeamColor.B.ToString()
                    , seamFinishBase.SeamWidthInPts.ToString()
                    , seamFinishBase.VisioDashType.ToString()
                }
            );

            outpRow++;
        }

        private void exportReport()
        {
            int maxRow = rprtMtrx.Last().Key;


            string rprtText = string.Empty;

            if (rbnColumnFormatted.Checked)
            {
                rprtText = rprtTextColumnFormatted();
            }

            else
            {
                rprtText = rprtTextDelimitterFormatted();
            }

            if (this.rbnExportToFile.Checked)
            {
                exportReportToFile(rprtText);
            }

            else if (this.rbnExportToClipboard.Checked)
            {
                exportReportToClipboard(rprtText);
            }
        }

        private void exportReportToFile(string rprtText)
        {
            string defaultExportPath = RegistryUtils.GetRegistryStringValue("TextExportFullReportDefaultPath", string.Empty).Trim();

            SaveFileDialog sfd = new SaveFileDialog();

            string defaultFileName = null;
            string defaultPathName = null;

            if (!string.IsNullOrEmpty(defaultExportPath))
            {
                defaultFileName = Path.GetFileName(defaultExportPath);
                defaultPathName = Path.GetDirectoryName(defaultExportPath);

                sfd.FileName = defaultFileName;

                sfd.InitialDirectory = defaultPathName;

            }

            DialogResult dr = DialogResult.OK;

            dr = sfd.ShowDialog();


            if (dr != DialogResult.OK)
            {
                return;
            }

            string actualExportPath = sfd.FileName;

            File.WriteAllText(actualExportPath, rprtText);

            MessageBox.Show("Report has been written to '" + actualExportPath + "'.");

            RegistryUtils.SetRegistryValue("TextExportFullReportDefaultPath", actualExportPath);
        }

        private void exportReportToClipboard(string rprtText)
        {
            System.Windows.Forms.Clipboard.SetText(rprtText);


            MessageBox.Show("Report has been written to the clipboard.");
        }

        private string rprtTextColumnFormatted()
        {
            string rprtText = string.Empty;

            int maxFieldSize = 0;

            int maxRow = rprtMtrx.Last().Key;

            foreach (List<string> row in rprtMtrx.Values.Select(x=>x.Item3))
            {
                int maxLclFieldSize = row.Select(x=>x.Length).Max();

                if (maxLclFieldSize > maxFieldSize) maxFieldSize = maxLclFieldSize;
            }

            maxFieldSize += 1;

            string[] rowList = new string[maxRow];

            for (int i = 0; i < maxRow; i++) rowList[i] = "";

            foreach (var x in rprtMtrx)
            {
                int row = x.Key;
                var line = x.Value;

                rowList[row - 1] = formatRow(line.Item2, line.Item3, maxFieldSize);
            }
           
            //rprtMtrx.ForEach(x => rowList[x.Item1-1] = formatRow(x.Item2, x.Item3, maxFieldSize));

            rprtText = string.Join("\n", rowList);

            return rprtText;
        }

        private string formatRow(TextFullRowType rowType, List<string> rowList, int maxFieldSize)
        {
            if (rowType == TextFullRowType.HeaderRow)
            {
                return string.Join(" ", rowList.Select(x => Utilities.FormatUtils.CenterText(x, maxFieldSize)));
            }

            else
            {
                return string.Join(" ", rowList.Select(x => x.PadLeft(maxFieldSize)));
            }
        }

        private string rprtTextDelimitterFormatted()
        {
            string rprtText = string.Empty;

            string separator = string.Empty;

            if (rbnCSV.Checked)
            {
                separator = ",";
            }

            else if (rbnTSV.Checked)
            {
                separator = "\t";
            }

            else if (rbnOtherSeparator.Checked)
            {
                separator = txbOtherSeparator.Text;
            }

            int maxRow =  rprtMtrx.Last().Key;

            string[] rowList = new string[ maxRow ];

            for (int i = 0; i < maxRow; i++) rowList[i] = "";

            foreach (var x in rprtMtrx)
            {
                rowList[x.Key - 1] = string.Join(separator, x.Value.Item3);
            }
            
            rprtText = string.Join("\n", rowList);

            return rprtText;
        }

        private void addLineToRprtMtrx(TextFullRowType rowType, List<string> rprtMtrxLine)
        {

            rprtMtrx.Add(outpRow, new Tuple<int, TextFullRowType, List<string>>(outpRow, rowType, rprtMtrxLine));

        }


        private void addLineToRprtMtrx(int rowNmbr, TextFullRowType rowType, List<string> rprtMtrxLine)
        {

            rprtMtrx.Add(rowNmbr, new Tuple<int, TextFullRowType, List<string>>(rowNmbr, rowType, rprtMtrxLine));

        }

        private void fillListOutToColumn(List<string> rprtMtrxLine, int column)
        {
            while (rprtMtrxLine.Count < column)
            {
                rprtMtrxLine.Add("");
            }
        }
    }
  
    public enum TextFullRowType
    {
        None = 0
        ,HeaderRow = 1
        ,DataRow = 2
        ,TotalRow = 3
    }
}
