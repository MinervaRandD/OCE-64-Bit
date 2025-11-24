#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: EmbeddedCut.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace MaterialsLayout
{
    using System;
  

    using Geometry;

    [Serializable]
    public class EmbeddedCut
    {
        // An embedded cut is always rectangular, hence only need two coordinates to describe.

        public Rectangle CutRectangle { get; set; }

        public EmbeddedCut() { }

        public EmbeddedCut(Coordinate upperLeftCorner, Coordinate lowerRghtCorner)
        {
            CutRectangle = new Rectangle(upperLeftCorner, lowerRghtCorner);
        }

        public EmbeddedCut(Rectangle rectangle)
        {
            CutRectangle = rectangle.Clone();
        }

        internal void Rotate(double theta)
        {
            CutRectangle.Rotate(theta);
        }

        internal void Translate(Coordinate translateCoord)
        {
            CutRectangle.Translate(translateCoord);
        }

        public void Transform(Coordinate translateCoord, double theta)
        {
            Translate(translateCoord);
            Rotate(theta);
        }

        public bool IsRotated { get; set; }

        public double CutAngle { get; set; }
        public Coordinate CutOffset { get; internal set; }

        internal EmbeddedCut Clone()
        {
            EmbeddedCut clonedEmbededCut = new EmbeddedCut(this.CutRectangle);

            return clonedEmbededCut;
        }
    }
}
