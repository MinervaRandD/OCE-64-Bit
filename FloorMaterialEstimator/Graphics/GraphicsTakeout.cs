//-------------------------------------------------------------------------------//
// <copyright file="Page.cs" company="Bruun Estimating, LLC">                    // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

namespace Graphics
{
    using System.Drawing; 
    using Geometry;

    public class GraphicsTakeout: GraphicsCircle
    {
        public double TakeoutAmount { get; set; }

        public string LineFinishBaseGuid { get; set; }
        

        public GraphicsTakeout(
            GraphicsWindow window
            , GraphicsPage page
            , Coordinate center
            , double radius
            , double takeoutAmount
            , string lineFinishBaseGuid) // Need to maintain the line finish base, but cannot reference directly  due to library inclusion conflict.
            : base(window, page, center, radius)
        {
            TakeoutAmount = takeoutAmount;

            LineFinishBaseGuid = lineFinishBaseGuid;
        }

        public GraphicsTakeout(
            GraphicsPage page
            , Coordinate center
            , double radius
            , double takeoutAmount
            , string lineFinishBaseGuid
            , string guid) : base(page, center, radius, guid)
        {
            TakeoutAmount = takeoutAmount;

            LineFinishBaseGuid = lineFinishBaseGuid;
        }

        public void Draw(
            Color color
            , double sizeInPts
            , int visioLineStyle)
        {
            Shape = Page.DrawCircle(Guid, Center, Radius, Color.Red);

            VisioInterop.SetShapeText(Shape, TakeoutAmount.ToString("0.0"), color, sizeInPts);

            VisioInterop.SetBaseLineColor(Shape, color);

            VisioInterop.SetBaseLineStyle(Shape, visioLineStyle);

            //Page.TakeoutShapeDict.Add(this.NameID, this);


        }
    }
}