#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: Cut.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System.Drawing;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;

    public class Cut : INotifyPropertyChanged
    {
        //public static int CutIndexGenerator { get; set; } = 1;\\
        private decimal patternRepeats =0M;

        public event PropertyChangedEventHandler PropertyChanged;

        //public int CutIndex { get; set; }

        public object Tag { get; set; }

        public bool IsRotated { get; set; } = false;

        public bool Deleted { get; set; }

        public decimal PatternRepeats {
            get { return patternRepeats; }
            set
            {
                if (value != patternRepeats)
                {
                    patternRepeats = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///  The cut rectangle is the original segment of the material roll that created the cut.
        /// </summary>
        public Geometry.Rectangle CutRectangle { get; set; }

        /// <summary>
        /// This is the rectangle if the original cut size/shape is overridding in an editor.
        /// TODO: This should be pushed to a higher level, e.g. graphics or 
        /// </summary>
        public Geometry.Rectangle OverrideCutRectangle { get; set; }

        public double SeamWidth { get; set; }

        public double MaterialWidth { get; set; }

        public double MaterialOverlap { get; set; }

        public int ExtraInchesPerCut { get; set; }

        public bool ShapeHasBeenOverridden =>
            OverrideCutRectangle is null ? false :
            (CutRectangle.Width != OverrideCutRectangle.Width || CutRectangle.Height != OverrideCutRectangle.Height);


        /// <summary>
        /// The cut polygon list represents the actual shape of the segment of material that was cut out. Note that there can be more than 
        /// one completed polygon. This occurs when the material layout overlaps horizontally.
        /// </summary>
        public List<DirectedPolygon> CutPolygonList = new List<DirectedPolygon>();

        /// <summary>
        /// The parent roll out of the cut
        /// </summary>
        public Rollout ParentRollout { get; set; } = null;

        /// <summary>
        /// Cuts can have a list of overages. These are pieces of material that were cut from the
        /// original cut rectangle to form the cut polygon
        /// </summary>
        public List<Overage> OverageList { get; set; } = new List<Overage>();

        public List<RemnantCut> RemnantCutList { get; set; } = new List<RemnantCut>();

        public double CutAngle { get; set; }

        public Coordinate CutOffset { get; set; }

        public Cut()
        {
            //CutIndex = CutIndexGenerator++;
        }

        public Cut(
            Rollout parentRollout
            , Geometry.Rectangle cutRectangle
            , DirectedPolygon cutPolygon
            , double seamWidth
            , double materialWidth
            , double materialOverlap)
        {
            this.ParentRollout = parentRollout;

            this.CutRectangle = cutRectangle;

            this.CutPolygonList = new List<DirectedPolygon>() { cutPolygon };

            this.SeamWidth = seamWidth;

            this.MaterialWidth = materialWidth;

            this.MaterialOverlap = materialOverlap;

            //CutIndex = CutIndexGenerator++;
        }

        //public Cut(Rollout parentRollout, Geometry.Rectangle cutRectangle, List<DirectedPolygon> cutPolygonList, int cutIndex)
        //{
        //    this.ParentRollout = parentRollout;

        //    this.CutRectangle = cutRectangle;

        //    this.CutPolygonList = new List<DirectedPolygon>();

        //    this.CutPolygonList.AddRange(cutPolygonList);

        //    CutIndex = cutIndex;
        //}

        public void Translate(Coordinate translateCoordinate)
        {
            if (CutPolygonList != null)
            {
                CutPolygonList.ForEach(p=>p.Translate(translateCoordinate));
            }

            if (OverageList != null)
            {
                foreach (Overage overage in OverageList)
                {
                    overage.Translate(translateCoordinate);
                }
            }

            if (RemnantCutList != null)
            {
                RemnantCutList.ForEach(e => e.Translate(translateCoordinate));
            }

            CutRectangle.Translate(translateCoordinate);
        }

        public void Rotate(double[,] rotationMatrix, double angle)
        {
            if (CutPolygonList != null)
            {
                CutPolygonList.ForEach(p=>p.Rotate(rotationMatrix));
            }

            if (OverageList != null)
            {
                foreach (Overage overage in OverageList)
                {
                    overage.Rotate(angle, rotationMatrix);
                }
            }

            if (RemnantCutList != null)
            {
                RemnantCutList.ForEach(e => e.Rotate(rotationMatrix));
            }

            CutRectangle.Rotate(angle);
        }

        public void Transform(Coordinate translateCoordinate, double theta, double[,] rotationMatrix)
        {
            if (CutPolygonList != null)
            {
                CutPolygonList.ForEach(p=>p.Transform(translateCoordinate, rotationMatrix));
            }

            //if (Rollout != null)
            //{
            //    Rollout.Transform(translateCoordinate, rotationMatrix);
            //}

            if (OverageList != null)
            {
                foreach (Overage overage in OverageList)
                {
                    overage.Transform(translateCoordinate, theta, rotationMatrix);
                }
            }
        }

        public void Clear()
        {

        }

        internal void Draw(Color cutPenColor, Color cutFillColor, double lineWidthInPts)
        {
            throw new NotImplementedException();
        }

        internal double CutAreaInSqrInches(double drawingScaleInInches)
        {
            return CutRectangle.AreaInSqrInches(drawingScaleInInches);

        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
