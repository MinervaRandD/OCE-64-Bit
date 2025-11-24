using Geometry;
using Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO.Ports;
using FinishesLib;
using Utilities;

namespace FloorMaterialEstimator.CanvasManager
{
    public class PatternFill: List<GraphicsDirectedLine>
    {
        CanvasLayoutArea canvasLayoutArea = null;

        AreaFinishManager areaFinishManager;

        AreaFinishBase areaFinishBase => areaFinishManager.AreaFinishBase;

        public Color lineColor => areaFinishBase.Color;

        double lineWidthInPts => areaFinishBase.FillPatternLineWidthInPts;

        int lineStyle => areaFinishBase.FillPatternLineStyle;

        //double interlineDistanceInFt => areaFinishBase.FillPatternInterlineDistanceInFt;

        public double interlineDistanceInFt = 0.25;

        int pattern => areaFinishBase.Pattern;

        public PatternFill() { }

        public PatternFill(CanvasLayoutArea canvasLayoutArea, AreaFinishManager areaFinishManager)
        {
            this.canvasLayoutArea = canvasLayoutArea;

            this.areaFinishManager = areaFinishManager;

            interlineDistanceInFt = areaFinishBase.FillPatternInterlineDistanceInFt;

            areaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;

            areaFinishBase.OpacityChanged += AreaFinishBase_OpacityChanged;

            areaFinishBase.FillPatternLineWidthChanged += AreaFinishBase_FillPatternLineWidthChanged;

            areaFinishBase.FillPatternLineStyleChanged += AreaFinishBase_FillPatternLineStyleChanged;

            //areaFinishBase.FillPatternLineInterlineDistanceInFtChanged += AreaFinishBase_FillPatternLineInterlineDistanceInFtChanged;
            areaFinishBase.PatternChanged += AreaFinishBase_PatternChanged;
        }

        //public PatternFill(
        //    CanvasLayoutArea canvasLayoutArea
        //    ,AreaFinishManager areaFinishManager
        //    ,IEnumerable<GraphicsDirectedLine> lines): this(canvasLayoutArea, areaFinishManager)
        //{
        //    this.AddRange(lines);
        //}

        public void GeneratePatternLines()
        {
            this.Clear();

            if (pattern == 0)
            {
                return;
            }

            Coordinate centeralCoord = canvasLayoutArea.VoronoiLabelLocation();

            if (pattern == 1 || pattern == 3)
            {
                Coordinate coord1 = new Coordinate(centeralCoord.X - 1, centeralCoord.Y);
                Coordinate coord2 = new Coordinate(centeralCoord.X + 1, centeralCoord.Y);

                DirectedLine baseLine = new DirectedLine(coord1, coord2);

                canvasLayoutArea.GenerateSeamsAndRolloutsPatternFill(baseLine);

                foreach (MaterialsLayout.Seam seam in canvasLayoutArea.SeamList)
                {
                    GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(canvasLayoutArea.Window, canvasLayoutArea.Page, seam.ToDirectedLine(), LineRole.PatternLine, false);

                    graphicsDirectedLine.Guid = GuidMaintenance.CreateGuid(graphicsDirectedLine);

                    this.Add(graphicsDirectedLine);
                }

                canvasLayoutArea.RolloutList.Clear();
                canvasLayoutArea.CutList.Clear();
                canvasLayoutArea.SeamList.Clear();
            }

            if (pattern == 2 || pattern == 3)
            {
                Coordinate coord1 = new Coordinate(centeralCoord.X, centeralCoord.Y - 1);
                Coordinate coord2 = new Coordinate(centeralCoord.X, centeralCoord.Y + 1);

                DirectedLine baseLine = new DirectedLine(coord1, coord2);

                canvasLayoutArea.GenerateSeamsAndRolloutsPatternFill(baseLine);

                foreach (MaterialsLayout.Seam seam in canvasLayoutArea.SeamList)
                {
                    GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(canvasLayoutArea.Window, canvasLayoutArea.Page, seam.ToDirectedLine(), LineRole.PatternLine, false);

                    this.Add(graphicsDirectedLine);
                }

                canvasLayoutArea.RolloutList.Clear();
                canvasLayoutArea.CutList.Clear();
                canvasLayoutArea.SeamList.Clear();
            }

            //TestAndDebug.DumpPatternLines((List<GraphicsDirectedLine>) this);

        }

        public void Draw(GraphicsPage page)
        {
            foreach(GraphicsDirectedLine line in this)
            {
                line.Draw(lineColor, lineWidthInPts, lineStyle);

                line.SetBaseLineOpacity(areaFinishBase.Opacity);

                line.Guid = GuidMaintenance.CreateGuid(line);

                line.Shape.SetShapeData("[Fill Pattern Line]", "Line", line.Guid);

                page.AddToPageShapeDict(line);

                VisioInterop.BringToFront(line.Shape);
            }  
        }

        public void Delete(GraphicsPage page)
        {
            foreach (GraphicsDirectedLine line in this)
            {
                page.RemoveFromPageShapeDict(line.Shape);

                line.Delete();
            }

            this.Clear();
        }

        private void AreaFinishBase_ColorChanged(FinishesLib.AreaFinishBase finishBase, System.Drawing.Color color)
        {
            UpdateColor(color);
        }

        private void AreaFinishBase_OpacityChanged(FinishesLib.AreaFinishBase finishBase, double opacity)
        {
            UpdateOpacity(opacity);
        }

        private void AreaFinishBase_FillPatternLineWidthChanged(AreaFinishBase finishBase, double widthInPts)
        {
            UpdateLineWidth(widthInPts);
        }

        private void AreaFinishBase_FillPatternLineStyleChanged(AreaFinishBase finishBase, int lineStyle)
        {
            UpdateLineStyle(lineStyle);
        }

        private void AreaFinishBase_FillPatternLineInterlineDistanceInFtChanged(AreaFinishBase finishBase, double InterlineDistanceInFt)
        {
            UpdateInterLineDistanceInFt(InterlineDistanceInFt);
        }

        private void AreaFinishBase_PatternChanged(AreaFinishBase finishBase, int pattern)
        {
            UpdatePattern(pattern);
        }

        public void UpdateColor(Color color)
        {
            foreach (GraphicsDirectedLine line in this)
            {
                line.SetBaseLineColor(color);
            }
        }

        public void UpdateOpacity(double opacity)
        {
            foreach (GraphicsDirectedLine line in this)
            {
                line.SetBaseLineOpacity(opacity);
            }
        }

        public void UpdateLineWidth(double lineWidthInPts)
        {
            foreach (GraphicsDirectedLine line in this)
            {
                line.SetBaseLineWidth(lineWidthInPts);
            }
        }

        public void UpdateLineStyle(int lineStyle)
        {
            foreach (GraphicsDirectedLine line in this)
            {
                line.SetBaseLineStyle(lineStyle);
            }
        }

        public void UpdateInterLineDistanceInFt(double interlineDistanceInFt)
        {

            canvasLayoutArea.SetFillPattern();

        }

        public void UpdatePattern(int pattern)
        {

        }

        internal void AddToLayer(GraphicsLayerBase layer)
        {
            if (pattern == 0) return;

            foreach (CanvasDirectedLine canvasDirectedLine in this)
            {
                layer.AddShape(canvasDirectedLine.Shape, 1);
            }
        }

        internal bool NeedsUpdating()
        {
            if (this.interlineDistanceInFt == areaFinishBase.FillPatternInterlineDistanceInFt)
            {
                return false;
            }

            return true;
        }
    }
}
