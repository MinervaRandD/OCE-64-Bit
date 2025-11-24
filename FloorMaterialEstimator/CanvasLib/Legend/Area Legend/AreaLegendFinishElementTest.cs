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

namespace CanvasLib.Legend
{
    using Graphics;
    using FinishesLib;
   
    using System.Drawing;

    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    /// <summary>
    /// The LegendElement is an element on the legend corresponding to a specific area finish.
    /// </summary>
    public class AreaLegendFinishElement
    {
        public AreaFinishBase AreaFinishBase { get; set; }

        public Shape Shape { get; set; } = null;

        private Shape nameShape;
        private Shape areaShape;

        private Shape[] seamShape = new Shape[2];

        private GraphicsPage page;

        private GraphicsWindow window;

        public double BaseX { get; set; }
        public double BaseY { get; set; }

        private double sizeX;
        private double textSizeY;
        private double areaSizeY;

        public int PosnOnPalette { get; set; }

        public bool Filtered => AreaFinishBase.Filtered;

        public string Guid => AreaFinishBase.Guid;

        #region Events and Delegates

        public delegate void FilteredChangedHandler(AreaLegendFinishElement legendElement, bool filtered);

        public event FilteredChangedHandler FilteredChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the legend element
        /// </summary>
        /// <param name="window">The graphics window for this graphics object</param>
        /// <param name="page">The graphics page for this graphics object</param>
        /// <param name="areaFinishBase">The area finish base element associated with this legend element</param>
        /// <param name="sizeX">The width of the element when drawn</param>
        /// <param name="textSizeY">The height of the text component</param>
        /// <param name="areaSizeY">The height of the area finish (graphics) component</param>
        /// <param name="posnOnPalette">The position of this element on the legend</param>
        public AreaLegendFinishElement(
            GraphicsWindow window,
            GraphicsPage page,
            AreaFinishBase areaFinishBase,
            double sizeX,
            double textSizeY,
            double areaSizeY,
            int posnOnPalette)
        {
            this.window = window;
            this.page = page;

            this.AreaFinishBase = areaFinishBase;

            this.sizeX = sizeX;
            this.textSizeY = textSizeY;
            this.areaSizeY = areaSizeY;

            this.AreaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
            this.AreaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            this.AreaFinishBase.FilteredChanged += AreaFinishBase_FilteredChanged;
            this.AreaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;
            this.AreaFinishBase.OpacityChanged += AreaFinishBase_OpacityChanged;
    
            this.PosnOnPalette = posnOnPalette;
        }

        #endregion

        /// <summary>
        /// Draws the legend element on the visio canvas
        /// </summary>
        /// <param name="baseX">x coord for the location to be drawn</param>
        /// <param name="baseY">y coord for the location to be drawn</param>
        /// <returns></returns>
        public Shape Draw(
            double baseX,
            double baseY)
        {
            this.BaseX = baseX;
            this.BaseY = baseY;

            areaShape = page.DrawRectangle(baseX, baseY - textSizeY - areaSizeY, baseX + sizeX, baseY - textSizeY);

            areaShape.ShowShapeOutline(false);

            VisioInterop.SetFillPattern(this.areaShape, "7", AreaFinishBase.Color);

            this.Shape = areaShape;

            return areaShape;
        }

        /// <summary>
        /// Removes the legend element from the visio canvas
        /// </summary>
        public void Undraw()
        {
            VisioInterop.DeleteShape(nameShape);
            VisioInterop.DeleteShape(areaShape);
            VisioInterop.DeleteShape(seamShape[0]);
            VisioInterop.DeleteShape(seamShape[1]);

            Shape = null;
        }

        /// <summary>
        /// Deletes the legend element. This also removes it from the visio canvas.
        /// </summary>
        public void Delete()
        {
            if (Utilities.IsNotNull(Shape))
            {
                page.RemoveFromPageShapeDict(Shape);

                page.AreaLegendLayer.RemoveShapeFromLayer(Shape, 0);

                Shape.Delete();
            }

            this.AreaFinishBase.AreaNameChanged -= AreaFinishBase_AreaNameChanged;
            this.AreaFinishBase.ColorChanged -= AreaFinishBase_ColorChanged;
            this.AreaFinishBase.FilteredChanged -= AreaFinishBase_FilteredChanged;
            this.AreaFinishBase.FinishSeamChanged -= AreaFinishBase_FinishSeamChanged;
            this.AreaFinishBase.OpacityChanged -= AreaFinishBase_OpacityChanged;

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

            VisioInterop.SetBaseFillColor(areaShape, this.AreaFinishBase.Color);
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

            //VisioInterop.SetFillPattern(this.Shape, "7", AreaFinishBase.Color);
            //VisioInterop.SetPatternColor(Shape, AreaFinishBase.Color);
            VisioInterop.SetPatternOpacity(areaShape, AreaFinishBase.Opacity);
            //if (AreaFinishBase.Pattern == 0)
            //{
            //    VisioInterop.SetBaseFillColor(Shape, AreaFinishBase.Color);
            //    VisioInterop.SetFillOpacity(Shape, AreaFinishBase.Opacity);
            //}

            //else
            //{
            //    VisioInterop.SetPatternColor(Shape, AreaFinishBase.Color);
            //    VisioInterop.SetPatternOpacity(Shape, AreaFinishBase.Opacity);
            //}

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

        /// <summary>
        /// Responds to a change in the filter status change. We invoke an event which is picked up and responded
        /// to by the legend class.
        /// </summary>
        /// <param name="finishBase"></param>
        /// <param name="filtered"></param>
        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            if (FilteredChanged != null)
            {
                FilteredChanged.Invoke(this, filtered);
            }
        }

    }
}
