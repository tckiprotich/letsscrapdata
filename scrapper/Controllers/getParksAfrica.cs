using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace scrapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getParksAfrica : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<getParksAfrica> _logger;

        public getParksAfrica(AppDbContext context, ILogger<getParksAfrica> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<parkModels>> GetParkName()
        {
            // Get Names from the database
            var parkName = await _context.parkName.ToListAsync();

            foreach (var park in parkName)
            {
                var parkNameForUrl = park.Name.Replace(" ", "_");
                var web = new HtmlWeb();
                var doc = web.Load($"https://en.wikipedia.org/wiki/{parkNameForUrl}");

                var parkNamed = doc.DocumentNode.SelectSingleNode("//*[@id=\"firstHeading\"]/span");
                var description = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/p[1]");

                if (parkNamed != null && description != null)
                {
                    var name = parkNamed.InnerText;
                    var desc = description.InnerText;

                    var newParkName = new parkModels
                    {
                        Name = name,
                        Description = desc,
                        NearestCity = "N/A",
                    };

                    _context.parkModels.Add(newParkName);
                }
                else
                {
                    _logger.LogWarning("Failed to scrape data for park: {ParkName}", park.Name);
                }
            }

            await _context.SaveChangesAsync();

            // Get the updated list from the database
            var parks = await _context.parkModels.ToListAsync();

            return parks;
        }
    }
}