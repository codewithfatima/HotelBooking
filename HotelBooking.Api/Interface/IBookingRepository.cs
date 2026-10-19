using HotelBooking.Api.Models;

namespace HotelBooking.Api.Interface
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking> AddBooking(Booking booking); 
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task SaveChangesAsync();
    }
}
