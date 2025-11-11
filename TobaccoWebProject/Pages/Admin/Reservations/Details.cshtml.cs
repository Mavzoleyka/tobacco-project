using Domain.UserDomain;
using Domain.UserDomain.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Admin.Reservations
{
    public class DetailsModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public DetailsModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public ReservationDTO Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _reservationRepository.GetReservationByIdAsync(id);

            if (Reservation == null)
                return NotFound();

            return Page();
        }
    }
}
