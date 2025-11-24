#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsPageShapes.cs. Project: Graphics. Created: 6/10/2024         */
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


using System.ComponentModel;

namespace Graphics
{
    using Geometry;
    using Microsoft.Office.Interop.Visio;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Serialization;
    using TracerLib;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;

    public partial class GraphicsPage
    {

        //--------------------------------------------------------------------------------------------------------------------------------------------/
        //                                                         Page shape dictionary
        //--------------------------------------------------------------------------------------------------------------------------------------------/

        public Dictionary<string, GraphicShape> PageShapeDict = new Dictionary<string, GraphicShape>();

        public IEnumerable<GraphicShape> PageShapeDictValues => PageShapeDict.Values;

        public void AddToPageShapeDict(IGraphicsShape iShape)
        {
            
            GraphicShape graphicShape = (GraphicShape)iShape.Shape;

            if (graphicShape is null)
            {
                return;
            }

            string guid = graphicShape.Guid;

            if (graphicShape.Guid != iShape.Guid)
            {
               // throw new NotImplementedException();
            }
            if (string.IsNullOrEmpty(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a shape to ShapeDict with a null guid in GraphicsPage:AddToPageShapeDict.", 1, true);
                return;
            }

            if (PageShapeDictContains(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a shape '" + ((GraphicShape)iShape).ToString() + "' to ShapeDict with an existing guid in GraphicsPage:AddToPageShapeDict.", 1, true);
                return;
            }

            string s = graphicShape.ToString();

            //Console.WriteLine("Adding shape to page shape dict: " + s);

            
            PageShapeDict.Add(guid, graphicShape);
        }

        public void RemoveFromPageShapeDict(GraphicShape shape)
        {
            if (shape is null)
            {
                return; // Bug here where changing to tiles.
            }

            if (PageShapeDict is null)
            {
                return;
            }

            if (PageShapeDict.Count == 0)
            {
                return;
            }

            if (!PageShapeDictContains(shape))
            {
                return;
            }

            string s = shape.ToString();

            //Console.WriteLine("ShapeDict Deleting " + shape.ToString());

            RemoveFromPageShapeDict(shape.Guid);
        }

        public void RemoveFromPageShapeDict(string guid)
        {
            if (PageShapeDict is null)
            {
                return;
            }
           
            if (PageShapeDict.Count == 0)
            {
                return;
            }

            if (!PageShapeDictContains(guid))
            {
                return;
            }

            GraphicShape shape = PageShapeDict[guid];

            string s = shape.ToString();

            //Console.WriteLine("ShapeDict Deleting " + shape.ToString());

            PageShapeDict.Remove(guid);
        }

        public bool PageShapeDictContains(IGraphicsShape iShape)
        {
            return PageShapeDictContains(iShape.Guid);
        }

        public bool PageShapeDictContains(string guid)
        {
            return PageShapeDict.ContainsKey(guid);
        }

        public GraphicShape PageShapeDictGetShape(string guid)
        {
            if (!PageShapeDictContains(guid))
            {
                return null;
            }

            return PageShapeDict[guid];
        }



        public GraphicShape GroupShapes(GraphicsWindow window, GraphicsPage page, List<GraphicShape> shapeList)
        {
            GraphicShape shape = VisioInterop.GroupShapes(window, shapeList.ToArray());

            shape.Window = window;
            shape.Page = page;

            string guid = GuidMaintenance.CreateGuid(shape);

            shape.Guid = guid;

            return shape;
        }

        public List<GraphicShape> GetSelectedBorderShapeList(double x, double y)
        {
            List<GraphicShape> returnList = new List<GraphicShape>();

            Visio.Selection selection = VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection is null)
            {
                return returnList;
            }

            if (selection.Count <= 0)
            {
                return returnList;
            }

            foreach (Visio.Shape shape in selection)
            {
                if (shape.Data2 != "BoxLeft" && shape.Data2 != "BoxRght")
                {
                    continue;
                }

                string guid = shape.Data3;

                if (!PageShapeDict.ContainsKey(guid))
                {
                    continue;
                }

                if (PageShapeDict[guid] is GraphicShape)
                {
                    returnList.Add((GraphicShape)PageShapeDict[guid]);
                }
            }

            return returnList;
        }

        public GraphicShape SelectedTextBox { get; set; }

        public GraphicShape SelectedCanvasArrow { get; set; }
    }
}
