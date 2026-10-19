using HotelBooking.Api.Models;

namespace HotelBooking.Api.Interface
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
