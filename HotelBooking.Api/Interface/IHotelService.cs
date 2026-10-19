using HotelBooking.Api.Dtos.Hotel;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Interface
{
    public interface IHotelService
    {
        Task<List<ResponseHotelDtos>> GetAllHotelAsync();
        Task<ResponseHotelDtos?> GetHotelByIdAsync(int id);
        Task<ResponseHotelDtos?> AddHotelAsync(CreateHotelDtos createHotelDtos);
        Task<ResponseHotelDtos?> UpdateHotelAsync(UpdateHotelDtos updateHotelDtos);
        Task DeleteHotelAsync(int id);
    }
}
