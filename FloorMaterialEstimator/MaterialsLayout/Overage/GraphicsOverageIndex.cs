#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsOverageIndex.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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


namespace MaterialsLayout
{
    using Geometry;
    using Graphics;
    using SettingsLib;
    using System;
    using System.Drawing;
    using Utilities;
    using Globals;
    /// <summary>
    /// This case implements the overage index tag that gets put in the display of the overage in seam mode.
    /// It is a circle with a lower case text string to display the actual value.
    /// </summary>
    public class GraphicsOverageIndex : GraphicCircleTag, IGraphicsShape
    {
        #region Indexing

        /// <summary>
        /// OverageIndex is the integer value of the overage tag. It gets displayed as a lower case string indexed from 1.
        /// </summary>
        public uint OverageIndex
        {
            get
            {
                return tagIndexInt;
            }

            set
            {
                if (tagIndexInt == value)
                {
                    return;
                }

                tagIndexInt = value;

               // DrawOverageIndexText(); // The index value may be updated within the code, so we redraw it here if necessary.
            }
        }

        #endregion

        #region Properties

        public GraphicsOverage GraphicsOverage { get; set; } = null;

        public Coordinate Location
        {
            get
            {
                return this.Center;
            }

            set
            {
                this.Center = value;
            }
        }

        #endregion

        #region Constructors and Cloners

        public GraphicsOverageIndex(
            GraphicsOverage parentOverage
            , GraphicsWindow window
            , GraphicsPage page
            , GraphicsLayerBase graphicsLayer
            , string guid
            , Coordinate center
            , double radius): base(null, window, page, graphicsLayer, guid, center, radius, Color.Blue)
        {
            if (guid is null)
            {
                Guid = GuidMaintenance.GenerateGuid();
            }

            else
            {
                Guid = guid;
            }

            GraphicsOverage = parentOverage;

            base.ParentObject = this;

            ShapeType |= ShapeType.OverageIndex | ShapeType.CircleTag;
        }

        #endregion

        #region Deleters and Destructors

        public new void Delete()
        {
            Overage.RemoveIndex(OverageIndex);

            Page.RemoveFromPageShapeDict(Guid);

            if (Utilities.IsNotNull(Shape))
            {
                Shape.Delete();
            }

            base.Delete();
        }

        #endregion

        #region Drawers

        public void DrawOverageIndexText()
        { 
            if (Utilities.IsNotNull(Shape))
            {
                Shape.SetShapeText(Utilities.IndexToLowerCaseString(OverageIndex), GlobalSettings.OverageIndexFontColor, GlobalSettings.OverageIndexFontInPts);

                this.Window?.DeselectAll();
            }
        }

        public new void Draw()
        {
            DrawCircleTag();

            Shape.ParentObject = this;

            Shape.SetFillOpacity(Shape, 0.0);

            Shape.SetShapeData("[OverIndex]", "OverIndex", Guid);

            ShapeType |= ShapeType.OverageIndex | ShapeType.CircleTag;

            this.Window?.DeselectAll();
        }

        #endregion

        public void Select()
        {
            if (Shape is null)
            {
                return;
            }

            Shape.SetLineWidth(3);

            Shape.SetLineColor(Color.Red);
        }

        public void Deselect()
        {
            Shape.SetLineWidth(1);
            Shape.SetLineColor(Color.Red);
        }

        internal void SetVisibility(bool visible)
        {
            if (this.Shape is null)
            {
                return;
            }

            VisioInterop.SetShapeLineVisibility(Shape, visible);


            // Kludge. Can't get text to go away otherwise :(

            if (visible)
            {
                Shape.SetShapeText(Utilities.IndexToLowerCaseString(OverageIndex), GlobalSettings.OverageIndexFontColor, GlobalSettings.OverageIndexFontInPts);

                this.Window?.DeselectAll();
            }

            else
            {
                Shape.SetShapeText("", Color.White, 0);

                this.Window?.DeselectAll();
            }
        }
    }
}
