using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteTables
{
    public class Product : IDelete
    {
        [Key]
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(200)]
        [Required]
        public string Name { get; set; } = null!;
        [MaxLength(1000)]
        public string? Description { get; set; }
        public int? Stock { get; set; }
        [MaxLength(100)]
        public string? Brand { get; set; }
        [Precision(10, 2)]
        public decimal? Price { get; set; }

        public ProductCategory Categorie { get; set; } = null!;
        [ForeignKey(nameof(Categorie))]
        public int CategodyId { get; set; }

        public Manufacturer Manufacturer { get; set; } = null!;
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }

        public List<ProductPhoto> CigarettesPhotos { get; set; } = new List<ProductPhoto>();

        public bool IsDelete { get; set; } = false;
    }
}
