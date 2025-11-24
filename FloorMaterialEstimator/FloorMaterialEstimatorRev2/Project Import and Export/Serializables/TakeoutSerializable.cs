using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using Graphics;
using CanvasLib.DoorTakeouts;

namespace FloorMaterialEstimator
{
    public class TakeoutSerializable
    {
        public string Guid { get; set; }

        public Coordinate Center { get; set; }

        public double Radius { get; set; }

        public double TakeoutAmount { get; set; }

        public string LineFinishBaseGuid { get; set; }

        public TakeoutSerializable() { }

        public TakeoutSerializable(DoorTakeout takeout)
        {
            this.Guid = takeout.Guid;

            this.Center = takeout.Center;

            this.Radius = takeout.Radius;

            this.TakeoutAmount = takeout.TakeoutAmount;

            this.LineFinishBaseGuid = takeout.LineFinishBaseGuid;
        }

        public DoorTakeout Deserialize(GraphicsPage page)
        {
            DoorTakeout takeout = new DoorTakeout(
                page
                ,this.Center
                ,this.Radius
                ,this.TakeoutAmount
                ,this.LineFinishBaseGuid
                ,this.Guid);

            return takeout;
        }


    }
}
