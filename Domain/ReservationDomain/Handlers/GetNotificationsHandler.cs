using Domain.ReservationDomain.Notifications;
using Domain.ReservationDomain.Notifications.Object;
using Domain.ReservationDomain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Handlers
{
    public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDTO>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetNotificationsHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<List<NotificationDTO>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetByClientIdAsync(request.ClientId, includeRead: true, cancellationToken);
        }
    }
}
