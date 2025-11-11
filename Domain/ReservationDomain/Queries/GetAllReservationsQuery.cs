using Domain.UserDomain.Commands.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Queries
{
    public class GetAllReservationsQuery : IRequest<List<ReservationDTO>>
    {

    }
}
