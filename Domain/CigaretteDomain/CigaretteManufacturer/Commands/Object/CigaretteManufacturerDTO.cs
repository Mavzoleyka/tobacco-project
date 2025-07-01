using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteCategorie.Commands.Object
{
    public class CigaretteManufacturerDTO
    {
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(300)]
        public string? Country { get; set; }
    }
}
