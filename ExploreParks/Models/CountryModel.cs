using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Models
{
    public class CountryModel
    {
        [Key]
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        public List<ParkModel> Parks { get; set; } = new List<ParkModel>();
    }
}