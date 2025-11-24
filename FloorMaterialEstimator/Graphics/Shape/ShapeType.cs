#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: ShapeType.cs. Project: Graphics. Created: 6/10/2024         */
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

namespace Graphics
{
    public enum ShapeType: ulong
    {
        Unknown                    = 0b0000000000000000000000000001
        , Line                     = 0b0000000000000000000000000010 
        , Circle                   = 0b0000000000000000000000000100
        , Rectangle                = 0b0000000000000000000000001000
        , Polyline                 = 0b0000000000000000000000010000
        , Polygon                  = 0b0000000000000000000000100000
        , LayoutArea               = 0b0000000000000000000001000000
        , Legend                   = 0b0000000000000000000010000000
        , TextBox                  = 0b0000000000000000000100000000
        , Image                    = 0b0000000000000000001000000000
        , CutIndex                 = 0b0000000000000000010000000000
        , OverageIndex             = 0b0000000000000000100000000000
        , UndrageIndex             = 0b0000000000000001000000000000
        , MeasuringStick           = 0b0000000000000010000000000000
        , SeamingTool              = 0b0000000000000100000000000000
        , PlaceMarker              = 0b0000000000001000000000000000
        , ExtendedCrosshairs       = 0b0000000000010000000000000000
        , LayoutAreaShape          = 0b0000000000100000000000000000
        , SeamTag                  = 0b0000000001000000000000000000
        , Overage                  = 0b0000000010000000000000000000
        , CircleTag                = 0b0000000100000000000000000000
        , AreaLegendCounterElement = 0b0000001000000000000000000000
        , CounterShapeElement      = 0b0000010000000000000000000000
        ,CopyMarker                = 0b0000100000000000000000000000
        ,LockIcon                  = 0b000100000000000000000000000
    }
}
