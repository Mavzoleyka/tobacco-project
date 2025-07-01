using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteTables
{
    public class CigarettesCategorie : IDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public bool IsDelete { get; set; }
    }
}
