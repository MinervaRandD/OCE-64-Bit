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
    using Graphics;
    using FinishesLib;
    using Utilities;

    using Geometry;
    using System;
    using CanvasLib.Counters;

    /// <summary>
    /// This class instantiates the legends that can appear on the canvas at the users request.
    /// 
    /// Note that the approach here is pretty 'brute force' -- reconstructing the legend in response
    /// to changes in the area finish base list, rather than updating elements on a case by case basis.
    /// The latter would be more elegant and better programming, but as a first implementation, this is 
    /// done to save implementation time.
    /// </summary>
    /// 
    public class Legend
    {
        private GraphicsPage Page { get; set; }

        private GraphicsWindow Window { get; set; }

        private AreaFinishBaseList areaFinishBaseList;

        private CounterList counterList;

        private double yBase;
        private double xBase;

        public Shape Shape
        {
            get;
            set;
        }

        private Shape baseRectangle;

        public GraphicsLayerBase LegendLayer => Page.AreaLegendLayer;

        public bool Visible { get; private set; }

        public LegendLocation LegendShowLocation
        { 
            get;
            set;
        }  = LegendLocation.None;


        // This dictionary contains the area base list elements. We need to rebuild this because of the
        // order of event generation the lists in the area base list may not be up to date.

        private Dictionary<string, AreaFinishBase> areaFinishBaseDict = new Dictionary<string, AreaFinishBase>();

        // This dictionary mirrors what is actually in the area base list.
        private Dictionary<string, AreaLegendPaletteElement> baseLegendElementDict = new Dictionary<string, AreaLegendPaletteElement>();

        private IEnumerable<AreaLegendPaletteElement> baseLegendElements => baseLegendElementDict.Values;

        // This dictionary represents what is currently actually on the pallet.
        private Dictionary<string, AreaLegendPaletteElement> palletLegendElementDict = new Dictionary<string, AreaLegendPaletteElement>();

        #region Constructors and Initializers

        /// <summary>
        /// Construct with window and page as with all graphics objects.
        /// The area finish base list is the list of finishes.
        /// </summary>
        /// <param name="window">Current graphics window</param>
        /// <param name="page">Current graphics page</param>
        /// <param name="areaFinishBaseList">The area finish base list to display</param>
        public Legend(
            GraphicsWindow window
            ,GraphicsPage page
            ,AreaFinishBaseList areaFinishBaseList
            ,CounterList counterList
            )
        {
            this.areaFinishBaseList = areaFinishBaseList;
            this.counterList = counterList;
            this.Page = page;
            this.Window = window;

            // Set up subscriptions to events, so as the base list changes, so does the legend.

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            LegendShowLocation = LegendLocation.None;

            ShowLegend(false);

            Init(0, Page.PageHeight - 0.125);
        }

        /// <summary>
        /// Initialize the legend and draw it at the base location (xBase, yBase). This routine actually draws the legend
        /// on the Visio canvas.
        /// </summary>
        /// <param name="xBase">x coord at which to show the legend</param>
        /// <param name="yBase">y coord at which to show the legend</param>
        public void Init(double xBase, double yBase, double size = 0)
        {
            // Remove any previous legend elements, if necessary.

            foreach (AreaLegendPaletteElement legendElement in baseLegendElementDict.Values)
            {
                legendElement.Delete();
            }

            baseLegendElementDict.Clear();
            palletLegendElementDict.Clear();

            this.xBase = xBase;
            this.yBase = yBase;

            int posnOnPalette = 0;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                AreaLegendPaletteElement legendElement = new AreaLegendPaletteElement(Window, Page, areaFinishBase, 0.67 + (0.67 * size), 0.2, 0.33 + (33 * size), posnOnPalette);
                //LegendElement legendElement = new LegendElement(window, page, areaFinishBase, 0.67, 0.2, 0.33, posnOnPalette);

                baseLegendElementDict.Add(legendElement.Guid, legendElement);

                legendElement.FilteredChanged += LegendElement_FilteredChanged;

                posnOnPalette++;
            }

            generate();


            Redraw();
        }
        /// <summary>
        /// Generates the legend for the first time.
        /// </summary>
        private void generate()
        {
            areaFinishBaseDict.Clear();

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                areaFinishBaseDict.Add(areaFinishBase.Guid, areaFinishBase);
            }

            // Add elements to our local dictionary of the base legend elements if necessary. This will happen if items
            // have been added or inserted.

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseDict.Values)
            {
                if (!baseLegendElementDict.ContainsKey(areaFinishBase.Guid))
                {
                    AreaLegendPaletteElement legendElement = new AreaLegendPaletteElement(Window, Page, areaFinishBase, 0.67, 0.2, 0.33, 1);
                    baseLegendElementDict.Add(legendElement.Guid, legendElement);
                }
            }

            // Now the legend element dictionary should be consistent with the area finish base list.
            // Remove all elements from the pallet that have been filtered.

            foreach (AreaLegendPaletteElement legendElement in baseLegendElementDict.Values)
            {
                if (legendElement.Filtered)
                {
                    continue;
                }

                palletLegendElementDict.Add(legendElement.Guid, legendElement);
            }

            // Now re-establish the pallet, moving or adding only as necessary.

            int positionOnPalette = 0;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (areaFinishBase.Filtered)
                {
                    continue;
                }

                AreaLegendPaletteElement legendElement = baseLegendElementDict[areaFinishBase.Guid];

                legendElement.PosnOnPalette = positionOnPalette++;
            }

            baseRectangle = Page.DrawRectangle(xBase, yBase, xBase + 0.75, yBase - (palletLegendElementDict.Count - 1) * 0.75 + 0.25);

            baseRectangle.VisioShape.Data1 = "[Legend]BaseRectangle";

            baseRectangle.ShowShapeOutline(false);

            Window?.DeselectAll();

            LegendLayer.AddShapeToLayer(baseRectangle, 0);

            //baseRectangle.LockSelected(1);

            Shape = baseRectangle;

        }

        /// <summary>
        /// Reinitialize the legend. This occurs when a new, blank or existing project is loaded. The reason for the 
        /// re-initialization is that the area finish base list is reloaded and hence we need to reset the legend
        /// to match the new area finish list.
        /// </summary>
        /// <param name="areaFinishBaseList">The new area finish base list</param>
        public void Reinit(AreaFinishBaseList areaFinishBaseList)
        {
            // Remove subscriptions from previous area finish base list.

            areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;

            this.areaFinishBaseList = areaFinishBaseList;
            
            // Add subscriptions to the new area finish base list.

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            if (LegendShowLocation == LegendLocation.Left)
            {
                LegendShowLocation = LegendLocation.Notset;
               
                ShowLeft();
            }

            else if (LegendShowLocation == LegendLocation.Right)
            {
                LegendShowLocation = LegendLocation.Notset;

                ShowRight();
            }

            else
            {
                ShowNone();
            }
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
            regenerate();
        }

        /// <summary>
        /// Regenerate the legend if an item is added
        /// </summary>
        /// <param name="item">The area finish that is added. Ignored.</param>
        private void AreaFinishBaseList_ItemAdded(AreaFinishBase item)
        {
            regenerate();
        }

        /// <summary>
        /// Regenerate the legend if an item is removed.
        /// </summary>
        /// <param name="guid">The guid of the area finish that was removed. Ignored.</param>
        /// <param name="position">The position from which it was removed. Ignored.</param>
        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            regenerate();
        }

        /// <summary>
        /// Regenerate the legend if items have been swapped.
        /// </summary>
        /// <param name="position1">The first position swapped. Ignored.</param>
        /// <param name="position2">The second position swapped. Ignored.</param>
        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            regenerate();
        }

        /// <summary>
        /// Regenerate the legend if the filtering of an item has changed.
        /// </summary>
        /// <param name="legendElement">The legend element corresponding to the area finish for which the filtering has changed.</param>
        /// <param name="filtered">Whether or not the item has been filted. Ignored.</param>
        private void LegendElement_FilteredChanged(AreaLegendPaletteElement legendElement, bool filtered)
        {
            regenerate();
        }

        #endregion

      
        /// <summary>
        /// Regenerates the pallet in reasonably efficient way in response to changes in the state of the area base list.
        /// The objective is to minimize the amount of 'redrawing' in the visio canvas.
        /// 
        /// Note that the primary complication here is the interaction between the actual elements and whether or not they
        /// are filtered. Were it not for filtering, a much simpler approach could be used.
        /// </summary>
        private void regenerate()
        {
            areaFinishBaseDict.Clear();

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                areaFinishBaseDict.Add(areaFinishBase.Guid, areaFinishBase);
            }

            // Add elements to our local dictionary of the base legend elements if necessary. This will happen if items
            // have been added or inserted.

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseDict.Values)
            {
                if (!baseLegendElementDict.ContainsKey(areaFinishBase.Guid))
                {
                    AreaLegendPaletteElement legendElement = new AreaLegendPaletteElement(Window, Page, areaFinishBase, 0.67, 0.2, 0.33, 1);
                    baseLegendElementDict.Add(legendElement.Guid, legendElement);
                }
            }

            // Remove elements to our local dictionary of the base legend elements if necessary. This will happen if items
            // have been removed.

            List<AreaLegendPaletteElement> legendElementList = new List<AreaLegendPaletteElement>(baseLegendElementDict.Values);

            foreach (AreaLegendPaletteElement legendElement in legendElementList)
            {
                if (!areaFinishBaseDict.ContainsKey(legendElement.AreaFinishBase.Guid))
                {
                    baseLegendElementDict.Remove(legendElement.Guid);
                }
            }

            // Now the legend element dictionary should be consistent with the area finish base list.
            // Remove all elements from the pallet that have been filtered.


            foreach (AreaLegendPaletteElement legendElement in baseLegendElementDict.Values)
            {
                if (legendElement.Filtered)
                {
                    if (palletLegendElementDict.ContainsKey(legendElement.Guid))
                    {
                        palletLegendElementDict[legendElement.Guid].Undraw();

                        palletLegendElementDict.Remove(legendElement.Guid);
                    }
                }

                else if (!palletLegendElementDict.ContainsKey(legendElement.Guid))
                {
                    palletLegendElementDict.Add(legendElement.Guid, legendElement);
                }
            }

            // Remove all elements from the pallet that are no longer in the base list.

            List<AreaLegendPaletteElement> palletElementList = new List<AreaLegendPaletteElement>(palletLegendElementDict.Values);

            foreach (AreaLegendPaletteElement legendElement in palletElementList)
            {
                string guid = legendElement.Guid;

                if (!baseLegendElementDict.ContainsKey(guid))
                {
                    AreaLegendPaletteElement palletLegendElement = palletLegendElementDict[guid];

                    palletLegendElement.Undraw();

                    palletLegendElement.Delete();

                    palletLegendElementDict.Remove(guid);

                }
            }

            // Now re-establish the pallet, moving or adding only as necessary.

            int positionOnPalette = 0;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (areaFinishBase.Filtered)
                {
                    continue;
                }

                AreaLegendPaletteElement legendElement = baseLegendElementDict[areaFinishBase.Guid];

                legendElement.PosnOnPalette = positionOnPalette++;
            }

            Redraw();
        }

        /// <summary>
        /// Redraw the legend -- place it on the visio canvas.
        /// </summary>
        private void Redraw()
        {
            Coordinate baseRectangleCoord = baseRectangle != null ? baseRectangle.UpperLeftLocation : Coordinate.NullCoordinate;

            //double baseRectUppr = baseRectangleCoord.Y;
            //double baseRectLeft = baseRectangleCoord.X;

            foreach (AreaLegendPaletteElement legendElement in palletLegendElementDict.Values)
            {
                Coordinate coordinate = legendElement.Shape is null ? Coordinate.NullCoordinate : legendElement.Shape.CenterPoint;
                double currLocnY = coordinate.Y;

                double updtLocnY = (yBase - 0.25) - legendElement.PosnOnPalette * 0.65;

                Coordinate updtLocnCoord = new Coordinate(xBase + 0.375, updtLocnY);

                if (legendElement.Shape is null)
                {
                    legendElement.Draw(updtLocnCoord.X, updtLocnY);

                    LegendLayer.AddShapeToLayer(legendElement.Shape, 0);

                    legendElement.Shape.LockSelected(1);
                }

                if (Math.Abs(currLocnY - updtLocnY) < 0.001)
                {
                    continue;
                }

                legendElement.Shape.BringToFront();

                legendElement.Shape.CenterPoint = updtLocnCoord;
            }

            //double currBaseRectHght1 = baseRectangle.Height;

            // The following two statements resize the base rectangle depending on the number of legend elements.
            // When visio changes the height of an object, it recenters it so the change appears both at the top and the 
            // bottom. The second statement is necessary to move the top of the rectangle back into it's original location.

            if (baseRectangle != null)
            {
                baseRectangle.Height = palletLegendElementDict.Count * 0.65 - 0.125;

                VisioInterop.SetYLocation(baseRectangle, yBase);
            }

            //double currBaseRectHght2 = baseRectangle.Height;
        }

        /// <summary>
        /// Remove the legend from the visio canvas.
        /// </summary>
        private void Undraw()
        {
            foreach (AreaLegendPaletteElement legendElement in baseLegendElementDict.Values)
            {
                legendElement.Undraw();
            }

            baseLegendElementDict.Clear();
            palletLegendElementDict.Clear();

            if (Utilities.IsNotNull(baseRectangle))
            {
                baseRectangle.Delete();
            }
        }

        /// <summary>
        /// Shows the legend on the left side of the canvas
        /// </summary>
        public void ShowLeft()
        {
            if (LegendShowLocation == LegendLocation.Left)
            {
                return;
            }

            LegendShowLocation = LegendLocation.Left;

            //VisioInterop.SetShapeLocation(Shape, 0, Page.PageHeight - 0.125);

            ShowLegend(true);

            Undraw();

            Init(0, Page.PageHeight - 0.125);

        }

        double x, y;

        public void Setlocation(double Size)
        {
            LegendShowLocation = LegendLocation.Notset;

            ShowLegend(true);

            Undraw();

            Init(xBase, yBase, (Size / 1000));
        }
        public void Setlocation(double xbase, double ybase, double Size)
        {

            LegendShowLocation = LegendLocation.Notset;

            ShowLegend(true);

            Undraw();
            if (xbase + ybase == 0)
            {
                xbase = x;
                ybase = y;
            }
            x = xbase;
            y = ybase;
            Init(xbase, ybase, (Size / 1000));
        }

        /// <summary>
        /// Shows the legend on the right side of the canvas.
        /// </summary>
        public void ShowRight()
        {
            if (LegendShowLocation == LegendLocation.Right)
            {
                return;
            }

            LegendShowLocation = LegendLocation.Right;

            //VisioInterop.SetShapeLocation(Shape, Page.PageWidth - .758, Page.PageHeight - 0.125);

            ShowLegend(true);

            Undraw();

            Init(Page.PageWidth - .758, Page.PageHeight - 0.125);

        }

        /// <summary>
        /// Hides the legend from being shown on the canvas.
        /// </summary>
        public void ShowNone()
        {
            if (LegendShowLocation == LegendLocation.None)
            {
                return;
            }

            LegendShowLocation = LegendLocation.None;

            Undraw();

            ShowLegend(false);
        }

        public void ShowLegend(bool showLegend)
        {
            if (Visible != showLegend)
            {
                LegendLayer.SetLayerVisibility(showLegend);

                Visible = showLegend;
            }
        }

        public void Delete()
        {
            // Remove subscriptions from previous area finish base list.

            areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;

            foreach (AreaLegendPaletteElement legendElement in baseLegendElements)
            {
                legendElement.FilteredChanged -= LegendElement_FilteredChanged;

                legendElement.Delete();
            }

        }
    }
}
