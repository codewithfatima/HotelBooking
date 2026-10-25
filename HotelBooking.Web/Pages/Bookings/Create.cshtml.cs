using HotelBooking.Api.Models;
using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HotelBooking.Web.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public RoomDto Room { get; set; } = new();

        [BindProperty]
        public CreateBookingDto CreateBooking { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int RoomId { get; set; }
        public string? ErrorMessage { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            if(RoomId <= 0)
            {
                return RedirectToPage("/Index");
            }
            await LoadFromDataAsync(RoomId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Auth/Login");

            var client = _httpClientFactory.CreateClient("HotelApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CreateBooking.RoomId = RoomId;

            var response = await client.PostAsJsonAsync("api/booking", CreateBooking);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Bookings/MyBookings");

            //  THIS WILL SHOW YOU THE EXACT API ERROR ON YOUR WEBPAGE
            var errorContent = await response.Content.ReadAsStringAsync();
            ErrorMessage = $"API Error ({response.StatusCode}): {errorContent}";

            await LoadFromDataAsync(RoomId);
            return Page();
        }

        private async Task LoadFromDataAsync(int id)
        {
           
            var client = _httpClientFactory.CreateClient("HotelApi");
            var roomResponse = await client.GetAsync($"api/room/{id}");

            if (roomResponse.IsSuccessStatusCode)
            {
                Room = await roomResponse.Content.ReadFromJsonAsync<RoomDto>() ?? new();
            }
        }


    }
}
