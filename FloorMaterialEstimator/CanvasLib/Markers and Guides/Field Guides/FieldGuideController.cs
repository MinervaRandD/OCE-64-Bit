

namespace CanvasLib.Markers_and_Guides
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Graphics;
    using SettingsLib;
    using Utilities;
    using AxMicrosoft = AxMicrosoft.Office.Interop.VisOcx;

    public class FieldGuideController
    {
        private AxMicrosoft.AxDrawingControl axDrawingControl;

        private GraphicsPage page { get; set; }

        private GraphicsWindow window { get; set; }

        public Dictionary<string, FieldGuide> FieldGuideList = new Dictionary<string, FieldGuide>();

        public IEnumerable<FieldGuide> fieldGuides => FieldGuideList.Values;

        public GraphicsLayerBase FieldGuideLayer { get; private set; }
        
        
        public Color LineColor { get; set; }

        public bool SuspendAlignMode { get; set; }

        public double Opacity { get; set; }

        public double LineWidthInPts { get; set; }

        public int LineStyle { get; set; }

        internal ToolStripButton btnShowFieldGuides;

        public FieldGuideController(
            AxMicrosoft.AxDrawingControl axDrawingControl
            , GraphicsWindow window
            , GraphicsPage page
            , ToolStripButton BtnShowFieldGuides)
        {
            this.axDrawingControl = axDrawingControl;

            this.page = page;

            this.window = window;

            FieldGuideLayer = new GraphicsLayerBase(window, page, "[FieldGuideLayer]", GraphicsLayerType.FieldGuideLayer, GraphicsLayerStyle.Dynamic);

            btnShowFieldGuides = BtnShowFieldGuides;

            if (btnShowFieldGuides.Checked)
            {
                FieldGuideLayer.SetLayerVisibility(true);
            }

            else
            {
                FieldGuideLayer.SetLayerVisibility(false);
            }
        }

        public void Init(Color lineColor, double opacity, double lineWidthInPts, int lineStyle)
        {
            DeleteAllFieldGuides();

            this.LineColor = lineColor;
            this.Opacity = opacity;
            this.LineWidthInPts = lineWidthInPts;
            this.LineStyle = lineStyle;
        }


        public bool GetClosestGuides(double currX, double currY, out double? guideX, out double? guideY, double snapDistance, int key)
        {
            guideX = null;
            guideY = null;

            if (FieldGuideList.Count <= 0)
            {
                return false;
            }

            double? minX = null;
            double? minY = null;

            double minXDist = double.MaxValue;
            double minYDist = double.MaxValue;

            double min = double.MaxValue;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                double distX = Math.Abs(currX - fieldGuide.X);
                double distY = Math.Abs(currY - fieldGuide.Y);

                if (distX <= snapDistance)
                {
                    if (distX < minXDist)
                    {
                        minXDist = distX;
                        minX = fieldGuide.X;
                    }
                }

                if (distY <= snapDistance)
                {
                    if (distY < minYDist)
                    {
                        minYDist = distY;
                        minY = fieldGuide.Y;
                    }
                }
            }

            if (minX == null && minY == null)
            {
                return false;
            }

            guideX = minX;
            guideY = minY;

            return true;
        }

        public void AddFieldGuide(double x, double y)
        {
            FieldGuide fieldGuide = new FieldGuide(window, page, x, y, LineStyle, LineColor, Opacity, LineWidthInPts, page.PageWidth, page.PageHeight, FieldGuideLayer);

            FieldGuideList.Add(fieldGuide.Guid, fieldGuide);

            fieldGuide.Draw();

            window?.DeselectAll();
        }

        public void AddFieldGuide(double x, double y, string guid)
        {
            FieldGuide fieldGuide = new FieldGuide(window, page, x, y, LineStyle, LineColor, Opacity, LineWidthInPts, page.PageWidth, page.PageHeight, FieldGuideLayer);

            fieldGuide.Guid = guid;

            FieldGuideList.Add(fieldGuide.Guid, fieldGuide);

            GuidMaintenance.AddGuid(guid, fieldGuide);

            fieldGuide.Draw();

            //------------------------------------------------------------------------------------------//
            // The following is necessary in case the layer is created as a result of the previous call //
            //------------------------------------------------------------------------------------------//

            FieldGuideLayer.SetLayerVisibility(true);

            //--------------------------------------------------------------------------------------------------------//
            // Check the 'show field guides' button here, because the user obviously wants to use them at this point. //
            //--------------------------------------------------------------------------------------------------------//

            if (!btnShowFieldGuides.Checked)
            {
                btnShowFieldGuides.Checked = true;
            }

            window?.DeselectAll();
        }

        public void DeleteFieldGuide(string Guid)
        {

            if (FieldGuideList.ContainsKey(Guid))
            {
                FieldGuide fieldGuide = FieldGuideList[Guid];

                FieldGuideList.Remove(Guid);

                fieldGuide.Delete();
            }
        }

        public void DeleteAllFieldGuides()
        {
            this.SuspendAlignMode = false;
            fieldGuides.ToList().ForEach(f => f.Delete());

            FieldGuideList.Clear();
        }

        public FieldGuide GetFieldGuide(double x, double y, double radius, out double? minDistX, out double? minDistY)
        {
            FieldGuide minGuide = null;
            double minDistance = double.MaxValue;

            minDistX = null;
            minDistY = null;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                double distX = x == -100000 ? double.MaxValue : Math.Abs(x - fieldGuide.X); // MathUtils.H2Distance(x, y, fieldGuide.X, fieldGuide.Y);
                double distY = y == -100000 ? double.MaxValue : Math.Abs(y - fieldGuide.Y);

                if (distX > radius && distY > radius)
                {
                    continue;
                }

                double distMin = Math.Min(distX, distY);
                
                if (distMin < minDistance)
                {
                    minDistance = distMin;

                    minDistX = distX <= radius ? distX : (double?)null;

                    minDistY = distY <= radius ? distY : (double?)null;

                    minGuide = fieldGuide;
                }
            }

            return minGuide;
        }

        public void HideFieldGuides()
        {
            this.SuspendAlignMode = false;
            this.FieldGuideLayer.SetLayerVisibility(false);
        }

        public void ShowFieldGuides()
        {
            this.FieldGuideLayer.SetLayerVisibility(true);
            if (FieldGuideList.Count > 0)
            {
                this.SuspendAlignMode = true;
            }
        }

        public void ProcessFieldGuideClick(double x, double y)
        {
            double? minDistX = null;
            double? minDistY = null;

            this.SuspendAlignMode = true;

            FieldGuide fieldGuide = GetFieldGuide(x, y, 0.1, out minDistX, out minDistY);

            if (fieldGuide is null)
            {
                AddFieldGuide(x, y);
            }

            else
            {
                if (minDistX.HasValue && !minDistY.HasValue)
                {
                    AddFieldGuide(-100000, fieldGuide.Y);
                }

                else if (minDistY.HasValue && !minDistX.HasValue)
                {
                    AddFieldGuide(fieldGuide.X, -100000);
                }

                DeleteFieldGuide(fieldGuide.Guid);
            }
        }

        public void SetLineOpacity(double lineOpacity)
        {
            this.Opacity = Math.Max(0.0, Math.Min(1.0, lineOpacity));

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                fieldGuide.SetLineOpacity(lineOpacity);
            }
        }

        public void SetLineWidth(double lineWidthInPts)
        {
            this.LineWidthInPts = lineWidthInPts;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                fieldGuide.SetLineWidth(lineWidthInPts);
            }
        }


        public void SetLineColor(Color lineColor)
        {
            this.LineColor = lineColor;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                fieldGuide.SetLineColor(lineColor);
            }
        }

        public void SetLineType(int lineStyle)
        {
            this.LineStyle = lineStyle;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                fieldGuide.SetLineType(lineStyle);
            }
        }

        public bool SnapToFieldGuide(double currX, double currY, int key, Point currCursorPosn, out Point p)
        {
            double origX = currX;
            double origY = currY;

            double? guideX = null;
            double? guideY = null;

            if (GetClosestGuides(currX, currY, out guideX, out guideY, GlobalSettings.SnapDistance, key))
            {
                currX = guideX.HasValue ? guideX.Value : origX;
                currY = guideY.HasValue ? guideY.Value : origY;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, window.VisioWindow, origX, origY, currX, currY, currCursorPosn);

                return true;
            }

            p = new Point(int.MinValue, int.MinValue);

            return false;
        }

        public bool SnapToLeftFieldGuide(double currX, double currY, int key, Point currCursorPosn, out Point p)
        {
            double origX = currX;
            double origY = currY;

            double? guideX = GetLeftGuideLineX(currX);

            if (guideX.HasValue)
            {
                currX = guideX.Value;
                
                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, window.VisioWindow, origX, origY, currX, currY, currCursorPosn);

                return true;
            }

            p = new Point(int.MinValue, int.MinValue);

            return false;
        }

        public bool SnapToRghtFieldGuide(double currX, double currY, int key, Point currCursorPosn, out Point p)
        {
            double origX = currX;
            double origY = currY;

            double? guideX = GetRghtGuideLineX(currX);

            if (guideX.HasValue)
            {
                currX = guideX.Value;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, window.VisioWindow, origX, origY, currX, currY, currCursorPosn);

                return true;
            }

            p = new Point(int.MinValue, int.MinValue);

            return false;
        }

        public bool SnapToUpprFieldGuide(double currX, double currY, int key, Point currCursorPosn, out Point p)
        {
            double origX = currX;
            double origY = currY;

            double? guideY = GetUpprGuideLineY(currY);

            if (guideY.HasValue)
            {
                currY = guideY.Value;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, window.VisioWindow, origX, origY, currX, currY, currCursorPosn);

                return true;
            }

            p = new Point(int.MinValue, int.MinValue);

            return false;
        }

        public bool SnapToLowrFieldGuide(double currX, double currY, int key, Point currCursorPosn, out Point p)
        {
            double origX = currX;
            double origY = currY;

            double? guideY = GetLowrGuideLineY(currY);

            if (guideY.HasValue)
            {
                currY = guideY.Value;

                p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, window.VisioWindow, origX, origY, currX, currY, currCursorPosn);

                return true;
            }

            p = new Point(int.MinValue, int.MinValue);

            return false;
        }

        public double? GetLeftGuideLineX(double MouseX)
        {
     
            double minXDist = double.MaxValue;
            double? minXValu = null;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                if (fieldGuide.X <= -100000)
                {
                    continue;
                }

                if (fieldGuide.X < MouseX - 0.01)
                {
                    double dist = MouseX - fieldGuide.X;

                    if (dist < minXDist)
                    {
                        minXDist = dist;
                        minXValu = fieldGuide.X;
                    }
                }
            }

            return minXValu;
        }

        public double? GetRghtGuideLineX(double MouseX)
        {
            double minXDist = double.MaxValue;
            double? minXValu = null;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                if (fieldGuide.X - 0.01 > MouseX)
                {
                    double dist = fieldGuide.X - MouseX;

                    if (dist < minXDist)
                    {
                        minXDist = dist;
                        minXValu = fieldGuide.X;
                    }
                }
            }

            return minXValu;
        }

        public double? GetUpprGuideLineY(double MouseY)
        {
            double minYDist = double.MaxValue;
            double? minYValu = null;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                if (fieldGuide.Y - 0.01 > MouseY)
                {
                    double dist = fieldGuide.Y - MouseY;

                    if (dist < minYDist)
                    {
                        minYDist = dist;
                        minYValu = fieldGuide.Y;
                    }
                }
            }

            return minYValu;
        }

        public double? GetLowrGuideLineY(double MouseY)
        {
            double minYDist = double.MaxValue;
            double? minYValu = null;

            foreach (FieldGuide fieldGuide in fieldGuides)
            {
                if (fieldGuide.Y <= -100000)
                {
                    continue;
                }

                if (fieldGuide.Y < MouseY - 0.01)
                {
                    double dist = MouseY - fieldGuide.Y;

                    if (dist < minYDist)
                    {
                        minYDist = dist;
                        minYValu = fieldGuide.Y;
                    }
                }
            }

            return minYValu;
        }

        public void GetBoundingGuides(double x, double y, out double? xMin, out double? xMax, out double? yMin, out double? yMax)
        {
            xMin = GetLeftGuideLineX(x);

            xMax = GetRghtGuideLineX(x);

            yMax = GetUpprGuideLineY(y);

            yMin = GetLowrGuideLineY(y);
        }
    }
}
