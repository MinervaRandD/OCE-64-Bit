#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsDirectedPolygon.cs. Project: Graphics. Created: 11/14/2024         */
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

namespace Graphics
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Geometry;
    using System.Diagnostics;
    using System.Net.NetworkInformation;

    /// <summary>
    /// Graphics directed polygon represents a completed polyline on the canvas. 
    /// </summary>


    public class GraphicsDirectedPolygon: DirectedPolygon
    {
  
        public string Guid { get; set; }

        public GraphicsPage Page { get; set; }

        public GraphicsWindow Window { get; set; }

        public new delegate void PerimeterLineAddedHandler(GraphicsDirectedLine graphicsDirectedLine);

        public new event PerimeterLineAddedHandler PerimeterLineAdded;

        private List<GraphicsDirectedLine> perimeter;

        public List<GraphicsDirectedLine> Perimeter
        {
            get
            {
                if (perimeter is null)
                {
                    perimeter = new List<GraphicsDirectedLine>();

                    base.ForEach(l => perimeter.Add(new GraphicsDirectedLine(Window, Page, l, LineRole.ExternalPerimeter)));
                }

                return perimeter;
            }

            set
            {
                perimeter = value;

                base.Clear();

                perimeter.ForEach(l => base.Add(l));
            }
        }

        public void PerimeterAdd(GraphicsDirectedLine line)
        {
            if (perimeter == null)
            {
                perimeter = new List<GraphicsDirectedLine>();
            }

            perimeter.Add(line);

            base.Add(line);
        }

        public GraphicShape PolygonInternalArea { get; set; }

        public GraphicShape Shape { get; set; } = null;

        public ShapeType ShapeType { get; } = ShapeType.Polygon;


        public static List<GraphicsDirectedPolygon> Union(GraphicsWindow window, GraphicsPage page, List<GraphicsDirectedPolygon> gdpList)
        {
            List<DirectedPolygon> dpl = new List<DirectedPolygon>();

            gdpList.ForEach(g => dpl.Add((DirectedPolygon)g));

            List<DirectedPolygon> results = DirectedPolygon.Union(dpl);

            List<GraphicsDirectedPolygon> rtrnList = new List<GraphicsDirectedPolygon>();

            results.ForEach(dp => rtrnList.Add(new GraphicsDirectedPolygon(window, page, dp)));

            return rtrnList;
        }

        /// <summary>
        /// Returns the nearest point on the polygon boundary to the provided coordinate
        /// </summary>
        /// <param name="coord">The coordinate to find the nearest point o</param>
        /// <returns>Returns the nearest point on the polygon boundary to the provided coordinate</returns>
        public Coordinate GetNearestPointToBoundary(Coordinate coord)
        {
            if (Coordinate.IsNullCoordinate(coord))
            {
                return Coordinate.NullCoordinate;
            }

            if (base.Count <= 0)
            {
                return Coordinate.NullCoordinate;
            }

            Coordinate minCoord = base[0].GetNearestPointOnLineToCoord(coord);
            double minDistance = Coordinate.H2Distance(coord, minCoord);

            for (int i = 1; i < base.Count; i++)
            {
                Coordinate nxtCoord = base[i].GetNearestPointOnLineToCoord(coord);
                double nxtDistance = Coordinate.H2Distance(coord, nxtCoord);

                if (nxtDistance < minDistance)
                {
                    minDistance = nxtDistance;
                    minCoord = nxtCoord;
                }
            }

            return minCoord;
        }

        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page)
        {
            this.Window = window;

            this.Page = page;

            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }

        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page, List<Coordinate> coordList)
        {
            this.Window = window;

            this.Page = page;

            perimeter = new List<GraphicsDirectedLine>();

            for (int i = 0; i < coordList.Count;i++)
            {
                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(window, page, new DirectedLine(coordList[i], coordList[(i+1) % coordList.Count]), LineRole.ExternalPerimeter);

                PerimeterAdd(graphicsDirectedLine);
            }

            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }

        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page, GraphicsDirectedPolyline polyline)
        {
            this.Window = window;

            this.Page = page;

            perimeter = new List<GraphicsDirectedLine>();

            foreach (DirectedLine directedLine in polyline)
            {
                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(window, page, directedLine, LineRole.ExternalPerimeter);

                //this.Add(graphicsDirectedLine);
                PerimeterAdd(graphicsDirectedLine);
            }

            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }

        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page, List<DirectedLine> directedLines)
        {
            this.Window = window;

            this.Page = page;

            perimeter = new List<GraphicsDirectedLine>();

            foreach (DirectedLine directedLine in directedLines)
            {
                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(window, page, directedLine, LineRole.ExternalPerimeter);

                PerimeterAdd(graphicsDirectedLine);
            }

            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }

        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page, List<GraphicsDirectedLine> directedLines)
        {
            this.Window = window;

            this.Page = page;

            Perimeter = directedLines;

            directedLines.ForEach(l => base.Add(l));

            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }
        
        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page, DirectedPolygon directedPolygon)
        {
            this.Window = window;

            this.Page = page;

            List<GraphicsDirectedLine> lineList = new List<GraphicsDirectedLine>();

            directedPolygon.ForEach(l => lineList.Add(new GraphicsDirectedLine(window, page, l, LineRole.ExternalPerimeter)));

            this.Perimeter = lineList;


            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }

        public GraphicsDirectedPolygon(GraphicsWindow window, GraphicsPage page, DirectedPolygon directedPolygon, List<DirectedPolygon> internalAreaList)
        {
            this.Window = window;

            this.Page = page;

            List<GraphicsDirectedLine> lineList = new List<GraphicsDirectedLine>();

            directedPolygon.ForEach(l => lineList.Add(new GraphicsDirectedLine(window, page, l, LineRole.ExternalPerimeter)));

            this.Perimeter = lineList;


            base.PerimeterLineAdded += GraphicsDirectedPolygon_PerimeterLineAdded;
        }

        private void GraphicsDirectedPolygon_PerimeterLineAdded(DirectedLine directedLine)
        {
            if (perimeter is null)
            {
                perimeter = new List<GraphicsDirectedLine>();

                perimeter.Add(new GraphicsDirectedLine(this.Window, this.Page, directedLine, LineRole.ExternalPerimeter));
            }
        }

        public GraphicShape Draw(Color perimeterColor, Color areaFillColor)
        {
            return Draw(this.Window, this.Page, perimeterColor, areaFillColor);
        }

        public GraphicShape Draw(GraphicsWindow window, GraphicsPage page, Color perimeterColor, Color areaFillColor)
        {
            double[] coordinates = GenerateCoordinates();

            Shape = page.DrawPolyline(this, coordinates, 1);

            Shape.SetLineColor(perimeterColor);

            if (areaFillColor.A > 0)
            {
                Shape.SetFillColor(areaFillColor);
            }

            else
            {
                Shape.SetFillTransparency("100%");
            }

            PolygonInternalArea = Shape;

            window?.DeselectAll();

            return Shape;
        }

        public double AreaInSqrInches()
        {
            return base.AreaInSqrInches(Page.DrawingScaleInInches);
        }

        public GraphicShape Draw(GraphicsPage page, Color perimeterColor, Color areaFillColor)
        {
            double[] coordinates = GenerateCoordinates();

            GraphicShape shape = page.DrawPolyline(this, coordinates, 1);

            shape.SetLineColor(perimeterColor);

            if (areaFillColor.A > 0)
            {
                shape.SetFillColor(areaFillColor);
            }

            else
            {
                shape.SetFillTransparency("100%");
            }

            return shape;
        }

        public void Delete()
        {
            foreach (GraphicsDirectedLine line in Perimeter)
            {
                line.Delete();
            }

            Perimeter.Clear();

            if (!(Shape is null))
            {
                Shape.Delete();

                Shape = null;
            }

           
        }

        public double[] GenerateCoordinates()
        {
            int count = base.Count;

            double[] coordinates = new double[2 * (count+1)];

            for (int i = 0; i <= count; i++)
            {
                int index = i % count;

                coordinates[2 * i] = base[index].Coord1.X;
                coordinates[2 * i+1] = base[index].Coord1.Y;
            }

            return coordinates;
        }

        public new GraphicsDirectedPolygon Clone()
        {
            GraphicsDirectedPolygon clonedPolygon = new GraphicsDirectedPolygon(this.Window, this.Page);

            this.ForEach(l => clonedPolygon.Add(l.Clone()));

            return clonedPolygon;
        }

        public Dictionary<string, GraphicShape> GenerateShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            foreach (GraphicsDirectedLine graphicsDirectedLine in this.perimeter)
            {
                GraphicShape perimeterShape = graphicsDirectedLine.Shape;

                if (perimeterShape == null)
                {
                    continue;
                }

                if (perimeterShape.VisioShape == null)
                {
                    continue;
                }

                if (!rtrnDict.ContainsKey(perimeterShape.Guid))
                {
                    rtrnDict[perimeterShape.Guid] = perimeterShape;
                }

            }

            return rtrnDict;
        }

        public double Length => this.MaxX - this.MinX;

        public double Height => this.MaxY - this.MinY;
    }
}
