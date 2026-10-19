using Microsoft.EntityFrameworkCore;
using HotelBooking.Api.Enum;

namespace HotelBooking.Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public  int UserId  { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public BookingStatus Status { get; set; } = BookingStatus.Pending;  // this is a PROPERTY
        public int RoomId { get; set; } // foreign key to the room

        public Room Room { get; set; } = null!;  // navigation property to the room

    }
}

