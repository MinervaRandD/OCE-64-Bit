//-------------------------------------------------------------------------------//
// <copyright file="TestPolyLines.cs" company="Bruun Estimating, LLC">           // 
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Finish_Controls;

    public static class TestPolyLines
    {
        public static Polyline TestPolyline01(CanvasManager canvasManager)
        {
            List<Coordinate> coordList = new List<Coordinate>()
            {
                new Coordinate(1.0, 11.0),
                new Coordinate(3.0, 11.0),
                new Coordinate(5.0, 8.0),
                new Coordinate(7.0, 11.0),
                new Coordinate(7.0, 5.0),
                new Coordinate(1.0, 5.0)
            };

            UCLine ucLine = canvasManager.linePallet.lineTypeDict.Values.First();

            Polyline polyline = new Polyline(canvasManager);

            Perimeter perimeter = polyline.Perimeter;

            int count = coordList.Count;

            for (int i = 0; i < count ; i++)
            {
                Coordinate coord1 = coordList[i];
                Coordinate coord2 = coordList[(i + 1) % count];

                double x1 = coord1.X;
                double y1 = coord1.Y;

                double x2 = coord2.X;
                double y2 = coord2.Y;

                PerimeterLine perimeterLine = new PerimeterLine(x1, y1, x2, y2, perimeter, ucLine, canvasManager);

                polyline.Perimeter.AddLine(perimeterLine);
            }

            perimeter.Draw();

            polyline.CreateInternalAreaShape();
               
            return polyline;
        }
    }
}
