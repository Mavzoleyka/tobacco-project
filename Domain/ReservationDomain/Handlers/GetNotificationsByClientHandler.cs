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
    public class GetNotificationsByClientHandler : IRequestHandler<GetNotificationsByClientQuery, List<NotificationDTO>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetNotificationsByClientHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationDTO>> Handle(GetNotificationsByClientQuery request, CancellationToken ct)
        {
            return await _notificationRepository.GetByClientIdAsync(request.ClientId);
        }
    }
}
