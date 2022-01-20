using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Entities.Models;
using SmartApartmentData.Business.Contracts;

namespace SmartApartmentData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesManager _manager;

        public PropertiesController(IPropertiesManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetProperties([FromQuery] Properties queryparams)
        {
            if (queryparams == null
                || string.IsNullOrEmpty(queryparams.market))
                return BadRequest(queryparams);

            var response = _manager.GetDistinctProperties(queryparams);
            return Ok(response);
        }
        
    }
}
