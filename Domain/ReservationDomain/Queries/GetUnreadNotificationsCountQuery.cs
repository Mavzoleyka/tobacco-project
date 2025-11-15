using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Queries
{
    public class GetUnreadNotificationsCountQuery : IRequest<int>
    {
        public int ClientId { get; set; }

        public GetUnreadNotificationsCountQuery(int clientId)
        {
            ClientId = clientId;
        }
    }
}
