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

        [HttpGet("parksAfrica")]
        public async Task<IEnumerable<parkModels>> Get()
        {
            // Get Names from the database
            var parkNames = await _context.parkName.ToListAsync();

            // Create a list to store the scraped data
            var parks = new List<parkModels>();

            foreach (var park in parkNames)
            {
            var web = new HtmlWeb();
            var parkNameForUrl = park.Name.Replace(" ", "_");
            Console.WriteLine(parkNameForUrl);
            var doc = web.Load($"https://en.wikipedia.org/wiki/{parkNameForUrl}");
            // var doc = web.Load("https://en.wikipedia.org/wiki/Hoggar_Mountains");

            var parkNameNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"firstHeading\"]/span");
            var parkName = parkNameNode?.InnerText;

            var descriptionNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/p[1]");
            var description = descriptionNode?.InnerText;

            // Update the park with the description
            park.Description = description;

            // Create a new park
            var newPark = new parkModels
            {
                Id = park.Id,
                Name = park.Name,
                Description = park.Description
            };



            // Add the new park to the list
            parks.Add(newPark);

            _context.parkModels.Add(newPark);
            try
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving park");
            }
            }
            return parks ;
        }

    }
}