//-------------------------------------------------------------------------------//
// <copyright file="DebugForm.cs"                                                //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2020        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using FloorMaterialEstimator.CanvasManager;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;
    using System.Collections.Generic;
    using FloorMaterialEstimator.Finish_Controls;
    using CanvasLib.Counters;
    using Globals;
    using Graphics;
    using Geometry;
    using TracerLib;

    public partial class DebugForm : Form, ICursorManagementForm
    {
        FloorMaterialEstimatorBaseForm baseForm;

        GraphicsWindow window;

        GraphicsPage page;

        CanvasManager.CanvasManager canvasManager => baseForm.CanvasManager;

        CanvasPage currentPage => canvasManager.CurrentPage;

        /// <summary>
        /// The debug form is a form used for examining the internal state of the system.
        /// </summary>
        /// <param name="baseForm">Reference to the floor materials estimator base form</param>
        public DebugForm(
            FloorMaterialEstimatorBaseForm baseForm
            , GraphicsWindow window
            , GraphicsPage page)
        {
            InitializeComponent();

            this.window = window;

            this.page = page;

           // AddToCursorManagementList();

            this.FormClosed += FinishesEditForm_FormClosed;

            this.baseForm = baseForm;

            setupLayoutAreaHierarchy();
            setupLayoutAreaOverview();
            //setupCounters();
            setupDirectedLines();
            setupBaseVisioShapes();
            setupCanvasShapes();
            setupGuidMaintenance();
            setupAreaFinishes();
            setupLineFinishes();
            setupGraphicsLayers();
            setupVisioLayers();
            setupGraphicsShapes();
            setupVisioShapes();

            setSize();

            this.SizeChanged += DebugForm_SizeChanged;
            this.KeyDown += DebugForm_KeyDown;
            this.KeyPreview = true;
            Refresh();
        }

        private void DebugForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                this.btnRefresh_Click(null, null);
            }
        }

        private void DebugForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int rfrsSizeX = this.btnRefresh.Width;
            int rfrsSizeY = this.btnRefresh.Height;

            int rfrsPosnX = (formSizeX - rfrsSizeX) / 2;
            int rfrsPosnY = formSizeY - rfrsSizeY - 12;

            int tabcSizeX = formSizeX - 16;
            int tabcSizeY = rfrsPosnY - 12;

            int tabcPosnX = 8;
            int tabcPosnY = 8;

            this.tbcDebugForm.Size = new Size(tabcSizeX, tabcSizeY);
            this.tbcDebugForm.Location = new Point(tabcPosnX, tabcPosnY);

            this.dgvVisioLayers.Size = new Size(830, 500);
            this.dgvGraphicsLayers.Size = new Size(930, 500);

            this.btnRefresh.Location = new Point(rfrsPosnX, rfrsPosnY);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            doRefresh();
        }

        public void Refresh()
        {
            doRefresh();
        }

        private void doRefresh()
        {
            populateBaseVisioShapes();
            populateCanvasShapes();
            populateAreaHierarchy();
            populateLayerSummary();
            populateAreaOverview();
            populateDirectedLines();
            populateGuidMaintenance();
            populateAreaFinishes();
            populateLineFinishes();
            //populateCounters();
            populateGraphicsLayers();
            populateVisioLayers();
            populateGraphicsShapes();
            populateVisioShapes();
        }


        private void setupLayoutAreaHierarchy()
        {
            this.trvLayoutAreaHierarchy.ShowPlusMinus = true;
            this.trvLayoutAreaHierarchy.ShowLines = true;

            FontFamily fontFamily = new FontFamily("Courier New");
            this.trvLayoutAreaHierarchy.Font = new Font(fontFamily, 10, FontStyle.Regular);
        }

        private void setupAreaFinishes()
        {
            this.trvAreaFinishes.ShowPlusMinus = true;
            this.trvAreaFinishes.ShowLines = true;

            FontFamily fontFamily = new FontFamily("Courier New");
            this.trvAreaFinishes.Font = new Font(fontFamily, 10, FontStyle.Regular);
        }

        private void setupLineFinishes()
        {
            this.trvLineFinishes.ShowPlusMinus = true;
            this.trvLineFinishes.ShowLines = true;

            FontFamily fontFamily = new FontFamily("Courier New");
            this.trvLineFinishes.Font = new Font(fontFamily, 10, FontStyle.Regular);
        }

        private void setupLayoutAreaOverview()
        {
            this.trvLayoutAreaOverview.ShowPlusMinus = true;
            this.trvLayoutAreaOverview.ShowLines = true;

            FontFamily fontFamily = new FontFamily("Courier New");
            this.trvLayoutAreaOverview.Font = new Font(fontFamily, 10, FontStyle.Regular);
        }

        private void setupBaseVisioShapes()
        {
            this.dgvBaseVisioShapes.TabIndex = 0;
            this.dgvBaseVisioShapes.TabStop = false;

            this.dgvBaseVisioShapes.Columns.Add("Data 1", "Role");
            this.dgvBaseVisioShapes.Columns.Add("Data 2", "Type");
            this.dgvBaseVisioShapes.Columns.Add("Data 3", "Guid");

            for (int i = 0; i < 3; i++)
            {
                this.dgvBaseVisioShapes.Columns[i].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, FontStyle.Regular);
                this.dgvBaseVisioShapes.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
            this.dgvBaseVisioShapes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, FontStyle.Bold);
            this.dgvBaseVisioShapes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            this.dgvBaseVisioShapes.Columns[0].Width = 190;
            this.dgvBaseVisioShapes.Columns[1].Width = 180;
            this.dgvBaseVisioShapes.Columns[2].Width = 76;

            this.dgvBaseVisioShapes.AllowUserToAddRows = false;
            this.dgvBaseVisioShapes.RowHeadersVisible = false;

            this.dgvBaseVisioShapes.ClearSelection();
        }

        private void setupCanvasShapes()
        {
            this.dgvCanvasShapes.Columns.Add("Data 1", "Role");
            this.dgvCanvasShapes.Columns.Add("Data 2", "Type");
            this.dgvCanvasShapes.Columns.Add("Data 3", "Guid");

            for (int i = 0; i < 3; i++)
            {
                this.dgvCanvasShapes.Columns[i].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, FontStyle.Regular);
                this.dgvCanvasShapes.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dgvCanvasShapes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, FontStyle.Bold);
            this.dgvCanvasShapes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            this.dgvCanvasShapes.Columns[0].Width = 190;
            this.dgvCanvasShapes.Columns[1].Width = 180;
            this.dgvCanvasShapes.Columns[2].Width = 76;

            this.dgvCanvasShapes.AllowUserToAddRows = false;
            this.dgvCanvasShapes.RowHeadersVisible = false;

            this.dgvCanvasShapes.ClearSelection();
        }

        //private void setupCounters()
        //{
        //    this.dgvCounters.Columns.Add("Tag", "Tag");
        //    this.dgvCounters.Columns.Add("Guid", "Guid");
           
        //    for (int i = 0; i < 2; i++)
        //    {
        //        this.dgvCounters.Columns[i].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, FontStyle.Regular);
        //        this.dgvCounters.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    }

        //    this.dgvCounters.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, FontStyle.Bold);
        //    this.dgvCounters.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        //    this.dgvCounters.Columns[0].Width = 48;
        //    this.dgvCounters.Columns[1].Width = 96;
            
        //    this.dgvCounters.AllowUserToAddRows = false;
        //    this.dgvCounters.RowHeadersVisible = false;
        //}

        private void setupDirectedLines()
        {
            this.dgvDirectedLines.Columns.Add("Guid", "Guid");
            this.dgvDirectedLines.Columns.Add("Polygon Guid", "Polygon Guid");
            this.dgvDirectedLines.Columns.Add("Area Guid", "Area Guid");
            this.dgvDirectedLines.Columns.Add("Coord1", "Coord1");
            this.dgvDirectedLines.Columns.Add("Coord2", "Coord2");

            for (int i = 0; i < this.dgvDirectedLines.Columns.Count; i++)
            {
                this.dgvDirectedLines.Columns[i].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
                this.dgvDirectedLines.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dgvDirectedLines.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Bold);
            this.dgvDirectedLines.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            this.dgvDirectedLines.Columns[0].Width = 96;
            this.dgvDirectedLines.Columns[1].Width = 96;
            this.dgvDirectedLines.Columns[2].Width = 96;
            this.dgvDirectedLines.Columns[3].Width = 160;
            this.dgvDirectedLines.Columns[4].Width = 160;

            this.dgvDirectedLines.AllowUserToAddRows = false;
            this.dgvDirectedLines.RowHeadersVisible = false;
        }

        private void setupGuidMaintenance()
        {
            this.dgvGuidMaintenance.Columns.Add("Guid", "Guid");
            this.dgvGuidMaintenance.Columns.Add("Shape", "Shape");

            for (int i = 0; i < this.dgvGuidMaintenance.Columns.Count; i++)
            {
                this.dgvGuidMaintenance.Columns[i].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
                this.dgvGuidMaintenance.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dgvGuidMaintenance.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Bold);
            this.dgvGuidMaintenance.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGuidMaintenance.Columns[0].Width = 96;
            this.dgvGuidMaintenance.Columns[1].Width = 512;
           
            this.dgvGuidMaintenance.AllowUserToAddRows = false;
            this.dgvGuidMaintenance.RowHeadersVisible = false;
        }

        private void populateGuidMaintenance()
        {
            this.dgvGuidMaintenance.Rows.Clear();

            foreach (KeyValuePair<string, object> kvp in GuidMaintenance.GuidDict)
            {
                dgvGuidMaintenance.Rows.Add(new object[] { kvp.Key.Substring(0,8), kvp.Value.GetType().ToString() });
            }

            dgvGuidMaintenance.Refresh();

            //this.lblBaseVisioShapes.Focus();
        }

        private void populateBaseVisioShapes()
        {
            int count = 0;

            this.dgvBaseVisioShapes.Rows.Clear();

            foreach (Visio.Shape visioShape in page.VisioPage.Shapes)
            {
                string data1 = string.IsNullOrEmpty(visioShape.Data1) ? "" : visioShape.Data1;
                string data2 = string.IsNullOrEmpty(visioShape.Data2) ? "" : visioShape.Data2;
                string data3 = string.IsNullOrEmpty(visioShape.Data3) ? "" :
                    visioShape.Data3.Length <= 8 ? visioShape.Data3 : visioShape.Data3.Substring(0, 8);

                dgvBaseVisioShapes.Rows.Add(new object[] { data1, data2, data3 });

                count++;
            }

            dgvBaseVisioShapes.Refresh();

            this.dgvBaseVisioShapes.ClearSelection();

            this.lblVisioShapeCount.Text = "Count: " + count.ToString();
        }

        private void populateCanvasShapes()
        {
            int count = 0;

            this.dgvCanvasShapes.Rows.Clear();

            foreach (IGraphicsShape iShape in page.PageShapeDictValues)
            {
                Shape shape = iShape.Shape;

                string data1 = "<null>";
                string data2 = "<null>";
                string data3 = iShape.Guid;

                try
                {
                    if (shape != null)
                    {
                        if (shape.VisioShape != null)
                        {
                            data1 = string.IsNullOrEmpty(shape.Data1) ? "" : shape.Data1;
                            data2 = string.IsNullOrEmpty(shape.Data2) ? "" : shape.Data2;
                            data3 = string.IsNullOrEmpty(shape.Data3) ? "" :
                                shape.Data3.Length <= 8 ? shape.Data3 : shape.Data3.Substring(0, 8);

                            count++;
                        }
                    }
                }

                catch (Exception ex)
                {
                    data1 = "<unavailable>";
                    data2 = "<unavailable>";
                }

                dgvCanvasShapes.Rows.Add(new object[] { data1, data2, data3 });
            }

            dgvCanvasShapes.Refresh();

            this.dgvCanvasShapes.ClearSelection();

            this.lblShapeDictCount.Text = "Count: " + count.ToString();
        }

        private void populateDirectedLines()
        {
            this.dgvDirectedLines.Rows.Clear();

            foreach (CanvasDirectedLine directedLine in currentPage.DirectedLines)
            {
                string directedlineGuid = string.IsNullOrEmpty(directedLine.Guid) ? string.Empty : directedLine.Guid.Substring(0, 8);
                string parentPolygonGuid = string.IsNullOrEmpty(directedLine.ParentPolygonGuid) ? string.Empty : directedLine.ParentPolygonGuid.Substring(0, 8);
                string parentLayoutAreaGuid = string.IsNullOrEmpty(directedLine.ParentAreaGuid) ? string.Empty : directedLine.ParentAreaGuid.Substring(0, 8);

                string coord1 = '(' + directedLine.Coord1.X.ToString("0.000").PadLeft(6) + ',' + directedLine.Coord1.Y.ToString("0.000").PadLeft(6) + ')';
                string coord2 = '(' + directedLine.Coord2.X.ToString("0.000").PadLeft(6) + ',' + directedLine.Coord2.Y.ToString("0.000").PadLeft(6) + ')';

                dgvDirectedLines.Rows.Add(
                    new object[] {
                        directedlineGuid,
                        parentPolygonGuid,
                        parentLayoutAreaGuid,
                        coord1,
                        coord2
                    });
            }

            dgvDirectedLines.Refresh();
        }

        private void populateGraphicsLayers()
        {
            this.dgvGraphicsLayers.Rows.Clear();

            foreach (GraphicsLayer graphicsLayer in currentPage.GraphicsLayers)
            {
                string graphicsLayerGuid = string.IsNullOrEmpty(graphicsLayer.Guid) ? string.Empty : graphicsLayer.Guid.Substring(0, 8);

                string layerName = string.Empty;

                string visioGuid = string.Empty;

                Visio.Layer visioLayer = graphicsLayer.visioLayer;

                if (Utilities.IsNotNull(visioLayer))
                {
                    layerName = visioLayer.Name;
                    visioGuid = string.IsNullOrEmpty(visioLayer.NameU) ? string.Empty : visioLayer.NameU.Substring(0, 8);
                }

                int? shapeCount = null;

                if (Utilities.IsNotNull(graphicsLayer.ShapeDict))
                {
                    shapeCount = graphicsLayer.ShapeDict.Count;
                }

                bool visible = graphicsLayer.Visibility;

                dgvGraphicsLayers.Rows.Add(
                    new object[] {
                        graphicsLayerGuid
                        ,layerName
                        ,visioGuid
                        ,shapeCount
                        ,visible.ToString()
                    }) ;
            }

            dgvDirectedLines.Refresh();

            this.lblTotalGraphicsLayers.Text = "Total graphics layers: " + currentPage.GraphicsLayerDict.Count;
           
            //this.lblDirectedLines.Focus();
        }

        Dictionary<Visio.Layer, int> LayerCountDict = new Dictionary<Visio.Layer, int>();

        private void populateVisioLayers()
        {
            this.dgvVisioLayers.Rows.Clear();

            LayerCountDict.Clear();

            foreach (Visio.Shape shape in this.page.VisioPage.Shapes)
            {
                for (short i = 1; i <= shape.LayerCount; i++)
                {
                    Visio.Layer layer = shape.Layer[i];

                    if (!LayerCountDict.ContainsKey(layer))
                    {
                        LayerCountDict.Add(layer, 0);
                    }

                    LayerCountDict[layer]++;
                }
            }


            foreach (Visio.Layer layer in page.VisioPage.Layers)
            {
                string layerName = layer.Name;

                string visioGuid = string.IsNullOrEmpty(layer.NameU) ? string.Empty : layer.NameU.Substring(0, 8);

                int count = 0;

                if (LayerCountDict.ContainsKey(layer))
                {
                    count = LayerCountDict[layer];
                }

                bool visible = VisioInterop.GetLayerVisibility(layer);

                dgvVisioLayers.Rows.Add(
                    new object[] {
                        layerName
                        ,visioGuid
                        ,count.ToString()
                        ,visible.ToString()
                    }) ;
            }

            dgvVisioLayers.Refresh();

            this.lblTotalVisioLayers.Text = "Total visio layers: " + page.VisioPage.Layers.Count;
            //this.lblDirectedLines.Focus();
        }

        private void populateVisioShapes()
        {
            this.dgvVisioShapes.Rows.Clear();

            List<Visio.Shape> visioShapeList = new List<Visio.Shape>();

            foreach (Visio.Shape shape in page.VisioPage.Shapes)
            {
                visioShapeList.Add(shape);
            }

            visioShapeList.Sort((s1, s2) => string.Compare(s1.Data1, s2.Data1));

            foreach (Visio.Shape shape in visioShapeList)
            {
                string shapeData1 = shape.Data1;
                string shapeData2 = shape.Data2;
                string shapeGuid = string.Empty;

                if (!string.IsNullOrEmpty(shape.Data3))
                {
                     shapeGuid = shape.Data3.Substring(0, Math.Min(shape.Data3.Length, 8));
                }

                dgvVisioShapes.Rows.Add(
                    new object[] {
                        shapeData1,
                        shapeData2,
                        shapeGuid
                    });
            }

            dgvVisioShapes.Refresh();

            this.lblTotalVisioShapes.Text = "Total Visio Shapes: " + page.VisioPage.Shapes.Count.ToString();

            //this.lblTotalVisioLayers.Text = "Total visio layers: " + VsoPage.Layers.Count;
        }

        private void populateGraphicsShapes()
        {
            this.dgvGraphicsShapes.Rows.Clear();

            List<Shape> shapeList = new List<Shape>();

            foreach (IGraphicsShape iShape in this.page.PageShapeDictValues)
            {
                Shape shape = iShape.Shape;

                if (shape is null)
                {
                    continue;
                }

                shapeList.Add(iShape.Shape);
            }

            shapeList.Sort((s1, s2) => string.Compare(s1.Data1, s2.Data1));

            foreach (Shape shape in shapeList)
            {
                string shapeGuid = shape.Guid;

                if (!string.IsNullOrEmpty(shapeGuid))
                {
                    shapeGuid = shapeGuid.Substring(0, Math.Min(shapeGuid.Length, 8));
                }

                string shapeType = shape.ShapeType.ToString();

                string visioGuid = "<null>";

                string visioSrce = "<null>";
                string visioType = "<null>";

                Visio.Shape visioShape = shape.VisioShape;

                if (Utilities.IsNotNull(visioShape))
                {
                    // There is a bug here in which the data reference throws an exception.
                    // Trapped for the time being

                    try
                    {
                        visioGuid = visioShape.Data3;

                        if (visioGuid.Length > 8)
                        {
                            visioGuid = visioGuid.Substring(0, 8);
                        }

                    }

                    catch
                    {
                        visioGuid = "(Throws exception)";
                    }

                    try
                    {
                        visioSrce = visioShape.Data1;
                    }

                    catch
                    {
                        visioSrce = "(Throws exception)";
                    }

                    try
                    {
                        visioType = visioShape.Data2;
                    }

                    catch
                    {
                        visioType = "(Throws exception)";
                    }
                }

                dgvGraphicsShapes.Rows.Add(
                    new object[] {
                        shapeGuid,
                        shapeType,
                        visioGuid,
                        visioSrce,
                        visioType
                    });
            }

           
            this.lblGraphicsShapeCount.Text = "Total Graphics Shapes: " + shapeList.Count.ToString();

            dgvGraphicsShapes.Refresh();
        }

        private void populateAreaHierarchy()
        {
            this.trvLayoutAreaHierarchy.BeginUpdate();

            this.trvLayoutAreaHierarchy.Nodes.Clear();

            foreach (CanvasLayoutArea layoutArea in currentPage.LayoutAreas)
            {
                if (layoutArea.ParentArea != null)
                {
                    continue;
                }

                TreeNode treeNode = generateTreeNode(layoutArea);

                this.trvLayoutAreaHierarchy.Nodes.Add(treeNode);

            }

            this.trvLayoutAreaHierarchy.EndUpdate();

            this.trvLayoutAreaHierarchy.Refresh();

            //this.lblLayoutAreaHierarchy.Focus();
        }

        private TreeNode generateTreeNode(CanvasLayoutArea layoutArea)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layoutArea });

            try
            {
                string nodeLabel = string.Empty;

                if (string.IsNullOrEmpty(layoutArea.Guid))
                {
                    nodeLabel = "<No GUID>";
                }

                else if (layoutArea.Guid.Length < 8)
                {
                    nodeLabel = layoutArea.Guid;
                }

                else
                {
                    nodeLabel = layoutArea.Guid.Substring(0, 8) + "    ";
                }

                if (layoutArea.ParentArea is null)
                {
                    nodeLabel += "        ";
                }

                else
                {
                    nodeLabel += layoutArea.ParentAreaGuid.Substring(0, 8);
                }

                TreeNode treeNode = new TreeNode(nodeLabel);

                foreach (CanvasLayoutArea offspring in layoutArea.OffspringAreas)
                {
                    treeNode.Nodes.Add(generateTreeNode(offspring));
                }

                return treeNode;
            }
            
            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in DebugForm:generateTreeNode", ex, 1, true);

                return new TreeNode("Error");
            }
        }

        private void populateLayerSummary()
        {
            this.trvLayerSummary.BeginUpdate();

            this.trvLayerSummary.Nodes.Clear();

            foreach (GraphicsLayer layer in page.GraphicsLayers)
            {
                if (layer.ShapeDict.Count <= 0)
                {
                    continue; // Only show layers with shapes
                }
                
                TreeNode treeNode = generateTreeNode(layer);

                this.trvLayerSummary.Nodes.Add(treeNode);

            }

            this.trvLayerSummary.EndUpdate();

            this.trvLayerSummary.Refresh();

        }

        private TreeNode generateTreeNode(GraphicsLayer layer)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { layer });

            try
            {
                TreeNode treeNode = new TreeNode(layer.ToString());

                foreach (IGraphicsShape iShape in layer.ShapeDict.Values)
                {
                    treeNode.Nodes.Add(generateTreeNode(iShape.Shape));
                }

                return treeNode;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in DebugForm:generateTreeNode", ex, 1, true);

                return new TreeNode("Error");
            }
        }

        private TreeNode generateTreeNode(Shape shape)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { shape });

            try
            {
                TreeNode treeNode = new TreeNode(shape.ToString());

                return treeNode;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in DebugForm:generateTreeNode", ex, 1, true);

                return new TreeNode("Error");
            }
        }

        private void populateAreaOverview()
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });

            try
            {
                this.trvLayoutAreaOverview.BeginUpdate();

                this.trvLayoutAreaOverview.Nodes.Clear();

                foreach (CanvasLayoutArea layoutArea in currentPage.LayoutAreas)
                {
                    string nodeLabel = string.Empty;

                    if (string.IsNullOrEmpty(layoutArea.Guid))
                    {
                        nodeLabel = "<No GUID>";
                    }

                    else if (layoutArea.Guid.Length < 8)
                    {
                        nodeLabel = layoutArea.Guid;
                    }

                    else
                    {
                        nodeLabel = layoutArea.Guid.Substring(0, 8) + "    ";
                    }


                    if (layoutArea.ParentArea is null)
                    {
                        nodeLabel += "        ";
                    }

                    else
                    {
                        nodeLabel += layoutArea.ParentAreaGuid.Substring(0, 8);
                    }

                    TreeNode treeNode = new TreeNode(nodeLabel);

                    if (layoutArea.ExternalArea != null)
                    {
                        TreeNode externalArea = generateNode(layoutArea.ExternalArea, "External area:");

                        treeNode.Nodes.Add(externalArea);
                    }

                    if (layoutArea.InternalAreas != null)
                    {
                        foreach (CanvasDirectedPolygon polygon in layoutArea.InternalAreas)
                        {
                            TreeNode internalArea = generateNode(polygon, "Internal area:");

                            treeNode.Nodes.Add(internalArea);
                        }
                    }

                    this.trvLayoutAreaOverview.Nodes.Add(treeNode);
                }

                this.trvLayoutAreaOverview.EndUpdate();

                this.trvLayoutAreaOverview.Refresh();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in DebugForm:generateTreeNode", ex, 1, true);
            }
            //this.lblLayoutAreaSummary.Focus();
        }

        private TreeNode generateNode(CanvasDirectedPolygon polygon, string prefix = "")
        {
            string nodeLabel = prefix + ' ';

            if (!string.IsNullOrEmpty(polygon.Guid))
            {
                nodeLabel += polygon.Guid.Substring(0, 8);
            }

            TreeNode treeNode = new TreeNode(nodeLabel);

            foreach (CanvasDirectedLine directedLine in polygon)
            {
                TreeNode lineNode = generateNode(directedLine, "Line:");

                treeNode.Nodes.Add(lineNode);
            }

            return treeNode;
        }

        private TreeNode generateNode(CanvasDirectedLine directedLine, string prefix = "")
        {
            string nodeLabel = prefix + ' ';

            if (!string.IsNullOrEmpty(directedLine.Guid))
            {
                nodeLabel += directedLine.Guid.Substring(0, 8);
            }

            else
            {
                nodeLabel += "        ";
            }

            string coord1 = '(' + directedLine.Coord1.X.ToString("0.000") + ',' + directedLine.Coord1.Y.ToString("0.000") + ')';
            string coord2 = '(' + directedLine.Coord2.X.ToString("0.000") + ',' + directedLine.Coord2.Y.ToString("0.000") + ')';

            nodeLabel += ' ' + coord1 + ' ' + coord2;

            TreeNode treeNode = new TreeNode(nodeLabel);
            
            return treeNode;
        }

        private void populateAreaFinishes()
        {
            this.trvAreaFinishes.BeginUpdate();

            this.trvAreaFinishes.Nodes.Clear();

            foreach (AreaFinishManager areaFinishManager in CanvasManagerGlobals.AreaFinishManagerList)
            {
                TreeNode areaTreeNode = new TreeNode(areaFinishManager.AreaFinishBase.AreaName);

                foreach (CanvasLayoutArea layoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    TreeNode layoutTreeNode = new TreeNode(layoutArea.Guid.Substring(0,8));

                    areaTreeNode.Nodes.Add(layoutTreeNode);
                }

                trvAreaFinishes.Nodes.Add(areaTreeNode);
            }

            this.trvAreaFinishes.EndUpdate();

            this.trvAreaFinishes.Refresh();
        }

        private void populateLineFinishes()
        {
            this.trvLineFinishes.BeginUpdate();

            this.trvLineFinishes.Nodes.Clear();

            foreach (LineFinishManager lineFinishManager in CanvasManagerGlobals.LineFinishManagerList)
            {
                TreeNode lineTreeNode = new TreeNode(lineFinishManager.LineFinishBase.LineName);

                foreach (CanvasDirectedLine directedLine in lineFinishManager.CanvasDirectedLines)
                {
                    TreeNode directedLineTreeNode = new TreeNode(directedLine.Guid.Substring(0,8));

                    lineTreeNode.Nodes.Add(directedLineTreeNode);
                }

                trvLineFinishes.Nodes.Add(lineTreeNode);

            }

            this.trvLineFinishes.EndUpdate();

            this.trvLineFinishes.Refresh();
        }

        private void setupGraphicsLayers()
        {
            this.dgvGraphicsLayers.Columns.Add("Guid", "Guid");
            this.dgvGraphicsLayers.Columns.Add("Layer Name", "Layer Name");
            this.dgvGraphicsLayers.Columns.Add("Visio Guid", "Visio Guid");
            this.dgvGraphicsLayers.Columns.Add("Shape Count", "Shape Count");
            this.dgvGraphicsLayers.Columns.Add("Visible", "Visible");

            this.dgvGraphicsLayers.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsLayers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsLayers.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsLayers.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.dgvGraphicsLayers.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsLayers.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsLayers.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsLayers.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvGraphicsLayers.Columns[4].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsLayers.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsLayers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Bold);
            this.dgvGraphicsLayers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsLayers.Columns[0].Width = 100;
            this.dgvGraphicsLayers.Columns[1].Width = 512;
            this.dgvGraphicsLayers.Columns[2].Width = 100;
            this.dgvGraphicsLayers.Columns[3].Width = 100;
            this.dgvGraphicsLayers.Columns[4].Width = 100;

            this.dgvGraphicsLayers.AllowUserToAddRows = false;
            this.dgvGraphicsLayers.RowHeadersVisible = false;

            this.dgvGraphicsLayers.Size = new Size(96 + 512 + 96 + 96 + 20, this.dgvGraphicsLayers.Height);
        }

        private void setupVisioLayers()
        {
            this.dgvVisioLayers.Columns.Add("Layer Name", "Layer Name");
            this.dgvVisioLayers.Columns.Add("Visio Guid", "Visio Guid");
            this.dgvVisioLayers.Columns.Add("Shape Count", "Shape Count");
            this.dgvVisioLayers.Columns.Add("Visible", "Visible");

            this.dgvVisioLayers.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioLayers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.dgvVisioLayers.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioLayers.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvVisioLayers.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioLayers.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvVisioLayers.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioLayers.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvVisioLayers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Bold);
            this.dgvVisioLayers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvVisioLayers.Columns[0].Width = 512;
            this.dgvVisioLayers.Columns[1].Width = 100;
            this.dgvVisioLayers.Columns[2].Width = 100;
            this.dgvVisioLayers.Columns[3].Width = 100;

            this.dgvVisioLayers.AllowUserToAddRows = false;
            this.dgvVisioLayers.RowHeadersVisible = false;

            this.dgvVisioLayers.Size = new Size(96 + 512 +  96 + 20, this.dgvVisioLayers.Height);
        }

        private void setupGraphicsShapes()
        {
            this.dgvGraphicsShapes.Columns.Add("Shape Guid", "Shape Guid");
            this.dgvGraphicsShapes.Columns.Add("Shape Type", "Shape Type");
            this.dgvGraphicsShapes.Columns.Add("Visio Guid", "Visio Guid");
            this.dgvGraphicsShapes.Columns.Add("Visio Srce", "Visio Srce");
            this.dgvGraphicsShapes.Columns.Add("Visio Type", "Visio Type");

            this.dgvGraphicsShapes.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsShapes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsShapes.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsShapes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsShapes.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsShapes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsShapes.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsShapes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsShapes.Columns[4].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvGraphicsShapes.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsShapes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Bold);
            this.dgvVisioLayers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvGraphicsShapes.Columns[0].Width = 128;
            this.dgvGraphicsShapes.Columns[1].Width = 128;
            this.dgvGraphicsShapes.Columns[2].Width = 128;
            this.dgvGraphicsShapes.Columns[3].Width = 280;
            this.dgvGraphicsShapes.Columns[4].Width = 128;

            this.dgvGraphicsShapes.AllowUserToAddRows = false;
            this.dgvGraphicsShapes.RowHeadersVisible = false;

            this.dgvGraphicsShapes.Size = new Size(this.dgvGraphicsLayers.Width, this.dgvGraphicsShapes.Height);
        }

        private void setupVisioShapes()
        {
            this.dgvVisioShapes.Columns.Add("Shape Data 1", "Shape Data 1");
            this.dgvVisioShapes.Columns.Add("Shape Data 2", "Shape Data 2");
            this.dgvVisioShapes.Columns.Add("Shape Guid", "Shape Guid");

            this.dgvVisioShapes.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioShapes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.dgvVisioShapes.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioShapes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.dgvVisioShapes.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Regular);
            this.dgvVisioShapes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvVisioShapes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Courier New", 10F, FontStyle.Bold);
            this.dgvVisioLayers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvVisioShapes.Columns[0].Width = 340;
            this.dgvVisioShapes.Columns[1].Width = 128;
            this.dgvVisioShapes.Columns[2].Width = 128;

            this.dgvVisioShapes.AllowUserToAddRows = false;
            this.dgvVisioShapes.RowHeadersVisible = false;

            this.dgvVisioShapes.Size = new Size(this.dgvVisioLayers.Width, this.dgvVisioShapes.Height) ;
        }

        private void btnTestCase1_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = Color.Orange;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 6);
            canvasManager.ContinueSubdivisionDraw(11, 6);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase2_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = Color.Orange;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 3);
            canvasManager.ContinueSubdivisionDraw(11, 3);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase3_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = Color.Orange;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 4);
            canvasManager.ContinueSubdivisionDraw(11, 4);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase4_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = Color.Orange;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 6);
            canvasManager.ContinueSubdivisionDraw(11, 6);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase5_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = Color.Orange;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 3);
            canvasManager.ContinueSubdivisionDraw(11, 3);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase6_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = Color.Orange;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);

            baseForm.btnLayoutAreaTakeout.BackColor = SystemColors.ControlLightLight;

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 4);
            canvasManager.ContinueSubdivisionDraw(11, 4);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase7_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.BtnSeamDesignState_Click(null, null);
            baseForm.SeamMode = SeamMode.Subdivision;

            canvasManager.SelectAreaForSubdivision(3, 3);
            canvasManager.ContinueSubdivisionDraw(1, 2);
            canvasManager.ContinueSubdivisionDraw(11, 2);

            canvasManager.ProcessPolylineCompleteShape(true);
        }

        private void btnTestCase8_Click(object sender, EventArgs e)
        {
            List<Coordinate> coordList = new List<Coordinate>()
            {
                new Coordinate(3.7917639940635364, 7.0085473824698967)
                , new Coordinate(8.3760688229518276, 9.4483299524206146)
                , new Coordinate(15.104895910777694, 6.37140671133627)
                , new Coordinate(12.478633144397621, 0.93240098214677125)
                , new Coordinate(5.82750613841732, 2.7505828973329751)
            };

            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            foreach (Coordinate coord in coordList)
            {
                canvasManager.ProcessAreaDesignStateClick(2, 0, coord.X, coord.Y, ref cancelDefault);
            }
        }
        private void btnTestCaseS1_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
           // baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.CurrentPage.DrawingScaleInInches = 60;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
      

            baseForm.BtnSeamDesignState_Click(null, null);
        }

        private void btnTestCaseS2_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
           // baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.CurrentPage.DrawingScaleInInches = 72;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.BtnSeamDesignState_Click(null, null);
            
        }

        private void btnTestCaseS3_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
           // baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.CurrentPage.DrawingScaleInInches = 72;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 12, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 12, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 11, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 11, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 9, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 9, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 5, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 5, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 3, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 3, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.BtnSeamDesignState_Click(null, null);
        }

        private void btnTestCaseS4_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
           // baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.CurrentPage.DrawingScaleInInches = 72;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 12, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 12, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 11, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 11, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 9, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 9, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 5, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 3, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);


            baseForm.BtnSeamDesignState_Click(null, null);
        }

        private void btnTestCaseS5_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
         //   baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.CurrentPage.DrawingScaleInInches = 72;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 12, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 7, 12, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
           

            baseForm.BtnSeamDesignState_Click(null, null);
        }

        private void btnRemnantTestCaseS1_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
       //     baseForm.AreaMode = AreaMode.Layout;

            canvasManager.CurrentPage.DrawingScaleInInches = 60;

            baseForm.BtnSeamDesignState_Click(null, null);

            baseForm.btnSeamDesignStateRemnantMode_Click(null, null);

            bool cancelDefault = false;

            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 8, 2, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 8, 8, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 2, 8, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 2, 2, ref cancelDefault);

        }

        private void btnRemnantTestCaseS2_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
            //baseForm.AreaMode = AreaMode.Layout;

            canvasManager.CurrentPage.DrawingScaleInInches = 72;

            baseForm.BtnSeamDesignState_Click(null, null);

            baseForm.btnSeamDesignStateRemnantMode_Click(null, null);

            bool cancelDefault = false;

            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 12, 2, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 7, 12, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 2, 2, ref cancelDefault);

        }

        private void btnRemnantTestCaseS3_Click(object sender, EventArgs e)
        {
            btnTestCaseS5_Click(null, null);

            baseForm.btnSeamDesignStateRemnantMode_Click(null, null);

            bool cancelDefault = false;

            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 12, 10, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 16, 10, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 18, 6, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 10, 6, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 12, 10, ref cancelDefault);
        }

        private void btnRemnantTestCaseS4_Click(object sender, EventArgs e)
        {
            btnTestCaseS6_Click(null, null);

            baseForm.btnSeamDesignStateRemnantMode_Click(null, null);

            bool cancelDefault = false;

            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 12, 10, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 18, 10, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 15, 6, ref cancelDefault);
            canvasManager.processSeamDesignStateRemnantAnalysisClick(2, 0, 12, 10, ref cancelDefault);
        }

        private void btnTestCaseS6_Click(object sender, EventArgs e)
        {

            SystemState.DesignState = DesignState.Area;
      //      baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.CurrentPage.DrawingScaleInInches = 50;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 11, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 11, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 6, 6, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 11, ref cancelDefault);


            baseForm.BtnSeamDesignState_Click(null, null);
        }

        private void btnTestCase11_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
      //      baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
           
            baseForm.BtnSeamDesignState_Click(null, null);

            baseForm.BtnSeamDesignStateSubdivisionMode_Click(null, null);

            canvasManager.SelectAreaForSubdivision(6, 6);

            canvasManager.ProcessSeamDesignStateClick(1, 0, 4, 4, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 8, 8, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 4, 8, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 4, 4, ref cancelDefault);

        }

        private void btnTestCase12_Click(object sender, EventArgs e)
        {
            SystemState.DesignState = DesignState.Area;
        //    baseForm.AreaMode = AreaMode.Layout;

            bool cancelDefault = false;

            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 2, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 10, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 10, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 2, 2, ref cancelDefault);

            baseForm.BtnLayoutAreaTakeout_Click(null, null);


            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 4, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 8, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 8, ref cancelDefault);
            canvasManager.ProcessAreaDesignStateClick(2, 0, 4, 4, ref cancelDefault);

            baseForm.BtnSeamDesignState_Click(null, null);

            baseForm.BtnSeamDesignStateSubdivisionMode_Click(null, null);

            canvasManager.SelectAreaForSubdivision(3, 3);

            canvasManager.ProcessSeamDesignStateClick(1, 0, 5, 3, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 7, 3, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 7, 6, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 5, 6, ref cancelDefault);
            canvasManager.ProcessSeamDesignStateClick(1, 0, 5, 3, ref cancelDefault);
        }

        #region Cursor Management

        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
        }

        public bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }

        public void AddToCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Add(this);
        }

        public void RemoveFromCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Remove(this);
        }
        public bool IsTopMost { get; set; } = false;

        #endregion

        private void btnRecombinationTestCase1_Click(object sender, EventArgs e)
        {
            List<Coordinate> figure1Coordinates = new List<Coordinate>()
            {
                new Coordinate(0,0)
                ,new Coordinate(17,0)
                ,new Coordinate(24,8)
                ,new Coordinate(17,16)
                ,new Coordinate(24,24)
                ,new Coordinate(8,24)
                ,new Coordinate(0,16)
                ,new Coordinate(0,0)
            };

            List<Coordinate> figure2Coordinates = new List<Coordinate>()
            {
                new Coordinate(0,0)
                ,new Coordinate(13,0)
                ,new Coordinate(13,3)
                ,new Coordinate(4,3)
                ,new Coordinate(0,0)
            };

            List<Coordinate> figure3Coordinates = new List<Coordinate>()
            {
                new Coordinate(0,0)
                ,new Coordinate(7,0)
                ,new Coordinate(7,4)
                ,new Coordinate(4,4)
                ,new Coordinate(0,0)
            };

            List<Coordinate> figure4Coordinates = new List<Coordinate>()
            {
                new Coordinate(4,0)
                ,new Coordinate(11,0)
                ,new Coordinate(11,5)
                ,new Coordinate(0,5)
                ,new Coordinate(0,4)
                ,new Coordinate(4,0)
            };

            List<Coordinate> figure5Coordinates = new List<Coordinate>()
            {
                new Coordinate(1,0)
                ,new Coordinate(5,0)
                ,new Coordinate(5,4)
                ,new Coordinate(9,7)
                ,new Coordinate(2,7)
                ,new Coordinate(0,2)
                ,new Coordinate(1,0)
            };

            List<Coordinate> figure6Coordinates = new List<Coordinate>()
            {
                new Coordinate(0,0)
                ,new Coordinate(3,0)
                ,new Coordinate(3,4)
                ,new Coordinate(2,4)
                ,new Coordinate(0,3)
                ,new Coordinate(0,0)
            };

            SystemState.DesignState = DesignState.Area;
            
            bool cancelDefault = false;

            double figure1OffsetX = 1;
            double figure1OffsetY = 11;

            foreach (Coordinate coordinate in figure1Coordinates)
            {
                canvasManager.ProcessAreaDesignStateClick(
                    2
                    ,0
                    ,0.25 * coordinate.X + figure1OffsetX
                    , figure1OffsetY - 0.25 * coordinate.Y
                    ,ref cancelDefault);
            }

            double figure2OffsetX = 9;
            double figure2OffsetY = 11;

            foreach (Coordinate coordinate in figure2Coordinates)
            {
                canvasManager.ProcessAreaDesignStateClick(
                    2
                    , 0
                    , 0.25 * coordinate.X + figure2OffsetX
                    , figure2OffsetY - 0.25 * coordinate.Y
                    , ref cancelDefault);
            }

            double figure3OffsetX = 10.25;
            double figure3OffsetY = 9.75;

            foreach (Coordinate coordinate in figure3Coordinates)
            {
                canvasManager.ProcessAreaDesignStateClick(
                    2
                    , 0
                    , 0.25 * coordinate.X + figure3OffsetX
                    , figure3OffsetY - 0.25 * coordinate.Y
                    , ref cancelDefault);
            }

            double figure4OffsetX = 9;
            double figure4OffsetY = 8.25;

            foreach (Coordinate coordinate in figure4Coordinates)
            {
                canvasManager.ProcessAreaDesignStateClick(
                    2
                    , 0
                    , 0.25 * coordinate.X + figure4OffsetX
                    , figure4OffsetY - 0.25 * coordinate.Y
                    , ref cancelDefault);
            }

            double figure5OffsetX = 8.75;
            double figure5OffsetY = 6.5;

            foreach (Coordinate coordinate in figure5Coordinates)
            {
                canvasManager.ProcessAreaDesignStateClick(
                    2
                    , 0
                    , 0.25 * coordinate.X + figure5OffsetX
                    , figure5OffsetY - 0.25 * coordinate.Y
                    , ref cancelDefault);
            }

            double figure6OffsetX = 10;
            double figure6OffsetY = 4.25;

            foreach (Coordinate coordinate in figure6Coordinates)
            {
                canvasManager.ProcessAreaDesignStateClick(
                    2
                    , 0
                    , 0.25 * coordinate.X + figure6OffsetX
                    , figure6OffsetY - 0.25 * coordinate.Y
                    , ref cancelDefault);
            }
        }

        private void btnConsistencyCheck_Click(object sender, EventArgs e)
        {
            this.txbInconsistencies.Text = string.Empty;

            ConsistencyChecker consistencyChecker = new ConsistencyChecker(window, currentPage, page);

            List<string> inconsistencies = consistencyChecker.GenerateConsistencyErrors();

            if (inconsistencies.Count <= 0)
            {
                MessageBox.Show("No inconsistencies found.");

                return;
            }

            this.txbInconsistencies.Text = string.Join("\r\n", inconsistencies);
        }
    }
}
