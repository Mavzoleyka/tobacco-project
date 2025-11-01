using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Commands.Object
{
    public class CigaretteProductDTO : AddCigaretteProductDTO
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public decimal? Price { get; set; }
    }
}
