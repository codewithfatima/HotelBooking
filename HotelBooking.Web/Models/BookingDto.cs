namespace HotelBooking.Web.Models
{
    public class CreateBookingDto
    {
        public int RoomId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
    }
}