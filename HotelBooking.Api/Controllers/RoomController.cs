using HotelBooking.Api.Dtos.Room;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Repository;
using HotelBooking.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RoomController:ControllerBase
    {
         private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;

        public RoomController(IRoomService roomService , IHotelService hotelService )
        {
            _roomService = roomService;
            _hotelService = hotelService;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok( rooms);
        }

        [Authorize]
        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelIdAsync(int hotelId)
        {
            var room = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            if(room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [Authorize(Roles = "Admin")]    // only admins
        [HttpPost]
        public async Task<IActionResult> AddRoomAsync(CreateRoomDto createRoomDto)
        {
            var room = await _roomService.AddRoomAsync(createRoomDto);
            if(room == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoomByIdAsync), new { id = room.Id }, room); 
        }

        //Update room by id 
        [Authorize(Roles = "Admin")]    // only admins
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomAsync(int id, UpdateRoomDto dto)
        {
            var updated = await _roomService.UpdateRoomAsync(id, dto);

            if (updated == null)
                return NotFound(new { message = $"Room with id {id} not found." });

            return Ok(updated);
        }


        //get hotel by id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomByIdAsync(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);

            if(room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [Authorize(Roles = "Admin")]    // only admins
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAsync(int id)
        {
           var room = await _roomService.GetRoomByIdAsync(id);
            if(room == null)
            {
                return NotFound(new { message = $"Room with id {id} not found." });
            }
            await _roomService.DeleteRoomAsync(id);
           return NoContent();

        }
    }
}

