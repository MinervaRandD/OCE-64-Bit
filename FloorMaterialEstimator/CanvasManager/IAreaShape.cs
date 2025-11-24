

namespace CanvasManager
{
    using System.Collections.Generic;
    using global::CanvasManager.CanvasShapes;
    using global::CanvasManager.FinishManager;
    using Graphics;
    using CanvasManagerLib.FinishManager;

    public interface IAreaShape: IGraphicsShape
    {
        List<CanvasDirectedLine> Perimeter { get; set; }

        GraphicShape PolygonInternalArea { get; set; }

        AreaShapeBuildStatus BuildStatus { get; set; }

        AreaFinishManager AreaFinishManager { get; set; }

        double PerimeterLength();

        void SetFillColor(string visioFillColorFormula);

        void SetFillTransparancy(string visioFillTransparencyFormula);

        double NetAreaInSqrInches();
    }
}
