using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Ready,
        PickedUp,
        Canceled,
        Expired
    }
}
