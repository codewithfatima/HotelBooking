using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace HotelBooking.Web.Pages.Hotels
{
    public class IndexModel:PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Hotel> Hotels { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("HotelApi");
            Hotels = await client.GetFromJsonAsync<List<Hotel>>("api/hotel") ?? new();
        }
    }
}
