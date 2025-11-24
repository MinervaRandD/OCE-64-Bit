//-------------------------------------------------------------------------------//
// <copyright file="GraphicsCut.cs" company="Bruun Estimating, LLC">             // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace MaterialsLayout
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;
    using Geometry;
    using Utilities;
   
    public class GraphicsCut: Cut, IGraphicsShape
    {
        public GraphicsCut(Page page, Cut cut): base(cut) //base(cut.CutPolygon, cut.UpperOffset, cut.LowerOffset)
        {
            Page = page;

            if (cut.OverageList != null)
            {
                GraphicsOverageList = new List<GraphicsOverage>();

                cut.OverageList.ForEach(o => GraphicsOverageList.Add(new GraphicsOverage(Page, o)));
            }
        }

        public Shape Shape { get; set; }
        public Page Page { get; set; }

        public List<GraphicsOverage> GraphicsOverageList;

        public string NameID
        {
            get
            {
                if (Shape == null)
                {
                    return string.Empty;
                }

                return Shape.NameID;
            }
        }

        ShapeType IGraphicsShape.ShapeType
        {
            get
            {
                return ShapeType.Polygon;
            }
        }

        internal void Draw(Color cutPenColor, Color cutFillColor, double lineWidthInPts)
        {
            GraphicsCutPolygon graphicsCutPolygon = new GraphicsCutPolygon(Page, base.CutPolygon);

            this.Shape = graphicsCutPolygon.Draw(cutPenColor, cutFillColor, lineWidthInPts);

            VisioInterop.SendToBack(this.Shape);

            if (GraphicsOverageList != null)
            {
                foreach (GraphicsOverage graphicsOverage in GraphicsOverageList)
                {
                    graphicsOverage.Draw(Color.Yellow);
                }
            }
        }

        public new void Delete()
        {

            if (GraphicsOverageList != null)
            {
                foreach (GraphicsOverage graphicsOverage in GraphicsOverageList)
                {
                    graphicsOverage.Delete();
                }
            }

            if (Shape != null)
            {
                Shape.Delete();
            }
        }
    }
}
