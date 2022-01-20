using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Entities.Models;
using SmartApartmentData.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var response = _manager.GetDistinctProperties(queryparams);
            return Ok(response);
        }
        
    }
}
