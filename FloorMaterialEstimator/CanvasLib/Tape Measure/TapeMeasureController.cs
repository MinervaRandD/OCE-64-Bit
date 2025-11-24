

namespace CanvasLib.Tape_Measure
{
    using Geometry;
    using Utilities;
    using Graphics;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using Globals;
  
    public class TapeMeasureController
    {
        public TapeMeasureState MeasureLineState = TapeMeasureState.NotActive;

        public Coordinate MeasureFrstCoord = Coordinate.NullCoordinate;
        public Coordinate MeasureScndCoord = Coordinate.NullCoordinate;

        public TapeMeasureMarker MeasureFrstMarker = null;
        public TapeMeasureMarker MeasureScndMarker = null;

        public GraphicShape measureConnectingLine = null;

        private double measureLineMarkerWidth
        {
            get
            {
                return 0.16 / Math.Max(1, SystemState.ZoomPercent);
            }
        }

        private double measureLineWidth
        {
            get
            {
                return 1.25 / Math.Max(1, SystemState.ZoomPercent);
            }
        }

        private int measureLineFontSize
        {
            get
            {
                return (int)Math.Round(24.0 / Math.Max(1, SystemState.ZoomPercent));
            }
        }

        private int measureArrowSize
        {
            get
            {
                if (SystemState.ZoomPercent <= 1.0)
                {
                    return 5;
                }

                if (SystemState.ZoomPercent <= 2.25)
                {
                    return 5;
                }

                if (SystemState.ZoomPercent <= 4.5)
                {
                    return 3;
                }

                if (SystemState.ZoomPercent <= 6.75)
                {
                    return 1;
                }

                return 0;
            }
        }


        private GraphicsPage page;

        private GraphicsWindow window;

        public TapeMeasureController(
            GraphicsWindow window,
            GraphicsPage page)
        {
            this.window = window;
            this.page = page;
        }

        public void InitiateGetScale()
        {
            MeasureLineState = TapeMeasureState.GetScaleInitiated;
        }

        public void TapeMeasureDrawingModeClick(int button, double x, double y)
        {
            if (button != 2)
            {
                return;
            }

            switch (MeasureLineState)
            {
                case TapeMeasureState.GetScaleInitiated:
                    MeasureLineFrstPointClick(x, y);
                    return;

                case TapeMeasureState.FrstPointSelected:
                    MeasureLineScndPointClick(x, y);
                    return;

                case TapeMeasureState.ScndPointSelected:
                    resetGetScale();
                    MeasureLineFrstPointClick(x, y);
                    return;
            }
        }

        public void MeasureLineFrstPointClick(double x, double y)
        {
            Debug.Assert(MeasureLineState == TapeMeasureState.GetScaleInitiated);

            this.MeasureLineState = TapeMeasureState.FrstPointSelected;

            this.MeasureFrstMarker = new TapeMeasureMarker(x, y, measureLineMarkerWidth, measureLineWidth);

            MeasureFrstMarker.Draw(page);

            MeasureFrstCoord = new Coordinate(x, y);

            window?.DeselectAll();

            page.LastPointDrawn = MeasureFrstCoord;
            
            //SystemState.DrawingShape = true;
        }

        public void MeasureLineScndPointClick(double x, double y)
        {
            Debug.Assert(MeasureLineState == TapeMeasureState.FrstPointSelected);

            this.MeasureLineState = TapeMeasureState.ScndPointSelected;
 
            this.MeasureScndMarker = new TapeMeasureMarker(x, y, measureLineMarkerWidth, measureLineWidth);

            MeasureScndMarker.Draw(page);

            MeasureScndCoord = new Coordinate(x, y);

            drawConnectingMeasureLine();

            window?.DeselectAll();

            page.LastPointDrawn = Coordinate.NullCoordinate;

            //SystemState.DrawingShape = true;
        }

        private void drawConnectingMeasureLine()
        {
            measureConnectingLine = page.DrawLine(this, MeasureFrstCoord.X, MeasureFrstCoord.Y, MeasureScndCoord.X, MeasureScndCoord.Y, string.Empty);

            measureConnectingLine.SetLineColor(Color.Red);
            measureConnectingLine.SetLineWidth(measureLineWidth);
            measureConnectingLine.SetEndpointArrows(8, measureArrowSize);

            VisioInterop.SetEndpointArrows(measureConnectingLine, 8);

            setMeasureLineLength();
        }

        private void setMeasureLineLength()
        {
            if (measureConnectingLine is null)
            {
                return;
            }


            double totalInches = MathUtils.H2Distance(MeasureFrstCoord.X, MeasureFrstCoord.Y, MeasureScndCoord.X, MeasureScndCoord.Y) * page.DrawingScaleInInches;

            int feet = (int)Math.Floor((double)totalInches / 12.0);
            double inchesOnly = totalInches - (double)feet * 12.0;

            string inchFormat = Utilities.FormatInchesTo2DecimalPlaces(inchesOnly);

            measureConnectingLine.SetLineText(' ' + feet.ToString("#,##0") + "' " + inchFormat + "\" ");
            measureConnectingLine.SetTextFontSize(measureLineFontSize);
        }

        private void resetGetScale()
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

            MeasureLineState = TapeMeasureState.GetScaleInitiated;

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

            MeasureLineState = TapeMeasureState.NotActive;

            if (GetScaleCompleted != null)
            {
                GetScaleCompleted.Invoke();
            }
        }

        public delegate void GetScaleCompletedHandler();

        public event GetScaleCompletedHandler GetScaleCompleted;
    }

}
