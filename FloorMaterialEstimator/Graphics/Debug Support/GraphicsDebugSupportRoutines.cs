#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsDebugSupportRoutines.cs. Project: Graphics. Created: 6/10/2024         */
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
using System.Windows.Forms;

using Visio = Microsoft.Office.Interop.Visio;
using Microsoft.Office.Interop.Visio;
using Utilities;
namespace Graphics
{
    public static class GraphicsDebugSupportRoutines
    {

        public static void CheckForNullInPageShapeDict(GraphicsPage page, int checkPoint)
        {
            return;

#if false
            foreach (IGraphicsShape iShape in page.PageShapeDict.Values)
            {
                if ((GraphicShape)iShape is null)
                {
                    MessageBox.Show("Null shape in shape dict at point " + checkPoint);
                    return;
                }

                if (((GraphicShape)iShape).VisioShape is null)
                {
                    MessageBox.Show("Null visio shape dict at point " + checkPoint);
                    return;
                }
            }
#endif
        }

        public static void CheckForInvalidVisioShapeInPageShapeDict(GraphicsPage page)
        {
            List<Visio.Shape> visioShapeList = new List<Visio.Shape>();

            foreach (Visio.Shape shape in page.VisioPage.Shapes)
            {
                visioShapeList.Add(shape);
            }

            foreach (Visio.Shape visioShape in visioShapeList)
            {
                try
                {
                    string data1 = visioShape.Data1;
                }

                catch
                {
                    ;
                }
            }


            foreach (IGraphicsShape iShape in page.PageShapeDictValues)
            {
                GraphicShape shape = (GraphicShape)iShape;

                try
                {

                    Visio.Shape visioShape = shape.VisioShape;

                    if (visioShape is null)
                    {
                        continue;
                    }

                    string data3 = visioShape.Data3;
                    
                }

                catch
                {
                    ;
                }
            }
        }

        public static void CheckForNullShapeInGraphicsLayers(GraphicsPage page, int checkPoint)
        {
            return;

#if false
            foreach (GraphicsLayer graphicsLayer in page.GraphicsLayers)
            {
                foreach (Shape shape in graphicsLayer.Shapes)
                {
                    if (shape.VisioShape is null)
                    {
                        MessageBox.Show("Null shape.visioShape in graphics layer " + graphicsLayer.Name + " at point " + checkPoint);
                        return;
                    }
            
                    if (string.IsNullOrEmpty(shape.Data1) || string.IsNullOrEmpty(shape.Data2) || string.IsNullOrEmpty(shape.Data3))
                    {
                        MessageBox.Show("Null shape data in graphics layer " + graphicsLayer.Name + " at point " + checkPoint);
                    }
                }
            }
#endif
        }

        public static void SeamLineCheck(Visio.Page visioPage)
        {
            HashSet<string> lineGuids = new HashSet<string>();

            foreach (Visio.Shape visioShape in visioPage.Shapes)
            {
                if (visioShape.Data1 != "Seam")
                {
                    continue;
                }

                if (visioShape.Data2 != "Line")
                {
                    continue;
                }

                string guid = visioShape.Data3;

                if (lineGuids.Contains(guid))
                {
                    MessageBox.Show("Duplicate line guid found.");
                    return;
                }

                lineGuids.Add(guid);
            }
        }

        public static void CheckForOverageIndex(GraphicsPage page, string guid)
         {
            foreach (Visio.Shape visioShape in page.VisioPage.Shapes)
            {
                 
                if (visioShape.Data1.Contains("OverIndex"))
                {
                    ;
                }

                if (visioShape.Data3.Contains(guid))
                {
                    ;
                }
            }
        }

        public static HashSet<string> GenerateExistingSeamGuids(Visio.Page visioPage)
        {
            HashSet<string> seamGuids = new HashSet<string>();

            foreach (Visio.Shape visioShape in visioPage.Shapes)
            {
                if (visioShape.Data1 != "Seam")
                {
                    continue;
                }

                if (visioShape.Data2 != "Line")
                {
                    continue;
                }

                string guid = visioShape.Data3;

                if (seamGuids.Contains(guid))
                {
                    continue;
                }

                seamGuids.Add(guid);
            }

            return seamGuids;
        }

    }
}
