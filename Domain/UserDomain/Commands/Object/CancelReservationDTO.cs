using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public class CancelReservationDTO
    {
        public int ReservationId { get; set; }
        public string Status { get; set; } = null!;
    }
}
