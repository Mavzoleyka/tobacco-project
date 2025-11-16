using Domain.ReservationDomain.Notifications;
using Domain.ReservationDomain.Notifications.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Connection _connection;

        public NotificationRepository(Connection connection)
        {
            _connection = connection;
        }

        
        public async Task AddAsync(NotificationDTO dto, CancellationToken ct = default)
        {
            var entity = new Notification
            {
                Message = dto.Message,
                IsRead = dto.IsRead,
                CreatedAt = dto.CreatedAt,
                ClientId = dto.ClientId,
                ReservationId = dto.ReservationId,
                IsDelete = false
            };

            await _connection.Notifications.AddAsync(entity, ct);
            await _connection.SaveChangesAsync(ct);

            dto.Id = entity.Id; 
        }

        public async Task<List<NotificationDTO>> GetByClientIdAsync(
            int clientId,
            bool includeRead = true,
            CancellationToken ct = default)
        {
            var query = _connection.Notifications
                .AsNoTracking()
                .Where(n => n.ClientId == clientId);

            if (!includeRead)
                query = query.Where(n => !n.IsRead);

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    ClientId = n.ClientId,
                    ReservationId = n.ReservationId
                })
                .ToListAsync(ct);
        }

        public async Task<int> GetUnreadCountAsync(int clientId, CancellationToken ct = default)
        {
            return await _connection.Notifications
                .Where(n => n.ClientId == clientId && !n.IsRead)
                .CountAsync(ct);
        }

        public async Task MarkAsReadAsync(int notificationId, int clientId, CancellationToken ct = default)
        {
            var notification = await _connection.Notifications
                .FirstOrDefaultAsync(n =>
                    n.Id == notificationId &&
                    n.ClientId == clientId,
                    ct);

            if (notification == null) return;

            notification.IsRead = true;
            await _connection.SaveChangesAsync(ct);
        }

        public async Task MarkAllAsReadAsync(int clientId, CancellationToken ct = default)
        {
            var notifications = await _connection.Notifications
                .Where(n => n.ClientId == clientId && !n.IsRead)
                .ToListAsync(ct);

            if (!notifications.Any()) return;

            foreach (var n in notifications)
                n.IsRead = true;

            await _connection.SaveChangesAsync(ct);
        }
    }
}
