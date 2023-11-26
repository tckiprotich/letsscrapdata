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
            var doc = web.Load($"https://en.wikipedia.org/wiki/{parkNameForUrl}");
            // var doc = web.Load("https://en.wikipedia.org/wiki/Hoggar_Mountains");

            var parkNameNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"firstHeading\"]/span");
            var parkName = parkNameNode?.InnerText;

            var descriptionNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/p[1]");
            var description = descriptionNode?.InnerText;

            string latitude = null;
            string longitude = null;

            var latitudeNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody/tr[6]/td/span/span/span/a/span[1]/span/span[1]");
            latitude = latitudeNode?.InnerText;

            var longitudeNode = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div/div[3]/main/div[3]/div[3]/div[1]/table[1]/tbody/tr[6]/td/span/span/span/a/span[1]/span/span[2]");
            longitude = longitudeNode?.InnerText;

            var countryNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody/tr[11]/td");
            var Country = countryNode?.InnerText;

            var area = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[2]/tbody/tr[7]/td/text()[1]");
            var Area = area?.InnerText;

            var established = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[2]/tbody/tr[8]/td");
            var Established = established?.InnerText;

            var nearestcity = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[2]/tbody/tr[5]/td/a");
            var NearestCity = nearestcity?.InnerText;

            var imageNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody/tr[2]/td/span/a/img");
            var imageUrl = imageNode?.Attributes["src"].Value;



            var newPark = new parkModels
            {
                Id = Guid.NewGuid(),
                Name = parkName,
                Country = Country,
                Latitude = latitude,
                Longitude = longitude,
                Description = description,
                Area = Area,
                Established = Established,
                NearestCity = NearestCity,
                ImageUrl = imageUrl,
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

            // Return the list of parks
            return parks;
        }

    }
}