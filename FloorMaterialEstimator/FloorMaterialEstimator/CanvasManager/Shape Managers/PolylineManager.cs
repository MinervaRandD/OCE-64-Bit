//-------------------------------------------------------------------------------//
// <copyright file="CanvasManagerPolylineManger.cs"                              //
//            company="Bruun Estimating, LLC">                                   // 
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

    using FloorMaterialEstimator;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Test_and_Debug;
    using FloorMaterialEstimator.Utilities;

    public partial class CanvasManager
    {
        Polyline buildingPolyline = null;

        public Polyline BuildingPolyline
        {
            get
            {
                return this.buildingPolyline;
            }

            set
            {
                this.buildingPolyline = value;
            }
        }

        private void InitializePolylineDraw(double x, double y)
        {
            Debug.WriteLineIf((Program.Debug & DebugCond.VisioPolyLineDraw) != 0, "Initializing polyline at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + ")");

            if (BuildingPolyline != null)
            {
                // Defensive. Should never initialize polyline when building polyline exists.

                throw new NotImplementedException();
            }

            BuildingPolyline = new Polyline(this);

            selectedFinishType.areaShapeList.Add(BuildingPolyline);

            BuildingPolyline.BuildStatus = AreaShapeBuildStatus.Building;

            PerimeterLine line = BuildingPolyline.AddInitialLine(x, y);

            this.ShapeDict.Add(line.NameID, line);

            DrawingShape = true;

            VsoWindow.DeselectAll();
        }

        public void ContinuePolylineDraw(double x, double y)
        {
            Debug.WriteLineIf((Program.Debug & DebugCond.VisioPolyLineDraw) != 0, "Continuing polyline draw at (" + x.ToString("0.0000") + "," + y.ToString("0.0000") + ")");

            if (BuildingPolyline == null)
            {
                throw new NotImplementedException();
            }

            PerimeterLine line = BuildingPolyline.AddExtendingLine();

            this.ShapeDict.Add(line.NameID, line);

            this.VsoWindow.DeselectAll();
        }

        public void CompletePolylineDraw()
        {
            Debug.WriteLineIf((Program.Debug & DebugCond.VisioPolyLineDraw) != 0, "Completing polyline");

            Debug.Assert(BuildingPolyline != null, "Attempt to complete polyline on null building polyline");

            Debug.Assert(BuildingPolyline.LineCount >= 3, "Attempt to complete polyline on too few lines.");

            Coordinate frstCoord = BuildingPolyline.GetFirstCoordinate();

            GraphicsLine lastLine = BuildingPolyline.LastLine;

            lastLine.SetLineEndPoint(frstCoord);

            GraphicsLine frstLine = BuildingPolyline.FirstLine;

            // The following is necessary to correct for snap to grid

            frstLine.SetLineStartPoint(lastLine.GetLineEndpoint());

            BuildingPolyline.Perimeter.ValidateConsistentPerimeter();

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Normal)
            {
                Graphics.SetLineBeginShape(frstLine, 0, 0);
            }

            BuildingPolyline.CreateInternalAreaShape();

            if (baseForm.LineAreaMode == LineArea.Area)
            {
                BuildingPolyline.Perimeter.SetCompletedLineWidth();
            }

            BuildingPolyline.BuildStatus = AreaShapeBuildStatus.Completed;

            this.BuildingPolyline = null;

            this.DrawingShape = false;

           this.VsoWindow.DeselectAll();
        }

    }
}
