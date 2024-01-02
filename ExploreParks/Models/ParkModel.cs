global using System.ComponentModel.DataAnnotations;

namespace ExploreParks.Models;


public class ParkModel
{
    [Key]
    public Guid ParkId { get; set; }
    public string? ParkName { get; set; }
    public string? ParkDescription { get; set; }
    public string? ParkLocation { get; set; }
    // public string? ContentUrls { get; set; }
    public string? NearestCity { get; set; }
    public string? Language { get; set; }
    public string? ParkImage { get; set; }
    public string? URL { get; set; }
    public string? ParkArea { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? ParkEstablished { get; set; }
}