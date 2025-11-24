//-------------------------------------------------------------------------------//
// <copyright file="VisioTestAndDebug.cs" company="Bruun Estimating, LLC">       // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Test_and_Debug
{
    using FloorMaterialEstimator;
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AxMicrosoft.Office.Interop.VisOcx;
    using Visio = Microsoft.Office.Interop.Visio;
 
    public class VisioTestAndDebug
    {
        private CanvasManager canvasManager;

        private Visio.Window vsoWindow
        {
            get
            {
                return canvasManager.VsoWindow;
            }
        }

        public void ViewShapeCharacteristics(Visio.Shape visioShape)
        {
           Visio.Path path = visioShape.PathsLocal[1];

            

            Visio.Curve curve = path[1];

            
            double start = curve.Start;
            double end = curve.End;

            Array pointsArray;

            curve.Points(0.001, out pointsArray);
        }

        public VisioTestAndDebug(CanvasManager canvasManager)
        {
            this.canvasManager = canvasManager;

            vsoWindow.SelectionChanged += VsoWindow_SelectionChanged;
            vsoWindow.ViewChanged += VsoWindow_ViewChanged;
            vsoWindow.WindowChanged += VsoWindow_WindowChanged;
            vsoWindow.WindowActivated += VsoWindow_WindowActivated;
            vsoWindow.WindowTurnedToPage += VsoWindow_WindowTurnedToPage;
        }

        public void ViewLineCoordinates(GraphicsLine line)
        {
            line.GetLineEndpoints();
        }

        private void VsoWindow_WindowChanged(Visio.Window Window)
        {
            
        }

        private void VsoWindow_ViewChanged(Visio.Window Window)
        {
            
        }

        private void VsoWindow_SelectionChanged(Visio.Window Window)
        {
            
        }

        private void VsoWindow_WindowTurnedToPage(Visio.Window Window)
        {
            
        }

        private void VsoWindow_WindowActivated(Visio.Window Window)
        {
            
        }
    }
}
