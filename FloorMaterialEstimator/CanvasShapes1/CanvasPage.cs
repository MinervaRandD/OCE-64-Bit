//-------------------------------------------------------------------------------//
// <copyright file="CanvasPage.cs"                                               //
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

namespace CanvasShapes
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;


    using Visio = Microsoft.Office.Interop.Visio;

    using MaterialsLayout;
    using Graphics;
    using Utilities;
 
    using System.Diagnostics;

    using FinishesLib;
    using SettingsLib;
    using CanvasLib.Legend;
    using CanvasLib.Markers_and_Guides;
    using Globals;
    using Geometry;
    using CanvasLib.SeamingTool;
    using System.Windows.Forms;
    using TracerLib;
    using CanvasLib.Area_and_Seam_Views;

    /// <summary>
    /// The CanvasPage encapsulates the code mirror of what is on the canvas
    /// </summary>
    public class CanvasPage: GraphicsPage
    {
        public static CanvasPage CurrentPage { get; set; }

        public List<CanvasDirectedLine> LineHistoryList = new List<CanvasDirectedLine>();

        public Dictionary<string, CanvasLayoutArea> LayoutAreaForSubdivisionDict = new Dictionary<string, CanvasLayoutArea>();

        //-------------------------------------- Elements related to Layout Areas ------------------------------- //

        // Eventually, all elements should be indexed by guid. This is an intermediate step.

        public Dictionary<string, CanvasLayoutArea> LayoutAreaDict { get; set; } = new Dictionary<string, CanvasLayoutArea>();

        public void AddLayoutArea(CanvasLayoutArea canvasLayoutArea)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { canvasLayoutArea });
#endif

            try
            {
                string guid = canvasLayoutArea.Guid;

                if (LayoutAreaDict.ContainsKey(guid))
                {
                    Tracer.TraceGen.TraceError(
                        "Attempt to add a layout area in CanvasPage:AddLayoutArea with the same guid as an existing layout area."
                        , 1, true);

                    return;
                }

                LayoutAreaDict[guid] = canvasLayoutArea;

                AddToPageShapeDict(canvasLayoutArea);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasPage:AddLayoutArea throws an exception", ex, 1, true);
            }
        }

        public void RemoveLayoutArea(CanvasLayoutArea canvasLayoutArea)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { canvasLayoutArea });
#endif

            RemoveLayoutArea(canvasLayoutArea.Guid);
        }

        public void RemoveLayoutArea(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif

            try
            {
                if (!LayoutAreaDict.ContainsKey(guid))
                {
                    Tracer.TraceGen.TraceError(
                        "Attempt to remove a layout area in CanvasPage:RemoveLayoutArea which does not exist in the GuidLayoutAreaDict."
                        , 1, true);


                    //RemoveFromPageShapeDict(guid); // This is a safety feature. May have to remove from shape dict anyway.

                    return;
                }

                LayoutAreaDict.Remove(guid);

                //RemoveFromPageShapeDict(guid);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("CanvasPage:RemoveLayoutArea throws an exception", ex, 1, true);
            }
        }

        public CanvasLayoutArea GetCanvasLayoutAreaByGuid(string guid)
        {
            return LayoutAreaDict.ContainsKey(guid) ? LayoutAreaDict[guid] : null;
        }

        public int LayoutAreaCount => LayoutAreaDict.Count;

        public void ClearLayoutAreaDict() => LayoutAreaDict.Clear();

        public IEnumerable<CanvasLayoutArea> LayoutAreas => LayoutAreaDict.Values;

        public List<CanvasLayoutArea> TemporaryLayoutAreaList = new List<CanvasLayoutArea>();


        //------------------------------------------------------------------------------------------------------------ //


        //-------------------------------------- Elements related to Directed Lines ------------------------------- //

        // Eventually, all elements should be indexed by guid. This is an intermediate step.

        /// <summary>
        /// DirectedLineDict contains a table of all shapes of type 'line' that exist in visio on the current page
        /// </summary>

        public Dictionary<string, CanvasDirectedLine> DirectedLineDict = new Dictionary<string, CanvasDirectedLine>();

        public void AddToDirectedLineDict(CanvasDirectedLine directedLine)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { directedLine });
#endif

            if (directedLine.Guid.StartsWith("Sheet"))
            {
                Tracer.TraceGen.TraceError("Attempt to add a line with an invalid guid in the DirectedLineDict in CanvasPage:AddDirectedLine", 1, true);
                return;
            }

            if (DirectedLineDict.ContainsKey(directedLine.Guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a line with a guid already in the DirectedLineDict in CanvasPage:AddDirectedLine", 1, true);
                return;
            }

            DirectedLineDict.Add(directedLine.Guid, directedLine);
            
            // AddToPageShapeDict(directedLine);
        }

        public void RemoveFromDirectedLineDict(CanvasDirectedLine directedLine)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { directedLine });
#endif

            RemoveFromDirecteLineDict(directedLine.Guid);
        }

        public void RemoveFromDirecteLineDict(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif

            if (guid.StartsWith("Sheet"))
           {
                Tracer.TraceGen.TraceError("Attempt to add a line with an invalid guid in the DirectedLineDict in CanvasPage:RemoveDirectedLine", 1, true);
                return;
            }

            if (!DirectedLineDict.ContainsKey(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to remove a line with a guid not in the DirectedLineDict in CanvasPage:RemoveDirectedLine", 1, true);
                return;
            }

            DirectedLineDict.Remove(guid);

            RemoveFromPageShapeDict(guid);
        }

        public bool DirectedLineDictContains(string guid) => DirectedLineDict.ContainsKey(guid);

        public bool DirectedLineDictContains(CanvasDirectedLine line) => DirectedLineDictContains(line.Guid);

        public CanvasDirectedLine GetDirectedLine(string guid) => DirectedLineDict.ContainsKey(guid) ? DirectedLineDict[guid] : null;

        public IEnumerable<CanvasDirectedLine> DirectedLines => DirectedLineDict.Values;

        public CanvasDirectedPolyline BuildingPolyline { get; set; }  = null;

        public int DirectedLineCount => DirectedLineDict.Count;

        public void ClearDirectedLineDict()
        {
            DirectedLineDict.Clear();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------//

        internal Dictionary<string, CanvasSeam> GetSeamDictionary(AreaFinishManager selectedAreaFinishManager)
        {
            Dictionary<string, CanvasSeam> rtrnDict = new Dictionary<string, CanvasSeam>();

            foreach (CanvasLayoutArea canvasLayoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                foreach (CanvasSeam canvasSeam in canvasLayoutArea.CanvasSeamList)
                {
                    rtrnDict.Add(canvasSeam.GraphicsSeam.Guid, canvasSeam);
                }
            }

            return rtrnDict;
        }



        internal List<CanvasSeam> GetSeamList(AreaFinishManager selectedAreaFinishManager)
        {
            List<CanvasSeam> rtrnList = new List<CanvasSeam>();

            foreach (CanvasLayoutArea canvasLayoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                foreach (CanvasSeam canvasSeam in canvasLayoutArea.CanvasSeamList)
                {
                    rtrnList.Add(canvasSeam);
                }
            }

            return rtrnList;
        }

        public Dictionary<string, CanvasDirectedLine> SelectedLineDict { get; set; } = new Dictionary<string, CanvasDirectedLine>();

        public IEnumerable<CanvasDirectedLine> SelectedLines => SelectedLineDict.Values;


        public List<CanvasDirectedLine> LineDesignStateSelectedLines()
        {
            List<CanvasDirectedLine> rtrnList = new List<CanvasDirectedLine>();

            foreach (LineFinishManager lineFinishManager in FinishManagerGlobals.LineFinishManagerList)
            {
                foreach (CanvasDirectedLine canvasDirectedLine in lineFinishManager.CanvasDirectedLines)
                {
                    if (canvasDirectedLine.LineDesignStateEditModeSelected)
                    {
                        rtrnList.Add(canvasDirectedLine);
                    }
                }
            }

            return rtrnList;
        }

        //------------------------------------------------------------------------------------------------------------ //
       

        // public Dictionary<string, CanvasLayoutArea> AreaDesignStateSelectedAreaDict { get; set; } = new Dictionary<string, CanvasLayoutArea>();

        public List<CanvasLayoutArea> AreaDesignStateSelectedAreas()
        {
            List<CanvasLayoutArea> rtrnList = new List<CanvasLayoutArea>();

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    if (canvasLayoutArea.AreaDesignStateEditModeSelected)
                    {
                        rtrnList.Add(canvasLayoutArea);
                    }
                }
            }

            return rtrnList;
        }
        public List<CanvasLayoutArea> AreaDesignStateSelectedForCopyAreas()
        {
            List<CanvasLayoutArea> rtrnList = new List<CanvasLayoutArea>();

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    if (canvasLayoutArea.AreaDesignStateSelectedForCopy)
                    {
                        rtrnList.Add(canvasLayoutArea);
                    }
                }
            }

            return rtrnList;
        }
      
        //------------------------------------------------------------------------------------------------------------ //


        //-------------------------------------- Elements related to Borders ---------------------------- //

        #region Borders

        #endregion

        //------------------------------------------------------------------------------------------------------------ //

        //public CanvasPage() { }

        public CanvasPage(GraphicsWindow window, GraphicsPage page): base(window, page.VisioPage)
        {
            Window = window;

            Page = page;
        }

        public double RemnantSeamWidthInFeet()
        {
            string strFeet = SystemState.TxbRemnantWidthFeet.Text.Trim();
            string strInch = SystemState.TxbRemnantWidthInches.Text.Trim();

            if (string.IsNullOrEmpty(strFeet) && string.IsNullOrEmpty(strInch))
            {
                MessageBox.Show("A valid seam width must be provided.");
                return 0;
            }

            int iFeet = 0;
            int iInch = 0;

            if (!string.IsNullOrEmpty(strFeet))
            {
                if (!Utilities.IsAllDigits(strFeet))
                {
                    MessageBox.Show("A whole number must be provided for the seam width feet.");
                    return 0;
                }

                iFeet = int.Parse(strFeet);
            }

            if (!string.IsNullOrEmpty(strInch))
            {
                if (!Utilities.IsAllDigits(strInch))
                {
                    MessageBox.Show("A whole number must be provided for the seam width inches.");
                    return 0;
                }

                iInch = int.Parse(strInch);

                if (iInch > 11)
                {
                    MessageBox.Show("A whole number between 0 and 11 must be provided for the seam width inches.");
                    return 0;
                }
            }

            return ((double)iFeet) + ((double)iInch) / 12.0;

        }

        public void SetupMarkerAndGuides(Coordinate coord)
        {
            bool deselectRequired = false;

            if (GlobalSettings.ShowMarker)
            {
                this.RemoveMarker();
                this.PlaceMarker(coord, GlobalSettings.MarkerWidth);

                deselectRequired = true;
            }

            if (GlobalSettings.ShowGuides)
            {
                this.RemoveGuides();
                this.PlaceGuides(coord);

                deselectRequired = true;
            }

            if (deselectRequired)
            {
                Window?.DeselectAll();
            }
            //#if DEBUG
            //            BaseForm.UpdateDebugForm();
            //#endif
        }

        public void RemoveMarkerAndGuides()
        {
            CurrentPage.RemoveMarker();
            CurrentPage.RemoveAllGuides();
        }


        public CanvasLayoutArea SelectedLayoutArea = null;

        public Marker CurrentMarker { get; set; }

        public void PlaceMarker(Coordinate coord, double width)
        {
            PlaceMarker(coord.X, coord.Y, width);
        }

        public void PlaceMarker(double x, double y, double width)
        {
            CurrentMarker = new Marker(this.Window, this, x, y, width);

            CurrentMarker.Draw(this.VisioPage);
        }

        public void RemoveMarker()
        {
            if (CurrentMarker == null)
            {
                return;
            }

            CurrentMarker.Delete();

            CurrentMarker = null;
        }

        public List<Guide> CurrentGuideList { get; set; } = new List<Guide>();

        public Guide CurrentGuide { get; set; }
        
        public Shape Drawing { get; internal set; }

        public void PlaceGuides(Coordinate coord)
        {
            PlaceGuides(coord.X, coord.Y);
        }

        internal void PlaceGuides(double x, double y)
        {
            CurrentGuide = new Guide(x, y);

            CurrentGuide.Draw(this);

            CurrentGuideList.Add(CurrentGuide);

            //Window?.DeselectAll();
        }

        internal void RemoveGuides()
        {
            if (CurrentGuideList is null)
            {
                return;
            }

            if (CurrentGuideList.Count <= 0)
            {
                return;
            }

            if (GlobalSettings.KeepAllGuidesOnCanvas)
            {
                return;
            }

            if (GlobalSettings.KeepInitialGuideOnCanvas)
            {
                int count = CurrentGuideList.Count;

                if (count <= 1)
                {
                    return;
                }

                for (int i = 1; i < count; i++)
                {
                    CurrentGuideList[i].Delete();
                    CurrentGuideList[i] = null;
                }

                CurrentGuideList.RemoveRange(1, count - 1);

                return;
            }

            RemoveAllGuides();
        }


        internal void RemoveAllGuides()
        {
            if (CurrentGuideList is null)
            {
                return;
            }

            if (CurrentGuideList.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < CurrentGuideList.Count;i++)
          
            {
                CurrentGuideList[i].Delete();
                CurrentGuideList[i] = null;
            }

            CurrentGuide = null;

            CurrentGuideList.Clear();
        }

        public LineModeStartMarker LineModeStartMarker { get; set; } = null;

        public void PlaceLineModeStartMarker(Coordinate coord, double radius, Color lineColor, StartMarkerType startMarkerType)
        {
            PlaceLineModeStartMarker(coord.X, coord.Y, radius, lineColor, startMarkerType);
        }

        public void PlaceLineModeStartMarker(double x, double y, double radius, Color lineColor, StartMarkerType startMarkerType)
        {
            this.LineModeStartMarker = new LineModeStartMarker(x, y, radius, lineColor, startMarkerType);

            this.LineModeStartMarker.Draw(this);

            this.Window?.DeselectAll();
        }

        public void RemoveLineModeStartMarker()
        {
            if (Utilities.IsNotNull(LineModeStartMarker))
            {
                LineModeStartMarker.Delete();
                LineModeStartMarker = null;
            }
        }

        public void UpdateAreaDesignStateAreaSelectionStatus(CanvasLayoutArea canvasLayoutArea)
        {
            if (canvasLayoutArea.AreaDesignStateEditModeSelected)
            {
                canvasLayoutArea.AreaDesignStateEditModeSelected = false;

                canvasLayoutArea.SetFillColor(canvasLayoutArea.AreaFinishBase.Color);
                canvasLayoutArea.SetFillOpacity(canvasLayoutArea.AreaFinishBase.Opacity);
            }

            else
            {
                canvasLayoutArea.AreaDesignStateEditModeSelected = true;
                
                Color selectedAreaColor = GlobalSettings.SelectedAreaColor;

                canvasLayoutArea.SetFillColor(selectedAreaColor);

                double opacity = GlobalSettings.SelectedAreaOpacity;

                canvasLayoutArea.SetFillOpacity(opacity);

                if (canvasLayoutArea.Shape.Data1 == "PastedLayoutArea")
                {
                    Window.DeselectAll();
                    VisioInterop.SelectShape(Window, canvasLayoutArea.Shape);
                }
            }
        }

        public void SetAreaDesignStateAreaSelectionStatus(CanvasLayoutArea canvasLayoutArea, bool selected)
        {
            if (!selected)
            {
                canvasLayoutArea.AreaDesignStateEditModeSelected = false;

                canvasLayoutArea.SetFillColor(canvasLayoutArea.AreaFinishBase.Color);
                canvasLayoutArea.SetFillOpacity(canvasLayoutArea.AreaFinishBase.Opacity);

                //Window.DeselectAll();
            }

            else
            {
                canvasLayoutArea.AreaDesignStateEditModeSelected = true;

                Color selectedAreaColor = GlobalSettings.SelectedAreaColor;

                canvasLayoutArea.SetFillColor(selectedAreaColor);

                double opacity = GlobalSettings.SelectedAreaOpacity;

                canvasLayoutArea.SetFillOpacity(opacity);
            }
        }

        //-------------------------------------- Elements related to Seam Design State ------------------------------- //

        /// <summary>
        /// Note that it would be more efficient simply to have a table of layout areas that are currently selected, rather than cycle through all
        /// the layout areas and return a list of those whose selection flag has been set. But since the project is currently volatile, this method
        /// is much less likely to create errors. In future versions a table is probably a better approach.
        /// </summary>
        /// <returns>Returns a list of all layout areas that are currently selected in seam mode</returns>
        public List<CanvasLayoutArea> SeamDesignStateSelectedAreas()
        {
            List<CanvasLayoutArea> rtrnList = new List<CanvasLayoutArea>();

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                    {
                        rtrnList.Add(canvasLayoutArea);
                    }
                }
            }

            return rtrnList;

        }

        public List<CanvasLayoutArea> SeamDesignStateSelectedAreas(string areaFinishGuid)
        {
            List<CanvasLayoutArea> rtrnList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.AreaFinishManagerList[areaFinishGuid].CanvasLayoutAreas)
            {
                if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    rtrnList.Add(canvasLayoutArea);
                }
            }

            return rtrnList;

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaFinishGuid"></param>
        /// <returns>Returns the set of used seam area indicies for a specific area finish as defined by the guid</returns>
        public HashSet<int> UsedSeamAreaIndices(string areaFinishGuid, LayoutAreaType layoutAreaType)
        {
            HashSet<int> rtrnSet = new HashSet<int>();

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.AreaFinishManagerList[areaFinishGuid].CanvasLayoutAreas)
            {
                if (canvasLayoutArea.SeamAreaIndex == 0)
                {
                    continue;
                }

                if (canvasLayoutArea.LayoutAreaType != layoutAreaType)
                {
                    continue;
                }

                rtrnSet.Add(canvasLayoutArea.SeamAreaIndex);
            }

            return rtrnSet;
        }
        
        public SortedList<int, double> SeamModeAreaData(string guid)
        {
            SortedList<int, double> rtrnList = new SortedList<int, double>();

            AreaFinishManager areaFinishManager = FinishManagerGlobals.AreaFinishManagerList[guid];

            double fixedWidthArea = 0;

            bool fixedWidthExists = false;

            foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
            {
                if (canvasLayoutArea.IsBorderArea)
                {
                    fixedWidthExists = true;

                    fixedWidthArea += canvasLayoutArea.NetAreaInSqrInches();
                }

                // else if (canvasLayoutArea.SeamDesignStateSelectionModeSelected && canvasLayoutArea.IsSeamed())
                else if (canvasLayoutArea.SeamDesignStateSelectionModeSelected && canvasLayoutArea.HasSeamsOrRollouts)
                {
                    if (canvasLayoutArea.SeamAreaIndex == 0)
                    {
                        // This is an unfortunate patch. The seam index should never be zero if selected.

                        continue;
                    }

                    if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.Normal)
                    {
                        // To avoid double counting we only tally when we are at a leaf node for layout area hierarchy

                        if (canvasLayoutArea.OffspringAreas.Count <= 0)
                        {
                            rtrnList.Add(canvasLayoutArea.SeamAreaIndex, canvasLayoutArea.NetAreaInSqrInches());
                        }
                    }
                }
            }

            if (fixedWidthExists)
            {
                rtrnList.Add(-1, fixedWidthArea);
            }

            return rtrnList;
        }

        public SortedList<int, double> SeamModeSeamData(string guid)
        {
            SortedList<int, double> rtrnList = new SortedList<int, double>();

            AreaFinishManager areaFinishManager = FinishManagerGlobals.AreaFinishManagerList[guid];

            foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
            {
                if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    if (canvasLayoutArea.CanvasSeamList is null)
                    {
                        continue;
                    }

                    if (canvasLayoutArea.CanvasSeamList.Count <= 0)
                    {
                        continue;
                    }

                    if (canvasLayoutArea.LayoutAreaType != LayoutAreaType.ZeroArea && canvasLayoutArea.LayoutAreaType != LayoutAreaType.OversGenerator)
                    {
                        rtrnList.Add(canvasLayoutArea.SeamAreaIndex, canvasLayoutArea.CanvasSeamList.Sum(s => ((CanvasSeam)s).GraphicsSeam.Length()));
                    }
                }
            }

            return rtrnList;
        }

        public void UpdateSeamTotals()
        {
            Dictionary<string, double> seamLengthDict = new Dictionary<string, double>();

            foreach (SeamFinishBase seamFinishBase in FinishGlobals.SeamFinishBaseList)
            {
                seamLengthDict.Add(seamFinishBase.Guid, 0);
            }

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                if (areaFinishManager.SeamFinishBase is null)
                {
                    continue;
                }

                string guid = areaFinishManager.SeamFinishBase.Guid;

                // Since seams can now fall on area boundaries (i.e. if they have been subdivided by the seaming tool
                // seam lines may overlap, so we need to be sure to eliminate the contribution of doubly seamed boundaries.

                HashSet<string> canvasSeamSet = new HashSet<string>();

                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    foreach (CanvasSeam canvasSeam in canvasLayoutArea.CanvasSeamList)
                    {
                        // All this is to make sure that seam lines falling on top of each other are not double counted

                        Coordinate coord1 = canvasSeam.GraphicsSeam.Seam.Coord1;
                        Coordinate coord2 = canvasSeam.GraphicsSeam.Seam.Coord2;

                        if (coord1 > coord2)
                        {
                            Utilities.Swap(ref coord1, ref coord2);
                        }

                        string key = coord1.ToString() + '_' + coord2.ToString();

                        if (canvasSeamSet.Contains(key))
                        {
                            continue;
                        }

                        canvasSeamSet.Add(key);

                        seamLengthDict[guid] += canvasSeam.GraphicsSeam.Length();
                    }
                }
            }

            foreach (SeamFinishBase seamFinishBase in FinishGlobals.SeamFinishBaseList)
            {
                // To avoid triggering events, only update if values have changed.

                if (seamFinishBase.LengthInInches != seamLengthDict[seamFinishBase.Guid])
                {
                    seamFinishBase.LengthInInches = seamLengthDict[seamFinishBase.Guid];
                }
            }
        }

        /// <summary>
        /// Updates the seam selection status of a layout area. If the layout area is selected, then it is de-selected, otherwise it is selected
        /// </summary>
        /// <param name="canvasLayoutArea">The canvas layout area in question</param>
        /// <param name="selectedAreaFinishGuid">The guid of the corresponding area finish</param>
        /// <param name="x">current click location x (to add seam tag)</param>
        /// <param name="y">current click location y (to add seam tag)</param>
        public void UpdateSeamDesignStateAreaSelectionStatus(CanvasLayoutArea canvasLayoutArea, string selectedAreaFinishGuid, double x, double y)
        {
            HashSet<int> usedSeamAreaIndices = UsedSeamAreaIndices(selectedAreaFinishGuid, canvasLayoutArea.LayoutAreaType);

            string guid = canvasLayoutArea.Guid;

            if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
            {
                canvasLayoutArea.SeamDesignStateSelectionModeSelected = false;

                if (!(canvasLayoutArea.SeamIndexTag is null))
                {
                    canvasLayoutArea.AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea.SeamIndexTag, 1);

                    canvasLayoutArea.SeamIndexTag.Delete();
                }

                if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.Remnant)
                {
                    string seamAreaIndex = "R" + (-canvasLayoutArea.SeamAreaIndex).ToString();

                    if (((UCRemnantsView)StaticGlobals.RemnantsView).ContainsRemnantKey(seamAreaIndex))
                    {
                        ((UCRemnantsView)StaticGlobals.RemnantsView).RemoveRemnantArea(seamAreaIndex);
                    }
                }

                canvasLayoutArea.RemoveSeamIndexTag();

                canvasLayoutArea.RemoveSeamsAndRollouts();

                canvasLayoutArea.RemoveEmbeddedOvers();
            }

            else
            {
                canvasLayoutArea.SeamDesignStateSelectionModeSelected = true;

                canvasLayoutArea.RemoveEmbeddedOvers();

                if (!(canvasLayoutArea.SeamIndexTag is null))
                {
                    canvasLayoutArea.SeamIndexTag.Draw();
                }

                else
                {
                    int seamIndex = GetSeamAreaIndex(usedSeamAreaIndices);

                    Coordinate seamIndexTagLocation = canvasLayoutArea.VoronoiLabelLocation();

                    double locX = seamIndexTagLocation.X;
                    double locY = seamIndexTagLocation.Y;

                    canvasLayoutArea.SeamIndexTag = new CanvasSeamTag(Window, (GraphicsPage) this, locX, locY, seamIndex, canvasLayoutArea.LayoutAreaType);

                    canvasLayoutArea.SeamIndexTag.Draw();

                    canvasLayoutArea.AreaFinishManager.AreaFinishLayers.SeamDesignStateLayer.AddShapeToLayer(canvasLayoutArea.SeamIndexTag, 1);

                    if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.Remnant)
                    {
                        canvasLayoutArea.AreaFinishManager.RemnantSeamDesignStateLayer.AddShapeToLayer(canvasLayoutArea.SeamIndexTag.Shape, 1);
                    }
                }
            }

            foreach (CanvasLayoutArea layoutArea in this.LayoutAreas)
            {
                layoutArea.SetLineGraphics(SystemState.DesignState, layoutArea.SeamDesignStateSelectionModeSelected && layoutArea.AreaFinishManager.Selected, AreaShapeBuildStatus.Completed);
            }
        }

        //public void SetupSeamDesignStateAreaSelectionStatus(CanvasLayoutArea canvasLayoutArea)
        //{
        //    string selectedAreaFinishGuid = canvasLayoutArea.UCAreaFinish.Guid;

        //    canvasLayoutArea.SeamDesignStateSelectionModeSelected = true;
            
        //}

        //public void ClearSeamDesignStateTables()
        //{
        //    //SeamDesignStateSelectedAreaDict.Clear();
        //    //UsedSeamAreaIndices.Clear();
        //    //SeamModeAreaData.Clear();
        //    //SeamModeSeamData.Clear();
        //}

        public int GetSeamAreaIndex(HashSet<int> usedSeamAreaIndices)
        {
            int i = 1;

            while (usedSeamAreaIndices.Contains(i)) { i += 1; }

            return i;
        }
        //------------------------------------------------------------------------------------------------------------ //

        public void UpdateSelectedAreaColors(int colorIndex)
        {
            Color color;
            double opacity;

            if (colorIndex == 0)
            {
                color = GlobalSettings.AreaEditSettingColor1;
                opacity = Math.Min(1.0, Math.Max(0.0, 1.0 - GlobalSettings.AreaEditSettingColor1Transparency));
            }

            else if (colorIndex == 1)
            {
                color = GlobalSettings.AreaEditSettingColor2;
                opacity = Math.Min(1.0, Math.Max(0.0, 1.0 - GlobalSettings.AreaEditSettingColor2Transparency));
            }

            else
            {
                color = Color.FromArgb(3, 252, 239);
                opacity = 1.0;
            }

            foreach (CanvasLayoutArea canvasLayoutArea in this.AreaDesignStateSelectedAreas())
            {
                canvasLayoutArea.SetFillColor(color);

                canvasLayoutArea.SetFillOpacity(opacity);
            }

            foreach (CanvasLayoutArea canvasLayoutArea in this.SeamDesignStateSelectedAreas())
            {
                canvasLayoutArea.SetFillColor(color);

                canvasLayoutArea.SetFillOpacity(opacity);
            }

            if (LayoutAreaForSubdivisionDict.ContainsKey(FinishGlobals.SelectedAreaFinish.Guid))
            {
                CanvasLayoutArea layoutAreaForSubdivision = LayoutAreaForSubdivisionDict[FinishGlobals.SelectedAreaFinish.Guid];

                layoutAreaForSubdivision.SetFillColor(color);

                layoutAreaForSubdivision.SetFillOpacity(opacity);
            }
        }

        public void ProcessShowUnseamedAreas(bool show)
        {
            Color color;
            double opacity;

            color = GlobalSettings.LineEditSettingColor2;
            opacity = Math.Min(1.0, Math.Max(0.0, 1.0 - GlobalSettings.AreaEditSettingColor2Transparency));

            if (show)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in LayoutAreas)
                {
                    if (canvasLayoutArea.IsSeamed())
                    {
                        continue;
                    }

                    canvasLayoutArea.SetFillColor(color);

                    canvasLayoutArea.SetFillOpacity(opacity);
                }
            }

            else
            {
                foreach (CanvasLayoutArea canvasLayoutArea in LayoutAreas)
                {
                    if (canvasLayoutArea.IsSeamed())
                    {
                        continue;
                    }

                    canvasLayoutArea.SetFillColor(canvasLayoutArea.AreaFinishManager.Color);

                    canvasLayoutArea.SetFillOpacity(canvasLayoutArea.AreaFinishManager.Opacity);
                }
            }
        }

        public void ProcessShowZeroAreas(bool show)
        {
            Color color;
            double opacity;

            color = GlobalSettings.LineEditSettingColor2;
            opacity = Math.Min(1.0, Math.Max(0.0, 1.0 - GlobalSettings.AreaEditSettingColor2Transparency));

            if (show)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in LayoutAreas)
                {
                    if (canvasLayoutArea.LayoutAreaType != LayoutAreaType.ZeroArea)
                    {
                        continue;
                    }

                    canvasLayoutArea.SetFillColor(color);

                    canvasLayoutArea.SetFillOpacity(opacity);
                }
            }

            else
            {
                foreach (CanvasLayoutArea canvasLayoutArea in LayoutAreas)
                {
                    if (canvasLayoutArea.LayoutAreaType != LayoutAreaType.ZeroArea)
                    {
                        continue;
                    }

                    canvasLayoutArea.SetFillColor(canvasLayoutArea.AreaFinishManager.Color);

                    canvasLayoutArea.SetFillOpacity(canvasLayoutArea.AreaFinishManager.Opacity);
                }
            }
        }

        public void UpdateSelectedLineColors(int colorIndex)
        {
            Color color;
            int intensity;

            if (colorIndex == 0)
            {
                color = GlobalSettings.LineEditSettingColor1;
                intensity = GlobalSettings.LineEditSettingColor1Intensity;
            }

            else if (colorIndex == 1)
            {
                color = GlobalSettings.LineEditSettingColor2;
                intensity = GlobalSettings.LineEditSettingColor2Intensity;
            }

            else
            {
                color = Color.FromArgb(3, 252, 239);
                intensity = 100;
            }

            foreach (CanvasDirectedLine canvasDirectedLine in this.SelectedLineDict.Values)
            {
                color = DrawingUtils.modifyColorByIntensity(color, intensity);

                canvasDirectedLine.SetBaseLineColor(color);
            }
        }

        /// <summary>
        /// Gets the 'best' layout area corresponding to the point (x,y) on the screen. Best is defined as the
        /// area that contains (x,y) which is smallest.
        /// </summary>
        /// <param name="x">The x position of the area to search for</param>
        /// <param name="y">The y position of the area to search for</param>
        /// <returns>Returns a selected canvas layout area if one is found, otherwise null</returns>
        internal CanvasLayoutArea GetSelectedLayoutArea(
            double x
            , double y
            , MaterialsType materialType = MaterialsType.Any
            , LayoutAreaType layoutAreaType = LayoutAreaType.Unknown)
        {
            //Debug.Assert(CanvasManager.DesignState == DesignState.Area); // This should only be used in area design mode as currently designed.

            Visio.Selection selection = VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            CanvasLayoutArea minLayoutArea = null;

            double minAreaArea = double.MaxValue;

            foreach (Visio.Shape visioShape in selection)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }
                
                if (layoutArea.IsSubdivided())
                {
                    continue;
                }

                // Currently, in area mode, select only if layout area is not an offspring to another layout area.

                //if (!(layoutArea.ParentArea is null))
                //{
                //    continue;
                //}

                if (materialType != MaterialsType.Any)
                {
                    if (layoutArea.AreaFinishBase.MaterialsType != materialType)
                    {
                        continue;
                    }
                }

                if (layoutAreaType != LayoutAreaType.Unknown)
                {
                    if ((layoutArea.LayoutAreaType & layoutAreaType) == 0)
                    {
                        continue;
                    }
                }

                double nxtAreaArea = VisioInterop.GetShapeArea(layoutArea.Shape);
                
                if (nxtAreaArea < 0)
                {
                    MessageBox.Show("Invalid layout area size returned from visio");
                    continue;
                }

                if (nxtAreaArea < minAreaArea)
                {
                    minAreaArea = nxtAreaArea;
                    minLayoutArea = layoutArea;
                }
            }

            return minLayoutArea;
        }
        
        internal CanvasLayoutArea GetSelectedToplevelLayoutArea(
         double x
         , double y
         , MaterialsType materialType = MaterialsType.Any
         , LayoutAreaType layoutAreaType = LayoutAreaType.Unknown)
        {
            //Debug.Assert(CanvasManager.DesignState == DesignState.Area); // This should only be used in area design mode as currently designed.

            Visio.Selection selection = VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            CanvasLayoutArea minLayoutArea = null;

            double minAreaArea = double.MaxValue;

            foreach (Visio.Shape visioShape in selection)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }

                if (Utilities.IsNotNull(layoutArea.ParentArea))
                {
                    continue;
                }

                // Currently, in area mode, select only if layout area is not an offspring to another layout area.

                //if (!(layoutArea.ParentArea is null))
                //{
                //    continue;
                //}

                if (materialType != MaterialsType.Any)
                {
                    if (layoutArea.AreaFinishBase.MaterialsType != materialType)
                    {
                        continue;
                    }
                }

                if (layoutAreaType != LayoutAreaType.Unknown)
                {
                    if ((layoutArea.LayoutAreaType & layoutAreaType) == 0)
                    {
                        continue;
                    }
                }

                double nxtAreaArea = VisioInterop.GetShapeArea(layoutArea.Shape);

                if (nxtAreaArea < 0)
                {
                    MessageBox.Show("Invalid layout area size returned from visio");
                    continue;
                }

                if (nxtAreaArea < minAreaArea)
                {
                    minAreaArea = nxtAreaArea;
                    minLayoutArea = layoutArea;
                }
            }

            return minLayoutArea;
        }
        internal CanvasLayoutArea GetContainingLayoutArea(Coordinate coord1, Coordinate coord2, Coordinate coord3, Coordinate coord4, bool allowSubdividedAreas = false)
        {
            Debug.Assert(SystemState.DesignState == DesignState.Area); // This should only be used in area design mode as currently designed.

            Visio.Selection selection1 = VisioPage.SpatialSearch[coord1.X, coord1.Y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection1 is null)
            {
                return null;
            }

            if (selection1.Count <= 0)
            {
                return null;
            }

            Visio.Selection selection2 = VisioPage.SpatialSearch[coord2.X, coord2.Y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection2 is null)
            {
                return null;
            }

            if (selection2.Count <= 0)
            {
                return null;
            }

            Visio.Selection selection3 = VisioPage.SpatialSearch[coord3.X, coord3.Y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection3 is null)
            {
                return null;
            }

            if (selection3.Count <= 0)
            {
                return null;
            }

            Visio.Selection selection4 = VisioPage.SpatialSearch[coord4.X, coord4.Y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection4 is null)
            {
                return null;
            }

            if (selection4.Count <= 0)
            {
                return null;
            }

            Dictionary<string, CanvasLayoutArea> layoutAreaDict1 = new Dictionary<string, CanvasLayoutArea>();
            Dictionary<string, CanvasLayoutArea> layoutAreaDict2 = new Dictionary<string, CanvasLayoutArea>();
            Dictionary<string, CanvasLayoutArea> layoutAreaDict3 = new Dictionary<string, CanvasLayoutArea>();
            Dictionary<string, CanvasLayoutArea> layoutAreaDict4 = new Dictionary<string, CanvasLayoutArea>();

            foreach (Visio.Shape visioShape in selection1)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }

                layoutAreaDict1.Add(layoutArea.Guid, layoutArea);
            }

            foreach (Visio.Shape visioShape in selection2)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }

                if (!layoutAreaDict1.ContainsKey(layoutArea.Guid))
                {
                    continue;
                }

                layoutAreaDict2.Add(layoutArea.Guid, layoutArea);
            }

            foreach (Visio.Shape visioShape in selection3)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }

                if (!layoutAreaDict2.ContainsKey(layoutArea.Guid))
                {
                    continue;
                }

                layoutAreaDict3.Add(layoutArea.Guid, layoutArea);
            }

            foreach (Visio.Shape visioShape in selection4)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }

                layoutAreaDict4.Add(layoutArea.Guid, layoutArea);
            }

            if (layoutAreaDict4.Count <= 0)
            {
                return null;
            }

            DirectedPolygon polygon = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord3, coord4 });

            CanvasLayoutArea minLayoutArea = null;
            double minAreaArea = double.MaxValue;

            foreach (CanvasLayoutArea layoutArea in layoutAreaDict4.Values)
            {
                // Currently, in area mode, select only if layout area is not an offspring to another layout area.

                if (!(layoutArea.ParentArea is null))
                {
                    continue;
                }

                if (!allowSubdividedAreas)
                {
                    if (layoutArea.IsSubdivided())
                    {
                        continue;
                    }
                }

                if (!layoutArea.Contains(polygon))
                {
                    continue;
                }

                double nxtAreaArea = layoutArea.NetAreaInSqrInches();

                if (nxtAreaArea < minAreaArea)
                {
                    minAreaArea = nxtAreaArea;
                    minLayoutArea = layoutArea;
                }
            }

            return minLayoutArea;
        }

        internal CanvasLayoutArea GetContainingLayoutArea(double x, double y)
        {
            return GetContainingLayoutArea(new Coordinate(x, y));
        }

        internal CanvasLayoutArea GetContainingLayoutArea(Coordinate coord)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[coord.X, coord.Y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }


            foreach (Visio.Shape visioShape in selection)
            {
                CanvasLayoutArea layoutArea = this.GetCanvasLayoutAreaByGuid(visioShape.Data3);

                if (layoutArea is null)
                {
                    continue;
                }

                if (layoutArea.IsSubdivided())
                {
                    continue;
                }

                return layoutArea;
            }

            return null;
        }

        internal CanvasLayoutArea GetContainingLayoutAreaMinusInternalArea(IEnumerable<CanvasLayoutArea> candidateLayoutAreas, Coordinate coord)
        {
            CanvasLayoutArea rtrnCanvasLayoutArea = null;

            double minArea = double.PositiveInfinity;

            foreach (CanvasLayoutArea candidateLayoutArea in candidateLayoutAreas)
            {
                if (candidateLayoutArea.ExternalArea is null)
                {
                    continue;
                }

                if (candidateLayoutArea.ExternalArea.Contains(coord))
                {
                    double area = candidateLayoutArea.ExternalArea.AreaInSqrInches();

                    if (area < minArea)
                    {
                        minArea = area;

                        rtrnCanvasLayoutArea = candidateLayoutArea;
                    }
                }
            }

            return rtrnCanvasLayoutArea;
        }
        //public Shape GetLegend(double x, double y)
        //{
        //    Visio.Selection selection = VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

        //    if (selection is null)
        //    {
        //        return null;
        //    }

        //    if (selection.Count <= 0)
        //    {
        //        return null;
        //    }

        //    foreach (Visio.Shape visioShape in selection)
        //    {
        //        if (visioShape.Data2 == "Legend1")
        //        {
        //            Visio.Selection selection1
        //                = VisioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSub, AreaModeLegend.Shape.VisioShape);

        //            selection1.Select(AreaModeLegend.Shape.VisioShape, 2);


                  
        //            return AreaModeLegend.Shape;
        //        }
        //    }

        //    return null;
        //}

        //internal bool MouseOverSeamingTool(double x, double y, CanvasLib.SeamingTool.SeamingTool seamingTool)
        //{
        //    if (seamingTool is null)
        //    {
        //        return false;
        //    }

        //    if (seamingTool.Shape is null)
        //    {
        //        return false;
        //    }

        //    Visio.Selection selection = VisioPage.SpatialSearch[x, y, 
        //        (short)(Visio.VisSpatialRelationCodes.visSpatialTouching | Visio.VisSpatialRelationCodes.visSpatialContainedIn | Visio.VisSpatialRelationCodes.visSpatialContain), .1, 0];

        //    foreach (Visio.Shape visioShape in selection)
        //    {
        //        string guid = visioShape.Data3;

        //        if (seamingTool.Shape.Guid == visioShape.Data3)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;

        //}



        internal List<CanvasLayoutArea> GetSelectedAreaShapeList(
            double x
            , double y
            , bool ignoreBorderAreas = false)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { x, y, ignoreBorderAreas });

            List<CanvasLayoutArea> returnList = new List<CanvasLayoutArea>();

            try
            {
                Debug.Assert(SystemState.DesignState == DesignState.Seam); // This should only be used in seam design mode as currently designed.

                List<string> guidList = VisioInterop.SpatialSearchGuidList(Window, this, x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0);

                //Visio.Selection selection = VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

                if (guidList is null)
                {
                    return returnList;
                }

                if (guidList.Count <= 0)
                {
                    return returnList;
                }

                foreach (string guid in guidList)
                {
                   
                    CanvasLayoutArea canvasLayoutArea = this.GetCanvasLayoutAreaByGuid(guid);

                    if (canvasLayoutArea is null)
                    {
                        continue;
                    }

                    // Only return leaf nodes.
                    if (canvasLayoutArea.IsBorderArea && ignoreBorderAreas)
                    {
                        continue;
                    }

                    if (!canvasLayoutArea.IsSubdivided())
                    {
                        returnList.Add(canvasLayoutArea);
                    }
                }

                return returnList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in CanvasPage:GetSelectedAreaShapeList", ex, 1, true);

                return returnList;
            }
        }

        internal CanvasDirectedLine GetSelectedLineShape(double x, double y)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[
                x, y,
                (short)(Visio.VisSpatialRelationCodes.visSpatialContain | Visio.VisSpatialRelationCodes.visSpatialOverlap |
                Visio.VisSpatialRelationCodes.visSpatialTouching),
                .1, 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                string guid = visioShape.Data3;

                if (this.DirectedLineDictContains(guid))
                {
                    return this.GetDirectedLine(guid);
                }
            }

            return null;
        }

        internal List<CanvasDirectedLine> GetSelectedLineShapeList(Coordinate coordinate)
        {
            return GetSelectedLineShapeList(coordinate.X, coordinate.Y);
        }

        internal List<CanvasDirectedLine> GetSelectedLineShapeList(double x, double y)
        {
            List<CanvasDirectedLine> lineList = new List<CanvasDirectedLine>();

            Visio.Selection selection = VisioPage.SpatialSearch[
                x, y,
                (short)(Visio.VisSpatialRelationCodes.visSpatialContain | Visio.VisSpatialRelationCodes.visSpatialOverlap |
                Visio.VisSpatialRelationCodes.visSpatialTouching),
                .1, 0];

            if (selection is null)
            {
                return lineList;
            }

            if (selection.Count <= 0)
            {
                return lineList;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                string guid = visioShape.Data3;

                if (this.DirectedLineDictContains(guid))
                {
                    lineList.Add(this.GetDirectedLine(guid));
                }
            }

            return lineList;
        }

        internal IGraphicsShape GetSelectedCutOverOrUnderIndexShape(double x, double y)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[
               x, y,
               (short)(
                Visio.VisSpatialRelationCodes.visSpatialContain |
                Visio.VisSpatialRelationCodes.visSpatialOverlap |
                Visio.VisSpatialRelationCodes.visSpatialContainedIn |
                Visio.VisSpatialRelationCodes.visSpatialTouching),
               .2, 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                if (visioShape.Data2 == "CutIndex" || visioShape.Data2 == "OverIndex" || visioShape.Data2 == "UndrIndex")
                {
                    if (this.PageShapeDictContains(visioShape.Data3))
                    {
                        return ((IGraphicsShape)PageShapeDictGetShape(visioShape.Data3));
                    }
                }
            }

            return null;
        }

        internal List<CanvasSeam> GetSelectedSeamList(Coordinate coordinate, Dictionary<string, CanvasSeam> seamDict)
        {
            return GetSelectedSeamList(coordinate.X, coordinate.Y, seamDict);
        }

        internal List<CanvasSeam> GetSelectedSeamList(double x, double y, Dictionary<string, CanvasSeam> seamDict)
        {
            List<CanvasSeam> seamList = new List<CanvasSeam>();

            Visio.Selection selection = VisioPage.SpatialSearch[
                x, y,
                (short)(Visio.VisSpatialRelationCodes.visSpatialContain | Visio.VisSpatialRelationCodes.visSpatialOverlap |
                Visio.VisSpatialRelationCodes.visSpatialTouching),
                .1, 0];

            if (selection is null)
            {
                return seamList;
            }

            if (selection.Count <= 0)
            {
                return seamList;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                string guid = visioShape.Data3;

                if (seamDict.ContainsKey(guid))
                {
                    seamList.Add(seamDict[guid]);
                }
            }

            return seamList;
        }


        internal List<CanvasSeam> GetSelectedSeams()
        {
            List<CanvasSeam> rtrnList = new List<CanvasSeam>();

            foreach (CanvasLayoutArea canvasLayoutArea in LayoutAreas)
            {
                foreach (CanvasSeam canvasSeam in canvasLayoutArea.CanvasSeamList)
                {
                    if (canvasSeam.GraphicsSeam.Selected)
                    {
                        rtrnList.Add(canvasSeam);
                    }
                }
            }

            return rtrnList;
        }


        public void UpdateLineDesignStateAreaSelectionStatus(CanvasDirectedLine canvasDirectedLine)
        {
            if (canvasDirectedLine.LineDesignStateEditModeSelected)
            {
                canvasDirectedLine.LineDesignStateEditModeSelected = false;

                canvasDirectedLine.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);

                SelectedLineDict.Remove(canvasDirectedLine.Guid);
            }

            else
            {
                canvasDirectedLine.LineDesignStateEditModeSelected = true;

                Color selectedLineColor = GlobalSettings.SelectedLineColor;

                canvasDirectedLine.SetBaseLineColor(selectedLineColor);

                SelectedLineDict.Add(canvasDirectedLine.Guid, canvasDirectedLine);
            }
        }

        public void SetLineDesignStateAreaSelectionStatus(CanvasDirectedLine canvasDirectedLine, bool status)
        {
            if (status == true)
            {
                if (!canvasDirectedLine.LineDesignStateEditModeSelected)
                {
                    canvasDirectedLine.LineDesignStateEditModeSelected = true;

                    Color selectedLineColor = GlobalSettings.SelectedLineColor;

                    canvasDirectedLine.SetBaseLineColor(selectedLineColor);

                    SelectedLineDict.Add(canvasDirectedLine.Guid, canvasDirectedLine);
                }
            }

            else
            {
                if (canvasDirectedLine.LineDesignStateEditModeSelected)
                {
                    canvasDirectedLine.LineDesignStateEditModeSelected = false;

                    canvasDirectedLine.SetLineGraphics(DesignState.Line, false, AreaShapeBuildStatus.Completed);

                    SelectedLineDict.Remove(canvasDirectedLine.Guid);
                }
            }
        }


        internal List<IGraphicsShape> GetShapeList()
        {
            Dictionary<string, IGraphicsShape> shapeDict = new Dictionary<string, IGraphicsShape>();

            //foreach (GraphicsLayer graphicsLayer in this.GraphicsLayers)
            //{
            //    foreach (Shape shape in graphicsLayer.Shapes)
            //    {
            //        if (!shapeDict.ContainsKey(shape.Guid))
            //        {
            //            shapeDict.Add(shape.Guid, shape);
            //        }
            //    }
            //}

            foreach (IGraphicsShape iShape in this.PageShapeDict.Values)
            {
                if (!shapeDict.ContainsKey(iShape.Guid))
                {
                    shapeDict.Add(iShape.Guid, iShape);
                }
            }

            return shapeDict.Values.ToList();
        }

        public override double DrawingScaleInInches
        {
            get
            {
                return base.DrawingScaleInInches;
            }

            set
            {
                if (base.DrawingScaleInInches == value)
                {
                    return;
                }

                base.DrawingScaleInInches = value;

                SystemState.BtnSetCustomScale.Image = StaticGlobals.CustomScaleLineColoredImage;
                SystemState.BtnTapeMeasure.Enabled = true;
                ScaleHasBeenSet = true;
            }
        }

        public bool ScaleHasBeenSet
        {
            get
            {
                return SystemState.ScaleHasBeenSet;
            }

            set
            {
                SystemState.ScaleHasBeenSet = value;

                ScaleStateChangeHandler(SystemState.ScaleHasBeenSet);

                if (value == true)
                {
                    SystemState.TssDrawoutLength.Text = string.Empty;
                }

                else
                {
                    SystemState.TssDrawoutLength.Text = "<No Scale Set>";
                }
            }
        }

        public void ScaleStateChangeHandler(bool scaleHasBeenSet)
        {
            if (scaleHasBeenSet)
            {
                SystemState.TssHighlightedArea.Text = string.Empty;
            }
            else
            {
                SystemState.TssHighlightedArea.Text = "<No Scale Set>";
            }
        }

        internal void SetDefaultScale(double drawingScaleInInches)
        {
            base.DrawingScaleInInches = drawingScaleInInches;
            SystemState.BtnSetCustomScale.Image = StaticGlobals.CustomScaleLineUncoloredImage;
            SystemState.BtnTapeMeasure.Enabled = false;

        }


        internal bool SeamedAreasExist()
        {
            foreach (CanvasLayoutArea layoutArea in LayoutAreas)
            {
                if (layoutArea.IsSeamed())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Create the cuts, overs and unders layers
        /// </summary>
        public void CreateCutsOversUndrsLayers()
        {
            //CutsLayer  = new GraphicsLayer(CanvasManager.Window, this, "[CanvasPage]CutsLayer" , GraphicsLayerType.Dynamic);
            //CutsIndexLayer = new GraphicsLayer(CanvasManager.Window, this, "[CanvasPage]CutsIndexLayer", GraphicsLayerType.Dynamic);
            //OversLayer = new GraphicsLayer(CanvasManager.Window, this, "[CanvasPage]OversLayer", GraphicsLayerType.Dynamic);
            //UndrsLayer = new GraphicsLayer(CanvasManager.Window, this, "[CanvasPage]UndrsLayer", GraphicsLayerType.Dynamic);
            //RmntEmbdLayer = new GraphicsLayer(CanvasManager.Window, this, "[CanvasPage]EmbdssLayer", GraphicsLayerType.Dynamic);
            //EmbdOverLayer = new GraphicsLayer(CanvasManager.Window, this, "[CanvasPage]EmbdOverLayer", GraphicsLayerType.Dynamic);

            //CutsLayer.SetLayerVisibility(false);
           //CutsIndexLayer.SetLayerVisibility(false);
           // OversLayer.SetLayerVisibility(false);
           // UndrsLayer.SetLayerVisibility(false);
            //RmntEmbdLayer.SetLayerVisibility(false);
            //EmbdOverLayer.SetLayerVisibility(false);
        }

    }
}
