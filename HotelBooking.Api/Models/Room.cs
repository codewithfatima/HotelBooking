using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Models
{
    public class Room
    {
        public int Id { get; set; }   
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;

        // Foreign key
        public int HotelId { get; set; }        

        public Hotel Hotel { get; set; } = null!;  // navigation property to the hotel
    }
}

