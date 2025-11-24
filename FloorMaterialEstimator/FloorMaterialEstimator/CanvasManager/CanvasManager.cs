//-------------------------------------------------------------------------------//
// <copyright file="CanvasManager.cs" company="Bruun Estimating, LLC">           // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AxMicrosoft.Office.Interop.VisOcx;

    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Supporting_Forms;
    using FloorMaterialEstimator.ShortcutsAndSettings;
    using FloorMaterialEstimator.Test_and_Debug;
    using FloorMaterialEstimator.Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    /// <summary>
    /// Canvas manager implements all functionality related to the visio graphics engine
    /// </summary>
    public partial class CanvasManager
    {
        private const double clickResolution = 0.05;

        public const double CompletedShapeLineWidthInPts = 0.25;

        public const string ZeroLineStyleFormula = "9";
        /// <summary>
        /// The current visio page. This changes when the user selects a different page
        /// </summary>
        //public Visio.Page CurrentPage { get; set; }

        public Page CurrentPage { get; set; }

        public VisioTestAndDebug Vtb { get; set; }

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public double GridScale { get; set; } = 12.0;

        public double GridOffset
        {
            get
            {
                return GlobalSettings.GridlineOffsetSetting;
            }
        } 

        public DrawingMode DrawingMode
        {
            get
            {
                return baseForm.DrawingMode;
            }

            set
            {
                baseForm.DrawingMode = value;
            }
        }

        private FloorMaterialEstimatorBaseForm baseForm;

        private AxDrawingControl axDrawingControl;

        public UCLinePallet linePallet
        {
            get
            {
                return baseForm.linePallet;
            }
        }

        public UCLine SelectedLineType
        {
            get
            {
                return linePallet.selectedLine;
            }
        }

        public UCFinishPallet finishPallet
        {
            get
            {
                return baseForm.finishPallet;
            }
        }

        public UCFinish selectedFinishType
        {
            get
            {
                return finishPallet.selectedFinish;
            }
        }

        Dictionary<string, UCLine> LineTypeDict
        {
            get
            {
                return this.linePallet.lineTypeDict;
            }
        }
        
        private Dictionary<string, UCFinish> finishTypeDict
        {
            get
            {
                return finishPallet.finishDict;
            }
        }

        //private SortedDictionary<int, AreaShape> areaShapeDict = new SortedDictionary<int, AreaShape>();

        public CanvasManager(FloorMaterialEstimatorBaseForm baseForm, AxDrawingControl axDrawingControl)
        {
           
            this.baseForm = baseForm;
            this.axDrawingControl = axDrawingControl;
 
            //axDrawingControl.Src = @"C:\Minerva Research and Development\Projects\OCERev2\Data\Drawing1.vsdx";

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;


            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Array x;

            this.VsoDocument.GetThemeNames(Visio.VisThemeTypes.visThemeTypeColor, out x);

            Visio.Pages pages = this.VsoDocument.Pages;

            this.CurrentPage = new Page(pages[1]);

            this.CurrentPage.Name = "Drawing-1";

            VsoWindow.ShowGrid = 1;

            this.VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.VsoWindow.MouseUp += VsoWindow_MouseUp;
            this.VsoWindow.MouseMove += VsoWindow_MouseMove;

            this.VsoWindow.KeyPress += VsoWindow_KeyPress;

            CurrentPage.VisioPage.ShapeAdded += CurrentPage_ShapeAdded;
            CurrentPage.VisioPage.BeforeShapeDelete += CurrentPage_BeforeShapeDelete;

            VsoWindow.Page = pages[1];

            this.VsoWindow.ShowRulers = 0;

            this.VsoWindow.SelectionChanged += VsoWindow_SelectionChanged;

            Graphics.SetPageSize(this.CurrentPage, 16, 12);

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Development)
            {
                GridScale = Graphics.SetPageGrid(CurrentPage, GlobalSettings.YGridlineCountSetting, GlobalSettings.GridlineOffsetSetting);
               
                if (GlobalSettings.ShowGridlineNumbersSetting)
                {
                    //AddGridNumbersToPage();
                }
            }

            Visio.Window sizeBoxWindow = VsoWindow.Windows.ItemFromID[(int)Visio.VisWinTypes.visWinIDSizePos];

            if (sizeBoxWindow != null)
            {
                sizeBoxWindow.Visible = false;
            }

            Vtb = new VisioTestAndDebug(this);
        }

        private void VsoWindow_SelectionChanged(Visio.Window Window)
        {
            
        }

        /// <summary>
        /// Load the base drawing.
        /// </summary>
        internal void LoadDrawing()
        {
            DrawingImporter drawingImporter = new DrawingImporter(CurrentPage);

            Visio.Shape drawing = drawingImporter.ImportDrawing();

            if (drawing == null)
            {
                return;
            }

            ShapeSize size = Graphics.GetShapeDimensions(drawing);

            Visio.Layer drawingLayer = CurrentPage.GetDrawingLayer();

            drawingLayer.Add(drawing, 1);

            Graphics.SendToBack(drawing);

            Graphics.LockLayer(drawingLayer);

            Graphics.SetLayerOpacity(drawingLayer, 0.0);

            Graphics.SetPageSize(this.CurrentPage, size);

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Development)
            {
                GridScale = Graphics.SetPageGrid(this.CurrentPage, 24, 0.1);
            }

            Graphics.SetShapeLocation(drawing);

            VsoWindow.ShowPageBreaks = 0;

            VsoWindow.DeselectAll();

            CurrentPage.Name = drawingImporter.drawingName;
        }

        internal void UpdateLineSelections(UCLine ucLine)
        {
            Visio.Selection selection = this.VsoWindow.Selection;

            Array selectedIds;

            selection.GetIDs(out selectedIds);

            foreach (int selectedId in selectedIds)
            {
                if (!this.lineShapeDict.ContainsKey(selectedId))
                {
                    continue;
                }

                this.linePallet.MoveLineToLineType(this.lineShapeDict[selectedId], ucLine);
            }
        }

        internal void SelectLineAreaMode(LineArea lineArea)
        {
            if (lineArea == LineArea.Line)
            {
                foreach (UCFinish ucFinish in this.finishTypeDict.Values)
                {
                    Graphics.SetLayerVisibility(ucFinish.layer, false);
                }

                foreach (UCLine ucLine in this.LineTypeDict.Values)
                {
                    ucLine.lineList.ForEach(l => l.SetLineGraphicsForLineMode());
                }
            }

            else
            {
                foreach (UCFinish ucFinish in this.finishTypeDict.Values)
                {
                    ucFinish.areaShapeList.ForEach(
                        a => a.Perimeter.SetLineGraphicsForAreaMode(a.BuildStatus));
                        
                    if (ucFinish.bIsFiltered)
                    {
                        Graphics.SetLayerVisibility(ucFinish.layer, false);
                    }

                    else
                    {
                        Graphics.SetLayerVisibility(ucFinish.layer, true);
                    }
                }
            }
        }

        internal void ShowDrawing(bool bShowDrawing)
        {
            Visio.Layer layer = CurrentPage.GetDrawingLayerIfExists();

            if (layer == null)
            {
                return;
            }

            Graphics.SetLayerVisibility(layer, bShowDrawing);
        }

        private void CurrentPage_BeforeShapeDelete(Visio.Shape visioShape)
        {
            DeleteFromVisioShapDict(visioShape);
        }

        //private void VsoWindow_WindowTurnedToPage(Visio.Window Window)
        //{
        //    Visio.Page page = Window.Page;

        //    CurrentPage = page;
        //}

        public bool DrawingShape { get; set; } = false;

        public Dictionary<string, Shape> ShapeDict
        {
            get
            {
                return CurrentPage.ShapeDict;
            }
        }

        Dictionary<int, GraphicsLine> lineShapeDict = new Dictionary<int, GraphicsLine>();
  
        List<Coordinate> coordinateList = new List<Coordinate>();

        GraphicsLine buildingLine;
        ScaleLine scaleLine;

        Rectangle buildingRectangle;

        /// <summary>
        /// Shape added event manager. The code is designed so that the logic should ONLY be 
        /// executed if the event is triggered as a result of a cut and paste operation. The event
        /// </summary>
        /// <param name="visioShape">The visio shape that was added</param>
        private void CurrentPage_ShapeAdded(Visio.Shape visioShape)
        {
            if (this.ShapeDict.ContainsKey(visioShape.NameID))
            {
                // Already added as a result of explicit mouse / create actions. We want
                // to ignore the event in this case.
                return;
            }

           // vtb.ViewShapeCharacteristics(visioShape);
            
            // The new shape was created as an impact of a copy/paste operation. So everything below
            // is designed to construct a new shape of the same shape type and add it to the system.
            if (visioShape.Data2 == "Line")
            {
                GraphicsLine line = new GraphicsLine(visioShape);

                if (!string.IsNullOrEmpty(visioShape.Data1))
                {
                    // Data1 is where the finish or line type info is stored in the base visio object.
                    // It is used here to add the newly created shape object into the proper finish or line type.
                    string lineName = visioShape.Data1;

                    this.LineTypeDict[lineName].AddLine(line);
                }
                else
                {
                    // The default is to now add the object to the current (selected) line type.
                    this.SelectedLineType.AddLine(line);
                }

                this.ShapeDict.Add(visioShape.NameID, new Shape(visioShape, ShapeType.Line));

                return;
            }

            if (visioShape.Data2 == "Rectangle")
            {
                Rectangle rectangle = new Rectangle(this, visioShape);

                if (!string.IsNullOrEmpty(visioShape.Data1))
                {
                    // Data1 is where the finish or line type info is stored in the base visio object.
                    // It is used here to add the newly created shape object into the proper finish or line type.
                    string finishName = visioShape.Data1;

                    if (!this.finishTypeDict.ContainsKey(finishName))
                    {
                        throw new NotImplementedException();
                    }

                    UCFinish finishType = this.finishTypeDict[finishName];

                    finishType.AddShape(rectangle);

                    finishType.UpdateFinishStats();
                }              
                else
                {
                    // The default is to now add the object to the current (selected) finish type.
                    this.selectedFinishType.AddShape(rectangle);

                    this.selectedFinishType.UpdateFinishStats();
                }

                this.ShapeDict.Add(visioShape.NameID, new Shape(visioShape, ShapeType.Rectangle));

                return;
            }

            if (visioShape.Data2 == "Polyline")
            {
                Polyline polyline = new Polyline(this, visioShape);

                if (!string.IsNullOrEmpty(visioShape.Data1))
                {
                    // Data1 is where the finish or line type info is stored in the base visio object.
                    // It is used here to add the newly created shape object into the proper finish or line type.
                    string finishName = visioShape.Data1;

                    if (!this.finishTypeDict.ContainsKey(finishName))
                    {
                        throw new NotImplementedException();
                    }

                    UCFinish finishType = this.finishTypeDict[finishName];

                    finishType.AddShape(polyline);

                    finishType.UpdateFinishStats();
                }
                else
                {
                    // The default is to now add the object to the current (selected) finish type.
                    this.selectedFinishType.AddShape(polyline);

                    this.selectedFinishType.UpdateFinishStats();
                }

                this.ShapeDict.Add(visioShape.NameID, new Shape(visioShape, ShapeType.Polyline));

                return;
            }
        }


        /// <summary>
        /// Attempts to delete the visio shape of a line from the local visio shape dictionary
        /// </summary>
        /// <param name="line">The line with the visio shape to be deleted</param>
        internal void DeleteFromVisioShapDict(GraphicsLine line)
        {
            if (line == null)
            {
                return;
            }

            if (line.VisioShape == null)
            {
                return;
            }

            if (!this.ShapeDict.ContainsKey(line.NameID))
            {
                this.ShapeDict.Remove(line.NameID);
            }
        }

        /// <summary>
        /// Attempts to delete a visio shape from the local visio shape dictionary
        /// </summary>
        /// <param name="visioShape">The visio shape to be deleted</param>
        private void DeleteFromVisioShapDict(Visio.Shape visioShape)
        {
            if (visioShape == null)
            {
                return;
            }

            if (!this.ShapeDict.ContainsKey(visioShape.NameID))
            {
                return;
            }

            this.ShapeDict.Remove(visioShape.NameID);
        }

    }
}
