using HotelBooking.Api.Enum;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName  { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public UserRole Role { get; set; } = UserRole.Guest; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Booking> Bookings { get; set; } = new();

    }
}

