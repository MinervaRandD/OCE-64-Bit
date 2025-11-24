#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: VisioValidations.cs. Project: Graphics. Created: 6/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion


namespace Graphics
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Utilities;

    using Geometry;
    using Graphics;
    using TracerLib;

    using Visio = Microsoft.Office.Interop.Visio;

    using Globals;

    public static class VisioValidations
    {
        public static bool ValidateIShapeParm(IGraphicsShape iShape, string methodName)
        {
            if (SystemState.LoadingExistingProject)
            {
                return true;
            }

            if (iShape == null)
            {
                Tracer.TraceGen.TraceError("Null IShape in call to " + methodName, 1, true);

                return false;
            }

            return ValidateShapeParm((GraphicShape)iShape, methodName);
        }

        public static bool ValidateShapeParm(GraphicShape shape, string methodName)
        {
            if (SystemState.LoadingExistingProject)
            {
                return true;
            }

            if (shape == null)
            {
#if TRACE0
                Tracer.TraceGen.TraceError("Null Shape in call to " + methodName, 1, true);
#endif
                return false;
            }

            Visio.Shape visioShape = shape.VisioShape;

            if (visioShape == null)
            {
#if TRACE0
                Tracer.TraceGen.TraceError("Null visio shape in call to " + methodName, 1, true);
#endif
                return false;
            }

            return true;
        }


        public static bool ValidateWindowParm(GraphicsWindow window, string methodName)
        {
            if (SystemState.LoadingExistingProject)
            {
                return true;
            }

            if (window is null)
            {
                Tracer.TraceGen.TraceError("Null Window in call to " + methodName, 1, true);

                return false;
            }

            Visio.Window visioWindow = window.VisioWindow;

            if (visioWindow is null)
            {
                Tracer.TraceGen.TraceError("Null visio Window in call to " + methodName, 1, true);

                return false;
            }

            return true;
        }

        public static bool ValidatePageParm(GraphicsPage page, string methodName)
        {
            if (SystemState.LoadingExistingProject)
            {
                return true;
            }

            if (page is null)
            {
                Tracer.TraceGen.TraceError("Null Page in call to " + methodName, 1, true);

                return false;
            }

            Visio.Page visioPage = page.VisioPage;

            if (visioPage is null)
            {
                Tracer.TraceGen.TraceError("Null visio Page in call to " + methodName, 1, true);

                return false;
            }

            return true;
        }

        public static bool ValidateLayerParm(GraphicsLayer layer, string methodName)
        {
            if (SystemState.LoadingExistingProject)
            {
                return true;
            }
            if (layer is null)
            {
                Tracer.TraceGen.TraceError("Null layer in call to " + methodName, 1, true);

                return false;
            }

            Visio.Layer visioLayer = layer.visioLayer;

            if (visioLayer is null)
            {
                Tracer.TraceGen.TraceError("Null visio layer in call to " + methodName, 1, true);

                return false;
            }

            return true;
        }

        internal static bool VisioShapeNotDeleted(Visio.Shape visioShape, string methodName)
        {
            if (visioShape is null)
            {
                Tracer.TraceGen.TraceError("Attempt to operate on a deleted shape in " + methodName, 1, true);

                return false;
            }

            if (visioShape.ID > 0)
            {
                return true;
            }

            Tracer.TraceGen.TraceError("Attempt to operate on a deleted shape in " + methodName, 1, true);

            return false;
        }
    }
}
