using AxMicrosoft.Office.Interop.VisOcx;
using Globals;
using Microsoft.Office.Interop.Visio;
using NetTopologySuite.Algorithm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    using Visio = Microsoft.Office.Interop.Visio;

    public static class VisioInteropDebugger
    {
        public static string DiagnoseDeleteBlockers(Visio.Shape shp)
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine($"Shape: {shp.NameU} (ID {shp.ID})");

            // 1) Shape-level protection
            bool lockDelete = GetBoolCell(shp, "Protection.LockDelete");
            bool lockSelect = GetBoolCell(shp, "Protection.LockSelect");
            bool lockFromGroup = GetBoolCell(shp, "Protection.LockFromGroupFormat");
            report.AppendLine($"- Shape Protection: LockDelete={lockDelete}, LockSelect={lockSelect}, LockFromGroupFormat={lockFromGroup}");

            // 2) Layer locks
            if (shp.LayerCount > 0)
            {
                for (short i = 1; i <= shp.LayerCount; i++)
                {
                    Visio.Layer layer = shp.Layer[i];
                    bool layerLocked = IsLayerLocked(layer);   // 0 = unlocked, -1 (True) = locked
                    report.AppendLine($"- Layer: {layer.Name} Lock={layerLocked}");
                }
            }
            else
            {
                report.AppendLine($"- Layer: (none)");
            }

            // 3) Group ancestry
            Visio.Shape parent = shp.ContainingShape;
            if (parent != null)
            {
                report.AppendLine($"- Containing group: {parent.NameU} (ID {parent.ID})");
                bool groupLockDelete = GetBoolCell(parent, "Protection.LockDelete");
                bool groupLockFromGroup = GetBoolCell(parent, "Protection.LockFromGroupFormat");
                report.AppendLine($"  Group Protection: LockDelete={groupLockDelete}, LockFromGroupFormat={groupLockFromGroup}");
            }
            else
            {
                report.AppendLine($"- Containing group: (none)");
            }

            // 4) Container membership (UI is most reliable, but we can hint)
            // NOTE: Membership checks vary by Visio version; often easiest to verify via UI:
            // Right-click the shape → Container → Remove from Container (enabled?) 
            report.AppendLine("- Container: If right-click → Container → Remove from Container is enabled, membership may be locked.");

            // 5) Document/Page protection
            Visio.Document doc = shp.Document;
            report.AppendLine($"- Document Protection flags: {(int)doc.Protection}");
            Visio.Page page = shp.ContainingPage;
            report.AppendLine($"- Page: {page.Name}");

            return report.ToString();
        }

        public static void UnlockForDelete(Visio.Shape shp)
        {
            // a) Unlock shape protection
            SetBoolCell(shp, "Protection.LockSelect", false);
            SetBoolCell(shp, "Protection.LockDelete", false);
            SetBoolCell(shp, "Protection.LockFromGroupFormat", false);

            // b) Unlock ancestor group(s) if any
            var ancestor = shp.ContainingShape;
            while (ancestor != null)
            {
                SetBoolCell(ancestor, "Protection.LockDelete", false);
                SetBoolCell(ancestor, "Protection.LockFromGroupFormat", false);
                ancestor = ancestor.ContainingShape;
            }

            // c) Unlock all layers this shape belongs to
            for (short i = 1; i <= shp.LayerCount; i++)
            {
                Visio.Layer layer = shp.Layer[i];
                if (IsLayerLocked(layer)) UnlockLayer(layer);
            }

            // d) Last resort: temporarily unprotect the document (optional; comment out if undesired)
            // shp.Document.Protection = Visio.VisProtectionCodes.visProtectNone;
        }

        private static bool GetBoolCell(Visio.Shape s, string cellU)
        {
            try
            {
                if (s.CellExistsU[cellU, 0] != 0)
                    return s.CellsU[cellU].ResultIU != 0.0;
            }
            catch { }
            return false;
        }

        private static void SetBoolCell(Visio.Shape s, string cellU, bool value)
        {
            try
            {
                if (s.CellExistsU[cellU, 0] != 0)
                    s.CellsU[cellU].FormulaU = value ? "1" : "0";
            }
            catch { /* ignore if cell not present */ }
        }

        private static bool IsLayerLocked(Visio.Layer layer)
        {
            // Nonzero means locked
            return layer.CellsC[(short)Visio.VisCellIndices.visLayerLock].ResultIU != 0.0;
        }

        private static void UnlockLayer(Visio.Layer layer)
        {
            layer.CellsC[(short)Visio.VisCellIndices.visLayerLock].FormulaU = "0";
        }
       
        public static string ContainersReport(Visio.Shape shp)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("[Containers owning this shape]");

            Visio.Selection owners = null;
            try
            {
                // ownersIds is a SAFEARRAY (boxed as System.Array)
                object ownersIdsObj = shp.MemberOfContainers;
                var ownersIds = ownersIdsObj as System.Array;

                if (ownersIds == null || ownersIds.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No containers own this shape.");
                }
                else
                {
                    var page = shp.ContainingPage;
                    for (int i = 0; i < ownersIds.Length; i++)
                    {
                        // IDs come back as boxed numeric types; normalize to short/int
                        int id = Convert.ToInt32(ownersIds.GetValue(i));
                        Visio.Shape container = page.Shapes.ItemFromID[id];

                        // Check lock via ContainerProperties (if it’s a real container)
                        bool hasCP = false;
                        try { var _ = container.ContainerProperties; hasCP = true; } catch { }

                        bool lockedMembership = false;
                        if (hasCP)
                        {
                            try { lockedMembership = container.ContainerProperties.LockMembership; } catch { }
                            // Some stencils also use a user cell:
                            try
                            {
                                if (container.CellExistsU["User.msvSDContainerLocked", 0] != 0)
                                    lockedMembership |= container.CellsU["User.msvSDContainerLocked"].ResultIU != 0.0;
                            }
                            catch { }
                        }

                        System.Diagnostics.Debug.WriteLine(
                            $"Container '{container.NameU}' (ID {container.ID}) LockMembership={lockedMembership}"
                        );
                    }
                }

            }
            catch
            {
                sb.AppendLine("  (API not available on this Visio version)");
                return sb.ToString();
            }

            if (owners == null || owners.Count == 0)
            {
                sb.AppendLine("  (none)");
                return sb.ToString();
            }

            for (short i = 1; i <= owners.Count; i++)
            {
                var c = owners[i];
                // Common lock indicator for containers:
                // User.msvSDContainerLocked = TRUE when membership is locked
                bool locked = GetBoolCellSafe(c, "User.msvSDContainerLocked")
                              || GetBoolCellSafe(c, "User.msvSDListLocked"); // lists
                sb.AppendLine($"  Container '{c.NameU}' (ID {c.ID}) LockedMembership={locked}");
            }

            return sb.ToString();
        }

        private static bool GetBoolCellSafe(Visio.Shape s, string cellU)
        {
            try
            {
                if (s.CellExistsU[cellU, 0] != 0)
                    return s.CellsU[cellU].ResultIU != 0.0;
            }
            catch { }
            return false;
        }

        public static void TestTextCentering(Visio.Shape s)
        {

            // 1) Center text horizontally + vertically
            s.CellsU["Para.HorzAlign"].FormulaU = "1"; // 0=Left, 1=Center, 2=Right
            s.CellsU["VerticalAlign"].FormulaU = "1"; // 0=Top, 1=Middle, 2=Bottom

            // 2) Make the text block coincide with the shape bounds (prevents odd offsets)
            s.CellsU["TxtWidth"].FormulaU = "Width";
            s.CellsU["TxtHeight"].FormulaU = "Height";
            s.CellsU["TxtPinX"].FormulaU = "Width*0.5";
            s.CellsU["TxtPinY"].FormulaU = "Height*0.5";
            s.CellsU["TxtLocPinX"].FormulaU = "TxtWidth*0.5";
            s.CellsU["TxtLocPinY"].FormulaU = "TxtHeight*0.5";

            // (optional) trim margins if you want tighter centering
            s.CellsU["LeftMargin"].FormulaU = "1 pt";
            s.CellsU["RightMargin"].FormulaU = "1 pt";
            s.CellsU["TopMargin"].FormulaU = "1 pt";
            s.CellsU["BottomMargin"].FormulaU = "1 pt";
        }

        public static Visio.Shape TestCreateFixedWidthTextBox(GraphicsPage page, double leftIn, double topIn, double boxWIn, string txt)
        {
            Visio.Page visioPage = page.VisioPage;

            // Draw a small stub; we’ll let Height auto-size after text
            var s = visioPage.DrawRectangle(leftIn, topIn, leftIn + boxWIn, topIn - 0.25);

            // Text
            s.Text = txt;

            // 1) Fix the shape width
            s.CellsU["Width"].FormulaU = $"{boxWIn} in";

            // 2) Make the text block coincide with the shape’s bounds
            s.CellsU["TxtWidth"].FormulaU = "Width";
            s.CellsU["TxtHeight"].FormulaU = "Height";
            s.CellsU["TxtPinX"].FormulaU = "Width*0.5";
            s.CellsU["TxtPinY"].FormulaU = "Height*0.5";
            s.CellsU["TxtLocPinX"].FormulaU = "TxtWidth*0.5";
            s.CellsU["TxtLocPinY"].FormulaU = "TxtHeight*0.5";

            // 3) Word-wrap happens when TxtWidth is narrower than the text.
            //    Align text as you like:
            s.CellsU["Para.HorzAlign"].FormulaU = "0"; // 0=Left, 1=Center, 2=Right
            s.CellsU["VerticalAlign"].FormulaU = "0"; // 0=Top, 1=Middle, 2=Bottom

            // 4) Margins (adjust to taste)
            s.CellsU["LeftMargin"].FormulaU = "4 pt";
            s.CellsU["RightMargin"].FormulaU = "4 pt";
            s.CellsU["TopMargin"].FormulaU = "2 pt";
            s.CellsU["BottomMargin"].FormulaU = "2 pt";

            // 5) Auto-size HEIGHT from the wrapped text:
            //    TEXTHEIGHT(TheText, TxtWidth) returns the height needed for current text & width.
            //    Add top+bottom margins and clamp to a minimum height if desired.
            s.CellsU["Height"].FormulaU =
                "MAX(0.2 in, TEXTHEIGHT(TheText, TxtWidth) + TopMargin + BottomMargin)";

            // (Optional) keep the top-left corner fixed while the box grows downward:
            s.CellsU["LocPinX"].FormulaU = "0"; // pin at left edge
            s.CellsU["LocPinY"].FormulaU = "Height"; // pin at top edge
            s.CellsU["PinX"].FormulaU = $"{leftIn} in";
            s.CellsU["PinY"].FormulaU = $"{topIn} in";

            return s;
        }

        private static List<GraphicsLayer> AreaModeGraphicsSelectionLayers(GraphicsPage page)
        {
            List<GraphicsLayer> rtrnList = new List<GraphicsLayer>();

            foreach (GraphicsLayer graphicsLayer in page.GraphicsLayers)
            {
                if (graphicsLayer.Visibility == false)
                {
                    continue;
                }

                if (graphicsLayer.LayerName.StartsWith("[AreaMode:"))
                {
                    rtrnList.Add(graphicsLayer);
                }
            }

            return rtrnList;
        }

#if false

        const string OverlayLayerName = "LockOverlayIcon";
        private static Visio.Shape _icon = null; // keep a reference

        public static void EnsureOverlayLayer(Visio.Page page)
        {
            foreach (Visio.Layer l in page.Layers)
                if (l.NameU == OverlayLayerName) return;

            var layer = page.Layers.Add(OverlayLayerName);
            layer.CellsC[(short)Visio.VisCellIndices.visLayerPrint].FormulaU = "0";  // non-printing
            layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "0"; // start hidden
        }

        public static Visio.Shape CreateLockIcon(Visio.Page page)
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;               // host process folder
            string iconPath = System.IO.Path.Combine(exeDir, System.IO.Path.Combine("Img", "lockIcon.png"));

            Visio.Shape icon = page.Import(iconPath);

            icon.CellsU["LockSelect"].FormulaU = "1";
            icon.CellsU["NonPrinting"].FormulaU = "1";
            icon.CellsU["NoObjHandles"].FormulaU = "1";

            icon.BringToFront();

            return icon;
        }


        public static void CreateIconOnce(Visio.Page page, string iconPngFullPath)
        {
            if (_icon != null) return;

            EnsureOverlayLayer(page);

            // 1) “Picture box”: import PNG -> a picture SHAPE
            _icon = page.Import(iconPngFullPath);

            // 2) size and locks (so users can’t select/print it)
            _icon.CellsU["Width"].ResultIU = 0.25; // 1/4"
            _icon.CellsU["Height"].ResultIU = 0.25;
            _icon.CellsU["LockSelect"].FormulaU = "1";
            _icon.CellsU["NonPrinting"].FormulaU = "1";
            _icon.CellsU["NoObjHandles"].FormulaU = "1";

            // 3) put it on the overlay layer
            var layer = page.Layers[OverlayLayerName];
            layer.Add(_icon, (short)Visio.VisMemberAddOptions.visMemberAddDoNotExpand);

            // 4) keep it out of the way initially
            _icon.CellsU["PinX"].ResultIU = -1000; // off-page
            _icon.CellsU["PinY"].ResultIU = -1000;

            // 5) bring to front so it’s not hidden behind shapes
            _icon.BringToFront();
        }

        public static void ShowIconOnShapeCenter(Visio.Page page, Visio.Shape target)
        {
            if (target == null) { HideIcon(page); return; }

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;               // host process folder
            string iconPath = System.IO.Path.Combine(exeDir, System.IO.Path.Combine("Img", "lockIcon.png"));

            if (!System.IO.File.Exists(iconPath))
            {
                throw new FileNotFoundException(iconPath);
            }


            CreateIconOnce(page, iconPath);

            // center on target’s PinX/PinY
            double x = target.CellsU["PinX"].ResultIU;
            double y = target.CellsU["PinY"].ResultIU;

            double w = target.CellsU["Width"].ResultIU;
            double h = target.CellsU["Height"].ResultIU;

            double d = Math.Min(w, h);

            _icon.CellsU["Width"].ResultIU = d * 0.25;
            _icon.CellsU["Height"].ResultIU = d * 0.25;

            _icon.CellsU["PinX"].ResultIU = x;
            _icon.CellsU["PinY"].ResultIU = y;

            // make the overlay layer visible
            page.Layers[OverlayLayerName]
                .CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "1";
        }

        public static void HideIcon(Visio.Page page)
        {
            if (_icon == null) return;

            // simplest: hide the whole layer (only the icon lives there)
            page.Layers[OverlayLayerName]
                .CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "0";
        }

        public static void BeginFastUpdate(Visio.Application app)
        {
            if (app == null) return;

            try
            {
                
                app.DeferRecalc = -1;   // delay ShapeSheet formula recalculation
                app.ScreenUpdating = 0;  // no visual updates
                app.UndoEnabled = false;
            }
            catch { /* ignore older versions missing any property */ }
        }

        public static void EndFastUpdate(Visio.Application app)
        {
            if (app == null) return;

            try
            {
                app.DeferRecalc = 0;
                app.ScreenUpdating = -1;
                app.UndoEnabled = true;
            }
            catch { /* ignore older versions */ }

            // Force a repaint in case Visio didn’t automatically refresh
            try { app.ActiveWindow?.SelectAll(); app.ActiveWindow?.DeselectAll(); } catch { }
        }
#endif
    }

}
