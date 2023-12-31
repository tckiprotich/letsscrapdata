global using ExploreParks.Services.Interface;
global using ExploreParks.Data;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Services
{
    public class ExploreParksInterface : IExploreParksInterface
    {

        // db context
        private readonly ExploreParksDbContext _context;

        // constructor
        public ExploreParksInterface(ExploreParksDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseStatus<List<ParkModel>>> GetParksAsync()
        {
            // load target url
            var web = new HtmlWeb();

            // loading the target web page 
            var doc = web.Load("https://en.wikipedia.org/wiki/List_of_national_parks_in_Africa");
            // get the table from entity framework
            var parkModels = new List<ParkModel>();

            // getting the table from the web page
            var table = doc.DocumentNode.SelectSingleNode("//table[@class='wikitable sortable']");

            // getting the rows from the table
            var rows = table.SelectNodes(".//tr");

            // mapping the rows to the park model

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
                    var newParkModels = new ParkModel
                    {
                        ParkName = parkName,
                        ParkLocation = location,
                        ParkArea = area,
                        ParkEstablished = established
                    };

                    Console.WriteLine("Park Name: " + parkName);

                    // adding the new parkModels entity to the list
                    parkModels.Add(newParkModels);

                    // adding the new parkName entity to the database
                    await _context.SaveChangesAsync();


                }
            }
            // saving the changes to the database
            try
            {
                await _context.SaveChangesAsync();
                await _context.Database.EnsureCreatedAsync();
                Console.WriteLine("Database created, Saving changes to the database...");
                await _context.SaveChangesAsync();
                Console.WriteLine("Changes saved to the database...");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred: " + e.Message);
            }

            return new ResponseStatus<List<ParkModel>>
            {
                Success = true,
                Message = "Parks successfully Added to the database",
                Data = parkModels
            };


        }
    }
}