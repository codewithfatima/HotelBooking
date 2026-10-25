using HotelBooking.Api.Dtos.Booking;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repository;

namespace HotelBooking.Api.Service
{
    public class BookingService:IBoookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository , IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;

        }

        public async Task<List<ResponseBookingDto>> GetAllBookingsAsync()
        {
            var booking = await _bookingRepository.GetAllBookingsAsync();

            var result = booking.Select(booking => new ResponseBookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                PricePerNight = booking.PricePerNight,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,

            }).ToList();
            return result;
        }
        public async Task<ResponseBookingDto> GetBookingAsyncById(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if(booking == null)
            {
                return null;
            }

            return new ResponseBookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                PricePerNight = booking.PricePerNight,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
            };
        }
        public async Task<ResponseBookingDto?> CreateBookingAsync(CreateBookingDto createBookingDto, int userId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(createBookingDto.RoomId);
            if (room == null) return null;

            var nights = createBookingDto.CheckOutDate.DayNumber - createBookingDto.CheckInDate.DayNumber;
            var pricePerNight = room.Price;
            var totalPrice = pricePerNight * nights;

            var booking = new Booking
            {
                RoomId = createBookingDto.RoomId,
                CheckInDate = createBookingDto.CheckInDate,
                CheckOutDate = createBookingDto.CheckOutDate,
                UserId =userId,
                PricePerNight = pricePerNight,
                TotalPrice = totalPrice,
                CreatedAt = DateTime.UtcNow
            };
      

            await _bookingRepository.AddBooking(booking);
            await _bookingRepository.SaveChangesAsync();

            return new ResponseBookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                PricePerNight = booking.PricePerNight,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
            };
        }
        public async Task<ResponseBookingDto> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if(booking == null)
            {
                return null;
            }

            booking.CheckInDate = updateBookingDto.CheckInDate;
            booking.CheckOutDate = updateBookingDto.CheckOutDate;

            await _bookingRepository.UpdateBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync  ();

            return new ResponseBookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                PricePerNight = booking.PricePerNight,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
            };

        }
        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if(booking == null)
            {
                return false;
            }   

            await _bookingRepository.DeleteBookingAsync(id);
            await _bookingRepository.SaveChangesAsync();
            return true;
        }

    }
}
