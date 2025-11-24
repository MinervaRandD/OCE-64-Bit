

namespace CanvasShapes
{
    using System;
    using System.Collections.Generic;
    using Graphics;
    using FinishesLib;
    using Globals;
    using System.Diagnostics;

    public class AreaFinishManagerList : List<AreaFinishManager>, IDisposable
    {
        GraphicsWindow window;

        GraphicsPage page;

        public Dictionary<string, AreaFinishManager> AreaFinishManagerDict { get; private set; } = new Dictionary<string, AreaFinishManager>();

        public IEnumerable<AreaFinishManager> AreaFinishManagers => (IEnumerable<AreaFinishManager>)this;

        private AreaFinishBaseList areaFinishBaseList;

        public int SelectedIndex => areaFinishBaseList.SelectedItemIndex;

        public AreaFinishManager SelectedAreaFinishManager => this[SelectedIndex];

        public AreaFinishManagerList(
            GraphicsWindow window
            , GraphicsPage page
            , AreaFinishBaseList areaFinishBaseList)
        {
            this.window = window;

            this.page = page;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                AreaFinishManager areaFinishManager = new AreaFinishManager(window, page, areaFinishBaseList, areaFinishBase);

                AreaFinishManagerDict.Add(areaFinishManager.Guid, areaFinishManager);

                this.Add(areaFinishManager);
            }

            this.areaFinishBaseList = areaFinishBaseList;

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;

            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;

            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;
        }

        private void SystemState_DesignStateChanged(DesignState prevDesignState, DesignState designState)
        {
            SetDesignStateFormat(SystemState.DesignState, SystemState.SeamMode, SystemState.ShowAreas);
        }

        internal void SetDesignStateFormat(DesignState designState, SeamMode seamMode, bool showAreas)
        {
            foreach (AreaFinishManager areaFinishManager in this)
            {
                areaFinishManager.SetDesignStateFormat(designState, seamMode, areaFinishManager.Filtered, showAreas);
            }
        }

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            AreaFinishManager tempItem = this[position1];

            this[position1] = this[position2];
            this[position2] = tempItem;
        }

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase areaFinishBase)
        {
            AreaFinishManager areaFinishManager = new AreaFinishManager(window, page, areaFinishBaseList, areaFinishBase);

            AreaFinishManagerDict.Add(areaFinishManager.Guid, areaFinishManager);

            this.Add(areaFinishManager);
        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase areaFinishBase, int position)
        {
            AreaFinishManager areaFinishManager = new AreaFinishManager(window, page, areaFinishBaseList, areaFinishBase);

            this.Insert(position, areaFinishManager);

            this.AreaFinishManagerDict.Add(areaFinishBase.Guid, areaFinishManager);

        }

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            AreaFinishManager areaFinishManager = AreaFinishManagerDict[guid];

            AreaFinishManagerDict.Remove(guid);

            this.RemoveAt(position);

            areaFinishManager.Dispose();
        }

        public void UpdateFinishStats()
        {
            foreach (AreaFinishManager areaFinishManager in this)
            {
                areaFinishManager.UpdateFinishStats();
            }
        }

        public void SetupAllSeamLayers()
        {
            foreach (AreaFinishManager areaFinishManager in this)
            {
                areaFinishManager.SetupAllSeamLayers();
            }
        }

        internal void MoveCanvasLayoutAreaToFinishType(CanvasLayoutArea canvasLayoutArea, AreaFinishManager nextFinishManager, bool updateColor = true)
        {
            Debug.Assert(!canvasLayoutArea.IsSubdivided()); // Only leaf areas can be removed

            AreaFinishManager currFinishManager = canvasLayoutArea.AreaFinishManager;

            if (nextFinishManager == currFinishManager)
            {
                return;
            }

            currFinishManager.RemoveLayoutAreaFromFinish(canvasLayoutArea, true);
            nextFinishManager.AddNormalLayoutArea(canvasLayoutArea, true, true);

            //canvasLayoutArea.DrawCompositeShape(CanvasManager.Window, CanvasManager.Page);

            canvasLayoutArea.PolygonInternalArea.VisioShape.Data3 = canvasLayoutArea.Guid;
        }

        internal void SetupSeamFilters()
        {
            if (SystemState.DesignState == DesignState.Area)
            {
                SetupAllSeamLayers();
            }

            else if (SystemState.DesignState == DesignState.Seam)
            {
                SystemGlobals.SetupAllSeamStateSeamLayersForSelectedArea();
            }
        }

        internal bool CanvasAreasAreSeamed()
        {
            foreach (AreaFinishManager areaFinishManager in this)
            {
                if (areaFinishManager.CanvasAreasAreSeamed())
                {
                    return true;
                }
            }

            return false;
        }

        public AreaFinishManager this[string guid]
        {
            get
            {
                if (!AreaFinishManagerDict.ContainsKey(guid))
                {
                    return null;
                }

                return AreaFinishManagerDict[guid];
            }
        }

        public void Dispose()
        {
            foreach (AreaFinishManager areaFinishManager in AreaFinishManagers)
            {
                areaFinishManager.DeleteLayers();
                areaFinishManager.Delete();
            }

            this.Clear();

            areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;

            areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;

            areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;

            areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;

            SystemState.DesignStateChanged -= SystemState_DesignStateChanged;
        }

        internal bool CanvasAreasDefinined()
        {
            foreach (AreaFinishManager areaFinishManager in this)
            {
                if (areaFinishManager.CanvasLayoutAreaDict.Count > 0)
                {
                    return true;
                }

                if (areaFinishManager.RemnantCanvasLayoutAreaDict.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool ElementsFiltered()
        {
            foreach (AreaFinishManager areaFinishManager in this)
            {
                if (areaFinishManager.Filtered)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
