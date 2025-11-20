using Domain.UserDomain.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Notifications
{
    public interface IEmailNotificationService
    {
        public Task SendReservationStatusChangedEmailAsync(
        string toEmail,
        int reservationId,
        ReservationStatus newStatus,
        CancellationToken ct = default);
    }
}
