using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Querys
{
    public class GetProductsByCategoryQueryService : IQueryService<GetByIdDTOforQuery, Task<List<CigaretteProductDTO>>>
    {
        private readonly ICigaretteProductRepository _repository;
        public GetProductsByCategoryQueryService(ICigaretteProductRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }
        public async Task<List<CigaretteProductDTO>> Execute(GetByIdDTOforQuery obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            return await _repository.GetProductsByCategoryAsync(obj);
        }
    }
}
