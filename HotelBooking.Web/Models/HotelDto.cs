namespace HotelBooking.Web.Models
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = "";
        public decimal Rating { get; set; }
    }
}
