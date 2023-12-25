using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Models
{
    public class ParksModel
    {
        public int Id { get; set; }
        public string ParkName { get; set; }
        public string ParkLocation { get; set; }
        public string ParkType { get; set; }
        public string ParkDescription { get; set; }
        public string ParkUrl { get; set; }
        public string ParkImageUrl { get; set; }
        public float ParkArea { get; set; }
        public int ParkEstablished { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}