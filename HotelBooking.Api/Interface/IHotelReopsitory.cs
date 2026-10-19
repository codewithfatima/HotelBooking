using HotelBooking.Api.Models;

namespace HotelBooking.Api.Interface
{
    public interface IHotelReopsitory
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel?> GetHotelByIdAsync(int id);
        Task<Hotel?> AddHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(int id);
        Task SaaveChangesAsync();
    }
}
