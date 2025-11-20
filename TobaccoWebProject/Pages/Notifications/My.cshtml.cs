using Domain.ReservationDomain.Commands;
using Domain.ReservationDomain.Notifications.Object;
using Domain.ReservationDomain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Notifications
{
    public class MyModel : PageModel
    {
        private readonly IMediator _mediator;

        public MyModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<NotificationDTO> Notifications { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var clientIdClaim = User.FindFirst("ClientId");
            if (clientIdClaim == null)
            {
                TempData["ErrorMessage"] = "Не удалось определить пользователя. Пожалуйста, войдите снова.";
                return RedirectToPage("/Account/Login");
            }

            int clientId = int.Parse(clientIdClaim.Value);
            Notifications = await _mediator.Send(new GetNotificationsByClientQuery { ClientId = clientId });

            return Page();
        }
        public async Task<IActionResult> OnPostMarkAsReadAsync(int NotificationId)
        {
            var clientIdClaim = User.FindFirst("ClientId");
            if (clientIdClaim == null)
                return RedirectToPage("/Account/Login");

            int clientId = int.Parse(clientIdClaim.Value);

            await _mediator.Send(
            new MarkNotificationAsReadCommand(NotificationId, clientId)
);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostMarkAllAsync()
        {
            var clientIdClaim = User.FindFirst("ClientId");
            if (clientIdClaim == null)
                return RedirectToPage("/Account/Login");

            int clientId = int.Parse(clientIdClaim.Value);

            await _mediator.Send(new MarkAllNotificationsAsReadCommand(clientId));

            return RedirectToPage();
        }
    }
}
