#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: OversToUndrsMapper.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    
    using OversUnders;
   // using OversUndersLib;
    using SettingsLib;
    using Utilities;

    public class OversToUndrsMapper
    {
        public List<MaterialArea> OversList = new List<MaterialArea>();
        public List<MaterialArea> UndrsList = new List<MaterialArea>();

        OversUnders oversUndrs;

        public OversToUndrsMapper(List<MaterialArea> oversList, List<MaterialArea> undrsList)
        {
            OversList = oversList;
            UndrsList = undrsList;

            oversUndrs = new OversUnders(this.OversList, this.UndrsList);
        }

        public int GenerateWasteEstimate(double rollWidthInInches)
        {
            double totalFillLength = 0;
            double wasteFactor = 0;

           // OversUndersProcessor.GetOUsOutput(OversList, UndrsList, rollWidthInInches, out totalFillLength, out wasteFactor);

            int totlFill = oversUndrs.GenerateWasteEstimate((int) Math.Round(rollWidthInInches));

            return totlFill;

            //return (int) totalFillLength;
        }
    }
}
