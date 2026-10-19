using HotelBooking.Api.Enum;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Dtos.Booking
{
    public class ResponseBookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId{ get; set; }   // nwhich room it was
        public decimal PricePerNight { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; } 
        public BookingStatus Status { get; set; }   // this is a PROPERTY

    }
}
