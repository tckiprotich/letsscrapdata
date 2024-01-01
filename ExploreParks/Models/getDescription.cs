namespace ExploreParks.Models
{
    public class getDescription
    {

        [Key]
        public int ParkId { get; set; }
        public string? ParkName { get; set; }
        public string? ParkDescription { get; set; }
        public string? ParkLatitude { get; set; }
        public string? ParkLongitude { get; set; }
        public string? NearestCity { get; set; }
        public string? ContentUrls { get; set; }
        public string? Language { get; set; }

    }
}