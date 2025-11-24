//-------------------------------------------------------------------------------//
// <copyright file="DrawingImporter.cs" company="Bruun Estimating, LLC">         // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
// <copyright file="DrawingImporter.cs" company="Bruun Estimating, LLC">         //
//                                                                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    using Graphics;
    using Microsoft.Office.Interop.Visio;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    using Path = System.IO.Path;

    class DrawingImporter
    {
        GraphicsPage page = null;

        GraphicsWindow window = null;

        //FloorMaterialEstimatorBaseForm baseForm;

        public DrawingImporter(GraphicsWindow window, GraphicsPage page)
        {
         
            this.window = window;

            this.page = page;

            //this.baseForm = baseForm;
        }

        public string drawingName = string.Empty;

        public GraphicShape ImportDrawing(string sDrawingFolderPath, bool eraseCurrentContents = false, Visio.Application vsoApplication = null)
        {
            try
            {
                page.DrawingInBytes = File.ReadAllBytes(sDrawingFolderPath);

                //vsoApplication.Documents.Open(sDrawingFolderPath);

                GraphicShape drawingShape = new GraphicShape(null, window, page, page.VisioPage.Import(sDrawingFolderPath), ShapeType.Image);

                VisioInterop.SetShapeData(drawingShape, Path.GetFileName(sDrawingFolderPath), "Image", drawingShape.Guid);

                return drawingShape;

            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("Visio is unable to complete importing"))
                {
                    string fileName = Path.GetFileName(sDrawingFolderPath);

                    MessageBox.Show("File '" + fileName + "' is too large to import. It can be reduced in size by reading it into Visio and writing it out again in the original format.");
                }
                return null;
            }
        }

        public GraphicShape ImportDrawing(byte[] drawing)
        {
            if (drawing is null)
            {
                return null;
            }

            // string tempFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

            string tempFileName = @"C:\OCEOperatingData\Workspace\ImportDrawing.png";

            
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }

            File.WriteAllBytes(tempFileName, drawing);


            //Visio.Shape visioShape = Page.VisioPage.InsertFromFile(tempFileName, (short)0);

            Visio.Shape visioShape = page.VisioPage.Import(tempFileName);

            File.Delete(tempFileName);

            return new GraphicShape(null, window, page, visioShape, ShapeType.Image);
        }
    }
}
