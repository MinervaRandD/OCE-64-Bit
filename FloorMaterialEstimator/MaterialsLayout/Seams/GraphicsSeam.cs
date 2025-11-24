#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsSeam.cs. Project: MaterialsLayout. Created: 11/14/2024         */
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

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace MaterialsLayout
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using FinishesLib;
    using Geometry;
    using Graphics;
    using Utilities;

    [Serializable]
    public class GraphicsSeam : IGraphicsShape
    {
        public GraphicShape Shape { get; set; }

        public GraphicsLayerBase GraphicsLayer
        {
            get
            {
                return Shape.SingleGraphicsLayer;
            }

            set
            {
                if (Shape is null)
                {
                    throw new NotImplementedException();
                }

                Shape.AddToLayerSet(value);

            }
        }


        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public GraphicsDirectedLine graphicsDirectedLine { get; set; }

        public SeamType SeamType
        {
            get
            {
                if (Seam is null)
                {
                    throw new NotImplementedException("Attempt to get seam type with seam undefined.");
                }

                return Seam.SeamType;
            }

            set
            {
                if (Seam is null)
                {
                    throw new NotImplementedException("Attempt to set seam type with seam undefined.");
                }

                Seam.SeamType = value;
            }
        }

        public bool IsHideable
        {
            get
            {
                if (Seam is null)
                {
                    throw new NotImplementedException("Attempt to get hideable with seam undefined.");
                }

                return Seam.IsHideable;
            }

            set
            {
                if (Seam is null)
                {
                    throw new NotImplementedException("Attempt to set hideable with seam undefined.");
                }

                Seam.IsHideable = value;
            }
        }

        public bool Selected { get; set; } = false;

        public Seam Seam { get; set; }

        public GraphicsSeam(GraphicsWindow window, GraphicsPage page, Seam seam)
        {
            Window = window;

            Page = page;

            this.Seam = seam;

            this.Guid = GuidMaintenance.CreateGuid(this);
        }

        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.Line;
            }
        }
        public string Guid { get; set; }

        public void Draw(Color seamColor, double lineWidthInPts, int visioDashType, bool lockShape = true, string seamType = "Seam")
        {
            graphicsDirectedLine = new GraphicsDirectedLine(Window, Page, this.Seam, LineRole.Seam, false, this.Guid);

            graphicsDirectedLine.Draw(seamColor, lineWidthInPts, visioDashType);

            this.Shape = graphicsDirectedLine.Shape;

            VisioInterop.SetShapeData(this.Shape, seamType, "Line", this.Shape.Guid);

            Page.AddToPageShapeDict(this.Shape);

            if (lockShape)
            {
                VisioInterop.LockShape(this.Shape);
            }
        }

        public void Draw(SeamFinishBase seamFinishBase, bool lockShape = true)
        {
            Draw(seamFinishBase.SeamColor, seamFinishBase.SeamWidthInPts, seamFinishBase.VisioDashType, lockShape);
        }

        public  void Delete()
        {
            Page.RemoveFromPageShapeDict(this.Shape);

            VisioInterop.DeleteShape(Shape);

            if (graphicsDirectedLine != null)
            {
                graphicsDirectedLine.Delete();
            }
        }

        public double Length()
        {
            return Page.DrawingScaleInInches * Seam.Length;
        }

        public void Undraw()
        {
            Delete();
        }

        public void SetSeamLineWidth(double seamWidthInPts)
        {
            this.Shape.SetLineWidth(seamWidthInPts);
        }

        public void UpdateSeamGraphics(Color seamColor, int visioDashType, double seamWidthInPts)
        {
            this.Shape.SetLineColor(seamColor);
            this.Shape.SetLineWidth(seamWidthInPts);

            if (seamWidthInPts > 0)
            {
                this.Shape.SetLineStyle(visioDashType.ToString());
            }

            else
            {
                this.Shape.SetLineStyle("0");
            }
        }

        public GraphicsSeam Clone()
        {
            Seam clonedSeam = this.Seam.Clone();

            GraphicsSeam clonedGraphicsSeam = new GraphicsSeam(this.Window, this.Page, clonedSeam)
            {
                SeamType = this.SeamType,
                IsHideable = this.IsHideable,
            };

            clonedGraphicsSeam.Guid = GuidMaintenance.CreateGuid(clonedGraphicsSeam);

            return clonedGraphicsSeam;
        }

        public Dictionary<string, GraphicShape> GenerateGrahpicsShapeDict()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            if (this.Shape == null)
            {
                return rtrnDict;
            }

            if (this.Shape.VisioShape is null)
            {
                return rtrnDict;
            }

            rtrnDict[this.Shape.Guid] = this.Shape;

            return rtrnDict;
        }
    }
}
