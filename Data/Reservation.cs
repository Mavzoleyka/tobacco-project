using Data.CigaretteTables;
using Data.LogTables;
using Domain.UserDomain.Commands.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Reservation :IDelete
    {
        [Key, Required]
        public int Id { get; set; }
        public DateTime PickupDateTime { get; set; }
        [MaxLength(100)]
        
        public string? Comment {  get; set; }

        public ReservationStatus Status;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get;set; }

        [Precision(10, 2)]
        public decimal? TotalPrice { get; set; }
        [ForeignKey(nameof(Client))]
        public int ClientId {  get; set; }
        public Client Client { get; set; } = null!;

        public List<ReservationItem> Items { get; set; } = new();

        public ICollection<ReservationStatusLog> StatusLogs { get; set; } = new List<ReservationStatusLog>();

        public bool IsDelete { get ; set; }
    }
}
