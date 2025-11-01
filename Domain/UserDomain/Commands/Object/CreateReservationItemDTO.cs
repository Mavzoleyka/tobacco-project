using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public class CreateReservationItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? PriceAtBooking { get; set; }
    }
}
