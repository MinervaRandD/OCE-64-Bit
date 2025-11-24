using CanvasLib.Counters;
using CanvasManager.Reports.SummaryReport;
using FinishesLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.CanvasManager.Reporting.Summary_Report
{
    abstract class SectionListController
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
        protected Dictionary<string, IReportRow> itemRowDict = new Dictionary<string, IReportRow>();
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

    class AreaSectionListController : SectionListController
    {
        public double TotalNet { get; private set; }
        public double SelectedNet { get; private set; }

        public AreaSectionListController(Panel panelArea, Control titleRow, AreaFinishBaseList areaFinishBaseList)
            : base(panelArea, titleRow)
        {
            this.areaFinishBaseList = areaFinishBaseList;
        }

        public override void Init()
        {
            addTitleRow();

            int index = 1;

            SetSoftNotifyMode();

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {

                addNewAreaFinishBaseElement(areaFinishBase, index);

                index++;
            }

            this.areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            this.areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            this.areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            this.areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            CalculateTotals();

            LayoutRows();

            NotifyIfTotalsChanged();
        }

        protected override List<IReportRow> GetSortedRowList()
        {
            var currReportRowList = new List<IReportRow>(this.itemRowDict.Values);

            currReportRowList.Sort(ReportRowComparer);

            return currReportRowList;
        }

        private void addNewAreaFinishBaseElement(AreaFinishBase areaFinishBase, int index)
        {
            if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
            {
                RollRow rollRow = new RollRow();

                rollRow.ReportRowChanged += RollRow_ReportRowChanged;

                rollRow.Index = index;

                rollRow.Init(areaFinishBase, scaleHasBeenSet);

                this.itemRowDict.Add(rollRow.Guid, rollRow);

                this.panelArea.Controls.Add(rollRow);
            }

            else
            {
                NonRollRow nonRollRow = new NonRollRow();

                nonRollRow.ReportRowChanged += RollRow_ReportRowChanged;

                nonRollRow.Index = index;

                nonRollRow.Init(areaFinishBase, scaleHasBeenSet);

                this.itemRowDict.Add(nonRollRow.Guid, nonRollRow);

                this.panelArea.Controls.Add(nonRollRow);
            }

            areaFinishBase.MaterialsTypeChanged += AreaFinishBase_MaterialsTypeChanged;
        }

        private void RollRow_ReportRowChanged(IReportRow reportRow)
        {
            CalculateTotals();
        }

        internal void LayoutRows()
        {
            LayoutRows(GetSortedRowList());

        }

        private void CalculateTotals()
        {
            double totalGross = 0.0;
            double totalNet = 0.0;
            double selectedGross = 0.0;
            double selectedNet = 0.0;

            foreach (RollRow rollRow in this.itemRowDict.Values.Where(r => r.ReportRowType == ReportRowType.RollRow))
            {
                totalGross += rollRow.TotalGrossInSqrFeet;

                totalNet += rollRow.TotalNetInSqrFeet;

                if (rollRow.Selected)
                {
                    selectedGross += rollRow.TotalGrossInSqrFeet;

                    selectedNet += rollRow.TotalNetInSqrFeet;
                }
            }

            foreach (NonRollRow nonRollRow in this.itemRowDict.Values.Where(r => r.ReportRowType == ReportRowType.NonRollRow))
            {
                totalGross += nonRollRow.TotalInSqrFeet;

                totalNet += nonRollRow.TotalNetInSqrFeet;

                if (nonRollRow.Selected)
                {
                    selectedGross += nonRollRow.TotalInSqrFeet;

                    selectedNet += nonRollRow.TotalNetInSqrFeet;
                }
            }

            TotalGross = totalGross;
            TotalNet = totalNet;
            SelectedGross = selectedGross;
            SelectedNet = selectedNet;

            NotifyTotalsChanged();
        }


        private void AreaFinishBaseList_ItemAdded(AreaFinishBase areaFinishBase)
        {
            addNewAreaFinishBaseElement(areaFinishBase, 0);

            LayoutRows();
        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase areaFinishBase, int position)
        {
            addNewAreaFinishBaseElement(areaFinishBase, position);

            LayoutRows();
        }


        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            if (!itemRowDict.ContainsKey(guid))
            {
                // This should generate an error. Defensive for now.

                return;
            }

            IReportRow reportRow = itemRowDict[guid];

            reportRow.ReportRowChanged -= RollRow_ReportRowChanged;

            this.panelArea.Controls.Remove(reportRow.ControlBase);

            this.itemRowDict.Remove(guid);

            LayoutRows();
        }

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            LayoutRows();
        }

        private int ReportRowComparer(IReportRow row1, IReportRow row2)
        {
            if (row1.ReportRowType == ReportRowType.RollRow)
            {
                if (row2.ReportRowType != ReportRowType.RollRow)
                {
                    return -1;
                }

                else
                {
                    return row1.Index - row2.Index;
                }
            }

            else 
            {
                return 1;
            }
 
        }

        
        private void AreaFinishBase_MaterialsTypeChanged(AreaFinishBase areaFinishBase, MaterialsType materialsType)
        {
            string guid = areaFinishBase.Guid;

            if (!this.itemRowDict.ContainsKey(guid))
            {
                return;
            }

            IReportRow reportRow = this.itemRowDict[guid];

            if (reportRow.ReportRowType == ReportRowType.RollRow)
            {
                if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    return;
                }

                RollRow rollRow = (RollRow)reportRow;

                NonRollRow nonRollRow = new NonRollRow();

                nonRollRow.Init(areaFinishBase, scaleHasBeenSet);

                nonRollRow.Index = rollRow.Index;

                nonRollRow.SetSelectionStatus(rollRow.Selected);

                this.itemRowDict.Remove(guid);

                this.panelArea.Controls.Remove(rollRow.ControlBase);


                rollRow.Delete();

                this.itemRowDict.Add(nonRollRow.Guid, nonRollRow);

                this.panelArea.Controls.Add(nonRollRow.ControlBase);
            }

            else if (reportRow.ReportRowType == ReportRowType.NonRollRow)
            {
                if (areaFinishBase.MaterialsType == MaterialsType.Tiles)
                {
                    return;
                }

                NonRollRow nonRollRow = (NonRollRow)reportRow;

                RollRow rollRow = new RollRow();

                rollRow.Init(areaFinishBase, scaleHasBeenSet);

                rollRow.Index = nonRollRow.Index;

                rollRow.SetSelectionStatus(nonRollRow.Selected);

                this.itemRowDict.Remove(guid);

                this.panelArea.Controls.Remove(nonRollRow.ControlBase);

                nonRollRow.Delete();

                this.itemRowDict.Add(rollRow.Guid, rollRow);

                this.panelArea.Controls.Add(rollRow.ControlBase);
            }

            LayoutRows();
        }

        public override void Close()
        {
            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                areaFinishBase.MaterialsTypeChanged -= AreaFinishBase_MaterialsTypeChanged;
            }

            foreach (IReportRow row in this.itemRowDict.Values)
            {
                row.ReportRowChanged -= RollRow_ReportRowChanged;
            }

            this.areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            this.areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            this.areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            this.areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;
        }

        private AreaFinishBaseList areaFinishBaseList;
    }

    class LineSectionListController : SectionListController
    {
        public LineSectionListController(Panel panelArea, Control titleRow, LineFinishBaseList lineFinishBaseList)
            : base(panelArea, titleRow)
        {
            this.lineFinishBaseList = lineFinishBaseList;
        }

        public override void Init()
        {
            addTitleRow();

            int index = 1;

            SetSoftNotifyMode();

            foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
            {
                addNewLineFinishBaseElement(lineFinishBase, index);

                index++;
            }

            this.lineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;
            this.lineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;
            this.lineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;
            this.lineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;

            LayoutRows();

            CalculateTotals();

            NotifyIfTotalsChanged();
        }

        protected override List<IReportRow> GetSortedRowList()
        {
            var currReportRowList = new List<IReportRow>(this.itemRowDict.Values);

            currReportRowList.Sort(ReportRowComparer);

            return currReportRowList;
        }

        private void CalculateTotals()
        {
            double totalGross = 0.0;
            double selectedGross = 0.0;

            foreach (LineRow lineRow in this.itemRowDict.Values)
            {
                totalGross += lineRow.LengthInInches;

                if (lineRow.Selected)
                {
                    selectedGross += lineRow.LengthInInches;
                }
            }

            TotalGross = totalGross;
            SelectedGross = selectedGross;

            NotifyTotalsChanged();
        }

        private void LineFinishBaseList_ItemAdded(LineFinishBase lineFinishBase)
        {
            addNewLineFinishBaseElement(lineFinishBase, 0);

            LayoutRows();
        }

        private void LineFinishBaseList_ItemInserted(LineFinishBase lineFinishBase, int position)
        {
            addNewLineFinishBaseElement(lineFinishBase, 0);

            LayoutRows();
        }

        private void addNewLineFinishBaseElement(LineFinishBase lineFinishBase, int index)
        {
            LineRow lineRow = new LineRow();

            lineRow.ReportRowChanged += LineRow_ReportRowChanged;

            lineRow.Index = index;

            lineRow.Init(lineFinishBase, scaleHasBeenSet);

            this.itemRowDict.Add(lineRow.Guid, lineRow);

            this.panelArea.Controls.Add(lineRow);
            //if (lineFinishBase.LengthInInches <= 0)
            //    lineRow.Visible = false;
        }

        private void LineRow_ReportRowChanged(IReportRow reportRow)
        {
            CalculateTotals();
        }

        private void LineFinishBaseList_ItemRemoved(string guid, int position)
        {
            if (!itemRowDict.ContainsKey(guid))
            {
                // This should generate an error. Defensive for now.

                return;
            }

            IReportRow reportRow = itemRowDict[guid];

            this.panelArea.Controls.Remove(reportRow.ControlBase);

            this.itemRowDict.Remove(guid);

            LayoutRows();
        }

        private void LineFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            LayoutRows();
        }

        internal void LayoutRows()
        {
            LayoutRows(GetSortedRowList());

        }

        private int ReportRowComparer(IReportRow row1, IReportRow row2)
        {
            return row1.Index - row2.Index;
        }

        public override void Close()
        {
            foreach (IReportRow row in this.itemRowDict.Values)
            {
                row.ReportRowChanged -= LineRow_ReportRowChanged;
            }

            this.lineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            this.lineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            this.lineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            this.lineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;
        }

        private LineFinishBaseList lineFinishBaseList;
    }

    class SeamSectionListController : SectionListController
    {
        public SeamSectionListController(Panel panelArea, Control titleRow, SeamFinishBaseList seamFinishBaseList) 
            : base(panelArea, titleRow)
        {
            this.seamFinishBaseList = seamFinishBaseList;
        }


        public override void Init()
        {
            addTitleRow();

            int index = 1;

            SetSoftNotifyMode();

            foreach (SeamFinishBase seamFinishBase in seamFinishBaseList)
            {
                addNewSeamFinishBaseElement(seamFinishBase, index);

                index++;
            }

            this.seamFinishBaseList.ItemAdded += SeamFinishBaseList_ItemAdded;
            this.seamFinishBaseList.ItemInserted += SeamFinishBaseList_ItemInserted;
            this.seamFinishBaseList.ItemRemoved += SeamFinishBaseList_ItemRemoved;
            this.seamFinishBaseList.ItemsSwapped += SeamFinishBaseList_ItemsSwapped;

            LayoutRows();

            CalculateTotals();

            NotifyIfTotalsChanged();
        }

        protected override List<IReportRow> GetSortedRowList()
        {
            var currReportRowList = new List<IReportRow>(this.itemRowDict.Values);

            currReportRowList.Sort(ReportRowComparer);

            return currReportRowList;
        }

        private void CalculateTotals()
        {
            double totalGross = 0.0;
            double selectedGross = 0.0;

            foreach (SeamRow seamRow in this.itemRowDict.Values)
            {
                totalGross += seamRow.LengthInInches;

                if (seamRow.Selected)
                {
                    selectedGross += seamRow.LengthInInches;
                }
            }

            TotalGross = totalGross;
            SelectedGross = selectedGross;

            NotifyTotalsChanged();
        }

        private void SeamFinishBaseList_ItemAdded(SeamFinishBase seamFinishBase)
        {
            addNewSeamFinishBaseElement(seamFinishBase, 0);

            LayoutRows();
        }

        private void SeamFinishBaseList_ItemInserted(SeamFinishBase seamFinishBase, int position)
        {
            addNewSeamFinishBaseElement(seamFinishBase, 0);

            LayoutRows();
        }

        private void addNewSeamFinishBaseElement(SeamFinishBase seamFinishBase, int index)
        {
            SeamRow seamRow = new SeamRow();

            seamRow.ReportRowChanged += SeamRow_ReportRowChanged;

            seamRow.Index = index;

            seamRow.Init(seamFinishBase, scaleHasBeenSet);

            this.itemRowDict.Add(seamRow.Guid, seamRow);

            this.panelArea.Controls.Add(seamRow);


        }

        private void SeamRow_ReportRowChanged(IReportRow reportRow)
        {
            CalculateTotals();
        }

        private void SeamFinishBaseList_ItemRemoved(string guid, int position)
        {
            if (!itemRowDict.ContainsKey(guid))
            {
                // This should generate an error. Defensive for now.

                return;
            }

            var seamRow = itemRowDict[guid];

            this.panelArea.Controls.Remove(seamRow.ControlBase);

            this.itemRowDict.Remove(guid);

            LayoutRows();
        }

        private void SeamFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            LayoutRows();
        }

        internal void LayoutRows()
        {
            LayoutRows(GetSortedRowList());
        }

        private int ReportRowComparer(IReportRow row1, IReportRow row2)
        {
            return row1.Index - row2.Index;
        }


        public override void Close()
        {
            foreach (IReportRow row in this.itemRowDict.Values)
            {
                row.ReportRowChanged -= SeamRow_ReportRowChanged;
            }

            this.seamFinishBaseList.ItemAdded -= SeamFinishBaseList_ItemAdded;
            this.seamFinishBaseList.ItemInserted -= SeamFinishBaseList_ItemInserted;
            this.seamFinishBaseList.ItemRemoved -= SeamFinishBaseList_ItemRemoved;
            this.seamFinishBaseList.ItemsSwapped -= SeamFinishBaseList_ItemsSwapped;
        }

        private SeamFinishBaseList seamFinishBaseList;
    }


    class CntrSectionListController : SectionListController
    {
        public delegate void CountOrTotalChangedHandler(Counter counter);

        public event CountOrTotalChangedHandler CountOrTotalChanged;

        public CntrSectionListController(Panel panelArea, Control titleRow, CounterList counterList)
            : base(panelArea, titleRow)
        {
            this.counterList = counterList;
        }

        public override void Init()
        {
            addTitleRow();

            int index = 1;

            SetSoftNotifyMode();

            foreach (Counter counter in counterList.Counters)
            {
                addNewCounterBaseElement(counter, index);

                index++;
            }

            LayoutRows();

            CalculateTotals(false);

            NotifyIfTotalsChanged();
        }

        protected override List<IReportRow> GetSortedRowList()
        {
            var currReportRowList = new List<IReportRow>(this.itemRowDict.Values);

            currReportRowList.Sort(ReportRowComparer);

            return currReportRowList;
        }

        public void CalculateTotals(bool showSelectedOnly)
        {
            Count = 0;

            Total = 0;

            foreach (IReportRow iCntrRow in this.itemRowDict.Values)
            {
                CntrRow cntrRow = (CntrRow)iCntrRow;

                if (showSelectedOnly && !cntrRow.Selected)
                {
                    continue;
                }

                Count += cntrRow.Count;

                Total += (double) cntrRow.Count * cntrRow.CounterSize;

            }
           
            //NotifyTotalsChanged();
        }

        private void addNewCounterBaseElement(Counter counter, int index)
        {
            CntrRow cntrRow = new CntrRow();

            cntrRow.ReportRowChanged += CntrRow_ReportRowChanged;

            cntrRow.Index = index;

            cntrRow.Init(counter);

            this.itemRowDict.Add(cntrRow.Guid, cntrRow);

            this.panelArea.Controls.Add(cntrRow);


        }

        private void CntrRow_ReportRowChanged(IReportRow reportRow)
        {
            if (CountOrTotalChanged != null)
            {
                CountOrTotalChanged.Invoke(((CntrRow)reportRow).Counter);
            }
        }

        internal void LayoutRows()
        {
            LayoutRows(GetSortedRowList());
        }

        private int ReportRowComparer(IReportRow row1, IReportRow row2)
        {
            return row1.Index - row2.Index;
        }


        public override void Close()
        {
            foreach (IReportRow row in this.itemRowDict.Values)
            {
                row.ReportRowChanged -= CntrRow_ReportRowChanged;
            }

        }

        public int Count { get; private set; }

        public double Total { get; private set; }

        private CounterList counterList ;
    }
}
