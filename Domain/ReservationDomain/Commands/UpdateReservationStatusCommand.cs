using Domain.UserDomain.Commands.Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Commands
{
    public class UpdateReservationStatusCommand : IRequest<Unit>
    {
        public int ReservationId { get; set; }
        public ReservationStatus NewStatus { get; set; }
        public string? ChangedBy { get; set; }

        public UpdateReservationStatusCommand(int reservationId, ReservationStatus newStatus, string? changedBy)
        {
            ReservationId = reservationId;
            NewStatus = newStatus;
            ChangedBy = changedBy;
        }
    }
}
