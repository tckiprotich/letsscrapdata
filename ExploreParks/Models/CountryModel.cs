using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Models
{
    public class CountryModel
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public List<Park> Parks { get; set; }
    }
}