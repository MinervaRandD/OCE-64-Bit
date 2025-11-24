//-------------------------------------------------------------------------------//
// <copyright file="AreaShape.cs" company="Bruun Estimating, LLC">               // 
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
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Seams_And_Cuts;

    using Visio = Microsoft.Office.Interop.Visio;

    public abstract class AreaShape
    {
        private static int areaShapeIndex = 1;

        public Page Page { get; }

        public CanvasManager CanvasManager { get; set; }


        public AreaShape(CanvasManager canvasManager, ShapeType ShapeType)
        {
            this.CanvasManager = canvasManager;

            this.ID = areaShapeIndex++;

            this.ShapeType = ShapeType;

            this.Page = canvasManager.CurrentPage;
        }

        public AreaShape(CanvasManager canvasManager, ShapeType ShapeType, Shape internalAreaShape)
        {
            this.CanvasManager = canvasManager;

            this.InternalAreaShape = internalAreaShape;

            this.ShapeType = ShapeType;

            this.ID = areaShapeIndex++;
        }

        public ShapeType ShapeType { get; set; }

        public int ID{ get; set; }

        public Perimeter Perimeter { get; set; }

        private Shape _InternalAreaShape = null;

        public Shape InternalAreaShape
        {
            get
            {
                return _InternalAreaShape;
            }

            set
            {
                _InternalAreaShape = value;

                _InternalAreaShape.VisioShape.CellChanged += VisioShape_CellChanged;
            }
        }

        private void VisioShape_CellChanged(Visio.Cell Cell)
        {
            if (ucFinish != null)
            {
                ucFinish.UpdateFinishStats();
            }
        }

        public UCFinish ucFinish {
            get;
            set;
        }

        public double PerimeterLength()
        {
            return Perimeter.TotalLength();
        }

        public double InternalArea()
        {
            return InternalAreaShape.VisioShape.AreaIU;
        }

        public ShapeType InternalAreaShapeType
        {
            get
            {
                if (InternalAreaShape == null)
                {
                    return ShapeType.Unknown;
                }

                return InternalAreaShape.ShapeType;
            }

            set
            {
                InternalAreaShape.ShapeType = value;
            }
        }

        public void SetFillColor(string fillColorFormula)
        {
            InternalAreaShape.SetFillColor(fillColorFormula);
        }

        public void SetFillOpacity(string fillOpacityFormula)
        {
            InternalAreaShape.SetFillOpacity(fillOpacityFormula);
        }

        private AreaShapeBuildStatus buildStatus = AreaShapeBuildStatus.Unknown;

        internal AreaShapeBuildStatus BuildStatus
        {
            get
            {
                return buildStatus;
            }

            set
            {
                buildStatus = value;
            }
        }

        public List<SeamLine> SeamLineList { get; set; } = new List<SeamLine>();
        public List<Cut> cutList = new List<Cut>();

        internal void DoSeemsAndCuts(UCLine ucLine, int lineIndex, double rollWidthInInches)
        {
            SeamLineList.ForEach(s => s.Delete());

            cutList.ForEach(c => c.DeleteBoundary());

            SeamLineList.Clear();
            cutList.Clear();

            SeamsAndCutsGenerator seamsAndCutsGenerator = new SeamsAndCutsGenerator(Perimeter, lineIndex, rollWidthInInches);

            List<Seam> seamList = seamsAndCutsGenerator.GenerateSeamList();

            seamList.ForEach(s => SeamLineList.Add(new SeamLine(s, ucLine, this.CanvasManager)));

            cutList = seamsAndCutsGenerator.GenerateCutList();

            cutList.ForEach(c => c.DrawBoundary(CanvasManager.CurrentPage));

            List<Overage> overageList = seamsAndCutsGenerator.GenerateOverages(0.25);
        }
    }
}
