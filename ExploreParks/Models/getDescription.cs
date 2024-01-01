namespace ExploreParks.Models
{
    public class getDescription
    {

        [Key]
        public int ParkId { get; set; }
        public string? ParkName { get; set; }
        public string? ParkDescription { get; set; }

    }
}