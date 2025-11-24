    
namespace TestDriverMeasuringStick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Input;

    using Geometry;
    using Graphics;
    using CanvasLib.MeasuringStick;

    using Visio = Microsoft.Office.Interop.Visio;
    using Utilities;
    using System.Reflection;
    using System.IO;
    using TracerLib;

    public partial class MeasuringStickForm : Form, IMessageFilter
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        List<GraphicsDirectedLine> directedLineList = new List<GraphicsDirectedLine>();

        public GraphicsPage Page;

        public GraphicsWindow Window { get; set; }


        private List<GraphicsDirectedPolygon> polygonList = new List<GraphicsDirectedPolygon>();

        private const double scale = 12;

        private GraphicsLayer graphicsLayer;

        private MeasuringStick measuringStick;

        private Visio.Document rulerStencil { get; set; }

        public MeasuringStickForm()
        {
            InitializeComponent();

            Tracer.TraceGen = new Tracer((TracerLib.TraceLevel)0xff, @"C:\Temp\TraceLog.txt", true);

            Visio.UIObject uiObject = this.axDrawingControl.Document.Application.BuiltInMenus;
            int accelTableNumber = uiObject.AccelTables.Count;

            for (int i = 0; i < accelTableNumber; ++ i)
            {
                Visio.AccelTable accelTable = uiObject.AccelTables[i];
                int accelItemNumber = accelTable.AccelItems.Count;

                for (int j = 0; j < accelItemNumber; ++j) 
                {
                    Visio.AccelItem accelItem = accelTable.AccelItems[j];
                    if (accelItem.Key == 0xA2/*Control*/) 
                    {
                        accelItem.Delete();
                    }
                }
            }

            this.axDrawingControl.Size = new Size(this.Width - 2, this.Height - 66);
            this.axDrawingControl.Location = new Point(1, 64);

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 12 * 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 12 * 20;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            Page.VisioPage.AutoSize = true;

            GraphicsWindow window = new GraphicsWindow(VsoWindow);
            GraphicsPage page = new GraphicsPage(window, VsoPage);

            DirectedLine directedLine = new DirectedLine(new Coordinate(4, 100), new Coordinate(200, 100));

            GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(window, page, directedLine, LineRole.SingleLine);

            graphicsDirectedLine.Draw(Color.Red, 4);

            graphicsLayer = new GraphicsLayer(window, page, "abc", GraphicsLayerType.Dynamic);

            graphicsLayer.AddShape(graphicsDirectedLine.Shape, 1);

            ExtractFileResource("MeasuringStick.vss");

            String measuringStickPath = Application.StartupPath + "\\MeasuringStick.vss";

            this.rulerStencil = this.axDrawingControl.Document.Application.Documents.OpenEx(measuringStickPath, (short)Visio.VisOpenSaveArgs.visOpenHidden);

            this.measuringStick = new MeasuringStick(Window, Page);

            this.measuringStick.RulerStencil = this.rulerStencil;

            this.measuringStick.Show();

            Shape measuringStickShape = this.measuringStick.Shape;

            Visio.Shape visioShape = measuringStickShape.VisioShape;

            this.SizeChanged += MeasuringStickForm_SizeChanged;

            // technique to prevent copy on drag (when Ctrl key is pressed)
            axDrawingControl.Document.ShapeAdded += new Visio.EDocument_ShapeAddedEventHandler(ShapeAddedEventHandler);
        }

        public void ShapeAddedEventHandler(Visio.Shape shape) 
        {
            if (MeasuringStick.IsMeasuringStickShape(shape)) 
            {
                shape.Delete();
                axDrawingControl.Document.PurgeUndo();
            }
        }

        private void MeasuringStickForm_SizeChanged(object sender, EventArgs e)
        {
            this.axDrawingControl.Size = new Size(this.Width - 2, this.Height - 65);
            this.axDrawingControl.Location = new Point(1, 64);
        }

        public bool PreFilterMessage(ref Message m)
        {
            this.lblMessage.Text = m.Msg.ToString();

            this.lblWParam.Text = m.WParam.ToString();

            if (m.Msg == (int)WindowsMessage.WM_RBUTTONUP)
            {
                // Check to see if the measuring stick is selected

                if (this.measuringStick.IsVisible)
                {
                    if (this.measuringStick.IsSelected())
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        private void ExtractFileResource(string fileName)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string[] resourceNames = assembly.GetManifestResourceNames();
                string resourceName = Array.Find(resourceNames, str => str.EndsWith(fileName));
                Stream stream = assembly.GetManifestResourceStream(resourceName);
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create));

                // copy the .dll from Assembly to the file system
                byte[] buffer = new byte[2048];
                int bytesRead;
                while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, bytesRead);
                }

                reader.Close();
                writer.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Cannot extract file resource:\n", ex, 1, true);
            }
        }
    }
}
