using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public class CreateReservationDTO
    {
        public int ClientId { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string? Comment { get; set; }
        public List<CreateReservationItemDTO> Items { get; set; } = new();
        public decimal? TotalPrice { get; set; }
    }
}
