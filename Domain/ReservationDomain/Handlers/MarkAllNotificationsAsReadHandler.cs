using Domain.ReservationDomain.Commands;
using Domain.ReservationDomain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Handlers
{
    public class MarkAllNotificationsAsReadHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, Unit>
    {
        private readonly INotificationRepository _repo;
        private readonly ILogger<MarkAllNotificationsAsReadHandler> _logger;

        public MarkAllNotificationsAsReadHandler(INotificationRepository repo,
            ILogger<MarkAllNotificationsAsReadHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Unit> Handle(MarkAllNotificationsAsReadCommand request,
            CancellationToken ct)
        {
            _logger.LogInformation(
            "Client {ClientId} called MarkAllAsRead at {Time}",
            request.ClientId,
            DateTime.UtcNow);

            try
            {
                await _repo.MarkAllAsReadAsync(request.ClientId, ct);
                _logger.LogInformation(
                    "Successfully marked all notifications as read for Client {ClientId}",
                    request.ClientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to mark all notifications as read for Client {ClientId}",
                    request.ClientId);

                throw;
            }

            return Unit.Value;
        }
    }
}
