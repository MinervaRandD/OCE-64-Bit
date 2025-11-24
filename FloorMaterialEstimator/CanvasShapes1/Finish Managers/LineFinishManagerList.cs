
namespace CanvasShapes
{
    using System;
    using System.Collections.Generic;
    using Graphics;
    using FinishesLib;
    using Globals;
    using System.Diagnostics;

    public class LineFinishManagerList : List<LineFinishManager>, IDisposable
    {
        GraphicsWindow window;

        GraphicsPage page;

        public Dictionary<string, LineFinishManager> LineFinishManagerDict { get; private set; } = new Dictionary<string, LineFinishManager>();

        public IEnumerable<LineFinishManager> LineFinishManagers => (IEnumerable<LineFinishManager>)this;

        private LineFinishBaseList lineFinishBaseList;

        public int SelectedIndex => lineFinishBaseList.SelectedItemIndex;

        public LineFinishManager SelectedLineFinishManager => this[SelectedIndex];


        public LineFinishManagerList(
            GraphicsWindow window
            , GraphicsPage page
            , LineFinishBaseList lineFinishBaseList)
        {
            this.window = window;

            this.page = page;

            foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
            {
                LineFinishManager lineFinishManager = new LineFinishManager(window, page, lineFinishBaseList, lineFinishBase);

                LineFinishManagerDict.Add(lineFinishManager.Guid, lineFinishManager);

                this.Add(lineFinishManager);
            }

            this.lineFinishBaseList = lineFinishBaseList;

            lineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;

            lineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;

            lineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;

            lineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;
        }

        private void SystemState_DesignStateChanged(DesignState prevDesignState, DesignState designState)
        {
            foreach (LineFinishManager lineFinishManager in this)
            {
                lineFinishManager.SetLineState(designState, SystemState.SeamMode, lineFinishManager.Selected);
            }
        }

        //internal void SetDesignStateFormat(DesignState designState, SeamMode seamMode, bool showLines)
        //{
        //    foreach (LineFinishManager lineFinishManager in this)
        //    {
        //        lineFinishManager.SetDesignStateFormat(designState, seamMode, lineFinishManager.Filtered, showLines);
        //    }
        //}

        private void LineFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            LineFinishManager tempItem = this[position1];

            this[position1] = this[position2];
            this[position2] = tempItem;
        }

        private void LineFinishBaseList_ItemAdded(LineFinishBase lineFinishBase)
        {
            LineFinishManager lineFinishManager = new LineFinishManager(window, page, lineFinishBaseList, lineFinishBase);

            LineFinishManagerDict.Add(lineFinishManager.Guid, lineFinishManager);

            this.Add(lineFinishManager);
        }

        private void LineFinishBaseList_ItemInserted(LineFinishBase lineFinishBase, int position)
        {
            LineFinishManager lineFinishManager = new LineFinishManager(window, page, lineFinishBaseList, lineFinishBase);

            this.Insert(position, lineFinishManager);

            this.LineFinishManagerDict.Add(lineFinishBase.Guid, lineFinishManager);

        }

        private void LineFinishBaseList_ItemRemoved(string guid, int position)
        {
            LineFinishManager lineFinishManager = LineFinishManagerDict[guid];
            // Order is important here, since the Delete1 function clears the Guid

            lineFinishManager.AreaDesignStateLayer.Delete();
            lineFinishManager.LineDesignStateLayer.Delete();
            lineFinishManager.SeamDesignStateLayer.Delete();
            lineFinishManager.RemnantSeamDesignStateLayer.Delete();

            LineFinishManagerDict.Remove(guid);

            this.RemoveAt(position);

            lineFinishManager.Dispose();
        }

        //public void UpdateFinishStats()
        //{
        //    foreach (LineFinishManager lineFinishManager in this)
        //    {
        //        lineFinishManager.UpdateFinishStats();
        //    }
        //}

        //public void SetupAllSeamLayers()
        //{
        //    foreach (LineFinishManager lineFinishManager in this)
        //    {
        //        lineFinishManager.SetupAllSeamLayers();
        //    }
        //}

        //internal void MoveCanvasLayoutLineToFinishType(CanvasLayoutLine canvasLayoutLine, LineFinishManager nextFinishManager, bool updateColor = true)
        //{
        //    Debug.Assert(!canvasLayoutLine.IsSubdivided()); // Only leaf lines can be removed

        //    LineFinishManager currFinishManager = canvasLayoutLine.LineFinishManager;

        //    if (nextFinishManager == currFinishManager)
        //    {
        //        return;
        //    }

        //    currFinishManager.RemoveLayoutLineFromFinish(canvasLayoutLine, true);
        //    nextFinishManager.AddNormalLayoutLine(canvasLayoutLine, true, true);

        //    //canvasLayoutLine.DrawCompositeShape(CanvasManager.Window, CanvasManager.Page);

        //    canvasLayoutLine.PolygonInternalLine.VisioShape.Data3 = canvasLayoutLine.Guid;
        //}

        //internal void SetupSeamFilters()
        //{
        //    if (SystemState.DesignState == DesignState.Line)
        //    {
        //        SetupAllSeamLayers();
        //    }

        //    else if (SystemState.DesignState == DesignState.Seam)
        //    {
        //        StaticGlobals.SetupAllSeamStateSeamLayersForSelectedLine();
        //    }
        //}

        internal void MoveLineToSelectedLineType(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return;
            }

            if (!CanvasPage.CurrentPage.DirectedLineDictContains(guid))
            {
                return;
            }

            CanvasDirectedLine canvasDirectedLine = CanvasPage.CurrentPage.GetDirectedLine(guid);

            MoveLineToLineType(canvasDirectedLine, SelectedLineFinishManager);
        }

        internal void MoveLineToSelectedLineType(CanvasDirectedLine canvasDirectedLine)
        {
            if ((object)canvasDirectedLine == null)
            {
                return;
            }

            MoveLineToLineType(canvasDirectedLine, SelectedLineFinishManager);
        }

        internal void MoveLineToLineType(CanvasDirectedLine line, LineFinishManager nextLineFinishManager)
        {
            if ((object)line == null)
            {
                return;
            }

            LineFinishManager currLineFinishManager = line.LineFinishManager;

            if (nextLineFinishManager == currLineFinishManager)
            {
                return;
            }

            line.RemoveLineFinish();
            line.SetLineFinish(nextLineFinishManager);

            line.Shape.SetShapeData2("line[" + nextLineFinishManager.LineName + "]");

        }

        public void UpdateFinishStats()
        {
            foreach (LineFinishManager lineFinishManager in this)
            {
                lineFinishManager.UpdateFinishStats();
            }
        }

        public LineFinishManager this[string guid]
        {
            get
            {
                if (!LineFinishManagerDict.ContainsKey(guid))
                {
                    return null;
                }

                return LineFinishManagerDict[guid];
            }
        }

        public void Dispose()
        {
            foreach (LineFinishManager lineFinishManager in LineFinishManagers)
            {
                lineFinishManager.DeleteLayers();
                lineFinishManager.Delete();
            }

            this.Clear();

            lineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;

            lineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;

            lineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;

            lineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;

            SystemState.DesignStateChanged -= SystemState_DesignStateChanged;
        }

        internal bool LinesAreaDefined()
        {
            foreach (LineFinishManager lineFinishManager in this)
            {
                if (lineFinishManager.LineDict.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool ElementsFiltered()
        {
            foreach (LineFinishManager lineFinishManger in this)
            {
                if (lineFinishManger.Filtered)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
