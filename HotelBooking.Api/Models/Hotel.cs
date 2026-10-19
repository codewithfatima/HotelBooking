namespace HotelBooking.Api.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Rating { get; set; } = 0;
        // One Hotel has many Room
        public List<Room> Rooms { get; set; } = new();
    }
}
