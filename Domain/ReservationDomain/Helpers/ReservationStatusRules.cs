using Domain.UserDomain.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReservationDomain.Helpers
{
    public static class ReservationStatusRules
    {
        private static readonly Dictionary<ReservationStatus, ReservationStatus[]> AllowedTransitions = new()
        {
            { ReservationStatus.Pending,   new[] { ReservationStatus.Confirmed, ReservationStatus.Canceled } },
            { ReservationStatus.Confirmed, new[] { ReservationStatus.Ready, ReservationStatus.Canceled } },
            { ReservationStatus.Ready,     new[] { ReservationStatus.PickedUp, ReservationStatus.Canceled } },
            { ReservationStatus.PickedUp,  Array.Empty<ReservationStatus>() },
            { ReservationStatus.Canceled,  Array.Empty<ReservationStatus>() },
            { ReservationStatus.Expired,   Array.Empty<ReservationStatus>() }
        };

        public static bool CanTransition(ReservationStatus current, ReservationStatus target)
        {
            if (!AllowedTransitions.ContainsKey(current))
                return false;

            return AllowedTransitions[current].Contains(target);
        }
    }
}
