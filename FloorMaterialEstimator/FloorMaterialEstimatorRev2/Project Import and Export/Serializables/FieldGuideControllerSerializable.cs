

namespace FloorMaterialEstimator
{
    using CanvasLib.Markers_and_Guides;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Serialization;

    public class FieldGuideControllerSerializable
    {
        public List<FieldGuideSerializable> FieldGuideList { get; set; } = new List<FieldGuideSerializable>();

        public int FieldGuideLineColorA { get; set; }
        public int FieldGuideLineColorR { get; set; }
        public int FieldGuideLineColorG { get; set; }
        public int FieldGuideLineColorB { get; set; }

        [XmlIgnore]
        public Color FieldGuideLineColor
        {
            get
            {
                return Color.FromArgb(FieldGuideLineColorA, FieldGuideLineColorR, FieldGuideLineColorG, FieldGuideLineColorB);
            }

            set
            {
                FieldGuideLineColorA = value.A;
                FieldGuideLineColorR = value.R;
                FieldGuideLineColorG = value.G;
                FieldGuideLineColorB = value.B;
            }
        }

        public int FieldGuideLineStyle { get; set; }

        public double FieldGuideLineOpacity { get; set; }

        public double FieldGuideLineWidthInPts { get; set; }

        public bool FieldGuidesShowing { get; set; }
        public FieldGuideControllerSerializable() { }

        public FieldGuideControllerSerializable(FieldGuideController fieldGuideController, bool fieldGuidesShowing)
        {
            this.FieldGuideLineColor = fieldGuideController.LineColor;
            this.FieldGuideLineOpacity = fieldGuideController.Opacity;
            this.FieldGuideLineStyle = fieldGuideController.LineStyle;
            this.FieldGuideLineWidthInPts = fieldGuideController.LineWidthInPts;
            this.FieldGuidesShowing = fieldGuidesShowing;

            foreach (FieldGuide fieldGuide in fieldGuideController.fieldGuides)
            {
                FieldGuideList.Add(new FieldGuideSerializable(fieldGuide));
            }
        }
    }
}
