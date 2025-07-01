using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteCategorie.Querys
{
    public class GetCategoryByIdQueryService : IQueryService<GetByIdDTO, Task<CigaretteCategoryDTO>>
    {
        private readonly ICigaretteCategoryRepository _repository;
        public GetCategoryByIdQueryService(ICigaretteCategoryRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<CigaretteCategoryDTO?> Execute(GetByIdDTO obj)
        {
            if(obj == null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }

            return await _repository.GetByIdAsync(obj);
            
        }
    }
}
