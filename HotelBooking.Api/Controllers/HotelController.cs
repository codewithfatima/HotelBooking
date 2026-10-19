using HotelBooking.Api.Dtos.Hotel;
using HotelBooking.Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController:ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
     
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotelAsync();
            return Ok(hotels);
        }

        [HttpGet("{id}")]   
        public async Task<IActionResult> GetHotelByIdAsync(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if(hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        
        [Authorize(Roles = "Admin")]    // only admins
        [HttpPost]
        public async Task<IActionResult> AddHotelAsync(CreateHotelDtos createHotelDtos)
        {
            var hotel = await _hotelService.AddHotelAsync(createHotelDtos);
            return Ok(new { message = "Hotel added successfully", hotel });

        }

    }
}
