using Domain.ReservationDomain.Queries;
using Domain.UserDomain;
using Domain.UserDomain.Commands.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Handlers
{
    public class GetAllReservationsHandler : IRequestHandler<GetAllReservationsQuery, List<ReservationDTO>>
    {
        private readonly IReservationRepository _repo;

        public GetAllReservationsHandler(IReservationRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ReservationDTO>> Handle(GetAllReservationsQuery request, CancellationToken ct)
        {
            return await _repo.GetAllReservationsAsync();
        }
    }
}
