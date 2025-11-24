//-------------------------------------------------------------------------------//
// <copyright file="LegendElement.cs"                                            //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

using System;
using System.ComponentModel;
using Geometry;

namespace CanvasLib.Legend
{
    using Graphics;
    using FinishesLib;

    using System.Drawing;

    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    /// <summary>
    /// The LegendElement is an element on the legend corresponding to a specific line finish.
    /// </summary>
    public class LineLegendElement
    {
        private GraphicsWindow Window;

        private GraphicsPage Page;

        public LineFinishBase LineFinishBase { get; set; }

        public GraphicShape Shape { get; set; } = null;

        public GraphicShape nameShape { get; set; } = null;

        public GraphicShape lineShape;

        private LineModeLegend lineModeLegend;

        public double BaseX { get; set; }
        public double BaseY { get; set; }

        public double SizeX { get; set; }
        public double SizeY { get; set; }

        private double textSizeY;

        private double lineSizeY;

        public int PosnOnPalette { get; set; }

        public bool Filtered => LineFinishBase.Filtered;

        public string Guid => LineFinishBase.Guid;

        #region Events and Delegates

        #endregion

        #region Constructors

        public LineLegendElement(
            GraphicsWindow window,
            GraphicsPage page,
            LineModeLegend lineModeLegend,
            LineFinishBase lineFinishBase)
        {
            this.Window = window;
            this.Page = page;
            this.lineModeLegend = lineModeLegend;
            this.LineFinishBase = lineFinishBase;

            this.LineFinishBase.LineNameChanged += LineFinishBase_LineNameChanged;
            this.LineFinishBase.LineColorChanged += LineFinishBase_LineColorChanged;
            this.LineFinishBase.FilteredChanged += LineFinishBase_FilteredChanged;
            this.LineFinishBase.LineWidthChanged += LineFinishBase_LineWidthChanged;
            this.LineFinishBase.LineTypeChanged += LineFinishBase_LineTypeChanged;
        }

        #endregion

        public GraphicShape Draw(
            double baseX,
            double baseY,
            double sizeX,
            double textSizeY,
            double areaSizeY,
            int fontSize,
            int posnOnPalette
        )
        {
            this.BaseX = baseX;
            this.BaseY = baseY;
            this.SizeX = sizeX;

            this.textSizeY = textSizeY;
            this.lineSizeY = areaSizeY;

            this.SizeY = this.textSizeY + this.lineSizeY;

            this.PosnOnPalette = posnOnPalette;

            double x1 = baseX;
            double x2 = baseX + sizeX;
            double y1 = baseY - textSizeY;
            double y2 = baseY;

            nameShape = Page.DrawTextBox(this, x1, y1, x2, y2, LineFinishBase.LineName);

            nameShape.ShowShapeOutline(false);

            nameShape.SetTextFontSize(fontSize);

            nameShape.CenterText();
            //nameShape.SetTextVerticalAlignment(0);
           
            lineShape = Page.DrawLine(this, x1, y1, x2, y1, "");

            lineShape.ShowShapeOutline(true);

            
            lineShape.SetLineWidth(LineFinishBase.LineWidthInPts * SizeX);

            lineShape.SetFillColor(Color.White);

            lineShape.SetBaseLineColor(LineFinishBase.LineColor);
            lineShape.SetBaseLineStyle(LineFinishBase.VisioLineType);

            GraphicShape[] shapeArray = new GraphicShape[2] { nameShape, lineShape };

            Shape = VisioInterop.GroupShapes(Window, shapeArray);

            string guid = GuidMaintenance.CreateGuid(Shape);

            Shape.Guid = guid;

            Shape.SetFillColor(Color.White);

            VisioInterop.SetShapeData(Shape, "[LineLegendElement]" + LineFinishBase.LineName, "LegendElement", guid);

            Shape.Page = Page;

            Shape.Window = Window;

            Shape.ParentObject = this;

            Page.AddToPageShapeDict(Shape);

            Window?.DeselectAll();

            return Shape;
        }

        /// <summary>
        /// Deletes the legend element. This also removes it from the visio canvas.
        /// </summary>
        public void Delete()
        {
            this.LineFinishBase.LineNameChanged -= LineFinishBase_LineNameChanged;
            this.LineFinishBase.LineColorChanged -= LineFinishBase_LineColorChanged;
            this.LineFinishBase.FilteredChanged -= LineFinishBase_FilteredChanged;
            this.LineFinishBase.LineWidthChanged -= LineFinishBase_LineWidthChanged;
            this.LineFinishBase.LineTypeChanged -= LineFinishBase_LineTypeChanged;

            Shape = null;
        }

        /// <summary>
        /// Updates the legend element text if the name of the line finish is changed globally.
        /// </summary>
        /// <param name="finishBase">The line finish for which the name has been changed. Ignored.</param>
        /// <param name="lineName">the new line name. Ignored.</param>
        private void LineFinishBase_LineNameChanged(LineFinishBase finishBase, string lineName)
        {
            if (nameShape is null)
            {
                return;
            }

            nameShape.SetShapeText(this.LineFinishBase.LineName, Color.Black, 8);
        }

        private void LineFinishBase_LineTypeChanged(LineFinishBase lineFinishBase, int lineType)
        {
            if (lineShape == null)
            {
                return;
            }

            lineShape.SetLineType(lineType);
        }

        private void LineFinishBase_LineWidthChanged(LineFinishBase LineFinishBase, double lineWidthInPts)
        {
            if (lineShape == null)
            {
                return;
            }

            lineShape.SetBaseLineWidth(lineWidthInPts);
        }

        private void LineFinishBase_LineColorChanged(LineFinishBase LineFinishBase, Color lineColor)
        {
            if (lineShape == null)
            {
                return;
            }

            lineShape.SetBaseLineColor(lineColor);
        }

        /// <summary>
        /// Responds to a change in the filter status change. We invoke an event which is picked up and responded
        /// to by the legend class.
        /// </summary>
        /// <param name="finishBase"></param>
        /// <param name="filtered"></param>
        private void LineFinishBase_FilteredChanged(LineFinishBase finishBase, bool filtered)
        {
            lineModeLegend.Init();
        }

    }
}
