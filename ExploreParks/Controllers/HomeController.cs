using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [SwaggerOperation(Summary = "Get all parks")]
        public async Task<ActionResult<List<ParkModel>>> Get()
        {
            // get all the parks asynchronously
            var parks = await _context.Parks.ToListAsync();
            return Ok(parks);
        }
    }
}