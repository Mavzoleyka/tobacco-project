using Domain.ReservationDomain.Queries;
using Domain.ReservationDomain.Queries.Object;
using Domain.UserDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Handlers
{
    public class GetUserReservationsHandler : IRequestHandler<GetUserReservationsQuery, List<ReservationPreviewDTO>>
    {
        private readonly IReservationRepository _repository;
        public GetUserReservationsHandler(IReservationRepository repository)
            => _repository = repository;

        public async Task<List<ReservationPreviewDTO>> Handle(GetUserReservationsQuery request, CancellationToken ct)
        {
            var list = await _repository.GetReservationsByClientIdAsync(request.ClientId);

            return list.Select(r => new ReservationPreviewDTO
            {
                Id = r.Id,
                PickupDateTime = r.PickupDateTime,
                Comment = r.Comment,
                Status = r.ReservationStatus.ToString()
            })
            .OrderByDescending(x => x.PickupDateTime)
            .ToList();
        }

    }
}
