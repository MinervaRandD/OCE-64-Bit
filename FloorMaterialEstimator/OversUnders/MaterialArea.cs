
namespace OversUnders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using SettingsLib;

    public class MaterialArea: IComparable<MaterialArea>
    {
        public MaterialAreaType MaterialAreaType;

        public int MaterialAreaId;

        public int WidthInInches;
        public int LngthInInches;

        public static int OverMaterialIdGenerator = 1;
        public static int UndrMaterialIdGenerator = -1;

        public List<int> parentMaterialAreaList = new List<int>();

        public int SeamAreaIndex = 0;

        public MaterialArea(MaterialAreaType materialAreaType, int widthInInches, int lngthInInches)
        {
            WidthInInches = widthInInches;
            LngthInInches = lngthInInches;

            MaterialAreaType = materialAreaType;

            if (materialAreaType == MaterialAreaType.Over)
            {
                MaterialAreaId = OverMaterialIdGenerator++;
            }

            else
            {
                MaterialAreaId = UndrMaterialIdGenerator--;
            }
        }


        //public bool EqualsByDimensions(MaterialArea m2)
        //{
        //    return this.WidthInInches == m2.WidthInInches && this.LngthInInches == m2.LngthInInches;
        //}

        //public static bool EqualsByDimensions(MaterialArea m1, MaterialArea m2)
        //{
        //    return m1.WidthInInches == m2.WidthInInches && m1.LngthInInches == m2.LngthInInches;
        //}

        //public List<MaterialArea> OverMinusUndr(MaterialArea undr, bool favorWidths = true)
        //{
        //    Debug.Assert(this.MaterialAreaType == MaterialAreaType.Over);
        //    Debug.Assert(undr.MaterialAreaType == MaterialAreaType.Undr);

        //    List<MaterialArea> returnList = new List<MaterialArea>();

        //    if (this.WidthInInches == undr.WidthInInches)
        //    {
        //        if (this.LngthInInches == undr.LngthInInches)
        //        {

        //        }

        //        else if (this.LngthInInches > undr.LngthInInches)
        //        {
        //            returnList.Add(new MaterialArea(MaterialAreaType.Over, this.WidthInInches, this.LngthInInches - undr.LngthInInches));
        //        }

        //        else
        //        {
        //            returnList.Add(new MaterialArea(MaterialAreaType.Undr, this.WidthInInches, undr.LngthInInches - this.LngthInInches));
        //        }
        //    }

        //    else if (this.WidthInInches > undr.WidthInInches)
        //    {
        //        if (this.LngthInInches == undr.LngthInInches)
        //        {
        //            returnList.Add(new MaterialArea(MaterialAreaType.Over, this.WidthInInches - undr.WidthInInches, this.LngthInInches));
        //        }

        //        else if (this.LngthInInches > undr.LngthInInches)
        //        {
        //            int deltaW = this.WidthInInches - undr.WidthInInches;
        //            int deltaL = this.LngthInInches - undr.LngthInInches;

        //            if (deltaW >= GlobalSettings.MinOverageWidthInInches && deltaL < GlobalSettings.MinOverageLengthInInches)
        //            {
        //                returnList.Add(new MaterialArea(MaterialAreaType.Over, deltaW, this.LngthInInches));
        //            }

        //            if (deltaW < GlobalSettings.MinOverageWidthInInches && deltaL >= GlobalSettings.MinOverageLengthInInches)
        //            {
        //                returnList.Add(new MaterialArea(MaterialAreaType.Over, this.WidthInInches, deltaL));
        //            }

        //            else
        //            {
        //                if (favorWidths)
        //                {
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Over, deltaW, this.LngthInInches));
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Over, undr.WidthInInches, deltaL));
        //                }

        //                else
        //                {
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Over, deltaW, undr.LngthInInches));
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Over, this.WidthInInches, deltaL));
        //                }
        //            }

        //        }

        //        else // this.LngthInInches < undr.LngthInInches
        //        {
        //            returnList.Add(new MaterialArea(MaterialAreaType.Over, this.WidthInInches - undr.WidthInInches, this.LngthInInches));
        //            returnList.Add(new MaterialArea(MaterialAreaType.Undr, undr.WidthInInches, undr.LngthInInches - this.LngthInInches));
        //        }
        //    }

        //    else // this.WidthInInches < undr.WidthInInches
        //    {
        //        if (this.LngthInInches == undr.LngthInInches)
        //        {
        //            returnList.Add(new MaterialArea(MaterialAreaType.Undr, undr.WidthInInches - this.WidthInInches, this.LngthInInches));
        //        }

        //        else if (this.LngthInInches > undr.LngthInInches)
        //        {
        //            returnList.Add(new MaterialArea(MaterialAreaType.Undr, undr.WidthInInches - this.WidthInInches, undr.LngthInInches));
        //            returnList.Add(new MaterialArea(MaterialAreaType.Over, this.WidthInInches, this.LngthInInches - undr.LngthInInches));
        //        }
           
        //        else
        //        {
        //            int deltaW = undr.WidthInInches - this.WidthInInches;
        //            int deltaL = undr.LngthInInches - this.LngthInInches;

        //            if (deltaW >= GlobalSettings.MinUnderageWidthInInches && deltaL < GlobalSettings.MinUnderageLengthInInches)
        //            {
        //                returnList.Add(new MaterialArea(MaterialAreaType.Undr, deltaW, undr.LngthInInches));
        //            }

        //            if (deltaW < GlobalSettings.MinUnderageWidthInInches && deltaL >= GlobalSettings.MinUnderageLengthInInches)
        //            {
        //                returnList.Add(new MaterialArea(MaterialAreaType.Over, undr.WidthInInches, deltaL));
        //            }

        //            else
        //            {
        //                if (favorWidths)
        //                {
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Undr, deltaW, undr.LngthInInches));
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Undr, this.WidthInInches, deltaL));
        //                }

        //                else
        //                {
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Undr, deltaW, this.LngthInInches));
        //                    returnList.Add(new MaterialArea(MaterialAreaType.Undr, undr.WidthInInches, deltaL));
        //                }
        //            }
        //        }
        //    }

        //    return returnList;
        //}

        internal MaterialArea Clone()
        {
            MaterialArea materialArea = new MaterialArea(MaterialAreaType, WidthInInches, LngthInInches);

            materialArea.parentMaterialAreaList.Add(MaterialAreaId);

            return materialArea;
        }

        public int Area()
        {
            return WidthInInches * LngthInInches;
        }

        public override string ToString()
        {
            double lngthInFeet = (double)this.LngthInInches / 12.0;
            double widthInFeet = (double)this.WidthInInches / 12.0;

            string rtrnString = widthInFeet.ToString("#,##0.0") + " X " + lngthInFeet.ToString("#,##0.0");

            if (MaterialAreaType == MaterialAreaType.Over)
            {
                rtrnString = '+' + rtrnString;
            }

            else
            {
                rtrnString = '(' + rtrnString + ')';
            }

            return rtrnString;
        }

        public int CompareTo(MaterialArea other)
        {
            if (this.MaterialAreaType < other.MaterialAreaType)
            {
                return -1;
            }

            else if (this.MaterialAreaType > other.MaterialAreaType)
            {
                return 1;
            }

            int rc = this.WidthInInches - other.WidthInInches;

            if (rc != 0)
            {
                return rc;
            }

            return this.LngthInInches - other.LngthInInches;
        }
    }
}
