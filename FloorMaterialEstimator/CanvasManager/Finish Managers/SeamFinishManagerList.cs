namespace CanvasManager.FinishManager
{
    using System;
    using System.Collections.Generic;
    using Graphics;
    using FinishesLib;
    using Globals;
    using global::CanvasManager.CanvasShapes;

    public class SeamFinishManagerList : List<SeamFinishManager>, IDisposable
    {
        CanvasWindow canvasWindow;

        CanvasPage canvasPage;

        public Dictionary<string, SeamFinishManager> SeamFinishManagerDict { get; private set; } = new Dictionary<string, SeamFinishManager>();

        public IEnumerable<SeamFinishManager> SeamFinishManagers => (IEnumerable<SeamFinishManager>)this;

        private SeamFinishBaseList seamFinishBaseList;

        public int SelectedIndex => seamFinishBaseList.SelectedItemIndex;

        public SeamFinishManager SelectedSeamFinishManager => this[SelectedIndex];


        public SeamFinishManagerList(
            CanvasWindow canvasWindow
            , CanvasPage canvasPage
            , GraphicsWindow window
            , GraphicsPage page
            , SeamFinishBaseList seamFinishBaseList)
        {
            this.canvasWindow = canvasWindow;

            this.canvasPage = canvasPage;

            foreach (SeamFinishBase seamFinishBase in seamFinishBaseList)
            {
                SeamFinishManager seamFinishManager = new SeamFinishManager(canvasWindow, canvasPage, seamFinishBaseList, seamFinishBase);

                SeamFinishManagerDict.Add(seamFinishManager.Guid, seamFinishManager);

                this.Add(seamFinishManager);
            }

            this.seamFinishBaseList = seamFinishBaseList;

            seamFinishBaseList.ItemAdded += SeamFinishBaseList_ItemAdded;

            seamFinishBaseList.ItemRemoved += SeamFinishBaseList_ItemRemoved;

            seamFinishBaseList.ItemsSwapped += SeamFinishBaseList_ItemsSwapped;

            seamFinishBaseList.ItemInserted += SeamFinishBaseList_ItemInserted;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;
        }

        private void SystemState_DesignStateChanged(DesignState previousDesignState, DesignState currentDesignState)
        {
            foreach (SeamFinishManager seamFinishManager in this)
            {
                seamFinishManager.SetSeamState(currentDesignState, SystemState.SeamMode, seamFinishManager.Selected);
            }
        }

        //internal void SetDesignStateFormat(DesignState designState, SeamMode seamMode, bool showSeams)
        //{
        //    foreach (SeamFinishManager seamFinishManager in this)
        //    {
        //        seamFinishManager.SetDesignStateFormat(designState, seamMode, seamFinishManager.Filtered, showSeams);
        //    }
        //}

        private void SeamFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            SeamFinishManager tempItem = this[position1];

            this[position1] = this[position2];
            this[position2] = tempItem;
        }

        private void SeamFinishBaseList_ItemAdded(SeamFinishBase seamFinishBase)
        {
            SeamFinishManager seamFinishManager = new SeamFinishManager(canvasWindow, canvasPage, seamFinishBaseList, seamFinishBase);

            SeamFinishManagerDict.Add(seamFinishManager.Guid, seamFinishManager);

            this.Add(seamFinishManager);
        }

        private void SeamFinishBaseList_ItemInserted(SeamFinishBase seamFinishBase, int position)
        {
            SeamFinishManager seamFinishManager = new SeamFinishManager(canvasWindow, canvasPage, seamFinishBaseList, seamFinishBase);

            this.Insert(position, seamFinishManager);

            this.SeamFinishManagerDict.Add(seamFinishBase.Guid, seamFinishManager);

        }

        private void SeamFinishBaseList_ItemRemoved(string guid, int position)
        {
            SeamFinishManager seamFinishManager = SeamFinishManagerDict[guid];
            // Order is important here, since the Delete1 function clears the Guid

            seamFinishManager.AreaDesignStateLayer.Delete();
            seamFinishManager.SeamDesignStateLayer.Delete();
            seamFinishManager.SeamDesignStateLayer.Delete();

            SeamFinishManagerDict.Remove(guid);

            this.RemoveAt(position);

            seamFinishManager.Dispose();
        }

        //public void UpdateFinishStats()
        //{
        //    foreach (SeamFinishManager seamFinishManager in this)
        //    {
        //        seamFinishManager.UpdateFinishStats();
        //    }
        //}

        //public void SetupAllSeamLayers()
        //{
        //    foreach (SeamFinishManager seamFinishManager in this)
        //    {
        //        seamFinishManager.SetupAllSeamLayers();
        //    }
        //}

        //internal void MoveCanvasLayoutSeamToFinishType(CanvasLayoutSeam canvasLayoutSeam, SeamFinishManager nextFinishManager, bool updateColor = true)
        //{
        //    Debug.Assert(!canvasLayoutSeam.IsSubdivided()); // Only leaf seams can be removed

        //    SeamFinishManager currFinishManager = canvasLayoutSeam.SeamFinishManager;

        //    if (nextFinishManager == currFinishManager)
        //    {
        //        return;
        //    }

        //    currFinishManager.RemoveLayoutSeamFromFinish(canvasLayoutSeam, true);
        //    nextFinishManager.AddNormalLayoutSeam(canvasLayoutSeam, true, true);

        //    //canvasLayoutSeam.DrawCompositeShape(CanvasManager.Window, CanvasManager.Page);

        //    canvasLayoutSeam.PolygonInternalSeam.VisioShape.Data3 = canvasLayoutSeam.Guid;
        //}

        //internal void SetupSeamFilters()
        //{
        //    if (SystemState.DesignState == DesignState.Seam)
        //    {
        //        SetupAllSeamLayers();
        //    }

        //    else if (SystemState.DesignState == DesignState.Seam)
        //    {
        //        SystemGlobals.SetupAllSeamStateSeamLayersForSelectedSeam();
        //    }
        //}

        //internal void MoveSeamToSelectedSeamType(string guid)
        //{
        //    if (string.IsNullOrEmpty(guid))
        //    {
        //        return;
        //    }

        //    if (!canvasManager.CurrentPage.DirectedSeamDictContains(guid))
        //    {
        //        return;
        //    }

        //    CanvasDirectedSeam canvasDirectedSeam = canvasManager.CurrentPage.GetDirectedSeam(guid);

        //    MoveSeamToSeamType(canvasDirectedSeam, SelectedSeamFinishManager);
        //}

        //internal void MoveSeamToSelectedSeamType(CanvasDirectedSeam canvasDirectedSeam)
        //{
        //    if ((object)canvasDirectedSeam == null)
        //    {
        //        return;
        //    }

        //    MoveSeamToSeamType(canvasDirectedSeam, SelectedSeamFinishManager);
        //}

        //internal void MoveSeamToSeamType(CanvasDirectedSeam seam, SeamFinishManager nextSeamFinishManager)
        //{
        //    if ((object)seam == null)
        //    {
        //        return;
        //    }

        //    SeamFinishManager currSeamFinishManager = seam.SeamFinishManager;

        //    if (nextSeamFinishManager == currSeamFinishManager)
        //    {
        //        return;
        //    }

        //    seam.RemoveSeamFinish();
        //    seam.SetSeamFinish(nextSeamFinishManager);

        //    seam.Shape.SetShapeData2("seam[" + nextSeamFinishManager.SeamName + "]");

        //}

        //public void UpdateFinishStats()
        //{
        //    foreach (SeamFinishManager seamFinishManager in this)
        //    {
        //        seamFinishManager.UpdateFinishStats();
        //    }
        //}

        public SeamFinishManager this[string guid]
        {
            get
            {
                if (!SeamFinishManagerDict.ContainsKey(guid))
                {
                    return null;
                }

                return SeamFinishManagerDict[guid];
            }
        }

        public void Dispose()
        {
            foreach (SeamFinishManager seamFinishManager in SeamFinishManagers)
            {
                seamFinishManager.Delete();
            }

            this.Clear();

            seamFinishBaseList.ItemAdded -= SeamFinishBaseList_ItemAdded;

            seamFinishBaseList.ItemRemoved -= SeamFinishBaseList_ItemRemoved;

            seamFinishBaseList.ItemsSwapped -= SeamFinishBaseList_ItemsSwapped;

            seamFinishBaseList.ItemInserted -= SeamFinishBaseList_ItemInserted;

            SystemState.DesignStateChanged -= SystemState_DesignStateChanged;
        }
    }
}
