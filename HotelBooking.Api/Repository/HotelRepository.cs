using HotelBooking.Api.Data;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Repository
{
    public class HotelRepository:IHotelReopsitory
    {
            private readonly AppDbContext _context;

            public HotelRepository(AppDbContext context)
            {
                _context = context;
            }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
            => await _context.Hotels.ToListAsync();
        public async Task<Hotel?> GetHotelByIdAsync(int id)
            => await _context.Hotels.FindAsync(id); 
        public async Task<Hotel?> AddHotelAsync(Hotel hotel)
            {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task UpdateHotelAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteHotelAsync(int id)
            => await _context.Hotels.FindAsync(id);
        public async Task SaaveChangesAsync()
            => await _context.SaveChangesAsync();
    }


}
