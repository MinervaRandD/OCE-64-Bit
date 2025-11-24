//-------------------------------------------------------------------------------//
// <copyright file="Graphics.cs" company="Bruun Estimating, LLC">                // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Utilities
{
    using FloorMaterialEstimator.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Visio = Microsoft.Office.Interop.Visio;

    /// <summary>
    /// VisioSupport encapsulates ALL functionality related to the use of the visio embedded
    /// surface. The objective is to isolate the actual platform from the functionality in the
    /// code so that the impact will be minimal if a different surface is used.
    /// </summary>
    public static class Graphics
    {
        public static void SetLayerVisibility(Visio.Layer layer, bool visible = true)
        {
            if (visible)
            {
                layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "1";
            }

            else
            {
                layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "0";
            }
        }

        public static void SetShapeTransparency(Visio.Shape visioShape, double transparency)
        {
            visioShape.Cells["Transparency"].ResultIU = Math.Max(0.0, Math.Min(1.0, transparency));
        }

        internal static void SetBaseLineColor(GraphicsLine line, string lineColorFormula)
        {
            Visio.Shape visioShape = line.VisioShape;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLineColor]
                    .FormulaU = lineColorFormula;
        }

        internal static void SetBaseLineOpacity(GraphicsLine line, double opacity)
        {
            Visio.Shape visioShape = line.VisioShape;

            double transparency = 100.0 * Math.Max(0.0, Math.Min(1.0, 1.0 - opacity));

            visioShape.Cells["Transparency"].FormulaU = transparency.ToString("0.0") + "%";
        }

        internal static void SetBaseLineStyle(GraphicsLine line, string lineStyleFormula)
        {
            Visio.Shape visioShape = line.VisioShape;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLinePattern]
                    .FormulaU = lineStyleFormula;
        }

        public static void SetBaseFillColor(Visio.Shape visioShape, string colorFormula)
        {
            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowFill,
                (short)Visio.VisCellIndices.visFillForegnd]
                    .FormulaU = colorFormula;

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowFill,
                (short)Visio.VisCellIndices.visFillBkgnd]
                    .FormulaU = colorFormula;
        }


        internal static void SetLineWidth(GraphicsLine line, double lineWidthInPts)
        {
            Visio.Shape visioShape = line.VisioShape;
            visioShape.Cells["LineWeight"].FormulaU = lineWidthInPts.ToString("0.00") + " pt";
        }

        internal static void AddTextBox(Page page, Coordinate upperLeft, Coordinate lowerRght, string text)
        {
            Visio.Shape textBox = page.DrawRectangle(upperLeft.X, upperLeft.Y, lowerRght.X, lowerRght.Y);

            textBox.TextStyle = "Normal";
            textBox.LineStyle = "Text Only";
            textBox.FillStyle = "Text Only";

            textBox.CellsU["LinePattern"].ResultIU = 0;
            textBox.CellsU["FillPattern"].ResultIU = 0;

            textBox.Text = text;
        }

        internal static Coordinate GetShapeBeginPoint(Visio.Shape visioShape)
        {
            return new Coordinate(visioShape.Cells["BeginX"].ResultIU, visioShape.Cells["BeginY"].ResultIU);
        }

        internal static Coordinate GetShapeEndPoint(Visio.Shape visioShape)
        {
            return new Coordinate(visioShape.Cells["EndX"].ResultIU, visioShape.Cells["EndY"].ResultIU);
        }

        public static void SetNolineMode(Visio.Shape visioShape)
        {
            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowLine,
                (short)Visio.VisCellIndices.visLinePattern]
                    .FormulaU = "0";

            visioShape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowGradientProperties,
                (short)Visio.VisCellIndices.visLineGradientEnabled]
                    .FormulaU = "FALSE";
        }

        #region Layer related functionality

        internal static void LockLayer(Visio.Layer layer)
        {
            layer.CellsC[(short)Visio.VisCellIndices.visLayerLock].FormulaU = "1";
        }

        internal static void SetLayerOpacity(Visio.Layer layer, double opacity)
        {
            opacity = Math.Max(0, Math.Min(1.0, opacity));

            layer.CellsC[(short)Visio.VisCellIndices.visLayerColorTrans].FormulaU = (100.0 * (1.0 - opacity)).ToString("0") + '%';
        }

        internal static void SendToBack(Visio.Shape visioShape)
        {
            visioShape.SendToBack();
        }

        internal static ShapeSize GetShapeDimensions(Visio.Shape visioShape)
        { 
            double width = visioShape.Cells["width"].ResultIU;
            double height = visioShape.Cells["height"].ResultIU;

            return new ShapeSize(width, height);
        }

        internal static void SetEndpointArrows(Visio.Shape visioShape, int arrowIndex)
        {
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

        internal static double SetPageGrid(Page page, double gridCount, double gridOffset)
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

        internal static void SetPageSize(Page page, double width, double height)
        {
            Visio.Page visioPage = page.VisioPage;

            visioPage.PageSheet.CellsU["PageHeight"].ResultIU = height;
            visioPage.PageSheet.CellsU["PageWidth"].ResultIU = width;
        }

        internal static void SetPageSize(Page page,  ShapeSize size)
        {
            SetPageSize(page, size.Width, size.Height);

            //double width = Math.Ceiling(size.Width * 1.0e8) * 1.0e-8;
            //double height = Math.Ceiling(size.Height * 1.0e8) * 1.0e-8;

            //SetPageSize(visioPage, width, height);
        }

        internal static void SetYAxis(Visio.Window visioWindow, ShapeSize size)
        {
            double height = Math.Ceiling(size.Height * 1.0e8) * 1.0e-8;

            visioWindow.Shape.CellsSRC[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowRulerGrid,
                (short)Visio.VisCellIndices.visYRulerOrigin]
                    .FormulaU = height.ToString("0.00000000") + " in";
        }

        internal static void SetShapeLocation(Visio.Shape visioShape)
        {
            double locX = visioShape.Cells["pinx"].ResultIU;
            double locY = visioShape.Cells["piny"].ResultIU;

            ShapeSize size = GetShapeDimensions(visioShape);

            locX += size.Width * 0.5 - locX;
            locY += size.Height * 0.5 - locY;

            visioShape.Cells["pinx"].ResultIU = locX;
            visioShape.Cells["piny"].ResultIU = locY;
        }


        internal static void SetLineText(GraphicsLine line, string lineText)
        {
            Visio.Shape visioShape = line.VisioShape;

            visioShape.Text = lineText;
        }

        internal static void SetLineBeginShape(GraphicsLine line, int beginShapeIndex, int beginShapeSize)
        {
            Visio.Shape visioShape = line.VisioShape;

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

        internal static void SetLineEndShape(GraphicsLine line, int endShapeIndex, int endShapeSize)
        {
            Visio.Shape visioShape = line.VisioShape;

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

        public static void FormatCutBox(Visio.Shape visioShape)
        {
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
    }
}
