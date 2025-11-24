//-------------------------------------------------------------------------------//
// <copyright file="Shape.cs" company="Bruun Estimating, LLC">                   // 
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
    using FloorMaterialEstimator.Utilities;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.CanvasManager;

    using Visio = Microsoft.Office.Interop.Visio;

    public class Shape
    {
        public Visio.Shape VisioShape;

        public ShapeType ShapeType;

        public string NameID
        {
            get
            {
                if (VisioShape == null)
                {
                    return string.Empty;
                }

                return VisioShape.NameID;
            }
        }

        public Shape() { }
        
        public Shape(Visio.Shape visioShape, ShapeType shapeType)
        {
            this.VisioShape = visioShape;

            this.ShapeType = shapeType;
        }

        public virtual void SetFillColor(string colorFormula)
        {
            Graphics.SetBaseFillColor(this.VisioShape, colorFormula);
        }

        public virtual void SetNolineMode()
        {
            Graphics.SetNolineMode(this.VisioShape);
        }

        public void SetFillOpacity(string opacityFormula)
        {
            VisioShape.CellsSRC[
                (int)Visio.VisSectionIndices.visSectionObject,
                (int)Visio.VisRowIndices.visRowFill,
                (int)Visio.VisCellIndices.visFillForegndTrans]
                    .FormulaU = opacityFormula;

            VisioShape.CellsSRC[
                 (int)Visio.VisSectionIndices.visSectionObject,
                 (int)Visio.VisRowIndices.visRowFill,
                 (int)Visio.VisCellIndices.visFillBkgndTrans]
                    .FormulaU = opacityFormula;
        }

        public virtual void Delete()
        {
            VisioShape.Delete();
        }
    }
}
