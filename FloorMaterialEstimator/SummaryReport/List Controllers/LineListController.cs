
namespace SummaryReport
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using FinishesLib;
    using Utilities;

    public class LineSectionListController : SectionListController
    {

        public double? TotalGrossSF { get; protected set; }

        public double TotalGrossLF { get; protected set; }

        public double? SelectedGrossSF { get; protected set; }

        public double SelectedGrossLF { get; protected set; }


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

        public void CalculateTotals()
        {
            double totalGrossLF = 0.0;
            double? totalGrossSF = null;
           
            double selectedGrossLF = 0.0;
            double? selectedGrossSF = null;

            foreach (LineRow lineRow in this.itemRowDict.Values)
            {
                totalGrossLF += lineRow.LgthInFeet;
                totalGrossSF = MathUtils.AddToNullableDouble(totalGrossSF, lineRow.SqrAreaInFeet);

                if (lineRow.Selected)
                {
                    selectedGrossLF += lineRow.LgthInFeet;
                    selectedGrossSF = MathUtils.AddToNullableDouble(totalGrossSF, lineRow.SqrAreaInFeet);
                }
            }

            TotalGrossLF = totalGrossLF;
            SelectedGrossLF = selectedGrossLF;

            TotalGrossSF = totalGrossSF;
            SelectedGrossSF = selectedGrossSF;

            NotifyTotalsChanged();
        }

        public void CalculateTotals1()
        {
            double totalGrossLF = 0.0;
            double? totalGrossSF = null;

            double selectedGrossLF = 0.0;
            double? selectedGrossSF = null;

            foreach (var lineFinishBase in this.lineFinishBaseList)
            {
                totalGrossLF += lineFinishBase.LgthInFeet;
                totalGrossSF = MathUtils.AddToNullableDouble(totalGrossSF, lineFinishBase.AreaInSqrFeet);

                if (lineFinishBase.Selected)
                {
                    selectedGrossLF += lineFinishBase.LgthInFeet;
                    selectedGrossSF = MathUtils.AddToNullableDouble(totalGrossSF, lineFinishBase.AreaInSqrFeet);
                }
            }

            TotalGrossLF = totalGrossLF;
            SelectedGrossLF = selectedGrossLF;

            TotalGrossSF = totalGrossSF;
            SelectedGrossSF = selectedGrossSF;
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

}
