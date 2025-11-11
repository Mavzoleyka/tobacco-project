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
        private readonly Connection _db;

        public NotificationRepository(Connection db)
        {
            _db = db;
        }

        public async Task<List<NotificationDTO>> GetByClientIdAsync(int clientId)
        {
            return await _db.Notifications
                .Where(n => n.ClientId == clientId && !n.IsDelete)
                .Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    ReservationId = n.ReservationId
                })
                .ToListAsync();
        }

        public async Task AddAsync(NotificationDTO dto)
        {
            var entity = new Notification
            {
                Message = dto.Message,
                IsRead = dto.IsRead,
                CreatedAt = dto.CreatedAt,
                ClientId = dto.ReservationId.HasValue ? _db.Reservations
                    .Where(r => r.Id == dto.ReservationId)
                    .Select(r => r.ClientId)
                    .FirstOrDefault() : 0,
                ReservationId = dto.ReservationId
            };

            _db.Notifications.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var entity = await _db.Notifications.FindAsync(id);
            if (entity != null)
            {
                entity.IsRead = true;
                await _db.SaveChangesAsync();
            }
        }
    }
}
