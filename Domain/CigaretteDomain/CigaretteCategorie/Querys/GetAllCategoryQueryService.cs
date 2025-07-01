using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteCategorie.Querys
{
    public class GetAllCategoryQueryService : IQueryService<All, Task<List<CigaretteCategoryDTO>>>
    {
        private readonly ICigaretteCategoryRepository _repository;
        public GetAllCategoryQueryService(ICigaretteCategoryRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<List<CigaretteCategoryDTO>> Execute(All obj)
        {
            return await _repository.GetAllAsync();
        }
    }
}
