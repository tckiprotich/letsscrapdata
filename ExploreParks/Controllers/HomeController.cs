using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExploreParks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ExploreParksDbContext _context;

        // constructor
        public HomeController(ExploreParksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/")]
        [ProducesResponseType(typeof(List<ParkModel>), 200)]
        [SwaggerOperation(Summary = "Get the API root", Tags = new[] { "API Root" })]
        public ActionResult<List<ParkModel>> Get()
        {
            // get all the parks
            var parks = _context.Parks.ToList();
            return Ok(parks);
        }
    }
}