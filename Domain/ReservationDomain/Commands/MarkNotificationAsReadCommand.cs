using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Commands
{
    public class MarkNotificationAsReadCommand : IRequest<Unit>
    {
        public int NotificationId { get; }
        public int ClientId { get;  }

        public MarkNotificationAsReadCommand(int notificationId, int clientId)
        {
            NotificationId = notificationId;
            ClientId = clientId;
        }
    }
}
