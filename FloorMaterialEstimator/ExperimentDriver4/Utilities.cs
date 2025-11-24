using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using Visio = Microsoft.Office.Interop.Visio;

namespace ExperimentDriver4
{
    public static class Utilities
    {
        public static Point MapVisioToScreenCoords(
            AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl control,
            Visio.Window refWin,
            double visioX,
            double visioY,
            bool rulers = false)
        {
            int rulerOffset = 0;

            if (rulers)
            {
                rulerOffset = 17;
            }

            Point p1 = control.PointToScreen(new Point(0, 0));

            int windowsX = 0;
            int windowsY = 0;

            MapVisioToWindows(refWin, visioX, visioY, rulerOffset, out windowsX, out windowsY);

            return new Point(p1.X + windowsX + rulerOffset, p1.Y + windowsY + rulerOffset);
        }

        public static void MapVisioToWindows(
            Visio.Window refWin,
            double visioX,
            double visioY,
            int rulerOffset,
            out int windowsX,
            out int windowsY)
        {
            // The drawing control object must be valid.
            if (refWin == null)
            {
                // Throw a meaningful error.
                throw new ArgumentNullException("window");
            }

            windowsX = 0;
            windowsY = 0;

            double visioLeft;
            double visioTop;
            double visioWidth;
            double visioHeight;
            int pixelLeft;
            int pixelTop;
            int pixelWidth;
            int pixelHeight;

            // Get the window coordinates in Visio units.
            refWin.GetViewRect(
                out visioLeft, out visioTop, out visioWidth, out visioHeight);

            // Get the window coordinates in pixels.
            refWin.GetWindowRect(out pixelLeft, out pixelTop, out pixelWidth, out pixelHeight);

            // GetWindowRect does not take the scrollbar sizes into account
            pixelWidth -= System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + rulerOffset;
            pixelHeight -= System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight + 37 + rulerOffset;

            // Convert the X coordinate by using pixels per inch from the
            // width values.
            windowsX = (int)(pixelLeft + ((pixelWidth / visioWidth) * (visioX - visioLeft)));

            // Convert the Y coordinate by using pixels per inch from the
            // height values and transform from a top-left origin (windows
            // coordinates) to a bottom-left origin (Visio coordinates).
            windowsY = (int)(pixelTop + ((pixelHeight / visioHeight) * (visioTop - visioY)));

            //return new System.Drawing.Point(windowsX, windowsY);
        }

        public static void MapWindowsToVisio(
            Visio.Window referenceWindow,
            int windowsX,
            int windowsY,
            int additionalMarginX,
            int additionalMarginY,
            out double visioX,
            out double visioY)
        {

            // The drawing control object must be valid.
            if (referenceWindow == null)
            {

                // Throw a meaningful error.
                throw new ArgumentNullException("referenceWindow");
            }

            visioX = 0;
            visioY = 0;
            double visioLeft;
            double visioTop;
            double visioWidth;
            double visioHeight;
            int pixelLeft;
            int pixelTop;
            int pixelWidth;
            int pixelHeight;

            // Get the window coordinates in Visio units.
            referenceWindow.GetViewRect(out visioLeft, out visioTop,
            out visioWidth, out visioHeight);

            // Get the window coordinates in pixels.
            referenceWindow.GetWindowRect(out pixelLeft, out pixelTop,
            out pixelWidth, out pixelHeight);

            if (ExperimentDriver4BaseForm.debug)
            {
                int y = 1;
            }
            // GetWindowRect does not take the scrollbar sizes into account
            pixelWidth -=
                    System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + additionalMarginX;
            pixelHeight -=
                    System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight + additionalMarginY;


            // Convert the X coordinate by using pixels per inch from the
            // width values.
            visioX = visioLeft + ((visioWidth * (double)(windowsX)) / (double)(pixelWidth+8));

            // Convert the Y coordinate by using pixels per inch from the
            // height values and transform from a top-left origin (windows
            // coordinates) to a bottom-left origin (Visio coordinates).

            double visioInches = (visioHeight * (double)(windowsY)) / (double)pixelHeight;

            visioY = visioTop - visioInches;

            if (ExperimentDriver4BaseForm.debug)
            {
                int y = 1;
            }
        }

    }
}
