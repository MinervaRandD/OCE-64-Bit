//-------------------------------------------------------------------------------//
// <copyright file="LegendElement.cs"                                            //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                              //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace CanvasLib.Legend
{
    using Graphics;
    using FinishesLib;
   
    using System.Drawing;

    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using System;

    /// <summary>
    /// The LegendElement is an element on the legend corresponding to a specific area finish.
    /// </summary>
    public class AreaLegendFinishElement: IGraphicsShape
    {
        public GraphicsWindow Window { get; set; } = null;

        public GraphicsPage Page { get; set; } = null;

        public AreaFinishBase AreaFinishBase { get; set; }

        public GraphicShape Shape { get; set; } = null;

        private GraphicShape nameShape;

        private GraphicShape areaShape;

        private GraphicShape[] seamShape = new GraphicShape[2];


        private AreaModeLegend areaModeLegend = null;

        public ShapeType ShapeType { get; set; } = ShapeType.Unknown;

        public GraphicsLayerBase GraphicsLayer { get; set; } = null;

        public double BaseX { get; set; }
        public double BaseY { get; set; }

        public double SizeX { get; set; }
        public double SizeY { get; set; }


        private double textSizeY;
        private double areaSizeY;

        public int PosnOnPalette { get; set; }

        public bool Filtered => AreaFinishBase.Filtered;

        public string Guid => AreaFinishBase.Guid;

        #region Events and Delegates

        #endregion

        #region Constructors

        public AreaLegendFinishElement(
            GraphicsWindow window,
            GraphicsPage page,
            AreaModeLegend areaModeLegend,
            AreaFinishBase areaFinishBase)
        {
            this.Window = window;
            this.Page = page;
            this.areaModeLegend = areaModeLegend;

            this.AreaFinishBase = areaFinishBase;

            this.AreaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
            this.AreaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            this.AreaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;
            this.AreaFinishBase.OpacityChanged += AreaFinishBase_OpacityChanged;
            this.AreaFinishBase.PatternChanged += AreaFinishBase_PatternChanged;
            this.AreaFinishBase.FilteredChanged += AreaFinishBase_FilteredChanged;
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
            this.areaSizeY = areaSizeY;

            this.SizeY = this.textSizeY + this.areaSizeY;

            this.PosnOnPalette = posnOnPalette;

           // int fontSize = (int)(textSizeY * 30.0);

           double x1 = baseX;
           double x2 = baseX + sizeX;
           double y1 = baseY-textSizeY;
           double y2 = baseY;

            nameShape = Page.DrawTextBox(this, x1, y1, x2, y2, AreaFinishBase.AreaName);

            nameShape.ShowShapeOutline(false);
            
            nameShape.SetTextFontSize(fontSize);

            nameShape.CenterText();
            
            double width = 0;

            y2 = y1;

            y1 -= areaSizeY;
           
            areaShape = Page.DrawRectangle(this, x1, y1, x2, y2);
          
            areaShape.VisioShape.Data1 = "[LegendElement]" + AreaFinishBase.AreaName;

            areaShape.ShowShapeOutline(true);

            int translatedPattern = translatePatternForElement(AreaFinishBase.Pattern);

            VisioInterop.SetFillPattern(areaShape, translatedPattern.ToString());

            areaShape.BringToFront();

            if (translatedPattern == 0)
            {
                VisioInterop.SetBaseFillColor(areaShape, AreaFinishBase.Color);
                VisioInterop.SetFillOpacity(areaShape, AreaFinishBase.Opacity);
            }

            else
            {

                VisioInterop.SetPatternColor(areaShape, AreaFinishBase.Color);
                VisioInterop.SetPatternOpacity(areaShape, AreaFinishBase.Opacity);
            }

            SeamFinishBase seamFinishBase = AreaFinishBase.SeamFinishBase;

            double yIncr = areaSizeY / 3.0;

            y1 = baseY - textSizeY - yIncr;
            y2 = baseY - textSizeY - 2 * yIncr;

            seamShape[0] = Page.DrawLine(this, baseX, y1, baseX + SizeX, y1, string.Empty);
            seamShape[1] = Page.DrawLine(this, baseX, y2, baseX + SizeX, y2, string.Empty);

            setupSeams();

            Visio.Selection selection = Page.VisioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSub, areaShape);

            selection.Select(areaShape.VisioShape, 2);
            selection.Select(nameShape.VisioShape, 2);

            if (seamShape[0] != null)
            {
                selection.Select(seamShape[0].VisioShape, 2);
            }

            if (seamShape[1] != null)
            {
                selection.Select(seamShape[1].VisioShape, 2);
            }

            Shape = new GraphicShape(this, this.Window, this.Page, selection.Group(), ShapeType.Legend);
           
            VisioInterop.SetShapeData(Shape, "[AreaLegendFinishElement]" + AreaFinishBase.AreaName, "Legend Element", Shape.Guid);

            Shape.ParentObject = this;

            SizeY = VisioInterop.GetShapeHeight(Shape);

           
            //The following is apparently a very expensive call, so done at a higher level.
            Window?.DeselectAll();

            return Shape;
        }


        /// <summary>
        /// Deletes the legend element. This also removes it from the visio canvas.
        /// </summary>
        public void Delete()
        {
            //if (Utilities.IsNotNull(Shape))
            //{
            //    Page.RemoveFromPageShapeDict(Shape);

            //    Page.AreaLegendLayer.RemoveShapeFromLayer(Shape, 0);

            //    Shape.Delete();
            //}

            this.AreaFinishBase.AreaNameChanged -= AreaFinishBase_AreaNameChanged;
            this.AreaFinishBase.ColorChanged -= AreaFinishBase_ColorChanged;
            this.AreaFinishBase.FinishSeamChanged -= AreaFinishBase_FinishSeamChanged;
            this.AreaFinishBase.OpacityChanged -= AreaFinishBase_OpacityChanged;
            this.AreaFinishBase.PatternChanged -= AreaFinishBase_PatternChanged;
            this.AreaFinishBase.FilteredChanged -= AreaFinishBase_FilteredChanged;

            Shape = null;
        }

        /// <summary>
        /// Updates the legend element text if the name of the area finish is changed globally.
        /// </summary>
        /// <param name="finishBase">The area finish for which the name has been changed. Ignored.</param>
        /// <param name="areaName">the new area name. Ignored.</param>
        private void AreaFinishBase_AreaNameChanged(AreaFinishBase finishBase, string areaName)
        {
            if (nameShape is null)
            {
                return;
            }

            nameShape.SetShapeText(this.AreaFinishBase.AreaName, Color.Black, 8);
        }

        /// <summary>
        /// Updates the color of the legend element if the color is changed globally.
        /// </summary>
        /// <param name="finishBase">The area finish for which the color has changed. Ignored.</param>
        /// <param name="color">The new color. Ignored.</param>
        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, System.Drawing.Color color)
        {
            if (areaShape is null)
            {
                return;
            }

            if (false && AreaFinishBase.Pattern == 0)
            {
                VisioInterop.SetBaseFillColor(Shape, AreaFinishBase.Color);
                VisioInterop.SetFillOpacity(Shape, AreaFinishBase.Opacity);
            }

            else
            {
                VisioInterop.SetPatternColor(areaShape, AreaFinishBase.Color);
                VisioInterop.SetPatternOpacity(areaShape, AreaFinishBase.Opacity);
            }
        }

        private void AreaFinishBase_PatternChanged(AreaFinishBase finishBase, int pattern)
        {
            if (areaShape is null)
            {
                return;
            }

            int translatedPattern = translatePatternForElement(pattern);

            if (translatedPattern == 0)
            {
                VisioInterop.SetFillPattern(areaShape,"1");

                VisioInterop.SetBaseFillColor(Shape, AreaFinishBase.Color);
                VisioInterop.SetFillOpacity(Shape, AreaFinishBase.Opacity);
            }

            else
            {

                VisioInterop.SetFillPattern(areaShape, translatedPattern.ToString());

                VisioInterop.SetPatternColor(areaShape, AreaFinishBase.Color);
                VisioInterop.SetPatternOpacity(areaShape, AreaFinishBase.Opacity);
            }

        }

        /// <summary>
        /// Updates the opacity of the area finish if it has been changed globally.
        /// </summary>
        /// <param name="finishBase">The area finish for which the opacity has changed. Ignored.</param>
        /// <param name="opacity">The new opacity. Ignored.</param>
        private void AreaFinishBase_OpacityChanged(AreaFinishBase finishBase, double opacity)
        {
            if (areaShape is null)
            {
                return;
            }

            if (false && AreaFinishBase.Pattern == 0)
            {
                VisioInterop.SetBaseFillColor(Shape, AreaFinishBase.Color);
                VisioInterop.SetFillOpacity(Shape, AreaFinishBase.Opacity);
            }

            else
            {
                VisioInterop.SetPatternColor(areaShape, AreaFinishBase.Color);
                VisioInterop.SetPatternOpacity(areaShape, AreaFinishBase.Opacity);
            }

        }

        /// <summary>
        /// Updtes the seams displayed on the area finish elements when they are changed globally.
        /// </summary>
        /// <param name="finishBase">The area finish for which the seam has changed. Ignored.</param>
        /// <param name="seamFinishBase">The seam finish base which has been changed. Ignored.</param>
        private void AreaFinishBase_FinishSeamChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            setupSeams();
        }


        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            areaModeLegend.Init();
        }

        /// <summary>
        /// Set up the seam display.
        /// </summary>
        private void setupSeams()
        {
            SeamFinishBase seamFinishBase = AreaFinishBase.SeamFinishBase;

            if (seamFinishBase is null)
            {
                seamShape[0].ShowShapeOutline(false);
                seamShape[1].ShowShapeOutline(false);
            }

            else if (seamShape[0] != null && seamShape[1] != null)
            {
                seamShape[0].SetLineColor(seamFinishBase.SeamColor);
                seamShape[1].SetLineColor(seamFinishBase.SeamColor);

                seamShape[0].SetLineStyle(seamFinishBase.VisioDashType.ToString());
                seamShape[1].SetLineStyle(seamFinishBase.VisioDashType.ToString());

                seamShape[0].ShowShapeOutline(true);
                seamShape[1].ShowShapeOutline(true);
            }
        }

        private int translatePatternForElement(int inptPattern)
        {
            if (inptPattern == 1)   
            {
                return 19;
            }

            if (inptPattern == 2)
            {
                return 20;
            }

            if (inptPattern == 3)
            {
                return 23;
            }

            return inptPattern;
        }

    }
}
