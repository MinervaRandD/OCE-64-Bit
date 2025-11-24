#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: CutPolygon.cs. Project: MaterialsLayout. Created: 6/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace MaterialsLayout
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;

    public class CutPolygon
    {
        public Coordinate UpperLeftCorner;
        public Coordinate UpperRghtCorner;
        public Coordinate LowerRghtCorner;
        public Coordinate LowerLeftCorner;

        public List<CutLineSegment> PolygonPerimeter = new List<CutLineSegment>();

        public Coordinate UpperLeftBoundaryPoint(double y)
        {
            double minX = PolygonPerimeter.Min(s => Math.Min(s.Coord1.X, s.Coord2.X));

            return new Coordinate(minX, y);
        }

        public Coordinate LowerRightBoundaryPoint(double y)
        {
            double maxX = PolygonPerimeter.Max(s => Math.Max(s.Coord1.X, s.Coord2.X));
            
            return new Coordinate(maxX, y);
        }

       
        public CutPolygon() { }

        public CutPolygon(List<CutLineSegment> cutLineSegmentList)
        {
            PolygonPerimeter = cutLineSegmentList;
        }

        public CutPolygon(CutPolygon cutPolygon)
        {
            this.UpperLeftCorner = cutPolygon.UpperLeftCorner;
            this.UpperRghtCorner = cutPolygon.UpperRghtCorner;
            this.LowerLeftCorner = cutPolygon.LowerLeftCorner;
            this.LowerRghtCorner = cutPolygon.LowerRghtCorner;

            PolygonPerimeter = new List<CutLineSegment>();

            cutPolygon.PolygonPerimeter.ForEach(l => PolygonPerimeter.Add(l.Clone()));
        }

        public void Add(CutLineSegment cutLineSegment)
        {
            if (PolygonPerimeter.Count > 0)
            {
                Debug.Assert(PolygonPerimeter[PolygonPerimeter.Count - 1].Coord2 == cutLineSegment.Coord1);
            }

            PolygonPerimeter.Add(cutLineSegment);
        }

        public CutLineSegment GetFirstLine()
        {
            if (PolygonPerimeter.Count <= 0)
            {
                return null;
            }

            return PolygonPerimeter[0];
        }

        public void Translate(Coordinate translateCoordinate)
        {
            PolygonPerimeter.ForEach(p => p.Translate(translateCoordinate));

            UpperLeftCorner.Translate(translateCoordinate);
            UpperRghtCorner.Translate(translateCoordinate);
            LowerRghtCorner.Translate(translateCoordinate);
            LowerLeftCorner.Translate(translateCoordinate);
        }


        internal void Rotate(double[,] rotationMatrix)
        {
            PolygonPerimeter.ForEach(p => p.Rotate(rotationMatrix));

            UpperLeftCorner.Rotate(rotationMatrix);
            UpperRghtCorner.Rotate(rotationMatrix);
            LowerRghtCorner.Rotate(rotationMatrix);
            LowerLeftCorner.Rotate(rotationMatrix);
        }
        
        internal void GenerateBoundaries(double y1, double y2)
        {
            Coordinate upperLeftCoord = UpperLeftBoundaryPoint(y1);
            Coordinate lowerRightCoord = LowerRightBoundaryPoint(y2);

            UpperLeftCorner = upperLeftCoord;
            UpperRghtCorner = new Coordinate(lowerRightCoord.X, upperLeftCoord.Y);
            LowerRghtCorner = lowerRightCoord;
            LowerLeftCorner = new Coordinate(upperLeftCoord.X, lowerRightCoord.Y);
        }

        public double[] GetCoordinates()
        {
            double[] coordinates = new double[10];

            coordinates[0] = UpperLeftCorner.X;
            coordinates[1] = UpperLeftCorner.Y;

            coordinates[2] = UpperRghtCorner.X;
            coordinates[3] = UpperRghtCorner.Y;

            coordinates[4] = LowerRghtCorner.X;
            coordinates[5] = LowerRghtCorner.Y;

            coordinates[6] = LowerLeftCorner.X;
            coordinates[7] = LowerLeftCorner.Y;

            coordinates[8] = UpperLeftCorner.X;
            coordinates[9] = UpperLeftCorner.Y;

            return coordinates;
        }

        internal CutPolygon Clone()
        {
            List<CutLineSegment> clonedLineSegmentList = new List<CutLineSegment>();

            this.PolygonPerimeter.ForEach(p => clonedLineSegmentList.Add(p.Clone()));

            CutPolygon clonedCutPolygon = new CutPolygon(this);

            clonedCutPolygon.UpperRghtCorner = this.UpperRghtCorner.Clone();
            clonedCutPolygon.UpperLeftCorner = this.UpperLeftCorner.Clone();
            clonedCutPolygon.LowerRghtCorner = this.LowerRghtCorner.Clone();
            clonedCutPolygon.LowerLeftCorner = this.LowerLeftCorner.Clone();

            return clonedCutPolygon;
        }
   
    }

}
