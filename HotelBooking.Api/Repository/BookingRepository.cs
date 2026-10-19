using HotelBooking.Api.Data;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Repository
{
    public class BookingRepository:IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

       public async Task<List<Booking>> GetAllBookingsAsync()
            => await _context.Bookings.ToListAsync();
       public async Task<List<Booking>> GetBookingsByUserIdAsync(int userId)
            => await _context.Bookings.Where(b => b.UserId == userId).ToListAsync(); 
        public async Task<Booking?> GetBookingByIdAsync(int id)
            => await _context.Bookings.FindAsync(id);
        public async Task<Booking> AddBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async  Task<Booking> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
             await _context.SaveChangesAsync();
             return booking;    
        }

        public async  Task<bool> DeleteBookingAsync(int id)
        {
           var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;

        }
       public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
