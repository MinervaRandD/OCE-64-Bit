#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsSelection.cs. Project: Graphics. Created: 6/10/2024         */
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
using Utilities;

namespace Graphics
{
    public class GraphicsSelection: List<GraphicShape>
    {
        public GraphicsSelection() { }

        public List<GraphicShape> FilterByData1(string data1)
        {
            List<GraphicShape> rtrnList = new List<GraphicShape>();

            if (this.Count <= 0)
            {
                return rtrnList;
            }

            foreach (GraphicShape shape in this)
            {
                if (Utilities.Utilities.IsNotNull(shape.VisioShape))
                {
                    if (shape.VisioShape.Data1 == data1)
                    {
                        rtrnList.Add(shape);
                    }

                }
            }

            return rtrnList;
        }
    }
}
