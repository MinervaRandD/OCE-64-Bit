//-------------------------------------------------------------------------------//
// <copyright file="KeyPressEventManager.cs" company="Bruun Estimating, LLC">    // 
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
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Test_and_Debug;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CanvasManager
    {
        private void VsoWindow_KeyPress(int KeyAscii, ref bool CancelDefault)
        {
            CancelDefault = true;

            if (KeyAscii == 99)
            {
                ProcessCompletePolyLine();

                return;
            }

            if (KeyAscii == 96 || KeyAscii == 8)
            {
                ProcessDeleteBuildingLine();

                return;
            }

            if (KeyAscii == 122)
            {
                ProcessSwitchZeroLine();

                return;
            }

            if (KeyAscii == 115)
            {
                ProcessSeamAndCutArea();

                return;
            }

            if (KeyAscii == 116)
            {
                Polyline polyline = Test_and_Debug.TestPolyLines.TestPolyline01(this);

                polyline.Perimeter.PerimeterLines.ForEach(l => this.ShapeDict.Add(l.NameID, l));
                
            }
        }

        /// <summary>
        /// Complete the currently building shape. Currently only applies to polyline.
        /// </summary>
        private void ProcessCompletePolyLine()
        {
            if (this.DrawingMode == DrawingMode.Default)
            {
                return;
            }

            if (!this.DrawingShape)
            {
                return;
            }

            if (this.DrawingMode != DrawingMode.Polyline)
            {
                return;
            }

            if (buildingPolyline == null)
            {
                return;
            }

            if (buildingPolyline.LineCount <= 0)
            {
                return;
            }

            if (buildingPolyline.buildingLine.Length > 0.1)
            {
                // This occurs when there has been a previous click and the mouse has
                // been moved away from the location where the click occured.

                // Complete the current line draw before completing to the end point.

                Coordinate lineCoord = buildingPolyline.buildingLine.GetLineEndpoint();

                ContinuePolylineDraw(lineCoord.X, lineCoord.Y);
            }

            else
            {
                // This occurs when there has been a previous click, but the mouse hasn't
                // been moved away from the last click point.
            }

            CompletePolylineDraw();

            buildingPolyline = null;
            this.DrawingShape = false;
        }

        /// <summary>
        /// Remove the last line created during drawing. Currently only applies to polyline.
        /// </summary>
        private void ProcessDeleteBuildingLine()
        {
            if (this.DrawingMode != DrawingMode.Polyline || !this.DrawingShape || buildingPolyline == null)
            {
                return;
            }

            if (buildingPolyline.buildingLine == null)
            {
                buildingPolyline = null;
                this.DrawingShape = false;

                return;
            }

            GraphicsLine lastLine = buildingPolyline.RemoveLastLine();

            if (buildingPolyline.LineCount <= 0)
            {
                this.buildingPolyline = null;
                this.DrawingShape = false;
            }

            return;
        }

        private void ProcessSwitchZeroLine()
        {
            if (this.DrawingMode != DrawingMode.Polyline || !this.DrawingShape || buildingPolyline == null)
            {
                return;
            }

            if (buildingPolyline.buildingLine == null)
            {
                return;
            }

            bool currZeroLineValue = buildingPolyline.buildingLine.IsZeroLine;

            if (currZeroLineValue)
            {
                buildingPolyline.buildingLine.SetBaseLineStyle();
                buildingPolyline.buildingLine.IsZeroLine = false;

                return;
            }

            else
            {
                buildingPolyline.buildingLine.SetBaseLineStyle(CanvasManager.ZeroLineStyleFormula);
                buildingPolyline.buildingLine.IsZeroLine = true;

                return;
            }

        }

        private void ProcessSeamAndCutArea()
        {
            Visio.Selection selection = VsoWindow.Selection;

            if (selection == null)
            {
                return;
            }

            if (selection.Count != 1)
            {
                return;
            }

            Visio.Shape visioShape = selection.PrimaryItem;

            if (!ShapeDict.ContainsKey(visioShape.NameID))
            {
                return;
            }

            Shape shape = ShapeDict[visioShape.NameID];

            if (shape.ShapeType != ShapeType.Line)
            {
                return;
            }

            if (((GraphicsLine) shape).LineType != LineType.PerimeterLine)
            {
                return;
            }

            PerimeterLine line = (PerimeterLine)shape;

            Perimeter perimeter = line.Perimeter;

            if (perimeter == null)
            {
                return;
            }

            AreaShape area = perimeter.AreaShape;

            if (area == null)
            {
                return;
            }

            int lineIndex = perimeter.GetLineIndex(line);

            area.DoSeemsAndCuts(linePallet.lineTypeDict.Values.First(), lineIndex, 1);
        }

    }
}
