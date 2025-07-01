using AuthDomain.Querys.Object;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Security.Claims;

namespace TobaccoWebProject.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IQueryService<User, ClaimsPrincipal> _createPrincipalService;
        private readonly IQueryService<EntryDTO, User?> _entryService;

        public LoginModel(IQueryService<User, ClaimsPrincipal> createPrincipalService,
                          IQueryService<EntryDTO, User?> entryService)
        {
            ArgumentNullException.ThrowIfNull(createPrincipalService, nameof(createPrincipalService));
            ArgumentNullException.ThrowIfNull(entryService, nameof(entryService));
            _createPrincipalService = createPrincipalService;
            _entryService = entryService;
        }

        [BindProperty]
        public EntryDTO Input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            User? user = _entryService.Execute(Input);
            if(user != null)
            {
                var principal = _createPrincipalService.Execute(user);
                await HttpContext.SignInAsync("Cookies", principal);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
