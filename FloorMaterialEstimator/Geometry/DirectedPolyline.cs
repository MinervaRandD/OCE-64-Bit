#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: DirectedPolyline.cs. Project: Geometry. Created: 6/10/2024                                    */
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


namespace Geometry
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Utilities;

    public class DirectedPolyline: List<DirectedLine>
    {
        public DirectedPolyline() { }
       
        public DirectedPolyline(List<DirectedLine> lineList)
        {
            base.AddRange(lineList);

            Debug.Assert(IsValid());
        }

        public void Translate(Coordinate translateCoord)
        {
            base.ForEach(l => l.Translate(translateCoord));
        }

        public void Rotate(double[,] rotationMatrix)
        {
            base.ForEach(l => l.Rotate(rotationMatrix));
        }

        public void Transform(Coordinate translateCoord, double[,] rotationMatrix)
        {
            Translate(translateCoord);
            Rotate(rotationMatrix);
        }

        public void InverseTransform(double[,] inverseRotationMatrix, Coordinate inverseTranslateCoord)
        {
            Rotate(inverseRotationMatrix);
            Translate(inverseTranslateCoord);
        }

        public DirectedPolyline Clone()
        {
            List<DirectedLine> clonedLineList = new List<DirectedLine>();

            base.ForEach(l => clonedLineList.Add(l.Clone()));

            return new DirectedPolyline(clonedLineList);
        }

        public bool IsValid()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                if (base[i].Coord2 != base[i+1].Coord1)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsClosed()
        {
            if (!IsValid())
            {
                return false;
            }

            return base[Count - 1].Coord2 == base[0].Coord1;
        }


        public double Orientation()
        {
            if (Count <= 1)
            {
                return 0;
            }

            List<Tuple<double, double>> coordinateList = new List<Tuple<double, double>>();

            coordinateList.Add(new Tuple<double, double>(this[0].Coord1.X, this[0].Coord1.Y));

            for (int i = 0; i < Count; i++)
            {
                coordinateList.Add(new Tuple<double, double>(this[i].Coord2.X, this[i].Coord2.Y));
            }

            return MathUtils.CurveOrientation(coordinateList);
        }
    }
}
