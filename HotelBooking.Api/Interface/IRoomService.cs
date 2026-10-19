using HotelBooking.Api.Models;
using HotelBooking.Api.Dtos.Room;
namespace HotelBooking.Api.Interface
{
    public interface IRoomService
    {
        Task<List<ResponseRoomDto>> GetAllRoomsAsync();
        Task<List<ResponseRoomDto>> GetRoomsByHotelIdAsync(int hotelId);
        Task<ResponseRoomDto?> GetRoomByIdAsync(int id);
        Task<ResponseRoomDto?> AddRoomAsync(CreateRoomDto createRoomDto);
        Task<ResponseRoomDto?> UpdateRoomAsync(int id, UpdateRoomDto dto);
        Task<bool> DeleteRoomAsync(int id);
    }
}
