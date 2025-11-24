


namespace SummaryReport
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using FinishesLib;

    public class AreaSectionListController : SectionListController
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
                TileRow nonRollRow = new TileRow();

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
                if (rollRow.TotalGrossInSqrFeet.HasValue)
                {
                    totalGross += rollRow.TotalGrossInSqrFeet.Value;
                }
               
                totalNet += rollRow.TotalNetInSqrFeet;

                if (rollRow.Selected)
                {
                    if (rollRow.TotalGrossInSqrFeet.HasValue)
                    {
                        selectedGross += rollRow.TotalGrossInSqrFeet.Value;
                    }
                    
                    selectedNet += rollRow.TotalNetInSqrFeet;
                }
            }

            foreach (TileRow nonRollRow in this.itemRowDict.Values.Where(r => r.ReportRowType == ReportRowType.NonRollRow))
            {
                if (nonRollRow.TotalInSqrFeet.HasValue)
                {
                    totalGross += nonRollRow.TotalInSqrFeet.Value;
                }
                totalNet += nonRollRow.TotalNetInSqrFeet;

                if (nonRollRow.Selected)
                {
                    if (nonRollRow.TotalInSqrFeet.HasValue)
                    {
                        selectedGross += nonRollRow.TotalInSqrFeet.Value;
                    }

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

                TileRow nonRollRow = new TileRow();

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

                TileRow nonRollRow = (TileRow)reportRow;

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

}
