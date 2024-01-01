using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploreParks.Services;
using ExploreParks.Models;

namespace ExploreParks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetDescriptionsUpdatedController : ControllerBase
    {
        private readonly ExploreParksDbContext _context;

        public GetDescriptionsUpdatedController(ExploreParksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDescriptionsUpdated()
        {
            // Return getDescription from the database
            var response = await _context.Descriptions.ToListAsync();
            return Ok(response);
        }
    }
}