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
    public class СigarettesProduct : IDelete
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

        public CigarettesCategorie Categorie { get; set; } = null!;
        [ForeignKey(nameof(Categorie))]
        public int CategodyId { get; set; }

        public CigarettesManufacturer Manufacturer { get; set; } = null!;
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }

        public List<CigarettesPhoto> CigarettesPhotos { get; set; } = new List<CigarettesPhoto>();

        public bool IsDelete { get; set; } = false;
    }
}
