using HotelBooking.Api.Dtos.Register;
using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace HotelBooking.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public LoginDto LoginDto { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("HotelApi");
            var response = await client.PostAsJsonAsync("api/auth/login", LoginDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<AuthResponseDto>();

                HttpContext.Session.SetString("token", result!.Token);
                HttpContext.Session.SetString("username", result.UserName);
                HttpContext.Session.SetString("role", result.Role.ToString());

                return RedirectToPage("/Hotels/Index");
            }
            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}