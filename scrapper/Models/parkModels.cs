using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrapper.Models
{
    public class parkModels
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? Area { get; set; }
        public string? Established { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? ImageUrl { get; set; }
        public List<string>? Activities { get; set; }
        public List<string>? OperatingHours { get; set; }
        public string? EntranceFees { get; set; }
        public string? WeatherInfo { get; set; }
        public string? DirectionsInfo { get; set; }
        public string? DirectionsUrl { get; set; }
        public string? ContactInfo { get; set; }
    }
}
