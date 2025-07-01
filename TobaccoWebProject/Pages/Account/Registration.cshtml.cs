using AuthDomain.Querys.Object;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace TobaccoWebProject.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        private readonly IQueryService<RegistrationDTO, User?> _registrationService;
        private readonly IQueryService<User, ClaimsPrincipal> _createPrincipalService;

        public RegistrationModel(IQueryService<RegistrationDTO, User?> registrationService,
                                 IQueryService<User, ClaimsPrincipal> createPrincipalService)
        {
            ArgumentNullException.ThrowIfNull(registrationService, nameof(registrationService));
            ArgumentNullException.ThrowIfNull(createPrincipalService, nameof(createPrincipalService));
            _registrationService = registrationService;
            _createPrincipalService = createPrincipalService;
        }
        [BindProperty]
        public RegistrationDTO Input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            User? user = _registrationService.Execute(Input);
            if(user != null)
            {
                ClaimsPrincipal principal = _createPrincipalService.Execute(user);
                await HttpContext.SignInAsync("Cookies", principal);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
