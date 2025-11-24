#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsDirectedLine.cs. Project: Graphics. Created: 11/10/2024         */
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

using Microsoft.Office.Interop.Visio;

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
    using Utilities;
    using Globals;

    public class GraphicsDirectedLine : DirectedLine, IGraphicsShape
    {
        public GraphicsDirectedLine() { }

        public GraphicsDirectedLine(GraphicsWindow window, GraphicsPage page, DirectedLine line, LineRole lineRole, bool isDoubleLine = false, string guid = null) : base(line.Coord1, line.Coord2)
        {
            this.Window = window;

            this.Page = page;

            this.ShapeType = ShapeType.Line;

            this.LineRole = lineRole;

            if (isDoubleLine)
            {
                LineCompoundType = LineCompoundType.Double;
            }

            else
            {
                LineCompoundType = LineCompoundType.Single;
            }

            if (guid is null)
            {
                Guid = GuidMaintenance.CreateGuid(this);
            }

            else
            {
                Guid = guid;
            }
        }


        public string Guid { get; set; }

        private LineRole lineRole = LineRole.Unknown;

        public LineRole LineRole
        {
            get
            {
                return lineRole;
            }

            set
            {
                lineRole = value;
            }

        } 

        [XmlIgnore]
        public bool GraphicsHasBeenSet { get; set; } = false;

        public LineCompoundType LineCompoundType { get; set; } = LineCompoundType.Single;

        public GraphicsWindow Window { get; set; } = null;

        public GraphicsPage Page { get; set; } = null;

        private GraphicShape _shape = null;

        public GraphicShape Shape
        {
            get
            {
                return _shape;
            }

            set
            {
                if (_shape == value)
                {
                    return;
                }

                if (_shape != null)
                {
                    _shape.Delete();
                }

                _shape = value;
            }
        }

        public ShapeType ShapeType { get; }

        public void SetLineStartpoint(Coordinate coord)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetLineStartpoint(this.Shape, coord.X, coord.Y);
        }

        public Coordinate GetLineStartpoint()
        {
            if (Shape is null)
            {
                return Coordinate.NullCoordinate;
            }

            return VisioInterop.GetLineStartpoint(this.Shape);
        }

        public Coordinate GetLineEndpoint()
        {
            if (Shape is null)
            {
                return Coordinate.NullCoordinate;
            }

            return VisioInterop.GetLineEndpoint(this.Shape);
        }

        public void SetBaseLineColor(Color color)
        {
            if (Shape is null)
            {
                return;
            }

            this.Shape.SetLineColor(color);
        }

        public void SetBaseLineColor(string visioLineColorFormula)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetBaseLineColor(this.Shape, visioLineColorFormula);
        }

        public void SetBaseLineStyle(int lineStyle)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetBaseLineStyle(this.Shape, lineStyle.ToString());
        }

        public void SetBaseLineStyle(string visioLineStyleFormula)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetBaseLineStyle(this.Shape, visioLineStyleFormula);
        }

        public void SetBaseLineOpacity(double opacity)
        {
            if (Shape is null)
            {
                return;
            }

            this.Shape.SetLineOpacity(opacity);
        }

        public void SetBaseLineCompoundType(string visioLineCompoundTypeFormula)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetLineCompoundType(this.Shape, visioLineCompoundTypeFormula);
        }

        public void SetBaseLineWidth(double updtWidthInPts)
        {
            if (Shape is null)
            {
                return;
            }

            this.Shape.SetLineWidth(updtWidthInPts);
        }

        public void SetEndpointArrows(int arrowIndex)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetEndpointArrows(this.Shape, arrowIndex);
        }

        public void Delete()
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.DeleteShape(Shape);
        }

        [XmlIgnore]
        public new double Length
        {
            get
            {
                return VisioInterop.GetLineLength(this.Shape);
            }
        }

        public GraphicsLayerBase GraphicsLayer { get; set; } = null;

        public double GetScaledLineLength(double scale)
        {
            
            return MathUtils.H2Distance(Coord1.X, Coord1.Y, Coord2.X, Coord2.Y) * scale;
        }

        public GraphicShape Draw(Color lineColor, double lineWidthInPts, int visioDashType = 1)
        {
            if (SystemState.LoadingExistingProject && Shape != null)
            {
                return Shape;
            }

            Shape = Page.DrawLine(this, Coord1.X, Coord1.Y, Coord2.X, Coord2.Y, Guid);

            Shape.SetLineColor(lineColor);
            Shape.SetLineStyle(visioDashType.ToString());


            Shape.ParentObject = this;

            Shape.Data1 = "[DirectedLine]";

            if (LineCompoundType == LineCompoundType.Double)
            {
                Shape.SetLineWidth(2.0 * lineWidthInPts);
                VisioInterop.SetLineCompoundType(Shape, "1");
            }

            else
            {
                Shape.SetLineWidth(lineWidthInPts);
            }

            return Shape;
        }


        public new GraphicsDirectedLine ExtendStart(double extensionLength)
        {
            return new GraphicsDirectedLine(this.Window, this.Page, base.ExtendStart(extensionLength), this.LineRole, this.LineCompoundType == LineCompoundType.Double);
        }

        public new GraphicsDirectedLine ExtendEnd(double extensionLength)
        {
            return new GraphicsDirectedLine(this.Window, this.Page, base.ExtendEnd(extensionLength), this.LineRole, this.LineCompoundType == LineCompoundType.Double);
        }

        public new GraphicsDirectedLine Clone()
        {
            DirectedLine clonedDirectedLine = base.Clone();

            return new GraphicsDirectedLine(
                this.Window
                , this.Page
                , clonedDirectedLine
                , this.LineRole);

        }

        public GraphicsDirectedLine CloneBasic(GraphicsWindow window, GraphicsPage page)
        {
            DirectedLine clonedDirectedLine = base.Clone();

            return new GraphicsDirectedLine(
                window
                , page
                , clonedDirectedLine
                , this.LineRole);

        }

        public static explicit operator GraphicShape(GraphicsDirectedLine graphicDirectedLine)
        {
            return graphicDirectedLine.Shape;
        }

    }
}
