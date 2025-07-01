using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("Cookies");
            return RedirectToPage("/Index");
        }

        //public async Task<IActionResult> OnGet()
        //{
        //    await HttpContext.SignOutAsync("Cookies");
        //    return RedirectToPage("/Index");
        //}
    }
}
