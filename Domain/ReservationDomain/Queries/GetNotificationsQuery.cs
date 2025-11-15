using Domain.ReservationDomain.Notifications.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Queries
{
    public class GetNotificationsQuery : IRequest<List<NotificationDTO>>
    {
        public int ClientId { get; set; }

        public GetNotificationsQuery(int clientId)
        {
            ClientId = clientId;
        }
    }
}
