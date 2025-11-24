using MarketingManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;


namespace MarketingManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MarketingSupportContext _context;

        public HomeController(ILogger<HomeController> logger, MarketingSupportContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Map()
        {
            List<Marker> markerList = _context.Markers.Where(m=>m.UserId == StaticGlobals.defaultUserId).ToList();

            ViewBag.markerList = markerList;

            List<Contact> contactList = _context.Contacts.Where(c => c.UserId == StaticGlobals.defaultUserId).ToList();

            ViewBag.contactList = contactList;

            List<Location> locationList = _context.Locations.Where(l => l.UserId == StaticGlobals.defaultUserId).ToList();

            ViewBag.locationList = locationList;

            Location? initialLocation =
                _context.Locations.SingleOrDefault(l => l.UserId == StaticGlobals.defaultUserId && l.LocationName == "Current");

            if (initialLocation != null)
            {
                ViewBag.initialLocation = new Tuple<double, double>(initialLocation.Lat, initialLocation.Lng);
            }

            else
            {
                ViewBag.initialLocation = new Tuple<double, double>(StaticGlobals.homeLat, StaticGlobals.homeLng);
            }

            ViewBag.homeLat = StaticGlobals.homeLat;
            ViewBag.homeLng = StaticGlobals.homeLng;

            ViewBag.userId = StaticGlobals.defaultUserId;

            return View();
        }


        [HttpPost]
        public void MapMoved(string lat, string lng)
        {
            double dLat, dLng;

            if (!double.TryParse(lat, out dLat))
            {
                return;
            }

            if (!double.TryParse(lng, out dLng))
            {
                return;
            }

            Location? currentLocation =
                _context.Locations.SingleOrDefault(l => l.UserId == StaticGlobals.defaultUserId && l.LocationName == "Current");

            string guid = Utilities.GuidMaintenance.GenerateGuid(StaticGlobals.defaultUserId);

           if (currentLocation == null)
            {
                currentLocation = new Location
                {
                    UserId = StaticGlobals.defaultUserId,
                    LocationId = guid,
                    LocationName = "Current",
                    Lat = dLat,
                    Lng = dLng,
                    Comments = "Last map location center"
                };

                _context.Locations.Add(currentLocation);
            }

            else
            {
                currentLocation.Lat = dLat;
                currentLocation.Lng = dLng;
            }

           _context.SaveChanges();
        }

        public IActionResult Contacts()
        {

            List<Contact> contactList = _context.Contacts.Where(c => c.UserId == StaticGlobals.defaultUserId).ToList();

            ViewBag.contactList = contactList;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}