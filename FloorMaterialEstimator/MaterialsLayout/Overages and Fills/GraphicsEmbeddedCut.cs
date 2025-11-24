#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsEmbeddedCut.cs. Project: MaterialsLayout. Created: 6/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialsLayout
{
   
    //-------------------------------------------------------------------------------//
    // <copyright file="ParentGraphicsCut.cs" company="Bruun Estimating, LLC">             // 
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
        using Utilities;
        using System.Drawing;
        
        using Graphics;
        using Geometry;

        public class GraphicsEmbeddedCut
        {
            public string Guid { get; set; }

            public GraphicsWindow Window { get; set; }

            public GraphicsPage Page { get; set; }

            public EmbeddedCut EmbeddedCut { get; set; }

            public GraphicsCut GraphicsCut { get; set; }

            public bool IsRotated => EmbeddedCut.IsRotated;

            public double CutAngle => EmbeddedCut.CutAngle;

            public Coordinate CutOffset => EmbeddedCut.CutOffset;
            
            public GraphicsEmbeddedCut(GraphicsWindow window, GraphicsPage page, EmbeddedCut embeddedCut, string guid)
            {
                Window = window;

                Page = page;

                Guid = guid;

                EmbeddedCut = embeddedCut;
            }

            public GraphicsEmbeddedCut(GraphicsWindow window, GraphicsPage page, EmbeddedCut embeddedCut)
            {
                Window = window;

                Page = page;

                Guid = GuidMaintenance.CreateGuid(this);

                EmbeddedCut = embeddedCut;
            }

            public GraphicShape Shape
            {
                get;
                set;
            }

            public void Rotate(double theta) => EmbeddedCut.Rotate(theta);
           
            public void Translate(Coordinate translateCoord) => EmbeddedCut.Translate(translateCoord);

            public void Transform(Coordinate translateCoord, double theta, double[,] rotationMatrix) => EmbeddedCut.Transform(translateCoord, theta);

            public void Draw(Color cutPenColor, Color cutFillColor, double lineWidthInPts)
            {
                double x1 = EmbeddedCut.CutRectangle.UpperLeft.X;
                double y1 = EmbeddedCut.CutRectangle.UpperLeft.Y;
                double x2 = EmbeddedCut.CutRectangle.LowerRght.X;
                double y2 = EmbeddedCut.CutRectangle.LowerRght.Y;

                Shape = Page.DrawRectangle(this, x1, y1, x2, y2, Guid);
            }

            public void Delete()
            {
                if (Utilities.IsNotNull(Shape))
                {
                    Shape.Delete();
                }
            }

        }
    }

}
