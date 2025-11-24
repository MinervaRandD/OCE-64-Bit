//-------------------------------------------------------------------------------//
// <copyright file="Legend.cs"                                                   //
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
    using System.Collections.Generic;
    using System.Linq;
    using Graphics;
    using FinishesLib;
    using Geometry;
    using System;
    using CanvasLib.Counters;
    using System.Drawing;
    using Globals;

    /// <summary>
    /// This class instantiates the legends that can appear on the canvas at the users request.
    /// 
    /// Note that the approach here is pretty 'brute force' -- reconstructing the legend in response
    /// to changes in the line finish base list, rather than updating elements on a case by case basis.
    /// The latter would be more elegant and better programming, but as a first implementation, this is 
    /// done to save implementation time.
    /// </summary>
    /// 
    public class LineModeLegend
    {
        private GraphicsPage Page { get; } = null;

        private GraphicsWindow Window { get; } = null;

        public GraphicShape Shape
        {
            get;
            set;
        }

        private LineFinishBaseList lineFinishBaseList { get; } = null;

        public Coordinate Location
        {
            get => SystemGlobals.LineLegendLocation;
            set => SystemGlobals.LineLegendLocation = value;
        }

        double areaSizeX;
        double areaSizeY; 
        double textSizeY;

        private GraphicShape baseRectangle;

        private GraphicsLayerBase lineLegendLayer => Page.LineLegendLayer;

        public bool Visible
        {
            get;
            set;
        }

        public string Notes => SystemGlobals.LineLegendNotes;

        public double CurrentSize = 0;

        private LegendLocation _legendShowLocation = LegendLocation.NotSet;

        public LegendLocation LegendShowLocation
        {
            get
            {
                return _legendShowLocation;
            }

            set
            {
                _legendShowLocation = value;
            }
        }


        // This dictionary contains the line base list elements. We need to rebuild this because of the
        // order of event generation the lists in the line base list may not be up to date.

        // This dictionary mirrors what is actually in the line base list.
        private Dictionary<string, LineLegendElement> legendElementDict = new Dictionary<string, LineLegendElement >();

        private IEnumerable<LineLegendElement> legendElements => legendElementDict.Values;

        private IEnumerable<LineLegendElement> unfilteredLegendFinishElements => legendElements.Where(l => l.Filtered == false);

        private int unfilteredElementCount => legendElements.Count(l => l.Filtered == false);
        
        #region Constructors and Initializers

        /// <summary>
        /// Construct with Window and Page as with all graphics objects.
        /// The line finish base list is the list of finishes.
        /// </summary>
        /// <param name="window">Current graphics Window</param>
        /// <param name="page">Current graphics Page</param>
        /// <param name="lineFinishBaseList">The line finish base list to display</param>
        public LineModeLegend(
            
            GraphicsWindow window
            ,GraphicsPage page
            ,LineFinishBaseList lineFinishBaseList
            )
        {
            //this.areaLegendNavigationForm = areaLegendNavigationForm;

            this.Page = page;

            this.Window = window;

            this.lineFinishBaseList = lineFinishBaseList;

            // Set up subscriptions to events, so as the base list changes, so does the legend.

            lineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;
            lineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;
            lineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;
            lineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;

            LegendShowLocation = LegendLocation.NotSet;

            Shape = null;
        }

        private bool initialSetup = false;

        /// <summary>
        /// Initialize the legend and draw it at the base location (xBase, yBase). This routine actually draws the legend
        /// on the Visio canvas.
        /// </summary>
        /// <param name="xBase">x coord at which to show the legend</param>
        /// <param name="yBase">y coord at which to show the legend</param>
        public void Init(bool resize = false)
        {
            if (!initialSetup)
            {
                initialSetup = true;

                resize = true;
            }

            if (Shape != null)
            {
                Page.RemoveFromPageShapeDict(Shape);

                bool lineLegendLayerLocked = lineLegendLayer.IsLocked();

                if (lineLegendLayerLocked)
                {
                    lineLegendLayer.UnLock();
                }

                /*mdd*/
                //Console.WriteLine("Area Mode Legend remove shape from layer");
                lineLegendLayer.RemoveShapeFromLayer(Shape);
                Shape.Delete();
                Shape = null;

                if (lineLegendLayerLocked)
                {
                    lineLegendLayer.Lock();
                }
               
            }

            foreach (LineLegendElement  lineLegendFinishElement in legendElementDict.Values)
            {
                lineLegendFinishElement.Delete();
            }

            legendElementDict.Clear();
            
            // scale is 1 when size is maxed out

            double scale = SystemGlobals.LineLegendScale;


            // Create Elements

            if (SystemGlobals.ShowAreaLegendFinishes)
            {

                foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
                {
                    LineLegendElement  legendElement = new LineLegendElement (Window, Page, this, lineFinishBase);

                    legendElementDict.Add(legendElement.Guid,
                        legendElement); // Contains all elements for all defined areas

                }
            }

            if (resize)
            {
                // Calculate the total count

                double elementCount = legendElementDict.Count +
                                      (SystemGlobals.AreaLegendNotes != null ? 1 : 0);

                double sizeY = scale * Page.PageHeight / ((double)elementCount * 2.0);

                areaSizeY = sizeY;

                textSizeY = 7.5 * areaSizeY;

                interItemSpace = areaSizeY * 0.5;

                if (LegendShowLocation == LegendLocation.Right &&
                    SystemGlobals.LineLegendLocation.X + areaSizeX > Page.PageWidth)
                {
                    SystemGlobals.LineLegendLocation =
                        new Coordinate(Page.PageWidth - areaSizeX, SystemGlobals.LineLegendLocation.Y);
                }

                double initialScale = SystemGlobals.LineLegendScale;
                ;
                double initialSize = CreateLegendForSizingOnly(initialScale);

                if (initialScale == 0)
                {
                    return;
                }

                double targetHeight = Page.PageHeight * SystemGlobals.LineLegendScale;
                double targetWidth = Page.PageWidth * SystemGlobals.LineLegendScale * 0.2;

                double targetSize = Math.Min(targetHeight, targetWidth);

                areaSizeY *= targetSize / initialSize;
                textSizeY *= targetSize / initialSize;

                areaSizeX = textSizeY * 1.5;
            }

            if (string.IsNullOrEmpty(SystemGlobals.LineLegendNotes) &&
                unfilteredElementCount == 0)
            {
                return;
            }

            Draw();

            List<GraphicShape> legendShapes = new List<GraphicShape>() { baseRectangle };

            foreach (LineLegendElement  legendElement in unfilteredLegendFinishElements)
            {
                legendShapes.Add(legendElement.Shape);
            }

        }

        private const double interTypeHeight = -0.4;

        private double interItemSpace = .10;

        /* Provides an estimate of the actual height (in inches) of the legend, given the scale */

        private double CreateLegendForSizingOnly(double scale)
        {
            bool includeLines = unfilteredElementCount > 0 && SystemGlobals.ShowLineLegendLines;
            bool includeNotes = !string.IsNullOrEmpty(SystemGlobals.LineLegendNotes) && SystemGlobals.ShowLineLegendNotes;

            double locY = Page.PageHeight;
            interItemSpace = areaSizeY * 0.0;

            if (!includeLines && !includeNotes)
            {
                return 0;
            }

            if (includeLines)
            {
                locY -= (areaSizeY + textSizeY + interItemSpace) * (double) unfilteredElementCount;
            }

            if (includeNotes)
            {
                if (includeLines)
                {
                    locY -= interItemSpace * 10.0;
                }

                locY -= textSizeY;
            }


            return Page.PageHeight- locY;
        }
        private GraphicShape finishHeader = null;
        private GraphicShape counterHeader = null;
        private GraphicShape notesHeader = null;
        private GraphicShape notesText = null;

        List<GraphicShape> shapeList = new List<GraphicShape>();
        /// <summary>
        /// Redraw the legend -- place it on the visio canvas.
        /// </summary>
        /// 
        private double Draw()
        {
            shapeList.Clear();

            double locX = SystemGlobals.LineLegendLocation.X;
            double locY = SystemGlobals.LineLegendLocation.Y;

            int fontSize = (int) (textSizeY * 20);

            var dpi = VisioInterop.dotsPerInch();

            double dpiX = (double)dpi.dpiX;

            bool includeLines = unfilteredElementCount > 0 && SystemGlobals.ShowLineLegendLines;
            bool includeNotes = !string.IsNullOrEmpty(SystemGlobals.LineLegendNotes) && SystemGlobals.ShowLineLegendNotes;

            if (!includeLines && !includeNotes)
            {
                return 0;
            }

            int posnOnPalette = 0;

            //locY -= interItemSpace;
            interItemSpace = areaSizeY * 0.5;

            if (includeLines)
            {
                
                foreach (LineLegendElement  legendElement in unfilteredLegendFinishElements)
                {
                   
                    legendElement.Draw(locX, locY, areaSizeX, textSizeY, areaSizeY, (int)Math.Round(20.0 * textSizeY),posnOnPalette);

                    shapeList.Add(legendElement.Shape);

                    legendElement.Shape.BringToFront();

                    posnOnPalette++;

                    locY -= textSizeY + areaSizeY + interItemSpace;
                }
            }

            if (includeNotes)
            {
                if (includeLines)
                {
                    locY -= interItemSpace * 10.0;
                }

                GraphicShape textShape =
                    Page.CreateFixedWidthTextBox(locX, locY, areaSizeX * 2.0, SystemGlobals.LineLegendNotes, (double) fontSize * 1.25);
                
                textShape.SetBaseLineStyle(0);

                shapeList.Add(textShape);

                locY -= textShape.Height;

            }

            double x1 = Location.X;
            double y1 = Location.Y;
            double x2 = x1 + areaSizeX;
            double y2 = locY + interItemSpace;

            //Console.WriteLine("");
            
            //Console.WriteLine("Bounding Box: Lower Left (" + x1.ToString("0.0") + ", " + y1.ToString("0.00") + "), " +
            //                  "Upper Rght (" + x2.ToString("0.0") + ", " + y2.ToString("0.00") + ")");
            //Console.WriteLine("**********************************");

            baseRectangle = Page.DrawRectangle(this, x1, y1, x2, y2);

            //baseRectangle.VisioShape.Data1 = "[Legend]BaseRectangle";

            //baseRectangle.SetFillColor(Color.Blue);

            baseRectangle.ShowShapeOutline(false);

            baseRectangle.SetFillColor(Color.White);
            //this.Shape = baseRectangle;

            // Page.AddToPageShapeDict(Shape);

            //VisioInterop.SetYLocation(Shape, yBase);

            //lineLegendLayer.AddShape(baseRectangle, 0);

            baseRectangle.BringToFront();

            //VisioInterop.LockShape(baseRectangle);

            foreach (GraphicShape shape in shapeList)
            {
                shape.BringToFront();
            }


            shapeList.Insert(0, baseRectangle);

            Shape = Page.GroupShapes(Window, Page, shapeList);

            Shape.Page = Page;
            Shape.Window = Window;

            Shape.Data1 = "[Legend]AreaLegend";
            Shape.Data2 = "AreaLegend";

            lineLegendLayer.AddShape(Shape, 1);

            Shape.AddToLayerSet(lineLegendLayer);

            Page.AddToPageShapeDict(Shape);

            Window?.DeselectAll();

            return Location.Y - locY;
        }
        public void SetUp(double xbase, double ybase, LegendLocation legendLocation, double Size, bool visible)
        {
            LegendShowLocation = legendLocation;

            Visible = visible;

            //if (Size == CurrentSize)
            //{
            //    // No need to redraw //

            //    VisioInterop.SetShapeLocation(Shape, xBase + Shape.Width * 0.5, yBase + Page.PageHeight - Shape.Height * 0.5 - 0.125);
            //}

            CurrentSize = Size;

            Init();
        }


#endregion

        #region Event Subscriptions

        /// <summary>
        /// Regenerate the legend if an item is inserted
        /// </summary>
        /// <param name="lineFinishBase">The line finish base that is inserted. Ignored.</param>
        /// <param name="position">The position at which it is inserted. Ignored</param>
        private void LineFinishBaseList_ItemInserted(LineFinishBase lineFinishBase, int position)
        {
            Init();
        }

        /// <summary>
        /// Regenerate the legend if an item is added
        /// </summary>
        /// <param name="item">The line finish that is added. Ignored.</param>
        private void LineFinishBaseList_ItemAdded(LineFinishBase item)
        {
            Init();
        }

        /// <summary>
        /// Regenerate the legend if an item is removed.
        /// </summary>
        /// <param name="guid">The guid of the line finish that was removed. Ignored.</param>
        /// <param name="position">The position from which it was removed. Ignored.</param>
        private void LineFinishBaseList_ItemRemoved(string guid, int position)
        {
            Init();
        }

        /// <summary>
        /// Regenerate the legend if items have been swapped.
        /// </summary>
        /// <param name="position1">The first position swapped. Ignored.</param>
        /// <param name="position2">The second position swapped. Ignored.</param>
        private void LineFinishBaseList_ItemsSwapped(int position1, int position2)
        {
           
            Init();
        }


        private void LineFinishBase_FilteredChanged(LineFinishBase finishBase, bool filtered)
        {
            Init();
        }

        #endregion

        /// <summary>
        /// Shows the legend on the left side of the canvas
        /// </summary>
        public void ShowLeft()
        {
            ShowLegend(true);

            if (LegendShowLocation == LegendLocation.Left)
            {
                return;
            }

            LegendShowLocation = LegendLocation.Left;

            SystemGlobals.LineLegendLocation = new Coordinate(0, SystemGlobals.LineLegendLocation.Y);
            
            //Delete();

            Init();

        }

        /// <summary>
        /// Shows the legend on the right side of the canvas.
        /// </summary>
        public void ShowRight()
        {
            ShowLegend(true);

            if (LegendShowLocation == LegendLocation.Right)
            {
                return;
            }

            LegendShowLocation = LegendLocation.Right;

            SystemGlobals.LineLegendLocation =
                new Coordinate(Page.PageWidth - areaSizeX, SystemGlobals.LineLegendLocation.Y);
           // Delete();

            Init();

        }
        double x, y;

        public void Setlocation(double Size)
        {
            if (Shape is null)
            {
                return;
            }

            if (Size == CurrentSize)
            {
                // No need to redraw //

                //VisioInterop.SetShapeLocation();
            }

            CurrentSize = Size;

       
            double sx = Shape.Width;

            Delete();

            if (LegendShowLocation == LegendLocation.Right)
            {
                Init();

                return;
            }

            Init();
        }

        public void LocateToClick(double x, double y)
        {
            LegendShowLocation = LegendLocation.OnClick;

            SystemGlobals.LineLegendLocation = new Coordinate(x, y);

            Init();
        }

        public void ShowLegend(bool showLegend)
        {
            lineLegendLayer.SetLayerVisibility(showLegend);

            Visible = showLegend;

        }


        public void Delete()
        {
            foreach (LineLegendElement  legendElement in this.legendElements)
            {
                legendElement.LineFinishBase.FilteredChanged -= LineFinishBase_FilteredChanged;

                // string data1 = legendElement.Shape.Data1;

                legendElement.Delete();
            }

            foreach (GraphicShape shape in shapeList)
            {

                string data1 = shape.Data1;

                lineLegendLayer.RemoveShapeFromLayer(shape, 0);

                Page.RemoveFromPageShapeDict(shape);

                shape.Delete();
            }

            legendElementDict.Clear();

            lineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            lineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            lineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            lineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;

            if (Shape is null)
            {
                return;
            }


            Page.RemoveFromPageShapeDict(Shape);

            lineLegendLayer.RemoveShapeFromLayer(Shape, 0);

            Shape.Delete();

            Shape = null;
        }
    }
}
