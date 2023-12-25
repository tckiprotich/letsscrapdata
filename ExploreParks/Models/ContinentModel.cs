using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Models
{
    public class ContinentModel
    {
        public int ContinentId { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }

    }
}