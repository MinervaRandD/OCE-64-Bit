

namespace FloorMaterialEstimator.OversUndersForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Utilities;
    using System.IO;
    using System.Text.RegularExpressions;

    public partial class ExportReportForm : Form
    {
        Panel pnlCuts;

        Panel pnlOvers;

        Panel pnlUnders;

        public ExportReportForm(Panel pnlCuts, Panel pnlOvers, Panel pnlUnders)
        {
            InitializeComponent();

            this.pnlCuts = pnlCuts;
            this.pnlOvers = pnlOvers;
            this.pnlUnders = pnlUnders;

            if (RegistryUtils.GetRegistryStringValue("ExportFormExportCuts", "True") == "True")
            {
                this.ckbExportCuts.Checked = true;
            }

            else
            {
                this.ckbExportCuts.Checked = false;
            }

            if (RegistryUtils.GetRegistryStringValue("ExportFormExportOvers", "False") == "True")
            {
                this.ckbExportOvers.Checked = true;
            }

            else
            {
                this.ckbExportOvers.Checked = false;
            }

            if (RegistryUtils.GetRegistryStringValue("ExportFormExportUnders", "False") == "True")
            {
                this.ckbExportUnders.Checked = true;
            }

            else
            {
                this.ckbExportUnders.Checked = false;
            }

            string outputFormat = RegistryUtils.GetRegistryStringValue("ExportFormFormat", "CSV");
            
            if (outputFormat == "CSV")
            {
                this.rbnCSVFormat.Checked = true;
            }

            else if (outputFormat == "TSV")
            {
                this.rbnTSVFormat.Checked = true;
            }

            else if (outputFormat == "Text")
            {
                this.rbnFormattedTextFormat.Checked = true;
            }

            else
            {
                this.rbnCSVFormat.Checked = true;
            }

            string outputTo = RegistryUtils.GetRegistryStringValue("ExportFormExportTo", "File");

            if (outputTo == "File")
            {
                this.rbnExportToFile.Checked = true;
            }

            else if (outputTo == "Clipboard")
            {
                this.rbnCopyToClipboard.Checked = true;
            }

            else
            {
                this.rbnExportToFile.Checked = true;
            }

            if (RegistryUtils.GetRegistryStringValue("ExportFormIncludeHeader", "True") == "True")
            {
                this.ckbIncludeHeader.Checked = true;
            }

            else
            {
                this.ckbIncludeHeader.Checked = false;
            }

            this.ckbExportCuts.CheckedChanged += new System.EventHandler(this.ckbExportCuts_CheckedChanged);
            this.ckbExportOvers.CheckedChanged += new System.EventHandler(this.ckbExportOvers_CheckedChanged);
            this.ckbExportUnders.CheckedChanged += new System.EventHandler(this.ckbExportUnders_CheckedChanged);
            this.rbnCSVFormat.CheckedChanged += new System.EventHandler(this.rbnCSVFormat_CheckedChanged);
            this.rbnTSVFormat.CheckedChanged += new System.EventHandler(this.rbnTSVFormat_CheckedChanged);
            this.rbnFormattedTextFormat.CheckedChanged += new System.EventHandler(this.rbnFormattedTextFormat_CheckedChanged);
            this.rbnExportToFile.CheckedChanged += RbnExportToFile_CheckedChanged;
            this.rbnCopyToClipboard.CheckedChanged += RbnCopyToClipboard_CheckedChanged;
            this.ckbIncludeHeader.CheckedChanged += CkbIncludeHeader_CheckedChanged;
        }

        private void ckbExportCuts_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbExportCuts.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormExportCuts", "True");
            }

            else
            {
                RegistryUtils.SetRegistryValue("ExportFormExportCuts", "False");
            }
        }

        private void ckbExportOvers_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbExportOvers.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormExportOvers", "True");
            }

            else
            {
                RegistryUtils.SetRegistryValue("ExportFormExportOvers", "False");
            }
        }

        private void ckbExportUnders_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbExportUnders.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormExportUnders", "True");
            }

            else
            {
                RegistryUtils.SetRegistryValue("ExportFormExportUnders", "False");
            }
        }

        private void rbnCSVFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnCSVFormat.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormFormat", "CSV");
            }
        }

        private void rbnTSVFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnTSVFormat.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormFormat", "TSV");
            }
        }

        private void rbnFormattedTextFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnFormattedTextFormat.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormFormat", "Text");
            }
        }

        private void RbnExportToFile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnExportToFile.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormExportTo", "File");
            }
        }

        private void RbnCopyToClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnCopyToClipboard.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormExportTo", "Clipboard");
            }
        }

        private void CkbIncludeHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIncludeHeader.Checked)
            {
                RegistryUtils.SetRegistryValue("ExportFormIncludeHeader", "True");
            }

            else
            {
                RegistryUtils.SetRegistryValue("ExportFormIncludeHeader", "False");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string exportText = generateReport();

            if (string.IsNullOrEmpty(exportText))
            {
                MessageBox.Show("Generated report is empty.");

                return;
            }

            if (this.rbnCopyToClipboard.Checked)
            {
                Clipboard.SetText(exportText);

                MessageBox.Show("Report copied to the clipboard.");

                return;
            }

            string initialOutputFilePath = RegistryUtils.GetRegistryStringValue("ExportFormOutputFile", @"C:");

            SaveFileDialog sfd = new SaveFileDialog();

            if (initialOutputFilePath != "C:")
            {
                string outputFileFolder = Path.GetDirectoryName(initialOutputFilePath);
                string outputFile = Path.GetFileName(initialOutputFilePath);

                sfd.InitialDirectory = outputFileFolder;

                sfd.FileName = outputFile;
            }

            DialogResult dr = sfd.ShowDialog(this);

            if (dr != DialogResult.OK)
            {
                return;
            }

            string outputFilePath = sfd.FileName;

            File.WriteAllText(outputFilePath, exportText);

            RegistryUtils.SetRegistryValue("ExportFormOutputFile", outputFilePath);

            MessageBox.Show("Report written to " + outputFilePath + ".", "Report Written");
        }

        private string generateReport()
        {
            List<string> reportTextList = new List<string>();

            if (this.ckbExportCuts.Checked)
            {
                string reportText = generateCutsReport();

                if (!string.IsNullOrEmpty(reportText))
                {
                    reportTextList.Add(reportText);
                }
            }

            if (this.ckbExportOvers.Checked)
            {
                string reportText = generateOversReport();

                if (!string.IsNullOrEmpty(reportText))
                {
                    reportTextList.Add(reportText);
                }
            }

            if (this.ckbExportUnders.Checked)
            {
                string reportText = generateUndersReport();

                if (!string.IsNullOrEmpty(reportText))
                {
                    reportTextList.Add(reportText);
                }
            }

            if (reportTextList.Count <= 0)
            {
                return string.Empty;
            }

            return string.Join("\n\n", reportTextList);
        }

        private string generateCutsReport()
        {
            List<string> reportLines = new List<string>();

            if (this.ckbIncludeHeader.Checked)
            {
                reportLines.Add(generateReportHeader("Cuts"));
            }

            foreach (Control control in pnlCuts.Controls)
            {
                string reportLine = generateCutsReportLine(control);

                if (!string.IsNullOrEmpty(reportLine))
                {
                    reportLines.Add(reportLine);
                }
            }

            if (reportLines.Count <= 0)
            {
                return string.Empty;
            }

            return string.Join("\n", reportLines);
        }

        private string generateCutsReportLine(Control control)
        {
            if (control is UCCutsOversUndersFormElement)
            {
                UCCutsOversUndersFormElement formElement = (UCCutsOversUndersFormElement)control;

                string index = formElement.Index;
                string width = formElement.Width;
                string length = formElement.Length;
                string repeats = formElement.txbRepeats.Text;

               // string repeat = formElement.txbRepeats.Text;

                if (!string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(length))
                {
                    return generateReportLine(index, width, length, repeats);
                }

                return string.Empty;
            }

            else if (control is UCFillSummaryElement)
            {
                UCFillSummaryElement formElement = (UCFillSummaryElement)control;

                string index = formElement.Index;
                string width = formElement.Width;
                string length = formElement.Length;

                if (!string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(length))
                {
                    return generateReportLine(index, width, length);
                }

                return string.Empty;
            }

            else if (control is UCCutsExtraFormElement)
            {
                UCCutsExtraFormElement formElement = (UCCutsExtraFormElement)control;

                string index = formElement.Index;
                string width = formElement.Width;
                string length = formElement.Length;

                if (!string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(length))
                {
                    return generateReportLine(index, width, length);
                }

                return string.Empty;
            }

            return string.Empty;
        }

        private string generateOversReport()
        {
            List<string> reportLines = new List<string>();

            if (this.ckbIncludeHeader.Checked)
            {
                reportLines.Add(generateReportHeader("Overs"));
            }

            foreach (Control control in pnlOvers.Controls)
            {
                string reportLine = generateOversReportLine(control);

                if (!string.IsNullOrEmpty(reportLine))
                {
                    reportLines.Add(reportLine);
                }
            }

            if (reportLines.Count <= 0)
            {
                return string.Empty;
            }

            return string.Join("\n", reportLines);
        }

        private string generateOversReportLine(Control control)
        {
            if (control is UCOversOversUndersFormElement)
            {
                UCOversOversUndersFormElement formElement = (UCOversOversUndersFormElement)control;

                string index = formElement.Index;
                string width = formElement.Width;
                string length = formElement.Length;

                if (!string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(length))
                {
                    return generateReportLine(index, width, length);
                }
            }

            return string.Empty;
        }

        private string generateUndersReport()
        {
            List<string> reportLines = new List<string>();

            if (this.ckbIncludeHeader.Checked)
            {
                reportLines.Add(generateReportHeader("Unders"));
            }

            foreach (Control control in pnlUnders.Controls)
            {
                string reportLine = generateUndersReportLine(control);

                if (!string.IsNullOrEmpty(reportLine))
                {
                    reportLines.Add(reportLine);
                }
            }

            if (reportLines.Count <= 0)
            {
                return string.Empty;
            }

            return string.Join("\n", reportLines);
        }

        private string generateUndersReportLine(Control control)
        {
            if (control is UCUndrsOversUndersFormElement)
            {
                UCUndrsOversUndersFormElement formElement = (UCUndrsOversUndersFormElement)control;

                string index = formElement.Index;
                string width = formElement.Width;
                string length = formElement.Length;

                if (!string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(length))
                {
                    return generateReportLine(index, width, length);
                }
            }

            return string.Empty;
        }

        private string generateReportHeader(string reportType)
        {
            if (this.rbnCSVFormat.Checked)
            {
                return reportType + "\nIndex,Width,Length";
            }

            else if (this.rbnTSVFormat.Checked)
            {
                return reportType + "\nIndex\tWidth\tLength";
            }

            else
            {
                int pad = (28 - reportType.Length) / 2 + reportType.Length;
                
                return reportType.PadLeft(pad) + "\n Index" + ' ' + "    Width " + " X " + "  Length";
            }
        }

        private string generateReportLine(string index, string width, string length, string repeats = null)
        {
            index = Regex.Replace(index, @"\s+", " ");
            width = Regex.Replace(width, @"\s+", " ");
            length = Regex.Replace(length, @"\s+", " ");

            string separator = " ";
            
            if (this.rbnCSVFormat.Checked)
            {
                separator = "," ;
            }

            else if (this.rbnTSVFormat.Checked)
            {
                separator = "\t";
            }

            else
            {
                separator = " ";
            }

            if (string.IsNullOrEmpty(repeats))
            {
                return string.Join(separator, index, width, length);
            }

            else
            {
                return string.Join(separator, index, width, length, repeats);
            }
        }

    }
}
