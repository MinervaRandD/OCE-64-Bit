namespace SummaryReport
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using FinishesLib;

    public class SeamSectionListController : SectionListController
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


}
