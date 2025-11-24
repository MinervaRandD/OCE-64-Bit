namespace SummaryReport
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using CanvasLib.Counters;

    public class CntrSectionListController : SectionListController
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

                Total += (double)cntrRow.Count * cntrRow.CounterSize;

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

        private CounterList counterList;
    }
}
