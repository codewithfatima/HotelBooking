using HotelBooking.Api.Models;

namespace HotelBooking.Api.Interface
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Room?> GetRoomByIdAsync(int id);
        Task<Room> AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int id);
        Task SaveChangesAsync();
    }
}
