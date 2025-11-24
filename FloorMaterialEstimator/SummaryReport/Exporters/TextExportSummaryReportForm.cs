using FinishesLib;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace SummaryReport.Exporters
{
    public partial class TextExportSummaryReportForm : Form
    {
        AreaSectionListController areaSectionListController;

        public TextExportSummaryReportForm()
        {
            InitializeComponent();

            this.FormClosed += TextExportSummaryReportForm_FormClosed;
        }

        private void TextExportSummaryReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryUtils.SetRegistryValue("TextExportSummaryReportNonZeroItemsOnly", ckbNonZeroItemsOnly.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportSummaryReportCheckedItemsOnly", ckbCheckedItemsOnly.Checked.ToString());

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

            RegistryUtils.SetRegistryValue("TextExportSummaryOutpFmt",outpFmt);

            RegistryUtils.SetRegistryValue("TextExportSummaryOtherSeparator", txbOtherSeparator.Text.Trim());

            RegistryUtils.SetRegistryValue("TextExportSummaryReportIncludeTotals", ckbIncludeTotals.Checked.ToString());
            RegistryUtils.SetRegistryValue("TextExportSummaryReportIncludeHeaders", ckbIncludeHeaders.Checked.ToString());

            string exportTo = "File";

            if (rbnExportToFile.Checked)
            {
                exportTo = "File";
            }

            else if (rbnExportToClipboard.Checked)
            {
                exportTo = "Clipboard";
            }

            RegistryUtils.SetRegistryValue("TextExportSummaryReportExportTo", exportTo);

        }

        public void Init(
            AreaSectionListController areaSectionListController
            )
        {
            this.areaSectionListController = areaSectionListController;

            ckbNonZeroItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("TextExportSummaryReportNonZeroItemsOnly", "False") == "True";
            ckbCheckedItemsOnly.Checked = RegistryUtils.GetRegistryStringValue("TextExportSummaryReportCheckedItemsOnly", "False") == "True";

            string outpFmt = RegistryUtils.GetRegistryStringValue("TextExportSummaryOutpFmt", "CSV") ;

            switch (outpFmt)
            {
                case "CSV": rbnCSV.Checked = true; break;
                case "TSV": rbnTSV.Checked = true; break;
                case "ColumnFormatted": rbnColumnFormatted.Checked = true; break;
                case "OtherSeparator": rbnOtherSeparator.Checked = true; break;
                default: rbnCSV.Checked = true; break;
            }


            txbOtherSeparator.Text = RegistryUtils.GetRegistryStringValue("TextExportSummaryOtherSeparator", "");

            ckbIncludeTotals.Checked = RegistryUtils.GetRegistryStringValue("TextExportSummaryReportIncludeTotals", "False") == "True";
            ckbIncludeHeaders.Checked = RegistryUtils.GetRegistryStringValue("TextExportSummaryReportIncludeHeaders", "False") == "True";

            string exportTo = RegistryUtils.GetRegistryStringValue("TextExportSummaryReportExportTo", "File") ;

            switch (exportTo)
            {
                case "File": rbnExportToFile.Checked = true; break;
                case "Clipboard": rbnExportToClipboard.Checked = true; break;
                default: rbnExportToFile.Checked = true; break;
            }
        }

        int rprtRows = 0;
        int rprtCols = 0;

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

            string rprtText = generateReport();

            exportReport(rprtText);

        }

        private string generateReport()
        {
            generateReportMtrx();
            string rprtText = generateReportText();

            return rprtText;
        }


        double totlGross = 0;
        double totlNet = 0;

        List<TextSummaryMtrxRow> rprtMtrx = new List<TextSummaryMtrxRow>();

        private void generateReportMtrx()
        {
            totlGross = 0;
            totlNet = 0;

            rprtMtrx.Clear();

            if (this.ckbIncludeHeaders.Checked)
            {
                rprtMtrx.Add(new TextSummaryMtrxRow(TextSummaryMtrxRowType.headRow, "Material", "Net", "Gross", "Waste %"));
            }

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

            if (this.ckbIncludeTotals.Checked)
            {
                string wastePctStr = "";

                if (totlNet > 0)
                {
                    wastePctStr = (100.0 * (totlGross / totlNet - 1.0)).ToString("#,##0.0");
                }

                rprtMtrx.Add(new TextSummaryMtrxRow(
                    TextSummaryMtrxRowType.totlRow
                    ,"Total"
                    , totlNet.ToString("#,##0.0")
                    , totlGross.ToString("#,##0.0")
                    , wastePctStr));
            }
        }

        private TextSummaryMtrxRow generateReportRow(RollRow rollRow)
        {
            AreaFinishBase areaFinishBase = rollRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            totlNet += netAreaInSqrFeet;

            if (grossAreaInSqrFeet.HasValue)
            {
                totlGross += grossAreaInSqrFeet.Value;
            }

            TextSummaryMtrxRow rtrnValu = new TextSummaryMtrxRow(
                TextSummaryMtrxRowType.dataRow
                , areaFinishBase.AreaName
                , netAreaInSqrFeet.ToString("#,##0.0")
                , grossAreaInSqrFeet.HasValue ? grossAreaInSqrFeet.Value.ToString("#,##0.0") : ""
                , wastePct.HasValue ? (wastePct.Value * 100.0).ToString("0.0") : "");

            return rtrnValu;
        }

        private TextSummaryMtrxRow generateReportRow(TileRow tileRow)
        {
            AreaFinishBase areaFinishBase = tileRow.AreaFinishBase;

            double? grossAreaInSqrFeet = areaFinishBase.GrossAreaInSqrInches / 144.0;

            double netAreaInSqrFeet = areaFinishBase.NetAreaInSqrInches / 144.0;

            double? wastePct = areaFinishBase.WastePct;

            totlNet += netAreaInSqrFeet;

            if (grossAreaInSqrFeet.HasValue)
            {
                totlGross += grossAreaInSqrFeet.Value;
            }

            TextSummaryMtrxRow rtrnValu = new TextSummaryMtrxRow(
                TextSummaryMtrxRowType.dataRow
                , areaFinishBase.AreaName
                , netAreaInSqrFeet.ToString("#,##0.0")
                , grossAreaInSqrFeet.HasValue ? grossAreaInSqrFeet.Value.ToString("#,##0.0") : ""
                , wastePct.HasValue ? (wastePct.Value * 100.0).ToString("0.0") : "");

            return rtrnValu;
        }

        private string generateReportText()
        {
            if (this.rbnCSV.Checked)
            {
                return generateReportTextWithDelimiter(",");
            }

            if (this.rbnTSV.Checked)
            {
                return generateReportTextWithDelimiter("\t");
            }

            if (this.rbnOtherSeparator.Checked)
            {
                return generateReportTextWithDelimiter(this.txbOtherSeparator.Text);
            }

            if (this.rbnColumnFormatted.Checked)
            {
                return generateReportTextColumnFormatted();
            }

            return string.Empty;
        }

        private string generateReportTextWithDelimiter(string delimiter)
        {
            string rtrnValu = string.Empty;

            this.rprtMtrx.ForEach(x => rtrnValu += x.title + delimiter + x.net + delimiter + x.gross + delimiter + x.waste + "\n");
           
            return rtrnValu;
        }

        private string generateReportTextColumnFormatted()
        {
            int fieldTitleWidth = Math.Max(this.rprtMtrx.Max(x => x.title.Length), 1);
            int fieldNetWidth   = Math.Max(this.rprtMtrx.Max(x => x.net.Length), 1);
            int fieldGrossWidth = Math.Max(this.rprtMtrx.Max(x => x.gross.Length), 1);
            int fieldWasteWidth = Math.Max(this.rprtMtrx.Max(x => x.waste.Length), 1);


            string rtrnValu = string.Empty;

            for (int i = 0; i < this.rprtMtrx.Count; i++)
            {
                TextSummaryMtrxRow mtrxRow = this.rprtMtrx[i];

                if (mtrxRow.rowType == TextSummaryMtrxRowType.headRow)
                {
                    rtrnValu
                            += FormatUtils.CenterText(mtrxRow.title, fieldTitleWidth)
                            + "  " + FormatUtils.CenterText(mtrxRow.net, fieldNetWidth)
                            + "  " + FormatUtils.CenterText(mtrxRow.gross, fieldGrossWidth)
                            + "  " + FormatUtils.CenterText(mtrxRow.waste, fieldWasteWidth)
                            + "\n";
                }

                else if (mtrxRow.rowType == TextSummaryMtrxRowType.dataRow)
                {
                    rtrnValu += mtrxRow.title.PadRight(fieldTitleWidth)
                        + "  " + mtrxRow.net.PadLeft(fieldNetWidth)
                        + "  " + mtrxRow.net.PadLeft(fieldNetWidth)
                        + "  " + mtrxRow.net.PadLeft(fieldNetWidth)
                        + "\n";
                }

                else if (mtrxRow.rowType == TextSummaryMtrxRowType.totlRow)
                {
                    rtrnValu += mtrxRow.title.PadLeft(fieldTitleWidth)
                        + "  " + mtrxRow.net.PadLeft(fieldNetWidth)
                        + "  " + mtrxRow.net.PadLeft(fieldNetWidth)
                        + "  " + mtrxRow.net.PadLeft(fieldNetWidth)
                        + "\n";
                }
            }
           
            return rtrnValu;
        }

        private void exportReport(string rprtText)
        {

            if (this.rbnExportToFile.Checked)
            {
                exportToFile(rprtText);

                return;
            }

            if (this.rbnExportToClipboard.Checked)
            {
                exportToClipboard(rprtText); 
            }
        }

        private void exportToFile(string rprtText)
        {
            string defaultExportPath = RegistryUtils.GetRegistryStringValue("TextExportSummaryReportDefaultPath", string.Empty).Trim() ;

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

            RegistryUtils.SetRegistryValue("TextExportSummaryReportDefaultPath", actualExportPath);
        }

        private void exportToClipboard(string rprtText)
        {
            System.Windows.Forms.Clipboard.SetText(rprtText);

            MessageBox.Show("Report has been written to the clipboard.");
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

    public enum TextSummaryMtrxRowType
    {
        None = 0
        ,headRow = 1
        ,dataRow = 2
        ,totlRow = 3
    }

    public struct TextSummaryMtrxRow
    {
        public TextSummaryMtrxRowType rowType;
        public string title;
        public string net;
        public string gross;
        public string waste;

        public TextSummaryMtrxRow(
            TextSummaryMtrxRowType rowType
            , string title
            , string net
            , string gross
            , string waste)
        {
            this.rowType = rowType;
            this.title = title;
            this.net = net;
            this.gross = gross;
            this.waste = waste;
        }
    }
}
