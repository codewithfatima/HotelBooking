namespace HotelBooking.Web.Models
{
    public class BookingResponseDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int Status { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}