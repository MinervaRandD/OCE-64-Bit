

namespace FloorMaterialEstimator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Utilities;

    public class PerimeterLine: GraphicsLine
    {
        public PerimeterLine(double x1, double y1, double x2, double y2, Perimeter perimeter, UCLine ucLine, CanvasManager canvasManager)
            : base(x1, y1, x2, y2, ucLine, canvasManager)
        {
            this.Perimeter = perimeter;

            this.LineType = LineType.PerimeterLine;
        }


        public Perimeter Perimeter { get; set; } = null;

        internal override void SetLineGraphicsForLineMode()
        {
            Graphics.SetLineWidth(this, ucLine.LineWidthInPts);

            if (this.IsZeroLine)
            {
                Graphics.SetBaseLineStyle(this, "0");
            }

            else
            {
                Graphics.SetBaseLineStyle(this, ucLine.visioLineStyleFormula);
            }
        }

        internal override void SetLineGraphicsForAreaMode()
        {
            AreaShapeBuildStatus buildStatus = Perimeter.BuildStatus;

            if (buildStatus == AreaShapeBuildStatus.Building)
            {
                SetBaseLineWidth(ucLine.LineWidthInPts);
            }

            else if (buildStatus == AreaShapeBuildStatus.Completed)
            {
                SetBaseLineWidth(CanvasManager.CompletedShapeLineWidthInPts);
            }

            if (this.IsZeroLine)
            {
                SetBaseLineStyle(CanvasManager.ZeroLineStyleFormula);
            }

            else
            {
                SetBaseLineStyle(ucLine.visioLineStyleFormula);
            }

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Development)
            {

            }
        }
    }
}
