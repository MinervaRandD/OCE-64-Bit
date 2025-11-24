

namespace CanvasShapes
{
    using System.Collections.Generic;
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
