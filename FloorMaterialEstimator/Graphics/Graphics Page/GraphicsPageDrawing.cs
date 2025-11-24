#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsPageDrawing.cs. Project: Graphics. Created: 6/10/2024         */
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


namespace Graphics
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;

    using Utilities;

    using Geometry;

    using Visio = Microsoft.Office.Interop.Visio;
    using System.Xml.Serialization;

    using TracerLib;

    public partial class GraphicsPage
    {

        // When a background image is loaded, a copy of the original file is stored here in case the user wants to export the project.
        // The alternative would be to refer back to the original image, which may have been moved in the mean time.

        public byte[] DrawingInBytes
        {
            get;
            set;
        }


        public GraphicShape DrawLine(object parentLine, string data1, double x1, double y1, double x2, double y2)
        {
            GraphicShape shape = VisioInterop.DrawLine(this, x1, y1, x2, y2, data1);

            shape.ParentObject = parentLine;

            return shape;
        }

        public GraphicShape DrawLine(object parentLine, double x1, double y1, double x2, double y2, string guid)
        {

            Visio.Shape visioShape = VisioPage.DrawLine(x1, y1, x2, y2);

            GraphicShape shape = new GraphicShape(parentLine, this.Window, this, visioShape, ShapeType.Line, guid);

            return shape;
        }

        public GraphicShape DrawRectangle(object parentRectangle, double x1, double y1, double x2, double y2)
        {
            Visio.Shape visioShape = VisioPage.DrawRectangle(x1, y1, x2, y2);

            GraphicShape shape = new GraphicShape(parentRectangle, this.Window, this, visioShape, ShapeType.Rectangle);

            return shape;
        }

        public GraphicShape DrawRectangle(object parentRectangle, double x1, double y1, double x2, double y2, string guid)
        {
            Visio.Shape visioShape = VisioPage.DrawRectangle(x1, y1, x2, y2);

            GraphicShape shape = new GraphicShape(parentRectangle, this.Window, this, visioShape, ShapeType.Rectangle, guid);

            return shape;
        }

        public GraphicShape DrawTextBox(object parentTextBox, double x1, double y1, double x2, double y2, string text)
        {
            Visio.Shape visioShape = null;

            //Visio.Page page = VisioDocument.Application.ActivePage ?? VisioDocument.Pages[1];

            //x2 = 1; y2 = 1;
            //x1 = 4; y1 = 4;

            //VisioPage.DrawLine(x1, y1, x2, y2);

            //VisioDocument.Application.
            visioShape = VisioPage.DrawRectangle(x1, y1, x2, y2);
            visioShape.TextStyle = "Normal";
            visioShape.LineStyle = "Text Only";
            visioShape.FillStyle = "Text Only";

            GraphicShape shape = new GraphicShape(parentTextBox, this.Window, this, visioShape, ShapeType.TextBox);

            shape.SetShapeText(text, Color.Black, 8);


            return shape;
        }

        public GraphicShape DrawPolyline(object parentPolyline, double[] coordinateArray, short flags)
        {
            Visio.Shape visioShape = VisioPage.DrawPolyline(coordinateArray, flags);

            GraphicShape shape = new GraphicShape(parentPolyline, this.Window, this, visioShape, ShapeType.Polyline);

            return shape;
        }

        public GraphicShape DrawPolygon(object parentPolygon, double[] coordinateArray, string guid = null)
        {
            Visio.Shape visioShape = VisioPage.DrawPolyline(coordinateArray, 0);

            visioShape.Data2 = "Polygon";
            visioShape.Data3 = guid is null ? string.Empty : guid;

            GraphicShape shape = new GraphicShape(parentPolygon, this.Window, this, visioShape, ShapeType.Polyline, guid);

            //shape.Guid = guid;

            return shape;
        }

        public GraphicShape DrawCircle(object parentCircle, string guid, Coordinate center, double radius, Color color)
        {
            GraphicShape shape = DrawCircle(parentCircle, center, radius, color);

            shape.Guid = guid;

            shape.VisioShape.Data3 = guid;

            return shape;
        }

        public GraphicShape DrawCircle(object parentCircle, Coordinate center, double radius, Color color, string guid = null)
        {
            double upperLeftX = center.X - radius;
            double upperLeftY = center.Y + radius;
            double lowerRghtX = center.X + radius;
            double lowerRghtY = center.Y - radius;

            Visio.Shape visioShape = VisioPage.DrawOval(upperLeftX, upperLeftY, lowerRghtX, lowerRghtY);

            GraphicShape shape = new GraphicShape(parentCircle, this.Window, this, visioShape, ShapeType.Circle, guid);

            shape.SetLineColor(color);

            return shape;
        }

        public GraphicShape DrawCircle(Coordinate center, double radius, Color color, GraphicShape shape)
        {
            double upperLeftX = center.X - radius;
            double upperLeftY = center.Y + radius;
            double lowerRghtX = center.X + radius;
            double lowerRghtY = center.Y - radius;

            Visio.Shape visioShape = VisioPage.DrawOval(upperLeftX, upperLeftY, lowerRghtX, lowerRghtY);

            shape.VisioShape = visioShape;

            shape.SetLineColor(color);

            return shape;
        }

        public void DrawCircle(object parentCircle, GraphicShape graphicShape, Coordinate center, double radius, Color color)
        {
            double upperLeftX = center.X - radius;
            double upperLeftY = center.Y + radius;
            double lowerRghtX = center.X + radius;
            double lowerRghtY = center.Y - radius;

            Visio.Shape visioShape = VisioPage.DrawOval(upperLeftX, upperLeftY, lowerRghtX, lowerRghtY);

            graphicShape.VisioShape = (Visio.Shape) visioShape;

            graphicShape.SetLineColor(color);

            graphicShape.ShapeType |= ShapeType.Circle;

            visioShape.Data1 = graphicShape.Data1;
            visioShape.Data2 = graphicShape.Data2;
            visioShape.Data3 = graphicShape.Data3;
        }
    }
}
