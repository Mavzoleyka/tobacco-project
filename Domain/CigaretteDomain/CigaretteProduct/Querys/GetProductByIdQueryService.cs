using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Querys
{
    public class GetProductByIdQueryService : IQueryService<GetByIdDTO, Task<CigaretteProductDTO>>
    {
        private readonly ICigaretteProductRepository _repository;
        public GetProductByIdQueryService(ICigaretteProductRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<CigaretteProductDTO?> Execute(GetByIdDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            return await _repository.GetByIdAsync(obj);
        }
    }
}
