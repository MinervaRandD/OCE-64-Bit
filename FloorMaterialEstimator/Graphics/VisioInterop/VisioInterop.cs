#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: VisioInterop.cs. Project: Graphics. Created: 6/10/2024         */
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
//     Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019, 2020     //
//-------------------------------------------------------------------------------//
//#define ROTATION_DEBUG

namespace Graphics
{
    using Geometry;
    using Globals;
    using Graphics;
    using Microsoft.Office.Interop.Visio;
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using TracerLib;
    using Utilities;
    using static System.Windows.Forms.AxHost;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;
    using static Utilities.MessageBoxAdv;
    using Color = System.Drawing.Color;
    using Visio = Microsoft.Office.Interop.Visio;

    /// <summary>
    /// VisioSupport encapsulates ALL functionality related to the use of the visio embedded
    /// surface. The objective is to isolate the actual platform from the functionality in the
    /// code so that the impact will be minimal if a different surface is used.
    /// </summary>
    public static class VisioInterop
    {
        private static Visio.Application vsoApplication;

        private static Visio.Window vsoWindow;

        private static Visio.Page vsoPage;

        public static GraphicsWindow Window { get; set; }

        public static GraphicsPage Page { get; set; }

        public static void Init(Visio.Application application, GraphicsWindow window, GraphicsPage page)
        {
            vsoApplication = application;

            VisioInterop.Window = window;

            VisioInterop.Page = page;

            VisioInterop.vsoWindow = window.VisioWindow;

            VisioInterop.vsoPage = page.VisioPage;
        }

        #region layers

        public static void SetLayerVisibility(GraphicsLayer layer, bool visible = true)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer, visible });
#endif

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:SetLayerVisibility"))
            {
                return;
            }

            Visio.Layer visioLayer = layer.visioLayer;

            SetLayerVisibility(visioLayer, visible);

            //foreach (IGraphicsShape iShape in layer.Shapes)
            //{
            //    SetShapeLineVisibility(iShape, visible);
            //}

            layer.Visibility = visible;
        }


        private static void SetLayerVisibility(Visio.Layer layer, bool visible = true)
        {
#if TRACE0    
            Tracer.TraceGen.TraceMethodCall(1, false, new object[]{layer, visible});
#endif

            try
            {
                if (layer is null)
                {
                    return;
                }

                if (visible)
                {
                    layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "1";
                }

                else
                {
                    layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "0";
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetLayerVisibility threw an exception.", ex, 1, true);
            }
        }

        public static void SetupExportResolution(Visio.ApplicationSettings vsoAppSettings)
        {
            vsoAppSettings.SetRasterExportResolution(
                Visio.VisRasterExportResolution.visRasterUseCustomResolution
                , 8.0
                , 8.0
                , Visio.VisRasterExportResolutionUnits.visRasterPixelsPerInch);

            //vsoAppSettings.SetRasterExportSize(
            //    Visio.VisRasterExportSize.visRasterFitToSourceSize
            //    , 4.666667
            //    , 3.197917
            //    , Visio.VisRasterExportSizeUnits.visRasterInch);

            vsoAppSettings.RasterExportColorFormat = Visio.VisRasterExportColorFormat.visRaster256Color;

            //Application.Settings.RasterExportDataFormat = visRasterInterlace
            //Application.Settings.RasterExportColorFormat = visRaster256Color
            //Application.Settings.RasterExportRotation = visRasterNoRotation
            //Application.Settings.RasterExportFlip = visRasterNoFlip
            //Application.Settings.RasterExportBackgroundColor = 16777215
            //Application.Settings.RasterExportTransparencyColor = 16777215
            //Application.Settings.RasterExportUseTransparencyColor = False
            //Application.ActiveWindow.Page.Export "C:\Users\Marc Diamond\Documents\Drawing1.png"
            //  vsoAppSettings.SetRasterExportResolution(Visio.VisRasterExportResolution.visRasterUsePrinterResolution, 100, 100, 0);

        }

        public static string VisioShapeToString(Visio.Shape visioShape)
        {
            if (visioShape is null)
            {
                return "{null, null, null}";
            }

            try
            {
                string guid = visioShape.Data3;

                if (guid.Length > 8)
                {
                    guid = guid.Substring(0, 8);
                }

                return "{" + visioShape.Data1 + ", " + visioShape.Data2 + ", " + guid + "}";
            }

            catch (Exception ex)
            {
                return "{<deleted>, <deleted>, <deleted>}";
            }
        }

        public static string VisioLayerToString(Visio.Layer visioLayer)
        {
            return visioLayer.Name;
        }

        public static bool GetLayerVisibility(GraphicsLayer layer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer });
#endif

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:GetLayerVisibility"))
            {
                return false;
            }

            try
            {
                Visio.Layer visioLayer = layer.visioLayer;

                return GetLayerVisibility(visioLayer);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:GetLayerVisibility", ex, 1, true);

                return false;
            }
        }


        public static bool GetLayerVisibility(Visio.Layer layer)
        {
            if (layer is null)
            {
                return false;
            }

            return layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU == "1";

        }

        #endregion

        /// <summary>
        /// Remove all shapes from the visio canvas. Should not be used once debugging has
        /// been completed.
        /// </summary>
        /// <param name="page">Graphics Page on which shapes should be removed</param>
        public static void RemoveAllShapes(GraphicsPage page)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page });

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:RemoveAllShapes"))
            {
                return;
            }

            try
            {
                Visio.Page visioPage = page.VisioPage;

                foreach (Visio.Shape shape in visioPage.Shapes)
                {
                    shape.Delete();
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:RemoveAllShapes throws an exception.", ex, 1, true);
            }
        }

        public static GraphicShape GroupShapes(GraphicsWindow window, GraphicShape[] shapeArray)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window });
#endif
            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:GroupShapes"))
            {
                return null;
            }

            foreach (GraphicShape shape in shapeArray)
            {
                if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GroupShapes"))
                {
                    return null;
                }

                if (!VisioValidations.VisioShapeNotDeleted(shape.VisioShape, "VisioInterop:GroupShapes"))
                {
                    return null;
                }
            }

            #endregion

            try
            {

                if (shapeArray.Length <= 0)
                {
                    return null;
                }

                Visio.Window visioWindow = window.VisioWindow;

                visioWindow.DeselectAll();

                Visio.Selection vsoSelection = visioWindow.Selection;

                int count = 0;

                foreach (GraphicShape shape in shapeArray)
                {
                    Visio.Shape visioShape = shape.VisioShape;

                    vsoSelection.Select(visioShape, (short)Visio.VisSelectArgs.visSelect);

                    count++;
                }

                if (count <= 0)
                {
                    return null;
                }

                Visio.Shape groupedShape = vsoSelection.Group();

                visioWindow.DeselectAll();

                GraphicShape rtrnShape = new GraphicShape();

                rtrnShape.VisioShape = groupedShape;

                return rtrnShape;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GroupShapes throws an exception.", ex, 1, true);

                return null;
            }
        }

        public static void UngroupShape(GraphicShape groupedShape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { groupedShape });
#endif
            #region Validations

            if (groupedShape is null)
            {
                return;
            }
          
            #endregion

            try
            {
               
                Visio.Shape visioShape = groupedShape.VisioShape;

                visioShape.Ungroup();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:UngroupShape throws an exception.", ex, 1, true);

                return ;
            }
        }

        public static void ShowPanAndZoomBox(GraphicsWindow window, bool show)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, show });
#endif

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:ShowPanAndZoomBox"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                visioWindow.Windows.ItemFromID[(short)Visio.VisWinTypes.visWinIDPanZoom].Visible = show;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:ShowPanAndZoomBox throws an exception.", ex, 1, true);
            }
        }

        //public static void DebugFormatAllShapes()
        //{
        //    foreach (Visio.Shape visioShape in vsoPage.Shapes)
        //    {
        //        VisioInterop.SetBaseFillColor(new GraphicShape(Window, Page, visioShape, ShapeType.Unknown), Color.Blue);
        //    }
        //}

        public static void AddShapeToLayer(GraphicShape shape, GraphicsLayer layer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, layer });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:AddShape"))
            {
                return;
            }

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:AddShape"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:AddShape"))
                {
                    return;
                }

                Visio.Layer visioLayer = layer.visioLayer;

                visioLayer.Add(visioShape, 1);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:AddShape", ex, 1, true);
            }
        }

        public static void RemoveShapeFromLayer(GraphicShape shape, GraphicsLayer layer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, layer });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:RemoveShapeFromLayer"))
            {
                return;
            }

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:RemoveShapeFromLayer"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:RemoveShapeFromLayer"))
                {
                    return;
                }

                Visio.Layer visioLayer = layer.visioLayer;

                // Not sure why this is throwing an exception, but protecting for now.
                // Patch.

                visioLayer.Remove(visioShape, 1);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:RemoveShapeFromLayer throws an exception.", ex, 1, true);
            }
        }

        public static void RemoveShapeFromLayer(Visio.Shape visioShape, GraphicsLayer layer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { visioShape, layer });
#endif
            if (visioShape is null)
            {
                Tracer.TraceGen.TraceError("null visioShape in call to VisioInterop:RemoveShapeFromlayer", 1, true);

                return;
            }

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:RemoveShapeFromLayer"))
            {
                return;
            }

            try
            {
                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:RemoveShapeFromLayer"))
                {
                    return;
                }

                Visio.Layer visioLayer = layer.visioLayer;

                // Not sure why this is throwing an exception, but protecting for now.
                // Patch.

                visioLayer.Remove(visioShape, 1);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:RemoveShapeFromLayer throws an exception.", ex, 1, true);
            }
        }

        public static void SetSizeBoxWindowVisibility(GraphicsWindow window, bool visible)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, visible });
#endif

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:SetSizeBoxWindowVisibility"))
            {
                return;
            }

            #endregion

            Visio.Window visioWindow = window.VisioWindow;

            Visio.Window sizeBoxWindow = visioWindow.Windows.ItemFromID[(int)Visio.VisWinTypes.visWinIDSizePos];

            if (sizeBoxWindow != null)
            {
                sizeBoxWindow.Visible = visible;
            }

        }

        public static void SetShapeBegin(GraphicShape shape, double beginX, double beginY)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, beginX, beginY });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeBegin"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeBegin"))
                {
                    return;
                }

                visioShape.SetBegin(beginX, beginY);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeBegin throws an exception.", ex, 1, true);
            }
        }

        public static void SetShapeEnd(GraphicShape shape, double endX, double endY)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, endX, endY });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeEnd"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeEnd"))
                {
                    return;
                }

                visioShape.SetEnd(endX, endY);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeEnd throws an exception.", ex, 1, true);
            }
        }


        /// <summary>
        /// Draws a line on the canvas.
        /// </summary>
        /// <param name="graphicsPage">The graphics Page on which to place the line</param>
        /// <param name="x1">The first x coordinate of the line</param>
        /// <param name="y1">The first y coordinate of the line</param>
        /// <param name="x2">The second x coordinate of the line</param>
        /// <param name="y2">The second y coordinate of the line</param>
        /// <param name="data1">The string to insert into the visio shape data1 location</param>
        /// <returns>Returns a shape object containing the line</returns>

        internal static GraphicShape DrawLine(GraphicsPage page, double x1, double y1, double x2, double y2, string data1 = "")
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page, x1, y1, x2, y2, data1 });
#endif

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:DrawLine"))
            {
                return null;
            }

            try
            {
                Visio.Page visioPage = page.VisioPage;

                Visio.Shape visioShape = visioPage.DrawLine(x1, y1, x2, y2);

                GraphicShape shape = new GraphicShape(null, Window, page, visioShape, ShapeType.Line);

                string guid = GuidMaintenance.CreateGuid(shape);

                shape.Guid = guid;

                visioShape.Data1 = data1;
                visioShape.Data2 = "Line";
                visioShape.Data3 = guid;

                // MDD Remove

                //if (!data1.Contains("Guide"))
                //{
                //    Console.WriteLine("Drawing line: (" + x1.ToString("0.00") + "," + y1.ToString("0.00") + ") (" + x2.ToString("0.00") + "," + y2.ToString("0.00") + ") [" +
                //        data1 + ", " + guid + "]");
                //}

                return shape;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:DrawLine", ex, 1, true);

                return null;
            }
        }

        public static void SetViewRect(GraphicsWindow window, double viewLeft, double viewUppr, double viewWdth, double viewHght)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, viewLeft, viewUppr, viewWdth, viewHght });
#endif

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:SetViewRect"))
            {
                return;
            }

            if (viewLeft <= -viewWdth / 2.0)
            {
                Tracer.TraceGen.TraceError("Invalid value for view left, " + viewLeft + ", in call to VisioInterop:SetViewRect", 1, true);

                return;
            }

            if (viewUppr <= 0)
            {
                Tracer.TraceGen.TraceError("Invalid value for view upper, " + viewLeft + ", in call to VisioInterop:SetViewRect", 1, true);

                return;
            }

            if (viewWdth <= 0)
            {
                Tracer.TraceGen.TraceError("Invalid value for view width, " + viewWdth + ", in call to VisioInterop:SetViewRect", 1, true);

                return;
            }

            if (viewHght <= 0)
            {
                Tracer.TraceGen.TraceError("Invalid value for view height, " + viewHght + ", in call to VisioInterop:SetViewRect", 1, true);

                return;
            }

            #endregion

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                visioWindow.SetViewRect(viewLeft, viewUppr, viewWdth, viewHght);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:SetViewRect", ex, 1, true);

                return;
            }
        }

        public static bool ShapeIsSelected(GraphicsWindow window, GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape });
#endif
            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:ShapeIsSelected"))
            {
                return false;
            }

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:ShapeIsSelected"))
            {
                return false;
            }

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                Visio.Selection selection = visioWindow.Selection;

                for (int i = 1; i <= selection.Count; i++)
                {
                    Visio.Shape selectedShape = selection[i];

                    if (selectedShape.Data3 == shape.Guid)
                    {
                        return true;
                    }
                }

                return false;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:ShapeIsSelected", ex, 1, true);

                return false;
            }
        }

        public static void SetBaseLineColor(GraphicShape shape, Color color)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseLineColor"))
            {
                return;
            }

            try
            {
                SetBaseLineColor(shape, "THEMEGUARD(RGB(" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + "))");
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:SetBaseLineColor", ex, 1, true);
            }
        }

        public static void SetBaseLineColor(GraphicShape shape, string lineColorFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, lineColorFormula });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseLineColor"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetBaseLineColor"))
                {
                    return;
                }

                visioShape.Cells["LineColor"].FormulaU = lineColorFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseLineColor throws an exception.", ex, 1, true);

                return;
            }
        }

        public static void SetBaseTextColor(GraphicShape shape, Color color)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseTextColor"))
            {
                return;
            }

            try
            {
                SetBaseTextColor(shape, "THEMEGUARD(RGB(" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + "))");
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:SetBaseLineColor", ex, 1, true);
            }
        }

        public static void SetBaseTextColor(GraphicShape shape, string textColorFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, lineColorFormula });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseLineColor"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetBaseLineColor"))
                {
                    return;
                }

                visioShape.CellsU["Char.Color"].FormulaU = textColorFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseTextColor throws an exception.", ex, 1, true);

                return;
            }
        }

        /// <summary>
        /// Sets the base line color
        /// </summary>
        /// <param name="shape">The graphics shape associated with the line</param>
        /// <param name="lineColorFormula">The line color formula</param>
        public static string GetBaseLineColor(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetBaseLineColor"))
            {
                return string.Empty;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetBaseLineColor"))
                {
                    return string.Empty;
                }

                return visioShape.Cells["LineColor"].FormulaU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseLineColor throws an exception.", ex, 1, true);

                return string.Empty;
            }
        }

        /// <summary>
        /// Sets the line opacity
        /// </summary>
        /// <param name="shape">The graphics shape associated with the line</param>
        /// <param name="opacity">The desired opacity (between 0 and 100)</param>
        public static void SetBaseLineOpacity(GraphicShape shape, double opacity)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, opacity });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseLineOpacity"))
            {
                return;
            }

            try
            {

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetBaseLineOpacity"))
                {
                    return;
                }

                double transparency = 100.0 * Math.Max(0.0, Math.Min(1.0, 1.0 - opacity));

                // visioShape.Cells["Transparency"].FormulaU = transparency.ToString("0.0000") + "%";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLine,
                    (short)Visio.VisCellIndices.visLineColorTrans]
                        .FormulaU = transparency.ToString("0.0000") + "%";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseLineOpacity throws an exception.", ex, 1, true);

                return;
            }
        }
        public static string GetBaseLineTransparecy(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, opacity });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetBaseLineOpacity"))
            {
                return string.Empty;
            }

            try
            {

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetBaseLineOpacity"))
                {
                    return string.Empty;
                }

                return visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLine,
                    (short)Visio.VisCellIndices.visLineColorTrans]
                        .FormulaU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetBaseLineOpacity throws an exception.", ex, 1, true);

                return string.Empty;
            }
        }
        public static void SetShapeText(GraphicShape shape, string text, Color color, double sizeInPts)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, text, color, sizeInPts });
#endif
            

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeText"))
            {
                return;
            }

            try
            {
               // Visio.Layer layer = shape.GraphicsLayer != null ? shape.GraphicsLayer.GetBaseLayer().visioLayer : null;

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeText"))
                {
                    return;
                }

                lockedLayerSet.Clear();

                foreach (var graphicsLayerBase in shape.LayerSet)
                {
                    if (graphicsLayerBase.IsLocked())
                    {
                        addToLockedLayerSet(graphicsLayerBase);
                        graphicsLayerBase.UnLock();
                    }
                }
               
                bool shapeIsLocked = false;

                if (IsLocked(shape))
                {
                    shapeIsLocked = true;
                    UnlockShape(visioShape);
                }

                visioShape.Text = text;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionCharacter,
                    (short)0,
                    (short)Visio.VisCellIndices.visCharacterSize]
                        .FormulaU = sizeInPts.ToString() + " pt";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionCharacter,
                    (short)0,
                    (short)Visio.VisCellIndices.visCharacterColor]
                        .FormulaU = "THEMEGUARD(RGB(" + color.R + "," + color.G + "," + color.B + "))";

                foreach (GraphicsLayerBase graphicsLayerBase in lockedLayerSet)
                {
                    graphicsLayerBase.Lock();
                }

                if (shapeIsLocked)
                {
                    LockShape(shape);
                }

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeText throws an exception.", ex, 1, true);
            }
        }


        public static void SetTextStyle(GraphicShape shape, short shapeTextStyle)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, shapeTextStyle });
#endif

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetTextStyle"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                //visioShape.Characters.CharProps[(short)Visio.VisCellIndices.visCharacterStyle] = shapeTextStyle;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionCharacter
                    , (short)0
                    , (short)Visio.VisCellIndices.visCharacterStyle].FormulaU = shapeTextStyle.ToString();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTextStyle throws an exception.", ex, 1, true);
            }
        }

        public static void SetTextColor(GraphicShape shape, Color color)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetTextColor"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            try
            {
                string colorFormula = "RGB(" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + ")";

                visioShape.CellsU["Chars.Color"].FormulaU = colorFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:SetBaseLineColor", ex, 1, true);
            }
        }


        public static void SetTextFontSize(GraphicShape shape, int fontSize)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, fontSize });
#endif

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetTextFontSize"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionCharacter,
                    (short)0,
                    (short)Visio.VisCellIndices.visCharacterSize]
                        .FormulaU = fontSize + " pt";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTextFontSize throws an exception.", ex, 1, true);
            }
        }

        public static void SetTextHorizontalAlignment(GraphicShape shape, int alignment)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, alignment });
#endif
            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetTextHorizontalAlignment"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                visioShape.CellsSRC[
                   (short)Visio.VisSectionIndices.visSectionParagraph,
                   (short)0,
                   (short)Visio.VisCellIndices.visHorzAlign]
                       .FormulaU = alignment.ToString();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTextHorizontalAlignment throws an exception.", ex, 1, true);
            }
        }


        public static void SetTextVerticalAlignment(GraphicShape shape, int alignment)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, alignment });
#endif
            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetTextVerticalAlignment"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                visioShape.CellsSRC[
                   (short)Visio.VisSectionIndices.visSectionObject,
                   (short)Visio.VisRowIndices.visRowText,
                   (short)Visio.VisCellIndices.visTxtBlkVerticalAlign]
                       .FormulaU = alignment.ToString();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTextVerticalAlignment throws an exception.", ex, 1, true);
            }
        }

        internal static void SetBaseLineStyle(GraphicShape shape, VisioLineStyle visioLineStyle)
        {
            SetBaseLineStyle(shape, ((int)visioLineStyle).ToString());
        }

        public static double[] GetShapeBoundary(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            try
            {
                if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeBoundary"))
                {
                    return null;
                }

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetShapeBoundary"))
                {
                    return null;
                }

                Visio.Paths visioPaths = visioShape.Paths;

                if (visioPaths.Count <= 0)
                {
                    return null;
                }

                // There is an assumption here that the shape only has a boundary, not any internal paths

                if (visioPaths.Count > 1)
                {
                    Tracer.TraceGen.TraceError("In VisioInterop:GetShapeBoundary shape has too many paths", 1, true);
                }

                Visio.Path visioPath = visioPaths[1];

                Array xyArray;

                visioPath.Points(0.01, out xyArray);

                double[] shapeBoundaryPoints = new double[xyArray.Length];

                Array.Copy(xyArray, shapeBoundaryPoints, xyArray.Length);

                return shapeBoundaryPoints;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeBoundary throws an exception.", ex, 1, true);

                return null;
            }
        }

        public static DirectedPolygon GetShapeBoundaries(GraphicShape shape, List<DirectedPolygon> internalBoundaries)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeBoundaries"))
            {
                return null;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetShapeBoundaries"))
            {
                return null;
            }

            #endregion

            try
            {
                internalBoundaries.Clear();

                Visio.Paths visioPaths = visioShape.Paths;

                if (visioPaths.Count <= 0)
                {
                    return null;
                }

                List<DirectedPolygon> boundaryList = new List<DirectedPolygon>();

                foreach (Visio.Path visioPath in visioPaths)
                {
                    DirectedPolygon directedPolygon = visioPathToDirectedPolygon(visioPath);

                    boundaryList.Add(directedPolygon);
                }

                if (boundaryList.Count <= 0)
                {
                    return null;
                }

                if (boundaryList.Count == 1)
                {
                    return boundaryList[0];
                }

                DirectedPolygon externalBoundary = getContainingBoundary(boundaryList);

                foreach (DirectedPolygon directedPolygon in boundaryList)
                {
                    if (directedPolygon != externalBoundary)
                    {
                        internalBoundaries.Add(directedPolygon);
                    }
                }

                // Mdd reset

                //internalBoundaries.Clear();

                return externalBoundary;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeBoundaries throws an exception.", ex, 1, true);

                return null;
            }
        }

        private static DirectedPolygon visioPathToDirectedPolygon(Visio.Path visioPath)
        {
            Array xyArray;

            visioPath.Points(0.01, out xyArray);

            double[] shapeBoundaryPoints = new double[xyArray.Length - 2];

            Array.Copy(xyArray, shapeBoundaryPoints, xyArray.Length - 2);

            DirectedPolygon directedPolygon = new DirectedPolygon(shapeBoundaryPoints);

            return directedPolygon;
        }

        private static DirectedPolygon getContainingBoundary(List<DirectedPolygon> boundaryList)
        {
            if (boundaryList is null)
            {
                return null;
            }

            if (boundaryList.Count <= 0)
            {
                return null;
            }

            if (boundaryList.Count == 1)
            {
                return boundaryList[0];
            }

            // Visio usually returns the outer boundary as the first shape. But the following guarantees the outer boundary.

            for (int i1 = 0; i1 < boundaryList.Count; i1++)
            {
                bool isOuter = true;

                for (int i2 = 0; i2 < boundaryList.Count; i2++)
                {
                    if (i1 == i2)
                    {
                        continue;
                    }

                    if (!boundaryList[i1].Contains(boundaryList[i2]))
                    {
                        isOuter = false;

                        break;
                    }
                }

                if (isOuter)
                {
                    return boundaryList[i1];
                }
            }

            return null;
        }

        public static void SetBaseLineStyle(GraphicShape shape, int lineStyleFormula)
        {
            SetBaseLineStyle(shape, lineStyleFormula.ToString());
        }

        public static void SetBaseLineStyle(GraphicShape shape, string lineStyleFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, lineStyleFormula });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseLineStyle"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetBaseLineStyle"))
                {
                    return;
                }

                visioShape.Cells["LinePattern"].FormulaU = lineStyleFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseLineStyle throws an exception.", ex, 1, true);
            }
        }
        public static void SetShapeLineVisibility(IGraphicsShape iShape, bool visible)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { iShape, visible });
#endif

            #region Validations

            if (!VisioValidations.ValidateIShapeParm(iShape, "VisioInterop:SetShapeLineVisibility"))
            {
                return;
            }

            #endregion

            GraphicShape shape = iShape.Shape;

            SetShapeLineVisibility(shape, visible);
        }


        public static void SetShapeLineVisibility(GraphicShape shape, bool visible)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, visible });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeLineVisibility"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeLineVisibility"))
                {
                    return;
                }

                visioShape.Cells["LineColorTrans"].ResultIU = visible ? 0.0 : 1.0;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeLineVisibility throws an exception.", ex, 1, true);
            }
        }



        public static double? GetShapeLineVisibility(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, visible });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeLineVisibility"))
            {
                return null;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeLineVisibility"))
                {
                    return null;
                }

                return visioShape.Cells["LineColorTrans"].ResultIU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeLineVisibility throws an exception.", ex, 1, true);

                return null;
            }
        }
        public static void SetShapeTextVisibility(GraphicShape shape, bool visible)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, visible });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeTextVisibility"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(shape.VisioShape, "VisioInterop:SetShapeTextVisibility"))
                {
                    return;
                }

                visioShape.Cells["TextBkgndTrans"].ResultIU = visible ? 0.0 : 1.0;
                visioShape.Cells["HideText"].ResultIU = visible ? 1 : 0;
                visioShape.Cells["Height"].ResultIU = 0;
                visioShape.Cells["Width"].ResultIU = 0;
            }


            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeTextVisibility throws an exception.", ex, 1, true);
            }
        }

        public static void SetBaseFillColor(GraphicShape shape, Color fillColor)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, fillColor });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseFillColor"))
            {
                return;
            }

            try
            {
                if (!VisioValidations.VisioShapeNotDeleted(shape.VisioShape, "VisioInterop:SetBaseFillColor"))
                {
                    return;
                }

                string colorFormula = string.Format("THEMEGUARD(RGB({0},{1},{2}))", fillColor.R, fillColor.G, fillColor.B);

                SetBaseFillColor(shape, colorFormula);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseFillColor throws an exception.", ex, 1, true);
            }
        }

        public static void SetPatternColor(GraphicShape shape, Color fillColor)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, fillColor });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetPatternColor"))
            {
                return;
            }

            try
            {
                if (!VisioValidations.VisioShapeNotDeleted(shape.VisioShape, "VisioInterop:SetPatternColor"))
                {
                    return;
                }

                string colorFormula = string.Format("THEMEGUARD(RGB({0},{1},{2}))", fillColor.R, fillColor.G, fillColor.B);

                SetPatternColor(shape, colorFormula);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetPatternColor throws an exception.", ex, 1, true);
            }
        }

        internal static void SetLineCompoundType(GraphicShape shape, string compoundTypeFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, compoundTypeFormula });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetLineCompoundType"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetLineCompoundType"))
                {
                    return;
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLine,
                    (short)Visio.VisCellIndices.visCompoundType]
                        .FormulaU = compoundTypeFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetLineCompoundType throws an exception.", ex, 1, true);
            }
        }

        public static void SetBaseFillColor(GraphicShape shape, string colorFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, colorFormula });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetBaseFillColor"))
            {
                return;
            }

            try
            {

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetBaseFillColor"))
                {
                    return;
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillForegnd]
                        .FormulaU = colorFormula;

                //string visioFillColorFormula =
                //string.Format("THEMEGUARD(RGB({0},{1},{2}))", 255, 255, 255);

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgnd]
                        .FormulaU = colorFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetBaseFillColor throws an exception.", ex, 1, true);
            }
        }

        public static void SetPatternColor(GraphicShape shape, string colorFormula)
        {
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetPatternColor"))
            {
                return;
            }

            try
            {

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetPatternColor"))
                {
                    return;
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillForegnd]
                        .FormulaForceU = colorFormula;

                string visioFillColorFormula =
                string.Format("THEMEGUARD(RGB({0},{1},{2}))", 255, 255, 255);

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgnd]
                        .FormulaForceU = visioFillColorFormula;


            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetPatternColor throws an exception.", ex, 1, true);
            }
        }
        public static GraphicsSelection SpatialSearch(
            GraphicsWindow window
            , GraphicsPage page
            , double x
            , double y
            , short spatialSearchParm
            , double v1
            , int v2)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, page, x, y, spatialSearchParm, v1, v2 });
#endif

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:SpatialSearch"))
            {
                return new GraphicsSelection();
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SpatialSearch"))
            {
                return new GraphicsSelection();
            }

            #endregion

            try
            {
                GraphicsSelection selection = new GraphicsSelection();

                Visio.Page visioPage = page.VisioPage;

                Visio.Selection visioSelection = visioPage.SpatialSearch[x, y, spatialSearchParm, 0.01, 0];

                foreach (Visio.Shape visioShape in visioSelection)
                {


                    GraphicShape shape = page.PageShapeDictGetShape(visioShape.Data3);

                    if (shape is null)
                    {
                        // This should never happen
                        
                        shape = new GraphicShape(null, window, page, visioShape, ShapeType.Unknown, visioShape.Data3);
                    }
                        
                    selection.Add(shape);
                }

                return selection;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SpatialSearch throws an exception.", ex, 1, true);

                return new GraphicsSelection();
            }
        }

        public static List<string> SpatialSearchGuidList(
            GraphicsWindow window
            , GraphicsPage page
            , double x
            , double y
            , short spatialSearchParm
            , double v1
            , int v2)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, page, x, y, spatialSearchParm, v1, v2 });

            List<string> rtrnList = new List<string>();

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:SpatialSearchGuidList"))
            {
                return rtrnList;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SpatialSearchGuidList"))
            {
                return rtrnList;
            }

            #endregion

            try
            {
                Visio.Page visioPage = page.VisioPage;

                Visio.Selection visioSelection = visioPage.SpatialSearch[x, y, spatialSearchParm, 0.01, 0];

                foreach (Visio.Shape visioShape in visioSelection)
                {
                    rtrnList.Add(visioShape.Data3);
                }

                return rtrnList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SpatialSearch throws an exception.", ex, 1, true);

                return rtrnList;
            }
        }

        public static void SendToBack(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SendToBack"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SendToBack"))
            {
                return;
            }

            try
            {
                visioShape.SendToBack();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SendToBack throws an exception.", ex, 1, true);
            }
        }

        public static void SetYLocation(GraphicShape shape, double yLoc)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, yLoc });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetYLocation"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetYLocation"))
            {
                return;
            }

            try
            {
                double height = visioShape.CellsU["Height"].ResultIU;

                visioShape.CellsU["PinY"].ResultIU = yLoc - 0.5 * height;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetYLocation throws an exception.", ex, 1, true);
            }

        }

        public static string GetSelectedShapeData1(GraphicsWindow window)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window });
#endif

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:GetSelectedShapeData1"))
            {
                return null;
            }

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                if (visioWindow.Selection.Count <= 0)
                {
                    return null;
                }

                Visio.Shape visioShape = visioWindow.Selection[1];

                return visioShape.Data1;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetSelectedShapeData1 throws an exception.", ex, 1, true);

                return null;
            }
        }

        public static string GetSelectedShapeGuid(GraphicsWindow window)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window });
#endif

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:GetSelectedShapeGuid"))
            {
                return null;
            }

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                if (visioWindow.Selection.Count <= 0)
                {
                    return null;
                }

                Visio.Shape visioShape = visioWindow.Selection[1];

                return visioShape.Data3;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetSelectedShapeGuid throws an exception.", ex, 1, true);

                return null;
            }
        }

        public static Visio.Shape GetSelectedShape(GraphicsWindow window)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window });
#endif

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:GetSelectedShape"))
            {
                return null;
            }

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                if (visioWindow.Selection.Count <= 0)
                {
                    return null;
                }

                Visio.Shape visioShape = visioWindow.Selection[1];

                return visioShape;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetSelectedShape throws an exception.", ex, 1, true);

                return null;
            }
        }

        public static void DeleteLayer(GraphicsLayer layer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer });
#endif

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:DeleteLayer"))
            {
                return;
            }

            try
            {
                Visio.Layer visioLayer = layer.visioLayer;

                DeleteLayer(visioLayer);

                layer.visioLayer = null;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:DeleteLayer throws an exception.", ex, 1, true);
            }
        }

        public static void DeleteLayer(Visio.Layer layer, bool ignoreComException = true)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer, ignoreComException });
#endif
            try
            {
                // The following is set up  to not delete the shapes assigned to the layer (argument: 0).
                // Deleting the shapes here may result in inconsistencies with the rest of the system and
                // result in memory leaks.

                if (IsLocked(layer))
                {
                    UnlockLayer(layer);
                }

                layer.Delete(0);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:DeleteLayer throws an exception.", ex, 1, true);

                if (ignoreComException)
                {
                    return;
                }

                throw new Exception("Attempt to delete layer failed: " + ex.Message, ex);
            }
        }

        public static void SetLineRounding(GraphicShape shape, string roundingFactor)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, roundingFactor });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetLineRounding"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetFillPattern"))
            {
                return;
            }

            visioShape.CellsSRC[
                     (short)Visio.VisSectionIndices.visSectionObject,
                     (short)Visio.VisRowIndices.visRowLine,
                     (short)Visio.VisCellIndices.visLineRounding]
                         .Formula = roundingFactor;

        }


        public static void SetFillPattern(GraphicShape shape, string fillPattern, Color? color = null)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, fillPattern });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetFillPattern"))
            {
                return;
            }

            try
            {
                // MDD Reset this if necessary
                //if (shape.IsLocked)
                //{
                //    return;
                //}

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetFillPattern"))
                {
                    return;
                }

                visioShape.CellsSRC[
                         (short)Visio.VisSectionIndices.visSectionObject,
                         (short)Visio.VisRowIndices.visRowFill,
                         (short)Visio.VisCellIndices.visFillPattern]
                             .FormulaU = fillPattern;

                if (fillPattern == "0")
                {
                    if (color.HasValue)
                    {
                        SetBaseFillColor(shape, color.Value);
                    }

                    return;
                }

                if (color.HasValue)
                {
                    string colorStr = string.Format("THEMEGUARD(RGB({0},{1},{2}))", color.Value.R, color.Value.G, color.Value.B);

                    visioShape.CellsSRC[
                        (short)Visio.VisSectionIndices.visSectionObject,
                        (short)Visio.VisRowIndices.visRowFill,
                        (short)Visio.VisCellIndices.visFillForegnd]
                        .FormulaU = colorStr;
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgnd]
                            .FormulaU = "THEMEGUARD(RGB(255,255,255))";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetFillPattern throws an exception.", ex, 1, true);
            }
        }

        public static void SetShapeData1(GraphicShape shape, string data1)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, data1 });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeData1"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeData1"))
                {
                    return;
                }

                if (string.IsNullOrEmpty(data1))
                {
                    data1 = string.Empty;
                }

                visioShape.Data1 = data1;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetVisioShapeData1 throws and exception.", ex, 1, true);
            }
        }

        public static void SetShapeData2(GraphicShape shape, string data2)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, data2 });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeData2"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeData2"))
                {
                    return;
                }

                if (string.IsNullOrEmpty(data2))
                {
                    data2 = string.Empty;
                }

                visioShape.Data2 = data2;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetVisioShapeData2 throws and exception.", ex, 1, true);
            }
        }

        public static void SetShapeData3(GraphicShape shape, string Data3)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, Data3 });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeData3"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeData3"))
                {
                    return;
                }

                if (string.IsNullOrEmpty(Data3))
                {
                    Data3 = string.Empty;
                }

                visioShape.Data3 = Data3;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetVisioShapeData3 throws and exception.", ex, 1, true);
            }
        }

        public static void SetShapeData(
            GraphicShape shape
            , string data1
            , string data2
            , string data3)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, data1, data2, data3 });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeData"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeData"))
                {
                    return;
                }

                visioShape.Data1 = data1;
                visioShape.Data2 = data2;
                visioShape.Data3 = data3;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeData throws and exception.", ex, 1, true);
            }
        }
        public static void SetTakeoutAreaPattern(GraphicShape shape, Color foregroundColor, string fillPattern)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, foregroundColor, fillPattern });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetTakeoutAreaPattern"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetTakeoutAreaPattern"))
                {
                    return;
                }

                string colorFormula = string.Format("THEMEGUARD(RGB({0},{1},{2}))", foregroundColor.R, foregroundColor.G, foregroundColor.B);

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillForegnd]
                        .FormulaForceU = colorFormula;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgnd]
                        .FormulaForceU = "THEMEGUARD(RGB(255,255,255))";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillPattern]
                        .FormulaU = fillPattern;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillForegndTrans]
                        .FormulaU = "50%";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgndTrans]
                        .FormulaU = "50%";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTakeoutAreaPattern throws and exception.", ex, 1, true);
            }
        }

        public static void BringToFront(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:BringToFront"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:BringToFront"))
                {
                    return;
                }

                visioShape.BringToFront();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:BringToFront throws and exception.", ex, 1, true);
            }

        }

        public static double GetShapeArea(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeArea"))
            {
                return -1;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetShapeArea"))
                {
                    return -1;
                }

                // The following is part of an attempt to catch an insidious bug

                return visioShape.AreaIU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeArea throws and exception.", ex, 1, true);

                return -1.0;
            }
        }

        public static void SetFillOpacity(GraphicShape shape, double opacity)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, opacity });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetFillOpacity"))
            {
                return;
            }

            try
            {
                if (opacity < 0)
                {
                    opacity = 0.0;
                }

                else if (opacity > 1.0)
                {
                    opacity = 1.0;
                }

                double transparency = (1.0 - opacity) * 100.0;

                SetFillTransparency(shape, transparency.ToString("0.0000") + "%");
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetFillOpacity throws an exception.", ex, 1, true);
            }
        }

        public static void SetPatternOpacity(GraphicShape shape, double opacity)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, opacity });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetFillPatternOpacity"))
            {
                return;
            }

            try
            {
                if (opacity < 0)
                {
                    opacity = 0.0;
                }

                else if (opacity > 1.0)
                {
                    opacity = 1.0;
                }

                double transparency = (1.0 - opacity) * 100.0;

                SetPatternTransparency(shape, transparency.ToString("0.0000") + "%");
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetFillPatternOpacity throws an exception.", ex, 1, true);
            }
        }

        public static void SetFillTransparency(GraphicShape shape, string visioFillTransparencyFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, visioFillTransparencyFormula });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetFillTransparancey"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetFillTransparancey"))
                {
                    return;
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillPattern]
                        .FormulaU = "1";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillForegndTrans]
                        .FormulaU = visioFillTransparencyFormula;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgndTrans]
                        .FormulaU = visioFillTransparencyFormula;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillGradientEnabled]
                        .FormulaU = "FALSE";

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetFillTransparency throws an exception.", ex, 1, true);
            }
        }

        public static void SetPatternTransparency(GraphicShape shape, string visioFillTransparencyFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, visioFillTransparencyFormula });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetFillPatternTransparancey"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetFillPatternTransparancey"))
                {
                    return;
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillForegndTrans]
                        .FormulaForceU = visioFillTransparencyFormula;

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillBkgndTrans]
                        .FormulaForceU = visioFillTransparencyFormula;

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetFillTransparency throws an exception.", ex, 1, true);
            }
        }

        public static void SetLineWidth(GraphicShape shape, double lineWidthInPts)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, lineWidthInPts });
#endif
            try
            {
                if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetLineWidth"))
                {
                    return;
                }

                Visio.Shape visioShape = shape.VisioShape;


                visioShape.Cells["LineWeight"].FormulaU = lineWidthInPts.ToString("0.00") + " pt";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:SetLineWidth", ex, 1, true);
            }
        }

        public static string GetLineWidth(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            try
            {
                if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetLineWidth"))
                {
                    return string.Empty;
                }

                Visio.Shape visioShape = shape.VisioShape;


                return visioShape.Cells["LineWeight"].FormulaU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:GetLineWidth", ex, 1, true);
                return string.Empty;
            }


        }

        public static Coordinate GetShapeBeginPoint(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeBeginPoint"))
            {
                return Coordinate.NullCoordinate;
            }

            #endregion

            try
            {


                Visio.Shape visioShape = shape.VisioShape;

                return new Coordinate(visioShape.Cells["BeginX"].ResultIU, visioShape.Cells["BeginY"].ResultIU);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeBeginPoint throws an exception.", ex, 1, true);

                return Coordinate.NullCoordinate;
            }
        }

        public static Coordinate GetShapeEndPointd(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeEndPoint"))
            {
                return Coordinate.NullCoordinate;
            }

            #endregion

            try
            {


                Visio.Shape visioShape = shape.VisioShape;

                return new Coordinate(visioShape.Cells["EndX"].ResultIU, visioShape.Cells["EndY"].ResultIU);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeEndPoint throws an exception.", ex, 1, true);

                return Coordinate.NullCoordinate;
            }

        }

        public static void SetNolineMode(GraphicShape shape, bool hideLine = true)
        {
            if (shape is null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape is null)
            {
                return;
            }

            if (hideLine)
            {
                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLine,
                    (short)Visio.VisCellIndices.visLinePattern]
                        .FormulaU = "0";

            }

            else
            {
                visioShape.CellsSRC[
                   (short)Visio.VisSectionIndices.visSectionObject,
                   (short)Visio.VisRowIndices.visRowLine,
                   (short)Visio.VisCellIndices.visLinePattern]
                       .FormulaU = "1";
            }

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowGradientProperties,
                (short)Visio.VisCellIndices.visLineGradientEnabled]
                    .FormulaU = "FALSE";
        }

        #region Layer related functionality

        public static void LockLayer(GraphicsLayer layer)
        {

#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer });
#endif
            // MDD Reset 2024-12-30

            //return;

            #region Validations

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:LockLayer"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Layer visioLayer = layer.visioLayer;

                if (IsLocked(visioLayer))
                {
                    return;
                }

                 visioLayer.CellsC[(short)Visio.VisCellIndices.visLayerLock].FormulaU = "1";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:LockLayer throws an exception.", ex, 1, true);
            }
        }

        public static void LockLayer(Visio.Layer visioLayer)
        {
            // MDD Reset 2024-12-30

            //return;

            if (IsLocked(visioLayer))
            {
                return;
            }

            visioLayer.CellsC[(short)Visio.VisCellIndices.visLayerLock].FormulaU = "1";
        }

        public static void UnlockLayer(GraphicsLayer layer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer });
#endif
            //if (layer.LayerName.Contains("[CutsIndexLayer]"))
            //{
            //    ;
            //}

            #region Validations

            if (!VisioValidations.ValidateLayerParm(layer, "VisioInterop:UnlockLayer"))
            {
                return;
            }

            #endregion

            try
            {
                UnlockLayer(layer.visioLayer);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:UnlockLayer throws an exception.", ex, 1, true);
            }
        }

        public static void UnlockLayer(Visio.Layer visioLayer)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { visioLayer });
#endif

            #region Validations

            if (visioLayer is null)
            {
                Tracer.TraceGen.TraceError("VisioInterop:UnlockLayer called with null layer.", 1, true);

                return;
            }

            #endregion

            try
            {

                if (!IsLocked(visioLayer))
                {
                    return;
                }

                visioLayer.CellsC[(short)Visio.VisCellIndices.visLayerLock].FormulaU = "0";
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:UnlockLayer throws an exception.", ex, 1, true);
            }
        }

        public static void SetLayerOpacity(GraphicsLayer layer, double opacity) //Visio.Layer layer, double opacity)
        {
            if (layer is null)
            {
                return;
            }

            Visio.Layer visioLayer = layer.visioLayer;

            if (visioLayer is null)
            {
                return;
            }

            opacity = Math.Max(0, Math.Min(1.0, opacity));

            visioLayer.CellsC[(short)Visio.VisCellIndices.visLayerColorTrans].FormulaU = (100.0 * (1.0 - opacity)).ToString("0") + '%';
        }

        public static void SendToBack(Visio.Shape visioShape)
        {
            visioShape.SendToBack();
        }

        public static ShapeSize GetShapeDimensions(GraphicShape shape)
        {
            if (shape is null)
            {
                return new ShapeSize(-1, -1);
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape is null)
            {
                return new ShapeSize(-1, -1);
            }

            return GetShapeDimensions(visioShape);
        }

        public static ShapeSize GetShapeDimensions(Visio.Shape visioShape)
        {
            double width = visioShape.Cells["width"].ResultIU;
            double height = visioShape.Cells["height"].ResultIU;

            return new ShapeSize(width, height);
        }

        public static void SetEndpointArrows(GraphicShape shape, int arrowIndex)
        {
            Visio.Shape visioShape = shape.VisioShape;

            string StrArrowIndex = arrowIndex.ToString();

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineBeginArrow]
                    .FormulaU = StrArrowIndex;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineEndArrow]
                    .FormulaU = StrArrowIndex;
        }

        public static void SetStartpointArrow(GraphicShape shape, int arrowIndex, int arrowSize)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, arrowIndex, arrowSize });

            Visio.Shape visioShape = shape.VisioShape;

            string StrArrowIndex = arrowIndex.ToString();
            string strArrowSize = arrowSize.ToString();

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineBeginArrow]
                    .FormulaU = StrArrowIndex;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineBeginArrowSize]
                    .FormulaU = strArrowSize;
        }

        public static void SetEndpointArrow(GraphicShape shape, int arrowIndex, int arrowSize)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, arrowIndex, arrowSize });

            Visio.Shape visioShape = shape.VisioShape;

            string StrArrowIndex = arrowIndex.ToString();
            string strArrowSize = arrowSize.ToString();

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineEndArrowSize]
                    .FormulaU = StrArrowIndex;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineEndArrowSize]
                    .FormulaU = strArrowSize;
        }

        public static void SetPageGrid1(GraphicsPage page, double spacing, double gridOffset)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page, spacing, gridOffset });
#endif

            #region Validations

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetPageGrid1"))
            {
                return;
            }

            if (spacing <= 0)
            {
                Tracer.TraceGen.TraceError("Non-positive spacing count in call to VisioInterop:SetPageGrid1");

                return;
            }

            #endregion

            try
            {
                Visio.Page visioPage = page.VisioPage;


                visioPage.PageSheet.CellsU["XGridDensity"].ResultIU = 0;

                visioPage.PageSheet.CellsU["YGridDensity"].ResultIU = 0;

                visioPage.PageSheet.CellsU["XGridSpacing"].ResultIU = spacing;

                visioPage.PageSheet.CellsU["YGridSpacing"].ResultIU = spacing;

                visioPage.PageSheet.CellsU["XGridOrigin"].ResultIU = gridOffset;

                visioPage.PageSheet.CellsU["YGridOrigin"].ResultIU = gridOffset;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetPageGrid1 throws an exception.", ex, 1, true);

                return;
            }
        }

        public static double SetPageGrid(GraphicsPage page, double gridCount, double gridOffset)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page, gridCount, gridOffset });

            #region Validations

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetPageGrid"))
            {
                return double.NaN;
            }

            if (gridCount <= 0)
            {
                Tracer.TraceGen.TraceError("Non-positive grid count in call to VisioInterop:SetPageGrid");

                return double.NaN;
            }

            #endregion

            try
            {
                Visio.Page visioPage = page.VisioPage;

                double height = visioPage.PageSheet.CellsU["PageHeight"].ResultIU - 2 * gridOffset;

                double spacing = height / gridCount;

                visioPage.PageSheet.CellsU["XGridDensity"].ResultIU = 0;

                visioPage.PageSheet.CellsU["YGridDensity"].ResultIU = 0;

                visioPage.PageSheet.CellsU["XGridSpacing"].ResultIU = spacing;

                visioPage.PageSheet.CellsU["YGridSpacing"].ResultIU = spacing;

                visioPage.PageSheet.CellsU["XGridOrigin"].ResultIU = gridOffset;

                visioPage.PageSheet.CellsU["YGridOrigin"].ResultIU = gridOffset;

                return spacing;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetPageGrid throws an exception.", ex, 1, true);

                return double.NaN;
            }
        }

        //public static void SetPageScale(GraphicsPage graphicsPage, double scale)
        //{
        //    if (graphicsPage is null)
        //    {
        //        return;
        //    }

        //    Visio.Page visioPage = graphicsPage.VisioPage;

        //    if (visioPage is null)
        //    {
        //        return;
        //    }


        //    visioPage.PageSheet.CellsU["PageScale"].ResultIU = scale;

        //}

        internal static void SetPageSize(GraphicsPage page, double width, double height)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page, width, height });
#endif

            #region Validations

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetPageSize"))
            {
                return;
            }

            if (width <= 0)
            {
                Tracer.TraceGen.TraceError("Non-positive width passed to VisioInterop:SetPageSize", 1, false);

                return;
            }

            if (height <= 0)
            {
                Tracer.TraceGen.TraceError("Non-positive height passed to VisioInterop:SetPageSize", 1, false);

                return;
            }

            #endregion

            try
            {
                Visio.Page visioPage = page.VisioPage;

                visioPage.PageSheet.CellsU["PageHeight"].ResultIU = height;
                visioPage.PageSheet.CellsU["PageWidth"].ResultIU = width;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetPageSize throws an exception.", ex, 1, true);
            }
        }

        public static void SetPageSize(GraphicsPage page, ShapeSize size)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page, size });

            #region Validations

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetPageSize"))
            {
                return;
            }

            double width = Math.Ceiling(size.Width * 1.0e2) * 1.0e-2;
            double height = Math.Ceiling(size.Height * 1.0e2) * 1.0e-2;

            if (width <= 0)
            {
                Tracer.TraceGen.TraceError("Non-positive width passed to VisioInterop:SetPageSize", 1, false);

                return;
            }

            if (height <= 0)
            {
                Tracer.TraceGen.TraceError("Non-positive height passed to VisioInterop:SetPageSize", 1, false);

                return;
            }

            #endregion

            try
            {
                SetPageSize(page, width, height);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetPageSize throws an exception.", ex, 1, true);
            }
        }

        //public static void ResizeToFitContents(GraphicsPage Page)
        //{
        //    if (Page is null)
        //    {
        //        return;
        //    }

        //    Visio.Page visioPage = Page.VisioPage;

        //    if (visioPage is null)
        //    {
        //        return;
        //    }

        //    visioPage.ResizeToFitContents();
        //}

        //public static void SetYAxis(Visio.Window visioWindow, ShapeSize size)
        //{
        //    double height = Math.Ceiling(size.Height * 1.0e8) * 1.0e-8;

        //    visioWindow.Shape.CellsSRC[
        //        (short)Visio.VisSectionIndices.visSectionObject,
        //        (short)Visio.VisRowIndices.visRowRulerGrid,
        //        (short)Visio.VisCellIndices.visYRulerOrigin]
        //            .FormulaU = height.ToString("0.00000000") + " in";
        //}

        public static void SetShapeLocation(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeLocation"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (visioShape is null)
                {
                    return;
                }

                double locX = visioShape.Cells["pinx"].ResultIU;
                double locY = visioShape.Cells["piny"].ResultIU;

                ShapeSize size = GetShapeDimensions(visioShape);

                locX += size.Width * 0.5 - locX;
                locY += size.Height * 0.5 - locY;

                visioShape.Cells["pinx"].ResultIU = locX;
                visioShape.Cells["piny"].ResultIU = locY;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeLocation throws an exception.", ex, 1, true);
            }
        }

        //public static void SetViewRect(GraphicsWindow graphicsWindow, double left, double top, double  width, double height)
        //{
        //    if (graphicsWindow is null)
        //    {
        //        return;
        //    }

        //    Visio.Window visioWindow = graphicsWindow.VisioWindow;

        //    if (visioWindow is null)
        //    {
        //        return;
        //    }

        //    visioWindow.SetViewRect(left, top, width, height);
        //}
        //public static void SetWindowRect(GraphicsWindow graphicsWindow, double left, double top, double width, double height)
        //{
        //    if (graphicsWindow is null)
        //    {
        //        return;
        //    }

        //    Visio.Window visioWindow = graphicsWindow.VisioWindow;

        //    if (visioWindow is null)
        //    {
        //        return;
        //    }

        //    //visioWindow.AllowEditing = false;

        //    visioWindow.SetWindowRect(
        //        (int)Math.Ceiling(left)
        //        , (int)Math.Ceiling(top)
        //        , (int)Math.Ceiling(width)
        //        , (int)Math.Ceiling(height));

        //}

        public static void SetLineText(GraphicShape shape, string lineText)
        {
            Visio.Shape visioShape = shape.VisioShape;

            visioShape.Text = lineText;
        }

        public static void SetLineBeginShape(GraphicShape shape, int beginShapeIndex, int beginShapeSize)
        {
            Visio.Shape visioShape = shape.VisioShape;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineBeginArrow]
                    .FormulaU = beginShapeIndex.ToString();

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineBeginArrowSize]
                    .FormulaU = beginShapeSize.ToString();
        }

        public static void SetLineEndShape(GraphicShape shape, int endShapeIndex, int endShapeSize)
        {
            Visio.Shape visioShape = shape.VisioShape;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineEndArrow]
                    .FormulaU = endShapeIndex.ToString();

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineEndArrowSize]
                    .FormulaU = endShapeSize.ToString();
        }

        #endregion

        public static void FormatCutBox(GraphicShape shape)
        {
            Visio.Shape visioShape = shape.VisioShape;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowFill,
                (short)Visio.VisCellIndices.visFillPattern]
                    .FormulaU = "0";

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowGradientProperties,
                (short)Visio.VisCellIndices.visFillGradientEnabled]
                    .FormulaU = "FALSE";

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineColor]
                      .FormulaU = "THEMEGUARD(RGB(0,255,0))";

            visioShape.Cells["LineWeight"].FormulaU = "2 pt";


        }

        public static void SetShapeLocationAndLocation(GraphicShape shape, Coordinate coord)
        {
            SetShapeLocationAndOrientation(shape, coord.X, coord.Y);
        }

        public static void SetShapeLocationAndOrientation(GraphicShape shape, double x, double y, int btn = 2)
        {

            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }
            if (btn == 2)
            {
                visioShape.Cells["Angle"].ResultIU = 1.5707963267949;
            }
            else
            {
                visioShape.Cells["Angle"].ResultIU = 0;
            }
            visioShape.Cells["PinX"].ResultIU = x;
            visioShape.Cells["PinY"].ResultIU = y;
        }

        public static void SetShapeLocation(GraphicShape shape, double x, double y)
        {

            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            visioShape.Cells["PinX"].ResultIU = x;
            visioShape.Cells["PinY"].ResultIU = y;
        }

        public static void SetLineStartpoint(GraphicShape shape, double x, double y)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            visioShape.Cells["BeginX"].ResultIU = x;
            visioShape.Cells["BeginY"].ResultIU = y;
        }

        public static void SetLineStartpoint(GraphicShape shape, Coordinate coord) => SetLineStartpoint(shape, coord.X, coord.Y);

        public static void SetLineEndpoint(GraphicShape shape, double x, double y)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            visioShape.Cells["EndX"].ResultIU = x;
            visioShape.Cells["EndY"].ResultIU = y;
        }

        public static void SetLineEndpoint(GraphicShape shape, Coordinate coord) => SetLineEndpoint(shape, coord.X, coord.Y);

        public static double GetShapeAngle(GraphicShape shape)
        {
            if (shape == null)
            {
                return double.NaN;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return double.NaN;
            }

            double angle = visioShape.Cells["Angle"].ResultIU;

            return angle;
        }

        public static void SetShapeAngle(GraphicShape shape, double angle)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            visioShape.Cells["Angle"].ResultIU = angle;
        }

        public static void SetShapeSize(GraphicShape shape, double height, double width)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, ignoreComException });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeSize"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                visioShape.Cells["Height"].ResultIU = height;
                visioShape.Cells["Width"].ResultIU = width;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:SetShapeSize", ex, 1, true);
            }
        }

        private static HashSet<GraphicsLayerBase> lockedLayerSet = new HashSet<GraphicsLayerBase>();

        private static bool addToLockedLayerSet(GraphicsLayerBase graphicsLayerBase)
        {
            if (lockedLayerSet.Contains(graphicsLayerBase))
            {
                return false;
            }

            lockedLayerSet.Add(graphicsLayerBase);

            return true;
        }

        public static void DeleteShape(GraphicShape shape, bool ignoreComException = true)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, ignoreComException });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:DeleteShape"))
            {
                return;
            }


            string data1 = shape.Data1;


            Visio.Shape visioShape = null;

            try
            {
                
                if (shape is null)
                {
                    return;
                }


                visioShape = shape.VisioShape;

                if (visioShape is null)
                {
                    return;
                }

                //GraphicsLayerBase lockedLayer = null;

                // Strictly speaking, shape should not be in any layers at this point. The following is
                // defensive code.

                lockedLayerSet.Clear();

                //if (shape.GraphicsLayer != null)
                //{
                //    if (shape.GraphicsLayer.IsLocked())
                //    {
                //        // shape.GraphicsLayer should be removed at some point. It was a bug
                //        // due to the assumption that a shape would only belong to one layer.
                //        // It should be substituted with the use of shape.LayerSet

                //        shape.GraphicsLayer.UnLock();
                //        addToLockedLayerSet(shape.GraphicsLayer);
                //    }
                //}

                foreach (var graphicsLayerBase in shape.LayerSet)
                {
                    if (graphicsLayerBase.IsLocked())
                    {
                        if (lockedLayerSet.Contains(graphicsLayerBase))
                        {
                            continue;
                        }

                        graphicsLayerBase.UnLock();
                        addToLockedLayerSet(graphicsLayerBase);
                    }

                }

                DeleteShape(visioShape, ignoreComException);

                foreach (var graphicsLayerBase in lockedLayerSet)
                {
                    graphicsLayerBase.Lock();
                }

                shape.VisioShape = null;
            }

            catch (Exception ex)
            {
                /*mdd*/
                /*mdd*/
                string report = VisioInteropDebugger.DiagnoseDeleteBlockers(visioShape) + "\n" +
                                VisioInteropDebugger.ContainersReport(visioShape);
                //Console.WriteLine(report);

                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:DeleteShape", ex, 1, true);
            }
        }

        public static void DeleteShape(Visio.Shape visioShape, bool ignoreComException = true)
        {
            if (visioShape is null)
            {
                return;
            }

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:DeleteShape"))
            {
                return;
            }

            
            try
            {
          
                visioShape.Delete();
            }

            catch (Exception ex)
            {
                /*mdd*/
                string report = VisioInteropDebugger.DiagnoseDeleteBlockers(visioShape) + "\n" +
                                VisioInteropDebugger.ContainersReport(visioShape);

                //Console.WriteLine(report);
                Tracer.TraceGen.TraceException("Exception thrown in VisioInterop:DeleteShape", ex, 1, true);
            }
        }

        public static bool LayerContainsShape(Visio.Layer layer,  Visio.Shape visioShape)
        {
            if (visioShape.Cells["LayerMember"].Formula == layer.Index.ToString())
            {
                return true;
            }

            return false;
        }
        public static Coordinate GetLineStartpoint(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetLineStartpoint"))
            {
                return Coordinate.NullCoordinate;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                return GetLineStartpoint(visioShape);
                
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetLineStartpoint throws the exception.", ex, 1, true);

                return Coordinate.NullCoordinate;
            }
        }

        public static Coordinate GetLineStartpoint(Visio.Shape visioShape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            if (visioShape is null)
            {
                return Coordinate.NullCoordinate;
            }

            double x = visioShape.Cells["BeginX"].ResultIU;
            double y = visioShape.Cells["BeginY"].ResultIU;

            return new Coordinate(x, y);
        }

        public static Coordinate GetShapeUpperLeftLocation(GraphicShape shape)
        {
            if (shape is null)
            {
                return Coordinate.NullCoordinate;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape is null)
            {
                return Coordinate.NullCoordinate;
            }

            if (shape.ShapeType == ShapeType.Line)
            {
                var beginX = visioShape.Cells["BeginX"].Result[Visio.VisUnitCodes.visInches];
                var beginY = visioShape.Cells["BeginY"].Result[Visio.VisUnitCodes.visInches];

                return new Coordinate(beginX, beginY);
            }

            double xMid = visioShape.Cells["PinX"].ResultIU;
            double yMid = visioShape.Cells["PinY"].ResultIU;

            double width = visioShape.Cells["Width"].ResultIU;
            double hight = visioShape.Cells["Height"].ResultIU;

            return new Coordinate(xMid - width * 0.5, yMid + hight * 0.5);
        }

        public static Coordinate GetShapeLowerRightLocation(GraphicShape shape)
        {
            if (shape is null)
            {
                return Coordinate.NullCoordinate;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape is null)
            {
                return Coordinate.NullCoordinate;
            }

            if (shape.ShapeType == ShapeType.Line)
            {
                var endX = visioShape.Cells["EndX"].Result[Visio.VisUnitCodes.visInches];
                var endY = visioShape.Cells["EndY"].Result[Visio.VisUnitCodes.visInches];

                return new Coordinate(endX, endY);
            }

            double xMid = visioShape.Cells["PinX"].ResultIU;
            double yMid = visioShape.Cells["PinY"].ResultIU;

            double width = visioShape.Cells["Width"].ResultIU;
            double hight = visioShape.Cells["Height"].ResultIU;

            return new Coordinate(xMid + width * 0.5, yMid - hight * 0.5);
        }

        // The following should only be called from shapes that have PinX and PinY in the center of the shape.

        public static Coordinate GetShapePinLocation(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeLocation"))
            {
                return Coordinate.NullCoordinate;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetShapePinLocation"))
                {
                    return Coordinate.NullCoordinate;
                }

                double x = visioShape.Cells["PinX"].ResultIU;
                double y = visioShape.Cells["PinY"].ResultIU;

                return new Coordinate(x, y);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeLocation throws an exception.", ex, 1, true);

                return Coordinate.NullCoordinate;
            }
        }

        public static void SetShapePinLocation(GraphicShape shape, double x, double y)
        {
            Visio.Shape visioShape = shape.VisioShape;

            visioShape.Cells["PinX"].ResultIU = x;
            visioShape.Cells["PinY"].ResultIU = y;
        }

        // Note. Apparently some times the PinX and PinY do not mean the center of the object, so we calculate it explicity here

        public static Coordinate GetLineCenter(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeLocation"))
            {
                return Coordinate.NullCoordinate;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                try
                {
                    double x1 = visioShape.Cells["StartX"].ResultIU;
                    double y1 = visioShape.Cells["StartY"].ResultIU;

                    double x2 = visioShape.Cells["EndX"].ResultIU;
                    double y2 = visioShape.Cells["EndY"].ResultIU;

                    double x = 0.5 * (x1 + x2);
                    double y = 0.5 * (y1 + y2);

                    return new Coordinate(x, y);
                }

                catch (Exception ex1)
                {
                    return Coordinate.NullCoordinate;
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeCenter throws an exception.", ex, 1, true);

                return Coordinate.NullCoordinate;
            }
        }

        public static Coordinate GetShapeCenter(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeLocation"))
            {
                return Coordinate.NullCoordinate;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (visioShape is null)
                {
                    return Coordinate.NullCoordinate;
                }

                try
                {
                    double x = visioShape.Cells["PinX"].ResultIU;
                    double y = visioShape.Cells["PinY"].ResultIU;

                    return new Coordinate(x, y);
                }

                catch (Exception ex1)
                {
                    return Coordinate.NullCoordinate;
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeCenter throws an exception.", ex, 1, true);

                return Coordinate.NullCoordinate;
            }
        }

        public static Coordinate GetLineEndpoint(GraphicShape shape)
        {
            if (shape is null)
            {
                return Coordinate.NullCoordinate;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape is null)
            {
                return Coordinate.NullCoordinate;
            }

            return GetLineEndpoint(visioShape);
        }

        public static Coordinate GetLineEndpoint(Visio.Shape visioShape)
        {
            if (visioShape is null)
            {
                return Coordinate.NullCoordinate;
            }

            double x = visioShape.Cells["EndX"].ResultIU;
            double y = visioShape.Cells["EndY"].ResultIU;

            return new Coordinate(x, y);
        }

        public static void Refresh()
        {
            vsoPage.Application.Redo();
        }

        public static double GetLineLength(GraphicShape shape)
        {
            if (shape == null)
            {
                return 0.0;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return 0.0;
            }

            return visioShape.Cells["Width"].ResultIU;
        }

        internal static void SetShapeText(GraphicShape shape, string shapeText, double fontSize, Color textColor)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            Visio.Characters characters = visioShape.Characters;

            characters.Text = shapeText;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionCharacter,
                (short)0,
                (short)Visio.VisCellIndices.visCharacterSize]
                    .FormulaU = fontSize.ToString() + " pt";

            visioShape.CellsSRC[
               (short)Visio.VisSectionIndices.visSectionCharacter,
               (short)0,
               (short)Visio.VisCellIndices.visCharacterColor]
                   .FormulaU = string.Format("THEMEGUARD(RGB({0},{1},{2}))", textColor.R, textColor.G, textColor.B);
        }

        public static void HorizontalScroll(Visio.Window visioWindow, int scrollDirection)
        {
            if (scrollDirection < 0)
            {
                visioWindow.Scroll((int)Visio.VisWindowScrollX.visScrollLeft, (int)Visio.VisWindowScrollY.visScrollNoneY);
            }

            else if (scrollDirection > 0)
            {
                visioWindow.Scroll((int)Visio.VisWindowScrollX.visScrollRight, (int)Visio.VisWindowScrollY.visScrollNoneY);
            }
        }

        public static void VerticalScroll(Visio.Window visioWindow, int scrollDirection)
        {
            if (scrollDirection < 0)
            {
                visioWindow.Scroll((int)Visio.VisWindowScrollX.visScrollNoneX, (int)Visio.VisWindowScrollY.visScrollUp);
            }

            else if (scrollDirection > 0)
            {
                visioWindow.Scroll((int)Visio.VisWindowScrollX.visScrollNoneX, (int)Visio.VisWindowScrollY.visScrollDown);
            }
        }
        public static string color_size = "0,0,0|0.0125";
        public static void FormatGuideLine(GraphicShape shape)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            visioShape.CellsSRC[
                            (short)Visio.VisSectionIndices.visSectionObject,
                            (short)Visio.VisRowIndices.visRowLine,
                            (short)Visio.VisCellIndices.visLineColor]
                                .FormulaU = "THEMEGUARD(MSOTINT(RGB(" + color_size.Split('|')[0] + "),50))";

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLinePattern]
                    .FormulaU = "9";
            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineWeight]
                .FormulaU = color_size.Split('|')[1];
        }

        public static void FormatFieldMarkerLine(GraphicShape shape, Color lineColor, double lineOpacity, double lineWidthInPts)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            SetBaseLineColor(shape, lineColor);

            SetLineWidth(shape, lineWidthInPts);

            SetBaseLineOpacity(shape, lineOpacity);
        }

        #region Field guide formatting

        public static void FormatFieldGuideLine(GraphicShape shape, Color lineColor, double lineOpacity, double lineWidthInPts, int lineStyle)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            SetBaseLineColor(shape, lineColor);

            SetLineWidth(shape, lineWidthInPts);

            SetBaseLineOpacity(shape, lineOpacity);

            SetBaseLineStyle(shape, lineStyle.ToString());
        }

        public static void FormatFieldGuideLineColor(GraphicShape shape, Color lineColor)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            SetBaseLineColor(shape, lineColor);
        }

        public static void FormatFieldGuideLineOpacity(GraphicShape shape, double opacity)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            SetBaseLineOpacity(shape, opacity);
        }

        public static void FormatFieldGuideLineWidth(GraphicShape shape, double lineWidthInPts)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            SetLineWidth(shape, lineWidthInPts);
        }

        public static void FormatFieldGuideLineStyle(GraphicShape shape, int lineStyle)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            SetBaseLineStyle(shape, lineStyle.ToString());
        }

        #endregion

        public static Point MapVisioToScreenCoords(
            AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl control,
            Visio.Window refWin,
            double x1, double y1,
            double x2, double y2,
            Point currCursorPosn)
        {
            double deltaX = Math.Round(x2 - x1, 3);
            double deltaY = Math.Round(y2 - y1, 3);

            if (deltaX == 0 && deltaY == 0)
            {
                return currCursorPosn;
            }

            Size formSizeInPixels = control.ClientSize;

            double cLeft = 0.0;
            double cUppr = 0.0;
            double cWdth = 0.0;
            double cHght = 0.0;

            refWin.GetViewRect(out cLeft, out cUppr, out cWdth, out cHght);

            double pxlPerInchX = formSizeInPixels.Width / cWdth;
            double pxlPerInchY = formSizeInPixels.Height / cHght;

            int posnX;
            int posnY;

            posnX = (int)Math.Round(currCursorPosn.X + deltaX * pxlPerInchX);
            posnY = (int)Math.Round(currCursorPosn.Y - deltaY * pxlPerInchY);

            return new Point(posnX, posnY);
        }

        public static Point MapVisioToScreenCoords(
            AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl control,
            Visio.Window refWin,
            double x,
            double y,
            bool usingRulers)
        {
            Size formSizeInPixels = control.ClientSize;

            int offsetX = 2;
            int offsetY = 2;

            if (usingRulers)
            {
                offsetX = 17;
                offsetY = 20;
            }

            Point clientLocationInPixels = control.PointToScreen(new Point(offsetX, offsetY));

            double cLeft = 0.0;
            double cUppr = 0.0;
            double cWdth = 0.0;
            double cHght = 0.0;

            refWin.GetViewRect(out cLeft, out cUppr, out cWdth, out cHght);

            double pxlPerInchX = formSizeInPixels.Width / cWdth;
            double pxlPerInchY = formSizeInPixels.Height / cHght;

            double screenX = clientLocationInPixels.X + (x - cLeft) * pxlPerInchX;
            double screenY = clientLocationInPixels.Y + (cUppr - y) * pxlPerInchY;

            return new Point((int)Math.Round(screenX), (int)Math.Round(screenY));

        }

        public static void GetViewRect(GraphicsWindow window, out double visioLeft, out double visioTop, out double visioWidth, out double visioHeight)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window });
#endif

            visioLeft = 0;
            visioTop = 0;
            visioWidth = 0;
            visioHeight = 0;

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:GetViewRect"))
            {
                return;
            }

            try
            {
                Visio.Window refWin = window.VisioWindow;

                // Get the Window coordinates in Visio units.
                refWin.GetViewRect(
                    out visioLeft, out visioTop, out visioWidth, out visioHeight);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetViewRect throws an exception.", ex, 1, true);
            }
        }

        public static void MapVisioToWindows(
            Visio.Window refWin,
            double visioX,
            double visioY,
            int rulerOffset,
            out int windowsX,
            out int windowsY)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { refWin, visioX, visioY, rulerOffset });
#endif

            windowsX = 0;
            windowsY = 0;

            try
            {
                // The drawing control object must be valid.
                if (refWin == null)
                {
                    // Throw a meaningful error.
                    throw new ArgumentNullException("Window");
                }

                double visioLeft;
                double visioTop;
                double visioWidth;
                double visioHeight;
                int pixelLeft;
                int pixelTop;
                int pixelWidth;
                int pixelHeight;

                // Get the Window coordinates in Visio units.
                refWin.GetViewRect(
                    out visioLeft, out visioTop, out visioWidth, out visioHeight);

                // Get the Window coordinates in pixels.
                refWin.GetWindowRect(out pixelLeft, out pixelTop, out pixelWidth, out pixelHeight);

                // GetWindowRect does not take the scrollbar sizes into account
                pixelWidth -= System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + rulerOffset;
                pixelHeight -= System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight + 37 + rulerOffset;

                // Convert the X coordinate by using pixels per inch from the
                // width values.
                windowsX = (int)(pixelLeft + ((pixelWidth / visioWidth) * (visioX - visioLeft)));

                // Convert the Y coordinate by using pixels per inch from the
                // height values and transform from a top-left origin (windows
                // coordinates) to a bottom-left origin (Visio coordinates).
                windowsY = (int)(pixelTop + ((pixelHeight / visioHeight) * (visioTop - visioY)));

                //return new System.Drawing.Point(windowsX, windowsY);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:MapVisioToWindows throws an exception.", ex, 1, true);
            }
        }

        public static void SetShapeFill(GraphicShape shape, string fillFormula)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, fillFormula });
#endif
            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeFill"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeFill"))
            {
                return;
            }

            try
            {
                visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowFill,
                (short)Visio.VisCellIndices.visFillForegnd]
                    .FormulaU = "THEMEGUARD(THEMEVAL(\"AccentColor\"))";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)Visio.VisCellIndices.visFillPattern]
                        .FormulaU = fillFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeFill throws an exception.", ex, 1, true);
            }
        }

        public static void ResetLocPinXY(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:ResetLocPinXY"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:ResetLocPinXY"))
            {
                return;
            }

            try
            {
                visioShape.CellsU["LocPinX"].ResultIU = 0;
                visioShape.CellsU["LocPinY"].ResultIU = visioShape.CellsU["Height"].ResultIU * 0.5;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:ResetLocPinXY throws an exception.", ex, 1, true);
            }
        }

        public static double GetShapeHeight(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeHeight"))
            {
                return double.NaN;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetShapeHeight"))
                {
                    return double.NaN;
                }

                return visioShape.CellsU["Height"].ResultIU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeHeight throws an exception.", ex, 1, true);

                return double.NaN;
            }
        }


        public static void SetShapeHeight(GraphicShape shape, double height)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, height });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeHeight"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeHeight"))
                {
                    return;
                }

                visioShape.CellsU["Height"].ResultIU = height;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeHeight throws an exception.", ex, 1, true);
            }
        }

        public static double GetShapeWidth(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetShapeHeight"))
            {
                return double.NaN;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetShapeWidth"))
                {
                    return double.NaN;
                }

                return visioShape.CellsU["Width"].ResultIU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetShapeWidth throws an exception.", ex, 1, true);

                return double.NaN;
            }
        }
        public static void SetShapeWidth(GraphicShape shape, double width)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, width });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SetShapeWidth"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;


                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:SetShapeWidth"))
                {
                    return;
                }

                visioShape.CellsU["Width"].ResultIU = width;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetShapeWidth throws an exception.", ex, 1, true);
            }
        }

        public static GraphicShape Union(object parentShape, GraphicsWindow window, GraphicsPage page, GraphicShape baseShape, List<GraphicShape> subtrahendShapes)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, page, baseShape, subtrahendShapes });

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:Union"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:Union"))
            {
                return null;
            }

            if (!VisioValidations.ValidateShapeParm(baseShape, "VisioInterop:Union"))
            {
                return null;
            }

            if (!VisioValidations.VisioShapeNotDeleted(baseShape.VisioShape, "VisioInterop:Union"))
            {
                return null;
            }

            foreach (GraphicShape subtrahendShape in subtrahendShapes)
            {
                if (!VisioValidations.ValidateShapeParm(baseShape, "VisioInterop:Union"))
                {
                    return null;
                }

                if (!VisioValidations.VisioShapeNotDeleted(baseShape.VisioShape, "VisioInterop:Union"))
                {
                    return null;
                }
            }

            try
            {
                Visio.Selection selection = page.VisioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSub, baseShape);

                selection.Select(baseShape.VisioShape, 2);

                subtrahendShapes.ForEach(s => selection.Select(s.VisioShape, 2));

                selection.Union();

                Visio.Shape result = window.VisioWindow.Selection[1];

                result.Data3 = baseShape.Guid;

                GraphicShape shape = new GraphicShape(parentShape, window, page, result, ShapeType.LayoutArea, baseShape.Guid);

                return shape;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:Union throws an exception.", ex, 1, true);

                return null;
            }
        }

        public static void SetDrawingScale(GraphicsPage page, string scaleFormula)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { page, scaleFormula });

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetDrawingScale"))
            {
                return;
            }

            Visio.Page visioPage = page.VisioPage;

            try
            {
                visioPage.PageSheet.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowPage,
                    (short)Visio.VisCellIndices.visPageScale]
                        .FormulaU = scaleFormula;


                visioPage.PageSheet.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowPage,
                    (short)Visio.VisCellIndices.visPageDrawingScale]
                        .FormulaU = scaleFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetDrawingScale throws an exception.", ex, 1, true);
            }
        }

        public static double Height(GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { Window, shape });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:Height"))
            {
                return double.NaN;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:Height"))
            {
                return double.NaN;
            }

            try
            {
                return visioShape.CellsU["Height"].ResultIU;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:Height throws an exception", ex, 1, true);

                return double.NaN;
            }
        }

        public static void SelectShape(GraphicsWindow window, GraphicShape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:SelectShape"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:DeselectShape"))
            {
                return;
            }

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:SelectShape"))
            {
                return;
            }

            Visio.Window visioWindow = window.VisioWindow;

            try
            {

                visioWindow.Select(visioShape, 258); // MDD Reset
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:SelectShape", ex, 1, true);
            }
        }


        public static void DeselectShape(GraphicsWindow window, GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:DeselectShape"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:DeselectShape"))
            {
                return;
            }

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:DeselectShape"))
            {
                return;
            }

            Visio.Window visioWindow = window.VisioWindow;

            try
            {
                visioWindow.Select(visioShape, 1);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:DeselectShape", ex, 1, true);
            }
        }

        public static void MoveShape(GraphicsWindow window, GraphicShape shape, double dx, double dy)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape, dx, dy });
#endif

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:MoveShape"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:MoveShape"))
            {
                return;
            }

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:MoveShape"))
            {
                return;
            }

            #endregion

            Visio.Window visioWindow = window.VisioWindow;

            try
            {
                visioWindow.DeselectAll();

                visioWindow.Select(visioShape, 2);

                visioWindow.Selection.Move(dx, dy);

                visioWindow.DeselectAll();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:MoveShape", ex, 1, true);
            }
        }


        public static void MoveSelectedShape(GraphicsWindow window, GraphicShape shape, double dx, double dy)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, shape, dx, dy });
#endif

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:MoveShape"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:MoveShape"))
            {
                return;
            }

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:MoveShape"))
            {
                return;
            }

            #endregion

            Visio.Window visioWindow = window.VisioWindow;


            Coordinate coord = VisioInterop.GetShapePinLocation(shape);

            try
            {

                visioShape.SetCenter(coord.X + dx, coord.Y + dy);
                //visioWindow.Selection.Move(dx, dy);

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:MoveShape", ex, 1, true);
            }
        }

        public static void LockShapeWidth(GraphicShape shape)
        {
#if ROTATION_DEBUG
            return;
#endif
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:LockShapeWidth"))
            {
                return;
            }

            try
            {

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:LockShapeWidth"))
                {
                    return;
                }

                visioShape.CellsU["LockWidth"].ResultIU = 1;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:LockShapeWidth", ex, 1, true);
            }
        }


        public static void LockShapeSelected(GraphicShape shape, int selectKey)
        {
#if ROTATION_DEBUG
            return;
#endif
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, selectKey });

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:LockShapeSelected"))
            {
                return;
            }

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                visioShape.CellsU["LockSelect"].ResultIU = selectKey;

                visioShape.CellsU["LockMoveX"].ResultIU = selectKey;
                visioShape.CellsU["LockMoveY"].ResultIU = selectKey;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:LockShapeSelected", ex, 1, true);
            }
        }

        public static void SetTextVerticalAlignment(GraphicShape shape, string alignment)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, alignment });
#endif

            if (!VisioValidations.ValidateIShapeParm(shape, "VisioInterop:SetTextVerticalAlignment"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            try
            {
                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkVerticalAlign]
                        .FormulaU = alignment;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTextVerticalAlignment throws an exception.", ex, 1, true);
            }
        }

        public static void SetTextHorizontalAlignment(GraphicShape shape, string alignment)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape, alignment });
#endif

            if (!VisioValidations.ValidateIShapeParm(shape, "VisioInterop:SetTextHorizontalAlignment"))
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            try
            {
                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionParagraph,
                    (short)0,
                    (short)Visio.VisCellIndices.visHorzAlign]
                        .FormulaU = alignment;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:SetTextHorizontalAlignment throws an exception.", ex, 1, true);
            }
        }

        public static void SetGridSpacing(GraphicsPage page, double xSpacing, double ySpacing)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { xSpacing, ySpacing });

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetGridSpacing"))
            {
                return;
            }

            try
            {
                Visio.Page visioPage = page.VisioPage;

                string xSpacingFormula = xSpacing.ToString() + " in";

                visioPage.PageSheet.CellsSRC[
                     (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowRulerGrid,
                    (short)Visio.VisCellIndices.visXGridSpacing].FormulaU = xSpacingFormula;


                string ySpacingFormula = ySpacing.ToString() + " in";

                visioPage.PageSheet.CellsSRC[
                     (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowRulerGrid,
                    (short)Visio.VisCellIndices.visYGridSpacing].FormulaU = ySpacingFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:SetGridSpacing", ex, 1, true);
            }
        }

        public static void SetGridOrigin(GraphicsPage page, double xOrigin, double yOrigin)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { xOrigin, yOrigin });

            if (!VisioValidations.ValidatePageParm(page, "VisioInterop:SetGridOrigin"))
            {
                return;
            }

            try
            {
                Visio.Page visioPage = page.VisioPage;

                string xOriginFormula = xOrigin.ToString() + " in";

                visioPage.PageSheet.CellsSRC[
                     (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowRulerGrid,
                    (short)Visio.VisCellIndices.visXGridOrigin].FormulaU = xOriginFormula;


                string yOriginFormula = yOrigin.ToString() + " in";

                visioPage.PageSheet.CellsSRC[
                     (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowRulerGrid,
                    (short)Visio.VisCellIndices.visYGridOrigin].FormulaU = yOriginFormula;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception in call to VisioInterop:SetGridOrigin", ex, 1, true);
            }
        }

        public static List<DirectedPolygon> GetPathList(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:GetPathList"))
            {
                return null;
            }

            try
            {
                List<DirectedPolygon> initialPolygonList = new List<DirectedPolygon>();

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:GetPathList"))
                {
                    return initialPolygonList;
                }

                Visio.Paths visioPaths = visioShape.Paths;

                foreach (Visio.Path visioPath in visioPaths)
                {
                    DirectedPolygon directedPolygon = PathToDirectedPolygon(visioPath);

                    initialPolygonList.Add(directedPolygon);
                }

                return initialPolygonList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:GetPathList throws an exception", ex, 1, true);

                return null;
            }
        }

        public static DirectedPolygon PathToDirectedPolygon(Visio.Path visioPath)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { visioPath });
#endif

            try
            {
                System.Array xyArray;

                visioPath.Points(0.001, out xyArray);

                double[] points = new double[xyArray.Length - 2];

                Array.Copy(xyArray, points, xyArray.Length - 2);

                DirectedPolygon directedPolygon = new DirectedPolygon(points);

                return directedPolygon;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:PathToDirectedPolygon throws an exception", ex, 1, true);

                return null;
            }
        }

        private static short[] lockTypeArray = new short[]
        {
            (short)Visio.VisCellIndices.visLockWidth
            ,(short)Visio.VisCellIndices.visLockHeight
            ,(short)Visio.VisCellIndices.visLockAspect
            ,(short)Visio.VisCellIndices.visLockMoveX
            ,(short)Visio.VisCellIndices.visLockMoveY
            ,(short)Visio.VisCellIndices.visLockRotate
            , (short)Visio.VisCellIndices.visLockBegin
            ,(short)Visio.VisCellIndices.visLockEnd
            ,(short)Visio.VisCellIndices.visLockSelect
            ,(short)Visio.VisCellIndices.visLockReplace
            //,(short)Visio.VisCellIndices.visLockFormat // Exclude so cut indices can be underlined.
            ,(short)Visio.VisCellIndices.visLockCustProp
            ,(short)Visio.VisCellIndices.visLockVtxEdit
            ,(short)Visio.VisCellIndices.visLockThemeIndex
            ,(short)Visio.VisCellIndices.visLockVariation
        };
        private static short[] lockTypeArray1 = new short[]
        {

            (short)Visio.VisCellIndices.visLockSelect

        };
        public static void LockShape(GraphicShape shape)
        {
#if ROTATION_DEBUG
            return;
#endif
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            
            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:LockShape"))
            {
                return;
            }

            #endregion

            try
            {

                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:LockShape"))
                {
                    return;
                }

                foreach (short visCellIndex in lockTypeArray1)
                {
                    visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLock,
                    visCellIndex]
                        .FormulaU = "1";
                }


                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoObjHandles]
                        .FormulaU = "TRUE";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoCtlHandles]
                        .FormulaU = "TRUE";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoAlignBox]
                        .FormulaU = "TRUE";

                //shape.IsLocked = true;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:LockShape throws an exception.", ex, 1, true);
            }
        }

        public static void UnlockShape(GraphicShape shape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:UnlockShape"))
            {
                return;
            }

            #endregion

            UnlockShape(shape.VisioShape);

            //shape.IsLocked = false;
        }

        public static void UnlockShape(Visio.Shape visioShape)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { visioShape });
#endif

            #region Validations

            if (visioShape is null)
            {
                Tracer.TraceGen.TraceError("VisioInterop:UnlockShape called with null visio shape", 1, true);

                return;
            }

            #endregion

            try
            {
                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:UnlockShape"))
                {
                    return;
                }

                foreach (short visCellIndex in lockTypeArray)
                {
                    visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLock,
                    visCellIndex]
                        .FormulaU = "0";
                }


                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoObjHandles]
                        .FormulaU = "FALSE";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoCtlHandles]
                        .FormulaU = "FALSE";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoAlignBox]
                        .FormulaU = "FALSE";

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:UnlockShape throws an exception.", ex, 1, true);
            }
        }

        public static bool IsLocked(GraphicShape shape)
        {
#if TRACE
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });
#endif
            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "VisioInterop:IsLocked"))
            {
                return false;
            }

            #endregion

            try
            {
                if (shape is null)
                {
                    return false;
                }

                Visio.Shape visioShape = shape.VisioShape;

                return IsLocked(visioShape);
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:IsLocked throws an exception.", ex, 1, true);

                return false;
            }
        }

        public static bool IsLocked(Layer layer)
        {
#if TRACE
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer });
#endif

            try
            {
                if (layer is null)
                {
                    return false;
                }


                return (layer.CellsC[(short)Visio.VisCellIndices.visLayerLock].FormulaU == "1");


            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:IsLocked throws an exception.", ex, 1, true);

                return false;
            }
        }

        public static bool IsLocked(Visio.Shape visioShape)
        {
#if TRACE
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { visioShape });
#endif

            try
            {
                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:IsLocked"))
                {
                    return false;
                }

                foreach (short visCellIndex in lockTypeArray)
                {
                    if (
                        visioShape.CellsSRC[
                            (short)Visio.VisSectionIndices.visSectionObject,
                            (short)Visio.VisRowIndices.visRowLock,
                            visCellIndex]
                        .FormulaU != "0")
                    {
                        return true;
                    }
                }

                if (
                    visioShape.CellsSRC[
                        (short)Visio.VisSectionIndices.visSectionObject,
                        (short)Visio.VisRowIndices.visRowMisc,
                        (short)Visio.VisCellIndices.visNoObjHandles]
                    .FormulaU != "FALSE")
                {
                    return true;
                }

                if (
                    visioShape.CellsSRC[
                        (short)Visio.VisSectionIndices.visSectionObject,
                        (short)Visio.VisRowIndices.visRowMisc,
                        (short)Visio.VisCellIndices.visNoCtlHandles]
                    .FormulaU != "FALSE")
                {
                    return true;
                }


                if (
                    visioShape.CellsSRC[
                        (short)Visio.VisSectionIndices.visSectionObject,
                        (short)Visio.VisRowIndices.visRowMisc,
                        (short)Visio.VisCellIndices.visNoAlignBox]
                    .FormulaU != "FALSE")
                {
                    return true;
                }

                return false;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:IsLocked throws an exception.", ex, 1, true);

                return false;
            }
        }

        private static short[] lockTypeArrayExceptForHorizontalMovement = new short[]
          {
                (short)Visio.VisCellIndices.visLockWidth
                ,(short)Visio.VisCellIndices.visLockHeight
                ,(short)Visio.VisCellIndices.visLockAspect
                ,(short)Visio.VisCellIndices.visLockMoveY
                ,(short)Visio.VisCellIndices.visLockRotate
                ,(short)Visio.VisCellIndices.visLockReplace
                ,(short)Visio.VisCellIndices.visLockFormat
                ,(short)Visio.VisCellIndices.visLockCustProp
                ,(short)Visio.VisCellIndices.visLockVtxEdit
                ,(short)Visio.VisCellIndices.visLockThemeIndex
                ,(short)Visio.VisCellIndices.visLockVariation
          };

        public static void LockShapeExceptForHorizontalMovement(GraphicShape shape)
        {
#if ROTATION_DEBUG
            return;
#endif
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            #region Validations

            if (!VisioValidations.ValidateShapeParm(shape, "LockShapeExceptForHorizontalMovement:LockShape"))
            {
                return;
            }

            #endregion

            try
            {
                Visio.Shape visioShape = shape.VisioShape;

                if (!VisioValidations.VisioShapeNotDeleted(visioShape, "VisioInterop:LockShape"))
                {
                    return;
                }

                foreach (short visCellIndex in lockTypeArrayExceptForHorizontalMovement)
                {
                    visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowLock,
                    visCellIndex]
                        .FormulaU = "1";
                }

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoObjHandles]
                        .FormulaU = "1";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoCtlHandles]
                        .FormulaU = "1";

                visioShape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowMisc,
                    (short)Visio.VisCellIndices.visNoAlignBox]
                        .FormulaU = "1";

                ////shape.IsLocked = true;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioInterop:LockShape throws an exception.", ex, 1, true);
            }
        }

        public static void HideDrawingExplorer(GraphicsWindow window)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window });
#endif

            if (!VisioValidations.ValidateWindowParm(window, "VisioInterop:HideDrawingExplorer"))
            {
                return;
            }

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                visioWindow.Windows.ItemFromID[(int)Visio.VisWinTypes.visWinIDDrawingExplorer].Close();
            }

            catch (Exception ex)
            {
                //----------------------------------------------------------------------------------------//
                // The trace of the exception is removed. It is not understood at this point, but         //
                // apparently does not impact application.                                                //
                //----------------------------------------------------------------------------------------//

                // Tracer.TraceGen.TraceException("VisioInterop:HideDrawingExplorer throws an exception.", ex, 1, true); // Remove trace for time being.
            }
        }

        public static void SetShapeCenter(GraphicShape shape, Coordinate center)
        {
            if (shape == null)
            {
                return;
            }

            if (shape.VisioShape == null)
            {
                return;
            }

            if (center == Coordinate.NullCoordinate)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            double x = center.X;
            double y = center.Y;

            visioShape.CellsU["PinX"].ResultIU = x;
            visioShape.CellsU["PinY"].ResultIU = y;

            //SetShapeCenter(shape, center.X, center.Y);
        }

        public static void SetShapeCenter(GraphicShape shape, double x, double y)
        {
            if (shape == null)
            {
                return;
            }

            if (shape.VisioShape == null)
            {
                return;
            }

            shape.VisioShape.SetCenter(x, y);
        }

        public static void AddGuide(double x, double y)
        {
            Page.VisioPage.AddGuide(2, x, y);
        }

        public static void SelectAllShapes()
        {
            Visio.Window visioWindow = Window.VisioWindow;

            visioWindow.SelectAll();
        }

        public static Tuple<double, double, double, double> GetLineCoordinates(Visio.Shape shape)
        {
            //if (shape.OneD != 1)
            //{
            //    return null;
            //}

            var startX = shape.Cells["BeginX"].Result[Visio.VisUnitCodes.visInches];
            var startY = shape.Cells["BeginY"].Result[Visio.VisUnitCodes.visInches];
            var endX = shape.Cells["EndX"].Result[Visio.VisUnitCodes.visInches];
            var endY = shape.Cells["EndY"].Result[Visio.VisUnitCodes.visInches];

            return new Tuple<double, double, double, double>(startX, startY, endX, endY);
        }

        public static void SyncLineToVisio(GraphicsDirectedLine line)
        {
            GraphicShape graphicShape = (GraphicShape)line.Shape;

            if (graphicShape is null)
            {
                return;
            }

            Visio.Shape visioShape = graphicShape.VisioShape;

            if (visioShape is null)
            {
                return;
            }

            line.Coord1 = GetLineStartpoint(visioShape);
            line.Coord2 = GetLineEndpoint(visioShape);
            
        }

        public static void SyncCircleToVisio(GraphicsCircle circle)
        {
            GraphicShape graphicShape = (GraphicShape)circle.Shape;

            if (graphicShape is null)
            {
                return;
            }

            Visio.Shape visioShape = graphicShape.VisioShape;

            if (visioShape is null)
            {
                return;
            }

            double centerX = visioShape.Cells["PinX"].ResultIU;
            double centerY = visioShape.Cells["PinY"].ResultIU;

            circle.Center = new Coordinate(centerX, centerY);


        }

        public static void SyncPolylineToVisio(GraphicsDirectedPolyline polyline)
        {
            foreach (GraphicsDirectedLine line in polyline)
            {
                SyncLineToVisio(line);
            }
        }

        public static void SyncCircleTagToVisio(GraphicCircleTag circleTag)
        {
            GraphicCircle circle = (GraphicCircle)circleTag;
            GraphicShape shape = circle.Shape;

            if (shape is null)
            {
                return;
            }

            circle.Center = VisioInterop.GetShapeCenter(shape);

            Visio.Shape visioShape = circle.Shape.VisioShape;

            visioShape.Cells["Angle"].ResultIU = 0;
        }

        public static void Exit()
        {
            Marshal.ReleaseComObject(Page.VisioPage);

            Marshal.ReleaseComObject(Window.VisioWindow);

        }

        public static void DisableScreenUpdating()
        {
            vsoApplication.ScreenUpdating = 0;

            vsoApplication.DeferRecalc = 1;     // You can still use booleans here depending on context

            vsoApplication.ShowChanges = false;
        }

        public static void CenterText(GraphicShape shape)
        {
            if (shape == null)
            {
                return;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
                return;
            }

            try
            {
                // 1) Center text horizontally + vertically
                visioShape.CellsU["Para.HorzAlign"].FormulaU = "1"; // 0=Left, 1=Center, 2=Right
                visioShape.CellsU["VerticalAlign"].FormulaU = "1"; // 0=Top, 1=Middle, 2=Bottom

                // 2) Make the text block coincide with the shape bounds (prevents odd offsets)
                visioShape.CellsU["TxtWidth"].FormulaU = "Width";
                visioShape.CellsU["TxtHeight"].FormulaU = "Height";
                visioShape.CellsU["TxtPinX"].FormulaU = "Width*0.5";
                visioShape.CellsU["TxtPinY"].FormulaU = "Height*0.5";
                visioShape.CellsU["TxtLocPinX"].FormulaU = "TxtWidth*0.5";
                visioShape.CellsU["TxtLocPinY"].FormulaU = "TxtHeight*0.5";

                // (optional) trim margins if you want tighter centering
                visioShape.CellsU["LeftMargin"].FormulaU = "1 pt";
                visioShape.CellsU["RightMargin"].FormulaU = "1 pt";
                visioShape.CellsU["TopMargin"].FormulaU = "1 pt";
                visioShape.CellsU["BottomMargin"].FormulaU = "1 pt";
            }

            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        public static void EnableScreenUpdating()
        {
            vsoApplication.ShowChanges = true;
            vsoApplication.DeferRecalc = 0;
            vsoApplication.ScreenUpdating = -1;
        }

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;

        public static (int dpiX, int dpiY) dotsPerInch()
        {
            IntPtr hwnd = (IntPtr)vsoWindow.WindowHandle;
            IntPtr hdc = GetDC(hwnd);

            int dpiX = GetDeviceCaps(hdc, LOGPIXELSX);
            int dpiY = GetDeviceCaps(hdc, LOGPIXELSY);

            ReleaseDC(hwnd, hdc);
            return (dpiX, dpiY);
        }

        public static Visio.Shape CreateFixedWidthTextBox(GraphicsPage page, double leftIn, double topIn, double boxWIn, string txt, double fontSize)
        {
            Visio.Page visioPage = page.VisioPage;

            // Draw a small stub; we’ll let Height auto-size after text
            var s = visioPage.DrawRectangle(leftIn, topIn, leftIn + boxWIn, topIn - 0.25);

            // Text
            s.Text = txt;

            s.CellsU["Char.Size"].FormulaU = $"{fontSize} pt";

            // 1) Fix the shape width
            s.CellsU["Width"].FormulaU = $"{boxWIn} in";

            // 2) Make the text block coincide with the shape’s bounds
            s.CellsU["TxtWidth"].FormulaU = "Width";
            s.CellsU["TxtHeight"].FormulaU = "Height";
            s.CellsU["TxtPinX"].FormulaU = "Width*0.5";
            s.CellsU["TxtPinY"].FormulaU = "Height*0.5";
            s.CellsU["TxtLocPinX"].FormulaU = "TxtWidth*0.5";
            s.CellsU["TxtLocPinY"].FormulaU = "TxtHeight*0.5";

            // 3) Word-wrap happens when TxtWidth is narrower than the text.
            //    Align text as you like:
            s.CellsU["Para.HorzAlign"].FormulaU = "0"; // 0=Left, 1=Center, 2=Right
            s.CellsU["VerticalAlign"].FormulaU = "0"; // 0=Top, 1=Middle, 2=Bottom

            // 4) Margins (adjust to taste)
            s.CellsU["LeftMargin"].FormulaU = "4 pt";
            s.CellsU["RightMargin"].FormulaU = "4 pt";
            s.CellsU["TopMargin"].FormulaU = "2 pt";
            s.CellsU["BottomMargin"].FormulaU = "2 pt";

            // 5) Auto-size HEIGHT from the wrapped text:
            //    TEXTHEIGHT(TheText, TxtWidth) returns the height needed for current text & width.
            //    Add top+bottom margins and clamp to a minimum height if desired.
            s.CellsU["Height"].FormulaU =
                "MAX(0.2 in, TEXTHEIGHT(TheText, TxtWidth) + TopMargin + BottomMargin)";

            // (Optional) keep the top-left corner fixed while the box grows downward:
            s.CellsU["LocPinX"].FormulaU = "0"; // pin at left edge
            s.CellsU["LocPinY"].FormulaU = "Height"; // pin at top edge
            s.CellsU["PinX"].FormulaU = $"{leftIn} in";
            s.CellsU["PinY"].FormulaU = $"{topIn} in";

            return s;
        }


        public static IEnumerable<GraphicShape> SelectShapes(GraphicsPage page, double x1, double y1, double x2,
            double y2)
        {
            HashSet<GraphicShape> selectedShapes = new HashSet<GraphicShape>();

            foreach (GraphicsLayer graphicsLayer in page.GraphicsLayers)
            {
                if (graphicsLayer.Visibility == false) { continue; }

                if (SystemState.DesignState == DesignState.Area /*|| SystemState.DesignState == DesignState.Line*/)
                {
                    if (!(graphicsLayer.LayerName.StartsWith("[AreaMode:")))
                    {
                        continue;
                    }
                }

                if (SystemState.DesignState == DesignState.Line)
                {
                    if (!(graphicsLayer.LayerName.StartsWith("[LineMode:")))
                    {
                        continue;
                    }
                }

                if (SystemState.DesignState == DesignState.Seam)
                {
                    if (!(graphicsLayer.LayerName.StartsWith("[NormalSeamLayer") ||
                        graphicsLayer.LayerName.StartsWith("[NormalSeamsUnhideableLayer") ||
                        graphicsLayer.LayerName.StartsWith("ManualSeamsAllLayer")))
                    {
                        continue;
                    }
                }

                SelectShapes(graphicsLayer, x1, y1, x2, y2, selectedShapes);
            }

            return selectedShapes;
        }

        public static void SelectShapes(GraphicsLayer layer, double x1, double y1, double x2, double y2, HashSet<GraphicShape> selectedShapes)
        {
            List<GraphicShape> rtrnList = new List<GraphicShape>();

            if (!layer.Visibility)
            {
                return;
            }

            double L = Math.Min(x1, x2), B = Math.Min(y1, y2);
            double R = Math.Max(x1, x2), T = Math.Max(y1, y2);

            short flags = (short)(
                Visio.VisBoundingBoxArgs.visBBoxExtents |
                Visio.VisBoundingBoxArgs.visBBoxUprightText |
                Visio.VisBoundingBoxArgs.visBBoxDrawingCoords);

            foreach (GraphicShape shape in layer.Shapes)
            {
                if (shape.VisioShape is null)
                {
                    continue;
                }

                double l, b, r, t;

                try
                {
                    shape.VisioShape.BoundingBox(flags, out l, out b, out r, out t);

                    bool inside = (l >= L && r <= R && b >= B && t <= T);

                    if (inside)
                    {
                        if (!selectedShapes.Contains(shape))
                        {
                            selectedShapes.Add(shape);
                        }
                    }
                }

                catch { continue; }
            }
        }


        public static Visio.Shape CreateLockIcon(Visio.Page page)
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;               // host process folder
            string iconPath = System.IO.Path.Combine(exeDir, System.IO.Path.Combine("Img", "lockIcon.png"));

            Visio.Shape icon = page.Import(iconPath);

            icon.CellsU["LockSelect"].FormulaU = "1";
            icon.CellsU["NonPrinting"].FormulaU = "1";
            icon.CellsU["NoObjHandles"].FormulaU = "1";

            icon.BringToFront();

            return icon;
        }
        public static void BeginFastUpdate()
        {
            if (vsoApplication == null) return;

            try
            {

                vsoApplication.DeferRecalc = -1;   // delay ShapeSheet formula recalculation
                vsoApplication.ScreenUpdating = 0;  // no visual updates
                vsoApplication.UndoEnabled = false;
            }
            catch { /* ignore older versions missing any property */ }
        }

        public static void EndFastUpdate()
        {
            if (vsoApplication == null) return;

            try
            {
                vsoApplication.DeferRecalc = 0;
                vsoApplication.ScreenUpdating = -1;
                vsoApplication.UndoEnabled = true;
            }
            catch { /* ignore older versions */ }

            // Force a repaint in case Visio didn’t automatically refresh
            //try { app.ActiveWindow?.SelectAll(); app.ActiveWindow?.DeselectAll(); } catch { }
        }

    }
}
