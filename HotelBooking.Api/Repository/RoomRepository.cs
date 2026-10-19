using HotelBooking.Api.Data;
using HotelBooking.Api.Dtos.Room;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Repository
{
    public class RoomRepository:IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<List<Room>> GetAllRoomsAsync()
            => await _context.Rooms.ToListAsync();
        public async Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId)
           => await _context.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();

        public async  Task<Room?> GetRoomByIdAsync(int id) 
            => await _context.Rooms.FindAsync(id);  
        public async Task<Room> AddRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task UpdateRoomAsync(Room room) 
        { 
            _context.Rooms.Update(room);  
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }

        public async  Task SaveChangesAsync()
          =>await _context.SaveChangesAsync();

    }
}
