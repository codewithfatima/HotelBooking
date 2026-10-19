using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBooking.Web.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");
            return  RedirectToPage("/Hotels/Index");
        }
    }
}
