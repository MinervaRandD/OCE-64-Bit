using CanvasLib.Legend;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class LegendSerializable
    {
        public CoordinateSerializable Location { get; set; }

        public double Size { get; set; }

        public bool Visible { get; set; }

        public bool LocateToClick { get; set; }

        public string Notes { get; set; }

        public LegendLocation LegendShowLocation
        {
            get;
            set;
        }

        public LegendSerializable() { }

        public LegendSerializable(
            Coordinate location
            , double size
            , LegendLocation legendShowLocation
            , bool visible
            , bool locateToClick
            , string notes)
        {
            Location = new CoordinateSerializable(location);

            Size = size;

            LegendShowLocation = legendShowLocation;

               this.Visible = visible;

            this.LocateToClick = locateToClick;

            this.Notes = notes;
        }
    }
}
