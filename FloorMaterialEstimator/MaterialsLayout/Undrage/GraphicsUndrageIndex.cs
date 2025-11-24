#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsUndrageIndex.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using FinishesLib;
    using Geometry;
    using Graphics;
    using SettingsLib;
    using System.Data;
    using System.Drawing;
    using Utilities;

    public class GraphicsUndrageIndex : GraphicCircleTag, IGraphicsShape
    {
        #region Indexing
        public uint UndrageIndex
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
            }
        }


        #endregion

        #region Properties

        public GraphicsUndrage GraphicsUndrage { get; set; } = null;

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

        #region Constructors

        public GraphicsUndrageIndex(
            GraphicsUndrage parentUndrage
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

            GraphicsUndrage = parentUndrage;

            base.ParentObject = this;

            ShapeType |= ShapeType.UndrageIndex | ShapeType.CircleTag;
        }

        #endregion

        #region Deleters and Destructors

        public new void Delete()
        {
            Undrage.RemoveIndex(UndrageIndex);

            Page.RemoveFromPageShapeDict(Guid);

            if (Utilities.IsNotNull(Shape))
            {
                Shape.Delete();
            }

            base.Delete();
        }

        #endregion

        public void DrawUndrageIndexText()
        {
            if (Utilities.IsNotNull(Shape))
            {
                Shape.SetShapeText(Utilities.IndexToUpperCaseString(UndrageIndex), Color.Blue, GlobalSettings.UnderageIndexFontInPts);

                this.Window?.DeselectAll();
            }
        }

        public new void Draw()
        {
            DrawCircleTag();

            Shape.ParentObject = this;

            Shape.SetFillOpacity(Shape, 0.0);

            Shape.SetShapeData("[UndrIndex]", "UndrIndex", Guid);

            ShapeType |= ShapeType.UndrageIndex | ShapeType.CircleTag;

            this.Window?.DeselectAll();
        }


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
                Shape.SetShapeText(Utilities.IndexToUpperCaseString(UndrageIndex), Color.Red, GlobalSettings.UnderageIndexFontInPts);

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
