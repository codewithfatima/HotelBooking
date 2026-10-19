using HotelBooking.Api.Enum;

namespace HotelBooking.Api.Dtos.Booking
{
    public class CreateBookingDto
    {
   
        public int RoomId { get; set; }   // nwhich room it was
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
     
    }
}
