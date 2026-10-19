using HotelBooking.Api.Dtos.Register;
using HotelBooking.Api.Enum;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HotelBooking.Api.Service
{
    public class AuthService : IAuthService

    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var userAlreadyExists = await _userRepository.EmailExistsAsync(registerDto.Email);

            if (userAlreadyExists)
            {
                throw new Exception("User with this email already exists.");
            }
            // REGISTER: generate salt and hash
            using var hmac = new HMACSHA512();
            byte[] salt = hmac.Key;
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

            // Step 3: create user
            var newUsers = new User
            {
                UserEmail = registerDto.Email,
                UserName = registerDto.FullName,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = UserRole.Guest,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddUserAsync(newUsers);
            await _userRepository.SaveChangesAsync();

            var token = GenerateJwtToken(newUsers);

            return new AuthResponseDto
            {
                Token = token,
                UserName = newUsers.UserName,
                Role = newUsers.Role.ToString()
            };

        }

        //logindto 
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
                return null;

            // Step 2: verify password
            using var hmac  = new HMACSHA512(user.PasswordSalt);    // generates random salt
            var computedHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(loginDto.Password)
            );
            if (!computedHash.SequenceEqual(user.PasswordHash))
                return null;

           
            // Step 3: generate token and return
            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Role = user.Role.ToString()
            };
        }
        private string GenerateJwtToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Role , user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

 
    }
}
