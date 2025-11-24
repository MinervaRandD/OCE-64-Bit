//-------------------------------------------------------------------------------//
// <copyright file="Perimeter.cs" company="Bruun Estimating, LLC">               // 
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

    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public class Perimeter
    {
        public AreaShape AreaShape { get; set; }

        private List<PerimeterLine> lineList = new List<PerimeterLine>();
        
        private CanvasManager canvasManager
        {
            get
            {
                if (AreaShape == null)
                {
                    return null;
                }

                return AreaShape.CanvasManager;
            }
        }

        internal void Draw()
        {
            foreach (PerimeterLine perimeterLine in PerimeterLines)
            {
                Coordinate coord1 = perimeterLine.GetLineStartPoint();
                Coordinate coord2 = perimeterLine.GetLineEndpoint();

                double x1 = coord1.X;
                double y1 = coord1.Y;

                double x2 = coord2.X;
                double y2 = coord2.Y;

                Visio.Shape visioShape = canvasManager.CurrentPage.DrawLine(x1, y1, x2, y2);

                perimeterLine.VisioShape = visioShape;
            }
        }

        private UCLine selectedLineType
        {
            get
            {
                return canvasManager.SelectedLineType;
            }
        }

        internal AreaShapeBuildStatus BuildStatus
        {
            get
            {
                return AreaShape.BuildStatus;
            }
        }

        public Perimeter(AreaShape areaShape)
        {
            this.AreaShape = areaShape;
        }

        public Page Page
        {
            get
            {
                return this.AreaShape.Page;
            }
        }

        private double gridScale
        {
            get
            {
                return this.canvasManager.GridScale;
            }
        }
        internal double TotalLength()
        {
            double totalLength = 0;

            lineList.ForEach(l => totalLength += l.VisioShape.LengthIU);

            return totalLength;
        }

        public List<PerimeterLine> PerimeterLines
        {
            get
            {
                return lineList;
            }
        }

        public int LineCount
        {
            get
            {
                return lineList.Count;
            }
        }

        public GraphicsLine FirstLine
        {
            get
            {
                if (lineList.Count <= 0)
                {
                    return null;
                }

                return lineList[0];
            }
        }

        public GraphicsLine LastLine
        {
            get
            {
                if (lineList.Count <= 0)
                {
                    return null;
                }

                return lineList[lineList.Count - 1];
            }
        }


        private bool visible = true;


        public bool Visible
        {
            get
            {
                return visible ;
            }

            internal set
            {
                visible = value;

                if (lineList == null)
                {
                    return;
                }

                if (lineList.Count <= 0)
                {
                    return;
                }

                lineList.ForEach(l => l.Visible = visible);
            }
        }

        internal PerimeterLine AddInitialLine(double x, double y)
        {
            Debug.Assert(LineCount == 0, "Unexpected call to Add InitialLine");

            PerimeterLine line = new PerimeterLine(x, y, x, y, this, selectedLineType, canvasManager);

            AddLine(line);

            return line;
        }


        internal PerimeterLine AddExtendingLine()
        {
            Debug.Assert(LineCount > 0, "Unexected call to AddExtendingLine");

            Coordinate coord = GetLastCoordinate();

            return AddLine(coord.X, coord.Y, coord.X, coord.Y);
        }

        internal GraphicsLine AddCompletingLine()
        {
            Debug.Assert(LineCount > 2, "Unexpected call to AddCompletingLine");

            Coordinate coord1 = GetLastCoordinate();
            Coordinate coord2 = GetFirstCoordinate();

            PerimeterLine line = new PerimeterLine(coord1.X, coord1.Y, coord2.X, coord2.Y, this, selectedLineType, canvasManager);

            ValidateConsistentPerimeter();

            AddLine(line);

            return line;
        }

        internal PerimeterLine AddLine(double x1, double y1, double x2, double y2)
        {
            PerimeterLine line = new PerimeterLine(x1, y1, x2, y2, this, selectedLineType, canvasManager);

            AddLine(line);

            return line;
        }

        internal void AddLine(PerimeterLine line)
        {
            lineList.Add(line);

            line.Perimeter = this;

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Development)
            {
                Graphics.SetLineBeginShape(line, 20, 3);
                Graphics.SetLineEndShape(line, 20, 3);

                AddPointLabel(lineList.Count, line.GetLineStartPoint());
            }

            else if (lineList.Count == 1)
            {
                Graphics.SetLineBeginShape(line, 20, 3);
            }

        }

        private void AddPointLabel(int pointNumber, Coordinate pointCoordinate)
        {
            Coordinate upperLeft;
            Coordinate lowerRght;

            upperLeft = pointCoordinate + new Coordinate(-0.25, 0.20);
            lowerRght = pointCoordinate + new Coordinate(0.25, 0.10);

            Graphics.AddTextBox(Page, upperLeft, lowerRght, pointNumber.ToString());

            double x = pointCoordinate.X / this.gridScale - 2.0 * GlobalSettings.GridlineOffsetSetting;
            double y = pointCoordinate.Y / this.gridScale - 2.0 * GlobalSettings.GridlineOffsetSetting;

            string coordText = "(" + x.ToString("0.0") + ", " + y.ToString("0.0") + ")";

            upperLeft = pointCoordinate + new Coordinate(-0.5, -0.15);
            lowerRght = pointCoordinate + new Coordinate(0.5, -0.25);

            Graphics.AddTextBox(Page, upperLeft, lowerRght, coordText);
        }

        internal int GetLineIndex(PerimeterLine line)
        {
            if (!lineList.Contains(line))
            {
                return -1;
            }

            return lineList.IndexOf(line);
        }

        internal Coordinate GetFirstCoordinate()
        {
            if (lineList.Count <= 0)
            {
                throw new NotImplementedException();
            }

            return lineList[0].GetLineStartPoint();
        }

        internal Coordinate GetLastCoordinate()
        {
            if (lineList.Count <= 0)
            {
                throw new NotImplementedException();
            }

            GraphicsLine line = lineList.Last();

            return line.GetLineEndpoint();
        }

        internal double[] GetCoordinateArray()
        {
            int count = this.lineList.Count;

            if (count <= 0)
            {
                throw new NotImplementedException();
            }

            double[] coordinateArray = new double[2 * (count + 1)];

            Coordinate coordinate;

            for (int i = 0; i < count; i++)
            {
                coordinate = this.lineList[i].GetLineStartPoint();

                coordinateArray[2 * i] = coordinate.X;
                coordinateArray[2 * i + 1] = coordinate.Y;
            }

            coordinate = this.lineList[0].GetLineStartPoint();

            coordinateArray[2 * count] = coordinate.X;
            coordinateArray[2 * count + 1] = coordinate.Y;

            return coordinateArray;
        }

        internal List<Coordinate> GetCoordinateList()
        {
            List<Coordinate> coordinateList = new List<Coordinate>();

            lineList.ForEach(l => coordinateList.Add(l.GetLineStartPoint()));

            return coordinateList;
        }

        internal void SetCompletedLineWidth()
        {
            lineList.ForEach(l => l.SetBaseLineWidth(CanvasManager.CompletedShapeLineWidthInPts));
        }

        internal void SetBaseLineWidth()
        {
            lineList.ForEach(l => l.SetBaseLineWidth(l.BaseLineWidthInPts));
        }

        /// <summary>
        /// Removes the last line of the perimeter. Returns a reference to the line removed.
        /// </summary>
        internal GraphicsLine RemoveLastLine()
        {
            int count = lineList.Count;

            if (lineList.Count <= 0)
            {
                return null ;
            }

            GraphicsLine line = lineList[count - 1];

            lineList.RemoveAt(count - 1);

            return line;
        }

        internal void SetLineGraphicsForAreaMode(AreaShapeBuildStatus buildStatus)
        {
            lineList.ForEach(l => l.SetLineGraphicsForAreaMode());
        }

        internal void ValidateConsistentPerimeter()
        {
            int count = lineList.Count;

            for (int iCurr = 0; iCurr < count; iCurr++)
            {
                int iNext = (iCurr + 1) % count;

                PerimeterLine l1 = lineList[iCurr];
                PerimeterLine l2 = lineList[iNext];

                Coordinate c1 = l1.GetLineEndpoint();
                Coordinate c2 = l2.GetLineStartPoint();

                if (c1 == c2)
                {
                    continue;
                }

                Debug.Assert(c1 == c2, "Inconsistent perimeter at line " + iCurr);
            }
        }

        public PerimeterLine this[int index]
        {
            get
            {
                if (index < 0 || index >= lineList.Count)
                {
                    return null;
                }

                return lineList[index];
            }
        }
    }
}
