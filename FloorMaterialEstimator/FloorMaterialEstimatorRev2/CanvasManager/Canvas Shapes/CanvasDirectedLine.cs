//-------------------------------------------------------------------------------//
// <copyright file="CanvasDirectedLine.cs" company="Bruun Estimating, LLC">      // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019, 2020  //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
 
    using FloorMaterialEstimator.Finish_Controls;

    using Graphics;
    using SettingsLib;
    using PaletteLib;
    using Globals;
    using Utilities;
    using MaterialsLayout;
    using CanvasLib.CanvasShapes;
    using Geometry;
    using FinishesLib;
    using global::CanvasManager.CanvasShapes;

    /// <summary>
    /// CanvasDirectedLine
    /// 
    ///     This is the basic line in the system although one could argue that there should be mulitple derived types.
    ///     
    ///     In trying to straighten out it use, so far there are 2 use cases that have been worked on:
    ///     
    ///     1) A perimeter line which defines the perimeter of a layout area.  Currenlty these are also placed with the 
    ///     line pallets (and they shouldn't).  These lines show up temporaily while building a layout area and also appear in 'Seam' mode.
    ///     
    ///     2) A Line which can either be singular our double.  These lines are connected to a line pallet.  These are the lines that are
    ///     manipulated in 'Line' mode.  Lines are also created from perimeter lines and so become a 'PerimeterRelatedLine'.  While normal lines can
    ///     be added or removed as needed, these PerimeterRelated lines never go away until the layout area goes away. 'Deleting' them causes the not to 
    ///     be deleted but to become a 'ZeroLine'.  These lines only appear in 'Line' mode.
    ///     
    ///     3) There are other uses for lines but these haven't yet been documented.
    /// </summary>
    [Serializable]
    public class CanvasDirectedLine: GraphicsDirectedLine, IGraphicsShape
    {
        private CanvasManager canvasManager;

        public CanvasLineType CanvasLineType { get; set; } = CanvasLineType.Normal;

        public CanvasDirectedLine() { }

        public CanvasDirectedLine(
            CanvasManager canvasManager
            , GraphicsWindow window
            , GraphicsPage page
            , LineFinishManager lineFinishManager
            , GraphicsDirectedLine line
            , DesignState designState
            , bool isDoubleLine = false)
            : base(window, page, line, line.LineRole, isDoubleLine)
        {
            OriginatingDesignState = designState;

            this.canvasManager = canvasManager;

            this.LineFinishManager = lineFinishManager;
        }

        public CanvasDirectedLine(
            CanvasManager canvasManager
            , LineFinishManager lineFinishManager
            , GraphicsDirectedLine graphicsDirectedLine
            , DesignState designState):
            base(graphicsDirectedLine.Window
                , graphicsDirectedLine.Page
                , graphicsDirectedLine
                , graphicsDirectedLine.LineRole
                , graphicsDirectedLine.LineCompoundType == LineCompoundType.Double)
        {
            OriginatingDesignState = designState;

            this.canvasManager = canvasManager;

            this.LineFinishManager = lineFinishManager;

        }

        public CanvasDirectedLine(CanvasManager canvasManager, DirectedLineSerializable line)
        {
            this.canvasManager = canvasManager;

            this.Guid = line.Guid;
            this.IsZeroLine = line.IsZeroLine;
            this.IsCompleteLLine = line.IsCompleteLLine;
            this.OriginatingDesignState = line.OriginatingDesignState;
            this.LineCompoundType = line.LineCompoundType;
            this.LineDrawoutMode = line.LineDrawoutMode;
            //this.ParentPolygonGuid = line.ParentPolygonGuid;

            this.Coord1 = line.Coord1;
            this.Coord2 = line.Coord2;

            this.LineRole = line.LineRole;

            Page = canvasManager.CurrentPage;

            UCLineFinishPalette linePalette = canvasManager.LineFinishPalette;

            ucLine = linePalette[line.LineFinishGuid];

            LineFinishManager = canvasManager.LineFinishManagerList[line.LineFinishGuid];

            IsSeamable = line.IsSeamable;

        }

        public bool IsDeleteable
        {
            get
            {
                bool deleteable = true;

                if (ParentPolygon != null)
                {
                    deleteable = false;
                } 
                else if (AssociatedDirectedLine != null
                        && AssociatedDirectedLine.IsZeroLine)
                { 
                    deleteable = false;
                }

                return deleteable;
            }
        }

        public bool IsPerimeterRelatedLine
        {
            get
            {
                return
                    (AssociatedDirectedLine != null) &&
                    ((AssociatedDirectedLine.LineRole == LineRole.ExternalPerimeter || AssociatedDirectedLine.LineRole == LineRole.InternalPerimeter));
            }
        }

        public void RemoveLineFinish()
        {
            if (this.LineFinishManager != null)
            {
                this.LineFinishManager.RemoveLine(this);
            }
        }

        public void SetLineFinish(LineFinishManager lineFinishManager)
        {
            this.SetZeroLine(false);    // temporarily here (Because project importer is still calling AddLineFull for zerolines
            lineFinishManager.AddLineFull(this);
            this.LineFinishManager = lineFinishManager;
        }

        public Point LineStartCursorPosition;

        public UCLineFinishPaletteElement ucLine;

        private LineFinishManager _lineFinishManager = null;

        public LineFinishManager LineFinishManager
        {
            get
            {
                if (_lineFinishManager is null)
                {
                    throw new NotImplementedException("Null line finish manager for canvas directed line");
                }

                return _lineFinishManager;
            }

            set
            {
                _lineFinishManager = value;
            }
        }

        public LineDrawoutMode LineDrawoutMode = LineDrawoutMode.ShowNormalDrawout;

        public bool LineDesignStateEditModeSelected { get; set; } = false;
        
        public DesignState OriginatingDesignState { get; set; }

        public string ParentPolygonGuid => ParentPolygon is null ? string.Empty : ParentPolygon.Guid;

        public CanvasDirectedPolygon ParentPolygon { get; set; }

        public string ParentAreaGuid => ParentLayoutArea is null ? string.Empty : ParentLayoutArea.Guid;

        public CanvasLayoutArea ParentLayoutArea => ParentPolygon is null ? null : ParentPolygon.ParentLayoutArea;
        
        public bool IsZeroLine
        {
            get; 
            set;
        } = false;

        /// <summary>
        /// Is complete L line indicates that this line was put on the canvas as a result
        /// of a 'complete L' command. It comes into play when backing out a complete L command
        /// since a sequence of complete L lines should be deleted at once
        /// </summary>
        public bool IsCompleteLLine
        {
            get;
            set;
        } = false;

        /// <summary>
        /// The associated directed line is used to link a line in a canvas layout area to the 'associated' line
        /// mode line. This is used only for the purpose of deleting lines in area mode to know which lines in
        /// line mode should be deleted.
        /// </summary>
        /// 
        public CanvasDirectedLine AssociatedDirectedLine { get; set; } = null;
        public bool RemoveOnReturnToAreaMode { get; set; } = false;
        public bool FirstLineInSequence { get; set; } = false;
        
        public bool IsHorizontalGuideLine { get; set; } = false;

        public bool IsVerticalGuideLine { get; set; } = false;

        internal GraphicShape Draw()
        {
            if (LineFinishManager is null)
            {
                return null;
            }

            return Draw(LineFinishManager.LineColor, LineFinishManager.LineWidthInPts);
        }

        public GraphicShape Draw(Color lineColor, double lineWidthInPts = 3)
        {
           
            if (IsZeroLine)
            {
                // Override in case of zero line. Not best programming practice, but quick kludge...

                lineColor = FinishGlobals.ZeroLineBase.LineColor;
                lineWidthInPts = FinishGlobals.ZeroLineBase.LineWidthInPts;
            }

            GraphicShape shape = base.Draw(lineColor, lineWidthInPts);

            if (IsZeroLine)
            {
                SetBaseLineStyle(FinishGlobals.ZeroLineBase.VisioLineType);
            }


            Page.AddToPageShapeDict((GraphicShape) this);

            Window?.DeselectAll();

            return shape;
        }

        public void SetBaseLineStyle(AreaShapeBuildStatus buildStatus)
        {
            SetBaseLineStyle(LineFinishManager.VisioLineStyleFormula);

            if (buildStatus == AreaShapeBuildStatus.Completed)
            {
                SetBaseLineWidth(CanvasManager.CompletedShapeLineWidthInPts);
            }

            if (this.IsZeroLine)
            {
                SetBaseLineStyle(FinishGlobals.ZeroLineBase.VisioLineType);
            }

        }

        public void SetBaseLineStyle()
        {
            SetBaseLineStyle(LineFinishManager.VisioLineColorFormula);
        }

        internal void SetShapeData()
        {
            string data1 = string.Empty;

            switch (LineRole)
            {
                case LineRole.Unknown:
                    data1 = "Unknown";
                    break;
                case LineRole.SingleLine:
                    data1 = "Single Line";
                    break;
                case LineRole.AssociatedLine:
                    data1 = "Associated Line";
                    break;
                case LineRole.ExternalPerimeter:
                    data1 = "External Perimeter";
                    break;
                case LineRole.InternalPerimeter:
                    data1 = "Internal Perimeter";
                    break;
                case LineRole.Seam:
                    data1 = "Seam";
                    break;
                case LineRole.NullPerimeter:
                    data1 = "Null Perimeter";
                    break;
                default:
                    data1 = "<null>";
                    break;
            }

            string data2 = string.Empty;

            if (LineFinishManager is null)
            {
                data2 = "Line[<null>]";
            }

            else
            {
                data2 = "Line[" + LineFinishManager.LineName + "]";
            }

            string data3 = this.Guid;

            //if (data3.Length > 8)
            //{
            //    data3 = data3.Substring(0, 8);
            //}

            VisioInterop.SetShapeData(this.Shape, data1, data2, data3);
        }

        internal void SetShapeData(string data1, string data2)
        {
            VisioInterop.SetShapeData(this.Shape, data1, data2, this.Guid);
        }

        internal void SetBaseLineOpacity(double opacity)
        {
            if (Shape is null)
            {
                return;
            }

            this.Shape.SetLineOpacity(opacity);
        }

        public void SetLineGraphics(DesignState designState, bool selected, AreaShapeBuildStatus buildStatus)
        {
            // The following is defensive. Should not hit in current desing

            if (LineRole == LineRole.NullPerimeter)
            {
                this.Shape.SetLineOpacity(0);
                return;
            }

            SetBaseLineColor(LineFinishManager.LineColor);

            SetBaseLineStyle(LineFinishManager.VisioLineStyleFormula);

            if (designState == DesignState.Area)
            {
                SetLineGraphicsForAreaDesignState(buildStatus);

                return;
            }

            else if (designState == DesignState.Line)
            {
                SetLineGraphicsForLineDesignState(buildStatus);

                return;
            }

            else if (designState == DesignState.Seam)
            {
                SetLineGraphicsForSeamDesignState(selected, buildStatus);


                return;
            }

            throw new NotImplementedException();
        }

        public void SetLineGraphicsForBorderArea()
        {
            this.Shape.SetLineOpacity(0);
            //SetBaseLineWidth(0);
        }

        private void SetLineGraphicsForAreaDesignState(AreaShapeBuildStatus buildStatus)
        {

            if (buildStatus == AreaShapeBuildStatus.Completed)
            {
                Shape.SetLineStyle(0);

                return;
            }


            SetBaseLineWidth(ucLine.LineWidthInPts);

            //SetBaseLineCompoundType(
            //    this.LineCompoundType == LineCompoundType.Single ? "0" : "1");

            if (this.IsZeroLine)
            {
                SetBaseLineStyle(FinishGlobals.ZeroLineBase.VisioLineType);
            }

            else
            {
                // This is a major kludge due to a recent change in specs. Now, lines on a perimeter in area mode are always
                // thin gray line. So we set it here and set it up so that it will never be set again.

                SetBaseLineStyle(CanvasManager.AreaModePerimeterLineStyleFormula);

                SetBaseLineColor(CanvasManager.AreaModePerimeterLineColor);
            }

            if (buildStatus == AreaShapeBuildStatus.Completed)
            {
                if (this.ParentPolygon is null && this.LineCompoundType == LineCompoundType.Double)
                {
                    // This is a case where a line was created in area mode (currently only a double line) and is a double line

                    // When a double line is displayed in area mode, we need to increase the size significantly in order to
                    // see the line displayed as doubled.

                    SetBaseLineWidth(12 * CanvasManager.CompletedShapeLineWidthInPts);
                }

                else
                {
                    // MDD Reset

                    SetBaseLineWidth(0 /* CanvasManager.CompletedShapeLineWidthInPts */);
                    VisioInterop.SetBaseLineOpacity(this.Shape, 0);
                }
            }

            else
            {
                SetBaseLineWidth(ucLine.LineWidthInPts);
            }

        }

        public bool Intersects(CanvasDirectedPolygon canvasDirectedPolygon)
        {
            foreach (CanvasDirectedLine canvasDirectedLine in canvasDirectedPolygon)
            {
                if (this.Intersects(canvasDirectedLine))
                {
                    return true;
                }
            }

            return false;
        }

        private void SetLineGraphicsForLineDesignState(AreaShapeBuildStatus buildStatus)
        {
            if (OriginatingDesignState == DesignState.Seam)
            {
                SetLineGraphicsForSeamDesignState(false, buildStatus);

                return;
            }

            SetBaseLineCompoundType(
                this.LineCompoundType == LineCompoundType.Single ? "0" : "1");

            if (LineRole == LineRole.ExternalPerimeter)
            {
                SetBaseLineWidth(0.25);

                //SetBaseLineColor(Color.Red);
                SetBaseLineColor(Color.Gray);

                SetBaseLineStyle("1");

                return;
            }

            SetBaseLineWidth(LineFinishManager.LineWidthInPts);

            if (this.IsZeroLine)
            {
                SetBaseLineWidth(0.25);

                SetBaseLineColor(Color.Gray);
                SetBaseLineStyle("9");
            }

            else
            {
                SetBaseLineStyle(LineFinishManager.VisioLineStyleFormula);

                if (LineCompoundType == LineCompoundType.Double)
                {
                    SetBaseLineWidth(2.0 * LineFinishManager.LineWidthInPts);
                }
            }
        }

        private void SetLineGraphicsForSeamDesignState(bool selected, AreaShapeBuildStatus buildStatus)
        {
            if (this.IsZeroLine)
            {
                SetBaseLineStyle(FinishGlobals.ZeroLineBase.VisioLineType);
            }

            else
            {
                // This is a major kludge due to a recent change in specs. Now, lines on a perimeter in area mode are always
                // thin gray line. So we set it here and set it up so that it will never be set again.

                SetBaseLineStyle(CanvasManager.AreaModePerimeterLineStyleFormula);

                SetBaseLineColor(CanvasManager.AreaModePerimeterLineColor);

               
            }

            SetBaseLineWidth(CanvasManager.CompletedShapeLineWidthInPts);

            //if (selected)
            //{
            //    SetBaseLineWidth(3);
            //}

            //else
            //{
            //    SetBaseLineWidth(CanvasManager.CompletedShapeLineWidthInPts);
            //}
        }

        public void ShowLineGraphics()
        {
            if (Shape is null)
            {
                return;
            }

            Shape.BringToFront();

            this.Shape.SetLineStyle(VisioLineStyle.Solid);
         
            //   this.Shape.SetLineOpacity(1);
        }

        public void HideLineGraphics()
        {
            if (Shape is null)
            {
                return;
            }

            this.Shape.SetLineStyle(0);
            //this.Shape.SetLineOpacity(0);

        }

        /// <summary>
        /// Add a slight amount to the start of a line
        /// </summary>
        /// <param name="extensionLength">The length of the extension</param>
        /// <returns></returns>
        internal new CanvasDirectedLine ExtendStart(double extensionLength)
        {
            return new CanvasDirectedLine(this.canvasManager, this.LineFinishManager, base.ExtendStart(extensionLength), SystemState.DesignState);
        }

        internal new  CanvasDirectedLine ExtendEnd(double extensionLength)
        {
            return new CanvasDirectedLine(this.canvasManager, this.LineFinishManager, base.ExtendEnd(extensionLength), SystemState.DesignState);
        }

        public void Draw(DesignState lineAreaMode, bool selected, AreaShapeBuildStatus buildStatus = AreaShapeBuildStatus.Completed)
        {
            base.Draw(ucLine.LineColor, ucLine.LineWidthInPts);

            this.SetLineGraphics(lineAreaMode, selected, buildStatus);
        }


        internal void SetDesignStateSelectedLineGraphics()
        {
            Shape.SetLineColor(Color.Red);
            Shape.SetLineWidth(3.0);
            Shape.SetLineStyle("1");
        }

        internal void RemoveFromSystem()
        {
            LineFinishManager lineFinishManager = this.LineFinishManager;

            if (!(lineFinishManager is null))
            {
                lineFinishManager.RemoveLineFull(this);
            }

            CanvasPage currentPage = this.canvasManager.CurrentPage;

            currentPage.RemoveFromDirectedLineDict(this);
           
        }

        internal void MakeDisappear()
        {
            VisioInterop.SetBaseLineStyle(this.Shape, "0");
        }

        internal void MakeReappear()
        {
            VisioInterop.SetBaseLineStyle(this.Shape, LineFinishManager.VisioLineStyleFormula);
        }

        internal new void Delete()
        {

            canvasManager.CurrentPage.RemoveFromDirectedLineDict(this);

            //Page.RemoveFromPageShapeDict(this);

            if (LineFinishManager != null)
            {
                // The shape is deleted in the call to RemoveLineFull

                this.LineFinishManager.RemoveLineFull(this);
            }

            else
            {
                VisioInterop.DeleteShape(Shape);
            }
        }

        internal void Redraw(DesignState lineArea, bool selected, AreaShapeBuildStatus areaBuildStatus)
        {
            Debug.Assert(LineFinishManager != null);

            Draw(lineArea, selected, areaBuildStatus);
        }

        public new CanvasDirectedLine Clone()
        {
            GraphicsDirectedLine clonedGraphicsDirectedLine = base.Clone();

            CanvasDirectedLine clonedCanvasDirectedLine = new CanvasDirectedLine(
                this.canvasManager,
                this.Window,
                this.Page,
                this.LineFinishManager,
                clonedGraphicsDirectedLine,
                this.OriginatingDesignState,
                this.LineCompoundType == LineCompoundType.Double)
            {
                IsZeroLine = this.IsZeroLine,
                IsCompleteLLine = this.IsCompleteLLine
            };

            clonedCanvasDirectedLine.ucLine = this.ucLine;

            clonedCanvasDirectedLine.LineFinishManager = this.LineFinishManager;

            return clonedCanvasDirectedLine;
        }

        public new CanvasDirectedLine CloneBasic(GraphicsWindow window, GraphicsPage page)
        {
            GraphicsDirectedLine clonedGraphicsDirectedLine = base.CloneBasic(window, page);

            CanvasDirectedLine clonedCanvasDirectedLine = new CanvasDirectedLine(
                this.canvasManager,
                window,
                page,
                this.LineFinishManager,
                clonedGraphicsDirectedLine,
                this.OriginatingDesignState,
                this.LineCompoundType == LineCompoundType.Double)
            {
                IsZeroLine = this.IsZeroLine,
                IsCompleteLLine = this.IsCompleteLLine
            };

            clonedCanvasDirectedLine.ucLine = this.ucLine;
            
            return clonedCanvasDirectedLine;
        }

        internal bool IsValidLineForSeamSelection()
        {
            if (LineRole != LineRole.ExternalPerimeter && LineRole != LineRole.InternalPerimeter)
            {
                return false;
            }

            if (ParentLayoutArea is null)
            {
                return false;
            }

            if (ParentLayoutArea.IsSubdivided())
            {
                return false;
            }

            if ((ParentLayoutArea.LayoutAreaType != LayoutAreaType.Normal) &&
                (ParentLayoutArea.LayoutAreaType != LayoutAreaType.OversGenerator))
            {
                return false;
            }

            if (ParentLayoutArea.AreaFinishManager.Guid != this.canvasManager.AreaFinishPalette.SelectedFinish.Guid)
            {
                return false;
            }

            return true;
        }

        public void SetSelectionMode(bool isSelected)
        {
            canvasManager.CurrentPage.SetLineDesignStateAreaSelectionStatus(this, isSelected);
        }

        public void SetZeroLine(bool newState)
        {
            if (this.IsZeroLine != newState)
            {
                this.IsZeroLine = newState;

                if (this.AssociatedDirectedLine != null)
                {
                    this.AssociatedDirectedLine.IsZeroLine = newState;
                }

                this.LineFinishManager.UpdateFinishStats();
            }
        }


        public static explicit operator GraphicShape(CanvasDirectedLine canvasDirectedLine)
        {
            return canvasDirectedLine.Shape;
        }

        public override string ToString()
        {
            string lineName = LineFinishManager != null ? LineFinishManager.LineName : "";

            return $"Name:{lineName} {LineRole} IsZeroLine:{IsZeroLine} {OriginatingDesignState}";
        }
    }
}
