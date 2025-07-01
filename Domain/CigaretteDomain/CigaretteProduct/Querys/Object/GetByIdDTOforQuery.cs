using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Querys.Object
{
    public class GetByIdDTOforQuery : IQuery
    {
        public int CategoryId { get; set; }
    }
}
