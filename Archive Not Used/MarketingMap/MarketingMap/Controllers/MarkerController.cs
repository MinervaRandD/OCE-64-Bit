using Microsoft.AspNetCore.Mvc;
using MarketingMap.Models;
using Utilities;

namespace MarketingMap.Controllers
{
    public class MarkerController : Controller
    {


        private readonly MarketingSupportContext _context;

        public MarkerController(MarketingSupportContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void MarkerMoved(string tag, string lat, string lng)
        {
            double dLat;
            double dLng;

            if (string.IsNullOrEmpty(tag))
            {
                return;
            }


            if (!double.TryParse(lat, out dLat))
            {
                return;
            }

            if (!double.TryParse(lng, out dLng))
            {
                return;
            }
            
            dLat = Convert.ToDouble(lat);
            dLng = Convert.ToDouble(lng);

            Marker? marker = _context.Markers.SingleOrDefault<Marker>(m => m.MarkerId == tag);

            if (marker == null)
            {
                return;
            }

            marker.Lat = dLat;

            marker.Lng = dLng;

            _context.Markers.Update(marker);

            _context.SaveChanges();
        }

        [HttpPost]
        public string MarkerAdded(string lat, string lng)
        {

            double dLat;
            double dLng;

            if (!double.TryParse(lat, out dLat))
            {
                return null;
            }


            if (!double.TryParse(lng, out dLng))
            {
                return null;
            }

            string guid = Utilities.GuidMaintenance.GenerateGuid();

            Marker? marker = new Marker
            {
                UserId = StaticGlobals.defaultUserId,
                MarkerId = guid,
                Lat = dLat,
                Lng = dLng
            };

            _context.Markers.Add(marker);

            _context.SaveChanges();

            return guid;
        }
    }
}
