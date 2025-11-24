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
    using System.Linq;
    using System.Collections.Generic;
    using FinishesLib;
    using Globals;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Border manager is the class that implements the logic for fixed width (border) generation
    /// </summary>
    public class BorderManager
    {
        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public List<BorderGenerationMarker> BorderGenerationMarkerList = new List<BorderGenerationMarker>();

        public List<BorderGuideBoundary> BorderGuideBoundaryList = new List<BorderGuideBoundary>();

        public BorderGenerationState BorderGenerationState { get; set; } = BorderGenerationState.Unknown;

        private double widthInLocalInches = 0.0;

        private Label lblFixedWidthJump;

        public double WidthInLocalInches
        {
            get
            {
                return widthInLocalInches;
            }

            set
            {
                if (value == widthInLocalInches)
                {
                    return;
                }

                widthInLocalInches = value;

                updateBorderGuideBoundaries();
            }
        }

        private AreaFinishBase areaFinishBase = null;

        public AreaFinishBase AreaFinishBase
        {
            get
            {
                return areaFinishBase;
            }

            set
            {
                areaFinishBase = value;
            }
        }

        public bool IsLeftSelected { get; set; } = false;

        public bool IsRghtSelected { get; set; } = false;

        public bool IsBorderSelected => (IsLeftSelected || IsRghtSelected);

        #region Constructors

        public BorderManager(
            GraphicsWindow window
            , GraphicsPage page)
        {
            Window = window;

            Page = page;
        }

        public void Init(
            double widthInLocalInches
            , AreaFinishBase areaFinishBase
            , Label lblFixedWidthJump)
        {
            this.widthInLocalInches = widthInLocalInches;

            clearCurrentBorderElements();

            this.BorderGenerationState = BorderGenerationState.Initial;

            this.AreaFinishBase = areaFinishBase;

            this.lblFixedWidthJump = lblFixedWidthJump;

            this.AreaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;

            this.AreaFinishBase.OpacityChanged += AreaFinishBase_OpacityChanged;
        }

        public void CompleteCurrentBorderDrawing()
        {
            
        }

        private void AreaFinishBase_OpacityChanged(AreaFinishBase finishBase, double opacity)
        {
            updateBorderGuideColors(finishBase);
        }

        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, System.Drawing.Color color)
        {
            updateBorderGuideColors(finishBase);
        }

        public void ResetWidth(double widthInLocalInches)
        {
            this.WidthInLocalInches = widthInLocalInches;
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
            BorderGenerationMarkerList.ForEach(m => m.Delete());
            BorderGuideBoundaryList.ForEach(b => b.Delete());

            BorderGenerationMarkerList.Clear();
            BorderGuideBoundaryList.Clear();

            BorderGenerationState = BorderGenerationState.None;
           
        }

        public void Reset()
        {
            clearCurrentBorderElements();

            IsLeftSelected = false;

            IsRghtSelected = false;

            this.BorderGenerationState = BorderGenerationState.Initial;

            setLblFormat();

            SystemState.DrawingShape = false;
        }

        public void BorderDrawingModeClick(
            int button
            , double x
            , double y
            )
        {
            // Must be a right click or exit with no action
            if (button == 1)
            {
                if (Utilities.IsNotNull(AreaFinishBase))
                {
                    processBorderDrawingLeftClick(x, y, AreaFinishBase.Color, AreaFinishBase.Opacity);
                }

                else
                {
                    processBorderDrawingLeftClick(x, y, Color.White, 1);
                }

                setLblFormat();

                return;
            }

            if (button == 2)
            {
                processBorderDrawingRghtClick(x, y);

                setLblFormat();

                return;
            }
        }


        private void processBorderDrawingLeftClick(double x, double y, Color color, double opacity)
        {
             List<GraphicShape> shapeList = Page.GetSelectedBorderShapeList(x, y);


            foreach (GraphicShape shape in shapeList)
            {
                if (shape.VisioShape.Data2 == "BoxLeft")
                {
                    if (BorderGuideBoundaryList.Exists(b => b.BoxLeft.Guid == shape.Guid))
                    {
                        if (IsLeftSelected)
                        {
                            clearLeftSelection();
                        }

                        else if (IsRghtSelected)
                        {
                            clearRghtSelection();

                            setLeftSelection(color, opacity);
                        }

                        else
                        {
                            setLeftSelection(color, opacity);
                        }

                        return;
                    }
                }

                if (shape.VisioShape.Data2 == "BoxRght")
                {
                    if (BorderGuideBoundaryList.Exists(b => b.BoxRght.Guid == shape.Guid))
                    {
                        if (IsRghtSelected)
                        {
                            clearRghtSelection();
                        }

                        else if (IsLeftSelected)
                        {
                            clearLeftSelection();

                            setRghtSelection(color, opacity);
                        }

                        else
                        {
                            setRghtSelection(color, opacity);
                        }

                        return;
                    }
                }
            }
        }

        public List<Tuple<AreaFinishBase, List<Coordinate>>> GetDirectedPolylineElementList()
        {
            List<Tuple<AreaFinishBase, List<Coordinate>>> rtrnList = new List<Tuple<AreaFinishBase, List<Coordinate>>>();

            if (!IsLeftSelected && !IsRghtSelected)
            {
                return rtrnList;
            }

            foreach (BorderGuideBoundary borderGuideBoundary in this.BorderGuideBoundaryList)
            {
                AreaFinishBase areaFinishBase = borderGuideBoundary.AreaFinishBase;
                List<Coordinate> coordinateList = borderGuideBoundary.GenerateCoordinateList(IsLeftSelected, IsRghtSelected);

                rtrnList.Add(new Tuple<AreaFinishBase, List<Coordinate>>(areaFinishBase, coordinateList));
            }

            return rtrnList;
        }

        private void clearLeftSelection()
        {
            IsLeftSelected = false;

            foreach (BorderGuideBoundary borderGuideBoundry in BorderGuideBoundaryList)
            {
                borderGuideBoundry.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
            }
        }

        private void setLeftSelection(Color color, double opacity)
        {
            IsLeftSelected = true;

            foreach (BorderGuideBoundary borderGuideBoundry in BorderGuideBoundaryList)
            {
                borderGuideBoundry.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
            }
        }

        private void clearRghtSelection()
        {
            IsRghtSelected = false;

            foreach (BorderGuideBoundary borderGuideBoundry in BorderGuideBoundaryList)
            {
                borderGuideBoundry.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
            }
        }

        private void setRghtSelection(Color color, double opacity)
        {
            IsRghtSelected = true;

            foreach (BorderGuideBoundary borderGuideBoundry in BorderGuideBoundaryList)
            {
                borderGuideBoundry.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);

            }
        }

        private void processBorderDrawingRghtClick(double x, double y)
        {
            // Take action base on the current fixed width state.

            switch (BorderGenerationState)
            {
                case BorderGenerationState.Initial: // Initial state, draw first point
                    
                    BorderFrstPointClicked(x, y);
                    Page.LastPointDrawn = new Coordinate(x, y);

                    return;

                case BorderGenerationState.FrstPointSelected: // First point drawn, draw second state

                    BorderNextPointClicked(x, y);
                    Page.LastPointDrawn = new Coordinate(x, y);

                    return;

                case BorderGenerationState.OngoingBorderBuild: // Ongoing sequence being built.

                    BorderNextPointClicked(x, y);
                    Page.LastPointDrawn = new Coordinate(x, y);

                    return;
            }
        }

        private void BorderFrstPointClicked(double x, double y)
        {
            BorderGenerationMarker borderGenerationMarker = new BorderGenerationMarker(Window, Page, x, y, 0.2);

            borderGenerationMarker.Draw();

            BorderGenerationMarkerList.Add(borderGenerationMarker);

            SystemState.DrawingShape = true;

            SystemState.DrawingMode = DrawingMode.FixedWidth;

            this.BorderGenerationState = BorderGenerationState.FrstPointSelected;

        }

       
        private void BorderNextPointClicked(double x, double y)
        {

            Coordinate coord1 = BorderGenerationMarkerList.Last().Coordinate();

            BorderGenerationMarker borderGenerationMarker = new BorderGenerationMarker(Window, Page, x, y, 0.2);

            Coordinate coord2 = new Coordinate(x, y);

            borderGenerationMarker.Draw();

            BorderGenerationMarkerList.Add(borderGenerationMarker);

            BorderGuideBoundary borderGuideBoundary = new BorderGuideBoundary(
                Window
                , Page
                , coord1
                , coord2
                , WidthInLocalInches
                , AreaFinishBase);

            borderGuideBoundary.Draw();

            borderGuideBoundary.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
           
            BorderGuideBoundaryList.Add(borderGuideBoundary);

            Window?.DeselectAll();

            SystemState.DrawingShape = true;

            this.BorderGenerationState = BorderGenerationState.OngoingBorderBuild;
        }

        public double LastBoundaryGuideLength()
        {
            if (BorderGuideBoundaryList.Count <= 0)
            {
                return 0;
            }

            return BorderGuideBoundaryList.Last().BoundaryGuideLength();
        }

        public Coordinate LastMarkerCoordinate()
        {
            if (BorderGenerationMarkerList.Count <= 0)
            {
                return Coordinate.NullCoordinate;
            }

            return BorderGenerationMarkerList.Last().Coordinate();
        }

        public void DeleteLastMarker()
        {
            if (BorderGenerationMarkerList.Count <= 0)
            {
                this.BorderGenerationState = BorderGenerationState.Initial;

                this.IsRghtSelected = false;

                this.IsLeftSelected = false;

                SystemState.DrawingShape = false;

                SystemState.DrawingMode = DrawingMode.Default;

                setLblFormat();

                return;
            }

            if (BorderGenerationMarkerList.Count == 1)
            {
                clearCurrentBorderElements();

                this.IsRghtSelected = false;

                this.IsLeftSelected = false;

                this.BorderGenerationState = BorderGenerationState.Initial;

                SystemState.DrawingShape = false;

                SystemState.DrawingMode = DrawingMode.Default;

                setLblFormat();

                return;
            }

            BorderGenerationMarker lastMarker = BorderGenerationMarkerList.Last();

            BorderGuideBoundary lastBorderBuidBoundary = BorderGuideBoundaryList.Last();

            BorderGenerationMarkerList.RemoveAt(BorderGenerationMarkerList.Count - 1);

            BorderGuideBoundaryList.RemoveAt(BorderGuideBoundaryList.Count - 1);

            lastMarker.Delete();

            lastBorderBuidBoundary.Delete();
        }

        private void updateBorderGuideBoundaries()
        {
            foreach (BorderGuideBoundary borderGuideBoundary in this.BorderGuideBoundaryList)
            {
                borderGuideBoundary.UpdateBoundaryWidth(this.WidthInLocalInches);

                if (Utilities.IsNotNull(AreaFinishBase))
                {
                    borderGuideBoundary.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
                }

                else
                {
                    borderGuideBoundary.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
                }
            }
        }


        private void updateBorderGuideColors(AreaFinishBase areaFinishBase)
        {
            foreach (BorderGuideBoundary borderGuideBoundary in this.BorderGuideBoundaryList)
            {
                if (borderGuideBoundary.AreaFinishBase == areaFinishBase)
                {
                    borderGuideBoundary.UpdateBorderGuideColors(IsLeftSelected, IsRghtSelected);
                }
            }
        }


        private void setLblFormat()
        {
            if (IsBorderSelected)
            {
                if (this.lblFixedWidthJump.BackColor == Color.Orange)
                {
                    return;
                }

                this.lblFixedWidthJump.BackColor = Color.Orange;
                this.lblFixedWidthJump.ForeColor = Color.Black;
            }

            else
            {
                if (this.lblFixedWidthJump.BackColor == SystemColors.ControlLightLight)
                {
                    return;
                }

                this.lblFixedWidthJump.BackColor = SystemColors.ControlLightLight;
                this.lblFixedWidthJump.ForeColor = Color.Orange;
            }
        }
    }

}
