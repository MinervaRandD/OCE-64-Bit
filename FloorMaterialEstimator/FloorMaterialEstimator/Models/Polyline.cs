//-------------------------------------------------------------------------------//
// <copyright file="Polyline.cs" company="Bruun Estimating, LLC">                // 
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
    using FloorMaterialEstimator.Utilities;
    using FloorMaterialEstimator.Test_and_Debug;

    using Visio = Microsoft.Office.Interop.Visio;

    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Polyline is a polygon object which extends the visio base representation.
    /// </summary>
    public class Polyline : AreaShape
    {
        public Polyline(CanvasManager canvasManager): base(canvasManager, ShapeType.Polyline)
        {
            Perimeter = new Perimeter(this);
        }

        public Polyline(CanvasManager canvasManager, Visio.Shape shape) : base(canvasManager, ShapeType.Polyline)
        {

        }

        public GraphicsLine buildingLine
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }

                if (LineCount <= 0)
                {
                    return null;
                }

                return Perimeter.LastLine;
            }
        }

        public int LineCount
        {
            get
            {
                if (Perimeter == null)
                {
                    return 0;
                }

                return Perimeter.LineCount;
            }
        }

        public GraphicsLine FirstLine
        {
            get
            {
                if (Perimeter.LineCount <= 0)
                {
                    return null;
                }

                return Perimeter.FirstLine;
            }
        }


        public GraphicsLine LastLine
        {
            get
            {
                if (Perimeter.LineCount <= 0)
                {
                    return null;
                }

                return Perimeter.LastLine;
            }
        }

        internal PerimeterLine AddInitialLine(double x, double y)
        {
            Debug.Assert(Perimeter != null, "Unexpected null perimeter");

            PerimeterLine line = Perimeter.AddInitialLine(x, y);

            return line;
        }

        internal PerimeterLine AddExtendingLine()
        {
            PerimeterLine line = Perimeter.AddExtendingLine();
         
            return line;
        }
        
        internal Coordinate GetFirstCoordinate()
        {
            return Perimeter.GetFirstCoordinate();
        }

        public Coordinate GetLastCoordinate()
        {
            return Perimeter.GetLastCoordinate();
        }

        internal void CreateInternalAreaShape()
        {
            double[] coordinateArray = Perimeter.GetCoordinateArray();

            Visio.Shape visioShape = CanvasManager.CurrentPage.DrawPolyline(coordinateArray, 0);

            InternalAreaShape = new Shape(visioShape, ShapeType.Polyline);

            CanvasManager.ShapeDict.Add(visioShape.NameID, InternalAreaShape);

            visioShape.Data2 = "Polyline";

            InternalAreaShape.SetNolineMode();

            InternalAreaShape.VisioShape.SendToBack();

            CanvasManager.selectedFinishType.AddShape(this);
        }

        /// <summary>
        /// Removes the last line in the perimeter. Returns a boolean indicating that the perimeter now has no lines.
        /// </summary>
        internal GraphicsLine RemoveLastLine()
        {
            if (Perimeter == null)
            {
                return null;
            }

            GraphicsLine lastLine = Perimeter.RemoveLastLine();
            
            CanvasManager.DeleteFromVisioShapDict(lastLine);

            lastLine.ucLine.RemoveLine(lastLine);

            lastLine.VisioShape.Delete();

            return lastLine ;
        }

        /// <summary>
        /// Completes the current polyline by drawing a line from the last line endpoint to the first line.
        /// </summary>
        internal GraphicsLine CompletePolylineDraw(Visio.Page drawingPage, UCLine selectedLineType)
        {
            if (LineCount <= 1)
            {
                // Need at least 2 lines to complete an area shape
                return null;
            }

            Coordinate frstCoord = Perimeter.GetFirstCoordinate();
            Coordinate lastCoord = Perimeter.GetLastCoordinate();

            if (MathUtils.H0Dist(frstCoord, lastCoord) <= 0.01)
            {
                Logger.LogError("Attempt to complete already completed polyline");

                throw new Exception("Attempt to complete already completed polyline");
            }

            GraphicsLine line = Perimeter.AddCompletingLine();

            return line;
        }
    }
}
