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

    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using CanvasLib.Counters;
    using Geometry;
    using System.Collections.Generic;
    using System;
    using System.Drawing;

    /// <summary>
    /// The LegendElement is an element on the legend corresponding to a specific area finish.
    /// </summary>
    public class AreaLegendCounterElement: IGraphicsShape
    {
        public Counter Counter { get; set; }

        public GraphicShape Shape { get; set; } = null;

        public GraphicsPage Page { get; set; } = null;

        private AreaModeLegend areaModeLegend;

        public GraphicsWindow Window { get; set; } = null;

        public ShapeType ShapeType { get; set; } = ShapeType.Unknown;

        public double BaseX { get; set; }
        public double BaseY { get; set; }

        private double sizeX;
        private double textSizeY;
        private double areaSizeY;

        private int fontSize = 10;

        public bool Filtered => Counter.Filtered;

        public string Guid => Counter.Tag.ToString();
        public GraphicsLayerBase GraphicsLayer { get; set; } = null;

        //private GraphicShape[] counterShapeElements = new GraphicShape[2];


        #region Events and Delegates

        public delegate void FilteredChangedHandler(AreaLegendCounterElement legendElement, bool filtered);

        public event FilteredChangedHandler FilteredChanged;

        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the legend element
        /// </summary>
        /// <param name="window">The graphics Window for this graphics object</param>
        /// <param name="page">The graphics Page for this graphics object</param>
        /// <param name="areaFinishBase">The area finish base element associated with this legend element</param>
        /// <param name="sizeX">The width of the element when drawn</param>
        /// <param name="textSizeY">The height of the text component</param>
        /// <param name="areaSizeY">The height of the area finish (graphics) component</param>
        /// <param name="posnOnPalette">The position of this element on the legend</param>
        public AreaLegendCounterElement(
            GraphicsWindow window,
            GraphicsPage page,
            AreaModeLegend areaModeLegend,
            Counter counter)
            
        {
            this.Window = window;
            this.Page = page;
            this.areaModeLegend = areaModeLegend;

            this.Counter = counter;

            this.Counter.CounterDescriptionChanged += Counter_CounterDescriptionChanged;
            this.Counter.CounterFilteredChanged += Counter_CounterFilteredChanged;
            this.Counter.CounterColorChanged += Counter_CounterColorChanged;
            this.Counter.CounterFilteredChanged += Counter_CounterFilteredChanged1;
        }

        #endregion

        /// <summary>
        /// Draws the legend element on the visio canvas
        /// </summary>
        /// <param name="baseX">x coord for the location to be drawn</param>
        /// <param name="baseY">y coord for the location to be drawn</param>
        /// <returns></returns>
        public GraphicShape Draw(
            double baseX,
            double baseY,
            double sizeX,
            double sizeY)
        {
            this.BaseX = baseX;
            this.BaseY = baseY;

            this.sizeX = sizeX;
            this.textSizeY = textSizeY;
            this.areaSizeY = areaSizeY;

            double diameter = Math.Min(sizeX, sizeY);

            double radius = diameter * 0.5;

            double centerX = baseX + radius + (sizeX - diameter) * 0.5;
            double centerY = baseY + radius + (sizeY - diameter) * 0.5 - sizeY;

            fontSize = (int) Math.Round(radius * 80.0);

            /*mdd*/
            //Console.WriteLine(Counter.Description);
           // Console.WriteLine("Center: (" + centerX.ToString("0.00") + ", " + centerY.ToString("0.00") + "), radius: " + radius.ToString("0.00"));
            Shape = Page.DrawCircle(this, new Coordinate(centerX, centerY), sizeY * 0.5, Counter.Color);

            Shape.ParentObject = this;
            Shape.SetShapeData1("[Counter]");
            Shape.SetShapeData2("Counter Element");
            Shape.ShapeType |= ShapeType.CounterShapeElement;
            Shape.SetShapeText(Counter.Tag.ToString(), System.Drawing.Color.Black, fontSize);
            Shape.CenterText();
            VisioInteropDebugger.TestTextCentering(Shape.VisioShape);

            Shape.SetFillColor(Color.White);
            Window?.DeselectAll();

            return Shape;
        }

        public double GetHeightEstimate()
        {
            double circleHeight = 2 * this.sizeX;
            double rectangleHeight = areaSizeY;

            return Math.Max(circleHeight, rectangleHeight);
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

            //    //counterShapeElements[0].Delete();
            //    //counterShapeElements[1].Delete();

            //    Shape = null;

            //    counterShapeElements[0] = null;
            //    counterShapeElements[1] = null;

            //}

            this.Counter.CounterFilteredChanged -= Counter_CounterFilteredChanged;
            this.Counter.CounterDescriptionChanged -= Counter_CounterDescriptionChanged;
            this.Counter.CounterColorChanged -= Counter_CounterColorChanged;
            this.Counter.CounterFilteredChanged -= Counter_CounterFilteredChanged1;
            Shape = null;
        }

        private void Counter_CounterFilteredChanged(Counter counter, bool filtered)
        {
            if (FilteredChanged != null)
            {
                FilteredChanged.Invoke(this, filtered);
            }
        }

        private void Counter_CounterDescriptionChanged(Counter counter, string description)
        {
            Shape.SetShapeText(Counter.Description, System.Drawing.Color.Black, fontSize);
        }


        private void Counter_CounterColorChanged(Counter counter, System.Drawing.Color color)
        {
            if (Shape is null)
            {
                return;
            }

            Shape.SetLineColor(Counter.Color);
            Shape.SetShapeText(Counter.Tag.ToString(), Counter.Color, fontSize);
        }

        private void Counter_CounterFilteredChanged1(Counter counter, bool filtered)
        {
            areaModeLegend.Init();
        }


    }
}
