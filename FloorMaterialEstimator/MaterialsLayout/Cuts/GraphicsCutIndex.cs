#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsCutIndex.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using Geometry;
    using Graphics;
    using SettingsLib;
    using System.Drawing;
    using Utilities;
    using TracerLib;

    public class GraphicsCutIndex : GraphicCircleTag, IGraphicsShape
    {
        public uint CutIndex
        {
            get
            {
                return tagIndexInt;
            }

            set
            {
                tagIndexInt = value;
            }
        }

        /// <summary>
        /// This property is the center of the circle which is drawn to display the overage index.
        /// </summary>
        public Coordinate Location
        {
            get
            {
                return this.Center;
            }

            set
            {
                this.Center = value;
            }
        }

        #region Constructors

        public GraphicsCutIndex(
            GraphicsWindow window
            , GraphicsPage page
            , GraphicsLayerBase layer
            )
        {
            Window = window;
            Page = page;

            ShapeType |= ShapeType.CutIndex;

            this.GraphicsLayer = layer;
        }

        #endregion

        #region Deleters and Destructors

        public void Delete()
        {
            Page.RemoveFromPageShapeDict(Guid);

            if (Utilities.IsNotNull(Shape))
            {
                Shape.Delete();

                Shape = null;
            }
        }

        #endregion

        public void DrawCutIndexText()
        {
            // There is an inherent bug in the system in that this call is made when first 
            // loading an existing project, before the index has been given a location.

            if (Shape is null)
            {
                Tracer.TraceGen.TraceError("GraphicsCutIndex:DrawCutIndexText is called with a null shape.", 1, true);

                return;
            }

            if (Utilities.IsNotNull(Shape))
            {
                Shape.SetShapeText(CutIndex.ToString(), GlobalSettings.CutIndexFontColor, GlobalSettings.CutIndexFontInPts);
            }
        }


        public GraphicShape Draw()
        {
            double circleRadius = GlobalSettings.CutIndexCircleRadius;

            Shape = Page.DrawCircle(this, Location, circleRadius, Color.Blue);

            this.ShapeType |= ShapeType.CutIndex | ShapeType.CircleTag;

            VisioInterop.SetFillOpacity(Shape, 0.0);

            VisioInterop.SetShapeData(Shape, "[Cut Index]", "Circle", Guid);

            Shape.BringToFront();

            if (CutIndex > 0)
            {
                DrawCutIndexText();
            }

            VisioInterop.DeselectShape(Window, Shape);

            return Shape;
        }

        public void Select()
        {
            if (Shape is null)
            {
                return;
            }

            Shape.SetLineWidth(3);
            Shape.SetLineColor(Color.Red);
        }

        public void Deselect()
        {
            Shape.SetLineWidth(1);
            Shape.SetLineColor(Color.Blue);
        }


        public void SetVisibility(bool visible)
        {
            if (this.Shape is null)
            {
                return;
            }

            if (visible)
            {
                VisioInterop.SetBaseLineOpacity(Shape, GlobalSettings.ShowCutIndexCircles ? 1 : 0);

                Shape.SetShapeText(CutIndex.ToString(), Color.Blue, GlobalSettings.CutIndexFontInPts);

                //// Kludge. Can't get text to go away otherwise :(
            }

            else
            {
                VisioInterop.SetBaseLineOpacity(Shape, 0);

                Shape.SetShapeText("", Color.White, 0);

                this.Window?.DeselectAll();
            }

            this.Window?.DeselectAll();
        }

        public void SetCircleVisibility()
        {
            if (this.Shape is null)
            {
                return;
            }

            VisioInterop.SetBaseLineStyle(Shape, GlobalSettings.ShowCutIndexCircles ? 1 : 0);
            //VisioInterop.SetBaseLineOpacity(Shape, GlobalSettings.ShowCutIndexCircles ? 1.0 : 0.0);
        }
    }
}
