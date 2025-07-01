using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CigaretteDomain.CigaretteManufacturer.Querys
{
    public class GeAllManufacturersQueryService : IQueryService<All, Task<List<CigaretteManufacturerDTO>>>
    {
        private readonly ICigaretteManufacturerRepository _repository;
        public GeAllManufacturersQueryService(ICigaretteManufacturerRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public async Task<List<CigaretteManufacturerDTO>> Execute(All obj)
        {
            return await _repository.GetAllAsync();
        }
    }
}
