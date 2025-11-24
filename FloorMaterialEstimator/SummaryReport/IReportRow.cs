using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SummaryReport
{
    using System.Windows.Forms;

    public interface IReportRow
    {
    
        ReportRowType ReportRowType { get; }

        int Index { get; set; }

        int LocationOnReport { get; set; }

        string Guid { get;}

        UserControl ControlBase { get; }

        void Delete();

        bool Selected { get; }

        void SetSelectionStatus(bool selectionStatus);

        void UpdateStatsDisplay(bool scaleHasBeenSet);

        bool HasMeasurement { get;  }

        event ReportRowChangedHandler ReportRowChanged;
    }

    public delegate void ReportRowChangedHandler(IReportRow reportRow);
}
