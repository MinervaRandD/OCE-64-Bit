#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: RemnantCut.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System;


    using Geometry;

    [Serializable]
    public class RemnantCut
    {
        public Coordinate UpperRght;

        public Coordinate LowerRght;

        public Coordinate UpperLeft;

        public Coordinate LowerLeft;

        public RemnantCut() { }

        public RemnantCut(
            Coordinate upperRght
            ,Coordinate lowerRght
            ,Coordinate upperLeft
            ,Coordinate lowerLeft)
        {
            UpperRght = upperRght;
            LowerRght = lowerRght;
            UpperLeft = upperLeft;
            LowerLeft = lowerLeft;
        }

        internal void Rotate(double[,] rotationMatrix)
        {
            UpperRght.Rotate(rotationMatrix);
            LowerRght.Rotate(rotationMatrix);
            UpperLeft.Rotate(rotationMatrix);
            LowerLeft.Rotate(rotationMatrix);
        }

        internal void Translate(Coordinate translateCoord)
        {
            UpperRght += translateCoord;
            LowerRght += translateCoord;
            UpperLeft += translateCoord;
            LowerLeft += translateCoord;
        }

        public void Transform(Coordinate translateCoord, double[,] rotationMatrix)
        {
            Translate(translateCoord);
            Rotate(rotationMatrix);
        }

        public bool IsRotated { get; set; }

        public double CutAngle { get; set; }
        public Coordinate CutOffset { get; internal set; }

        internal RemnantCut Clone()
        {
            RemnantCut clonedEmbededCut = new RemnantCut(this.UpperRght, this.LowerRght, this.UpperLeft, this.LowerRght);

            return clonedEmbededCut;
        }
    }
}
