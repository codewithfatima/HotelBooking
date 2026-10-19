using HotelBooking.Api.Models;

namespace HotelBooking.Api.Dtos.Hotel
{
    public class UpdateHotelDtos
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
