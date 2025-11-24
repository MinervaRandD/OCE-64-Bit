#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: VisioGeometryEngine.cs. Project: Graphics. Created: 6/10/2024         */
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
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TracerLib;

using Visio = Microsoft.Office.Interop.Visio;
using Geometry;

namespace Graphics
{
    public static class VisioGeometryEngine
    {

        public static List<GraphicShape> Subdivide(GraphicsWindow window, GraphicsPage page, GraphicShape baseShape, double[] polylinePoints)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, page, baseShape, polylinePoints });

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioGeometryEngine:Subdivide"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioGeometryEngine:Subdivide"))
            {
                return null;
            }

            if (!VisioValidations.ValidateShapeParm(baseShape, "VisioGeometryEngine:Subdivide"))
            {
                return null;
            }

            if (polylinePoints is null)
            {
                Tracer.TraceGen.TraceError("polylinePoints is null in call to VisioGeometryEngine:Subdivide", 1, true);
            }

            if (polylinePoints.Length <= 0)
            {
                Tracer.TraceGen.TraceError("polylinePoints is empty in call to VisioGeometryEngine:Subdivide", 1, true);
            }

            #endregion

            List<GraphicShape> rtrnList = new List<GraphicShape>();

            try
            {
                window.DeselectAll();

                Visio.Page visioPage = page.VisioPage;

                Visio.Shape subdivideLine = visioPage.DrawPolyline(polylinePoints, (short)Visio.VisDrawSplineFlags.visPolyline1D);

                string guid = Utilities.GuidMaintenance.CreateGuid(subdivideLine);

                subdivideLine.Data1 = "SubdivideLine";
                subdivideLine.Data2 = "Polyline";
                subdivideLine.Data3 = guid;

                string baseShapeGuid = baseShape.VisioShape.Data3;

                Dictionary<string, Visio.Shape> shapeDict = new Dictionary<string, Visio.Shape>();

                List<Tuple<string, Visio.Shape>> guidList = new List<Tuple<string, Visio.Shape>>();

                foreach (Visio.Shape shape in visioPage.Shapes)
                {
                    guidList.Add(new Tuple<string, Visio.Shape>(shape.Data3, shape));
                }

                foreach (Visio.Shape visioShape in visioPage.Shapes)
                {
                    if (!string.IsNullOrEmpty(visioShape.Data3))
                    {
                        if (!shapeDict.ContainsKey(visioShape.Data3))
                        {
                            shapeDict.Add(visioShape.Data3, visioShape);
                        }

                        // MDD Reset

                        else
                        {
                            List<Visio.Shape> dupShapeList = guidList.Where(s => s.Item1 == visioShape.Data3).Select(s => s.Item2).ToList();

                            Visio.Shape visioShape1 = dupShapeList[0];
                            Visio.Shape visioShape2 = dupShapeList[1];

                            GraphicShape shape1 = new GraphicShape(null, window, page, null);
                            GraphicShape shape2 = new GraphicShape(null, window, page, null);

                            shape1.VisioShape = visioShape1;
                            shape2.VisioShape = visioShape2;

                            shape1.Guid = visioShape1.Data3;
                            shape2.Guid = visioShape2.Data3;

                            Coordinate shape1Coord1 = VisioInterop.GetShapeBeginPoint(shape1);
                            Coordinate shape1Coord2 = VisioInterop.GetShapeEndPointd(shape1);

                            Coordinate shape2Coord1 = VisioInterop.GetShapeBeginPoint(shape2);
                            Coordinate shape2Coord2 = VisioInterop.GetShapeEndPointd(shape2);
                        }
                    }
                }

                List<Visio.Shape> visioShapeList = Fragment(window, page, baseShape.VisioShape, subdivideLine);

                foreach (Visio.Shape visioShape in visioShapeList)
                {
                    if (visioShape.Data2 == "Image")
                    {
                        continue;
                    }

                    if (visioShape.Data3 != baseShapeGuid && shapeDict.ContainsKey(visioShape.Data3))
                    {
                        continue;
                    }

                    //Shape shape = new Shape(Window, Page, visioShape, ShapeType.LayoutArea);

                    GraphicShape shape = new GraphicShape()
                    {
                        Window = window
                        ,
                        Page = page
                        ,
                        VisioShape = visioShape
                        ,
                        ShapeType = ShapeType.LayoutArea
                    };

                    //shape.Guid = Utilities.GuidMaintenance.CreateGuid(shape);

                    rtrnList.Add(shape);
                }

                return rtrnList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioGeometryEngine:Subdivide", ex, 1, true);

                return rtrnList;
            }
        }

        public static List<Visio.Shape> Fragment(GraphicsWindow window, GraphicsPage page, Visio.Shape baseShape, Visio.Shape subdivideLine)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { baseShape, subdivideLine });

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioGeometryEngine:Subdivide"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioGeometryEngine:Subdivide"))
            {
                return null;
            }

            #endregion

            List<Visio.Shape> rtrnList = new List<Visio.Shape>();

            try
            {
                //Window.DeselectAll();
                Visio.Page visioPage = page.VisioPage;

                Visio.Window visioWindow = window.VisioWindow;

                Visio.Selection selection = visioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSuper, baseShape);

                selection.DeselectAll();

                selection.Select(baseShape, (short)Visio.VisSelectArgs.visSelect);
                selection.Select(subdivideLine, (short)Visio.VisSelectArgs.visSelect);

                Visio.Shape groupedShape = selection.Group();

                window.DeselectAll();

                selection = visioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeSingle, Visio.VisSelectMode.visSelModeOnlySub, groupedShape);

                selection.Fragment();

                Visio.Selection result = visioWindow.Selection;

                if (result.Count <= 0)
                {
                    return rtrnList;
                }

                Visio.Shape containingShape = result[1].ContainingShape;

                foreach (Visio.Shape shape in containingShape.Shapes)
                {
                    rtrnList.Add(shape);
                }

                visioWindow.DeselectAll();

                return rtrnList;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioGeometryEngine:Fragment", ex, 1, true);

                return rtrnList;
            }
        }

        //public static GraphicShape Merge(GraphicsWindow Window, GraphicsPage Page, List<GraphicShape> shapeList)
        //{
        //    Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shapeList });

        //    #region Validations

        //    if (!VisioValidations.ValidateWindowParm(Window, "VisioGeometryEngine:Merge"))
        //    {
        //        return null;
        //    }

        //    if (!VisioValidations.ValidatePageParm(Page, "VisioGeometryEngine:Merge"))
        //    {
        //        return null;
        //    }

        //    if (shapeList is null)
        //    {
        //        Tracer.TraceGen.TraceError("shapeList is null in call to VisioGeometryEngine:Merge", 1, true);

        //        return null;
        //    }

        //    if (shapeList.Count <= 0)
        //    {
        //        Tracer.TraceGen.TraceError("shapeList is empty in call to VisioGeometryEngine:Merge", 1, true);

        //        return null;
        //    }

        //    foreach (GraphicShape shape in shapeList)
        //    {
        //        if (!VisioValidations.ValidateShapeParm(shape, "VisioGeometryEngine:Merge"))
        //        {
        //            return null;
        //        }
        //    }

        //    #endregion

        //    try
        //    {
        //        Window.DeselectAll();

        //        List<Visio.Shape> visioShapeList = shapeList.Select(s => s.VisioShape).ToList();

        //        Visio.Shape visioShape = Union(Window, Page, visioShapeList);

        //        if (visioShape is null)
        //        {
        //            return null;
        //        }

        //        GraphicShape rtrnShape = new GraphicShape(Window, Page, visioShape, ShapeType.LayoutArea);

        //        rtrnShape.VisioShape.Data1 = "Layout area from merge";

        //        return rtrnShape;
        //    }

        //    catch (Exception ex)
        //    {
        //        Tracer.TraceGen.TraceException("Exception thrown in VisioGeometryEngine:Merge", ex, 1, true);

        //        return null;
        //    }
        //}

        //public static GraphicShape Union(GraphicsWindow Window, GraphicsPage Page, List<GraphicShape> shapeList)
        //{
        //    Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shapeList });

        //    #region Validations

        //    if (!VisioValidations.ValidateWindowParm(Window, "VisioGeometryEngine:Intersection"))
        //    {
        //        return null;
        //    }

        //    if (!VisioValidations.ValidatePageParm(Page, "VisioGeometryEngine:Intersection"))
        //    {
        //        return null;
        //    }

        //    if (shapeList is null)
        //    {
        //        Tracer.TraceGen.TraceError("shapeList is null in call to VisioGeometryEngine:Intersection", 1, true);

        //        return null;
        //    }

        //    if (shapeList.Count <= 0)
        //    {
        //        Tracer.TraceGen.TraceError("shapeList is empty in call to VisioGeometryEngine:Intersection", 1, true);

        //        return null;
        //    }

        //    foreach (GraphicShape shape in shapeList)
        //    {
        //        if (!VisioValidations.ValidateShapeParm(shape, "VisioGeometryEngine:Intersection"))
        //        {
        //            return null;
        //        }
        //    }

        //    #endregion

        //    List<Visio.Shape> visioShapeList = new List<Visio.Shape>(shapeList.Select(s => s.VisioShape));

        //    Visio.Shape visioShape = Union(Window, Page, visioShapeList);

        //    GraphicShape rtrnShape = new GraphicShape(this, Window, Page, visioShape, ShapeType.Unknown);

        //    return rtrnShape;
        //}

        public static Visio.Shape Union(GraphicsWindow window, GraphicsPage page, List<Visio.Shape> visioShapeList)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { visioShapeList });

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioGeometryEngine:Intersection"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioGeometryEngine:Intersection"))
            {
                return null;
            }

            if (visioShapeList is null)
            {
                Tracer.TraceGen.TraceError("visioShapeList is null in call to VisioGeometryEngine:Intersection", 1, true);

                return null;
            }

            if (visioShapeList.Count <= 0)
            {
                Tracer.TraceGen.TraceError("visioShapeList is empty in call to VisioGeometryEngine:Intersection", 1, true);

                return null;
            }

            #endregion

            try
            {
                Visio.Window visioWindow = window.VisioWindow;

                Visio.Page visioPage = page.VisioPage;

                window.DeselectAll();

                Visio.Selection selection = visioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSuper, visioShapeList[0]);

                foreach (Visio.Shape visioShape in visioShapeList)
                {
                    selection.Select(visioShape, 2);
                }

                selection.Union();

                Visio.Selection result = visioWindow.Selection;

                if (result.Count <= 0)
                {
                    return null;
                }

                Visio.Shape rtrnShape = null;

                foreach (Visio.Shape visioShape in result.ContainingShape.Shapes)
                {
                    if (string.IsNullOrEmpty(visioShape.Data3))
                    {
                        rtrnShape = visioShape;
                    }
                }
                Visio.Shape containingShape = result[1].ContainingShape;

                window.DeselectAll();

                return rtrnShape;
            }


            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioGeometryEngine:Union", ex, 1, true);

                return null;
            }
        }


        public static Visio.Shape Intersection(GraphicsWindow window, GraphicsPage page, List<Visio.Shape> shapeList)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shapeList });

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioGeometryEngine:Intersection"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioGeometryEngine:Intersection"))
            {
                return null;
            }

            if (shapeList is null)
            {
                Tracer.TraceGen.TraceError("visioShapeList is null in call to VisioGeometryEngine:Intersection", 1, true);

                return null;
            }

            if (shapeList.Count <= 0)
            {
                Tracer.TraceGen.TraceError("visioShapeList is empty in call to VisioGeometryEngine:Intersection", 1, true);

                return null;
            }

            foreach (GraphicShape intersectionShape in shapeList)
            {
                if (!VisioValidations.ValidateShapeParm(intersectionShape, "Intersection:Subtract"))
                {
                    return null;
                }
            }

            #endregion

            try
            {
                Visio.Page visioPage = page.VisioPage;

                Visio.Window visioWindow = window.VisioWindow;

                window.DeselectAll();

                Visio.Selection selection = visioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeSingle, Visio.VisSelectMode.visSelModeSkipSuper, shapeList[0]);

                foreach (GraphicShape shape in shapeList)
                {
                    Visio.Shape visioShape = shape.VisioShape;

                    selection.Select(visioShape, 2);
                }

                selection.Intersect();

                Visio.Selection result = visioWindow.Selection;

                if (result.Count <= 0)
                {
                    return null;
                }

                Visio.Shape containingShape = result[1].ContainingShape;

                return containingShape.Shapes[0];
            }


            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in VisioGeometryEngine:Union", ex, 1, true);

                return null;
            }
        }

        public static GraphicShape Subtract(object parentLayoutArea, GraphicsWindow window, GraphicsPage page, GraphicShape baseShape, List<GraphicShape> subtrahendShapes)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { window, page, baseShape, subtrahendShapes });

            #region Validations

            if (!VisioValidations.ValidateWindowParm(window, "VisioGeometryEngine:Subtract"))
            {
                return null;
            }

            if (!VisioValidations.ValidatePageParm(page, "VisioGeometryEngine:Subtract"))
            {
                return null;
            }

            if (!VisioValidations.ValidateShapeParm(baseShape, "VisioGeometryEngine:Subtract"))
            {
                return null;
            }

            if (subtrahendShapes is null)
            {
                Tracer.TraceGen.TraceError("subtrahendShapes is null in call to VisioGeometryEngine:Subtract", 1, true);

                return null;
            }

            if (subtrahendShapes.Count <= 0)
            {
                Tracer.TraceGen.TraceError("subtrahendShapes is empty in call to VisioGeometryEngine:Subtract", 1, true);

                return null;
            }

            foreach (GraphicShape subtrahendShape in subtrahendShapes)
            {
                if (!VisioValidations.ValidateShapeParm(subtrahendShape, "VisioGeometryEngine:Subtract"))
                {
                    return null;
                }
            }

            #endregion

            try
            {
                // MDD Reset

                page.CheckForNullDataShape();

                window.DeselectAll();

                Visio.Page visioPage = page.VisioPage;

                //GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);

                Visio.Selection selection = visioPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeEmpty, Visio.VisSelectMode.visSelModeSkipSub, baseShape);

                selection.Select(baseShape.VisioShape, 2);


                subtrahendShapes.ForEach(s => selection.Select(s.VisioShape, 2));
                
                page.CheckForNullDataShape();

                //  GraphicsDebugSupportRoutines.CheckForInvalidVisioShapeInPageShapeDict(page);
                HashSet<int> shapeIdsBefore = visioPage.Shapes.Cast<Visio.Shape>()
                    .Select(s => s.ID)
                    .ToHashSet();

                selection.Subtract();

                page.CheckForNullDataShape();

                Visio.Shape resultShape = visioPage.Shapes.Cast<Visio.Shape>()
                    .FirstOrDefault(s => !shapeIdsBefore.Contains(s.ID));

                if (resultShape != null)
                {
                    resultShape.Data1 = "";
                    resultShape.Data2 = "";
                    resultShape.Data3 = baseShape.Guid;
                }

                GraphicShape shape = new GraphicShape(parentLayoutArea, window, page, resultShape, ShapeType.LayoutAreaShape, baseShape.Guid);

                page.CheckForNullDataShape();

                return shape;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("VisioGeometryEngine:Subtract throws an exception.", ex, 1, true);

                return null;
            }
        }

    }
}
