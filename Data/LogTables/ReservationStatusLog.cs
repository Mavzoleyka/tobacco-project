using Domain.UserDomain.Commands.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LogTables
{
    public class ReservationStatusLog : IDelete
    {
        [Key]
        public int Id { get; set; }

        public ReservationStatus OldStatus { get; set; }
        public ReservationStatus NewStatus { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? ChangedBy { get; set; } 

        [ForeignKey(nameof(Reservation))]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public bool IsDelete { get; set; }
    }
}
