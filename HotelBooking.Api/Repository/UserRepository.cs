using HotelBooking.Api.Data;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> EmailExistsAsync(string email)
            => await _context.Users.AnyAsync(u => u.UserEmail == email);

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

    }
}
