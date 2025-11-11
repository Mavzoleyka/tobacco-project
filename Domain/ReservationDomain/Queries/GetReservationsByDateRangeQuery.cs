using Domain.UserDomain.Commands.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Queries
{
    public class GetReservationsByDateRangeQuery : IRequest<List<ReservationDTO>>
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public GetReservationsByDateRangeQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
