#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsLayerStyle.cs. Project: Graphics. Created: 11/14/2024         */
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
    public enum GraphicsLayerStyle
    {
        // Static graphics layers are not project specific and need only be set up once (and potentially cleared with a new project).
        // Dynamic graphics layers are potentially project dependent so they have to be deleted and reconstructed when a new project is
        // created.

        Unknown = 0,
        Static = 1, // For layers that do not depend on the project.
        Dynamic = 2 // For layers that are project dependent
    }
}
