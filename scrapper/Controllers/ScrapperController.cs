using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace scrapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapperController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScrapperController> _logger;

        public ScrapperController(AppDbContext context, ILogger<ScrapperController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("getParksAfricaName")]
        public async Task<IEnumerable<parkName>> GetParkName()
        {
            var web = new HtmlWeb();
            // loading the target web page 
            var doc = web.Load("https://en.wikipedia.org/wiki/List_of_national_parks_in_Africa");

            var parkNames = new List<parkName>();

            // getting the table from the web page
            var table = doc.DocumentNode.SelectSingleNode("//table[@class='wikitable sortable']");

            // getting the rows from the table
            var rows = table.SelectNodes(".//tr");

            // looping through the rows
            foreach (var row in rows)
            {
                // getting the columns from the rows
                var columns = row.SelectNodes(".//td");
                if (columns != null)
                {
                    // getting the park name
                    var parkName = columns[0].InnerText;

                    // Location
                    var location = columns[1].InnerText;

                    // Area
                    var area = columns[2].InnerText;

                    // Established
                    var established = columns[3].InnerText;

                    // creating a new parkName entity
                    var newParkName = new parkName
                    {
                        Id = Guid.NewGuid(),
                        Name = parkName,
                        Location = location,
                        Area = area,
                        Established = established
                    };

                    // adding the park to the list
                    parkNames.Add(newParkName);

                    // adding the park to the DbContext
                    _context.parkName.Add(newParkName);
                }
            }

            // saving the park names to the database
            try
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _logger.LogInformation("Saving changes to the database...");
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully saved changes to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes to the database.");
            }

            return parkNames;

        }        
    }
}