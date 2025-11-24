using Microsoft.AspNetCore.Mvc;
using MarketingManager.Models;
using Newtonsoft.Json;
using Utilities;

namespace MarketingManager.Controllers
{
    public class CommonController : Controller
    {
        [HttpPost]
        public string GetNewGuid(string userId)
        {
            return GuidMaintenance.GenerateGuid(userId);
        }


    }
}
