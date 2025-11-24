using Microsoft.AspNetCore.Mvc;
using MarketingManager.Models;
using Newtonsoft.Json;
using Utilities;

namespace MarketingManager.Controllers
{
    public class MarkerController : Controller
    {


        private readonly MarketingSupportContext _context;

        public MarkerController(MarketingSupportContext context)
        {
            _context = context;
        }


        [HttpPost]
        public void AddMarker(string userId, string markerId, string markerJson)
        {
            Marker? marker = _context.Markers.Where(c => c.UserId == userId && c.MarkerId == markerId).FirstOrDefault();

            if (marker != null)
            {
                _context.Markers.Remove(marker);
            }

            marker = JsonConvert.DeserializeObject<Marker>(markerJson);

            if (marker == null)
            {
                return;
            }

            _context.Markers.Add(marker);

            _context.SaveChanges();
        }

        [HttpPost]
        public void UpdateMarker(string userId, string markerId, string markerJson)
        {
            Marker? marker = _context.Markers.Where(c => c.UserId == userId && c.MarkerId == markerId).FirstOrDefault();

            if (marker != null)
            {
                _context.Markers.Remove(marker);
            }

            marker = JsonConvert.DeserializeObject<Marker>(markerJson);

            if (marker == null)
            {
                return;
            }

            _context.Markers.Add(marker);

            _context.SaveChanges();
        }

        [HttpPost]
        public void DeleteMarker(string userId,  string markerId)
        {
            Marker? marker = _context.Markers.Where(m=>m.UserId == userId && m.MarkerId == markerId).FirstOrDefault();

            if (marker == null)
            {
                return;
            }

            _context.Markers.Remove(marker);

            _context.SaveChanges();
        }
    }
}
