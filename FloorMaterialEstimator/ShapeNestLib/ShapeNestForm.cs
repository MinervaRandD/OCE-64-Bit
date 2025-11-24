using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using Globals;
using Geometry;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using VoronoiLib;
using SettingsLib;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using DeepNestLib;

namespace ShapeNestLib
{
    public partial class ShapeNestForm : Form
    {
        private List<ShapeNestShape> shapeNestShapes;

        private List<ShapeNestShapePanel> shapeNestShapePanels = new List<ShapeNestShapePanel>();

        private object[] rotationsArray = new object[]
        {
             0, 180, 120, 90, 72, 60, 45, 40, 36, 30, 24, 20, 18, 15, 12, 10, 9, 8, 6, 5, 4, 3, 2, 1
        };

        public ShapeNestForm(double initialRollWidthInInches, List<ShapeNestShape> shapeNestShapes)
        {
            InitializeComponent();

            this.shapeNestShapes = shapeNestShapes;

            setupForm();

            formatForm();

            this.txbRollWidth.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(initialRollWidthInInches);

            double initialRollLength = getInitialRollWidth();

            this.txbMaxRollLength.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(initialRollLength * 1.2);

            this.txbMaterialSpacingInInches.Text = "0";

            this.txbMaterialMarginInches.Text = "0";

            this.pcbNesting.Paint += PcbNesting_Paint;

            this.SizeChanged += ShapeNestForm_SizeChanged;

            this.cmbRotations.Items.AddRange(rotationsArray);

            this.cmbRotations.SelectedIndex = 0;

            cmbRotations.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cmbRotations.SelectedIndexChanged += CmbRotations_SelectedIndexChanged;

            this.txbMaterialMarginInches.TextChanged += TxbMaterialMarginInches_TextChanged;

            this.txbMaterialSpacingInInches.TextChanged += TxbMaterialSpacingInInches_TextChanged;

            this.txbMaxRollLength.TextChanged += TxbMaxRollLength_TextChanged;

            this.txbRollWidth.TextChanged += TxbRollWidth_TextChanged;
        }


        private double getInitialRollWidth()
        {
            double initialRollWidth = shapeNestShapes.Select(s=>Math.Max(s.Width, s.Height)).Sum();

            return initialRollWidth;
        }

        private void CmbRotations_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { this.cmbRotations.Select(0, 0); }));
        }

        private void ShapeNestForm_SizeChanged(object sender, EventArgs e)
        {
            formatForm();
        }

        private void formatForm()
        {
            int sizeX = this.Width;
            int sizeY = this.Height;

            int pnlShapesPosnX = (sizeX - this.pnlShapes.Width) / 2;

            if (pnlShapesPosnX < 4)
            {
                pnlShapesPosnX = 4;
            }

            pnlShapes.Location = new Point(pnlShapesPosnX, 16);

            int pnlShapeControlsPosnX = pnlShapesPosnX + (this.pnlShapes.Width - this.pnlShapePaletteControls.Width) / 2;

            int pnlShapeControlsPosnY = this.pnlShapes.Location.Y + this.pnlShapes.Height + 10;

            pnlShapePaletteControls.Location = new Point(pnlShapeControlsPosnX, pnlShapeControlsPosnY);

            int pnlNestPanelPosnX =(sizeX - this.pnlNestPanel.Width) / 2;

            if (pnlNestPanelPosnX < 4)
            {
                pnlNestPanelPosnX = 4;
            }

            int nestPanelPosnY = this.pnlShapePaletteControls.Location.Y + this.pnlShapePaletteControls.Height + 32;

            pnlNestPanel.Location = new Point(pnlNestPanelPosnX, nestPanelPosnY);
        }

        private void setupForm()
        {
          
            shapeNestShapePanels.ForEach(p=>this.pnlShapes.Controls.Remove(p));


            int width = this.Width;

            int shapeRows = (int) Math.Ceiling((double) this.shapeNestShapes.Count / 10.0);

            this.pnlShapes.Height = 100 * shapeRows + 1;

            this.pnlShapes.Width = 1001;

            //this.pnlShapes.Location = new Point(20, 20);

            for (int i = 0; i < shapeNestShapes.Count; i++)
            {
                int row = i / 10;
                int col = i % 10;

                var shapeNestShape = shapeNestShapes[i];

                Color lineColor = shapeNestShape.lineColor;
                Color fillColor = shapeNestShape.fillColor;


                ShapeNestShapePanel panel = new ShapeNestShapePanel(shapeNestShapes[i], lineColor, fillColor);

                panel.Size = new System.Drawing.Size(100, 100);

                panel.BorderStyle = BorderStyle.FixedSingle;

                this.pnlShapes.Controls.Add(panel);

                int drawPanelY = row  * 100;
                int drawPanelX = col * 100;

                panel.Location = new System.Drawing.Point(drawPanelX, drawPanelY);

                panel.Invalidate();

                shapeNestShapePanels.Add(panel);
            }

            //int shapePaletteControlsX = this.pnlShapePaletteControls.Location.X;

            //int shapePaletteControlsY = this.pnlShapes.Location.Y + this.pnlShapes.Height + 10;

            //this.pnlShapePaletteControls.Location = new System.Drawing.Point(shapePaletteControlsX, shapePaletteControlsY);

            //int nestPanelX = 4;
            //int nestPanelY = this.pnlShapePaletteControls.Location.Y + this.pnlShapePaletteControls.Height + 32;

            //this.pnlNestPanel.Location = new System.Drawing.Point(nestPanelX, nestPanelY);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            shapeNestShapePanels.ForEach(panel => panel.SelectShape());
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            shapeNestShapePanels.ForEach(panel => panel.DeselectShape());
        }

        private void btnSelectOriginallySelected_Click(object sender, EventArgs e)
        {
            shapeNestShapePanels.ForEach(panel => panel.SelectIfNumbered());
        }

        
        private void btnDoNest_Click(object sender, EventArgs e)
        {
            doFullNest1();
        }

#if false
        private void doFullNest()
        {
            int? rollWidthFeet = 0;
            int? rollWidthInch = 0;

            int? maxRollLgthFeet = 0;
            int? maxRollLgthInch = 0;

            minRollArea = 0;
            minAreaOfPlacedItems = 0;
            maxRollLgthInInches = 0;
            maxPctOfRollAreaUsed = -1;

            this.lblUsedRollLgthValue.Text = string.Empty;
            this.lblUsedRollAreaValue.Text = string.Empty;
            this.lblNumberOfItemsPlacedValue.Text = string.Empty;
            this.lblTotalAreaOfPlacedItemsValue.Text = string.Empty;
            this.lblPctOfRollUsedValue.Text = string.Empty;

            this.lblUsedRollLgthValue.Refresh();
            this.lblUsedRollAreaValue.Refresh();
            this.lblNumberOfItemsPlacedValue.Refresh();
            this.lblTotalAreaOfPlacedItemsValue.Refresh();
            this.lblPctOfRollUsedValue.Refresh();


            if (!shapeNestShapePanels.Any(p=>p.Selected))
            {
                MessageBox.Show("No shapes selected to nest");
                return;
            }

            if (!Utilities.Utilities.CheckTextBoxValidMeasurementFeetAndOrInches(this.txbRollWidth, out rollWidthFeet, out rollWidthInch))
            {
                MessageBox.Show("Invalid roll width specified.");
                return;
            }


            if (!Utilities.Utilities.CheckTextBoxValidMeasurementFeetAndOrInches(this.txbMaxRollLength, out maxRollLgthFeet, out maxRollLgthInch))
            {
                MessageBox.Show("Invalid max roll length specified.");
                return;
            }

            double? result = null;

            if (!Utilities.Utilities.CheckTextBoxValidPositiveDouble(this.txbMaterialSpacingInInches, out result))
            {
                MessageBox.Show("Invalid material spacing.");
                return;
            }


            rollWdthInInches = rollWidthFeet.Value * 12 + rollWidthInch.Value;

            maxRollLgthInInches = maxRollLgthFeet.Value * 12 + maxRollLgthInch.Value;

            materialSpacingInInches = result.Value;

            nmbrIterations = (int)this.nudIterations.Value;

            int rotationsInDegrees = (int)rotationsArray[this.cmbRotations.SelectedIndex];

            if (rotationsInDegrees == 0)
            {
                rotationsInDegrees = 360;
            }

            rotations = 360 / rotationsInDegrees;

            nmbrRefreshes = (int) this.nudNmbrOfRefreshes.Value;

            for (refreshCount = 0; refreshCount < nmbrRefreshes; refreshCount++)
            {
                double pctOfRollAreaUsed = runRefresh(rotations);

                
            }

            drawNestingResults();

        }

        private double runRefresh(int rotations)
        {
            
            deepNestRunner = new DeepNestRunner();

            deepNestRunner.IterationComplete += DeepNestRunner_IterationComplete;
            var sheet = new NFP();

            int maxWidth = (int)Math.Ceiling(shapeNestShapes.Select(s => s.Width).Sum());

            sheet.AddPoint(new SvgPoint(0, 0));
            sheet.AddPoint(new SvgPoint(maxRollLgthInInches, 0));
            sheet.AddPoint(new SvgPoint(maxRollLgthInInches, rollWdthInInches));
            sheet.AddPoint(new SvgPoint(0, rollWdthInInches));
            sheet.AddPoint(new SvgPoint(0, 0));

            deepNestRunner.Context.Sheets.Add(sheet);

            List<NFP> polygonList = createPolygons();

            deepNestRunner.Context.LoadInputData(polygonList, 1);

            deepNestRunner.Context.MaterialSpacing = (int)Math.Round(materialSpacingInInches);
            deepNestRunner.Context.Rotation = rotations;
            deepNestRunner.Run(nmbrIterations);

            if (!validatePolygonPlacement(deepNestRunner.Context))
            {
                return -1;
            }

            normalizePolygons(deepNestRunner.Context);

            double lclMaxRollWidth = getMaxRollLgth(deepNestRunner.Context);
            double lclMinRollArea = rollWdthInInches * lclMaxRollWidth;
            double lclMinAreaOfPlacedItems = deepNestRunner.Context.Polygons.Where(p => Math.Round(p.x, 8) >= 0).Select(p => p.Area).Sum();

            double pctOfRollAreaUsed = lclMinAreaOfPlacedItems / lclMinRollArea;

            if (pctOfRollAreaUsed > this.maxPctOfRollAreaUsed)
            {
                finalPolygons = new List<NFP>();

                foreach (var polygon in deepNestRunner.Context.Polygons)
                {
                    if (Math.Round(polygon.x, 8) >= 0)
                    {
                        finalPolygons.Add(polygon);
                    }
                }

                maxPctOfRollAreaUsed = pctOfRollAreaUsed;
                minRollArea = lclMinRollArea;
                minAreaOfPlacedItems = lclMinAreaOfPlacedItems;
                maxRollLgthInInches = lclMaxRollWidth;

                double testMaxRollWidth = testMaxX();
            }

            return pctOfRollAreaUsed;

        }
#endif
        double rollWdthInInches;
        double effectiveRollWdthInInches => rollWdthInInches - 2 * materialMarginInches;

        double materialSpacingInInches;
        double materialMarginInches;

        double minRollArea = 0;
        double minAreaOfPlacedItems = 0;
        double maxRollLgthInInches = 0;
        
        // These items are the backup of the best results

        DeepNestLib.NestingContext maxContext = null;
        double maxUsedRollLgthInInches = 0;
        double effectiveMaxUsedRollLgthInInches => maxUsedRollLgthInInches + 2.0 *materialMarginInches;

        double maxPctOfRollAreaUsed = -1;


        int nmbrIterations = 0;
        int refresh = 0;
        int nmbrRefreshes = 0;

        DeepNestRunner deepNestRunner = null;

        int rotations = 0;

        List<NFP> nestPolygonList = null;

        private double maxWdthDelta = 0;
        private double maxLgthDelta = 0;

        private double maxPossibleWdthWithParts = 0;
        private double maxPossibleLgthWithParts = 0;

        private double areaOfUsedParts = 0;

        private void doFullNest1()
        {
            int? rollWidthFeet = 0;
            int? rollWidthInch = 0;

            int? maxRollLgthFeet = 0;
            int? maxRollLgthInch = 0;

            minRollArea = 0;
            minAreaOfPlacedItems = 0;
            maxRollLgthInInches = 0;

            maxPctOfRollAreaUsed = double.MinValue;


            this.lblUsedRollLgthValue.Text = string.Empty;
            this.lblUsedRollAreaValue.Text = string.Empty;
            this.lblNumberOfItemsPlacedValue.Text = string.Empty;
            this.lblTotalAreaOfPlacedItemsValue.Text = string.Empty;
            this.lblPctOfRollUsedValue.Text = string.Empty;

            this.lblUsedRollLgthValue.Refresh();
            this.lblUsedRollAreaValue.Refresh();
            this.lblNumberOfItemsPlacedValue.Refresh();
            this.lblTotalAreaOfPlacedItemsValue.Refresh();
            this.lblPctOfRollUsedValue.Refresh();


            if (!shapeNestShapePanels.Any(p => p.Selected))
            {
                MessageBox.Show("No shapes selected to nest");
                return;
            }

            if (!Utilities.Utilities.CheckTextBoxValidMeasurementFeetAndOrInches(this.txbRollWidth, out rollWidthFeet, out rollWidthInch))
            {
                MessageBox.Show("Invalid roll width specified.");
                return;
            }


            if (!Utilities.Utilities.CheckTextBoxValidMeasurementFeetAndOrInches(this.txbMaxRollLength, out maxRollLgthFeet, out maxRollLgthInch))
            {
                MessageBox.Show("Invalid max roll length specified.");
                return;
            }

            double? result = null;

            if (!Utilities.Utilities.CheckTextBoxValidPositiveDouble(this.txbMaterialSpacingInInches, out result))
            {
                MessageBox.Show("Invalid material spacing.");
                return;
            }


            materialSpacingInInches = result.Value;


            if (!Utilities.Utilities.CheckTextBoxValidPositiveDouble(this.txbMaterialMarginInches, out result))
            {
                MessageBox.Show("Invalid material margin.");
                return;
            }

            materialMarginInches = result.Value;

            rollWdthInInches = rollWidthFeet.Value * 12 + rollWidthInch.Value;

            maxRollLgthInInches = maxRollLgthFeet.Value * 12 + maxRollLgthInch.Value;

            nmbrIterations = (int)this.nudIterations.Value;

            int rotationsInDegrees = (int)rotationsArray[this.cmbRotations.SelectedIndex];

            if (rotationsInDegrees == 0)
            {
                rotationsInDegrees = 360;
            }

            rotations = 360 / rotationsInDegrees;

            shapeNestShapes.ForEach(s=>s.rotations = rotations);

            nmbrRefreshes = (int)this.nudNmbrOfRefreshes.Value;

            
            nestPolygonList = createPolygons();

            areaOfUsedParts = nestPolygonList.Select(p => p.Area).Sum();

            if (ckbUseFullRollWidth.Checked)
            {
                if (!doFullRollWidthFullNest())
                {
                    return;
                }
            }

            else
            {
                doNativeFullNest();
            }

            setProgressTo100();

            if (maxContext == null)
            {
                MessageBox.Show("A solution was not found, most likely due to the roll length requirement resulting in unplaced shapes.");
                clearAll();

                return;
            }

            normalizePolygons(maxContext);


            drawNestingResults();

        }

        private bool canAchieveFullWidth = true;

        private bool doFullRollWidthFullNest()
        {
            maxPossibleWdthWithParts = shapeNestShapes.Where(s => s.Selected).Select(s => s.VerticalSpan).Sum();
            maxPossibleLgthWithParts = shapeNestShapes.Where(s => s.Selected).Select(s => s.HorizontalSpan).Sum();
            
            maxWdthDelta = shapeNestShapes.Where(s => s.Selected).Select(s => s.VerticalSpan).Max();
            maxLgthDelta = shapeNestShapes.Where(s => s.Selected).Select(s => s.HorizontalSpan).Max();

            if (maxPossibleWdthWithParts + maxWdthDelta / 2.0 < effectiveRollWdthInInches)
            {
                DialogResult dr = Utilities.MessageBoxAdv.Show(
                    "Note: Unable to use full roll width with this set of shapes."
                    ,"Unable To Fill Full Roll Width"
                    , MessageBoxAdv.Buttons.OKCancel
                    , MessageBoxAdv.Icon.Exclamation);
              
                if (dr == DialogResult.Cancel)
                {
                    return false;
                }
               // MessageBox.Show("Note: Unable to use full roll width with this set of shapes.");

                canAchieveFullWidth = false;
            }

            for (refresh = 0; refresh < nmbrRefreshes; refresh++)
            {
                runRefreshFullWidth();
            }

            return true;

        }

        double  test = 0;
        const double nmbrTests = 10.0;

        private void runRefreshFullWidth()
        {
            if (!canAchieveFullWidth)
            {
                if (maxContext != null)
                {
                    maxRollLgthInInches = getMaxPartX();
                }
            }

            for (test = 0; test < nmbrTests; test++)
            {
                doNesting(maxRollLgthInInches);

                if (unplacedParts())
                {
                    maxRollLgthInInches += maxLgthDelta * 0.2;
                }

                else
                {
                    double maxY = getMaxPartY();
                    double maxX = getMaxPartX();

                    double limitIncr = maxWdthDelta * (0.25 + 0.5 * (test + 1.0) / nmbrTests);

                    if (canAchieveFullWidth && ((maxY + limitIncr) < effectiveRollWdthInInches))
                    {

                        maxRollLgthInInches = maxX * maxY / effectiveRollWdthInInches;
                    }

                    else
                    {
                        double pctOfRollAreaUsed = areaOfUsedParts / (maxX * rollWdthInInches);

                        if (pctOfRollAreaUsed > maxPctOfRollAreaUsed)
                        {
                            maxPctOfRollAreaUsed = pctOfRollAreaUsed;
                            maxContext = deepNestRunner.Context;
                            maxUsedRollLgthInInches = maxX;
                        }

                        break;

                    }
                
                }
            }
        }


        private void doNativeFullNest()
        {

            for (refresh = 0; refresh < nmbrRefreshes; refresh++)
            {
                runRefreshNative();
            }

        }

        private void runRefreshNative()
        {
            doNesting(maxRollLgthInInches);

            if (unplacedParts())
            {
                return;
            }

            double maxY = getMaxPartY();
            double maxX = getMaxPartX();

            double pctOfRollAreaUsed = areaOfUsedParts / (maxX * rollWdthInInches);

            if (pctOfRollAreaUsed > maxPctOfRollAreaUsed)
            {
                maxPctOfRollAreaUsed = pctOfRollAreaUsed;
                maxContext = deepNestRunner.Context;
                maxUsedRollLgthInInches = maxX;
            }
        

        }

        private double getMaxPartX()
        {
            double maxPartX = double.MinValue;

            foreach (var polygon in deepNestRunner.Context.Polygons)
            {
                double theta = polygon.rotation * Math.PI / 180.0;

                double cosTheta = Math.Cos(theta);
                double sinTheta = Math.Sin(theta);

                foreach (var point in polygon.Points)
                {
                    double x = polygon.x + point.x * cosTheta - point.y * sinTheta;

                    if (x > maxPartX)
                    {
                        maxPartX = x;
                    }
                }
            }

            return maxPartX;
        }


        private double getMaxPartY()
        {
            double maxPartY = double.MinValue;

            foreach (var polygon in deepNestRunner.Context.Polygons)
            {
                double theta = polygon.rotation * Math.PI / 180.0;

                double cosTheta = Math.Cos(theta);
                double sinTheta = Math.Sin(theta);

                foreach (var point in polygon.Points)
                {
                    double y = polygon.y + point.x * sinTheta + point.y * cosTheta;

                    if (y > maxPartY)
                    {
                        maxPartY = y;
                    }
                }
            }

            return maxPartY;
        }

        private bool unplacedParts()
        {
            NestingContext context = deepNestRunner.Context;

            int totalPolygonCount = context.Polygons.Count;
            int unplacedPolygonCount = 0;

            foreach (var polygon in context.Polygons)
            {
                if (Math.Round(polygon.x) < 0)
                {
                    unplacedPolygonCount++;
                }
            }

            if (unplacedPolygonCount > 0)
            {
                return true;
            }

            return false;
        }

        private void doNesting(double nestMaxRollLgthInInches)
        {
            deepNestRunner = new DeepNestRunner();

            deepNestRunner.IterationComplete += DeepNestRunner_IterationComplete;
            var sheet = new NFP();

            sheet.AddPoint(new SvgPoint(0, 0));
            sheet.AddPoint(new SvgPoint(nestMaxRollLgthInInches, 0));
            sheet.AddPoint(new SvgPoint(nestMaxRollLgthInInches, effectiveRollWdthInInches));
            sheet.AddPoint(new SvgPoint(0, effectiveRollWdthInInches));
            sheet.AddPoint(new SvgPoint(0, 0));

            deepNestRunner.Context.Sheets.Add(sheet);

            List<NFP> polygonList = createPolygons();

            deepNestRunner.Context.LoadInputData(polygonList, 1);

            deepNestRunner.Context.MaterialSpacing = (int)Math.Round(materialSpacingInInches);
            deepNestRunner.Context.Rotation = rotations;
            deepNestRunner.Run(nmbrIterations);

        }

        private void DeepNestRunner_IterationComplete(int iteration)
        {
           
            if (!this.ckbUseFullRollWidth.Checked)
            {
                doIterationCompleteBase(iteration);
            }

            else
            {
                doIterationCompleteFullWidth(iteration);
            }
            
        }

        private void doIterationCompleteBase(int iteration)
        {
            double num = refresh * nmbrIterations + iteration;
            double den = nmbrRefreshes * nmbrIterations;

            double progress = 100.0 * num / den;

            this.prbProgress.Value = (int)Math.Min(Math.Round(progress), 100);

            return;
        }

        private void doIterationCompleteFullWidth(int iteration)
        {
            double num = refresh * nmbrTests * nmbrIterations + test * nmbrIterations + iteration;
            double den = nmbrRefreshes * nmbrIterations * nmbrTests;

            double progress = 100.0 * num / den;
            int nextProgress = (int)Math.Min(Math.Round(progress), 100);
            int currProgress = (int)this.prbProgress.Value;

            for (int i = currProgress; i < nextProgress; i++)
            {
                this.prbProgress.Value = i;
                System.Threading.Thread.Sleep(10);
            }

            this.prbProgress.Value = nextProgress;

            return;
        }

        private void setProgressTo100()
        {
            int currProgress = (int)(this.prbProgress.Value);

            for (int i = currProgress;i < 100; i++)
            {
                this.prbProgress.Value = i;
                System.Threading.Thread.Sleep(10);
            }

            this.prbProgress.Value = 100;
        }

        private bool validatePolygonPlacement(NestingContext context)
        {
            int totalPolygonCount = context.Polygons.Count;
            int unplacedPolygonCount = 0;

            foreach (var polygon in context.Polygons)
            {
                if (Math.Round(polygon.x) < 0)
                {
                    unplacedPolygonCount++;
                }
            }

            if (unplacedPolygonCount == totalPolygonCount)
            {
                MessageBox.Show("No unplaced polygons. A polygon is unplaced when it cannot be fit within the role width.");
                return false;
            }

            if (unplacedPolygonCount > 0)
            {
                MessageBox.Show("Some polygons could not be placed. A polygon is unplaced when it cannot be fit within the role width.");
                return true;
            }

            return true;
        }

        private void normalizePolygons(NestingContext context)
        {
#if true
            foreach (var polygon in context.Polygons)
            {
                if (polygon.x < 0)
                {
                    continue;
                }

                if (polygon.rotation == 0)
                {
                    foreach (var point in polygon.Points)
                    {
                        point.x = Math.Round(point.x, 8) + materialMarginInches;
                        point.y = Math.Round(point.y, 8) + materialMarginInches;
                    }

                    continue;
                }

               
                double theta = polygon.rotation * Math.PI / 180.0;

                double cosTheta = Math.Cos(theta);
                double sinTheta = Math.Sin(theta);

                foreach (var point in polygon.Points)
                {

                    double x = point.x * cosTheta - point.y * sinTheta;
                    double y = point.x * sinTheta + point.y * cosTheta;

                    if (Math.Abs(x) < 1.0e-14)
                    {
                        x = 0.0;
                    }

                    if (Math.Abs(y) < 1.0e-14)
                    {
                        y = 0.0;
                    }

                    point.x = Math.Round(x,8) + materialMarginInches;
                    point.y = Math.Round(y,8) + materialMarginInches;
                }
            }
#endif
        }


        private List<NFP> createPolygons()
        {
            List<NFP> rtrnList = new List<NFP>();

            double totalWidth = 0.0;

            for (int i = 0; i < shapeNestShapes.Count; i++)
            {
                ShapeNestShape shapeNestShape = shapeNestShapes[i];


                if (!shapeNestShape.Selected)
                {
                    continue;
                }

                float minX = shapeNestShape.MinX;
                float minY = shapeNestShape.MinY;

                List<Point> translatedPoints = shapeNestShape.points.Select(p=>new Point((int) Math.Round(p.X - minX), (int) Math.Round(p.Y - minY))).ToList();

                int count = translatedPoints.Count;

                SvgPoint[] svgPoints = new SvgPoint[count];

                for (int j = 0; j < count; j++)
                {
                    svgPoints[j] = new SvgPoint(translatedPoints[j].X, translatedPoints[j].Y);
                }

                NFP polygon = new NFP();

                polygon.Points = svgPoints;
                polygon.id = shapeNestShape.indxNmbr; // Setting id is basically useless because it gets overwritten by deep nest algo when it runs

                polygon.Name = shapeNestShape.indxNmbr.ToString();

                polygon.FillColor = shapeNestShape.fillColor;
                polygon.LineColor = shapeNestShape.lineColor;

                rtrnList.Add(polygon);

                //TestDebug.DumpPolygonInfo(polygon, "C:\\Temp\\Polygon1.txt");

            }

            return rtrnList;
        }


        double scale = 0;
        double offsetX = 0;
        double offsetY = 0;

        List<NFP> finalPolygons = null;

        bool clearGraphics = false;

        private void clearAll()
        {
            this.lblUsedRollLgthValue.Text = string.Empty;
            this.lblUsedRollAreaValue.Text = string.Empty;
            this.lblNumberOfItemsPlacedValue.Text = string.Empty;
            this.lblTotalAreaOfPlacedItemsValue.Text = string.Empty;
            this.lblPctOfRollUsedValue.Text = string.Empty;

            clearGraphics = true;

            this.pcbNesting.Invalidate();

            clearGraphics = false;
        }

        private void drawNestingResults()
        {
            double scaleX = 800.0 / effectiveMaxUsedRollLgthInInches;
            double scaleY = 320.0 / (double)rollWdthInInches;

            scale = Math.Min(scaleX, scaleY);

            ignorePaint = false;

            this.pcbNesting.Invalidate();

            this.lblUsedRollLgthValue.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(effectiveMaxUsedRollLgthInInches, false);
            this.lblUsedRollAreaValue.Text = Math.Round(effectiveMaxUsedRollLgthInInches * rollWdthInInches / 144.0, 2) + "  sq. ft.";// Utilities.FormatUtils.FormatSqrInchesToSqrFeetAndInches(rollArea);
            this.lblNumberOfItemsPlacedValue.Text = maxContext.Polygons.Count.ToString();
            this.lblTotalAreaOfPlacedItemsValue.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(areaOfUsedParts, false);
            this.lblPctOfRollUsedValue.Text = ((areaOfUsedParts / (effectiveMaxUsedRollLgthInInches * rollWdthInInches)) * 100.0).ToString("0.0") + "%";
        }

        bool ignorePaint = true;

        Pen rollPen = new Pen(Color.Red, 2);
        Pen innerRollPen = new Pen(Color.Red, 1);

        Pen arrowEndPen = new Pen(Color.Red, 2);
        Pen arrowPen = new Pen(Color.Red, 2);

        Brush undrBrush = new SolidBrush(Color.Black);
        Brush dimBrush = new SolidBrush(Color.Red);
        Font dimFont = new Font("Arial", 10);
        Brush dimFillBrush = new SolidBrush(Color.White);
        Pen linePen = new Pen(Color.LightGray, 2);

        private void PcbNesting_Paint(object sender, PaintEventArgs e)
        {
            //bool drawHorizontalDimAboveArrow = false;

            if (ignorePaint)
            {
                return;
            }

            System.Drawing.Graphics graphics = e.Graphics;

            if (clearGraphics)
            {
                graphics.Clear(Color.White);

                return;

            }

            graphics.Clear(Color.White);

            int pcbWidth = this.pcbNesting.Width;
            int pcbHeight = this.pcbNesting.Height;

            float rectWidth = (float)(scale * effectiveMaxUsedRollLgthInInches);
            float rectHeight = (float)(scale * (double)rollWdthInInches);

            string horzDim = Utilities.FormatUtils.FormatInchesToFeetAndInches(effectiveMaxUsedRollLgthInInches, false);
            SizeF horzDimSizeF = graphics.MeasureString(horzDim, dimFont);

            offsetX = (pcbWidth - scale * maxUsedRollLgthInInches) * 0.5 + 10;
            offsetY = (pcbHeight - scale * (double)rollWdthInInches) * 0.5 + 20;
          
            if (offsetX < 12)
            {
                offsetX = 12;
            }

            if (offsetY < 12)
            {
                offsetY = 12;
            }

            float rectX = (float)offsetX;
            float rectY = (float)offsetY;
           
            
            foreach (var polygon in maxContext.Polygons)
            {
                if (polygon.x < 0)
                {
                    continue;
                }

                int npnts = polygon.Points.Length;

                bool addPnt = false;

                if (polygon.Points[npnts - 1] != polygon.Points[0])
                {
                    npnts = npnts + 1;

                    addPnt = true;
                }

                PointF[] points = new PointF[npnts];

                for (int i = 0; i < polygon.Points.Length; i++)
                {
                    double x = offsetX + (polygon.x + polygon.Points[i].x) * scale;
                    double y = offsetY + (polygon.y + polygon.Points[i].y) * scale - 20;

                    y = 400.0 - y + 20;

                    points[i] = new PointF((float)x, (float)y);
                }

                if (addPnt)
                {
                    points[npnts - 1] = points[0];
                }

                Brush fillBrush = new SolidBrush(polygon.FillColor);
               
                graphics.DrawPolygon(linePen, points);
                graphics.FillPolygon(fillBrush, points);

                List<DirectedLine> directedLineList = new List<DirectedLine>();

                for (int i = 0; i < points.Length - 1; i++)
                {
                    PointF transformedPoint1 = points[i];
                    PointF transformedPoint2 = points[(i + 1) % points.Length];

                    Coordinate coord1 = new Coordinate(transformedPoint1.X, transformedPoint1.Y);
                    Coordinate coord2 = new Coordinate(transformedPoint2.X, transformedPoint2.Y);

                    DirectedLine directedLine = new DirectedLine(coord1, coord2);

                    directedLineList.Add(directedLine);
                }

                VoronoiRunner vonoiRunner = new VoronoiRunner(directedLineList, 20);

                Coordinate centroid = vonoiRunner.RunVoroniAlgo();

                drawShapeNmbr(graphics, polygon.Name, centroid);
            }


            float fontSize = 12f;

            Font undrFont = new Font("Arial", (int)Math.Round(fontSize), FontStyle.Bold);

            float lineEndLnth = 8.0f;

            graphics.DrawRectangle(rollPen, rectX, rectY, rectWidth, rectHeight);

            if (materialMarginInches > 0)
            {
                innerRollPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                float innerRectX = (float)(offsetX - 1.5 + scale * materialMarginInches);
                float innerRectY = (float)(offsetY - 1.5 + scale * materialMarginInches);
                float innerRectWidth = (float)((scale * maxUsedRollLgthInInches) + 3);
                float innerRectHeight = (float)((scale * effectiveRollWdthInInches) + 3);

                graphics.DrawRectangle(innerRollPen, innerRectX, innerRectY, innerRectWidth, innerRectHeight);

            }
            arrowPen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            arrowPen.CustomStartCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);


            float horzDimLocnX = rectX + rectWidth * 0.5f - (horzDimSizeF.Width * 0.5f) + 2f;
            float horzDimLocnY = rectY - 20f - (horzDimSizeF.Height * 0.5f) + 10F;

            graphics.DrawLine(arrowPen, rectX, rectY - 20.0f, rectX + rectWidth, rectY - 20f);
            graphics.DrawLine(arrowEndPen, rectX, rectY - 20.0f - lineEndLnth, rectX, rectY - 20f + lineEndLnth);
            graphics.DrawLine(arrowEndPen, rectX + rectWidth, rectY - 20.0f - lineEndLnth, rectX + rectWidth, rectY - 20f + lineEndLnth);


            if (rectWidth > horzDimSizeF.Width + 18)
            {
                graphics.FillRectangle(dimFillBrush, horzDimLocnX, horzDimLocnY - 10F, horzDimSizeF.Width, horzDimSizeF.Height);
                graphics.DrawString(horzDim, dimFont, dimBrush, horzDimLocnX, horzDimLocnY - 10F);
            }

            else
            {
                graphics.DrawString(horzDim, dimFont, dimBrush, horzDimLocnX, horzDimLocnY - 28F);
            }



            string vertDim = Utilities.FormatUtils.FormatInchesToFeetAndInches(rollWdthInInches);
            SizeF vertDimSizeF = graphics.MeasureString(vertDim, dimFont);

            float vertDimLocnX = rectX - 20f - vertDimSizeF.Height * 0.5f; // 10f - (vertDimSizeF.Height * 0.5f);
            float vertDimLocnY = rectY + rectHeight * 0.5f - vertDimSizeF.Width * 0.5f;

            graphics.DrawLine(arrowPen, rectX - 20f, rectY, rectX - 20, rectY + rectHeight);
            graphics.DrawLine(arrowEndPen, rectX - 20f - lineEndLnth, rectY, rectX - 20f + lineEndLnth, rectY);
            graphics.DrawLine(arrowEndPen, rectX - 20f - lineEndLnth, rectY + rectHeight, rectX - 20f + lineEndLnth, rectY + rectHeight);

            graphics.FillRectangle(dimFillBrush, vertDimLocnX, vertDimLocnY, vertDimSizeF.Height, vertDimSizeF.Width);

            graphics.RotateTransform(90);

            graphics.DrawString(vertDim, dimFont, dimBrush, rectY + rectHeight * 0.5f - (vertDimSizeF.Width * 0.5f) + 2f, 20f - rectX - (vertDimSizeF.Height * 0.5f));
            //graphics.DrawString(vertDim, dimFont, dimBrush, 10f, 10f);
        }

        private void drawShapeNmbr(System.Drawing.Graphics g, string shapeNmbr, Coordinate centroid)
        {
            Brush textBrush = new SolidBrush(GlobalSettings.AreaIndexFontColor);

            string sn = shapeNmbr.ToString();

            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily, 8);

            g.DrawString(sn, font, textBrush, (float)centroid.X - 7f, (float)centroid.Y - 8f);

        }

        private void TxbRollWidth_TextChanged(object sender, EventArgs e)
        {
            if (!Utilities.Utilities.IsValidFeetAndOrInches(this.txbRollWidth.Text.Trim()))
            {
                this.txbRollWidth.BackColor = Color.Pink;
            }

            else
            {
                this.txbRollWidth.BackColor = SystemColors.ControlLightLight;
            }
        }

        private void TxbMaxRollLength_TextChanged(object sender, EventArgs e)
        {
            if (!Utilities.Utilities.IsValidFeetAndOrInches(this.txbMaxRollLength.Text.Trim()))
            {
                this.txbMaxRollLength.BackColor = Color.Pink;
            }

            else
            {
                this.txbMaxRollLength.BackColor = SystemColors.ControlLightLight;
            }
        }

        private void TxbMaterialSpacingInInches_TextChanged(object sender, EventArgs e)
        {
            if (!Utilities.Utilities.IsValidPosDbl(this.txbMaterialSpacingInInches.Text.Trim()))
            {
                this.txbMaterialSpacingInInches.BackColor = Color.Pink;
            }

            else
            {
                this.txbMaterialSpacingInInches.BackColor = SystemColors.ControlLightLight;
            }
        }

        private void TxbMaterialMarginInches_TextChanged(object sender, EventArgs e)
        {
            if (!Utilities.Utilities.IsValidPosDbl(this.txbMaterialMarginInches.Text.Trim()))
            {
                this.txbMaterialMarginInches.BackColor = Color.Pink;
            }

            else
            {
                this.txbMaterialMarginInches.BackColor = SystemColors.ControlLightLight;
            }
        }

    }
}
