
//-----------------------------------------------------------------------------------------------------------------//
// TODO: Group all the elements of the field guide and make it into one shape. Have it as an IGraphicsShape object //
//-----------------------------------------------------------------------------------------------------------------//
namespace CanvasLib.Markers_and_Guides
{
    
    using System.Drawing;
  
    using Graphics;
    using Utilities;
   

    public class FieldGuide
    {
        private GraphicsWindow window;

        private GraphicsPage page;

        GraphicShape[] lineShapeElements;

        public double X;
        public double Y;

        public double LineWidth;
        public Color LineColor;
        public double Opacity;
        public int LineType;

        double pageWidth;
        double pageHeight;

        public string Guid;

        private GraphicsLayerBase fieldGuideLayerBase;

        public FieldGuide(
            GraphicsWindow window
            , GraphicsPage page
            , double x
            , double y
            , int lineStyle
            , Color lineColor
            , double opacity
            , double lineWidth
            , double pageWidth
            , double pageHeight
            , GraphicsLayerBase layerBase)
        {

            this.window = window;

            this.page = page;

            this.X = x;
            this.Y = y;
            this.LineType = lineStyle;
            this.LineWidth = lineWidth;
            this.LineColor = lineColor;
            this.Opacity = opacity;

            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.fieldGuideLayerBase = layerBase;

        }

        public void Draw()
        {

            lineShapeElements = new GraphicShape[6];

            for (int i = 0; i < 6; i++)
            {
                lineShapeElements[i] = null;
            }

            if (X >= 0 && Y >= 0)
            {
                lineShapeElements[0] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(X - 0.075, Y, X + 0.075, Y), ShapeType.Line);
                lineShapeElements[1] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(X, Y - 0.075, X, Y + 0.075), ShapeType.Line);

                lineShapeElements[0].SetShapeData1("Field Guide Marker-H");
                lineShapeElements[1].SetShapeData1("Field Guide Marker-V");

                lineShapeElements[2] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(0, Y, X - 0.125, Y), ShapeType.Line);
                lineShapeElements[3] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(X + 0.125, Y, pageWidth, Y), ShapeType.Line);
                lineShapeElements[4] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(X, 0, X, Y - 0.125), ShapeType.Line);
                lineShapeElements[5] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(X, Y + 0.125, X, pageHeight), ShapeType.Line);

                lineShapeElements[2].SetShapeData1("Field Guide-L");
                lineShapeElements[3].SetShapeData1("Field Guide-R");
                lineShapeElements[4].SetShapeData1("Field Guide-U");
                lineShapeElements[5].SetShapeData1("Field Guide-D");

                for (int i = 0; i < 2; i++)
                {
                    GraphicShape shape = lineShapeElements[i];

                    if (!(shape is null))
                    {
                        VisioInterop.FormatFieldMarkerLine(lineShapeElements[i], LineColor, Opacity, LineWidth);
                    }
                }

                //for (int i = 0; i < 6; i++)
                //{
                //    if (lineShapeElements[i] != null)
                //    {
                //        Page.AddToPageShapeDict(lineShapeElements[i]);
                //    }
                //}
            }

            else if (X == -100000)
            {
                lineShapeElements[3] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(0, Y, pageWidth, Y), ShapeType.Line);

                lineShapeElements[3].SetShapeData1("Field Guide-H"); 
            }


            else if (Y == -100000)
            {
                lineShapeElements[4] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(X, 0, X, pageHeight), ShapeType.Line);

                lineShapeElements[4].SetShapeData1("Field Guide-V");
            }

            for (int i = 2; i < 6; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    VisioInterop.FormatFieldGuideLine(lineShapeElements[i], LineColor, Opacity, LineWidth, LineType);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (lineShapeElements[i] != null)
                {
                    page.AddToPageShapeDict(lineShapeElements[i]);
                }
            }

            AddToLayer(fieldGuideLayerBase);

        }

        public void AddToLayer(GraphicsLayerBase layerBase)
        {

            for (int i = 0; i < lineShapeElements.Length; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    layerBase.AddShape(shape, 1);
                }
            }
        }

        public void Delete()
        {
            if (lineShapeElements == null)
            {
                return;
            }

            RemoveFromLayer(fieldGuideLayerBase);

            for (int i = 0; i < lineShapeElements.Length; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    page.RemoveFromPageShapeDict(shape);

                    VisioInterop.DeleteShape(shape);
                }
            }
        }

        public void RemoveFromLayer(GraphicsLayerBase layerBase)
        {
            for (int i = 0; i < lineShapeElements.Length; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    layerBase.RemoveShapeFromLayer(shape, 1);
                }
            }
        }

        internal void SetLineOpacity(double lineOpacity)
        {
            for (int i = 0; i < 6; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    VisioInterop.FormatFieldGuideLineOpacity(shape, lineOpacity);
                }
            }
        }

        internal void SetLineWidth(double lineWidthInPts)
        {
            for (int i = 0; i < 6; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    VisioInterop.FormatFieldGuideLineWidth(shape, lineWidthInPts);
                }
            }
        }

        internal void SetLineColor(Color lineColor)
        {
            for (int i = 0; i < 6; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    VisioInterop.FormatFieldGuideLineColor(shape, lineColor);
                }
            }
        }

        internal void SetLineType(int lineStyle)
        {
            for (int i = 2; i < 6; i++)
            {
                GraphicShape shape = lineShapeElements[i];

                if (!(shape is null))
                {
                    VisioInterop.FormatFieldGuideLineStyle(lineShapeElements[i], lineStyle);
                }
            }
        }
    }
}
