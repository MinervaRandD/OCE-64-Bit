#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: VisioLineStyle.cs. Project: Graphics. Created: 6/10/2024         */
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public enum VisioLineStyle
    {
        Unknown = 0,
        Solid = 1,             // ______________
        Dashed = 2,            // __  __  __  __
        Dot = 3,               // .  .  .  .  .  .
        DashDot = 4,           // __  .  __  .  __ 
        DashDotDot = 5,
        DashDashDot = 6,
        LongDashShortDash = 7,
        LongDashShortDashShortDate = 8,
        HalfDash = 9,
        HalfDot = 10
    }
}
