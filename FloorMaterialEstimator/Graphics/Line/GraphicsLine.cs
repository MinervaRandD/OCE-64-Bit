//-------------------------------------------------------------------------------//
// <copyright file="GraphicsLine.cs" company="Bruun Estimating, LLC">            // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Utilities;

    public class GraphicsLine : Line, IGraphicsShape
    {
        public Shape Shape { get; set; }
        public GraphicsPage Page { get; set; }
        public ShapeType ShapeType { get; } = ShapeType.Line;
        public string Guid { get; set; }
        public LineCompoundType LineCompoundType { get; set; } = LineCompoundType.Single;

        public string NameID
        {
            get
            {
                if (Shape is null)
                {
                    return string.Empty;
                }

                return Shape.NameID;
            }
        }

        //public GraphicsLine(Line line): base(line.Coord1, line.Coord2)
        //{
        //    Guid = GuidMaintenance.CreateGuid(this);
        //}
        
        public void SetBaseLineColor(string visioLineColorFormula)
        {
            VisioInterop.SetBaseLineColor(this.Shape, visioLineColorFormula);
        }

        public void SetBaseLineStyle(string visioLineStyleFormula)
        {
            VisioInterop.SetBaseLineStyle(this.Shape, visioLineStyleFormula);
        }

        public void SetBaseLineWidth(double updtWidthInPts)
        {
            VisioInterop.SetLineWidth(this.Shape, updtWidthInPts);
        }

        public void SetStartPoint(Coordinate coord)
        {
            VisioInterop.SetLineStartpoint(this.Shape, coord.X, coord.Y);
        }

        public void SetEndpoint(Coordinate coord)
        {
            VisioInterop.SetLineEndpoint(this.Shape, coord.X, coord.Y);
        }

        public void SetEndpoint(double x, double y)
        {
            VisioInterop.SetLineEndpoint(this.Shape, x, y);
        }

        public void SetLineGraphicsForLineMode()
        {
            throw new NotImplementedException();
        }

    }
}
