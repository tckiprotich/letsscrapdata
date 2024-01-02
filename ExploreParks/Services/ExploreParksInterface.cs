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

            var parkModels = new List<ParkModel>();

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

                    // creating a new park model entity
                    var newParkModel = new ParkModel
                    {
                        ParkName = parkName,
                        ParkLocation = location,
                        ParkArea = area,
                        ParkEstablished = established
                    };

                    // adding the new park model entity to the list
                    _context.Parks.Add(newParkModel);
                }
            }

            try
            {
                // Delete existing records from the Descriptions table
                _context.Parks.RemoveRange(_context.Parks);
                // adding the new park models to the database
                await _context.Parks.AddRangeAsync(parkModels);

                // saving the changes to the database
                await _context.SaveChangesAsync();

                return new ResponseStatus<List<ParkModel>>
                {
                    Success = true,
                    Message = "Parks successfully added to the database",
                    Data = parkModels
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred: " + e.Message.ToString());

                return new ResponseStatus<List<ParkModel>>
                {
                    Success = false,
                    Message = "Error occurred while saving changes to the database",
                    Data = null
                };
            }
        }





        public async Task<ResponseStatus<List<getDescription>>> GetDescriptionAsync()
        {
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
                // Delete existing records from the Descriptions table
                _context.Descriptions.RemoveRange(_context.Descriptions);

                // Add the new park models to the database
                await _context.Descriptions.AddRangeAsync(descriptions);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred: " + e.Message);
                return new ResponseStatus<List<getDescription>>
                {
                    Success = false,
                    Message = "Error occurred while saving changes to the database",
                    Data = null
                };
            }

            return new ResponseStatus<List<getDescription>>
            {
                Success = true,
                Message = "Parks details successfully added to the database",
                Data = descriptions
            };
        }


    }

}