

namespace CanvasLib.Tape_Measure
{
    using Geometry;
    using Utilities;
    using Graphics;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using Visio = Microsoft.Office.Interop.Visio;
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator;

    public class TapeMeasureController
    {
        public MeasureLineState MeasureLineState = MeasureLineState.NotActive;

        public Coordinate MeasureFrstCoord = Coordinate.NullCoordinate;
        public Coordinate MeasureScndCoord = Coordinate.NullCoordinate;

        public MeasureLineMarker MeasureFrstMarker = null;
        public MeasureLineMarker MeasureScndMarker = null;

        public Shape measureConnectingLine = null;

        public const double measureLineMarkerWidth = 0.1;

        private CanvasManager canvasManager;

        private Visio.Window vsoWindow;

        private CanvasPage currentPage;

        public TapeMeasureController(
            CanvasManager canvasManager,
            Visio.Window vsoWindow,
            CanvasPage currentPage)
        {
            this.canvasManager = canvasManager;
            this.vsoWindow = vsoWindow;
            this.currentPage = currentPage;
        }

        public void InitiateGetScale()
        {
            MeasureLineState = MeasureLineState.GetScaleInitiated;

            canvasManager.DrawingMode = DrawingMode.TapeMeasure;
        }

        internal void TapeMeasureDrawingModeClick(int button, double x, double y)
        {
            if (button != 1)
            {
                return;
            }

            switch (MeasureLineState)
            {
                case MeasureLineState.GetScaleInitiated:
                    MeasureLineFrstPointClick(x, y);
                    return;

                case MeasureLineState.FrstPointSelected:
                    MeasureLineScndPointClick(x, y);
                    return;

                case MeasureLineState.ScndPointSelected:
                    return;
            }
        }

        private void MeasureLineFrstPointClick(double x, double y)
        {
            Debug.Assert(MeasureLineState == MeasureLineState.GetScaleInitiated);

            this.MeasureLineState = MeasureLineState.FrstPointSelected;

            this.MeasureFrstMarker = new MeasureLineMarker(x, y, measureLineMarkerWidth);

            MeasureFrstMarker.Draw(currentPage);

            MeasureFrstCoord = new Coordinate(x, y);

            vsoWindow.DeselectAll();

            canvasManager.DrawingShape = true;
        }

        private void MeasureLineScndPointClick(double x, double y)
        {
            Debug.Assert(MeasureLineState == MeasureLineState.FrstPointSelected);

            this.MeasureLineState = MeasureLineState.ScndPointSelected;
 
            this.MeasureScndMarker = new MeasureLineMarker(x, y, measureLineMarkerWidth);

            MeasureScndMarker.Draw(currentPage);

            MeasureScndCoord = new Coordinate(x, y);

            drawConnectingMeasureLine();

            vsoWindow.DeselectAll();

            canvasManager.DrawingShape = true;
        }

        private void drawConnectingMeasureLine()
        {
            measureConnectingLine = currentPage.DrawLine(MeasureFrstCoord.X, MeasureFrstCoord.Y, MeasureScndCoord.X, MeasureScndCoord.Y);

            VisioInterop.SetBaseLineColor(measureConnectingLine, Color.Red);
            VisioInterop.SetEndpointArrows(measureConnectingLine, 8);

            setMeasureLineLength();
        }

        private void setMeasureLineLength()
        {
            if (measureConnectingLine is null)
            {
                return;
            }


            double totalInches = MathUtils.H2Distance(MeasureFrstCoord.X, MeasureFrstCoord.Y, MeasureScndCoord.X, MeasureScndCoord.Y) *
            
                currentPage.DrawingScaleInInches;
   
            int inches = (int)Math.Round(totalInches, 0);

            int feet = inches / 12;
            int inch = inches % 12;

            VisioInterop.SetLineText(measureConnectingLine, ' ' + feet.ToString("#,##0") + "' " + inch.ToString("0") + "\" ");
        }

        public void CancelTapeMeasure()
        {
            exitGetScale();
        }

        private void exitGetScale()
        {
            if (!(MeasureFrstMarker is null))
            {
                this.MeasureFrstMarker.Delete();

                this.MeasureFrstMarker = null;
            }

            if (!(MeasureScndMarker is null))
            {
                this.MeasureScndMarker.Delete();

                this.MeasureScndMarker = null;
            }

            if (!(measureConnectingLine is null))
            {
                this.measureConnectingLine.Delete();

                this.measureConnectingLine = null;
            }

            this.MeasureFrstCoord = Coordinate.NullCoordinate;
            this.MeasureScndCoord = Coordinate.NullCoordinate;

            MeasureLineState = MeasureLineState.NotActive;

            if (GetScaleCompleted != null)
            {
                GetScaleCompleted.Invoke();
            }
        }

        public delegate void GetScaleCompletedHandler();

        public event GetScaleCompletedHandler GetScaleCompleted;
    }

}
