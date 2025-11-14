using Domain.ReservationDomain.Notifications.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Notifications
{
    public interface INotificationRepository
    {
        public Task<List<NotificationDTO>> GetByClientIdAsync(int clientId,
            bool includeRead = true,
            CancellationToken ct = default);
        public Task AddAsync(NotificationDTO notification, CancellationToken ct = default);
        public Task<int> GetUnreadCountAsync(int clientId, CancellationToken ct = default);
        public Task MarkAsReadAsync(int notificationId, int clientId, CancellationToken ct = default);
        public Task MarkAllAsReadAsync(int clientId, CancellationToken ct = default);
    }
}
