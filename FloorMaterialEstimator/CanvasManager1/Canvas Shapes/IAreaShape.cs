

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Finish_Controls;
    using Graphics;

    public interface IAreaShape: IGraphicsShape
    {
        List<CanvasDirectedLine> Perimeter { get; set; }

        Shape PolygonInternalArea { get; set; }

        AreaShapeBuildStatus BuildStatus { get; set; }

        AreaFinishManager AreaFinishManager { get; set; }

        double PerimeterLength();

        void SetFillColor(string visioFillColorFormula);

        void SetFillTransparancy(string visioFillTransparencyFormula);

        double NetAreaInSqrInches();
    }
}
