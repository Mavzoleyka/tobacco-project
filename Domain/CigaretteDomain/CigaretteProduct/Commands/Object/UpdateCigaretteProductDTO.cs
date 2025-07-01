using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Commands.Object
{
    public class UpdateCigaretteProductDTO : AddCigaretteProductDTO
    {
        public int Id { get; set; }
        
    }
}
