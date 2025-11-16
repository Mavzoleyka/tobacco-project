using Domain.ReservationDomain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TobaccoWebProject.Pages.Shared.Components.NotificationBadge
{
    public class NotificationBadgeViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public NotificationBadgeViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claim = HttpContext.User.FindFirst("ClientId");
            if (claim == null)
                return View(0);

            int clientId = int.Parse(claim.Value);

            int unreadCount = await _mediator.Send(
                new GetUnreadNotificationsCountQuery(clientId) );

            return View(unreadCount);
        }
    }
}
