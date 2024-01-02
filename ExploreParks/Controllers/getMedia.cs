using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ExploreParks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getMedia : ControllerBase
    {
        private readonly ExploreParksDbContext _context;

        public getMedia(ExploreParksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ResponseStatus<List<ParkModel>>> GetMedia()
        {
            var parkModels = await _context.Parks.ToListAsync();

            foreach (var parkModel in parkModels)
            {
                var encodedParkName = Uri.EscapeDataString(parkModel.ParkName);
                var apiUrl = $"https://en.wikipedia.org/api/rest_v1/page/media-list/{encodedParkName}?redirect=true";

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var parkInfo = JsonConvert.DeserializeObject<dynamic>(json);

                        if (parkInfo?.items != null && parkInfo.items.Count > 0)
                        {
                            var srcset = parkInfo.items[0]?.srcset;
                            if (srcset != null && srcset.Count > 0)
                            {
                                var ParkImage = srcset[0]?.src?.ToString();
                                Console.WriteLine($"Park Image: {ParkImage}");

                                parkModel.ParkImage = ParkImage;
                            }
                        }
                    }
                }
            }

            try
            {
               
                await _context.SaveChangesAsync();
                Console.WriteLine("Saving changes to the database");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred: " + e.Message);
                return new ResponseStatus<List<ParkModel>>
                {
                    Success = false,
                    Message = "Error occurred while saving media from Wikipedia",
                    Data = null
                };
            }

            return new ResponseStatus<List<ParkModel>>
            {
                Success = true,
                Message = "Media successfully added to the database",
                Data = parkModels
            };
        }
    }
}