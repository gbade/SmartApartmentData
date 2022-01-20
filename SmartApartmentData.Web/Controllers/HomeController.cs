using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartApartmentData.Business.Contracts;
using SmartApartmentData.Entities.Models;
using SmartApartmentData.Web.Models;

namespace SmartApartmentData.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertiesManager _manager;

        public HomeController(ILogger<HomeController> logger, IPropertiesManager manager)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewmodel = new PropertyViewModel();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Index(PropertyViewModel model)
        {
            var viewmodel = new PropertyViewModel();

            var properties = _manager.GetDistinctProperties(model.Property);

            foreach (var item in properties)
            {
                var property = new PropertyData
                {
                    Name = item.name,
                    PropertyID = item.propertyID,
                    FormerName = item.formerName,
                    StreetAddress = item.streetAddress,
                    City = item.city,
                    Market = item.market,
                    State = item.state,
                    Latitude = item.lat,
                    Longitude = item.lng,
                    MgmtID = item.mgmtID
                };

                viewmodel.Data.Add(property);
            }

            return View(viewmodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
