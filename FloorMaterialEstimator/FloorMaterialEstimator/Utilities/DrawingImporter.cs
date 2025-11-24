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

namespace FloorMaterialEstimator.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Win32;
    using System.IO;
    using System.Windows.Forms;

    using FloorMaterialEstimator.Models;

    using Visio = Microsoft.Office.Interop.Visio;

    class DrawingImporter
    {
        Page page = null;

        public DrawingImporter(Page page)
        {
            this.page = page;
        }

        public string drawingName = string.Empty;

        public Visio.Shape ImportDrawing(string sDrawingFolderPath = null)
        {
            if (string.IsNullOrEmpty(sDrawingFolderPath))
            {
                sDrawingFolderPath = Utilities.GetBasePlanInitialDirectory();
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = sDrawingFolderPath;

            openFileDialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|BMP flies (*.bmp)|*.bmp";
            openFileDialog.FilterIndex = 1;
            openFileDialog.DefaultExt = "png";
            openFileDialog.FileName = "";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            string drawingFileFullPath = openFileDialog.FileName;

            Utilities.SetBasePlanInitialDirectory(Path.GetDirectoryName(drawingFileFullPath));

            drawingName = Path.GetFileNameWithoutExtension(drawingFileFullPath);

            return page.VisioPage.Import(drawingFileFullPath);

        }

    }
}
