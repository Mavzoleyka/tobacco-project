using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public class ReservationItemDTO
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal? PriceAtBooking { get; set; }
    }
}
