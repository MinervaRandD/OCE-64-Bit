#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicCircle.cs. Project: Graphics. Created: 11/24/2024         */
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
using System.Drawing;
using System.Linq;
using Geometry;
using Globals;
using Utilities;

namespace Graphics
{
    public class GraphicCircle : Circle, IGraphicsShape
    {
        private object _parentObject = null;

        public object ParentObject
        {
            get
            {
                return _parentObject;       
            }

            set
            {
                if (_parentObject == value)
                {
                    return;
                }

                _parentObject = value;
            }
        }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public LayoutAreaType LayoutAreaType;

        public GraphicShape Shape { get; set; } = null;

        private ShapeType _shapeType = ShapeType.Circle;

        public ShapeType ShapeType
        {
            get
            {
                if (Shape is null)
                {
                    return _shapeType;
                }

                return Shape.ShapeType;
            }

            set
            {
                _shapeType = value | ShapeType.Circle;

                if (Shape is null)
                {
                    return;
                }

                Shape.ShapeType = _shapeType;
            }
        }

        string _guid = string.Empty;

        public string Guid
        {
            get
            {
                if (Shape is null)
                {
                    return _guid;
                }

                return Shape.Guid;
            }

            set
            {
                _guid = value;

                if (Shape is null)
                {
                    return;
                }

                Shape.Guid = value;
            }
        }


        private GraphicsLayerBase _graphicsLayerBase = null;

        public GraphicsLayerBase GraphicsLayer
        {
            get
            {
                // The following is based on the assumption that the graphic circle is on only one layer.
                if (Shape is null)
                {
                    return null;
                }

                if (Shape.LayerSet.Count == 0)
                {
                    return null;
                }

                if (Shape.LayerSet.Count > 1)
                {
                    throw new NotImplementedException();
                }

                return Shape.LayerSet.First();
            }

            set
            {
                if (value is null)
                {
                    return;
                }

                if (Shape is null)
                {
                    return;
                }

                Shape.AddToLayerSet(value);
            }
        }

        public Color LineColor { get; set; } = Color.Empty;

        public GraphicCircle()  
        {
            ShapeType |= ShapeType.Circle;
        }

        public GraphicCircle(
            GraphicsWindow window
            , GraphicsPage page
            , GraphicsLayerBase graphicsLayer
            , string guid
            , Coordinate center
            , double radius
            , Color lineColor)
        {
           
            Window = window;
            Page = page;
            GraphicsLayer = graphicsLayer;
            Guid = guid;
            Center = center;
            Radius = radius;
            LineColor = lineColor;

            Shape = new GraphicShape(this, window, page, graphicsLayer)
            {
                Guid = this.Guid
            };
        }

        //public GraphicShape Draw()
        //{

        //    Shape = Page.DrawCircle(this, Center, Radius, Color.Red);

        //    VisioInterop.SetFillOpacity(Shape, 0.0);

        //    VisioInterop.SetShapeData(Shape, "OverIndex", "OverIndex", Guid);

        //    Shape.BringToFront();

        //    //Shape.VisioShape.CellChanged += VisioShape_CellChanged;
        //    GraphicsDebugSupportRoutines.CheckForOverageIndex(Page, Guid);

        //    this.Window?.DeselectAll();

        //    return Shape;
        //}
        public void Delete()
        {

            try
            {

           
            if (Shape is null)
            {
                return;
            }

            if (Page is null)
            {
                return;
            }

           
            Page.RemoveFromPageShapeDict(Shape);

           foreach (GraphicsLayer graphicsLayerBase in Shape.LayerSet)
           {
               if (graphicsLayerBase is null)
               {
                   continue;
               }

               // Not sure why the following is throwing an exception. Patch for the time being

               try
               {
                   var iShape = (IGraphicsShape)this;

                   graphicsLayerBase.RemoveShape(this);
               }

               catch (Exception e)
               {
                   MessageBoxAdv.Show(
                       "An exception was thrown in delete graphics circle. Please record the circumstances around this and report.");
               }
             
           }

            Shape.Delete();

            Shape = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

        }

    }
}
