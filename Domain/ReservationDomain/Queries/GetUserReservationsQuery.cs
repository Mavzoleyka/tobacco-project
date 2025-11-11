using Domain.ReservationDomain.Queries.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Queries
{
    public class GetUserReservationsQuery : IRequest<List<ReservationPreviewDTO>>
    {
        public int ClientId { get; }
        public GetUserReservationsQuery(int clientId) => ClientId = clientId;
    }
}
