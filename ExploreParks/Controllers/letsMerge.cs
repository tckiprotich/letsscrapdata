using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExploreParks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class letsMerge : ControllerBase
    {
        private readonly IExploreParksInterface _exploreParksInterface;
        private readonly ExploreParksDbContext _context;

        public letsMerge(IExploreParksInterface exploreParksInterface, ExploreParksDbContext context)
        {
            _exploreParksInterface = exploreParksInterface;
            _context = context;
        }

        [HttpGet]
        // Merging getDescription Models to Park Models
        public async Task<IActionResult> GetMergedData()
        {
            var ParkDescription = _context.Descriptions.ToList();
            var parkModels = _context.Parks.ToList();

            foreach (var description in ParkDescription)
            {
                var parkModel = parkModels.FirstOrDefault(p => p.ParkName == description.ParkName);
                if (parkModel != null)
                {
                    parkModel.ParkDescription = description.ParkDescription;
                    parkModel.Latitude = description.ParkLatitude;
                    parkModel.Longitude = description.ParkLongitude;
                    parkModel.NearestCity = description.NearestCity;
                    parkModel.URL = description.ContentUrls;
                    parkModel.Language = description.Language;

                }
            }
            // await _context.Parks.SaveChangesAsync(parkModels);
            await _context.SaveChangesAsync();


            return Ok(parkModels);
        }
    }
}