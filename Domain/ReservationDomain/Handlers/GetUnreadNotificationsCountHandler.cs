using Domain.ReservationDomain.Notifications;
using Domain.ReservationDomain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Handlers
{
    public class GetUnreadNotificationsCountHandler : IRequestHandler<GetUnreadNotificationsCountQuery, int>
    {
        private readonly INotificationRepository _notificationRepository;
        public GetUnreadNotificationsCountHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<int> Handle(GetUnreadNotificationsCountQuery request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetUnreadCountAsync(request.ClientId, cancellationToken);
        }
    }
}
