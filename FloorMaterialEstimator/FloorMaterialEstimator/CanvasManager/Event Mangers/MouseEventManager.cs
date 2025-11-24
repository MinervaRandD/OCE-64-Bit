//-------------------------------------------------------------------------------//
// <copyright file="MouseEventManager.cs" company="Bruun Estimating, LLC">       // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//


namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using FloorMaterialEstimator;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Test_and_Debug;
    using FloorMaterialEstimator.Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CanvasManager
    {

        double mouseDownX = 0;
        double mouseDownY = 0;

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            mouseDownX = x;
            mouseDownY = y;

            Debug.WriteLineIf((Program.Debug & DebugCond.VisioMouseEvents) != 0, "Mouse down event at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + "), button = " + Button + ", KeyButtonState = " + KeyButtonState);

            if (this.tapeMeasure != null)
            {
                RemoveTapeMeasure();
            }

            if (this.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!this.DrawingShape)
            {
                if (this.DrawingMode == DrawingMode.Line)
                {
                    InitializeLineDraw(x, y);

                    CancelDefault = true;

                    return;
                }

                if (this.DrawingMode == DrawingMode.Rectangle)
                {
                    this.InitializeRectangleDraw(x, y);

                    return;
                }

                if (this.DrawingMode == DrawingMode.Polyline)
                {
                    CancelDefault = true;

                    return;
                }

                if (this.DrawingMode == DrawingMode.ScaleLine)
                {
                    this.InitializeScaleLineDraw(x, y);

                    CancelDefault = true;

                    return;
                }

                if (this.DrawingMode == DrawingMode.TapeMeasure)
                {
                    InitializeTapeMeasureDraw(x, y);

                    return;
                }
            }

            if (this.DrawingMode != DrawingMode.Polyline)
            {
                return;
            }
        }

        private void VsoWindow_MouseUp(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            Debug.WriteLineIf((Program.Debug & DebugCond.VisioMouseEvents) != 0, "Mouse up event at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + "), button = " + button + ", KeyButtonState = " + keyButtonState);

            if (MathUtils.H0Dist(this.mouseDownX, this.mouseDownY, x, y) <= 0.1)
            {
                VsoWindow_MouseClick(button, keyButtonState, x, y, ref cancelDefault);

                return;
            }

            if (this.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!this.DrawingShape)
            {
                return;
            }

            if (this.DrawingMode == DrawingMode.Line)
            {
                CompleteLineDraw(x, y);

                cancelDefault = true;

                return;
            }

            if (this.DrawingMode == DrawingMode.Rectangle)
            {
                CompleteRectangleDraw(x, y);

                return;
            }

            if (this.DrawingMode == DrawingMode.Polyline)
            {
                return;
            }

            if (this.DrawingMode == DrawingMode.ScaleLine)
            {
                CompleteScaleLineDraw(x, y);

                return;
            }

            if (this.DrawingMode == DrawingMode.TapeMeasure)
            {
                CompleteTapeMeasureDraw(x, y);

                return;
            }
        }

        private void VsoWindow_MouseClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            Debug.WriteLineIf((Program.Debug & DebugCond.VisioMouseEvents) != 0, "Mouse click event at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + "), button = " + button + ", KeyButtonState = " + keyButtonState);

            if (this.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (this.DrawingMode == DrawingMode.Polyline)
            {
                if (!this.DrawingShape)
                {
                    if (BuildingPolyline != null)
                    {
                        throw new NotImplementedException();
                    }

                    // If here, then we are starting a new polyline.

                    InitializePolylineDraw(x, y);

                    DrawingShape = true;

                    cancelDefault = true;

                    return;
                }

                else
                {
                    if (BuildingPolyline == null)
                    {
                        throw new NotImplementedException();
                    }

                    if (BuildingPolyline.buildingLine == null)
                    {
                        throw new NotImplementedException();
                    }

                    Visio.Shape shape = BuildingPolyline.buildingLine.VisioShape;


                    if (BuildingPolyline.buildingLine.Length <= clickResolution)
                    {
                        // Ignore this click. The line must have positive length

                        return;
                    }

                    // Check to see if this click is on or near the beginning of the perimeter.

                    Coordinate frstCoord = this.BuildingPolyline.GetFirstCoordinate();

                    if (MathUtils.H0Dist(frstCoord, new Coordinate(x, y)) <= clickResolution)
                    {
                        // Complete the drawing of the polyline

                        CompletePolylineDraw();

                        return;
                    }

                    else
                    {
                        ContinuePolylineDraw(x, y);

                        cancelDefault = true;
                    }

                }

            }

        }

        private void VsoWindow_MouseMove(int button, int keyButtonState, double x, double y, ref bool CancelDefault)
        {
            Debug.WriteLineIf((Program.Debug & DebugCond.VisioMouseEvents) != 0, "Mouse move event at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + "), button = " + button + ", KeyButtonState = " + keyButtonState + ", Drawing Mode = " + this.DrawingMode.ToString() + ", drawingShape = " + DrawingShape.ToString());

            baseForm.Cursor = new Cursor(Cursor.Current.Handle);
            
            if (GlobalSettings.OperatingModeSetting == OperatingMode.Development)
            {
                double cursorGridPosnX = x / this.GridScale - 2.0 * this.GridOffset;
                double cursorGridPosnY = y / this.GridScale - 2.0 * this.GridOffset;

                baseForm.lblCursorPosition.Text = "(" + cursorGridPosnX.ToString("0.00").PadLeft(5) + ", " + cursorGridPosnY.ToString("0.00").PadLeft(5) + ")";
            }

            if (this.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!DrawingShape)
            {
                return;
            }

            if (this.DrawingMode == DrawingMode.Line)
            {
                if (buildingLine == null)
                {
                    return;
                }

                buildingLine.SetLineEndPoint(x, y);

                //CancelDefault = true;

                return;
            }

            if (this.DrawingMode == DrawingMode.Rectangle)
            {
                if (this.buildingRectangle == null)
                {
                    return;
                }

                return;
            }

            if (this.DrawingMode == DrawingMode.Polyline)
            {
                if (this.BuildingPolyline == null)
                {
                    return;
                }

                if (this.BuildingPolyline.buildingLine == null)
                {
                    return;
                }

                this.BuildingPolyline.buildingLine.SetLineEndPoint(x, y);

                this.baseForm.SetLineLengthStatusStripDisplay(CurrentPage.DrawingScale * BuildingPolyline.buildingLine.Length);

                return;
            }

            if (this.DrawingMode == DrawingMode.ScaleLine)
            {
                if (Control.MouseButtons != MouseButtons.Left)
                {
                    return;
                }

                if (this.scaleLine == null)
                {
                    // Defensive. Should never be here.

                    return;
                }

                this.scaleLine.SetLineEndPoint(x, y);

                return;
            }

            if (this.DrawingMode == DrawingMode.TapeMeasure)
            {
                Debug.Assert(tapeMeasure != null, "Invalid null tape measure in mouse move.");

                if (Control.MouseButtons != MouseButtons.Left)
                {
                    return;
                }

                if (this.scaleLine == null)
                {
                    // Defensive. Should never be here.

                    return;
                }

                this.tapeMeasure.SetLineEndPoint(x, y);

                return;
            }
        }

    }
}
