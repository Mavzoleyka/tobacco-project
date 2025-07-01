using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Querys
{
    public class GetAllProductsQueryService : IQueryService<All, Task<List<CigaretteProductDTO>>>
    {
        private readonly ICigaretteProductRepository _repository;

        public GetAllProductsQueryService(ICigaretteProductRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<List<CigaretteProductDTO>> Execute(All obj)
        {
            return await _repository.GetAllAsync();
        }
    }
}
