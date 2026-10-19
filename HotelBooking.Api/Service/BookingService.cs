using HotelBooking.Api.Dtos.Booking;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Service
{
    public class BookingService:IBoookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
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
            var booking = new Booking
            {
                RoomId = createBookingDto.RoomId,
                CheckInDate = createBookingDto.CheckInDate,
                CheckOutDate = createBookingDto.CheckOutDate,
            };

            await  _bookingRepository.AddBooking(booking);
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
