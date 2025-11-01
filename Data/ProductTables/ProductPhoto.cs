using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteTables
{
    public class ProductPhoto : IDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string ImageURL { get; set; } = null!;
        [MaxLength(300)]
        public string? Caption { get; set; }
        public bool IsMain { get; set; } = false;
        public Product Product { get; set; } = null!;
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
