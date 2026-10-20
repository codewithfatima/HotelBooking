using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Web.Models
{
    public class RoomDto
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public int HotelId { get; set; }
    }
}
