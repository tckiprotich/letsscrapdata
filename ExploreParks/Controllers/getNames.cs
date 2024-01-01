using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExploreParks.Services;
using ExploreParks.Data;

namespace ExploreParks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetNamesController : ControllerBase
    {
        private readonly IExploreParksInterface _exploreParksInterface;
        private readonly ExploreParksDbContext _context;

        public GetNamesController(IExploreParksInterface exploreParksInterface, ExploreParksDbContext context)
        {
            _exploreParksInterface = exploreParksInterface;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDescriptionAsync()
        {
            var response = await _exploreParksInterface.GetDescriptionAsync();
            return Ok(response);
        }
    }
}