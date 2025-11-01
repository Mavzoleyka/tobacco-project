using Data.CigaretteTables;
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
    public class ReservationItem : IDelete
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public int Quantity { get; set; }
        [Precision(10, 2)]
        public decimal? PriceAtBooking { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        [ForeignKey(nameof(Reservation))]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public bool IsDelete { get  ; set ; }
    }
}
