using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Commands
{
    public class MarkAllNotificationsAsReadCommand : IRequest<Unit>
    {
        public int ClientId { get;}
        public MarkAllNotificationsAsReadCommand(int clientId)
        {
            ClientId = clientId;
        }
    }
}
