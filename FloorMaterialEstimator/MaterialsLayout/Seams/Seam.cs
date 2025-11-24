#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: Seam.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Geometry;
    using Utilities;

    [Serializable]
    public class Seam: DirectedLine
    {
        public int Line1Index;
        public int Line2Index;

        public SeamType SeamType { get; set; }

        public bool IsHideable { get; set; }

        public int ID { get; set; }

        public ICoordinatedListElement GetSubElement
        {
            get
            {
                return null;
            }
        }

        public static int SeamIndexCounter = -1;

        //public Seam(DirectedLine line, int line1Index, int line2Index) : base(line.Coord1, line.Coord2)
        //{
        //    this.Line1Index = line1Index;
        //    this.Line2Index = line2Index;

        //    ID = SeamIndexCounter--;
        //}

        public Seam(DirectedLine line, SeamType seamType = SeamType.Basic, bool isHideable = false) : base(line.Coord1, line.Coord2)
        {
            SeamType = seamType;

            IsHideable = isHideable;
        }

        public Seam(Coordinate coord1, Coordinate coord2, SeamType seamType = SeamType.Basic, bool isHideable = false) : base(coord1, coord2)
        {
            ID = SeamIndexCounter--;

            SeamType = seamType;

            IsHideable = isHideable;
        }

        public DirectedLine ToDirectedLine()
        {
            return new DirectedLine(this.Coord1, this.Coord2);
        }

        public Seam Clone()
        {

            Seam clonedSeam = new Seam(Coord1, Coord2);

            return clonedSeam;
        }
    }
}
