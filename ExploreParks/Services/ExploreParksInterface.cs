global using ExploreParks.Services.Interface;
global using ExploreParks.Data;
global using System.Globalization;
global using System.Net.Http;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using Newtonsoft.Json;
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





public async Task<ResponseStatus<List<getDescription>>> GetDescriptionAsync()
{
    // Get Names from Database and store in a list
     var parkNames = await _context.Parks.Select(p => p.ParkName).ToListAsync();
    var descriptions = new List<getDescription>();

    foreach (var parkName in parkNames)
    {
        var encodedParkName = Uri.EscapeDataString(parkName);
        var apiUrl = $"https://en.wikipedia.org/api/rest_v1/page/summary/{encodedParkName}?redirect=true";
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var parkInfo = JsonConvert.DeserializeObject<dynamic>(json);

                var extract = parkInfo?.extract?.ToString();
                var latitude = parkInfo?.coordinates?.lat?.ToString();
                var longitude = parkInfo?.coordinates?.lon?.ToString();
                var contentUrls = parkInfo.content_urls.desktop.page.ToString();
                var language = parkInfo.lang.ToString();

                var newDescription = new getDescription
                {
                    ParkName = parkName,
                    ParkDescription = extract,
                    ParkLatitude = latitude,
                    ParkLongitude = longitude,
                    ContentUrls = contentUrls,
                    Language = language
                };

                Console.WriteLine($"Park Name: {parkName}");

                _context.Descriptions.Add(newDescription);
            }
        }
       
           
    }

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

    return new ResponseStatus<List<getDescription>>
    {
        Success = true,
        Message = "Parks details successfully Added to the database",
        Data = descriptions
    };
}


    }
    
}