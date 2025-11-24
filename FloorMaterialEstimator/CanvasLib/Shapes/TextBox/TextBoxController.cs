//-------------------------------------------------------------------------------//
// <copyright file="ScaleRulerManager.cs" company="Bruun Estimating, LLC">       // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using CanvasLib.Scale_Line;

namespace CanvasLib.TextBox
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
   
    using Graphics;
    using Geometry;
    using Utilities;
    using Globals;
  
    using System.Diagnostics;
    using System.Resources;
    using System.Reflection;

    public class TextBoxDrawController
    {

        public Coordinate TxtBxFrstCoord = Coordinate.NullCoordinate;
        public Coordinate TxtBxScndCoord = Coordinate.NullCoordinate;

        public TextBoxDrawState textboxDrawState = TextBoxDrawState.NotActive;

        private GraphicsPage page;

        GraphicsWindow window;

        public TextBoxDrawController(
            GraphicsWindow window
            , GraphicsPage page
        )
        {
            this.window = window;
            this.page = page;

        }

        public void InitiateTextBoxDraw(Form baseForm, bool seamedAreasExist)
        {

        }


        public void TextBoxDrawingModeClick(int button, double x, double y)
        {
            if (button != 2)
            {
                return;
            }

            switch (textboxDrawState)
            {
                case TextBoxDrawState.TextBoxDrawInitiated:

                    return;

                case TextBoxDrawState.FrstPointSelected:


                    return;

                case TextBoxDrawState.ScndPointSelected:



                    return;
            }
        }

    }
}
