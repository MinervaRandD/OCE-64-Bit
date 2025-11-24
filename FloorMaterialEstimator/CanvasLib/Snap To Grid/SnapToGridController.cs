
namespace CanvasLib.Snap_To_Grid
{
    using SettingsLib;
    using Utilities;
    using Graphics;

    using Visio = Microsoft.Office.Interop.Visio;
    using AxMicrosoft = AxMicrosoft.Office.Interop.VisOcx;
    using System;
    using System.Drawing;
    using CanvasLib.Markers_and_Guides;
    using System.Diagnostics;
    using System.Runtime.Remoting.Messaging;
    using Microsoft.Office.Interop.Visio;

    public class SnapToGridController
    {
        private Visio.Window vsoWindow;
        private AxMicrosoft.AxDrawingControl axDrawingControl;
        private FieldGuideController fieldGuideController;

        public SnapToGridController(AxMicrosoft.AxDrawingControl axDrawingControl, Visio.Window vsoWindow, FieldGuideController fieldGuideController)
        {
            this.axDrawingControl = axDrawingControl;
            this.vsoWindow = vsoWindow;
            this.fieldGuideController = fieldGuideController;
        }

        double lastX = 0;
        double lastY = 0;

        public Point SnapToGrid(
            double baseX, // Base line x coordinate to snap to
            double baseY, // Base line y coordinate to snap to
            ref double currX, // Current cursor position x (in local coordinates / inches)
            ref double currY, // Current cursor position y (in local coordinates / inches)
            Point currCursorPosn,// Cursor position in pixels
            bool fieldGuideSelected,
            ref string returnInfo)
        {
           
            double origX = currX;
            double origY = currY;

            double? guideX = null;
            double? guideY = null;

            if (fieldGuideSelected)
            {
                if (fieldGuideController.GetClosestGuides(currX, currY, out guideX, out guideY, GlobalSettings.SnapDistance, 0))
                {
                    if (guideX != null)
                    {
                        currX = guideX.Value;
                    }

                    if (guideY != null)
                    {
                        currY = guideY.Value;
                    }

                    return VisioInterop.MapVisioToScreenCoords(axDrawingControl, vsoWindow, origX, origY, currX, currY, currCursorPosn);
                }
            }

            if (!SnapToGridCalc(baseX, baseY, ref currX, ref currY, GlobalSettings.SnapResolutionInDegrees))
            {
                returnInfo = "    return currCursorPosn";

                lastX = currX;
                lastY = currY;

                return currCursorPosn;


            }

            Point p = VisioInterop.MapVisioToScreenCoords(axDrawingControl, vsoWindow, origX, origY, currX, currY, currCursorPosn);

            returnInfo = "    return MapVisioToScreenCoords";

            lastX = currX;
            lastY = currY;
            return p;
        }


        double dx = 0;
        double dy = 0;

        public bool SnapToGridCalc(double guideX, double guideY, ref double currX, ref double currY, double snapResolutionInDegrees)
        {
            bool isDebug = KeyboardUtils.DKeyPressed;

            bool isWithinXSnapRange = withinSnapRange(guideY, currY);
            bool isWithinYSnapRange = withinSnapRange(guideX, currX);

            if (!isWithinXSnapRange && !isWithinYSnapRange)
            {
                return false;
            }

            dx = Math.Abs(lastX - currX);
            dy = Math.Abs(lastY - currY);

            bool result = false;

            if (isWithinXSnapRange && isWithinYSnapRange)
            {
                if (Math.Abs(guideY - currY) < Math.Abs(guideX - currX))
                {
                    result = processSnapToXGuide(guideY, ref currY);

                    if (result)
                    {
                        return true;
                    }

                    result = processSnapToYGuide(guideX, ref currX);

                    if (result)
                    {
                        return true;
                    }

                    return false;
                }

                else
                {
                    result = processSnapToYGuide(guideX, ref currX);

                    if (result)
                    {
                        return true;
                    }

                    result = processSnapToXGuide(guideY, ref currY);

                    if (result)
                    {
                        return true;
                    }

                    return false;
                }
            }

            if (isWithinXSnapRange)
            {
                result = processSnapToXGuide(guideY, ref currY);

                return result;
            }

            if (isWithinYSnapRange)
            {
                result = processSnapToYGuide(guideX, ref currX);

                return result;
            }

            return false;
        }
          
        private bool processSnapToXGuide(double guideY, ref double currY)
        { 
            if (dy < 0.1)
            {
                currY = guideY;

                return true;
            }

            double moveAngle = 0;

            moveAngle = MathUtils.C_180_over_pi * Math.Atan2(dx, dy);

            if (onGuide(guideY, currY) && movingInXDirection(moveAngle))
            {
                currY = guideY;

                return true;
            }

            if (onGuide(guideY, currY))
            {
                return false;
            }

            if (movingTowardsGuid(guideY, lastY, currY))
            {
                currY = guideY;

                return true;
            }
            
            return false;
        }
         
        private bool processSnapToYGuide(double guideX, ref double currX)
        {
            if (dx < 0.1)
            {
                currX = guideX;

                return true;
            }

            double moveAngle = 0;

            moveAngle = MathUtils.C_180_over_pi * Math.Atan2(dx, dy);

            if (onGuide(guideX, currX) && movingInYDirection(moveAngle))
            {
                currX = guideX;

                return true;
            }

            if (onGuide(guideX, currX))
            {
                return false;
            }

            if (movingTowardsGuid(guideX, lastX, currX))
            {
                currX = guideX;

                return true;
            }

            return false;
        }

        bool onGuide(double guide, double curr) => Math.Abs(guide - curr) <= 1.0e-4;

        bool movingInXDirection(double moveAngle)
        {
            double x = Math.Abs(Math.Abs(moveAngle) - 90.0);

            return x <= GlobalSettings.SnapResolutionInDegrees;
        }

        bool movingInYDirection(double moveAngle)
        {
            double x1 = Math.Abs(moveAngle);
            double x2 = Math.Abs(x1 - 180.0);

            return Math.Min(x1, x2) <= GlobalSettings.SnapResolutionInDegrees;
        }

        bool withinSnapRange(double guide, double curr) => Math.Abs(guide - curr) <= GlobalSettings.SnapDistance;

        bool movingTowardsGuid(double guide, double last, double curr) => Math.Abs(guide - last) > Math.Abs(guide - curr);

    }
}
