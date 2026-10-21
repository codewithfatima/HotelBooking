using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace HotelBooking.Web.Pages.Bookings
{
    public class MyBookingsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyBookingsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       
        public List<BookingResponseDto> Bookings { get; set; } = new();
        public async Task OnGetAsync(int roomId)
        {
            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token)) return;

            var client = _httpClientFactory.CreateClient("HotelApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Bookings = await client.GetFromJsonAsync<List<BookingResponseDto>>("api/booking") ?? new();
        }
    }
}
