using HotelBooking.Api.Dtos.Hotel;
using HotelBooking.Api.Dtos.Room;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotelBooking.Api.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelService _hotelService;


        public RoomService(IRoomRepository roomRepository, IHotelService hotelService)
        {
            _roomRepository = roomRepository;
            _hotelService = hotelService;
        }

        public async Task<List<ResponseRoomDto>> GetAllRoomsAsync()
        {
            var room = await _roomRepository.GetAllRoomsAsync();

            return room.Select(MapToResponse).ToList();
        }

        public async Task<List<ResponseRoomDto>> GetRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            return rooms.Select(MapToResponse).ToList();

        }
        public async Task<ResponseRoomDto?> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);

            if (room == null)
            {
                return null;
            }

            return MapToResponse(room);

        }
        public async Task<ResponseRoomDto?> AddRoomAsync(CreateRoomDto createRoomDto)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(createRoomDto.HotelId);

            if (hotel == null)
            {
                return null;
            }

            var room = new Room
            {
                RoomNumber = createRoomDto.RoomNumber,
                Capacity = createRoomDto.Capacity,
                RoomType = createRoomDto.RoomType,
                Description = createRoomDto.Description,
                Price = createRoomDto.Price,
                Image = createRoomDto.Image,
                HotelId = createRoomDto.HotelId
            };

            await _roomRepository.AddRoomAsync(room);
            await _roomRepository.SaveChangesAsync();

            return MapToResponse(room);
        }
        public async Task<ResponseRoomDto?> UpdateRoomAsync(int id, UpdateRoomDto dto)
        {
            var exisitingRoom = await _roomRepository.GetRoomByIdAsync(id);

            if (exisitingRoom == null)
            {
                return null;
            }

            exisitingRoom.RoomNumber = dto.RoomNumber;
            exisitingRoom.Price = dto.Price;
            exisitingRoom.Description = dto.Description;
            exisitingRoom.Image = dto.Image;

            await _roomRepository.UpdateRoomAsync(exisitingRoom);
            return MapToResponse(exisitingRoom);

        }
        public async Task<bool> DeleteRoomAsync(int id)
      => await _roomRepository.DeleteRoomAsync(id);


        private static ResponseRoomDto MapToResponse(Room r) => new ResponseRoomDto
            {
             Id = r.Id,
            RoomNumber = r.RoomNumber,
            Capacity = r.Capacity,
            RoomType = r.RoomType,
            Description = r.Description,
            Price = r.Price,
            Image = r.Image,
            HotelId = r.HotelId
    };

    }
}
