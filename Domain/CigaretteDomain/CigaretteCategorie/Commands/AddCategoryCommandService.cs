using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteCategorie.Commands
{
    public class AddCategoryCommandService : ICommandService<AddCigaretteCategoryDTO>
    {
        private readonly ICigaretteCategoryRepository _repository;
        public AddCategoryCommandService(ICigaretteCategoryRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task Execute(AddCigaretteCategoryDTO obj)
        {
            if(obj==null)
            {
                ArgumentNullException.ThrowIfNull(obj, nameof(obj));
            }
            await _repository.AddAsync(obj);
        }
    }
}
