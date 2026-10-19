using HotelBooking.Api.Dtos.Hotel;
using HotelBooking.Api.Interface;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repository;

namespace HotelBooking.Api.Service
{
    public class HotelService:IHotelService
    {
        private readonly IHotelReopsitory _hotelReopsitory;

        public HotelService(IHotelReopsitory hotelReopsitory)
        {
            _hotelReopsitory = hotelReopsitory;
        }

        public async Task<List<ResponseHotelDtos>> GetAllHotelAsync()
        {
            var hotels = await _hotelReopsitory.GetHotelsAsync();

            var response = hotels.Select(h => new ResponseHotelDtos
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                Image = h.Image,
                Rating = h.Rating
            }).ToList();

            return response;
        }


        public async Task<ResponseHotelDtos?> GetHotelByIdAsync(int id)
        {
            var hotel = await _hotelReopsitory.GetHotelByIdAsync(id);

            if (hotel == null)
            {
                return null;
            }

            return new ResponseHotelDtos
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Image = hotel.Image,
                Rating = hotel.Rating
            };
        }
        public async Task<ResponseHotelDtos?> AddHotelAsync(CreateHotelDtos createHotelDtos)
        {
            var hotel = new Hotel
            {
                Name = createHotelDtos.Name,
                Description = createHotelDtos.Description,
                Image = createHotelDtos.Image,
            };

             await _hotelReopsitory.AddHotelAsync(hotel);
             await _hotelReopsitory.SaaveChangesAsync();


           return new ResponseHotelDtos
           {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Image = hotel.Image,
                Rating = hotel.Rating
            };

        }
       public async Task<ResponseHotelDtos?> UpdateHotelAsync(UpdateHotelDtos updateHotelDtos)
        {
            var exisitingHotel = await _hotelReopsitory.GetHotelByIdAsync(updateHotelDtos.Id);

            if(exisitingHotel == null)
            {
                throw new Exception($"Hotel with ID {updateHotelDtos.Id} not found.");
            }

            exisitingHotel.Id = updateHotelDtos.Id;
            exisitingHotel.Name = updateHotelDtos.Name;
            exisitingHotel.Description = updateHotelDtos.Description;
            exisitingHotel.Image = updateHotelDtos.Image;

            await _hotelReopsitory.UpdateHotelAsync(exisitingHotel);
            await _hotelReopsitory.SaaveChangesAsync();

            return new ResponseHotelDtos
            {
                Id = exisitingHotel.Id,
                Name = exisitingHotel.Name,
                Description = exisitingHotel.Description,
                Image = exisitingHotel.Image,
                Rating = exisitingHotel.Rating
            };


        }
      public async  Task DeleteHotelAsync(int id)
        {
            var hotel = await _hotelReopsitory.GetHotelByIdAsync(id);

            if(hotel == null)
            {
                throw new Exception($"Hotel with ID {id} not found.");
            }
            await _hotelReopsitory.DeleteHotelAsync(id);
            await _hotelReopsitory.SaaveChangesAsync(); 
        }
    }
}
