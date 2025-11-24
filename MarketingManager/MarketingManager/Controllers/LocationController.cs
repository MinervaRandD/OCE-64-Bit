using Microsoft.AspNetCore.Mvc;
using MarketingManager.Models;
using Newtonsoft.Json;

namespace MarketingManager.Controllers
{
    public class LocationController : Controller
    {
        private readonly MarketingSupportContext _context;

        public LocationController(MarketingSupportContext context)
        {
            _context = context;
        }


        [HttpPost]
        public void UpdateLocation(string userId, string locationId, string locationJson)
        {
            Location? location = _context.Locations.Where(c => c.UserId == userId && c.LocationId == locationId).FirstOrDefault();

            if (location != null)
            {
                _context.Locations.Remove(location);
            }

            location = JsonConvert.DeserializeObject<Location>(locationJson);

            if (location == null)
            {
                return;
            }

            _context.Locations.Add(location); 

            _context.SaveChanges();
        }


        [HttpPost]
        public void DeleteLocation(string userId, string locationId)
        {
            Location? location = _context.Locations.Where(c => c.UserId == userId && c.LocationId == locationId).FirstOrDefault();

            if (location != null)
            {
                _context.Locations.Remove(location);
            }

            _context.SaveChanges();
        }
    }
}
