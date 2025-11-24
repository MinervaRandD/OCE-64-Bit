//-------------------------------------------------------------------------------//
// <copyright file="Page.cs" company="Bruun Estimating, LLC">                    // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using stdole;
    using Visio = Microsoft.Office.Interop.Visio;

    public class Page
    {
        public Visio.Page VisioPage;

        public int ID
        {
            get { return VisioPage.ID; }
        }

        public Page(Visio.Page visioPage)
        {
            this.VisioPage = visioPage;

            visioPage.BeforeShapeDelete += VisioPage_BeforeShapeDelete;
        }

        private void VisioPage_BeforeShapeDelete(Visio.Shape visioShape)
        {
            this.DeleteFromVisioShapDict(visioShape.NameID);
        }

        public string Name
        {
            get
            {
                return VisioPage.Name;
            }

            internal set
            {
                VisioPage.Name = value;
            }
        }

        public double DrawingScale { get; set; } = 12.0;

        public Dictionary<string, Shape> ShapeDict = new Dictionary<string, Shape>();

        /// <summary>
        /// Attempts to delete the visio shape of a line from the local visio shape dictionary
        /// </summary>
        /// <param name="line">The line with the visio shape to be deleted</param>
        internal void DeleteFromVisioShapDict(Shape shape)
        {
            if (shape == null)
            {
                return;
            }

            if (shape.VisioShape == null)
            {
                return;
            }

            DeleteFromVisioShapDict(shape.NameID);
        }

        /// <summary>
        /// Attempts to delete the visio shape of a line from the local visio shape dictionary
        /// </summary>
        /// <param name="line">The line with the visio shape to be deleted</param>
        internal void DeleteFromVisioShapDict(string nameID)
        {
            if (!this.ShapeDict.ContainsKey(nameID))
            {
                return;
            }

            this.ShapeDict.Remove(nameID);
        }

        /// <summary>
        /// Gets or creates the drawing layer for the specified page.
        /// </summary>
        /// <param name="visioPage">The visio page from which to get the drawing layer</param>
        /// <returns>Returns the drawing layer that was either created or already existed</returns>
        public Visio.Layer GetDrawingLayer()
        {
            foreach (Visio.Layer layer in VisioPage.Layers)
            {
                if (layer.Name == "DrawingLayer")
                {
                    return layer;
                }
            }

            return VisioPage.Layers.Add("DrawingLayer");
        }

        /// <summary>
        /// Attempts to get the drawing layer for the specified page.
        /// </summary>
        /// <param name="visioPage">The visio page on which to search for the drawing layer</param>
        /// <returns>Returns the drawing layer if it exists</returns>
        public Visio.Layer GetDrawingLayerIfExists()
        {
            foreach (Visio.Layer layer in VisioPage.Layers)
            {
                if (layer.Name == "DrawingLayer")
                {
                    return layer;
                }
            }

            return null;
        }

        internal Visio.Shape DrawLine(double x1, double y1, double x2, double y2)
        {
            return VisioPage.DrawLine(x1, y1, x2, y2);
        }

        internal Visio.Shape DrawRectangle(double x1, double y1, double x2, double y2)
        {
            return VisioPage.DrawRectangle(x1, y1, x2, y2);
        }

        internal Visio.Shape DrawPolyline(double[] coordinateArray, short flags)
        {
            return VisioPage.DrawPolyline(coordinateArray, flags);
        }
    }
}
