using CanvasLib.Counters;
using SummaryReport;
using FinishesLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SummaryReport
{
    public abstract class SectionListController
    {
        public double TotalGross { get; protected set; }

        public double SelectedGross { get; protected set; }


        public event EventHandler TotalsChanged;

        public SectionListController(Panel panelArea, Control titleRow)
        {
            this.panelArea = panelArea;
            this.titleRow = titleRow;
        }

        public abstract void Init();
        protected abstract List<IReportRow> GetSortedRowList();

        protected Panel panelArea;
        protected Control titleRow;
        public Dictionary<string, IReportRow> itemRowDict = new Dictionary<string, IReportRow>();
        protected bool scaleHasBeenSet = true;

        protected bool notifyTotalsChanged = false;
        protected bool softNotifyMode = false;
        protected bool suppressZeroMaterials = false;

        public void SetZeroMaterialsMode(bool set)
        {
            if (set != suppressZeroMaterials)
            {
                suppressZeroMaterials = set;
                LayoutRows(GetSortedRowList());
            }
        }

        protected void SetSoftNotifyMode()
        {
            this.notifyTotalsChanged = false;
            this.softNotifyMode = true;
        }

        //protected int rowHeight;

        protected void addTitleRow()
        {
            //container.Controls.Add(titleRow);
            panelArea.Parent.Controls.Add(titleRow);

            titleRow.Location = new Point(
                panelArea.Location.X,
                panelArea.Location.Y - titleRow.Height - 4);
        }

        protected void LayoutRows(List<IReportRow> currReportRowList)
        {
            int xPos = 8;
            int yPos = 1;

            this.panelArea.Controls.Clear();

            int positionOnReport = 1;

            foreach (IReportRow reportRow in currReportRowList)
            {
                if (this.suppressZeroMaterials && !reportRow.HasMeasurement)
                {
                    reportRow.ControlBase.Visible = false;
                }
                else
                {
                    this.panelArea.Controls.Add(reportRow.ControlBase);

                    reportRow.ControlBase.Visible = true;

                    reportRow.LocationOnReport = positionOnReport++;

                    reportRow.ControlBase.Location = new Point(xPos, yPos);

                    yPos += reportRow.ControlBase.Height;
                }
            }
        }

        protected void NotifyTotalsChanged()
        {
            if (this.softNotifyMode)
            {
                this.notifyTotalsChanged = true;
            }
            else if (TotalsChanged != null)
            {
                TotalsChanged(this, new EventArgs());

                this.notifyTotalsChanged = false;
                this.softNotifyMode = false;
            }

        }

        protected void NotifyIfTotalsChanged()
        {
            if (this.notifyTotalsChanged)
                this.softNotifyMode = false;
                NotifyTotalsChanged();
        }

        protected void SoftNotifyTotalsChanged()
        {
            this.notifyTotalsChanged = true;
        }

        public abstract void Close();
    }

}
