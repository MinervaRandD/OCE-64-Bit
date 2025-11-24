using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using FloorMaterialEstimator.CanvasManager;
using Utilities;
using System.Windows.Forms;
using TracerLib;
using AxMicrosoft.Office.Interop.VisOcx;

namespace FloorMaterialEstimator.Base_Form.Floor_Materials_Base_Form
{
    class MSStencilHelper
    {
        private Dictionary<Color, List<MeasuringStickStencil>> allMeasuringStickStencils = null;
        private int selectedMeasuringStickStencil = -1;
        private Color selectedMeasuringStickStencilColor = Color.White;

        public List<MeasuringStickStencil> MeasuringStickStencils
        {
            get { return this.allMeasuringStickStencils[this.selectedMeasuringStickStencilColor]; }
        }

        public List<Color> StencilColors
        {
            get { return new List<Color>(this.allMeasuringStickStencils.Keys); }
        }

        public int SelectecdStencilIndex
        {
            get { return this.selectedMeasuringStickStencil; }
        }

        public MeasuringStickStencil SelectecdStencil
        {
            get { return this.allMeasuringStickStencils[this.selectedMeasuringStickStencilColor][this.selectedMeasuringStickStencil]; }
        }

        public void Init(AxDrawingControl axDrawingControl) 
        {
            LoadMeasuringStickStencils(axDrawingControl);
        }

        private void LoadMeasuringStickStencils(AxDrawingControl axDrawingControl)
        {
            Color REDMEASURINGSTICKCOLOR = Color.FromArgb(0xFF, 0x0, 0x0);
            Color BLUEMEASURINGSTICKCOLOR = Color.FromArgb(0x0, 0x70, 0xC0);
            Color GREENMEASURINGSTICKCOLOR = Color.FromArgb(0x0, 0x96, 0x44);
            Color BLACKMEASURINGSTICKCOLOR = Color.FromArgb(0x0, 0x0, 0x0);
            string[] MEASURING_STICK_RESOURCE_NAMES = { 
                "MeasuringStick60.vss", "MeasuringStick66.vss", "MeasuringStick12.vss", "MeasuringStick125.vss",
                "MeasuringStick60-blue.vss", "MeasuringStick66-blue.vss", "MeasuringStick12-blue.vss", "MeasuringStick125-blue.vss",
                "MeasuringStick60-green.vss", "MeasuringStick66-green.vss", "MeasuringStick12-green.vss", "MeasuringStick125-green.vss",
                "MeasuringStick60-black.vss", "MeasuringStick66-black.vss", "MeasuringStick12-black.vss", "MeasuringStick125-black.vss"};
            string[] MEASURING_STICK_DISPLAY_NAMES = { 
                "6\'-0\"", "6\'-6\"", "12\'-0\"", "12\'-6\"",
                "6\'-0\"", "6\'-6\"", "12\'-0\"", "12\'-6\"",
                "6\'-0\"", "6\'-6\"", "12\'-0\"", "12\'-6\"",
                "6\'-0\"", "6\'-6\"", "12\'-0\"", "12\'-6\""};
            Color[] MEASURING_STICK_COLORS = { 
                REDMEASURINGSTICKCOLOR, REDMEASURINGSTICKCOLOR, REDMEASURINGSTICKCOLOR, REDMEASURINGSTICKCOLOR,
                BLUEMEASURINGSTICKCOLOR, BLUEMEASURINGSTICKCOLOR, BLUEMEASURINGSTICKCOLOR, BLUEMEASURINGSTICKCOLOR,
                GREENMEASURINGSTICKCOLOR, GREENMEASURINGSTICKCOLOR, GREENMEASURINGSTICKCOLOR, GREENMEASURINGSTICKCOLOR,
                BLACKMEASURINGSTICKCOLOR, BLACKMEASURINGSTICKCOLOR, BLACKMEASURINGSTICKCOLOR, BLACKMEASURINGSTICKCOLOR };

            this.allMeasuringStickStencils = new Dictionary<Color, List<MeasuringStickStencil>>();

            for (int i = 0; i < MEASURING_STICK_RESOURCE_NAMES.Length; ++i)
            {
                ExtractFileResource(MEASURING_STICK_RESOURCE_NAMES[i]);
                String measuringStickStencilPath = Application.StartupPath + "\\" + MEASURING_STICK_RESOURCE_NAMES[i];

                List<MeasuringStickStencil> measuringStickStencils = null;
                if (!allMeasuringStickStencils.TryGetValue(MEASURING_STICK_COLORS[i], out measuringStickStencils)) 
                {
                    measuringStickStencils = new List<MeasuringStickStencil>();
                    allMeasuringStickStencils.Add(MEASURING_STICK_COLORS[i], measuringStickStencils);
                }

                measuringStickStencils.Add(MeasuringStickStencil.CreateFromFile(MEASURING_STICK_DISPLAY_NAMES[i], MEASURING_STICK_COLORS[i],
                    measuringStickStencilPath, axDrawingControl));
            }

            this.selectedMeasuringStickStencilColor = MEASURING_STICK_COLORS[0];
            this.selectedMeasuringStickStencil = this.allMeasuringStickStencils.Count > 0 ? 0 : -1;
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
                Tracer.TraceGen.TraceException("MSStencilHelper:ExtractFileResource throws an exception.", ex, 1, true);
            }
        }

        public void SelectMeasuringStickStencil(string name)
        {
            this.selectedMeasuringStickStencil = allMeasuringStickStencils[this.selectedMeasuringStickStencilColor].FindIndex(mss => mss.DisplayName == name);
        }

        public void SelectMeasuringStickStencil(Color color)
        {
            this.selectedMeasuringStickStencilColor = color;
        }
    }
}
