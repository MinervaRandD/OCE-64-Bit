#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: Undrage.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Utilities;
    using Geometry;
    using Graphics;
   // using System.Drawing;

    [Serializable]
    public class Undrage: IRemnant
    {
        #region Undrage Indexing

        public static HashSet<uint> UndrageIndexSet
        {
            get;
            set;
        }= new HashSet<uint>();

        public static uint UndrageIndexGenerator()
        {
            uint indx = 1;

            while (true)
            {
                if (!UndrageIndexSet.Contains(indx))
                {
                    UndrageIndexSet.Add(indx);

                    return indx;
                }

                indx++;
            }
        }

        public static void RemoveIndex(uint indx)
        {
            if (UndrageIndexSet.Contains(indx))
            {
                UndrageIndexSet.Remove(indx);
            }
        }

        public static bool AddIndex(uint indx)
        {
            if (Undrage.UndrageIndexSet.Contains(indx))
            {
                return false;
            }

            Undrage.UndrageIndexSet.Add(indx);

            return true;
        }

        //public uint UndrageIndex
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Properties

        public Rollout ParentRollout { get; set; }

        public Rectangle BoundingRectangle { get; set; }

        public double MaterialWidth { get; set; }

        public double MaterialOverlap { get; set; }


        public List<EmbeddedCut> EmbeddedCutList { get; set; }

        public Tuple<double, double> EffectiveDimensions { get; set; } = null;

        /// <summary>
        /// These are the effective dimensions if the original underage size/shape is edited.
        /// TODO: This should be pushed to a higher level, e.g. graphics or canvas.
        /// </summary>
        public Tuple<double, double> OverrideEffectiveDimensions { get; set; } = null;

        public RemnantType RemnantType { get; set; } = RemnantType.Undrage;

        public bool Deleted { get; set; } = false;

        #endregion

        #region Constructors

        public Undrage() { }

        public Undrage(
            double width
            , double lngth
            , double materialWidth
            , double materialOverlap
            , double angle = 0)
        {
            //UndrageIndex = GetUndrIndx(this);

            MaterialWidth = materialWidth;

            MaterialOverlap = materialOverlap;

            Coordinate upperLeft = new Coordinate(0, width);
            Coordinate lowerRght = new Coordinate(lngth, 0);

            BoundingRectangle = new Rectangle(upperLeft, lowerRght);

            this.BoundingRectangle.Angle = angle;
        }

        public Undrage(
            Rollout parentRollout
            , Rectangle boundingRectangle
            , double materialWidth
            , double materialOverlap)
        {
           // UndrageIndex = Undrage.GetUndrIndx(this);

            BoundingRectangle = boundingRectangle;

            ParentRollout = parentRollout;

            MaterialWidth = materialWidth;

            MaterialOverlap = materialOverlap;
        }

        public Undrage(Undrage undrage)
        {
            this.MaterialWidth = undrage.MaterialWidth;

            this.OverrideEffectiveDimensions = undrage.OverrideEffectiveDimensions;

            this.BoundingRectangle = undrage.BoundingRectangle;

            this.EffectiveDimensions = undrage.EffectiveDimensions;

            this.Deleted = undrage.Deleted;

            this.MaterialOverlap = undrage.MaterialOverlap;

            this.RemnantType = undrage.RemnantType;

            if (undrage.EmbeddedCutList != null)
            {
                this.EmbeddedCutList = new List<EmbeddedCut>(undrage.EmbeddedCutList);
            }

        }

        #endregion

        //public static void ResetUndrageDict()
        //{
        //    UndrageIndexSet.Clear();
        //}

        public void Translate(Coordinate translateCoord)
        {
            if (BoundingRectangle != null)
            {
                BoundingRectangle.Translate(translateCoord);
            }

            if (EmbeddedCutList != null)
            {
                EmbeddedCutList.ForEach(ec => ec.Translate(translateCoord));
            }
        }

        public void Rotate(double theta, double[,] rotationMatrix)
        {
            if (BoundingRectangle != null)
            {
                BoundingRectangle.Rotate(theta);
            }

            if (EmbeddedCutList != null)
            {
                EmbeddedCutList.ForEach(ec => ec.Rotate(theta));
            }
        }

        public void Transform(Coordinate translateCoord, double theta, double[,] rotationMatrix)
        {
            Translate(translateCoord);
            Rotate(theta, rotationMatrix);
        }

        private List<EmbeddedCut> GenerateHorizontalEmbeddedCuts(SortedList<double, Seam> upperSeamList, SortedList<double, Seam> lowerSeamList)
        {
            List<EmbeddedCut> returnList = new List<EmbeddedCut>();

            if (upperSeamList is null)
            {
                return returnList;
            }

            if (upperSeamList.Count <= 0)
            {
                return returnList;
            }

            if (lowerSeamList is null)
            {
                return returnList;
            }

            if (lowerSeamList.Count <= 0)
            {
                return returnList;
            }

            HashSet<double> xValueSet = new HashSet<double>();

            double y1 = upperSeamList.Values.First().Coord1.Y;
            double y2 = lowerSeamList.Values.First().Coord1.Y;

            foreach (Seam seam in upperSeamList.Values)
            {
                double x1 = seam.Coord1.X;
                double x2 = seam.Coord2.X;

                if (isContainedInASeam(lowerSeamList, x1))
                {
                    if (!xValueSet.Contains(x1))
                    {
                        xValueSet.Add(x1);
                    }
                }

                if (isContainedInASeam(lowerSeamList, x2))
                {
                    if (!xValueSet.Contains(x2))
                    {
                        xValueSet.Add(x2);
                    }
                }
            }

            foreach (Seam seam in lowerSeamList.Values)
            {
                double x1 = seam.Coord1.X;
                double x2 = seam.Coord2.X;

                if (isContainedInASeam(upperSeamList, x1))
                {
                    if (!xValueSet.Contains(x1))
                    {
                        xValueSet.Add(x1);
                    }
                }

                if (isContainedInASeam(upperSeamList, x2))
                {
                    if (!xValueSet.Contains(x2))
                    {
                        xValueSet.Add(x2);
                    }
                }
            }

            List<double> xValueList = new List<double>(xValueSet);

            Debug.Assert(xValueList.Count % 2 == 0);

            xValueList.Sort();

            int count = xValueList.Count;

            for (int i = 0; i < count; i += 2)
            {
                Coordinate upperLeft = new Coordinate(xValueList[i], y1);
                Coordinate lowerRght = new Coordinate(xValueList[i + 1], y2);

                EmbeddedCut embeddedCut = new EmbeddedCut(upperLeft, lowerRght);

                returnList.Add(embeddedCut);
            }

            return returnList;
        }

        private bool isContainedInASeam(SortedList<double, Seam> seamList, double x)
        {
            foreach (Seam seam in seamList.Values)
            {
                if (x < seam.Coord1.X)
                {
                    return false;
                }

                if (x >= seam.Coord1.X && x <= seam.Coord2.X)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ShapeHasBeenOverridden =>
            OverrideEffectiveDimensions is null ? false : EffectiveDimensions != OverrideEffectiveDimensions;


    }
}
