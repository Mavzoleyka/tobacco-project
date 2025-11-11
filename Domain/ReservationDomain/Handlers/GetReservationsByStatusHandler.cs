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
    public class GetReservationsByStatusHandler : IRequestHandler<GetReservationsByStatusQuery, List<ReservationDTO>>
    {
        private readonly IReservationRepository _reservationRepository;
        public GetReservationsByStatusHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<List<ReservationDTO>> Handle(GetReservationsByStatusQuery request, CancellationToken cancellationToken)
        {
            return await _reservationRepository.GetReservationsByStatusAsync(request.Status);
        }
    }
}
