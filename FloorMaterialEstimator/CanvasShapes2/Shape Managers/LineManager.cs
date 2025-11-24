//-------------------------------------------------------------------------------//
// <copyright file="LineManager.cs" company="Bruun Estimating, LLC">             // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace CanvasShapes
{
    using System;

    using Globals;
    using Graphics;
    using Geometry;

    public class LineManager
    {
        private GraphicsDirectedLine mostRecentlyCompletedDoubleLine = null;

        public GraphicsDirectedLine MostRecentlyCompletedDoubleLine
        {
            get
            {
                return mostRecentlyCompletedDoubleLine;
            }

            set
            {
                if (value is null)
                {
                    SystemState.LineDuplicateMode = false;
                }

                else
                {
                    this.BaseForm.btnLayoutLineDuplicate.Enabled = true;
                }

                mostRecentlyCompletedDoubleLine = value;
            }
        }

        public DesignState DesignState => SystemState.DesignState;

        public SeamMode SeamMode => SystemState.SeamMode;

        internal void ZoomDelta(double zoomDelta)
        {
            VsoWindow.Zoom = Math.Max(0.05, Math.Min(9.99, VsoWindow.Zoom + zoomDelta));
        }

        internal void Zoom(double zoomAmount)
        {
            VsoWindow.Zoom = Math.Max(0.05, Math.Min(9.99, zoomAmount));
        }

        private void processLineDrawModeMouseMove(double x, double y, ref bool CancelDefault)
        {
            

            if (SystemState.DrawingMode == DrawingMode.Line1X)
            {
                if (Coordinate.IsNullCoordinate(Line1XModeBaseCoord))
                {
                    return;
                }

                double x1 = Line1XModeBaseCoord.X;
                double y1 = Line1XModeBaseCoord.Y;

                snapToAxis(x1, y1, ref x, ref y);
            }

            if (SystemState.DrawingMode == DrawingMode.Line2X)
            {
                if (CurrentPage.CurrentGuide is null)
                {
                    return;
                }
               

                double x1 = CurrentPage.CurrentGuide.X;
                double y1 = CurrentPage.CurrentGuide.Y;

                snapToAxis(x1, y1, ref x, ref y);
            }
        }


        public void Add(CanvasDirectedLine line)
        {
            CurrentPage.AddToDirectedLineDict(line);
        }

        //internal void UpdateSeamSelections(UCSeamPaletteElement ucSeam)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void DeleteSeamingTool()
        //{
        //    if (_seamingTool is null)
        //    {
        //        return;
        //    }

        //    _seamingTool.Delete();

        //    _seamingTool = null;
        //}
    }
}
