using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain.Commands.Object
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string? Comment { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public decimal? TotalPrice { get; set; }
        public int ClientId { get; set; }
        public string? ClientEmail { get; set; }
        public List<ReservationItemDTO> Items { get; set; } = new();
        public List<ReservationStatusLogDTO> Logs { get; set; } = new();
        public bool IsDelete { get; set; }
    }
}
