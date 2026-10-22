using HotelBooking.Api.Dtos.Booking;
using HotelBooking.Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;

namespace HotelBooking.Api.Controllers
{
   
   
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController:ControllerBase
    {
        private readonly IBoookingService _bookingService;

        public BookingController(IBoookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var booking = await _bookingService.GetAllBookingsAsync();
            return Ok(booking);
        }

        [Authorize]    // only admins
        [HttpGet("{id}")]   
        public async Task<IActionResult> GetBookingAsyncById(int id)
        {
            var booking = await _bookingService.GetBookingAsyncById(id);
            if(booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }


        [Authorize]    // only admins[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var createBooking = await _bookingService.CreateBookingAsync(createBookingDto , userId);
            return Ok(new { message = "Booking created Successfully "  , createBooking });
        }


        [Authorize(Roles = "Admin")]    // only admins
        [HttpPut("{id}")]   
        public async  Task<IActionResult> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto)
        {
            var updateBooking = await _bookingService.UpdateBookingAsync(id, updateBookingDto); 

            if(updateBooking == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Booking updated successfully", updateBooking });

        }

        [Authorize(Roles = "Admin")]    // only admins
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            var deleteBooking = await _bookingService.DeleteBookingAsync(id);   

            if(!deleteBooking)
            {
                return NotFound(new { message = $"Room with id {id} not found." });
            }

            return NoContent();
        }

    }
}
