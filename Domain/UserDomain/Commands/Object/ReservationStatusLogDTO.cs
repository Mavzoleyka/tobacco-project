using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public class ReservationStatusLogDTO
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public ReservationStatus OldStatus { get; set; }

        public ReservationStatus NewStatus { get; set; }

        public DateTime ChangedAt { get; set; }

        public string ChangedBy { get; set; } = "System";
    }
}
