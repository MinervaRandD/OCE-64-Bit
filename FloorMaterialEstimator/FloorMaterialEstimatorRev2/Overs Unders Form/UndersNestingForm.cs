using CanvasManagerLib.FinishManager;
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
using OversUnders;
using OversUndersLib;
using MaterialsLayout;
using DeepNestLib;
using Geometry;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using System.IO;

namespace FloorMaterialEstimator.OversUndersForm
{
    public partial class UndersNestingForm : Form
    {

        FloorMaterialEstimatorBaseForm baseForm;

        public AreaFinishManager areaFinishManager => baseForm.selectedAreaFinishManager;


        DataTable undrDataTabl ;

        public UndersNestingForm(FloorMaterialEstimatorBaseForm baseForm, List<GraphicsUndrage> undrList)
        {
            InitializeComponent();

            this.pcbNesting.Paint += PcbNesting_Paint;

            this.baseForm = baseForm;

            string nestAllUnders = RegistryUtils.GetRegistryStringValue("UndersNesting:NestAllUnders", "False");

            if (nestAllUnders == "True")
            {
                this.rbnNestAllUnders.Checked = true;
            }

            else
            {
                this.rbnNestNonCoveredUnders.Checked = true;
            }

            this.txbRollWidth.Text = FormatUtils.FormatInchesToFeetAndInches(areaFinishManager.AreaFinishBase.RollWidthInInches);

            string strMaterialSpacing = RegistryUtils.GetRegistryStringValue("UndersNesting:MaterialSpacingInInches", "0").Trim();

            if (Utilities.Utilities.IsValidPosDbl(strMaterialSpacing))
            {
                this.txbMaterialSpacingInInches.Text = strMaterialSpacing;
            }

            else
            {
                this.txbMaterialSpacingInInches.Text = "0";
            }

            string strNmbrOfIterations = RegistryUtils.GetRegistryStringValue("UndersNesting:NumberOfIterations", "100");

            if (Utilities.Utilities.IsValidPosInt(strNmbrOfIterations))
            {
                this.nudNmbrOfIterations.Value = (decimal)decimal.Parse(strNmbrOfIterations);
            } 

            else
            {
                this.nudNmbrOfIterations.Value = 100;
            }

            string strAllow90DegreeRotation = RegistryUtils.GetRegistryStringValue("UndersNesting:Allow90DegreeRotation", "False");

            if (strAllow90DegreeRotation == "True")
            {
                this.ckbAllow90DegreeRotation.Checked = true;
            }

            else
            {
                this.ckbAllow90DegreeRotation.Checked = false;
            }

            undrDataTabl= new DataTable();

            undrDataTabl.Columns.Add("Undrage", typeof(Undrage));
            undrDataTabl.Columns.Add("Select", typeof(bool));
            undrDataTabl.Columns.Add("Id", typeof(string)).ReadOnly=true;
            undrDataTabl.Columns.Add("Width", typeof(string)).ReadOnly = true;
            undrDataTabl.Columns.Add("Height", typeof(string)).ReadOnly = true;

            double drawingScaleInInches = baseForm.CurrentPage.DrawingScaleInInches;

            foreach (GraphicsUndrage undrage in undrList)
            {
                double width = undrage.EffectiveDimensions.Item1 * drawingScaleInInches;
                double height = undrage.EffectiveDimensions.Item2 * drawingScaleInInches;

                string strWidth = FormatUtils.FormatInchesToFeetAndInches(width);
                string strHeight = FormatUtils.FormatInchesToFeetAndInches(height);

          
                undrDataTabl.Rows.Add(
                    undrage
                   , true
                    , Utilities.Utilities.IndexToUpperCaseString(undrage.UndrageIndex)
                    , strWidth
                    , strHeight);
            }


            this.dgvUnders.DataSource = undrDataTabl;

            this.dgvUnders.Columns[0].Visible = false;

            this.dgvUnders.Columns[1].Width = 48;
            this.dgvUnders.Columns[2].Width = 64;
            this.dgvUnders.Columns[3].Width = 64;
            this.dgvUnders.Columns[4].Width = 64;

            this.dgvUnders.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvUnders.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvUnders.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvUnders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvUnders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.txbMaterialSpacingInInches.TextChanged += TxbMaterialSpacingInInches_TextChanged;

            this.nudNmbrOfIterations.ValueChanged += NudNmbrOfIterations_ValueChanged;

        }

        private void NudNmbrOfIterations_ValueChanged(object sender, EventArgs e)
        {
            Utilities.RegistryUtils.SetRegistryValue("UndersNesting:NumberOfIterations", this.nudNmbrOfIterations.Value.ToString());
        }

        private void TxbMaterialSpacingInInches_TextChanged(object sender, EventArgs e)
        {
           
        }

        const string workspaceDirectory = "C:\\OCEOperatingData\\Workspace";
        private void svgWriter()
        {
           
            double totalWidth = 0;

            if (!Directory.Exists(workspaceDirectory))
            {
                Directory.CreateDirectory(workspaceDirectory);
            }

            foreach (string file in Directory.EnumerateFiles(workspaceDirectory, "*.svg"))
            {
                File.Delete(file);
            }

            for (int i = 0; i < this.undrDataTabl.Rows.Count; i++)
            {
                if (!(bool)this.undrDataTabl.Rows[i][1])
                {
                    continue;
                }

                Undrage undrage = (Undrage)this.undrDataTabl.Rows[i][0];


                double drawingScaleInInches = baseForm.CurrentPage.DrawingScaleInInches;

                double width = undrage.EffectiveDimensions.Item1 * drawingScaleInInches;
                double height = undrage.EffectiveDimensions.Item2 * drawingScaleInInches;

                totalWidth += width;

                string outpText = "<svg version=\"1.1\">\n";

                outpText += "<polygon points=\"0,0 0," + height.ToString() + " " + width.ToString() + "," + height.ToString() + " " + width.ToString() + ",0 0,0\"/>\n";
                outpText += "</svg>\n";

                File.WriteAllText("C:\\OCEOperatingData\\Workspace\\Undrg" + (i + 1) + ".svg", outpText);

            }
        }

        int rollHeightInInches = 0;

        private void btnDoNest_Click(object sender, EventArgs e)
        {
            int? feet = 0;
            int? inches = 0;

            if (!Utilities.Utilities.CheckTextBoxValidMeasurementFeetAndOrInches(this.txbRollWidth, out feet, out inches))
            {
                MessageBox.Show("Invalid roll width specified.");
                return;
            }

            rollHeightInInches = feet.Value * 12 + inches.Value;

            double materialSpacing = 0;

            if (!string.IsNullOrWhiteSpace(this.txbMaterialSpacingInInches.Text))
            {
                if (!double.TryParse(this.txbMaterialSpacingInInches.Text.Trim(), out materialSpacing))
                {
                    MessageBox.Show("Invalid material spacing specified");
                    return;
                }
            }

            DeepNestRunner deepNestRunner = new DeepNestRunner();

            var sheet = new NFP();

            sheet.AddPoint(new SvgPoint(0, 0));
            sheet.AddPoint(new SvgPoint(5000, 0));
            sheet.AddPoint(new SvgPoint(5000, rollHeightInInches));
            sheet.AddPoint(new SvgPoint(0, rollHeightInInches));
            sheet.AddPoint(new SvgPoint(0, 0));

            deepNestRunner.Context.Sheets.Add(sheet);

            List<NFP> polygonList = createPolygons();

            deepNestRunner.Context.LoadInputData(polygonList, 1);

            int iterations = (int) this.nudNmbrOfIterations.Value;

            deepNestRunner.Context.MaterialSpacing = (int) Math.Round(materialSpacing);

            deepNestRunner.Run(iterations);

            drawNestingResults(deepNestRunner.Context);

        }
         
        private List<NFP> createPolygons()
        {
            List<NFP> rtrnList = new List<NFP>();

            double totalWidth = 0.0;

            for (int i = 0; i < this.undrDataTabl.Rows.Count; i++)
            {
                if (!(bool)this.undrDataTabl.Rows[i][1])
                {
                    continue;
                }

                GraphicsUndrage undrage = (GraphicsUndrage)this.undrDataTabl.Rows[i][0];


                double drawingScaleInInches = baseForm.CurrentPage.DrawingScaleInInches;

                double width = undrage.EffectiveDimensions.Item1 * drawingScaleInInches;
                double height = undrage.EffectiveDimensions.Item2 * drawingScaleInInches;

                totalWidth += width;

                SvgPoint[] svgPoints = new SvgPoint[5];

                svgPoints[0] = new SvgPoint(0, 0);
                svgPoints[1] = new SvgPoint(width, 0);
                svgPoints[2] = new SvgPoint(width, height);
                svgPoints[3] = new SvgPoint(0, height);
                svgPoints[4] = new SvgPoint(0, 0);

                NFP polygon = new NFP();

                polygon.Points = svgPoints;
                polygon.id = (int) undrage.UndrageIndex;

                polygon.Name = Utilities.Utilities.IndexToUpperCaseString(undrage.UndrageIndex);

                rtrnList.Add(polygon);

                //TestDebug.DumpPolygonInfo(polygon, "C:\\Temp\\Polygon1.txt");
               
            }

            return rtrnList;
        }

        double maxRollWidth = 0;

        double getMaxRollWidth(NestingContext context)
        {
            double maxRollWidth = 0;

            foreach (var polygon in context.Polygons)
            {
                double rollWidth = polygon.x + polygon.WidthCalculated;

                if (rollWidth > maxRollWidth)
                {
                    maxRollWidth = rollWidth;
                }
            }

            return maxRollWidth;
        }

        double scale = 0;
        double offsetX = 0;
        double offsetY = 0;

        List<NFP> polygons = null;
        private void drawNestingResults(NestingContext context)
        {

            maxRollWidth = getMaxRollWidth(context);

            double scaleX = 800.0 / maxRollWidth;
            double scaleY = 400.0 / (double)rollHeightInInches;

            scale = Math.Min(scaleX, scaleY);

            offsetX = 30.0 + (800.0 - scale * maxRollWidth) * 0.5;
            offsetY = 30.0 + (400.0 - scale * (double)rollHeightInInches) * 0.5;

            polygons = context.Polygons;

            ignorePaint = false;


            this.pcbNesting.Refresh();

            double rollArea = rollHeightInInches * maxRollWidth;
            double areaOfPlacedItems = context.Polygons.Select(p=>p.WidthCalculated * p.HeightCalculated).Sum();

            this.lblUsedRollWidthValue.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(maxRollWidth);
            this.lblUsedRollAreaValue.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(rollArea);
            this.lblNumberOfItemsPlacedValue.Text = context.Polygons.Count.ToString();
            this.lblTotalAreaOfPlacedItemsValue.Text = Utilities.FormatUtils.FormatInchesToFeetAndInches(areaOfPlacedItems);
            this.lblPctOfRollUsedValue.Text = ((areaOfPlacedItems / rollArea) * 100.0).ToString("0.0") + "%";
        }

        bool ignorePaint = true;

        Pen rollPen = new Pen(Color.Red, 2);
        Pen undrPen = new Pen(Color.Black, 2);
        Brush fillBrush = new SolidBrush(Color.Aqua);
        Pen linePen = new Pen(Color.Red, 2);
        Pen lineEndPen = new Pen(Color.Red, 2);

        Brush undrBrush = new SolidBrush(Color.Black);
        Brush dimBrush = new SolidBrush(Color.Red);
        Font dimFont = new Font("Arial", 10);
        Brush dimFillBrush = new SolidBrush(Color.White);

        private void PcbNesting_Paint(object sender, PaintEventArgs e)
        {
            if (ignorePaint)
            {
                return;
            }

            System.Drawing.Graphics graphics = e.Graphics;

            graphics.Clear(Color.White);

            float minWidth = float.MaxValue;
            float minHeight = float.MaxValue;

            foreach (var polygon in polygons)
            {
                float polygonX = (float)(offsetX + scale * polygon.x);
                float polygonY = (float)(offsetY + scale * polygon.y);
                float polygonWidth = (float)(scale * polygon.WidthCalculated);
                float polygonHeight = (float)(scale * polygon.HeightCalculated);

                if (polygonWidth < minWidth)
                {
                    minWidth = polygonWidth;
                }

                if (polygonHeight < minHeight)
                {
                    minHeight = polygonHeight;
                }


                graphics.DrawRectangle(undrPen, polygonX, polygonY, polygonWidth, polygonHeight);
                graphics.FillRectangle(fillBrush, new RectangleF(polygonX, polygonY, polygonWidth, polygonHeight));
            }


            float fontSize = Math.Min(minWidth, minHeight) * 0.333f;

            Font undrFont = new Font("Arial", (int) Math.Round(fontSize), FontStyle.Bold);

            foreach (var polygon in polygons)
            {
                float polygonX = (float)(offsetX + scale * polygon.x);
                float polygonY = (float)(offsetY + scale * polygon.y);
                float polygonWidth = (float)(scale * polygon.WidthCalculated);
                float polygonHeight = (float)(scale * polygon.HeightCalculated);

                float textLocnX = polygonX + polygonWidth * 0.5f;
                float textLocnY = polygonY + polygonHeight * 0.5f;

                int id = polygon.Id;

                string name = polygon.Name;
                //string undrDimension = Utilities.FormatUtils.FormatInchesToFeetAndInches(polygon.WidthCalculated) + " X "
                //    + Utilities.FormatUtils.FormatInchesToFeetAndInches(polygon.HeightCalculated);

                SizeF stringSizeF = graphics.MeasureString(name, undrFont);

                textLocnX -= stringSizeF.Width * 0.5f;
                textLocnY -= stringSizeF.Height * 0.5f;

                graphics.DrawString(name, undrFont, undrBrush, textLocnX, textLocnY);

            }

            float lineEndLnth = 8.0f;

            float rectX = (float)offsetX;
            float rectY = (float)offsetY;
            float rectWidth = (float)(scale * maxRollWidth);
            float rectHeight = (float)(scale * (double)rollHeightInInches);

            graphics.DrawRectangle(rollPen, rectX, rectY, rectWidth, rectHeight);

            linePen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            linePen.CustomStartCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

            string horzDim = Utilities.FormatUtils.FormatInchesToFeetAndInches(maxRollWidth);
            SizeF horzDimSizeF = graphics.MeasureString(horzDim, dimFont);

            float horzDimLocnX = rectX + rectWidth * 0.5f - (horzDimSizeF.Width * 0.5f) + 2f;
            float horzDimLocnY = rectY - 20f - (horzDimSizeF.Height * 0.5f);

            graphics.DrawLine(linePen, rectX, rectY - 20.0f, rectX + rectWidth, rectY - 20f);
            graphics.DrawLine(lineEndPen, rectX, rectY - 20.0f - lineEndLnth, rectX, rectY - 20f + lineEndLnth);
            graphics.DrawLine(lineEndPen, rectX + rectWidth, rectY - 20.0f - lineEndLnth, rectX + rectWidth, rectY - 20f + lineEndLnth);
            
            
            graphics.FillRectangle(dimFillBrush, horzDimLocnX, horzDimLocnY, horzDimSizeF.Width, horzDimSizeF.Height);

            graphics.DrawString(horzDim, dimFont, dimBrush, horzDimLocnX, horzDimLocnY);

            string vertDim = Utilities.FormatUtils.FormatInchesToFeetAndInches(rollHeightInInches);
            SizeF vertDimSizeF = graphics.MeasureString(vertDim, dimFont);

            float vertDimLocnX = 10f - (vertDimSizeF.Height * 0.5f);
            float vertDimLocnY = rectY + rectHeight * 0.5f - vertDimSizeF.Width * 0.5f;

            graphics.DrawLine(linePen, rectX - 20f, rectY, rectX - 20, rectY + rectHeight);
            graphics.DrawLine(lineEndPen, rectX - 20f - lineEndLnth, rectY, rectX - 20f + lineEndLnth, rectY);
            graphics.DrawLine(lineEndPen, rectX - 20f - lineEndLnth, rectY + rectHeight, rectX - 20f + lineEndLnth, rectY + rectHeight);

            graphics.FillRectangle(dimFillBrush, vertDimLocnX, vertDimLocnY, vertDimSizeF.Height, vertDimSizeF.Width);

            graphics.RotateTransform(90);
           
            graphics.DrawString(vertDim, dimFont, dimBrush, rectY + rectHeight * 0.5f - (vertDimSizeF.Width * 0.5f) + 2f, 20f - rectX - (vertDimSizeF.Height * 0.5f));
            //graphics.DrawString(vertDim, dimFont, dimBrush, 10f, 10f);
        }

    }
}
