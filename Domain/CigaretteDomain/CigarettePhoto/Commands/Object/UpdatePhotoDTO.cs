using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigarettePhoto.Commands.Object
{
    public class UpdatePhotoDTO
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string ImageURL { get; set; } = null!;
        [MaxLength(300)]
        public string? Caption { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
