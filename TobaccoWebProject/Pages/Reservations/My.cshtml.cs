using Domain.UserDomain;
using Domain.UserDomain.Commands.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Reservations
{
    [Authorize]
    public class MyModel : PageModel
    {
        private readonly IReservationRepository _repository;
        public MyModel(IReservationRepository repository)
        {
            _repository = repository;
        }

        public List<ReservationDTO> Reservations { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var clientIdClaim = User.FindFirst("ClientId");
            if (clientIdClaim == null)
            {
                TempData["ErrorMessage"] = "Не удалось определить пользователя. Пожалуйста, войдите снова.";
                return RedirectToPage("/Account/Login");
            }

            int clientId = int.Parse(clientIdClaim.Value);
            Reservations = await _repository.GetReservationsByClientIdAsync(clientId);

            return Page();
        }
    }
}
