

namespace CanvasLib.Legend
{
    using System.Collections.Generic;
    using Graphics;
    using FinishesLib;

    using System.Windows.Forms;
    using Visio = Microsoft.Office.Interop.Visio;
    public class LegendOld
    {
        private GraphicsPage page;

        private GraphicsWindow window;

        private Visio.Window vsoWindow;

        private AreaFinishBaseList areaFinishBaseList;

        private double yBase;
        private double xBase;

        public Shape Shape;

        private Shape baseRectangle;

        List<LegendElement> globalLegendElementList = new List<LegendElement>();
        List<LegendElement> palletLegendElementList = new List<LegendElement>();

        public GraphicsLayer LegendLayer
        {
            get;
            set;
        }

        public LegendLocation LegendShowLocation = LegendLocation.None;
        public LegendLocation LegendDrawLocation = LegendLocation.None;

        public LegendOld(
            GraphicsWindow window,
            GraphicsPage page,
            AreaFinishBaseList areaFinishBaseList
            )
        {
            this.areaFinishBaseList = areaFinishBaseList;
            this.page = page;
            this.window = window;

            this.vsoWindow = this.window.VisioWindow;

            LegendLayer = new GraphicsLayer(window, page, "[Legend]LegendLayer", GraphicsLayerType.Static);

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            VisioInterop.SetLayerVisibility(LegendLayer, false);

        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase item, int position)
        {
            Undraw();

            globalLegendElementList.Clear();
            palletLegendElementList.Clear();

            Init(xBase, yBase);
        }

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase item)
        {
            Undraw();

            globalLegendElementList.Clear();
            palletLegendElementList.Clear();

            Init(xBase, yBase);
        }

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            Undraw();

            globalLegendElementList.Clear();
            palletLegendElementList.Clear();

            Init(xBase, yBase);
        }

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            Undraw();

            globalLegendElementList.Clear();
            palletLegendElementList.Clear();

            Init(xBase, yBase);
        }

        public void Reinit(AreaFinishBaseList areaFinishBaseList)
        {
            areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;

            this.areaFinishBaseList = areaFinishBaseList;

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            if (LegendShowLocation == LegendLocation.Left)
            {
                LegendShowLocation = LegendLocation.Notset;
                LegendDrawLocation = LegendLocation.Notset;

                ShowLeft();
            }

            else if (LegendShowLocation == LegendLocation.Right)
            {
                LegendShowLocation = LegendLocation.Notset;
                LegendDrawLocation = LegendLocation.Notset;

                ShowRight();
            }

            else
            {
                ShowNone();
            }
        }

        public void Init(double xBase, double yBase)
        {
            this.xBase = xBase;
            this.yBase = yBase;

            int posnOnPallet = 0;

            globalLegendElementList.ForEach(e => e.Delete());

            globalLegendElementList.Clear();

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                LegendElement legendElement = new LegendElement(vsoWindow, page, areaFinishBase, 0.67, 0.2, 0.33, posnOnPallet);

                globalLegendElementList.Add(legendElement);

                legendElement.FilteredChanged += LegendElement_FilteredChanged;

                posnOnPallet++;
            }

            Draw();
        }

        private void Draw()
        {
            palletLegendElementList.Clear();

            double y = yBase - 0.125;
            double x = xBase + 0.125;

            foreach (LegendElement legendElement in globalLegendElementList)
            {
                if (legendElement.Filtered)
                {
                    continue;
                }

                palletLegendElementList.Add(legendElement);

                legendElement.Draw(x, y);

                y -= 0.75;
            }

            baseRectangle = page.DrawRectangle(xBase, yBase, x + 0.75, y + 0.25);

            baseRectangle.VisioShape.Data1 = "[Legend]BaseRectangle";

            VisioInterop.SetNolineMode(baseRectangle);

            Visio.Selection selection = page.VisioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSub, baseRectangle);

            selection.Select(baseRectangle.VisioShape, 2);

            palletLegendElementList.ForEach(
                s => {
                    VisioInterop.BringToFront(s.Shape);
                    selection.Select(s.Shape.VisioShape, 2);
                });

            Shape = new Shape(selection.Group(), ShapeType.Legend);

            Shape.VisioShape.Data1 = "Legend1";

            VisioInterop.DeselectAll(window);

            LegendLayer.Add(Shape, 0);
        }

        private void Undraw()
        {
            LegendLayer.Remove(Shape, 0);

            foreach (LegendElement legendElement in palletLegendElementList)
            {
                legendElement.Undraw();
            }

            Shape.Delete();
        }

        private void LegendElement_FilteredChanged(LegendElement legendElement, bool filtered)
        {
            Undraw();
            Draw();
        }

        public void ShowLeft()
        {
            if (LegendShowLocation == LegendLocation.Left)
            {
                return;
            }

            LegendShowLocation = LegendLocation.Left;
          
            VisioInterop.SetLayerVisibility(LegendLayer, true);

            if (LegendDrawLocation == LegendLocation.Left)
            {
                return;
            }

            LegendDrawLocation = LegendLocation.Left;

            Undraw();

            Init(0.125, page.PageHeight - 0.125);
            
        }

        public void ShowRight()
        {
            if (LegendShowLocation == LegendLocation.Right)
            {
                return;
            }

            LegendShowLocation = LegendLocation.Right;
           
            VisioInterop.SetLayerVisibility(LegendLayer, true);

            if (LegendDrawLocation == LegendLocation.Right)
            {
                return;
            }

            LegendDrawLocation = LegendLocation.Right;

            Undraw();

            Init(page.PageWidth - .8, page.PageHeight - 0.125);

        }

        public void ShowNone()
        {
            if (LegendShowLocation == LegendLocation.None)
            {
                return;
            }

            LegendShowLocation = LegendLocation.None;

            VisioInterop.SetLayerVisibility(LegendLayer, false);

        }
    }

    public enum LegendLocation
    {
        Notset = 0,
        None = 1,
        Left = 2,
        Right = 3
    }
}
