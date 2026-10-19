using HotelBooking.Api.Dtos.Booking;

namespace HotelBooking.Api.Interface
{
    public interface IBoookingService
    {
       Task<List<ResponseBookingDto>> GetAllBookingsAsync();
       Task<ResponseBookingDto> GetBookingAsyncById(int id);
        Task<ResponseBookingDto?> CreateBookingAsync(CreateBookingDto createBookingDto , int userId);
        Task<ResponseBookingDto> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto);
        Task<bool> DeleteBookingAsync( int id);

    }
}
