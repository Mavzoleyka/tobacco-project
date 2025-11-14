using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Notifications.Object
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClientId { get; set; }
        public int? ReservationId { get; set; }
    }
}
