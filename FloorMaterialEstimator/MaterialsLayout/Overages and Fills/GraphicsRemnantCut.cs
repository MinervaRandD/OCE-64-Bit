#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsRemnantCut.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    namespace MaterialsLayout
    {
        using Utilities;
        using System.Drawing;

        using Graphics;
        using Geometry;

        public class GraphicsRemnantCut
        {
            public string Guid { get; set; }

            public GraphicsWindow Window { get; set; }

            public GraphicsPage Page { get; set; }

            public RemnantCut RemnantCut { get; set; }

            public GraphicsCut GraphicsCut { get; set; }

            public bool IsRotated => RemnantCut.IsRotated;

            public double CutAngle => RemnantCut.CutAngle;

            public Coordinate CutOffset => RemnantCut.CutOffset;


            public Coordinate UpperRght => RemnantCut.UpperRght;

            public Coordinate LowerRght => RemnantCut.LowerRght;

            public Coordinate UpperLeft => RemnantCut.UpperLeft;

            public Coordinate LowerLeft => RemnantCut.LowerLeft;

            public GraphicsRemnantCut(GraphicsWindow window, GraphicsPage page, RemnantCut remnantCut, string guid)
            {
                Window = window;

                Page = page;

                Guid = guid;

                RemnantCut = remnantCut;
            }

            public GraphicsRemnantCut(GraphicsWindow window, GraphicsPage page, RemnantCut remnantCut)
            {
                Window = window;

                Page = page;

                Guid = GuidMaintenance.CreateGuid(this);

                RemnantCut = remnantCut;
            }

            public GraphicShape Shape
            {
                get;
                set;
            }

            public void Rotate(double[,] rotationMatrix) => RemnantCut.Rotate(rotationMatrix);

            public void Translate(Coordinate translateCoord) => RemnantCut.Translate(translateCoord);

            public void Transform(Coordinate translateCoord, double theta, double[,] rotationMatrix) => RemnantCut.Transform(translateCoord, rotationMatrix);

            public void Draw(GraphicsLayer rmntEmbdLayer, Color cutPenColor, Color cutFillColor, double lineWidthInPts)
            {
                double[] coordinateArray = new double[10];

                coordinateArray[0] = UpperRght.X;
                coordinateArray[1] = UpperRght.Y;
                coordinateArray[2] = LowerRght.X;
                coordinateArray[3] = LowerRght.Y;
                coordinateArray[4] = LowerLeft.X;
                coordinateArray[5] = LowerLeft.Y;
                coordinateArray[6] = UpperLeft.X;
                coordinateArray[7] = UpperLeft.Y;
                coordinateArray[8] = UpperRght.X;
                coordinateArray[9] = UpperRght.Y;

                Shape = Page.DrawPolygon(this, coordinateArray, Guid);

                Shape.SetLineColor(cutPenColor);
                Shape.SetLineWidth(lineWidthInPts);
                Shape.SetFillOpacity(0);

                rmntEmbdLayer.AddShape(Shape, 1);
            }

            public void Delete()
            {
                if (Utilities.IsNotNull(Shape))
                {
                    Shape.Delete();
                }
            }

            public double Width  => MathUtils.H2Distance(LowerLeft.X, LowerLeft.Y, LowerRght.X, LowerRght.Y);

            public double WidthInInches => Width * Page.DrawingScaleInInches;

            public double Height => MathUtils.H2Distance(LowerLeft.X, LowerLeft.Y, UpperLeft.X, UpperLeft.Y);

            public double HeightInInches => Height * Page.DrawingScaleInInches;

            public double Area => Width * Height;

            public double AreaInInches => WidthInInches * HeightInInches;
        }
    }

}
