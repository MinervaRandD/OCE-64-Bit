#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsLayerType.cs. Project: Graphics. Created: 11/14/2024         */
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
    public enum GraphicsLayerType: ulong
    {
        Unknown                              = 0b0000000000000000000000000000000000000000000,
        AreaMode_AreaDesignStateLayer        = 0b0000000000000000000000000000000000000000001,
        AreaMode_AreaFinishLabelLayer        = 0b0000000000000000000000000000000000000000010,
        AreaMode_AreaPerimeterLayer          = 0b0000000000000000000000000000000000000000100,
        AreaMode_RemnantSeamDesignStateLayer = 0b0000000000000000000000000000000000000001000,
        AreaMode_SeamDesignStateLayer        = 0b0000000000000000000000000000000000000010000,
        AreaMode_SeamStateLockLayer          = 0b0000000000000000000000000000000000000100000,
        CutsIndexLayer                       = 0b0000000000000000000000000000000000001000000,
        CutsLayer                            = 0b0000000000000000000000000000000000010000000,
        EmbdOverLayer                        = 0b0000000000000000000000000000000000100000000,
        LineMode_LineDesignStateLayer        = 0b0000000000000000000000000000000001000000000,
        ManualSeamsAllLayer                  = 0b0000000000000000000000000000000010000000000,
        NormalSeamsLayer                     = 0b0000000000000000000000000000000100000000000,
        NormalSeamsUnhideableLayer           = 0b0000000000000000000000000000001000000000000,
        OversLayer                           = 0b0000000000000000000000000000010000000000000,
        UndrsLayer                           = 0b0000000000000000000000000000100000000000000,
        EmbdCutsLayer                        = 0b0000000000000000000000000001000000000000000,
        GraphicsPage_TakeoutLayer            = 0b0000000000000000000000000010000000000000000,
        AreaMode_GlobalLayer                 = 0b0000000000000000000000000100000000000000000,
        LineMode_GlobalLayer                 = 0b0000000000000000000000001000000000000000000,
        SeamMode_GlobalLayer                 = 0b0000000000000000000000010000000000000000000,
        DrawingLayer                         = 0b0000000000000000000000100000000000000000000,
        AreaMode_LegandLayer                 = 0b0000000000000000000001000000000000000000000,
        LineMode_LegandLayer                 = 0b0000000000000000000010000000000000000000000,
        ExtendedCrossHairs                   = 0b0000000000000000000100000000000000000000000,
        InvisibilityWorkspace                = 0b0000000000000000001000000000000000000000000,
        LineMode_AreaDesignStateLayer        = 0b0000000000000000010000000000000000000000000,
        LineMode_SeamDesignStateLayer        = 0b0000000000000000100000000000000000000000000,
        LineMode_RemnantSeamDesignStateLayer = 0b0000000000000001000000000000000000000000000,
        SeamMode_AreaDesignStateLayer        = 0b0000000000000010000000000000000000000000000,
        SeamMode_LineDesignStateLayer        = 0b0000000000000100000000000000000000000000000,
        SeamMode_SeamDesignStateLayer        = 0b0000000000001000000000000000000000000000000,
        FieldGuideLayer                      = 0b0000000000010000000000000000000000000000000,
        Counter                              = 0b0000000000100000000000000000000000000000000,
        SeamingTool                          = 0b0000000001000000000000000000000000000000000,
        WorkLayer                            = 0b0000000010000000000000000000000000000000000,
        BoundaryLayer                        = 0b0000000100000000000000000000000000000000000,
        MainLayer                            = 0b0000001000000000000000000000000000000000000,
        MeasuringStick                       = 0b0000010000000000000000000000000000000000000,
        OversIndexLayer                      = 0b0000100000000000000000000000000000000000000,
        UndrsIndexLayer                      = 0b0001000000000000000000000000000000000000000,
        SeamAreaIndexLayer                   = 0b0010000000000000000000000000000000000000000
    }
}
