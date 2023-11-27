using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace scrapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LetsMergeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LetsMergeController> _logger;

        public LetsMergeController(AppDbContext context, ILogger<LetsMergeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<parkName>> GetParkName()
        {
            // Get Names from the database
            var parkName = await _context.parkName.ToListAsync();

            // get the data from the database
            var parksAfrica  = await _context.parkModels.ToListAsync();

            // Create a dictionary to map park names to their descriptions
var parkDescriptions = parksAfrica
    .GroupBy(p => p.Name)
    .ToDictionary(g => g.Key, g => g.First().Description);

            // Update description field in parkName with description from parkModels
            foreach (var item in parkName)
            {
                if (parkDescriptions.TryGetValue(item.Name, out var description))
                {
                    item.Description = description;
                }
            }

            return parkName;
        }
    }
}