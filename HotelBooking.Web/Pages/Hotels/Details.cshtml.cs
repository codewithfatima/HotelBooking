using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HotelBooking.Web.Pages.Hotels
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HotelDto Hotel { get; set; } = new();
        public List<RoomDto> Rooms { get; set; } = new();

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task OnGetAsync(int hotelId)
        {
            var client = _httpClientFactory.CreateClient("HotelApi");

            var hotelResponse = await client.GetAsync($"api/hotel/{hotelId}");
            var roomResponse = await client.GetAsync($"api/room/hotel/{hotelId}");

            // check status codes
            var hotelStatus = hotelResponse.StatusCode;
            var roomStatus = roomResponse.StatusCode;

            Hotel = await hotelResponse.Content.ReadFromJsonAsync<HotelDto>() ?? new();
            Rooms = await roomResponse.Content.ReadFromJsonAsync<List<RoomDto>>() ?? new();
        }
    }
}
