using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HotelBooking.Web.Pages.Hotels
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Hotel Hotel { get; set; } = new();
        public List<Room> Room { get; set; } = new();

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task OnGet(int hotelId)
        {
            var client = _httpClientFactory.CreateClient("HotelApi");
            Hotel = await client.GetFromJsonAsync<Hotel>($"api/Hotel/{hotelId}") ?? new();
           var rooms = await client.GetFromJsonAsync<List<Room>>($"api/Room") ?? new();

            Room = rooms.Where(r => r.HotelId == hotelId).ToList();
        }
    }
}
