//-------------------------------------------------------------------------------//
// <copyright file="BorderManager.cs"                                            //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace CanvasLib.Borders
{
    using Geometry;
    using Graphics;
    using Utilities;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using CanvasLib.Design_States_and_Modes;
    using CanvasLib.Markers_and_Guides;
    using Geometry;
    using MaterialsLayout;

    /// <summary>
    /// Border manager is the class that implements the logic for fixed width (border) generation
    /// </summary>
    public class BorderManagerOld
    {
        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }


        public Coordinate BorderFrstCoord { get; set; } = Coordinate.NullCoordinate;

        public Coordinate BorderScndCoord { get; set; } = Coordinate.NullCoordinate;

        public BorderGenerationState BorderGenerationState { get; set; } = BorderGenerationState.Unknown;

        public BorderGenerationMarker BorderFrstMarker { get; set; }  = null;
        
        public BorderGenerationMarker BorderScndMarker { get; set; }  = null;

        private Shape borderGuideLine = null;

        private double widthInLocalInches = 0.0;

        //private List<Shape> borderGuideLineList = new List<Shape>();

        private Guide guide = null;

        #region Constructors

        public BorderManagerOld(CanvasManager canvasManager, GraphicsWindow window, GraphicsPage page)
        {
            CanvasManager = canvasManager;

            Window = window;

            Page = page;

        }

        public void Init(double widthInLocalInches)
        {
            BorderGenerationState = BorderGenerationState.Initial;

            this.widthInLocalInches = widthInLocalInches;

            clearCurrentBorderElements();

            BorderGenerationState = BorderGenerationState.Initial;
        }

        public void ResetWidth(double widthInLocalInches)
        {
            this.widthInLocalInches = widthInLocalInches;
        }

        #endregion

        /// <summary>
        /// Exits the fixed width (border) state.
        /// </summary>
        public void Exit()
        {
            clearCurrentBorderElements();
        }

        private void clearCurrentBorderElements()
        {
            BorderGenerationState = BorderGenerationState.None;

            BorderFrstCoord = Coordinate.NullCoordinate;

            BorderScndCoord = Coordinate.NullCoordinate;

            if (Utilities.IsNotNull(BorderFrstMarker))
            {
                BorderFrstMarker.Delete();

                BorderFrstMarker = null;
            }

            if (Utilities.IsNotNull(BorderScndMarker))
            {
                BorderScndMarker.Delete();

                BorderScndMarker = null;
            }

            if (Utilities.IsNotNull(borderGuideLine ))
            {
                borderGuideLine.Delete();

                borderGuideLine = null;
            }
          
            if (Utilities.IsNotNull(guide))
            {
                guide.Delete();

                guide = null;
            }
           
        }

        /// <summary>
        /// Manages a fixed width (border) state click
        /// </summary>
        /// <param name="button">The button that was clicked </param>
        /// <param name="x">The x coordinate that the mouse was at the click</param>
        /// <param name="y">The y coordinate that the mouse was at the click</param>
        /// <param name="drawingShape">The reference to the drawing shape flag (for main canvas)</param>
        public void BorderDrawingModeClick(int button, double x, double y, ref bool drawingShape)
        {
            // Must be a right click or exit with no action
            if (button != 2)
            {
                return;
            }

            // Take action base on the current fixed width state.

            switch (BorderGenerationState)
            {
                case BorderGenerationState.Initial: // Initial state, draw first point
                    BorderFrstPointClicked(x, y, ref drawingShape);
                    return;

                case BorderGenerationState.FrstPointSelected: // First point drawn, draw second state


                    BorderScndPointClicked(x, y, ref drawingShape);

                    return;

                case BorderGenerationState.ScndPointSelected: // Second point, get direction click.

                    BorderDrctPointClicked(x, y, ref drawingShape);
                    return;
            }
        }

        /// <summary>
        /// Handles the first click during border creation.
        /// 
        /// Puts the first marker on the canvas, and sets drawing shape to true
        /// </summary>
        /// <param name="x">The x coordinate that the mouse was at the click</param>
        /// <param name="y">The y coordinate that the mouse was at the click</param>
        /// <param name="drawingShape">The reference to the drawing shape flag (for main canvas)</param>
        private void BorderFrstPointClicked(double x, double y, ref bool drawingShape)
        {
           

            this.BorderGenerationState = BorderGenerationState.FrstPointSelected;

            this.BorderFrstMarker = new BorderGenerationMarker(x, y, 0.2);

            BorderFrstMarker.Draw(Page);

            BorderFrstCoord = new Coordinate(x, y);

            if (Utilities.IsNotNull(guide))
            {
                guide.Delete();

                guide = null;
            }

            guide = new Guide(x, y);

            guide.Draw(Page);

            VisioInterop.DeselectAll(Window);

            CanvasManager.DrawingMode = FloorMaterialEstimator.DrawingMode.BorderGeneration;

            drawingShape = true;
        }

        /// <summary>
        /// Handles the second click during border creation.
        /// 
        ///  Puts the second marker on the canvas, and sets drawing shape to true
        /// </summary>
        /// <param name="x">The x coordinate that the mouse was at the click</param>
        /// <param name="y">The y coordinate that the mouse was at the click</param>
        /// <param name="drawingShape">The reference to the drawing shape flag (for main canvas)</param>
        private void BorderScndPointClicked(double x, double y, ref bool drawingShape)
        {
            // The following logic checks to see if the user clicked near the first marker. If so, it is removed
            // from the canvas and the system is put back into the initial state.

            if (!Coordinate.IsNullCoordinate(BorderFrstCoord))
            {
                if (MathUtils.H2Distance(x, y, BorderFrstCoord.X, BorderFrstCoord.Y) < 0.2)
                {
                    if (Utilities.IsNotNull(BorderFrstMarker))
                    {
                        BorderFrstMarker.Delete();

                        BorderFrstMarker = null;
                    }

                    if (Utilities.IsNotNull(guide))
                    {
                        guide.Delete();

                        guide = null;
                    }

                    BorderFrstCoord = Coordinate.NullCoordinate;

                    BorderGenerationState = BorderGenerationState.Initial;

                    drawingShape = false;

                    CanvasManager.DrawingMode = FloorMaterialEstimator.DrawingMode.Default;

                    return;
                }
            }

            //if (MathUtils.H2Distance(x, y, this.BorderFrstCoord.X, this.BorderFrstCoord.Y) <= 0.2)
            //{
            //    return;
            //}

            if (Utilities.IsNotNull(guide))
            {
                guide.Delete();

                guide = null;
            }

            this.BorderGenerationState = BorderGenerationState.ScndPointSelected;

            this.BorderScndMarker = new BorderGenerationMarker(x, y, 0.2);

            BorderScndMarker.Draw(Page);

            BorderScndCoord = new Coordinate(x, y);

            drawBorderGuideLine();

            VisioInterop.DeselectAll(Window);

            drawingShape = true;
        }

        /// <summary>
        /// Draws the border guide line.
        /// 
        /// This is the line that goes between the two markers.
        /// </summary>
        private void drawBorderGuideLine()
        {
            borderGuideLine = Page.DrawLine(BorderFrstCoord.X, BorderFrstCoord.Y, BorderScndCoord.X, BorderScndCoord.Y, string.Empty);

            VisioInterop.SetBaseLineColor(borderGuideLine, Color.Black);
            VisioInterop.SetBaseLineStyle(borderGuideLine, VisioLineStyle.HalfDot);
            VisioInterop.SetLineWidth(borderGuideLine, 2);

            //borderGuideLineList.Add(borderGuideLine);
        }

        /// <summary>
        /// Process the final (direction indicating) click.
        /// Create a new fixed width border
        /// </summary>
        /// <param name="x">The x coordinate that the mouse was at the click</param>
        /// <param name="y">The yx coordinate that the mouse was at the click</param>
        /// <param name="drawingShape">The reference to the drawing shape flag (for main canvas)</param>
        private void BorderDrctPointClicked(double x, double y, ref bool drawingShape)
        {
            // The following logic checks to see if the user clicked near the first marker. If so, it is removed
            // from the canvas and the second marker is reassigned to be the first marker. The system is then put back into the 
            // state of having the first marker on the canvas.

            if (!Coordinate.IsNullCoordinate(BorderFrstCoord))
            {
                if (MathUtils.H2Distance(x, y, BorderFrstCoord.X, BorderFrstCoord.Y) < 0.2)
                {
                    if (Utilities.IsNotNull(BorderFrstMarker))
                    {
                        BorderFrstMarker.Delete();

                        BorderFrstMarker = null;
                    }

                    if (Utilities.IsNotNull(guide))
                    {
                        guide.Delete();

                        guide = null;
                    }

                    if (Utilities.IsNotNull(borderGuideLine))
                    {
                        borderGuideLine.Delete();

                        borderGuideLine = null;
                    }

                    BorderFrstMarker = BorderScndMarker;

                    BorderFrstCoord = BorderScndCoord;

                    BorderScndMarker = null;

                    BorderScndCoord = Coordinate.NullCoordinate;

                    guide = new Guide(BorderFrstCoord.X, BorderFrstCoord.Y);

                    guide.Draw(Page);

                    BorderGenerationState = BorderGenerationState.FrstPointSelected;

                    return;
                }
            }

            // The following logic checks to see if the user clicked near the second marker. If so, the second marker is removed
            // from the canvas. The system is then put back into the 
            // state of having the first marker on the canvas.

            if (!Coordinate.IsNullCoordinate(BorderScndCoord))
            {
                if (MathUtils.H2Distance(x, y, BorderScndCoord.X, BorderScndCoord.Y) < 0.15)
                {
                    if (Utilities.IsNotNull(BorderScndMarker))
                    {
                        BorderScndMarker.Delete();

                        BorderScndMarker = null;
                    }

                    //if (Utilities.IsNotNull(guide))
                    //{
                    //    guide.Delete();

                    //    guide = null;
                    //}

                    if (Utilities.IsNotNull(borderGuideLine))
                    {
                        borderGuideLine.Delete();

                        borderGuideLine = null;
                    }

                    BorderScndCoord = Coordinate.NullCoordinate;

                    guide = new Guide(BorderFrstCoord.X, BorderFrstCoord.Y);

                    guide.Draw(Page);

                    BorderGenerationState = BorderGenerationState.FrstPointSelected;

                    return;
                }
            }

            //if (MathUtils.H2Distance(x, y, this.BorderFrstCoord.X, this.BorderFrstCoord.Y) <= 0.2)
            //{
            //    return;
            //}

            //if (MathUtils.H2Distance(x, y, this.BorderScndCoord.X, this.BorderScndCoord.Y) <= 0.2)
            //{
            //    return;
            //}

            double x1 = BorderFrstCoord.X;
            double x2 = BorderScndCoord.X;

            double y1 = BorderFrstCoord.Y;
            double y2 = BorderScndCoord.Y;

            double atan = MathUtils.Atan(x1, y1, x2, y2);

            Coordinate coord = new Coordinate(x, y);

            coord.Transform(-BorderFrstCoord, -atan);

            Coordinate frstCoord = BorderFrstCoord;
            Coordinate scndCoord = BorderScndCoord;

            frstCoord.Transform(-BorderFrstCoord, -atan); // Should end up at zero -- but just a sanity test.
            scndCoord.Transform(-BorderFrstCoord, -atan);

            Coordinate thrdCoord = new Coordinate(scndCoord.X, scndCoord.Y + Math.Sign(coord.Y) * widthInLocalInches);
            Coordinate frthCoord = new Coordinate(frstCoord.X, scndCoord.Y + Math.Sign(coord.Y) * widthInLocalInches);

            DirectedLine line1 = new DirectedLine(frstCoord, scndCoord);
            DirectedLine line2 = new DirectedLine(scndCoord, thrdCoord);
            DirectedLine line3 = new DirectedLine(thrdCoord, frthCoord);
            DirectedLine line4 = new DirectedLine(frthCoord, frstCoord);

            GraphicsDirectedLine gLine1 = new GraphicsDirectedLine(Window, Page, line1, LineRole.Perimeter);
            GraphicsDirectedLine gLine2 = new GraphicsDirectedLine(Window, Page, line2, LineRole.Perimeter);
            GraphicsDirectedLine gLine3 = new GraphicsDirectedLine(Window, Page, line3, LineRole.Perimeter);
            GraphicsDirectedLine gLine4 = new GraphicsDirectedLine(Window, Page, line4, LineRole.Perimeter);

            CanvasDirectedLine cLine1 = new CanvasDirectedLine(this.CanvasManager, gLine1, DesignState.Area);
            CanvasDirectedLine cLine2 = new CanvasDirectedLine(this.CanvasManager, gLine2, DesignState.Area);
            CanvasDirectedLine cLine3 = new CanvasDirectedLine(this.CanvasManager, gLine3, DesignState.Area);
            CanvasDirectedLine cLine4 = new CanvasDirectedLine(this.CanvasManager, gLine4, DesignState.Area);

            cLine1.LineRole = LineRole.NullPerimeter;
            cLine2.LineRole = LineRole.NullPerimeter;
            cLine3.LineRole = LineRole.NullPerimeter;
            cLine4.LineRole = LineRole.NullPerimeter;

            CanvasManager.SelectedLineType.AddLine(cLine1);
            CanvasManager.SelectedLineType.AddLine(cLine2);
            CanvasManager.SelectedLineType.AddLine(cLine3);
            CanvasManager.SelectedLineType.AddLine(cLine4);

            CanvasManager.CurrentPage.AddDirectedLine(cLine1);
            CanvasManager.CurrentPage.AddDirectedLine(cLine2);
            CanvasManager.CurrentPage.AddDirectedLine(cLine3);
            CanvasManager.CurrentPage.AddDirectedLine(cLine4);

            CanvasDirectedPolygon canvasDirectedPolygon = new CanvasDirectedPolygon(this.CanvasManager,  new List<CanvasDirectedLine>() { cLine1, cLine2, cLine3, cLine4 });

            canvasDirectedPolygon.InverseTransform(atan, BorderFrstCoord);

            CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(
                CanvasManager, canvasDirectedPolygon
                , new List<CanvasDirectedPolygon>()
                , DesignState.Area
                , CanvasManager.BaseForm.areaPallet.ckbColorOnly.Checked);

            canvasLayoutArea.IsBorderArea = true;

            canvasLayoutArea.BorderAreaRectangle = new Geometry.Rectangle(frstCoord, thrdCoord, atan);


            canvasLayoutArea.DrawBasic(Window, Page, CanvasManager.selectedFinishType.FinishColor, CanvasManager.SelectedLineType.LineColor);

            CanvasManager.selectedFinishType.AddNormalLayoutArea(canvasLayoutArea);

            CanvasManager.CurrentPage.AddLayoutArea(canvasLayoutArea);


            //VisioInterop.SetNolineMode(canvasLayoutArea.Shape);

            foreach (CanvasDirectedLine canvasDirectedLine in canvasLayoutArea.ExternalArea)
            {
                canvasDirectedLine.Draw();
                canvasDirectedLine.SetBaseLineOpacity(0);
            }

            // Create an overage or underage associated with this border area depending on width of the border area.

            if (this.CanvasManager.selectedFinishType.MaterialsType == FinishesLib.MaterialsType.Rolls)
            {
                double rollWidthInInches = this.CanvasManager.selectedFinishType.RollWidthInInches;

                canvasLayoutArea.setupBorderAreaOverageOrUndrage();
            }



            BorderFrstMarker.Delete();
            BorderScndMarker.Delete();

            BorderFrstCoord = Coordinate.NullCoordinate;
            BorderScndCoord = Coordinate.NullCoordinate;

            borderGuideLine.Delete();

            borderGuideLine = null;

            this.BorderGenerationState = BorderGenerationState.Initial;

            CanvasManager.DrawingMode = FloorMaterialEstimator.DrawingMode.Default;
            CanvasManager.drawingShape = false;

            if (this.CanvasManager.selectedFinishType.MaterialsType == FinishesLib.MaterialsType.Rolls)
            {
                this.CanvasManager.BaseForm.OversUndersFormUpdate();
            }
        }

    }

}
