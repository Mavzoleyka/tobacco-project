using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Notification : IDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Reservation? Reservation { get; set; }
        [ForeignKey(nameof(Reservation))]
        public int? ReservationId { get; set; }        
        public Client Client { get; set; } = null!;
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }                
        public bool IsDelete { get; set; }
    }
}
