using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace scrapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getDescriptions : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<getDescriptions> _logger;

        public getDescriptions(AppDbContext context, ILogger<getDescriptions> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<parkModels>> GetParkDescriptions()
        {
            //await data frpm parkModels and return it
            var parkModels = await _context.parkModels.ToListAsync();
            return parkModels;
        }
        
    }
}