using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;

namespace Domain.CigaretteDomain.CigaretteProduct.Commands.Object
{
    public class AddCigaretteProductDTO
    {
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(1000)]
        public string? Description { get; set; }
        public int? Stock { get; set; }
        [MaxLength(100)]
        public string? Brand { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public List<CigarettePhotoDTO> CigarettesPhotos { get; set; } = new List<CigarettePhotoDTO>();
    }
}
