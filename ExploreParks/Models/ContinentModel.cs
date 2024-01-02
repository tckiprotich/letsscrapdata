using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Models
{
    public class ContinentModel
    {
        [Key]
        public Guid ContinentId { get; set; }
        public string? ContinentName { get; set; }
        public List<CountryModel> Countries { get; set; } = new List<CountryModel>();


    }
}