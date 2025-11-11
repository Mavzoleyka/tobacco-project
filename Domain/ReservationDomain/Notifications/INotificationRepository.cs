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
        public Task<List<NotificationDTO>> GetByClientIdAsync(int clientId);
        public Task AddAsync(NotificationDTO notification);
        public Task MarkAsReadAsync(int id);
    }
}
