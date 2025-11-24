
namespace CanvasLib.Pan_And_Zoom
{
    using Geometry;
    using Graphics;
    using System;
    using System.Windows.Forms;
    using Globals;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;
    using SettingsLib;

    public class PanAndZoomController
    {
        private GraphicsWindow window;

        private GraphicsPage page;

        private Visio.Window vsoWindow => window is null ? null : window.VisioWindow;

        private Visio.Page visioPage => page is null ? null : page.VisioPage;

        ToolStripButton zoomInButton = null;

        ToolStripButton zoomOutButton = null;

        ToolStripDropDownButton zoomPctButton = null;
        public ToolStripStatusLabel lblCenter;
        ToolStripButton zoomToFitButton = null;

        double drawWdth
        {
            get;
            set;
        }

        double drawHght
        {
            get;
            set;
        }

        double margin = 0.0;

        double precision = 0.00;

        private bool lockOnUnderSize = false;

        public bool LockOnUnderSize
        {
            get
            {
                return lockOnUnderSize;
            }

            set
            {
                lockOnUnderSize = false;

                if (value == false)
                {
                    vsoWindow.ScrollLock = false;
                }

                else
                {
                    lockOnUnderSize = true;

                    setZoomLockIfNecessary();
                }
            }
        }

        public PanAndZoomController(
            GraphicsWindow window
            ,GraphicsPage page
            ,double drawWdth
            ,double drawHght
            ,ToolStripButton zoomInButton
            ,ToolStripButton zoomOutButton
            ,ToolStripDropDownButton zoomPctButton
            ,ToolStripButton zoomToFitButton
            ,bool lockOnUnderSize = false)
        {
            

            this.window = window;

            this.page = page;

            this.drawWdth = drawWdth;
            this.drawHght = drawHght;

            wCenter = drawWdth * 0.5;
            hCenter = drawHght * 0.5;
     
            this.LockOnUnderSize = lockOnUnderSize;

            this.zoomInButton = zoomInButton;
            this.zoomOutButton = zoomOutButton;
            this.zoomPctButton = zoomPctButton;
            this.zoomToFitButton = zoomToFitButton;

            if (zoomInButton != null)
            {
                this.zoomInButton.Click += ZoomInButton_Click;
            }

            if (zoomOutButton != null)
            {
                this.zoomOutButton.Click += ZoomOutButton_Click;
            }

            if (this.zoomPctButton != null)
            {
                this.zoomPctButton.DropDownItemClicked += ZoomPctButton_DropDownItemClicked;
            }

            if (this.zoomToFitButton != null)
            {
                this.zoomToFitButton.Click += ZoomToFitButton_Click;
            }

            vsoWindow.ZoomBehavior = Visio.VisZoomBehavior.visZoomVisioExact;
        }

        public ToolStripLabel lblIgnoreScrollEvent;

        bool ignoreScrollEvent = false;

        bool IgnoreScrollEvent
        {
            get
            {
                return ignoreScrollEvent;
            }

            set
            {
                ignoreScrollEvent = value;

                if (lblIgnoreScrollEvent != null)
                {
                    this.lblIgnoreScrollEvent.Text = "Ignore Scroll Events: " + value.ToString();
                }
            }
        }

        public void ProcessZoomToArea(Coordinate areaDragOutStartCoord, Coordinate areaDragOutEndCoord)
        {
            double minX = Math.Min(areaDragOutStartCoord.X, areaDragOutEndCoord.X);
            double maxX = Math.Max(areaDragOutStartCoord.X, areaDragOutEndCoord.X);

            double minY = Math.Min(areaDragOutStartCoord.Y, areaDragOutEndCoord.Y);
            double maxY = Math.Max(areaDragOutStartCoord.Y, areaDragOutEndCoord.Y);

            double deltaX = maxX - minX;
            double deltaY = maxY - minY;

            if (Math.Abs(deltaX) <= 0.5)
            {
                return;
            }

            if (Math.Abs(deltaY) <= 0.5)
            {
                return;
            }

            VisioInterop.SetViewRect(this.window, minX, maxY, maxX - minX, maxY - minY);

            //visioWindow.SetViewRect(minX, maxY, maxX - minX, maxY - minY);

            SetCurrZoom();

            this.zoomPctButton.Text = (100.0 * CurrZoom).ToString("#,##0") + '%';

            window?.DeselectAll();
        }

        public void SetPageSize(double pageWidth, double pageHeight)
        {
            this.drawWdth = pageWidth;
            this.drawHght = pageHeight;
        }

        bool vScrollActive = false;

        private bool inViewChanged = false;

        private void VsoWindow_ViewChanged(Visio.Window Window)
        {
           
            if (!inViewChanged)
            {
                inViewChanged = true;
                double dLeft, dTop, dWdth, dHght;

                Window.GetViewRect(out dLeft, out dTop, out dWdth, out dHght);

                VisioInterop.SetViewRect(this.window, 1.05 * dLeft, 1.05 * dTop, 1.05 * dWdth, 1.05 * dHght);

                //Window.SetViewRect(1.05 * dLeft, 1.05 * dTop, 1.05 * dWdth, 1.05 * dHght);

            }
            return;

            if (!inSetZoom)
            {

                // Call from mouse wheel zoom
                SetCurrZoom();

               // SetCenters();

                setZoomLockIfNecessary();

                if (this.zoomInButton != null)
                {
                    this.zoomPctButton.Text = (100.0 * CurrZoom).ToString("#,##0") + '%';
                }


            }

            if (inViewChanged)
            {
                return;
            }

            inViewChanged = true;

            doViewChanged();

            inViewChanged = false;
        }

        private void SetCurrZoom()
        {
            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            double ratioWdth = drawWdth / viewWdth;
            double ratioHght = drawHght / viewHght;


            if (ratioWdth <= ratioHght)
            {
                CurrZoom = ratioHght;
            }

            else
            {
                CurrZoom = ratioWdth;
            }
        }

        double prevViewLeft = 0;
        double prevViewUppr = 0;
        double prevViewWdth = 0;
        double prevViewHght = 0;

        private void doViewChanged()
        {
            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            // There is an apparent bug in the visio SetView function so that some times it doesn't actually set the view
            // rectangle as per the input parameters. As a result it triggers a view change event even though the view has not
            // changed. The following code checks to see if the view has actually changed and exits if it has not.

            if (viewLeft == prevViewLeft && viewUppr == prevViewUppr && viewWdth == prevViewWdth && viewHght == prevViewHght)
            {
                return;
            }

            if (vsoWindow.ScrollLock)
            {
                return;
            }
            
            horizontalScrollControl();

            verticalScrollControl();

            vsoWindow.GetViewRect(out prevViewLeft, out prevViewUppr, out prevViewWdth, out prevViewHght);

        }

        private void setZoomLockIfNecessary()
        {
            if (!lockOnUnderSize)
            {
                return;
            }

            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            double viewRght = Math.Round(viewLeft + viewWdth, 1);

            double viewLowr = Math.Round(viewUppr - viewHght);

            if (viewLeft < 0.0 && viewRght > drawWdth && viewLowr < 0.0 && viewUppr > drawHght)
            {
                vsoWindow.ScrollLock = true;

            }

            else
            {
                vsoWindow.ScrollLock = false;
            }
        }

        bool inHorizontalScrollControl = false;

        private void horizontalScrollControl()
        {
            if (inHorizontalScrollControl)
            {
                return;
            }

            inHorizontalScrollControl = true;

            doHorizontalScrollControl();

            inHorizontalScrollControl = false;
        }

        private void doHorizontalScrollControl()
        {
            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            double viewRght = Math.Round(viewLeft + viewWdth, 1);

           // double viewRght = viewLeft + viewWdth;

            double rsltLeft = viewLeft;

            ScrolledWindowState viewLeftState;
            ScrolledWindowState viewRghtState;

            if (viewWdth >= drawWdth)
            {
                viewLeftState
                    = viewLeft < 0.0 + margin ? ScrolledWindowState.OpenSpaced : viewLeft > 0.0 + margin ? ScrolledWindowState.Overlapped : ScrolledWindowState.FitToEdge;

                viewRghtState
                    = viewRght > drawWdth - margin ? ScrolledWindowState.OpenSpaced : viewRght < drawWdth - margin ? ScrolledWindowState.Overlapped : ScrolledWindowState.FitToEdge;

                if (viewLeftState == ScrolledWindowState.Overlapped)
                {
                    VisioInterop.SetViewRect(this.window, 0 - margin, viewUppr, viewWdth, viewHght);

                    //visioWindow.SetViewRect(0 - margin, viewUppr, viewWdth, viewHght);

                    rsltLeft = 0.0;
                }

                else if (viewRghtState == ScrolledWindowState.Overlapped)
                {
                    double left = drawWdth - viewWdth + margin;

                    VisioInterop.SetViewRect(this.window, left, viewUppr, viewWdth, viewHght);

                    //visioWindow.SetViewRect(left, viewUppr, viewWdth, viewHght);

                    viewLeft = left;

                    vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

                    viewRght = viewLeft + viewWdth;

                    double delta = 0;

                    while (viewRght > drawWdth)
                    {
                        delta += .1;

                        left -= drawWdth - viewRght + delta;

                        VisioInterop.SetViewRect(this.window, left, viewUppr, viewWdth, viewHght);

                        //visioWindow.SetViewRect(left, viewUppr, viewWdth, viewHght);

                        viewLeft = left;

                        vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

                        viewRght = viewLeft + viewWdth;
                    }
                   
                    rsltLeft = left;
                }

                IgnoreScrollEvent = true;

                double leftMax = drawWdth - viewWdth;


                IgnoreScrollEvent = false;

                return;
            }

            else
            {
                viewLeftState
                    = viewLeft < 0.0 - margin - precision ? ScrolledWindowState.OpenSpaced : viewLeft > 0.0 - margin + precision ? ScrolledWindowState.Overlapped : ScrolledWindowState.FitToEdge;

                viewRghtState
                    = viewRght > drawWdth + margin +  precision ? ScrolledWindowState.OpenSpaced : viewRght < drawWdth + margin - precision ? ScrolledWindowState.Overlapped : ScrolledWindowState.FitToEdge;

                if (viewLeftState == ScrolledWindowState.OpenSpaced)
                {
                    VisioInterop.SetViewRect(this.window, 0 - margin, viewUppr, viewWdth, viewHght);

                    //visioWindow.SetViewRect(0-margin, viewUppr, viewWdth, viewHght);

                    rsltLeft = 0;
                }

                else if (viewRghtState == ScrolledWindowState.OpenSpaced)
                {
                    VisioInterop.SetViewRect(this.window, drawWdth - viewWdth + margin, viewUppr, viewWdth, viewHght);

                    //visioWindow.SetViewRect(drawWdth - viewWdth + margin, viewUppr, viewWdth, viewHght);

                    rsltLeft = drawWdth - viewWdth;
                }

                IgnoreScrollEvent = true;

                double leftMax = drawWdth - viewWdth;

                IgnoreScrollEvent = false;

                return;
            }

        }

        bool inVerticalScrollControl = false;

        private void verticalScrollControl()
        {
            if (inVerticalScrollControl)
            {
                return;
            }

            inVerticalScrollControl = true;

            doVerticalScrollControl();

            inVerticalScrollControl = false;
        }

        private void doVerticalScrollControl()
        {
            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            double viewLowr = viewUppr - viewHght; // Math.Round(viewUppr - viewHght);

            ScrolledWindowState viewUpprState
                = viewUppr > drawHght ? ScrolledWindowState.OpenSpaced : viewUppr < drawHght ? ScrolledWindowState.Overlapped : ScrolledWindowState.FitToEdge;

            ScrolledWindowState viewLowrState
                = viewLowr < 0.0 ? ScrolledWindowState.OpenSpaced : viewLowr > 0.0 ? ScrolledWindowState.Overlapped : ScrolledWindowState.FitToEdge;


            double rsltUppr = viewUppr;

            if (viewHght >= drawHght)
            {
                if (viewLowrState == ScrolledWindowState.Overlapped)
                {
                    VisioInterop.SetViewRect(this.window, viewLeft, viewHght, viewWdth, viewHght);

                    //visioWindow.SetViewRect(viewLeft, viewHght, viewWdth, viewHght);

                    rsltUppr = viewHght;

                }

                else if (viewUpprState == ScrolledWindowState.Overlapped)
                {
                    VisioInterop.SetViewRect(this.window, viewLeft, drawHght, viewWdth, viewHght);

                    //visioWindow.SetViewRect(viewLeft, drawHght, viewWdth, viewHght);

                    vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

                    viewUppr = viewLowr + viewHght;

                    double delta = 0;

                    double uppr = viewUppr;

                    while (viewUppr < drawHght)
                    {
                        delta += .01;

                        uppr += delta;

                        VisioInterop.SetViewRect(this.window, viewLeft, uppr, viewWdth, viewHght);

                       // visioWindow.SetViewRect(viewLeft, uppr, viewWdth, viewHght);

                        vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);
                    }

                    rsltUppr = drawHght;
                }

                IgnoreScrollEvent = true;

                double v;

                double delta1 = viewHght - drawHght;

                if (delta1 < 0.001)
                {
                    IgnoreScrollEvent = false;

                    return;
                }

                else
                {
                    v = (viewHght - rsltUppr) / delta1;
                }

                IgnoreScrollEvent = false;

                return;
            }

            else
            {
                if (viewLowrState == ScrolledWindowState.OpenSpaced)
                {
                    VisioInterop.SetViewRect(this.window, viewLeft, viewHght, viewWdth, viewHght);

                   // visioWindow.SetViewRect(viewLeft, viewHght, viewWdth, viewHght);

                    vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

                    rsltUppr = viewHght;
                }

                else if (viewUpprState == ScrolledWindowState.OpenSpaced)
                {
                    VisioInterop.SetViewRect(this.window, viewLeft, drawHght, viewWdth, viewHght);

                    //visioWindow.SetViewRect(viewLeft, drawHght, viewWdth, viewHght);

                    rsltUppr = drawHght;
                }

                IgnoreScrollEvent = true;

                double v;

                double delta = drawHght - viewHght;

                if (delta < 0.001)
                {
                    IgnoreScrollEvent = false;

                    return;
                }

                else
                {
                    v = (drawHght - rsltUppr) / delta;
                }

                IgnoreScrollEvent = false;

                return;
            }
        }

        private double wCenter;
        private double hCenter;

        public double CurrZoom
        {
            get { return SystemState.ZoomPercent;  }
            set { SystemState.ZoomPercent = value; }
        } 

        private bool inSetZoom = false;


        public void SetZoom()
        {
            SetZoom(CurrZoom);
        }

        public void SetZoom(double zoom, bool centerDrawing = false)
        {
            inSetZoom = true;

            doSetZoom(zoom, centerDrawing);

            inSetZoom = false;
        }

        private void doSetZoom(double zoom, bool centerDrawing = false)
        {
            double viewLeft;
            double viewUppr;
            double viewWdth;
            double viewHght;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            double ratioWdth = drawWdth / viewWdth;
            double ratioHght = drawHght / viewHght;

            double zoomWdth = 0;
            double zoomHght = 0;

            zoomHght = drawHght / zoom;
            zoomWdth = drawWdth / zoom;

            double deltaZoom = zoom / CurrZoom;

            double viewHorizontalCenter;
            double viewVerticalCenter;

            if (!centerDrawing)
            {
                viewHorizontalCenter = viewLeft + viewWdth * 0.5;
                viewVerticalCenter = viewUppr - viewHght * 0.5;
            }

            else
            {
                viewHorizontalCenter = viewWdth * 0.5;
                viewVerticalCenter = viewHght * 0.5;
            }

            viewLeft = viewHorizontalCenter - zoomWdth * 0.5;
            viewUppr = viewVerticalCenter + zoomHght * 0.5;

            VisioInterop.SetViewRect(this.window, viewLeft, viewUppr, zoomWdth, zoomHght);

            //visioWindow.SetViewRect(viewLeft, viewUppr, zoomWdth, zoomHght);

            SetCurrZoom();

            this.zoomPctButton.Text = (100.0 * CurrZoom).ToString("#,##0") + '%';


            // MDD Reset. The following is to validate that the center has not changed. Can be removed after debugging.

            double viewLeft1;
            double viewUppr1;
            double viewWdth1;
            double viewHght1;

            vsoWindow.GetViewRect(out viewLeft1, out viewUppr1, out viewWdth1, out viewHght1);

            double viewHorizontalCenter1 = viewLeft1 + viewWdth1 * 0.5;
            double viewVerticalCenter1 = viewUppr1 - viewHght1 * 0.5;

        }


        public void ZoomInButton_Click(object sender, EventArgs e)
        {
            double zoomPct = (double) GlobalSettings.ZoomInOutButtonPercent / 100.0;

            double zoom = Math.Max(0.05, Math.Min(9.99, CurrZoom + zoomPct));
            
            SetZoom(zoom);
        }

        public void ZoomOutButton_Click(object sender, EventArgs e)
        {
            double zoomPct = (double)GlobalSettings.ZoomInOutButtonPercent / 100.0;

            double zoom = Math.Max(0.05, Math.Min(9.99, CurrZoom - zoomPct));
            
            SetZoom(zoom);
        }

        private void ZoomPctButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string strZoom = e.ClickedItem.Text;

            double zoom;

            if (strZoom == "Custom")
            {
                CustomZoom customZoom = new CustomZoom();

                DialogResult dr = customZoom.ShowDialog();

                if (dr == DialogResult.Cancel)
                {
                    this.zoomPctButton.Text = (100.0 * CurrZoom).ToString("#,##0") + '%';

                    return;
                }

                zoom = customZoom.zoomPercent / 100.0;

                this.zoomPctButton.Text = (100 * CurrZoom).ToString("#,##0") + '%';
            }

            else
            {
                strZoom = strZoom.Substring(0, strZoom.Length - 1);

                zoom = double.Parse(strZoom) / 100.0;

                this.zoomPctButton.Text = e.ClickedItem.Text;
            }


            SetZoom(zoom);
        }

        public void ZoomToFit()
        {
            ZoomToFitButton_Click(null, null);
        }

        public void ZoomToFitButton_Click(object sender, EventArgs e)
        {
            if (drawWdth <= 0 || drawHght <= 0)
            {
                SetZoom(1);

                return;
            }

            CurrZoom = 1.0;

            SetZoom(CurrZoom, true);

            CenterDrawing();

        }

        public void ProcessGeneralZoomIn()
        {
            this.ZoomInButton_Click(null, null);
        }

        public void ProcessGeneralZoomOut()
        {
            this.ZoomOutButton_Click(null, null);
        }

        public void CenterDrawing()
        {

            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            vsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            double pageWidth = page.PageWidth;
            double pageHeight = page.PageHeight;


            double scaleWdth = viewWdth / page.PageWidth;
            double scaleHght = viewHght / page.PageHeight;

            double scale = Math.Min(scaleWdth, scaleHght);

            //viewWdth = viewWdth * scale;
            //viewHght = viewHght * scale;

            viewLeft = Math.Max(0, (viewWdth - pageWidth) * 0.5);
            viewUppr = Math.Max(0, (viewHght - pageHeight) * 0.5) + pageHeight;

            vsoWindow.SetViewRect(-viewLeft, viewUppr, viewWdth, viewHght);

        }
    }

    public enum ScrolledWindowState
    {
        FitToEdge = 0,
        OpenSpaced = 1,
        Overlapped = 2
    }
}
