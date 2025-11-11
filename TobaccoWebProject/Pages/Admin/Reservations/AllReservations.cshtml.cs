using Domain.ReservationDomain.Commands;
using Domain.ReservationDomain.Queries;
using Domain.ReservationDomain.Queries.Object;
using Domain.UserDomain.Commands.Object;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Admin.Reservations
{
    public class AllReservationsModel : PageModel
    {
        private readonly IMediator _mediator;

        public AllReservationsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<ReservationDTO> Reservations { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public ReservationStatus? CurrentStatus { get; set; }

        [BindProperty]
        public int ReservationId { get; set; }

        [BindProperty]
        public ReservationStatus NewStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public async Task OnGetAsync()
        {
            Reservations = await _mediator.Send(new GetReservationsQuery
            {
                Status = CurrentStatus,
                StartDate = StartDate,
                EndDate = EndDate
            });
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            try
            {
                var userName = User.Identity?.Name ?? "Unknown";
                await _mediator.Send(new UpdateReservationStatusCommand(ReservationId, NewStatus, userName));
                TempData["SuccessMessage"] = "Статус успешно обновлён!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex}");
                TempData["ErrorMessage"] = "Произошла ошибка при обновлении статуса.";
            }
            return RedirectToPage();
        }
    }
}
