using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteProduct.Querys
{
    public class GetNameOfProductManufacturerQueryService
                                    : IQueryService<GetByIdDTOforQuery, Task<NameOfProductManufacturerDTO>>
    {
        private readonly ICigaretteProductRepository _repository;
        public GetNameOfProductManufacturerQueryService(ICigaretteProductRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<NameOfProductManufacturerDTO> Execute(GetByIdDTOforQuery obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            return await _repository.GetNameOfProductManufacturerAsync(obj);
        }
    }
}
