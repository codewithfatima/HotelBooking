using HotelBooking.Api.Dtos.Register;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json; // Required for PostAsJsonAsync and ReadFromJsonAsync
using Microsoft.AspNetCore.Http;

namespace HotelBooking.Web.Pages.Auth
{
    public class RegisterModel:PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public RegisterDto RegisterDto { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("HotelApi");
            var response = await client.PostAsJsonAsync("api/auth/register", RegisterDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                if (result != null)
                {
                    HttpContext.Session.SetString("token", result.Token);
                    HttpContext.Session.SetString("username", result.UserName);
                    HttpContext.Session.SetString("role", result.Role.ToString());
                }

                return RedirectToPage("/Auth/Login");
            }
            var errorDetails = await response.Content.ReadAsStringAsync();
            ErrorMessage = $"Registration failed: {errorDetails}";
            return Page();
        }

    }
}
