#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsPage.cs. Project: Graphics. Created: 6/10/2024         */
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

namespace Graphics
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;

    using Utilities;

    using Geometry;

    using Visio = Microsoft.Office.Interop.Visio;
    using System.Xml.Serialization;

    using TracerLib;
    using System.Windows.Forms;

    [Serializable]
    public partial class GraphicsPage
    {
        public Visio.Page VisioPage
        {
            get;set;
        }

        public Visio.Document VisioDocument;

        public GraphicsWindow Window { get; set; }

        //public GraphicsPage Page { get; set; }

        public double PageHeight { get; set; }
        public double PageWidth { get; set; }


        [XmlIgnore]
        public Coordinate LastPointDrawn = Coordinate.NullCoordinate;

        #region Events and Delegates

        public delegate void ScaleChangedEventHandler(double scaleValue);

        public event ScaleChangedEventHandler ScaleChanged;

        #endregion
        public string Name
        {
            get
            {
                return VisioPage.Name;
            }

            set
            {
                VisioPage.Name = value;
            }
        }

        private double drawingScaleInInches = 12;

        public virtual double DrawingScaleInInches
        {
            get
            {
                return drawingScaleInInches;
            }

            set
            {
                if (value == drawingScaleInInches)
                {
                    return;
                }

                drawingScaleInInches = value;

                if (ScaleChanged != null)
                {
                    ScaleChanged.Invoke(value);
                }
            }
        }

        //public GraphicsPage(GraphicsWindow window)
        //{
        //    Window = window;

        //    initializeGlobalLayers();

        //  //  window.VisioWindow.Application.BeforeShapeDelete += Application_BeforeShapeDelete;
        //}

        public GraphicsPage(GraphicsWindow window, Visio.Document visioDocument, Visio.Page visioPage)
        {
            this.VisioDocument = visioDocument;

            this.VisioPage = visioPage;

            Window = window;

            initializeGlobalLayers();

          //  window.VisioWindow.Application.BeforeShapeDelete += Application_BeforeShapeDelete;

            this.VisioPage.ShapeAdded += VisioPage_ShapeAdded;
        }

        private void VisioPage_ShapeAdded(Visio.Shape Shape)
        {
            string data1 = Shape.Data1;
            string datat2 = Shape.Data2;
        }

        public void SetPageSize(double width, double height)
        {
            VisioInterop.SetPageSize(this, width, height);
        }


        public void SetPageSizeToCurrentSize()
        {
            SetPageSize(PageWidth, PageHeight);
        }

        /// <summary>
        /// Attempts to get the drawing layer for the specified Page.
        /// </summary>
        /// <param name="visioPage">The visio Page on which to search for the drawing layer</param>
        /// <returns>Returns the drawing layer if it exists</returns>
        public GraphicsLayerBase GetTakeoutLayerIfExists()
        {
            return TakeoutLayer;
        }


        public bool MouseOverSeamingTool(double x, double y)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[
                x
                , y
                , (short)(Visio.VisSpatialRelationCodes.visSpatialTouching | Visio.VisSpatialRelationCodes.visSpatialContainedIn | Visio.VisSpatialRelationCodes.visSpatialOverlap)
                , 0.1
                , 0];

            if (selection is null)
            {
                return false;
            }

            if (selection.Count <= 0)
            {
                return false;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                if (visioShape.Data1 == "[SeamingTool]")
                {
                    return true;
                }
            }

            return false;

        }

        public GraphicShape MouseOverTextBox(double x, double y)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[
                x
                , y
                , (short)(Visio.VisSpatialRelationCodes.visSpatialTouching | Visio.VisSpatialRelationCodes.visSpatialContainedIn | Visio.VisSpatialRelationCodes.visSpatialOverlap)
                , 0.1
                , 0];

            if (selection is null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                if (visioShape.Data1 == "[TextBox]")
                {
                    string guid = visioShape.Data3;

                    if (PageShapeDict.ContainsKey(guid))
                    {
                        return (GraphicShape) PageShapeDict[guid];
                    }

                    return  null;
                }
            }

            return null;
        }

        public bool MouseOverCanvasArrow(double x, double y)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[
                x
                , y
                , (short)(Visio.VisSpatialRelationCodes.visSpatialTouching | Visio.VisSpatialRelationCodes.visSpatialContainedIn | Visio.VisSpatialRelationCodes.visSpatialOverlap)
                , 0.1
                , 0];

            if (selection is null)
            {
                return false;
            }

            if (selection.Count <= 0)
            {
                return false;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                if (visioShape.Data1 == "[CanvasArrow]")
                {
                    return true;
                }
            }

            return false;
        }
    

        public bool MouseOverMeasuringStickTool(double x, double y)
        {
            Visio.Selection selection = VisioPage.SpatialSearch[
                x
                , y
                , (short)(Visio.VisSpatialRelationCodes.visSpatialTouching | Visio.VisSpatialRelationCodes.visSpatialContainedIn | Visio.VisSpatialRelationCodes.visSpatialOverlap)
                , 0.1
                , 0];

            if (selection is null)
            {
                return false;
            }

            if (selection.Count <= 0)
            {
                return false;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                if (visioShape.Data1 == "[MeasuringStick]")
                {
                    return true;
                }
            }

            return false;
        }

        public GraphicShape CreateFixedWidthTextBox(double xTop, double yTop, double width, string text, double fontSize)
        {
            Visio.Shape visioShape = VisioInterop.CreateFixedWidthTextBox(this, xTop, yTop, width, text, fontSize);

            GraphicShape shape = new GraphicShape()
            {
                VisioShape = visioShape
            };

            shape.Data1 = "[TextBox]";
            shape.Data2 = "Text Box";
            shape.Data3 = GuidMaintenance.CreateGuid(this);

            AddToPageShapeDict(shape);
            return shape;
        }

    }
}
