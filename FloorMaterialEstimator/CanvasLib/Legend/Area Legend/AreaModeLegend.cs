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
    /// to changes in the area finish base list, rather than updating elements on a case by case basis.
    /// The latter would be more elegant and better programming, but as a first implementation, this is 
    /// done to save implementation time.
    /// </summary>
    /// 
    public class AreaModeLegend
    {
        private GraphicsPage Page { get; } = null;

        private GraphicsWindow Window { get; } = null;

        public GraphicShape Shape
        {
            get;
            set;
        }

        private AreaFinishBaseList areaFinishBaseList { get; } = null;

        private CounterList counterList { get; } = null;

        public Coordinate Location
        {
            get => SystemGlobals.AreaLegendLocation;
            set => SystemGlobals.AreaLegendLocation = value;
        }

        double areaSizeX;
        double areaSizeY;

        private GraphicShape baseRectangle;

        private GraphicsLayerBase areaLegendLayer => Page.AreaLegendLayer;

        public bool Visible
        {
            get;
            set;
        }

        public string Notes => SystemGlobals.AreaLegendNotes;

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


        // This dictionary contains the area base list elements. We need to rebuild this because of the
        // order of event generation the lists in the area base list may not be up to date.

        //private Dictionary<string, AreaFinishBase> areaFinishBaseDict = new Dictionary<string, AreaFinishBase>();

        // This dictionary mirrors what is actually in the area base list.
        private Dictionary<string, AreaLegendFinishElement> legendFinishElementDict = new Dictionary<string, AreaLegendFinishElement>();

        private IEnumerable<AreaLegendFinishElement> legendFinishElements => legendFinishElementDict.Values;

        private IEnumerable<AreaLegendFinishElement> unfilteredLegendFinishElements => legendFinishElements.Where(l => l.Filtered == false);

        private int unfilteredFinishElementCount => legendFinishElements.Count(l => l.Filtered == false);

        private Dictionary<string, AreaLegendCounterElement> legendCounterElementDict = new Dictionary<string, AreaLegendCounterElement>();

        private IEnumerable<AreaLegendCounterElement> legendCounterElements => legendCounterElementDict.Values;

        private IEnumerable<AreaLegendCounterElement> unfilteredLegendCounterElements => legendCounterElements.Where(l => l.Filtered == false);

        private int unfilteredCounterElementCount => legendCounterElements.Count(l => l.Filtered == false);

        
        //private int unfilteredElementCount => legendFinishElements.Count(l => l.Filtered == false);

        // This dictionary represents what is currently actually on the pallet.
        //private Dictionary<string, LegendElement> palletLegendElementDict = new Dictionary<string, LegendElement>();

        #region Constructors and Initializers

        /// <summary>
        /// Construct with Window and Page as with all graphics objects.
        /// The area finish base list is the list of finishes.
        /// </summary>
        /// <param name="window">Current graphics Window</param>
        /// <param name="page">Current graphics Page</param>
        /// <param name="areaFinishBaseList">The area finish base list to display</param>
        public AreaModeLegend(
            
            GraphicsWindow window
            ,GraphicsPage page
            ,AreaFinishBaseList areaFinishBaseList
            ,CounterList counterList
            )
        {
            //this.areaLegendNavigationForm = areaLegendNavigationForm;

            this.Page = page;

            this.Window = window;

            this.areaFinishBaseList = areaFinishBaseList;
            
            this.counterList = counterList;

            // Set up subscriptions to events, so as the base list changes, so does the legend.

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            LegendShowLocation = LegendLocation.NotSet;

            Shape = null;

            //ShowLegend(false);

            //Init();
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

                bool areaLegendLayerLocked = areaLegendLayer.IsLocked();

                if (areaLegendLayerLocked)
                {
                    areaLegendLayer.UnLock();
                }

                /*mdd*/
                //Console.WriteLine("Area Mode Legend remove shape from layer");
                areaLegendLayer.RemoveShapeFromLayer(Shape);
                Shape.Delete();
                Shape = null;

                if (areaLegendLayerLocked)
                {
                    areaLegendLayer.Lock();
                }
               
            }

            foreach (AreaLegendFinishElement areaLegendFinishElement in legendFinishElementDict.Values)
            {
                areaLegendFinishElement.Delete();
            }

            foreach (AreaLegendCounterElement areaLegendCounterElement in legendCounterElementDict.Values)
            {
                areaLegendCounterElement.Delete();
            }

            legendFinishElementDict.Clear();
            legendCounterElementDict.Clear();

            // scale is 1 when size is maxed out

            double scale = SystemGlobals.AreaLegendScale;


            // Create Elements

            if (SystemGlobals.ShowAreaLegendFinishes)
            {

                foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
                {
                    AreaLegendFinishElement legendElement = new AreaLegendFinishElement(Window, Page, this, areaFinishBase);

                    legendFinishElementDict.Add(legendElement.Guid,
                        legendElement); // Contains all elements for all defined areas

                }
            }

            if (SystemGlobals.ShowAreaLegendCounters)
            {
                foreach (Counter counter in counterList.Counters)
                {
                    AreaLegendCounterElement legendElement = new AreaLegendCounterElement(
                        Window
                        , Page
                        , this
                        , counter);

                  
                    legendCounterElementDict.Add(legendElement.Guid, legendElement); // Contains all elements for all defined areas
                }
            }

            if (resize)
            {
                // Calculate the total count

                double elementCount = legendCounterElementDict.Count + legendFinishElementDict.Count +
                                      (SystemGlobals.AreaLegendNotes != null ? 1 : 0);


                double sizeY = scale * Page.PageHeight / ((double)elementCount * 2.0);

                areaSizeY = sizeY;
                areaSizeX = areaSizeY * 2.0;

                if (LegendShowLocation == LegendLocation.Right &&
                    SystemGlobals.AreaLegendLocation.X + areaSizeX > Page.PageWidth)
                {
                    SystemGlobals.AreaLegendLocation =
                        new Coordinate(Page.PageWidth - areaSizeX, SystemGlobals.AreaLegendLocation.Y);
                }

                double textSizeY = areaSizeY * 0.5;

                double initialScale = SystemGlobals.AreaLegendScale;
                ;
                double initialSize = CreateLegendForSizingOnly(initialScale);

                if (initialScale == 0)
                {
                    return;
                }

                double targetSize = Page.PageHeight * SystemGlobals.AreaLegendScale;

                areaSizeY *= targetSize / initialSize;
                areaSizeX = 2 * areaSizeY;
            }

            if (string.IsNullOrEmpty(SystemGlobals.AreaLegendNotes) &&
                unfilteredFinishElementCount == 0 &&
                unfilteredCounterElementCount == 0)
            {
                return;
            }

            Draw();

            List<GraphicShape> legendShapes = new List<GraphicShape>() { baseRectangle };

            foreach (AreaLegendFinishElement legendElement in unfilteredLegendFinishElements)
            {
                legendShapes.Add(legendElement.Shape);
            }

        }

        private const double interTypeHeight = -0.4;

        private double interItemSpace = .10;

        /* Provides an estimate of the actual height (in inches) of the legend, given the scale */

        private double CreateLegendForSizingOnly(double scale)
        {
            double textSizeY = areaSizeY * 1;

            bool includeFinishes = unfilteredFinishElementCount > 0 && SystemGlobals.ShowAreaLegendFinishes;
            bool includeCounters = unfilteredCounterElementCount > 0 && SystemGlobals.ShowAreaLegendCounters;
            bool includeNotes = !string.IsNullOrEmpty(SystemGlobals.AreaLegendNotes) && SystemGlobals.ShowAreaLegendNotes;

            double locY = Page.PageHeight;
            interItemSpace = areaSizeY * 0.25;

            if (!includeFinishes && !includeCounters && !includeNotes)
            {
                return 0;
            }

            if (includeFinishes)
            {
                locY -= (areaSizeY + textSizeY + interItemSpace) * (double) unfilteredFinishElementCount;
            }


            if (includeCounters)
            {
                if (includeFinishes)
                {
                    locY -= interItemSpace * 2.0;
                }

                locY -= (1.5 * areaSizeY + interItemSpace) * (double)unfilteredCounterElementCount;
            }

            if (includeNotes)
            {
                if (includeFinishes || includeCounters)
                {
                    locY -= interItemSpace * 2.0;
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

            double locX = SystemGlobals.AreaLegendLocation.X;
            double locY = SystemGlobals.AreaLegendLocation.Y;

            double textSizeY = areaSizeY * 1;

            int fontSize = (int) (textSizeY * 20);

            var dpi = VisioInterop.dotsPerInch();

            double dpiX = (double)dpi.dpiX;

            bool includeFinishes = unfilteredFinishElementCount > 0 && SystemGlobals.ShowAreaLegendFinishes;
            bool includeCounters = unfilteredCounterElementCount > 0 && SystemGlobals.ShowAreaLegendCounters;
            bool includeNotes = !string.IsNullOrEmpty(SystemGlobals.AreaLegendNotes) && SystemGlobals.ShowAreaLegendNotes;

            if (!includeFinishes && !includeCounters && !includeNotes)
            {
                return 0;
            }

            int posnOnPalette = 0;

            //locY -= interItemSpace;
            interItemSpace = areaSizeY * 0.25;

            if (includeFinishes)
            {
                
                foreach (AreaLegendFinishElement legendElement in unfilteredLegendFinishElements)
                {
                   
                    legendElement.Draw(locX, locY, areaSizeX, textSizeY, areaSizeY, (int)Math.Round(25.0 * areaSizeY),posnOnPalette);

                    shapeList.Add(legendElement.Shape);

                    legendElement.Shape.BringToFront();

                    posnOnPalette++;

                    locY -= legendElement.SizeY + interItemSpace;
                }
            }

            
            if (includeCounters)
            {
                if (includeFinishes)
                {
                    locY -= interItemSpace * 2.0;
                }

                foreach (AreaLegendCounterElement legendElement in unfilteredLegendCounterElements)
                {
                    legendElement.Draw(locX, locY, areaSizeX,  areaSizeY);

                    shapeList.Add(legendElement.Shape);

                    posnOnPalette++;


                    locY -= (1.5 * areaSizeY + interItemSpace);
                }
            }

            if (includeNotes)
            {
                if (includeFinishes || includeCounters)
                {
                    locY -= interItemSpace * 2.0;
                }

                GraphicShape textShape =
                    Page.CreateFixedWidthTextBox(locX, locY, areaSizeX * 2.0, SystemGlobals.AreaLegendNotes, (double) fontSize * 1.25);
                
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


            baseRectangle.ShowShapeOutline(false);

            baseRectangle.SetFillColor(Color.White);
           

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

            areaLegendLayer.AddShape(Shape, 1);

            Shape.AddToLayerSet(areaLegendLayer);

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
        /// <param name="areaFinishBase">The area finish base that is inserted. Ignored.</param>
        /// <param name="position">The position at which it is inserted. Ignored</param>
        private void AreaFinishBaseList_ItemInserted(AreaFinishBase areaFinishBase, int position)
        {
            Init();
        }

        /// <summary>
        /// Regenerate the legend if an item is added
        /// </summary>
        /// <param name="item">The area finish that is added. Ignored.</param>
        private void AreaFinishBaseList_ItemAdded(AreaFinishBase item)
        {
            Init();
        }

        /// <summary>
        /// Regenerate the legend if an item is removed.
        /// </summary>
        /// <param name="guid">The guid of the area finish that was removed. Ignored.</param>
        /// <param name="position">The position from which it was removed. Ignored.</param>
        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            Init();
        }

        /// <summary>
        /// Regenerate the legend if items have been swapped.
        /// </summary>
        /// <param name="position1">The first position swapped. Ignored.</param>
        /// <param name="position2">The second position swapped. Ignored.</param>
        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
           
            Init();
        }


        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            Init();
        }

        private void Counter_CounterFilteredChanged(Counter counter, bool filtered)
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

            SystemGlobals.AreaLegendLocation = new Coordinate(0, SystemGlobals.AreaLegendLocation.Y);
            
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

            SystemGlobals.AreaLegendLocation =
                new Coordinate(Page.PageWidth - areaSizeX, SystemGlobals.AreaLegendLocation.Y);
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

            SystemGlobals.AreaLegendLocation = new Coordinate(x, y);

            Init();
        }

        public void ShowLegend(bool showLegend)
        {
            areaLegendLayer.SetLayerVisibility(showLegend);

            Visible = showLegend;

        }


        public void Delete()
        {
            foreach (AreaLegendFinishElement legendElement in this.legendFinishElements)
            {
                legendElement.AreaFinishBase.FilteredChanged -= AreaFinishBase_FilteredChanged;

                // string data1 = legendElement.Shape.Data1;

                legendElement.Delete();
            }

            foreach (AreaLegendCounterElement legendElement in legendCounterElements)
            {
                legendElement.Counter.CounterFilteredChanged -= Counter_CounterFilteredChanged;

                legendElement.Delete();
            }

            foreach (GraphicShape shape in shapeList)
            {

                string data1 = shape.Data1;

                areaLegendLayer.RemoveShapeFromLayer(shape, 0);

                Page.RemoveFromPageShapeDict(shape);

                shape.VisioShape = null;

                shape.Delete();
            }

            shapeList.Clear();
            legendFinishElementDict.Clear();
            legendCounterElementDict.Clear();

            areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;

            if (Shape is null)
            {
                return;
            }


            Page.RemoveFromPageShapeDict(Shape);

            areaLegendLayer.RemoveShapeFromLayer(Shape, 0);

            Shape.Delete();

            Shape = null;
        }
    }
}
