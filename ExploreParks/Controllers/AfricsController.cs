using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExploreParks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AfricsController : ControllerBase
    {
        private readonly IExploreParksInterface _exploreParksInterface;

        public AfricsController(IExploreParksInterface exploreParksInterface)
        {
            _exploreParksInterface = exploreParksInterface;
        }

        [HttpGet]
        [Route("api/Africs")]
        public async Task<ActionResult<List<ParkModel>>> Get()
        {
            var parks = await _exploreParksInterface.GetParksAsync();
            return Ok(parks);
        }
    }
}