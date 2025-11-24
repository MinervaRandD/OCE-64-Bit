#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: OverageLineSegment.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Utilities;

    public class OverageLineSegment: DirectedLine
    {
        public OverageLineSegment(Coordinate coord1, Coordinate coord2): base(coord1, coord2)
        {
            if (coord1 > coord2)
            {
                Utilities.Swap(ref coord1, ref coord2);
            }
        }
    }
}
