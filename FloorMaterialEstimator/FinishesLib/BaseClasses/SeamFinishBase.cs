//-------------------------------------------------------------------------------//
// <copyright file="SeamFinishBase.cs" company="Bruun Estimating, LLC">          // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FinishesLib
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Utilities;
    using System.Diagnostics;

    [Serializable]
    public class SeamFinishBase
    {
         #region Events and Delegates

        public delegate void SeamNameChangedHandler(SeamFinishBase seamFinishBase, string seamName);

        public event SeamNameChangedHandler SeamNameChanged;

        public delegate void SeamWidthChangedHandler(SeamFinishBase SeamFinishBase, double lineWidthInPts);

        public event SeamWidthChangedHandler SeamWidthChanged;

        public delegate void FilteredChangedHandler(SeamFinishBase SeamFinishBase, bool filtered);

        public event FilteredChangedHandler FilteredChanged;

        public delegate void SelectedChangedHandler(SeamFinishBase SeamFinishBase, bool selected);

        public event FilteredChangedHandler SelectedChanged;

        public delegate void SeamColorChangedHandler(SeamFinishBase SeamFinishBase, Color lineColor);

        public event SeamColorChangedHandler SeamColorChanged;

        public delegate void SeamDashTypeChangedHandler(SeamFinishBase SeamFinishBase, int VisioDashType);

        public event SeamDashTypeChangedHandler SeamDashTypeChanged;

        public delegate void LengthChangedHandler(SeamFinishBase SeamFinishBase, double lengthInInches);

        public event LengthChangedHandler LengthChanged;

        // The seam changed event is triggered whenever any graphic aspect of the current seam has been changed.
        // This is a convenience for other forms and program elements that need to update whenever some
        // graphics aspect of the current seam has changed. So for example, if a seam has to be redrawn it doesn't
        // matter whether the color or dash type has changed, it has to be redrawn anyway. This way, these
        // subscribing elements do not have to track every individual graphics event. 

        public delegate void SeamGraphicsChangedHandler(SeamFinishBase SeamFinishBase);

        public event SeamGraphicsChangedHandler SeamGraphicsChanged;

        #endregion
        public string Guid
        {
            get;
            set;
        }

        #region Non-Graphics Attributes

        private string _seamName;
        public string SeamName
        {
            get
            {
                return _seamName;
            }

            set
            {
                _seamName = value;

                if (SeamNameChanged != null)
                {
                    SeamNameChanged.Invoke(this, _seamName);
                }

            }
        }
        public string Product { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        private bool _filtered;
        public bool Filtered
        {
            get
            {
                return _filtered;
            }

            set
            {
                _filtered = value;

                if (FilteredChanged != null)
                {
                    FilteredChanged.Invoke(this, _filtered);
                }
            }
        }

        private bool _selected;

        public bool Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                _selected = value;

                if (SelectedChanged != null)
                {
                    SelectedChanged.Invoke(this, _selected);
                }
            }
        }

        double lengthInInches;
        public double LengthInInches
        {
            get
            {
                return lengthInInches;
            }

            set
            {
                if (lengthInInches == value)
                {
                    return;
                }

                lengthInInches = value;

                if (LengthChanged != null)
                {
                    LengthChanged.Invoke(this, lengthInInches);
                }
            }
        }

        #endregion

        #region Graphics Attributes

        private int visioDashType;
        public int VisioDashType
        {
            get
            {
                return visioDashType;
            }

            set
            {
                if (VisioDashType == value)
                {
                    return;
                }

                visioDashType = value;

                if (SeamDashTypeChanged != null)
                {
                    SeamDashTypeChanged.Invoke(this, visioDashType);
                }

                if (SeamGraphicsChanged != null)
                {
                    SeamGraphicsChanged.Invoke(this);
                }
            }
        }

        private double seamWidthInPts;
        public double SeamWidthInPts
        {
            get
            {
                return seamWidthInPts;
            }

            set
            {
                if (SeamWidthInPts == value)
                {
                    return;
                }

                seamWidthInPts = value;

                if (SeamWidthChanged != null)
                {
                    SeamWidthChanged.Invoke(this, seamWidthInPts);
                }

                if (SeamGraphicsChanged != null)
                {
                    SeamGraphicsChanged.Invoke(this);
                }
            }
        }

        // Color specification

        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        [XmlIgnore]
        public Color SeamColor
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }

            set
            {
                if (A == value.A && R == value.R && G == value.G && B == value.B)
                {
                    return;
                }

                A = value.A;
                R = value.R;
                G = value.G;
                B = value.B;

                if (SeamColorChanged != null)
                {
                    SeamColorChanged.Invoke(this, Color.FromArgb(A, R, G, B));
                }

                if (SeamGraphicsChanged != null)
                {
                    SeamGraphicsChanged.Invoke(this);
                }
            }
        }

        #endregion

        [XmlIgnore]
        public static SeamFinishBase DefaultFinishSeamLine = new SeamFinishBase()
        {
            SeamName = "",
            VisioDashType = 1,
            SeamWidthInPts = 2,
            A = Color.Cyan.A,
            R = Color.Cyan.R,
            G = Color.Cyan.G,
            B = Color.Cyan.B
        };

        [XmlIgnore]
        public static Color DefaultLineColor = Color.Cyan;

        [XmlIgnore]
        public static double DefaultLineWidthInPts = 2;

        [XmlIgnore]
        public static short DefaultVisioDashType = 1;

        public SeamFinishBase Clone()
        {
            SeamFinishBase clonedFinishSeamBase = new SeamFinishBase()
            {
                SeamName = this.SeamName
                ,VisioDashType = this.VisioDashType
                ,SeamWidthInPts = this.SeamWidthInPts
                ,SeamColor = this.SeamColor
                ,Product = this.Product
                ,Notes = this.Notes

                ,Guid = this.Guid
                
            };

            clonedFinishSeamBase.Guid = GuidMaintenance.CreateGuid(clonedFinishSeamBase);

            return clonedFinishSeamBase;
        }
    }

}
