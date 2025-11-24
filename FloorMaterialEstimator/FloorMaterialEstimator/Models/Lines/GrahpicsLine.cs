//-------------------------------------------------------------------------------//
// <copyright file="GrpahicsLine.cs" company="Bruun Estimating, LLC">            // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Models
{
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Utilities;

    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
 
    using Visio = Microsoft.Office.Interop.Visio;

    public class GraphicsLine : Shape
    {
        public UCLine ucLine = null;

        private CanvasManager canvasManager;

        private bool isZeroLine = false;

        public LineType LineType { get; set; }

        public bool IsZeroLine
        {
            get
            {
                return isZeroLine;
            }

            set
            {
                isZeroLine = value;
            }
        }

        public double BaseLineWidthInPts
        {
            get
            {
                if (ucLine == null)
                {
                    return 4.0;
                }

                return ucLine.LineWidthInPts;
            }
        }

        private Page currentPage
        {
            get
            {
                if (canvasManager == null)
                {
                    return null;
                }

                return canvasManager.CurrentPage;
            }
        }

        public GraphicsLine(Visio.Shape visioShape) : base(visioShape, ShapeType.Line)
        {
            LineType = LineType.GraphicsLine;
        }

        public GraphicsLine(double x1, double y1, double x2, double y2, UCLine ucLine, CanvasManager canvasManager)
        {
            this.ucLine = ucLine;
            this.canvasManager = canvasManager;

            this.VisioShape = currentPage.DrawLine(x1, y1, x2, y2);

            this.ShapeType = ShapeType.Line;

            this.VisioShape.Data2 = "Line";

            ucLine.AddLine(this);
        }

        public GraphicsLine(Coordinate coord1, Coordinate coord2, UCLine ucLine, CanvasManager canvasManager) :
            this(coord1.X, coord1.Y, coord2.X, coord2.Y, ucLine, canvasManager) { }
        
        public Coordinate[] GetLineEndpoints()
        {
            Coordinate[] lineCoordinates = new Coordinate[2];

            lineCoordinates[0] = Graphics.GetShapeBeginPoint(this.VisioShape);
            lineCoordinates[1] = Graphics.GetShapeEndPoint(this.VisioShape);

            return lineCoordinates;
        }

        public Coordinate GetLineEndpoint()
        {
            return Graphics.GetShapeEndPoint(this.VisioShape);
        }

        internal Coordinate GetLineStartPoint()
        {
            return Graphics.GetShapeBeginPoint(this.VisioShape);
        }

        internal void SetBaseLineColor(string lineColorFormula)
        {
            Graphics.SetBaseLineColor(this, lineColorFormula);
        }

        internal void SetBaseLineOpacity(double opacity)
        {
            Graphics.SetBaseLineOpacity(this, opacity);
        }

        internal void SetBaseLineStyle()
        {
            SetBaseLineStyle(ucLine.visioLineStyleFormula);
        }

        internal void SetBaseLineStyle(string lineStyleFormula)
        {
            Graphics.SetBaseLineStyle(this, lineStyleFormula);
        }

        public void SetBaseLineWidth(double lineWidthInPts)
        {
            Graphics.SetLineWidth(this, lineWidthInPts);
        }

        internal void SetLineStartPoint(Coordinate coord)
        {
            SetLineStartPoint(coord.X, coord.Y);
        }

        internal void SetLineStartPoint(double x, double y)
        {
            Coordinate c1 = GetLineStartPoint();

            VisioShape.SetBegin(x, y);

            Coordinate c2 = GetLineStartPoint();
        }

        internal void SetLineEndPoint(Coordinate coord)
        {
            SetLineEndPoint(coord.X, coord.Y);
        }

        internal void SetLineEndPoint(double x, double y)
        {
            if (GlobalSettings.SnapToAxisSetting)
            {
                Coordinate frstPoint = GetLineStartPoint();
                Coordinate lastPoint = new Coordinate(x, y);

                Utilities.SnapToGrid(frstPoint.X, frstPoint.Y, ref lastPoint.X, ref lastPoint.Y);

                VisioShape.SetEnd(lastPoint.X, lastPoint.Y);
            }

            else
            {
                VisioShape.SetEnd(x, y);
            }
        }

        internal void SetEndpointArrows(int arrowIndex)
        {
            Graphics.SetEndpointArrows(VisioShape, arrowIndex);
        }

        internal double Length
        {
            get
            {
                return MathUtils.H2Length(Graphics.GetShapeBeginPoint(this.VisioShape), Graphics.GetShapeEndPoint(this.VisioShape));
            }
        }

        private bool visible = true;
      

        public bool Visible
        {
            get
            {
                return visible;
            }

            internal set
            {
                visible = value;

              
            }
        }

        internal virtual void SetLineGraphicsForLineMode()
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

        internal virtual void SetLineGraphicsForAreaMode()
        {
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
