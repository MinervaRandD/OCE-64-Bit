#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: LineRole.cs. Project: Graphics. Created: 6/10/2024         */
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
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
namespace Graphics
{
      /// <summary>
      /// The specific role that a line plays.
      /// </summary>
    public enum LineRole
    {
        Unknown            = 0
        ,SingleLine        = 1    // Single line created in line mode
        ,AssociatedLine    = 2    // A line occuring in line mode that is the result of a layout  area drawn in area mode
        ,ExternalPerimeter = 3    // External perimeter line of a normal layout area
        ,InternalPerimeter = 4    // Internal perimeter line of a normal layout area
        ,Seam              = 5    // Seam line
        ,NullPerimeter     = 6    // Perimeter of a border area
        ,PatternLine       = 7
    }
}
