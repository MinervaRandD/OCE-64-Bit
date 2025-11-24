using MarketingMap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MarketingMap.Models;

namespace MarketingMap.Controllers
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
            List<Marker> markerList = _context.Markers.ToList();

            ViewBag.markerList = markerList;


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

            string guid = Utilities.GuidMaintenance.GenerateGuid();

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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}